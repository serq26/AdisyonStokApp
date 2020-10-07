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
    public partial class GarsonEkrani : Form
    {
        public static List<Masa> Masalar;
        public static int AdisyonNo;
        public static string PersonelName;

        public GarsonEkrani()
        {
            InitializeComponent();
            Masalar = new List<Masa>();
            using(AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                MasalariGetir(adisyonRepository.MasaBolumleriFirstID());
            }
        }

        private void GarsonEkrani_Load(object sender, EventArgs e)
        {
            this.Text = PersonelName;
            timer1.Enabled = true;
            GunBasiKontrol();
            CafeBolumleri();
        }

        private void GunBasiKontrol()
        {
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                string result = adisyonRepository.GunBasiYapildimiKontrol();
                if (result == "Hayır")
                {
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    panel1.Enabled = false;
                }
                else
                {
                    button1.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = true;
                    panel1.Enabled = true;
                }
            }
        }

        private void CafeBolumleri()
        {
            groupBox_cafe_bolumleri.Visible = true;
            List<MasaBolumleri> cafeBolumleri = new List<MasaBolumleri>();
            
            using(AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                cafeBolumleri = adisyonRepository.MasaBolumleriGetir();

                for (int i = 0; i < cafeBolumleri.Count; i++)
                {
                    Button bolum = new Button();
                    bolum.Text = cafeBolumleri[i].BolumAdi;
                    bolum.ForeColor = Color.White;
                    bolum.BackColor = Color.DarkRed;
                    bolum.Font = new Font("Century Gothic", 14, FontStyle.Bold);
                    bolum.Width = 250;
                    bolum.Height = 80;
                    bolum.FlatStyle = FlatStyle.Flat;
                    bolum.FlatAppearance.BorderSize = 0;
                    bolum.Click += Bolum_Click;

                    flowLayoutPanel2.Controls.Add(bolum);
                    groupBox_cafe_bolumleri.Controls.Add(flowLayoutPanel2);
                }
            }
        }

        private void Bolum_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            flowLayoutPanel1.Controls.Clear();
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                MasalariGetir(((Button)sender).Text);
            }
        }

        public void MasalariGetir(string bolum)
        {
            flowLayoutPanel1.Controls.Clear();

            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                //Masalar = adisyonRepository.MasalariGetir();
                Masalar = adisyonRepository.BolumeGoreMasalariGetir(bolum);
                //Masalar.RemoveAt(0); // TakeAway'in masa olarak gözükmemesi için..!
                for (int i = 0; i < Masalar.Count; i++)
                {
                    Button Masa = new Button();
                    Masa.Text = Masalar[i].MasaAdi;
                    if(Masalar[i].MasaRengi == "Açık")
                    {
                        Masa.BackColor = Color.FromArgb(240, 169, 63);
                    }
                    else if(Masalar[i].MasaRengi == "Rezerve")
                    {
                        Masa.BackColor = Color.SteelBlue;
                    }
                    else
                    {
                        Masa.BackColor = Color.FromArgb(46, 46, 46);
                    }

                    Masa.ForeColor = Color.White;
                    Masa.Font = new Font("Georgia", 24, FontStyle.Bold);
                    Masa.FlatStyle = FlatStyle.Flat;
                    Masa.FlatAppearance.BorderSize = 0;
                    Masa.Height = 100;
                    Masa.Width = 200;
                    Masa.Click += Masa_Click;
                    Masa.TabIndex = i;
                    this.flowLayoutPanel1.Controls.Add(Masa);
                    panel1.Controls.Add(flowLayoutPanel1);
                }
            }
        }

        private void Masa_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            MasaDetay.MasaAdi = b.Text;
            //MasaDetay.MasaId = b.TabIndex+1;
            using(AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                MasaDetay.MasaId = adisyonRepository.MasaIDGetir(b.Text);
            }

            panel1.Controls.Clear();
            MasaDetay masaDetay = new MasaDetay();
            masaDetay.TopLevel = false;
            panel1.Controls.Add(masaDetay);
            masaDetay.Show();
            masaDetay.Dock = DockStyle.Fill;
            masaDetay.BringToFront();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            flowLayoutPanel1.Controls.Clear();
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                MasalariGetir(adisyonRepository.MasaBolumleriFirstID());
            }
        }

        private void GarsonEkrani_FormClosed(object sender, FormClosedEventArgs e)
        {
            Giris giris = new Giris();
            giris.Show();
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MasaAktar masaAktar = new MasaAktar();
            Rezervasyon.IslemYapilanEkran = "GarsonEkrani";
            masaAktar.ShowDialog();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Rezervasyon rezervasyon = new Rezervasyon();
            Rezervasyon.IslemYapilanEkran = "GarsonEkrani";
            rezervasyon.ShowDialog();
            this.Hide();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            string saat = DateTime.Now.ToShortDateString();
            lbl_tarih.Text = saat;
            lbl_saat.Text = DateTime.Now.ToLongTimeString();
        }

    }
}
