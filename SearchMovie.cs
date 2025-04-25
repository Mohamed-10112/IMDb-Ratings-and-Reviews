using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace IMDBClone
{
    public partial class SearchMovie: Form
    {

        string ordb = "Data source=orcl;User Id=scott;Password=tiger;";
        OracleConnection conn;
        private Form _mainForm;

        public SearchMovie(Form mainForm)
        {
            InitializeComponent();
           _mainForm = mainForm;
            this.Load += new EventHandler(Form2_Load);
        }

        public void Form2_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();
            fillComboBox();
        }

        public void fillComboBox()
        {
            OracleCommand c = new OracleCommand();
            c.Connection = conn;
            c.CommandText = "select MovieID from MOVIES";
            c.CommandType = CommandType.Text;
            OracleDataReader dr = c.ExecuteReader();

            while (dr.Read())
            {
                string thisID = dr[0].ToString();
                cmb_ID.Items.Add(thisID);
            }
            dr.Close();
        }

        private void MovieID_SelectedIndexChanged(object sender, EventArgs e)
        {
            OracleCommand c = new OracleCommand();
            c.Connection = conn;
            c.CommandText = "select MOVIENAME, SYNOPSIS, RELEASEYEAR, MOVIEDURATION from MOVIES where MOVIEID =:id";
            c.CommandType = CommandType.Text;
            c.Parameters.Add(":id", cmb_ID.SelectedItem.ToString());
            OracleDataReader dr = c.ExecuteReader();

            if (dr.Read())
            {
                txt_MName.Text = dr[0].ToString();
                richTextBox1.Text = dr[1].ToString();
                txt_MRLYear.Text = dr[2].ToString();
                txt_MDuration.Text = dr[3].ToString();

            }
            dr.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _mainForm.Show();
            this.Hide();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            conn.Dispose();
            Application.Exit();
        }
    }
}
