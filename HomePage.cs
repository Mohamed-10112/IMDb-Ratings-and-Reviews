using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace IMDBClone
{
    public partial class HomePage : Form
    {
        LoginPage form4;
        public HomePage()
        {
            InitializeComponent();
            form4 = new LoginPage(this);
        }
        //join us
        private void button2_Click(object sender, EventArgs e)
        {
            form4.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}