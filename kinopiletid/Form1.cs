﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kinopiletid
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3("grom");
            form3.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3("mstiteli");
            form3.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3("iterste");
            form3.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3("sonic");
            form3.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3("showman");
            form3.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
