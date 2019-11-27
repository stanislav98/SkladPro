using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkladPro
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
         
        }

        private void aboutToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Message msg = new Message();
            msg.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Vuvejdane vuvejdane = new Vuvejdane();
            vuvejdane.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Pokupka pokupka = new Pokupka();
            pokupka.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Spravka spravka = new Spravka();
            spravka.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Korekciq korekciq = new Korekciq();
            korekciq.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
