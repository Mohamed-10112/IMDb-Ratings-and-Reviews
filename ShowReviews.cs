using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMDBClone
{
    public partial class ShowReviews : Form
    {
        string ordb = "Data source=orcl;User Id=scott;Password=tiger;";
        OracleConnection conn;

        OracleDataAdapter adapter;
        DataSet ds;

        public ShowReviews()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            conn = new OracleConnection(ordb);
            conn.Open();
            fillComboBox();
        }
        public void fillComboBox()
        {
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "GetMovieNames";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("id", OracleDbType.RefCursor, ParameterDirection.Output);
            OracleDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string thisID = dr[0].ToString();
                comboBox1.Items.Add(thisID);
            }
            dr.Close();
        }
        //show
        private void button1_Click(object sender, EventArgs e)
        {
            string selectedMovieName = comboBox1.SelectedItem.ToString();
            int movieId = GetMovieIdByName(selectedMovieName);
            string constr = "User Id=scott;Password=tiger;Data Source=orcl";
            string cmdstr = "select review from reviews where MOVIEID = :movieId ";

            adapter = new OracleDataAdapter(cmdstr, constr);
            adapter.SelectCommand.Parameters.Add("movieId", movieId);
            ds = new DataSet();
            adapter.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }
        private int GetMovieIdByName(string movieName)
        {
            int movieId = -1;
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT movieid FROM movies WHERE moviename = :movieName";
            cmd.Parameters.Add("movieName", movieName);
            OracleDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                movieId = Convert.ToInt32(dr[0]);
            }
            dr.Close();
            return movieId;
        }
        //back
        private void button2_Click(object sender, EventArgs e)
        {
            UserApps form6 = new UserApps();
            form6.Show();
            this.Hide();
        }

        private void Form8_FormClosing(object sender, FormClosingEventArgs e)
        {
            ds.Dispose();
            conn.Dispose();
            Application.Exit();
        }


    }
}
