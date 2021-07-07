using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WinFormsApp5
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public static DataSet ds = new DataSet();
        public static string[] quarter = { "quarter_one", "quarter_two", "quarter_three", "quarter_four" };
        public static string[] result_test = { "result_test_math", "result_test_russ" };
        public static int k1 = -1, k2 = -1, k3 = -1, k4 = -1;
        public static int mode1 = -1, mode2 = -1;
        public static void TableFill(string name, string sql)
        {
            Form1.connection.Open();
            if (ds.Tables[name] != null)
                ds.Tables[name].Clear();
            MySqlDataAdapter da;
            da = new MySqlDataAdapter(sql, Form1.connection);
            da.Fill(ds, name);
            Form1.connection.Close();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            TableFill("Преподаватели", "SELECT id AS Номер, name AS ФИО FROM teachers");
            dataGridView2.DataSource = ds.Tables["Преподаватели"];
            dataGridView2.CurrentCell = null;
            dataGridView2.AutoResizeColumns();
            TableFill("РезультатыГода", "SELECT name AS Класс, percent_learning AS Процент_успеваемости, quality_learning AS Качество_обучения, unlearning AS Неуспевающие FROM result_for_year INNER JOIN classes ON classes.id = result_for_year.id");
            dataGridView4.DataSource = ds.Tables["РезультатыГода"];
            dataGridView4.CurrentCell = null;
            dataGridView4.AutoResizeColumns();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Class cl = new Class();
            cl.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите элемент из списка!", "Результаты четвертей");
                return;
            }
            TableFill("РезультатыЧетверти", "SELECT name AS Класс, percent_learning AS Процент_успеваемости, quality_learning AS Качество_обучения, unlearning AS Неуспевающие FROM " + quarter[comboBox1.SelectedIndex] + " INNER JOIN classes ON classes.id = " + quarter[comboBox1.SelectedIndex] + ".id");
            dataGridView1.DataSource = ds.Tables["РезультатыЧетверти"];
            dataGridView1.CurrentCell = null;
            dataGridView1.AutoResizeColumns();
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            mode1 = comboBox1.SelectedIndex;
        }
        private void button9_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите элемент из списка!", "Результаты тестирования");
                return;
            }
            TableFill("РезультатыТестирования", "SELECT " + result_test[comboBox2.SelectedIndex] + ".id AS Номер, classes.name AS Класс, teachers.name AS Преподаватель, five AS Оценок_5, four AS Оценок_4, three AS Оценок_3, two AS Оценок_2 FROM " + result_test[comboBox2.SelectedIndex] + ", teachers, classes WHERE teachers.id = teacher_id AND classes.id = class_id");
            dataGridView3.DataSource = ds.Tables["РезультатыТестирования"];
            dataGridView3.CurrentCell = null;
            dataGridView3.Columns["Номер"].Visible = false;
            dataGridView3.AutoResizeColumns();
            button10.Enabled = true;
            button11.Enabled = true;
            button12.Enabled = true;
            mode2 = comboBox2.SelectedIndex;
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            k1 = dataGridView1.CurrentRow.Index;
        }
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            k2 = dataGridView2.CurrentRow.Index;
        }
        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            k3 = dataGridView3.CurrentRow.Index;
        }
        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            k4 = dataGridView4.CurrentRow.Index;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            k2 = -1;
            dataGridView2.CurrentCell = null;
            Teacher teacher = new Teacher();
            teacher.Show();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentCell == null)
            {
                MessageBox.Show("Выберите элемент из списка!", "Преподаватели");
                return;
            }
            dataGridView2.CurrentCell = null;
            Teacher teacher = new Teacher();
            teacher.Show();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentCell == null)
            {
                MessageBox.Show("Выберите элемент из списка!", "Преподаватели");
                return;
            }
            string warning = "Вы действительно хотите удалить эту запись?";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(warning, "Удаление", buttons);
            if (result == DialogResult.No) return;
            MySqlCommand npgsqlCommand = Form1.connection.CreateCommand();
            npgsqlCommand.CommandText = "DELETE FROM teachers WHERE id = " + ds.Tables["Преподаватели"].Rows[k2]["Номер"].ToString();
            Form1.connection.Open();
            npgsqlCommand.ExecuteNonQuery();
            Form1.connection.Close();
            ds.Tables["Преподаватели"].Rows.RemoveAt(k2);
        }
        private void button10_Click(object sender, EventArgs e)
        {
            k3 = -1;
            dataGridView3.CurrentCell = null;
            EditTest test = new EditTest();
            test.Show();
        }
        private void button11_Click(object sender, EventArgs e)
        {
            if (dataGridView3.CurrentCell == null)
            {
                MessageBox.Show("Выберите элемент из списка!", "Результаты Тестирования");
                return;
            }
            dataGridView3.CurrentCell = null;
            EditTest test = new EditTest();
            test.Show();
        }
        private void button12_Click(object sender, EventArgs e)
        {
            if (dataGridView3.CurrentCell == null)
            {
                MessageBox.Show("Выберите элемент из списка!", "Результаты Тестирования");
                return;
            }
            string warning = "Вы действительно хотите удалить эту запись?";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(warning, "Удаление", buttons);
            if (result == DialogResult.No) return;
            MySqlCommand npgsqlCommand = Form1.connection.CreateCommand();
            npgsqlCommand.CommandText = "DELETE FROM " + result_test[comboBox2.SelectedIndex] + " WHERE id = " + ds.Tables["РезультатыТестирования"].Rows[k3]["Номер"].ToString();
            Form1.connection.Open();
            npgsqlCommand.ExecuteNonQuery();
            Form1.connection.Close();
            ds.Tables["РезультатыТестирования"].Rows.RemoveAt(k3);
        }
        private void button13_Click(object sender, EventArgs e)
        {
            k4 = -1;
            dataGridView4.CurrentCell = null;
            EditYear year = new EditYear();
            year.Show();
        }
        private void button14_Click(object sender, EventArgs e)
        {
            if (dataGridView4.CurrentCell == null)
            {
                MessageBox.Show("Выберите элемент из списка!", "Результаты Года");
                return;
            }
            dataGridView4.CurrentCell = null;
            EditYear year = new EditYear();
            year.Show();
        }
        private void button15_Click(object sender, EventArgs e)
        {
            if (dataGridView4.CurrentCell == null)
            {
                MessageBox.Show("Выберите элемент из списка!", "Результаты Года");
                return;
            }
            string warning = "Вы действительно хотите удалить эту запись?";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(warning, "Удаление", buttons);
            if (result == DialogResult.No) return;
            MySqlCommand npgsqlCommand = Form1.connection.CreateCommand();
            npgsqlCommand.CommandText = "DELETE FROM result_for_year WHERE id = " + ds.Tables["РезультатыГода"].Rows[k4]["Класс"].ToString();
            Form1.connection.Open();
            npgsqlCommand.ExecuteNonQuery();
            Form1.connection.Close();
            ds.Tables["РезультатыГода"].Rows.RemoveAt(k4);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            k1 = -1;
            dataGridView1.CurrentCell = null;
            EditQuarter quarter = new EditQuarter();
            quarter.Show();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null)
            {
                MessageBox.Show("Выберите элемент из списка!", "Результаты четвертей");
                return;
            }
            dataGridView1.CurrentCell = null;
            EditQuarter quarter = new EditQuarter();
            quarter.Show();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null)
            {
                MessageBox.Show("Выберите элемент из списка!", "Результаты четвертей");
                return;
            }
            string warning = "Вы действительно хотите удалить эту запись?";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(warning, "Удаление", buttons);
            if (result == DialogResult.No) return;
            MySqlCommand npgsqlCommand = Form1.connection.CreateCommand();
            npgsqlCommand.CommandText = "DELETE FROM " + quarter[comboBox1.SelectedIndex] + " WHERE id = " + ds.Tables["РезультатыЧетверти"].Rows[k1]["Класс"].ToString();
            Form1.connection.Open();
            npgsqlCommand.ExecuteNonQuery();
            Form1.connection.Close();
            ds.Tables["РезультатыЧетверти"].Rows.RemoveAt(k1);
        }
    }
}
