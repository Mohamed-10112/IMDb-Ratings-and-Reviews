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

namespace IMDBClone
{
    public partial class AdminPage: Form
    {
        OracleDataAdapter adapter;
        OracleCommandBuilder builder;
        DataSet ds;

        private Form _mainForm;

        public AdminPage(Form mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;
            this.StartPosition = FormStartPosition.CenterScreen;

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            _mainForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _mainForm.Show();
            this.Hide();
        }

        private void showInfo_Click(object sender, EventArgs e)
        {
            string constr = "Data Source=orcl; User Id=scott; Password=tiger;";
            string cmdstr = "";

            if (rdo_movies.Checked)
                cmdstr = "select * from movies";
            else if (rdo_users.Checked)
                cmdstr = "select * from users";

            adapter = new OracleDataAdapter(cmdstr, constr);
            ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void save_Click(object sender, EventArgs e)
        {
            builder = new OracleCommandBuilder(adapter);
            adapter.Update(ds.Tables[0]);
        }

        private void search_Click(object sender, EventArgs e)
        {
            string constr = "User Id=scott;Password=tiger;Data Source=orcl";
            string cmdstr = @"select MOVIENAME, RELEASEYEAR, MOVIEDURATION,SYNOPSIS
                 from movies m , MOVIECAST ca, MOVIEACTORS ac
                 where m.MOVIEID = ca.MOVIEID
                 and ac.ACTORID = ca.ACTORID and ACTORNAME = :n ";

            OracleDataAdapter adapter = new OracleDataAdapter(cmdstr, constr);
            adapter.SelectCommand.Parameters.Add("n", txt_name.Text);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (adapter != null)
                adapter.Dispose();
            if (ds != null)
                ds.Dispose();
            Application.Exit();
        }
    }
}
