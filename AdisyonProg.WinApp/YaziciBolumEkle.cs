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
    public partial class YaziciBolumEkle : Form
    {
        public YaziciBolumEkle()
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
                    int value = adisyonRepository.YaziciBolumEkle(textBox1.Text);

                    if (value > 0)
                    {
                        MsgBox.baslik = "Bilgi";
                        MsgBox.BoxButtons = MessageBoxButtons.OK;
                        MsgBox.message = "Bölüm Eklendi.";
                        msgBox.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MsgBox.baslik = "Uyarı";
                        MsgBox.BoxButtons = MessageBoxButtons.OK;
                        MsgBox.message = "Bölüm Eklenemedi..!";
                        msgBox.ShowDialog();
                    }
                }
            }
            else
            {
                MsgBox.baslik = "Uyarı";
                MsgBox.BoxButtons = MessageBoxButtons.OK;
                MsgBox.message = "Tüm alanları doldurunuz..!";
                msgBox.ShowDialog();
            }
           
        }

        private void YaziciBolumEkle_FormClosed(object sender, FormClosedEventArgs e)
        {
            YaziciAyarla yaziciAyarla = new YaziciAyarla();
            this.Visible = false;
            yaziciAyarla.Show();
        }
    }
}
