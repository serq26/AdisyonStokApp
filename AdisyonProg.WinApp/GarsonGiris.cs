using AdisyonProg.Core.Repository;
using AdisyonProg.Entity;
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
    public partial class GarsonGiris : Form
    {
        public GarsonGiris()
        {
            InitializeComponent();
        }
        
        private void btn_giris_Click(object sender, EventArgs e)
        {
            MsgBox msgBox = new MsgBox();

            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                Garson garson = adisyonRepository.KullaniciGiris(txt_sifre.Text);

                if (garson.Sifre == txt_sifre.Text)
                {
                    GarsonEkrani garsonEkrani = new GarsonEkrani();
                    GarsonEkrani.PersonelName = garson.Ad + " " + garson.Soyad;
                    garsonEkrani.Show();
                    this.Close();
                }
                else
                {
                    MsgBox.baslik = "Hata";
                    MsgBox.message = "Kullanıcı adı veya Şifre yanlış..!";
                    MsgBox.BoxButtons = MessageBoxButtons.OK;
                    msgBox.ShowDialog();
                    txt_sifre.Text = string.Empty;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Giris giris = new Giris();
            giris.Show();
            this.Close();
        }

        private void GarsonGiris_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            txt_sifre.Text = txt_sifre.Text + ((Button)sender).Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txt_sifre.Text = txt_sifre.Text + ((Button)sender).Text;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            txt_sifre.Text = txt_sifre.Text + ((Button)sender).Text;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            txt_sifre.Text = txt_sifre.Text + ((Button)sender).Text;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            txt_sifre.Text = txt_sifre.Text + ((Button)sender).Text;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            txt_sifre.Text = txt_sifre.Text + ((Button)sender).Text;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            txt_sifre.Text = txt_sifre.Text + ((Button)sender).Text;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            txt_sifre.Text = txt_sifre.Text + ((Button)sender).Text;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            txt_sifre.Text = txt_sifre.Text + ((Button)sender).Text;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            txt_sifre.Text = txt_sifre.Text + ((Button)sender).Text;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (txt_sifre.Text.Length > 1)
            {
                txt_sifre.Text = txt_sifre.Text.Substring(0, txt_sifre.Text.Length - 1);
            }
            else
            {
                txt_sifre.Text = "";
            }
        }
    }
}
