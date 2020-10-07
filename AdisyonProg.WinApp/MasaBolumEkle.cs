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
    public partial class MasaBolumEkle : Form
    {
        public MasaBolumEkle()
        {
            InitializeComponent();
        }

        private void MasaBolumEkle_FormClosed(object sender, FormClosedEventArgs e)
        {
            Masalar masalar = new Masalar();
            this.Visible = false;
            masalar.Show();
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
                    int value = adisyonRepository.MasaBolumEkle(textBox1.Text);

                    if (value > 0)
                    {
                        MsgBox.baslik = "Bilgi";
                        MsgBox.message = "Bölüm başarıyla eklendi.";
                        MsgBox.BoxButtons = MessageBoxButtons.OK;
                        msgBox.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MsgBox.baslik = "Uyarı";
                        MsgBox.message = "Bölüm ekleme işlemi başarısız..!";
                        MsgBox.BoxButtons = MessageBoxButtons.OK;
                        msgBox.ShowDialog();
                    }
                }
            }
            else
            {
                MsgBox.baslik = "Uyarı";
                MsgBox.message = "Tüm alanları doldurunuz..!";
                MsgBox.BoxButtons = MessageBoxButtons.OK;
                msgBox.ShowDialog();
            }            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
