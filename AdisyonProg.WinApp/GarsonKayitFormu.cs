using AdisyonProg.Core.Helper;
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
    public partial class GarsonKayitFormu : Form
    {
        public GarsonKayitFormu()
        {
            InitializeComponent();
        }

        private void btn_kayit_Click(object sender, EventArgs e)
        {
            MsgBox msgBox = new MsgBox();

            this.Name = "form";
            Form f = this;
            bool control = Globalislemler.NullControl(f);

            if (control == true)
            {
                Garson garson = new Garson();
                garson.Ad = txt_ad.Text;
                garson.Soyad = txt_soyad.Text;
                garson.KullaniciAdi = txt_kullanici_adi.Text;
                garson.Sifre = txt_sifre.Text;
                garson.Telefon = txt_telefon.Text;
                garson.Adres = txt_adres.Text;
                garson.Gorev = comboBox1.Text;

                using (AdisyonRepository adisyonRepository = new AdisyonRepository())
                {
                    int returnValue = adisyonRepository.GarsonEkle(garson);

                    if (returnValue > 0)
                    {
                        MsgBox.baslik = "Kayıt";
                        MsgBox.message = "Kayıt işlemi başarılı...";
                        MsgBox.BoxButtons = MessageBoxButtons.OK;
                        msgBox.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MsgBox.baslik = "Hata";
                        MsgBox.message = "Kayıt işlemi başarısız. Başka bir ŞİFRE giriniz.";
                        MsgBox.BoxButtons = MessageBoxButtons.OK;
                        msgBox.ShowDialog();
                    }
                }
            }
            else
            {
                MsgBox.baslik = "Hata";
                MsgBox.message = "Tüm alanları doldurunuz..!";
                MsgBox.BoxButtons = MessageBoxButtons.OK;
                msgBox.ShowDialog();
            }
           
        }

        private void GarsonKayitFormu_Load(object sender, EventArgs e)
        {

        }

        private void GarsonKayitFormu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Personel personel = new Personel();
            this.Visible = false;
            personel.Show();
        }
    }
}
