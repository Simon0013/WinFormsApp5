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
    public partial class EditTest : Form
    {
        public EditTest()
        {
            InitializeComponent();
        }
        string class_id, teacher_id, id;
        private void EditTest_Load(object sender, EventArgs e)
        {
            MySqlCommand npgsql = new MySqlCommand("SELECT name FROM classes", Form1.connection);
            Form1.connection.Open();
            MySqlDataReader dataReader = npgsql.ExecuteReader();
            while (dataReader.Read()) comboBox1.Items.Add(dataReader[0]);
            Form1.connection.Close();
            npgsql = new MySqlCommand("SELECT name FROM teachers", Form1.connection);
            Form1.connection.Open();
            dataReader = npgsql.ExecuteReader();
            while (dataReader.Read()) comboBox2.Items.Add(dataReader[0]);
            Form1.connection.Close();
            if (Form2.k3 > -1)
            {
                comboBox1.Text = Form2.ds.Tables["РезультатыТестирования"].Rows[Form2.k3]["Класс"].ToString();
                comboBox2.Text = Form2.ds.Tables["РезультатыТестирования"].Rows[Form2.k3]["Преподаватель"].ToString();
                textBox1.Text = Form2.ds.Tables["РезультатыТестирования"].Rows[Form2.k3]["Оценок_5"].ToString();
                textBox2.Text = Form2.ds.Tables["РезультатыТестирования"].Rows[Form2.k3]["Оценок_4"].ToString();
                textBox3.Text = Form2.ds.Tables["РезультатыТестирования"].Rows[Form2.k3]["Оценок_3"].ToString();
                textBox4.Text = Form2.ds.Tables["РезультатыТестирования"].Rows[Form2.k3]["Оценок_2"].ToString();
                id = Form2.ds.Tables["РезультатыТестирования"].Rows[Form2.k3]["Номер"].ToString();
                npgsql = new MySqlCommand("SELECT id FROM classes WHERE name = '" + comboBox1.Text + "'", Form1.connection);
                Form1.connection.Open();
                dataReader = npgsql.ExecuteReader();
                dataReader.Read();
                class_id = dataReader[0].ToString();
                Form1.connection.Close();
                npgsql = new MySqlCommand("SELECT id FROM teachers WHERE name = '" + comboBox2.Text + "'", Form1.connection);
                Form1.connection.Open();
                dataReader = npgsql.ExecuteReader();
                dataReader.Read();
                teacher_id = dataReader[0].ToString();
                Form1.connection.Close();
            }
            else
            {
                npgsql = new MySqlCommand("SELECT MAX(id)+1 FROM " + Form2.result_test[Form2.mode2], Form1.connection);
                Form1.connection.Open();
                dataReader = npgsql.ExecuteReader();
                dataReader.Read();
                id = dataReader[0].ToString();
                Form1.connection.Close();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string sql;
            if (Form2.k3 == -1)
            {
                sql = "INSERT INTO " + Form2.result_test[Form2.mode2] + " VALUES (" + id + ", " + class_id + ", " + teacher_id + ", " + textBox1.Text + ", " + textBox2.Text + ", " + textBox3.Text + ", " + textBox4.Text + ")";
            }
            else
            {
                sql = "UPDATE " + Form2.result_test[Form2.mode2] + " SET class_id = " + class_id + ", teacher_id = " + teacher_id + ", five = " + textBox1.Text + ", four = " + textBox2.Text + ", three = " + textBox3.Text + ", two = " + textBox4.Text + " WHERE id = " + id;
            }
            MySqlCommand npgsql = new MySqlCommand(sql, Form1.connection);
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
            Form2.ds.Tables["РезультатыТестирования"].Rows.Add(new object[] { id, comboBox1.Text, comboBox2.Text, textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text });
            if (Form2.k3 > -1) Form2.ds.Tables["РезультатыТестирования"].Rows.RemoveAt(Form2.k3);
        }
        private void EditTest_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form2.k3 = -1;
        }
        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            MySqlCommand npgsql = new MySqlCommand("SELECT id FROM classes WHERE name = '" + comboBox1.Text + "'", Form1.connection);
            Form1.connection.Open();
            MySqlDataReader dataReader = npgsql.ExecuteReader();
            dataReader.Read();
            class_id = dataReader[0].ToString();
            Form1.connection.Close();
        }
        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            MySqlCommand npgsql = new MySqlCommand("SELECT id FROM teachers WHERE name = '" + comboBox2.Text + "'", Form1.connection);
            Form1.connection.Open();
            MySqlDataReader dataReader = npgsql.ExecuteReader();
            dataReader.Read();
            teacher_id = dataReader[0].ToString();
            Form1.connection.Close();
        }
    }
}
