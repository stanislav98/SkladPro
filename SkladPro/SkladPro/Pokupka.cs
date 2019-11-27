using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace SkladPro
{
    public partial class Pokupka : Form
    {
        public Pokupka()
        {
            InitializeComponent();
        }

        SkladProDb db = new SkladProDb();
        Regex checkNumbers = new Regex(@"^[0-9]+$");
        Message msg = new Message();
        ErrorForm err = new ErrorForm();

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = comboBox1.SelectedIndex + 1;
            db.fillCategories(ref dataGridView1, ref i);
        }

        private void Pokupka_Load(object sender, EventArgs e)
        {
            db.ShowCategories(ref comboBox1);
            db.ShowClients(ref comboBox2);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = comboBox2.SelectedIndex + 1;
            db.fillClients(ref dataGridView2, ref i);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //proverqvame dali poleto za kolichestvoto e prazno,0 i dali sudurja samo chisla,kakto i dali sa izbrani stoinosti ot combobox
                if (String.IsNullOrEmpty(textBox1.Text) || !checkNumbers.IsMatch(textBox1.Text) ||
                    comboBox1.SelectedIndex < 0 || comboBox2.SelectedIndex < 0 || textBox1.Text=="0")
                {
                    msg.Text = "Моля изберете правилни данни !";
                    msg.materialLabel1.Text = "ГРЕШКА ! ";
                    msg.Show();
                }
                else
                {
                    var today = string.Format("{0:MM-dd-yy}", DateTime.Now);
                    //get product info 
                    int ProductrowIndex = dataGridView1.CurrentCell.RowIndex;
                    string ProductID = dataGridView1.Rows[ProductrowIndex].Cells[0].Value.ToString();
                    string ProductName = dataGridView1.Rows[ProductrowIndex].Cells[1].Value.ToString();
                    int ProductQuantity = Int32.Parse(dataGridView1.Rows[ProductrowIndex].Cells[3].Value.ToString());
                    float ProductPrice = float.Parse(dataGridView1.Rows[ProductrowIndex].Cells[4].Value.ToString());
                    //get client info
                    int ClientrowIndex = dataGridView2.CurrentCell.RowIndex;
                    string ClientID = dataGridView2.Rows[ClientrowIndex].Cells[0].Value.ToString();
                    string ClientName =dataGridView2.Rows[ClientrowIndex].Cells[1].Value.ToString() +
                        dataGridView2.Rows[ClientrowIndex].Cells[2].Value.ToString();
                    string ClientCity = dataGridView2.Rows[ClientrowIndex].Cells[3].Value.ToString();
                    string ClientAddress = dataGridView2.Rows[ClientrowIndex].Cells[4].Value.ToString();
                    string ClientPhone = dataGridView2.Rows[ClientrowIndex].Cells[5].Value.ToString();
                    float ClientMoney = float.Parse(dataGridView2.Rows[ClientrowIndex].Cells[6].Value.ToString());
                    
                    int quantity = Int32.Parse(textBox1.Text);

                    //ako klienta nqma dostatuchno pari ili kolichestvoto v sklad e po malko ot vuvedenoto
                    if (ClientMoney < (ProductPrice * quantity) || ProductQuantity < quantity)
                    {
                        msg.Text = "Нямате достатъчно пари или сте въвели твърде голямо количество !";
                        msg.materialLabel1.Text = "ГРЕШКА ! ";
                        msg.Show();
                    }
                    else
                    {
                        float money = quantity * ProductPrice;
                        float newClientMoney = ClientMoney - money;
                        string newMoney = newClientMoney.ToString(CultureInfo.InvariantCulture);
                        string newQuantity = (ProductQuantity - quantity).ToString();
                        //zapisvame prodajbata
                        db.InsertSale(ClientID, ProductID, money.ToString(), today.ToString());
                        //zapisvame novite pari na klienta
                        db.UpdateClient(ref ClientID, ref newMoney);
                        //namalqvame kolichestvoto nalichnost vuv sklada za produkta
                        db.UpdateProductsAfterBuy(ref dataGridView2, ref ProductID, ref newQuantity);
                        int ID = Int32.Parse(ClientID);
                        //izvejdame obnovenite danni za klienta i produktite
                        db.fillClients(ref dataGridView2, ref ID);
                        int i = comboBox2.SelectedIndex + 1;

                        db.fillCategories(ref dataGridView1, ref i);

                        string fileName = @"D:\Faktura.docx";
                        string headlineText = "ФАКТУРА";
                        string paraOne = "БЛАГОДАРИМ ВИ ЗА ПОКУПКАТА!";

                        // A formatting object for our headline:
                        var headLineFormat = new Formatting();
                        headLineFormat.FontFamily = new Xceed.Document.NET.Font("Arial Black");
                        headLineFormat.Size = 18D;
                        headLineFormat.Position = 12;

                        // A formatting object for our normal paragraph text:
                        var paraFormat = new Formatting();
                        paraFormat.FontFamily = new Xceed.Document.NET.Font("Calibri");
                        paraFormat.Size = 10D;

                        // Create the document in memory:
                        var doc = DocX.Create(fileName);

                        // Insert the now text obejcts;
                        doc.InsertParagraph(headlineText, false, headLineFormat);
                        Table t = doc.AddTable(2,1);
                        Table t1 = doc.AddTable(2, 4);
                        Table t2 = doc.AddTable(2, 3);
                        doc.InsertParagraph("Данни за системата изготвяща фактурата", false, paraFormat);
                        t.Rows[0].Cells[0].Paragraphs.First().Append("SkladPro");
                        t.Rows[1].Cells[0].Paragraphs.First().Append(today);

                        t1.Rows[0].Cells[0].Paragraphs.First().Append("ИМЕ НА КЛИЕНТ");
                        t1.Rows[0].Cells[1].Paragraphs.First().Append("ГРАД");
                        t1.Rows[0].Cells[2].Paragraphs.First().Append("АДРЕС");
                        t1.Rows[0].Cells[3].Paragraphs.First().Append("Телефонен номер");

                        t1.Rows[1].Cells[0].Paragraphs.First().Append(ClientName);
                        t1.Rows[1].Cells[1].Paragraphs.First().Append(ClientCity);
                        t1.Rows[1].Cells[2].Paragraphs.First().Append(ClientAddress);
                        t1.Rows[1].Cells[3].Paragraphs.First().Append(ClientPhone);

                        t2.Rows[0].Cells[0].Paragraphs.First().Append("ПРОДУКТ");
                        t2.Rows[0].Cells[1].Paragraphs.First().Append("КОЛИЧЕСТВО");
                        t2.Rows[0].Cells[2].Paragraphs.First().Append("ЦЕНА");

                        t2.Rows[1].Cells[0].Paragraphs.First().Append(ProductName);
                        t2.Rows[1].Cells[1].Paragraphs.First().Append(quantity.ToString());
                        t2.Rows[1].Cells[2].Paragraphs.First().Append((quantity*ProductPrice).ToString());

                        doc.InsertParagraph("");
                        doc.InsertParagraph("");
                        doc.InsertTable(t);

                        doc.InsertParagraph("");
                        doc.InsertParagraph("");
                        doc.InsertTable(t1);

                        doc.InsertParagraph("");
                        doc.InsertParagraph("");
                        doc.InsertTable(t2);

                        doc.InsertParagraph("");
                        doc.InsertParagraph("");
                        doc.InsertParagraph(paraOne, false, paraFormat);

                        doc.InsertParagraph("");
                        doc.InsertParagraph("");
                        doc.InsertParagraph("Подпис..............", false, paraFormat);
                        
                        doc.Save();
                         
                        Process.Start("WINWORD.EXE", fileName);
                    }
                }
            }
            catch
            {
                err.Show();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
