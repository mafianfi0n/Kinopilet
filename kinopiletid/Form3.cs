using Microsoft.VisualBasic;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Services.Description;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

namespace kinopiletid
{
    public partial class Form3 : Form
    {
        public SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True;Connect Timeout=30");
        string name;
        public Form3(string Name)
        {
            name = Name;
            InitializeComponent();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        List<string> arr_pilet;
        Image red = Image.FromFile(AppContext.BaseDirectory + "red.png");
        Image yellow = Image.FromFile(AppContext.BaseDirectory + "yellow.png");
        Image green = Image.FromFile(AppContext.BaseDirectory + "green.png");
        Label[,] _arr = new Label[4, 4];
        Label[] read = new Label[4];

        private void Form3_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 4; i++)
            {
                read[i] = new Label();
                read[i].Text = "Rida" + (1 + i);
                read[i].ForeColor = Color.White;
                read[i].BackColor = Color.Transparent;
                read[i].Size = new Size(50, 50);
                read[i].Location = new Point(220, i * 50 + 150);
                this.Controls.Add(read[i]);
                for (int j = 0; j < 4; j++)
                {
                    _arr[i, j] = new Label();
                    _arr[i, j].Image = green;


                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                        var commandStr = "SELECT x,y from " + name;
                        SqlCommand command = new SqlCommand(commandStr, con);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (Convert.ToInt32(reader["x"]) == i && Convert.ToInt32(reader["y"]) == j)
                                {
                                    _arr[i, j].Image = red;
                                }
                            }
                        }

                        con.Close();
                    }

                    _arr[i, j].Size = new Size(50, 50);
                    _arr[i, j].BackColor = Color.Transparent;
                    _arr[i, j].Location = new Point(j * 50 + 270, i * 50 + 150);
                    this.Controls.Add(_arr[i, j]);
                    _arr[i, j].Tag = new int[] { i, j };
                    _arr[i, j].Click += new System.EventHandler(Form3_Click);

                }

            }
        }

        private void Form3_Click(object sender, EventArgs e)
        {
            int[] I = { }, J = { };
            var label = (Label)sender;
            var tag = (int[])label.Tag;
            if (_arr[tag[0], tag[1]].Image == red)
            {
                MessageBox.Show("This seat taken");
            }
            else
            {
                _arr[tag[0], tag[1]].Image = yellow;
                arr_pilet.Add("Pilet" + I.ToString() + "rida" + J.ToString() + "koht");

            }
            I.Append(i);
            J.Append(j);
        }
        private void label2_Click(object sender, EventArgs e)
        {
            var vastus = MessageBox.Show("Are you sure of your choice", "Cinema asks", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (vastus == DialogResult.Yes)
            {
                int t = 0;
                int[] I = { }, J = { };

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (_arr[i, j].Image == yellow)
                        {
                            t++;
                            _arr[i, j].Image = red;
                            if (con.State == ConnectionState.Closed)
                            {
                                con.Open();
                                var commandStr = "INSERT Into " + name + "(x,y) values (" + i + "," + j + ")";
                                using (SqlCommand command = new SqlCommand(commandStr, con))
                                    command.ExecuteNonQuery();

                                con.Close();
                            }

                            I.Append(i);
                            J.Append(j);
                        }
                    }
                }
                try
                {
                    string mailAd = Interaction.InputBox("Sisesta e-mail", "Kuhu saada", "Aleksandr.Ivanov.230600@gmail.com");
                    MailMessage mail = new MailMessage();
                    SmtpClient stmpClient = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential("Aleksandr.Ivanov.230600@gmail.com", "55s95a87n89ja"),
                        EnableSsl = true
                    };
                    mail.To.Add(mailAd);
                    mail.From = new MailAddress("mr.voron.voron@mail.ru");
                    mail.Subject = "Pilet";
                    mail.Body = "";
                    foreach (var item in arr_pilet)
                    {
                        Attachment data = new Attachment(item);
                        mail.Attachments.Add(data);
                    }
                    stmpClient.Send(mail);
                    foreach (var item in arr_pilet)
                    {
                        File.Delete(item);
                    }
                }
                catch (Exception)
                {
                    foreach (var item in arr_pilet)
                    {
                        File.Delete(item);
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
