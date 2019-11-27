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

//formata za catch error v stil material
namespace SkladPro
{
    public partial class ErrorForm : MaterialForm
    {
        MaterialSkin.MaterialSkinManager skinManager;
        public ErrorForm()
        {
            InitializeComponent();
        }

        private void ErrorForm_Load(object sender, EventArgs e)
        {
            skinManager = MaterialSkinManager.Instance;
            skinManager.AddFormToManage(this);
            skinManager.Theme = MaterialSkinManager.Themes.DARK;
            skinManager.ColorScheme = new ColorScheme(Primary.Red900, Primary.BlueGrey900, Primary.Grey600, Accent.Red700, TextShade.WHITE);
            materialLabel1.BackColor = Color.Red;
        }


        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
