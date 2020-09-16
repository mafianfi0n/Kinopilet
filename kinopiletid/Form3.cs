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

namespace kinopiletid
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        string[] arr_pilet;
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
        }

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
        private void label2_Click(object sender, EventArgs e)
        {
            var vastus = MessageBox.Show("Are you sure of your choice", "Cinema asks", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if( vastus == DialogResult.Yes)
            {
                int t = 0;
                
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (_arr[i, j].Image == yellow)
                        {
                            t++;
                            _arr[i, j].Image = red;
                            //StreamWriter pilet = new StreamWriter("Pilet" + (t).ToString() + "Rida" + i.ToString() + "Koht" + j.ToString() + ".txt");
                            //arr_pilet.Append<string>("Pilet" + (t).ToString() + "Rida" + i.ToString() + "Koht" + j.ToString() + ".txt");
                            //pilet.WriteLine(arr_pilet[t-1]);
                            //pilet.Close();
                            send_mail(i,j);
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

        private void send_mail(int i, int j)
        {
            string adress = Interaction.InputBox("Sisesta e-mail", "Kuhu saada", "aleks.iva.2000@mail.ru");
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpClient = new SmtpClient("smtp@gmail.com")
                {
                    Port = 587,
                    Credentials = new System.Net.NetworkCredential("aleksandr.ivanov.230600@gmail.com","55s95a87n89ja"),
                    EnableSsl = true
                };
                mail.From = new MailAddress("aleksandr.ivanov.230600@gmail.com");
                mail.To.Add(adress);
                mail.Subject = "Pilet";
                mail.Body = "Rida... Koht...";
                smtpClient.Send(mail);
                MessageBox.Show("Pilet oli saadetud mailile:" + adress);
            }
            catch (Exception)
            {
                //MessageBox.Show("Viga");
            }
        }
    }
}
