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
    public partial class Class : Form
    {
        public Class()
        {
            InitializeComponent();
        }
        string id;
        private void Class_Load(object sender, EventArgs e)
        {
            Form2.TableFill("Классы", "SELECT id AS Номер, name AS Класс FROM classes");
            dataGridView1.DataSource = Form2.ds.Tables["Классы"];
            dataGridView1.CurrentCell = null;
            dataGridView1.AutoResizeColumns();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id = Form2.ds.Tables["Классы"].Rows[dataGridView1.CurrentRow.Index]["Номер"].ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            id = null;
            dataGridView1.CurrentCell = null;
            textBox1.Visible = true;
            button4.Visible = true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null)
            {
                MessageBox.Show("Выберите элемент из списка!", "Классы");
                return;
            }
            textBox1.Visible = true;
            button4.Visible = true;
            textBox1.Text = Form2.ds.Tables["Классы"].Rows[dataGridView1.CurrentRow.Index]["Класс"].ToString();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null)
            {
                MessageBox.Show("Выберите элемент из списка!", "Классы");
                return;
            }
            string warning = "Вы действительно хотите удалить эту запись?";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(warning, "Удаление", buttons);
            if (result == DialogResult.No) return;
            MySqlCommand npgsqlCommand = Form1.connection.CreateCommand();
            npgsqlCommand.CommandText = "DELETE FROM classes WHERE id = " + Form2.ds.Tables["Классы"].Rows[dataGridView1.CurrentRow.Index]["Номер"].ToString();
            Form1.connection.Open();
            npgsqlCommand.ExecuteNonQuery();
            Form1.connection.Close();
            Form2.ds.Tables["Классы"].Rows.RemoveAt(dataGridView1.CurrentRow.Index);
            dataGridView1.CurrentCell = null;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            MySqlCommand npgsql;
            string sql;
            if (id == null)
            {
                npgsql = new MySqlCommand("SELECT MAX(id)+1 FROM classes", Form1.connection);
                Form1.connection.Open();
                MySqlDataReader dataReader = npgsql.ExecuteReader();
                dataReader.Read();
                string new_id = dataReader[0].ToString();
                Form1.connection.Close();
                sql = "INSERT INTO classes VALUES (" + new_id + ", '" + textBox1.Text + "')";
            }
            else
            {
                sql = "UPDATE classes SET name = '" + textBox1.Text + "' WHERE id = " + id;
            }
            npgsql = new MySqlCommand(sql, Form1.connection);
            Form1.connection.Open();
            try
            {
                npgsql.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
                Form1.connection.Close();
                return;
            }
            Form1.connection.Close();
            Form2.ds.Tables["Классы"].Rows.Add(new object[] { id, textBox1.Text });
            if (id == null) Form2.ds.Tables["Классы"].Rows.RemoveAt(dataGridView1.CurrentRow.Index);
            dataGridView1.CurrentCell = null;
            id = null;
            textBox1.Visible = false;
            button4.Visible = false;
        }
    }
}
