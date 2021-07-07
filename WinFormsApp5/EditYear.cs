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
    public partial class EditYear : Form
    {
        public EditYear()
        {
            InitializeComponent();
        }
        string classes_id;
        private void EditYear_Load(object sender, EventArgs e)
        {
            MySqlCommand npgsql = new MySqlCommand("SELECT name FROM classes", Form1.connection);
            Form1.connection.Open();
            MySqlDataReader dataReader = npgsql.ExecuteReader();
            while (dataReader.Read()) comboBox1.Items.Add(dataReader[0]);
            Form1.connection.Close();
            if (Form2.k4 > -1)
            {
                comboBox1.Text = Form2.ds.Tables["РезультатыГода"].Rows[Form2.k4]["Класс"].ToString();
                textBox1.Text = Form2.ds.Tables["РезультатыГода"].Rows[Form2.k4]["Процент_успеваемости"].ToString();
                textBox2.Text = Form2.ds.Tables["РезультатыГода"].Rows[Form2.k4]["Качество_обучения"].ToString();
                textBox3.Text = Form2.ds.Tables["РезультатыГода"].Rows[Form2.k4]["Неуспевающие"].ToString();
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
            if (Form2.k4 == -1)
            {
                sql = "INSERT INTO result_for_year VALUES (" + classes_id + ", " + textBox1.Text + ", " + textBox2.Text + ", " + textBox3.Text + ")";
            }
            else
            {
                sql = "UPDATE result_for_year SET percent_learning = " + textBox1.Text + ", quality_learning = " + textBox2.Text + ", unlearning = " + textBox3.Text + " WHERE id = " + classes_id;
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
            Form2.ds.Tables["РезультатыГода"].Rows.Add(new object[] { comboBox1.Text, textBox1.Text, textBox2.Text, textBox3.Text });
            if (Form2.k4 > -1) Form2.ds.Tables["РезультатыГода"].Rows.RemoveAt(Form2.k4);
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
        private void EditYear_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form2.k4 = -1;
        }
    }
}
