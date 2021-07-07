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
    public partial class EditQuarter : Form
    {
        public EditQuarter()
        {
            InitializeComponent();
        }
        string classes_id;
        private void EditQuarter_Load(object sender, EventArgs e)
        {
            MySqlCommand npgsql = new MySqlCommand("SELECT name FROM classes", Form1.connection);
            Form1.connection.Open();
            MySqlDataReader dataReader = npgsql.ExecuteReader();
            while (dataReader.Read()) comboBox1.Items.Add(dataReader[0]);
            Form1.connection.Close();
            if (Form2.k1 > -1)
            {
                comboBox1.Text = Form2.ds.Tables["РезультатыЧетверти"].Rows[Form2.k1]["Класс"].ToString();
                textBox1.Text = Form2.ds.Tables["РезультатыЧетверти"].Rows[Form2.k1]["Процент_успеваемости"].ToString();
                textBox2.Text = Form2.ds.Tables["РезультатыЧетверти"].Rows[Form2.k1]["Качество_обучения"].ToString();
                textBox3.Text = Form2.ds.Tables["РезультатыЧетверти"].Rows[Form2.k1]["Неуспевающие"].ToString();
                npgsql = new MySqlCommand("SELECT id FROM classes WHERE name = '" + comboBox1.Text + "'", Form1.connection);
                Form1.connection.Open();
                dataReader = npgsql.ExecuteReader();
                dataReader.Read();
                classes_id = dataReader[0].ToString();
                Form1.connection.Close();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string sql;
            if (Form2.k1 == -1)
            {
                sql = "INSERT INTO " + Form2.quarter[Form2.mode1] + " VALUES (" + classes_id + ", " + textBox1.Text + ", " + textBox2.Text + ", " + textBox3.Text + ")";
            }
            else
            {
                sql = "UPDATE " + Form2.quarter[Form2.mode1] + " SET percent_learning = " + textBox1.Text + ", quality_learning = " + textBox2.Text + ", unlearning = " + textBox3.Text + " WHERE id = " + classes_id;
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
            Form2.ds.Tables["РезультатыЧетверти"].Rows.Add(new object[] {comboBox1.Text, textBox1.Text, textBox2.Text, textBox3.Text});
            if (Form2.k1 > -1) Form2.ds.Tables["РезультатыЧетверти"].Rows.RemoveAt(Form2.k1);
        }
        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            MySqlCommand npgsql = new MySqlCommand("SELECT id FROM classes WHERE name = '" + comboBox1.Text + "'", Form1.connection);
            Form1.connection.Open();
            MySqlDataReader dataReader = npgsql.ExecuteReader();
            dataReader.Read();
            classes_id = dataReader[0].ToString();
            Form1.connection.Close();
        }
        private void EditQuarter_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form2.k1 = -1;
        }
    }
}
