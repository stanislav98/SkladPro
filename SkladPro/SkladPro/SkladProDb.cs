using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace SkladPro
{
    class SkladProDb
    {

        public SqlConnection mydb { set; get; } 
        public string dataBase { set; get; }
        public string strConnectionString = @"Server= DESKTOP-EK4FKT1\STOICHEVSQL ; Database = SkladPro ; Trusted_Connection = True ";

        //vmukvane na klient
        public void InsertClient(string fn, string ln, string city, string addr, string phone, string wallet)
        {
            mydb = new SqlConnection(strConnectionString);
            mydb.Open();
            string insert = "INSERT INTO Client (FirstName,LastName,City,Address,Phone,Wallet) " +
                            "VALUES('" + fn + "','" + ln + "','" + city + "','" + addr + "','" + phone + "','" + wallet + "');";
            SqlCommand cmd = new SqlCommand(insert, mydb);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            mydb.Close();
        }

        //vmukvane na produkt
        public void InsertProduct(string name, string quan, string price, string cat)
        {
            mydb = new SqlConnection(strConnectionString);
            mydb.Open();
            string insert = "INSERT INTO Articules (Name,Quantity,Price,ID_Category) " +
                            "VALUES('" + name + "','" + quan + "','" + price + "','" + cat + "');";
            SqlCommand cmd = new SqlCommand(insert, mydb);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            mydb.Close();
        }

        //vmukvane na prodabja
        public void InsertSale(string clID, string prID, string amount, string date)
        {
            mydb = new SqlConnection(strConnectionString);
            mydb.Open();
            string insert = "INSERT INTO Sales (ID_Client,ID_Articules,Amount,Data) " +
                            "VALUES('" + clID + "','" + prID + "','" + amount + "','" + date +"');";
            SqlCommand cmd = new SqlCommand(insert, mydb);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            mydb.Close();
        }

        //izvejdane na vsichki klienti
        public void ShowCustomers(ref DataGridView c)
        {
            //mydb = new SqlConnection(strConnectionString);
            //   mydb.Open();
           SqlDataAdapter adpt = new SqlDataAdapter("Select * from Client", strConnectionString);
           DataTable dt = new DataTable();
           adpt.Fill(dt);
           c.DataSource = dt;

            //mydb.Close();
        }
        public void ShowSell(ref DataGridView cc)
        {
            //mydb = new SqlConnection(strConnectionString);
            //   mydb.Open();
            SqlDataAdapter adpt = new SqlDataAdapter("Select * from Sales", strConnectionString);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            cc.DataSource = dt;
            //mydb.Close();
        }

        //izvejdane na vsichki kategorii
        public void ShowCategories(ref System.Windows.Forms.ComboBox cb, ref DataGridView cc)
        {
            mydb = new SqlConnection(strConnectionString);
            mydb.Open();
            SqlCommand cmd = new SqlCommand("Select * from Category", mydb);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
                cb.Items.Add(dr[1].ToString());
            dr.Close();
            cmd.Dispose();
            mydb.Close();
        }
        //overload 1 parameter with datagridvriew
        public void ShowCategories(ref DataGridView cc)
        {
            //mydb = new SqlConnection(strConnectionString);
         //   mydb.Open();
            SqlDataAdapter adpt = new SqlDataAdapter("Select * from Category", strConnectionString);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            cc.DataSource = dt;
            adpt.Dispose();
            //mydb.Close();
        }
        //overload 1 parameter for cb
        public void ShowCategories(ref System.Windows.Forms.ComboBox cb )
        {
            mydb = new SqlConnection(strConnectionString);
            mydb.Open();
            SqlCommand cmd = new SqlCommand("Select * from Category", mydb);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
                cb.Items.Add(dr[1].ToString());
            dr.Close();
            cmd.Dispose();
            mydb.Close();
        }
        //show client in sale 
        public void ShowClients(ref System.Windows.Forms.ComboBox cb)
        {
            mydb = new SqlConnection(strConnectionString);
            mydb.Open();
            SqlCommand cmd = new SqlCommand("Select * from Client", mydb);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
                cb.Items.Add(dr[1].ToString());
            dr.Close();
            cmd.Dispose();
            mydb.Close();
        }

        //zapulva datagridview i priema parametur i sochesht kum ID na posochenata kategoriq vuv ComboBox
        public void fillCategories(ref DataGridView cc,ref int i)
        {
          //  mydb = new SqlConnection(strConnectionString);
           // mydb.Open();
            SqlDataAdapter adpt = new SqlDataAdapter("Select * from Articules where ID_Category = " + i, strConnectionString);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            cc.DataSource = dt;
            adpt.Dispose();
            //mydb.Close();
        }
        //zapulva datagridview i priema parametur i sochesht kum ID na posocheniq klient vuv ComboBox
        public void fillClients(ref DataGridView cc, ref int i)
        {
            //  mydb = new SqlConnection(strConnectionString);
            // mydb.Open();
            SqlDataAdapter adpt = new SqlDataAdapter("Select * from Client where ID_Client = " + i, strConnectionString);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            cc.DataSource = dt;
            adpt.Dispose();
            //mydb.Close();
        }

        //izvejdane na vsichki produkti
        public void ShowProducts(ref DataGridView cc) 
        {
            //  mydb = new SqlConnection(strConnectionString);
            // mydb.Open();
            SqlDataAdapter adpt = new SqlDataAdapter("Select * from Articules", strConnectionString);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            cc.DataSource = dt;
            adpt.Dispose();
            //mydb.Close();
        }

        //iztrivane na kategoriq
        public void DeleteCategory(ref DataGridView cc,ref string ID,string dataBase)
        {
            //  mydb = new SqlConnection(strConnectionString);
            // mydb.Open();
            SqlDataAdapter adpt = new SqlDataAdapter("Delete from "+dataBase+" where ID_"+dataBase+" = "+ID, strConnectionString);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            cc.DataSource = dt;
            adpt.Dispose();
            //mydb.Close();
        }

        //vmukvane vuv kategoriqta
        public void InsertCategory(string name)
        {
            mydb = new SqlConnection(strConnectionString);
            mydb.Open();
            string insert = "INSERT INTO Category (Name) " +
                            "VALUES('" + name + "');";
            SqlCommand cmd = new SqlCommand(insert, mydb);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            mydb.Close();
        }

        //obnovqvane na imeto na kategoriqta
        public void UpdateCategory(ref DataGridView cc, ref string ID,ref string value)
        {
            //  mydb = new SqlConnection(strConnectionString);
            // mydb.Open();
            SqlDataAdapter adpt = new SqlDataAdapter("Update Category SET Name = '"+value+"' where ID_Category = " + ID, strConnectionString);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            cc.DataSource = dt;
            adpt.Dispose();
            //mydb.Close();
        }

        //vzemane na id-tata na kategiriite
        public List<int> GetAvailableCategoriesID()
        {
            List<int> Available = new List<int>();
            SqlDataAdapter adpt = new SqlDataAdapter("Select ID_Category from Category ", strConnectionString);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            Available = dt.AsEnumerable().Select(n => n.Field<int>(0)).ToList();
            return Available;
        }

        //korekciq na danni za produkta
        public void UpdateProducts(ref DataGridView cc, ref string ID, ref string value,string update)
        {
            //  mydb = new SqlConnection(strConnectionString);
            // mydb.Open();
            SqlDataAdapter adpt = new SqlDataAdapter("Update Articules SET "+update+" = '" + value + "' where ID_Articules = " + ID, strConnectionString);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            cc.DataSource = dt;
            adpt.Dispose();
            //mydb.Close();
        }
        public void UpdateProductsAfterBuy(ref DataGridView cc, ref string ID, ref string value)
        {
            //  mydb = new SqlConnection(strConnectionString);
            // mydb.Open();
            SqlDataAdapter adpt = new SqlDataAdapter("Update Articules SET Quantity = '" + value + "' where ID_Articules = " + ID, strConnectionString);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            cc.DataSource = dt;
            adpt.Dispose();
            //mydb.Close();
        }
        //korekciq update na vsichko na klienta
        public void UpdateClient(ref DataGridView cc, ref string ID, ref string value,ref string update)
        {
            //  mydb = new SqlConnection(strConnectionString);
            // mydb.Open();
            SqlDataAdapter adpt = new SqlDataAdapter("Update Client SET " + update + " = '" + value + "' where ID_Client = " + ID, strConnectionString);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            cc.DataSource = dt;
            adpt.Dispose();
            //mydb.Close();
        }

        //overload za pokupka - namalqvane na parite na clienta
        public void UpdateClient(ref string ID, ref string newMoney)
        {
            //  mydb = new SqlConnection(strConnectionString);
            // mydb.Open();
            SqlDataAdapter adpt = new SqlDataAdapter("Update Client SET Wallet = '" + newMoney + "' where ID_Client = " + ID, strConnectionString);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            adpt.Dispose();
            //mydb.Close();
        }

        //sled iztrivane da se vuzstanovqt id-tata
           public void ResetID(string dbName)
        {
            mydb = new SqlConnection(strConnectionString);
            mydb.Open();
            SqlCommand cmd = new SqlCommand("DBCC CHECKIDENT("+dbName+", RESEED,0)", mydb);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            mydb.Close();
        }
 
    }
}
