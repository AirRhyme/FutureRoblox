using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Future
{

    public partial class Form1 : Form
    {
        FutureMain fm = new FutureMain();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://tinyurl.com/FutureKey");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            if(textBox1.Text == wc.DownloadString("https://pastebin.com/raw/CVDLHp7n"))
            {
                MessageBox.Show("Correct Key! Loading Future. . .");
                fm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong Key! Try Again");
            }
        }
    }
}
