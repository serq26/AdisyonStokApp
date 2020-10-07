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
    public partial class MasaAktar : Form
    {
        public static string IslemYapilanEkran;

        int value1;
        int value2;
        string masa1;
        string masa2;
        List<Masa> Masalar;
        public MasaAktar()
        {
            InitializeComponent();
            Masalar = new List<Masa>();
        }

        private void MasaAktar_Load(object sender, EventArgs e)
        {
            using(AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                Masalar = new List<Masa>();                
                Masalar = adisyonRepository.MasalariGetir();
                if (Masalar.Count > 0)
                {
                    Masalar.RemoveAt(0);
                }
                comboBox1.DataSource = Masalar;                
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Enabled = true;
            comboBox2.BindingContext = new BindingContext();
            comboBox2.DataSource = Masalar;

            ComboBox cmb = (ComboBox)sender;
            masa1 = cmb.Text;
            using(AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                value1 = adisyonRepository.MasaIDGetir(masa1);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb2 = (ComboBox)sender;
            
            masa2 = cmb2.Text;
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                value2 = adisyonRepository.MasaIDGetir(masa2);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MsgBox msgBox = new MsgBox();
            List<Adisyon> Siparisler = new List<Adisyon>();
            int returnValue = 0;

            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                Siparisler = adisyonRepository.MasadakiSiparisleriGetir(value1);
                int adisyonNo = adisyonRepository.MasaAdisyonNumarasiniGetir(masa1);

                string durum = adisyonRepository.MasaDurumunuGetir(adisyonRepository.MasaIDGetir(masa2));

                if (durum == "Kapalı")
                {
                    returnValue = adisyonRepository.MasaAktar(value1, adisyonNo, masa2, value2);

                    if (returnValue > 0)
                    {
                        System.Threading.Thread.Sleep(1000);
                        adisyonRepository.MasaDurumunuGuncelle(value1, "Kapalı", DateTime.Now, 0); //MasaDetay.saat
                        adisyonRepository.MasaDurumunuGuncelle(value2, "Açık", DateTime.Now, adisyonNo);

                        MsgBox.baslik = "Masa Aktarma";
                        MsgBox.message = "Masa Aktarıldı...";
                        MsgBox.BoxButtons = MessageBoxButtons.OK;
                        msgBox.ShowDialog();

                        if (MsgBox.result == DialogResult.OK)
                        {
                            this.Close();
                        }
                    }
                    else
                    {
                        MsgBox.baslik = "Masa Aktarma";
                        MsgBox.message = "Masa Aktarma İşlemi Başarısız..!";
                        MsgBox.BoxButtons = MessageBoxButtons.OK;
                        msgBox.ShowDialog();
                    }
                }
                else
                {
                    MsgBox.baslik = "Masa Aktarma";
                    MsgBox.message = "Açık olan masaya aktarma yapılamaz..!";
                    MsgBox.BoxButtons = MessageBoxButtons.OK;
                    msgBox.ShowDialog();
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
        private void MasaAktar_FormClosed(object sender, FormClosedEventArgs e)
        {
            Yonlendir();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Yonlendir();          
        }
    }
}
