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
    public partial class AddReview : Form
    {
        string ordb = "Data source=orcl;User Id=scott;Password=tiger;";
        OracleConnection conn;
        public AddReview()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
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
        private void label2_Click(object sender, EventArgs e)
        {

        }

        //add
        private void button1_Click(object sender, EventArgs e)
        {
            string selectedMovieName = comboBox1.SelectedItem.ToString();
            int movieId = GetMovieIdByName(selectedMovieName);
            int userId = GetLoggedInUserId();
            string reviewContent = richTextBox1.Text;
            AddReviewToDatabase(movieId, userId, reviewContent);
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
        private int GetLoggedInUserId()
        {
            return Login.UserID;
        }
        private void AddReviewToDatabase(int movieId, int userId, string reviewContent)
        {
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "AddReview";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("movie_id", movieId);
                cmd.Parameters.Add("user_id", userId);
                cmd.Parameters.Add("user_review", reviewContent);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Review added successfully!");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to add review: " + ex.Message);
            }
        }
        //back
        private void button2_Click(object sender, EventArgs e)
        {
            UserApps form6 = new UserApps();
            form6.Show();
            this.Hide();
        }

        private void Form7_FormClosing(object sender, FormClosingEventArgs e)
        {
            conn.Dispose();
            Application.Exit();
        }
    }
}
