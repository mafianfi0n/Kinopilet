using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kinopiletid
{
    public partial class Form3 : Form
    {

        //public static async Task<SqlConnection> GetSqlConnection()
        //{
        //    var connectionString = ConfigurationManager.ConnectionStrings["SQLConnectionString01"];
        //    var sqlConnection = new SqlConnection(connectionString.ToString());
        //    await sqlConnection.OpenAsync();
        //    return sqlConnection;
        //}
        public Form3()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        Image red = Image.FromFile("red.png");
        Image yellow = Image.FromFile("yellow.png");
        Image green = Image.FromFile("green.png");
        Label[,] _arr = new Label[4, 4];
        Label[] read = new Label[4];
        private void Form3_Load(object sender, EventArgs e)
        {
            for (int i=0; i<4; i++)
            {
                read[i] = new Label();
                read[i].Text = "Rida" +  (1 + i);
                read[i].ForeColor = Color.White;
                read[i].BackColor = Color.Transparent;
                read[i].Size = new Size(50, 50);
                read[i].Location = new Point(220, i * 50+150);
                this.Controls.Add(read[i]);
                for (int j=0; j<4; j++)
                {
                    _arr[i, j] = new Label();
                    _arr[i, j].Image = green;
                    _arr[i, j].Size = new Size(50, 50);
                    _arr[i, j].BackColor = Color.Transparent;
                    _arr[i, j].Location = new Point(j*50+270,i * 50 + 150);
                    this.Controls.Add(_arr[i, j]);
                    _arr[i, j].Tag = new int[] { i, j };
                    _arr[i, j].Click +=  new System.EventHandler(Form3_Click);

                 }

            }
        }//Image.FromFile("..\\Puzzle\\Img\\444.jpg");

        private void Form3_Click(object sender, EventArgs e)
        {
            var label = (Label)sender;
            var tag = (int[])label.Tag;
            if(_arr[tag[0], tag[1]].Image == red)
            {
                MessageBox.Show("This seat taken");
            }
            else
            {
                _arr[tag[0], tag[1]].Image = yellow;
            }
        }

        private async void label2_Click(object sender, EventArgs e)
        {
            var vastus = MessageBox.Show("Are you sure of your choice", "Cinema asks", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if( vastus == DialogResult.Yes)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (_arr[i, j].Image == yellow)
                        {
                            _arr[i, j].Image = red;
                            //var sqlConnection = await GetSqlConnection();
                            //var command = new SqlCommand("INSERT INTO [kinopiletid] (row) VALUES (_arr[i])",
                            //    sqlConnection);
                            //var command2 = new SqlCommand("INSERT INTO [kinopiletid] (row) VALUES (_arr[j])",
                            //    sqlConnection);

                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (_arr[i, j].Image == yellow)
                        {
                            _arr[i, j].Image = green;
                        }
                    }
                }
            }
        }
    }
}
