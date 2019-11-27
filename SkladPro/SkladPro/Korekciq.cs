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
    public partial class Korekciq : Form
    {
        public Korekciq()
        {
            InitializeComponent();
        }

        SkladProDb db = new SkladProDb();
        Message msg = new Message();
        ErrorForm err = new ErrorForm();

        private void tabPage2_Click(object sender, EventArgs e)
        {
          
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
         
        }

        private void Korekciq_Load(object sender, EventArgs e)
        {
            //izvejdane na produkti,kategorii i klienti
            db.ShowCustomers(ref dataGridView1);
            db.ShowProducts(ref dataGridView2);
            db.ShowCategories(ref dataGridView3);
            //dobavqne na event pri smqna na stoinostta v nqkoi CELL 
            dataGridView3.CellValueChanged += new DataGridViewCellEventHandler(dataGridView3_CellValueChanged);
            dataGridView2.CellValueChanged += new DataGridViewCellEventHandler(dataGridView2_CellValueChanged);
            dataGridView1.CellValueChanged += new DataGridViewCellEventHandler(dataGridView1_CellValueChanged);
          
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                db.InsertCategory(textBox11.Text);
                db.ShowCategories(ref dataGridView3);
            }
            catch
            {
                err.Show();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            { 
                db.dataBase = "Category";
                int rowIndex = dataGridView3.CurrentCell.RowIndex;
                string ID = dataGridView3.Rows[rowIndex].Cells[0].Value.ToString();
                db.DeleteCategory(ref dataGridView3, ref ID,db.dataBase);
                db.ResetID(db.dataBase);
                db.ShowCategories(ref dataGridView3);

            }
            catch
            {
                err.Show();
            }

        }

        //UPDATE NA STOINOSTITE SLED SMQNA V POLETO
        private void dataGridView3_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            string value = dataGridView3[e.ColumnIndex, e.RowIndex].Value.ToString();
            int rowIndex = e.RowIndex;
            string ID = dataGridView3.Rows[rowIndex].Cells[0].Value.ToString();
            db.UpdateCategory(ref dataGridView3, ref ID,ref value);
            db.ShowCategories(ref dataGridView3);
        }
        //UPDATE NA STOINOSTITE SLED SMQNA V POLETO
        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try {
                string val = dataGridView2[e.ColumnIndex, e.RowIndex].Value.ToString();
                int rowIndex = e.RowIndex;
                string ID = dataGridView2.Rows[rowIndex].Cells[0].Value.ToString();
                string update = "";
                if (e.ColumnIndex == 1) { update = "Name"; }
                if (e.ColumnIndex == 2) { update = "ID_Category"; }
                if (e.ColumnIndex == 3) { update = "Quantity"; }
                if (e.ColumnIndex == 4) { update = "Price"; }
                int value = 0;
                if (e.ColumnIndex == 2)
                {
                    value = Int32.Parse(dataGridView2[e.ColumnIndex, e.RowIndex].Value.ToString());
                    List<int> available = db.GetAvailableCategoriesID();
                    if (available.Contains(value))
                    {
                        val = value.ToString();
                        db.UpdateProducts(ref dataGridView2, ref ID, ref val, update);
                        db.ShowProducts(ref dataGridView2);
                    }
                    else
                    {
                        msg.materialLabel1.Text = "Не сеществува такава категория!";
                        msg.Text = "Изберете правилна категория !";
                        msg.materialFlatButton1.Text = "Окей";
                        msg.Location = this.PointToScreen(new Point(450, 350));
                        msg.Show();
                    }
                }
                else
                {
                    db.UpdateProducts(ref dataGridView2, ref ID, ref val, update);
                    db.ShowProducts(ref dataGridView2);
                }
                dataGridView2.Columns["ID_Articules"].ReadOnly = true;
            }
            catch
            {
                err.Show();
            }
            
        }
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                db.dataBase = "Articules";
                int rowIndex = dataGridView2.CurrentCell.RowIndex;
                string ID = dataGridView2.Rows[rowIndex].Cells[0].Value.ToString();
                db.DeleteCategory(ref dataGridView2, ref ID, db.dataBase);
                db.ResetID(db.dataBase);
                db.ShowProducts(ref dataGridView2);
            }
            catch
            {
                err.Show();
            }

        }

        //UPDATE NA STOINOSTITE SLED SMQNA V POLETO
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
                string update = "";
                if (e.ColumnIndex == 1) { update = "FirstName"; }
                if (e.ColumnIndex == 2) { update = "LastName"; }
                if (e.ColumnIndex == 3) { update = "City"; }
                if (e.ColumnIndex == 4) { update = "Address"; }
                if (e.ColumnIndex == 5) { update = "Phone"; }
                if (e.ColumnIndex == 6) { update = "Wallet"; }
                string value = dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString();
                int rowIndex = e.RowIndex;
                string ID = dataGridView1.Rows[rowIndex].Cells[0].Value.ToString();
                db.UpdateClient(ref dataGridView3, ref ID, ref value,ref update);
                db.ShowCategories(ref dataGridView3);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                db.dataBase = "Client";
                int rowIndex = dataGridView1.CurrentCell.RowIndex;
                string ID = dataGridView1.Rows[rowIndex].Cells[0].Value.ToString();
                db.DeleteCategory(ref dataGridView1, ref ID, db.dataBase);
                db.ResetID(db.dataBase);
                db.ShowCustomers(ref dataGridView1);
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
