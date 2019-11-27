using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//material style form za izvejdane na suobshteniq v drugite formi
namespace SkladPro
{
    public partial class Message : MaterialForm
    {
        public Message()
        {
            InitializeComponent();
        }

        private void Message_Load(object sender, EventArgs e)
        {
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void materialLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}
