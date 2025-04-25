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
    public partial class UserApps : Form
    {
        public UserApps()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }

        //search
        private void button1_Click(object sender, EventArgs e)
        {
            SearchMovie form2 = new SearchMovie(this);
            form2.Show();
            this.Hide();
        }

        //add review
        private void button2_Click(object sender, EventArgs e)
        {
            AddReview form7 = new AddReview();
            form7.Show();
            this.Hide();
        }

        //show reviews
        private void button3_Click(object sender, EventArgs e)
        {
            ShowReviews form8 = new ShowReviews();
            form8.Show();
            this.Hide();
        }

        //back
        private void button4_Click(object sender, EventArgs e)
        {
            LoginPage form4 = new LoginPage(this);
            form4.Show();
            this.Hide();
        }

        private void Form6_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
