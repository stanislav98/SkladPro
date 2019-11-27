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
    public partial class Spravka : Form
    {
        SkladProDb db = new SkladProDb();
        Message msg = new Message();
        public Spravka()
        {
            InitializeComponent();
        }

        private void Spravka_Load(object sender, EventArgs e)
        {
            dataGridView1.Hide();
            comboBox2.Hide();
            label2.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            ErrorForm err = new ErrorForm();
            try
            {
                if (comboBox1.SelectedIndex == 2)
                {
                    comboBox2.Show();
                    label2.Show();
                    dataGridView1.Show();
                    db.ShowCategories(ref comboBox2, ref dataGridView1);
                }
                else if (comboBox1.SelectedIndex == 0)
                {
                    comboBox2.Hide();
                    label2.Hide();
                    dataGridView1.Show();
                    db.ShowCustomers(ref dataGridView1);
                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    comboBox2.Hide();
                    label2.Hide();
                    dataGridView1.Show();
                    db.ShowSell(ref dataGridView1);
                    if (dataGridView1.Rows.Count == 0)
                    {
                        dataGridView1.Hide();
                        msg.materialLabel1.Text = "Няма въведени данни в таблицата !";
                        msg.Text = "Празна таблица !";
                        msg.materialFlatButton1.Text = "Okay";
                        msg.Show();
                    }
                }
            }
            catch
            {
                err.Show();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = comboBox2.SelectedIndex + 1;
            db.fillCategories(ref dataGridView1,ref i);
        }
    }
}
