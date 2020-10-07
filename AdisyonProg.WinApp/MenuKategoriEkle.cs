using AdisyonProg.Core.Helper;
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
    public partial class MenuKategoriEkle : Form
    {
        public MenuKategoriEkle()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MsgBox msgBox = new MsgBox();

            this.Name = "form";
            Form f = this;
            bool control = Globalislemler.NullControl(f);

            if (control == true)
            {
                using (AdisyonRepository adisyonRepository = new AdisyonRepository())
                {
                    int value = adisyonRepository.MenuKategoriEkle(textBox1.Text);
                    if (value > 0)
                    {
                        MsgBox.baslik = "Bilgi";
                        MsgBox.BoxButtons = MessageBoxButtons.OK;
                        MsgBox.message = "Kategori başarıyla eklendi.";
                        msgBox.ShowDialog();
                        textBox1.Text = string.Empty;
                    }
                    else
                    {
                        MsgBox.baslik = "Hata";
                        MsgBox.BoxButtons = MessageBoxButtons.OK;
                        MsgBox.message = "Kategori eklenemedi..!";
                        msgBox.ShowDialog();
                    }
                }
            }
            else
            {
                MsgBox.baslik = "Hata";
                MsgBox.BoxButtons = MessageBoxButtons.OK;
                MsgBox.message = "Kategori Adı alanını doldurunuz..!";
                msgBox.ShowDialog();
            }
           
        }
        void Yonlendir()
        {
            AdisyonProg.WinApp.Menu menu = new AdisyonProg.WinApp.Menu();
            this.Visible = false;
            menu.Show();
        }
        private void MenuKategoriEkle_FormClosed(object sender, FormClosedEventArgs e)
        {
            Yonlendir();
        }
    }
}
