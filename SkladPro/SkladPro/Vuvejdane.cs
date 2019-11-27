using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkladPro
{
    public partial class Vuvejdane : Form
    {
        SkladProDb db = new SkladProDb();
        ErrorForm err = new ErrorForm();
        Message msg = new Message();

        public Vuvejdane()
        {
            InitializeComponent();
        }


    private void Vuvejdane_Load(object sender, EventArgs e)
        {
            groupBox1.Hide();
        }
        protected void textBoxChanged(object sender, EventArgs e)
        {

        }

        //funkciq za izchustvane na textBoxovete
        public void ClearTextBoxes()
        {
            Action<Control.ControlCollection> func = null;
            func = (controls) =>
            {
                foreach (Control control in controls)
                    if (control is TextBox)
                        (control as TextBox).Clear();
                    else
                        func(control.Controls);
            };
            func(Controls);
        }
        Regex checkNumbers = new Regex(@"^[0-9]+$");
        Regex checkLetters = new Regex(@"^[a-zA-Z]+$");
        Regex checkDoubles = new Regex(@"^[0-9]\d{0,9}(\.\d{1,2})?%?$");

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
       
            try
            {
                var textBoxes = Controls.OfType<TextBox>().Where(textBox => textBox.Name.StartsWith("text"));
                if (comboBox1.SelectedIndex == 0)
                {
                    ClearTextBoxes();
                    groupBox1.Show();
                    groupBox3.Show();

                    label2.Text = "Име";
                    label3.Text = "Фамилия";
                    label4.Text = "Град";
                    label5.Text = "Адрес";
                    label6.Text = "Телефон";
                    label7.Text = "Пари";
                }
                if (comboBox1.SelectedIndex == 1)
                { 
                    ClearTextBoxes();
                    label2.Text = "Име на продукта";
                    label3.Text = "Количество";
                    label4.Text = "Цена";
                    label5.Text = "Категория";
                    groupBox1.Show();
                    groupBox2.Show();
                    groupBox3.Hide();
                    groupBox4.Show();
                }
            }
            catch
            {
                MessageBox.Show("error");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
        }
        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    //proverka za pravilno vuvedeni danni v textBoxovete pri vuvejdane na klient 
                    textBox5.MaxLength = 10;
                    if (!checkLetters.IsMatch(textBox4.Text) || !checkDoubles.IsMatch(textBox6.Text) ||
                        !checkNumbers.IsMatch(textBox5.Text) || !checkLetters.IsMatch(textBox2.Text) ||
                        !checkLetters.IsMatch(textBox3.Text) || !checkLetters.IsMatch(textBox1.Text) ||
                        textBox5.Text.Length < 10 || textBox5.Text.Length > 10)
                    {
                        msg.materialLabel1.Text = "Моля въведете коректни данни !";
                        msg.Text = "ЕRROR!";
                        msg.materialFlatButton1.Text = "Затвори";
                        msg.Show();
                        ClearTextBoxes();
                    }
                    else
                    {
                        try
                        {
                            //vuvejdane na klienta
                            db.InsertClient(textBox1.Text, textBox2.Text, textBox3.Text,
                            textBox4.Text, textBox5.Text, textBox6.Text);
                            msg.Text = "Клиент";
                            msg.materialLabel1.Text = "Успешно въведохте клиент";
                            msg.Show();
                            ClearTextBoxes();
                        }
                        catch
                        {
                            err.Show();
                        }
                    }
                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    //validaciq na vuvedenite danni za produkt
                    if (!checkLetters.IsMatch(textBox1.Text) || !checkNumbers.IsMatch(textBox3.Text) ||
                        !checkNumbers.IsMatch(textBox2.Text) || !checkNumbers.IsMatch(textBox4.Text))
                    {
                        msg.materialLabel1.Text = "Моля въведете коректни данни !";
                        msg.Text = "ЕRROR!";
                        msg.materialFlatButton1.Text = "Затвори";
                        msg.Show();
                        ClearTextBoxes();
                    }
                    else
                    {
                        //MessageBox.Show(textBox1.Text + "\n" + textBox2.Text + "\n" + textBox3.Text + "\n" + textBox4.Text + "\n");
                        try
                        {
                            //vuvejdane na produkta v bazata danni
                            db.InsertProduct(textBox1.Text, textBox2.Text, 
                            textBox3.Text, textBox4.Text);
                            msg.Text = "Продукт";
                            msg.materialLabel1.Text = "Успешно въведохте продукт";
                            msg.Show();
                            ClearTextBoxes();
                        }
                        catch
                        {
                            err.Show();
                        }
                    }
                }
            }
            catch
            {
                err.Show();
            }
        }
    }
}
