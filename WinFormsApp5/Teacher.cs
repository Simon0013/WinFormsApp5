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
    public partial class Teacher : Form
    {
        public Teacher()
        {
            InitializeComponent();
        }
        string id;
        private void Teacher_Load(object sender, EventArgs e)
        {
            if (Form2.k2 > -1)
            {
                textBox1.Text = Form2.ds.Tables["Преподаватели"].Rows[Form2.k2]["ФИО"].ToString();
                id = Form2.ds.Tables["Преподаватели"].Rows[Form2.k2]["Номер"].ToString();
            }
            else
            {
                MySqlCommand npgsql = new MySqlCommand("SELECT MAX(id)+1 FROM teachers", Form1.connection);
                Form1.connection.Open();
                MySqlDataReader dataReader = npgsql.ExecuteReader();
                dataReader.Read();
                id = dataReader[0].ToString();
                Form1.connection.Close();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string sql;
            if (Form2.k2 == -1)
            {
                sql = "INSERT INTO teachers VALUES (" + id + ", '" + textBox1.Text + "')";
            }
            else
            {
                sql = "UPDATE teachers SET name = '" + textBox1.Text + "' WHERE id = " + id;
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
            Form2.ds.Tables["Преподаватели"].Rows.Add(new object[] { id, textBox1.Text });
            if (Form2.k2 > -1) Form2.ds.Tables["Преподаватели"].Rows.RemoveAt(Form2.k2);
        }
        private void Teacher_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form2.k2 = -1;
        }
    }
}
