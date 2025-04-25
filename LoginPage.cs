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
    public partial class LoginPage : Form
    {
        string ordb = "Data source=orcl;User Id=scott;Password=tiger;";
        OracleConnection conn;
        private Form _mainForm;
        public LoginPage(Form mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            _mainForm.Show();
            conn = new OracleConnection(ordb);
            conn.Open();
        }
        //register
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegisterPage form5 = new RegisterPage(this);
            form5.Show();
            this.Hide();

        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            conn.Dispose();
            Application.Exit();
        }

        //back
        private void button2_Click(object sender, EventArgs e)
        {
            HomePage form1 = new HomePage();
            form1.Show();
            this.Hide();
        }

        //login
        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            string password = textBox2.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both email and password.");
                return;
            }
            //if admin
            if (checkBox1.Checked)
            {
                bool isEmailValid = email.EndsWith("aadd@cc.com");
                bool isPasswordValid = password == "aadd123";

                if (isEmailValid && isPasswordValid)
                {
                    MessageBox.Show("Login successful!");
                    AdminPage form3 = new AdminPage(this);
                    form3.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid admin!");
                    checkBox1.Checked = false;
                }
            }
            else
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT USERID, USERNAME, EMAIL FROM USERS WHERE EMAIL = :email AND PASSWORD = :password";
                cmd.Parameters.Add("email", email);
                cmd.Parameters.Add("password", password);

                OracleDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Login.UserID = Convert.ToInt32(dr["USERID"]);
                    Login.Username = dr["USERNAME"].ToString();
                    Login.Email = dr["EMAIL"].ToString();

                    MessageBox.Show("Login successful!");

                    UserApps form6 = new UserApps();
                    form6.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid email or password!");
                    textBox1.Clear();
                    textBox2.Clear();
                }
                dr.Close();
            }


        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
