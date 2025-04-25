using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace IMDBClone
{
    public partial class RegisterPage : Form
    {
        string ordb = "Data source=orcl;User Id=scott;Password=tiger;";
        OracleConnection conn;

        LoginPage form4;
        private Form _mainForm;
        public RegisterPage(Form mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;
            form4 = new LoginPage(this);

        }

        private void Form5_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            form4.Show();
            this.Hide();
        }

        //register
        private void button1_Click(object sender, EventArgs e)
        {
            int maxID, newID;
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "GetUserID";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("id", OracleDbType.Int32, ParameterDirection.Output);
            cmd.ExecuteNonQuery();
            try
            {
                maxID = Convert.ToInt32(cmd.Parameters["id"].Value.ToString());
                newID = maxID + 1;
            }
            catch
            {
                newID = 1;
            }

            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }
            string email = textBox2.Text;

            if (!email.Contains('@') || !email.Contains('.'))
            {
                MessageBox.Show("Please enter a valid email!");
                textBox2.Clear();
                return;
            }
            OracleCommand cmd2 = new OracleCommand();
            cmd2.Connection = conn;
            cmd2.CommandText = "insert into USERS values (:id,:name,:email,:password)";
            cmd2.Parameters.Add("id", newID);
            cmd2.Parameters.Add("name", textBox1.Text);
            cmd2.Parameters.Add("email", textBox2.Text);
            cmd2.Parameters.Add("password", textBox3.Text);
            int r = cmd2.ExecuteNonQuery();
            if (r != -1)
            {
                MessageBox.Show("Registration successful!");
                form4.Show();
                this.Hide();
            }
        }
        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            conn.Dispose();
            Application.Exit();
        }
    }
}
