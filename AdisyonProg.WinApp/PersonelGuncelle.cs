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
    public partial class PersonelGuncelle : Form
    {
        public static int secilenID = 0;
        public static string secilenPersonel;
        public PersonelGuncelle()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using(AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                MsgBox msgBox = new MsgBox();

                int value = adisyonRepository.PersonelGuncelle(secilenID, txt_ad.Text, txt_soyad.Text, txt_kullanici.Text, txt_sifre.Text, txt_telefon.Text, txt_adres.Text, txt_gorev.Text);

                if (value > 0)
                {
                    MsgBox.baslik = "Bilgi";
                    MsgBox.BoxButtons = MessageBoxButtons.OK;
                    MsgBox.message = "Personel başarıyla güncellendi.";
                    msgBox.ShowDialog();
                    this.Close();
                }
                else
                {
                    MsgBox.baslik = "Hata";
                    MsgBox.BoxButtons = MessageBoxButtons.OK;
                    MsgBox.message = "Personel güncellenemedi..!";
                    msgBox.ShowDialog();
                }
            }
        }

        private void PersonelGuncelle_Load(object sender, EventArgs e)
        {
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                Garson personel = new Garson();

                personel = adisyonRepository.SecilenPersoneliGetir(secilenID, secilenPersonel);
                txt_ad.Text = personel.Ad;
                txt_soyad.Text = personel.Soyad;
                txt_kullanici.Text = personel.KullaniciAdi;
                txt_sifre.Text = personel.Sifre;
                txt_telefon.Text = personel.Telefon;
                txt_adres.Text = personel.Adres;
                txt_gorev.Text = personel.Gorev;
            }
        }

        private void PersonelGuncelle_FormClosed(object sender, FormClosedEventArgs e)
        {
            Personel personel = new Personel();
            this.Visible = false;
            personel.Show();
        }
    }
}
