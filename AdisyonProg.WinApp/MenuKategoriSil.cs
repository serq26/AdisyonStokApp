using AdisyonProg.Core.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdisyonProg.WinApp
{
    public partial class MenuKategoriSil : Form
    {
        public MenuKategoriSil()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                int value = adisyonRepository.MenuKategoriSil(comboBox1.Text);

                MsgBox msgBox = new MsgBox();
                if (value > 0)
                {
                    MsgBox.baslik = "Bilgi";
                    MsgBox.BoxButtons = MessageBoxButtons.OK;
                    MsgBox.message = "Kategori Silindi.";
                    msgBox.ShowDialog();
                    this.Close();                    
                }
                else
                {
                    MsgBox.baslik = "Hata";
                    MsgBox.BoxButtons = MessageBoxButtons.OK;
                    MsgBox.message = "Kategori Silinemedi..!";
                    msgBox.ShowDialog();
                }
            }
        }

        private void MenuKategoriSil_Load(object sender, EventArgs e)
        {
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                comboBox1.DataSource = adisyonRepository.MenuKategoriGetir();
            }
        }
        void Yonlendir()
        {
            AdisyonProg.WinApp.Menu menu = new AdisyonProg.WinApp.Menu();
            this.Visible = false;
            menu.Show();            
        }
        private void MenuKategoriSil_FormClosed(object sender, FormClosedEventArgs e)
        {
            Yonlendir();
        }
    }
}
