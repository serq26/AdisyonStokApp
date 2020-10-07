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
    public partial class Rezervasyon : Form
    {
        public static int masaID;
        public static string IslemYapilanEkran;
        public Rezervasyon()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MsgBox msgBox = new MsgBox();
            using(AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                string masaDurumu = adisyonRepository.MasaDurumunuGetir(masaID);

                if (masaDurumu != "Açık" && masaDurumu != "Rezerve")
                {
                    int value = adisyonRepository.MasaDurumunuGuncelle(masaID, "Rezerve", DateTime.Now, 0);

                    if (value > 0)
                    {
                        MsgBox.baslik = "Rezervasyon";
                        MsgBox.message = "Masa Rezerve Edildi.";
                        MsgBox.BoxButtons = MessageBoxButtons.OK;
                        msgBox.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MsgBox.baslik = "Hata";
                        MsgBox.message = "İşlem başarısız..!";
                        MsgBox.BoxButtons = MessageBoxButtons.OK;
                        msgBox.ShowDialog();
                    }
                }
                else
                {
                    MsgBox.baslik = "Hata";
                    MsgBox.message = "İşlem başarısız..!";
                    MsgBox.BoxButtons = MessageBoxButtons.OK;
                    msgBox.ShowDialog();
                }
            }           
        }

        private void Rezervasyon_Load(object sender, EventArgs e)
        {
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                List<Masa> Masalar = new List<Masa>();
                Masalar = adisyonRepository.MasalariGetir();
                if (Masalar.Count > 0)
                {
                    Masalar.RemoveAt(0);
                }
                comboBox1.DataSource = Masalar;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MsgBox msgBox = new MsgBox();
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                string masaDurumu = adisyonRepository.MasaDurumunuGetir(masaID);

                int value = adisyonRepository.MasaDurumunuGuncelle(masaID, "Kapalı", DateTime.Now, 0);

                if (masaDurumu == "Rezerve")
                {
                    if (value > 0)
                    {
                        MsgBox.baslik = "Rezervasyon";
                        MsgBox.message = "Masa Rezervasyonu İptal Edildi.";
                        MsgBox.BoxButtons = MessageBoxButtons.OK;
                        msgBox.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MsgBox.baslik = "Hata";
                        MsgBox.message = "İşlem başarısız..!";
                        MsgBox.BoxButtons = MessageBoxButtons.OK;
                        msgBox.ShowDialog();
                    }
                }
            }
        }
        void Yonlendir()
        {
            if (IslemYapilanEkran == "GarsonEkrani")
            {
                GarsonEkrani garsonEkrani = new GarsonEkrani();
                this.Visible = false;
                garsonEkrani.Show();
            }
            else
            {
                KasaEkrani kasaEkrani = new KasaEkrani();
                this.Visible = false;
                kasaEkrani.Show();
            }
        }
        private void Rezervasyon_FormClosed(object sender, FormClosedEventArgs e)
        {
            Yonlendir();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Yonlendir();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string masaAdi = ((ComboBox)sender).Text;
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                masaID = adisyonRepository.MasaIDGetir(masaAdi);
            }
        }
    }
}
