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
    public partial class KasaEkrani : Form
    {
        public static List<Masa> Masalar;
        public static int AdisyonNo;

        public KasaEkrani()
        {
            InitializeComponent();
            Masalar = new List<Masa>();
        }

        public void MasalariGetir(string bolum)
        {
            flowLayoutPanel1.Controls.Clear();

            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                //Masalar = adisyonRepository.MasalariGetir(bolum);
                //Masalar.RemoveAt(0); // TakeAway'in masa olarak gözükmemesi için..!
                Masalar = adisyonRepository.BolumeGoreMasalariGetir(bolum);

                for (int i = 0; i < Masalar.Count; i++)
                {
                    decimal Hesap = 0;
                    Button Masa = new Button();
                    int adisyonNo = adisyonRepository.MasaAdisyonNumarasiniGetir(Masalar[i].MasaAdi);
                    Hesap = adisyonRepository.MasaHesapDurumu(Masalar[i].MasaID,adisyonNo);
                    Masa.Text = Masalar[i].MasaAdi;
                    Label Lbl_hesap = new Label();
                    Lbl_hesap.Text = Hesap + "  TL";
                    Lbl_hesap.ForeColor = Color.Black;
                    Lbl_hesap.Font = new Font("Arial",15,FontStyle.Bold);
                    Lbl_hesap.Width = 150;
                    Lbl_hesap.Location = new Point(90, 75);  
                    Masa.Controls.Add(Lbl_hesap);

                    if (Masalar[i].MasaRengi == "Açık")
                    {
                        Masa.BackColor = Color.FromArgb(240, 169, 63);

                        Label lbl_masa_saati = new Label();
                        DateTime saat = adisyonRepository.MasaSaatiHesapla(Masalar[i].MasaID);
                        TimeSpan sonuc = saat - DateTime.Now;
                        int minute = Convert.ToInt32(sonuc.TotalMinutes * -1);
                        lbl_masa_saati.Text = minute.ToString() + " dk";
                        lbl_masa_saati.ForeColor = Color.FromArgb(41, 39, 41);
                        lbl_masa_saati.Font = new Font("Arial", 14, FontStyle.Italic);
                        lbl_masa_saati.Width = 100;
                        lbl_masa_saati.Location = new Point(5, 5);
                        Masa.Controls.Add(lbl_masa_saati);
                    }
                    else if(Masalar[i].MasaRengi == "Rezerve")
                    {
                        Masa.BackColor = Color.SteelBlue;
                        Lbl_hesap.Visible = false;
                    }
                    else
                    {
                        Masa.BackColor = Color.FromArgb(46,46,46);
                        Lbl_hesap.Visible = false;
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
            //MasaDetay.MasaId = b.TabIndex + 1;

            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
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
        private void KasaEkrani_FormClosed(object sender, FormClosedEventArgs e)
        {
            Giris giris = new Giris();
            giris.Show();
            this.Dispose();
        }
        private void btn_masalar_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            flowLayoutPanel1.Controls.Clear();
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                MasalariGetir(adisyonRepository.MasaBolumleriFirstID());
            }
        }
        private void KasaEkrani_Load(object sender, EventArgs e)
        {
            /////// Çözünürlük ////////////////////////////////
            //this.Location = new Point(0, 0);
            //this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            //////////////////////////////////////////////////

            //GunBasiKontrol();
            panel1.Controls.Clear();
            flowLayoutPanel1.Controls.Clear();           
            timer1.Enabled = true;
            timer2.Enabled = true;

            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                MasalariGetir(adisyonRepository.MasaBolumleriFirstID());
            }
            CafeBolumleri();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Rezervasyon rezervasyon = new Rezervasyon();
            Rezervasyon.IslemYapilanEkran = "KasaEkrani";
            rezervasyon.ShowDialog();
            this.Hide();
        }
        private void button2_Click(object sender, EventArgs e)
        {           
            panel1.Controls.Clear();
            TakeAway masaDetay = new TakeAway();
            masaDetay.TopLevel = false;
            panel1.Controls.Add(masaDetay);
            masaDetay.Show();
            masaDetay.Dock = DockStyle.Fill;
            masaDetay.BringToFront();


        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            string saat = DateTime.Now.ToShortDateString();
            lbl_tarih.Text = saat;
            lbl_saat.Text = DateTime.Now.ToLongTimeString();           
        }

        //private void GunBasiKontrol()
        //{
        //    using(AdisyonRepository adisyonRepository = new AdisyonRepository())
        //    {
        //        string result = adisyonRepository.GunBasiYapildimiKontrol();
        //        if (result == "Hayır")
        //        {
        //            button3.Text = "GÜN BAŞI YAP";
        //            button3.Image = Properties.Resources.start;
        //            button1.Enabled = false;
        //            button2.Enabled = false;
        //            btn_masalar.Enabled = false;
        //            panel1.Enabled = false;
        //        }
        //        else
        //        {
        //            lbl_gunbasi.Text = adisyonRepository.GunBasiTarih().ToString();
        //            button3.Text = "GÜN SONU YAP";
        //            button1.Enabled = true;
        //            button2.Enabled = true;
        //            btn_masalar.Enabled = true;
        //            panel1.Enabled = true;
        //            button3.Text = "GÜN SONU YAP";
        //            button3.BackColor = Color.FromArgb(39, 41, 40);
        //            button3.Image = Properties.Resources.finish;
        //        }
        //    }          
        //}

        private void CafeBolumleri()
        {
            groupBox_cafe_bolumleri.Visible = true;
            List<MasaBolumleri> cafeBolumleri = new List<MasaBolumleri>();

            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                cafeBolumleri = adisyonRepository.MasaBolumleriGetir();

                for (int i = 0; i < cafeBolumleri.Count; i++)
                {
                    Button bolum = new Button();
                    bolum.Text = cafeBolumleri[i].BolumAdi;
                    bolum.ForeColor = Color.White;
                    bolum.BackColor = Color.DarkRed;
                    bolum.Font = new Font("Century Gothic", 14, FontStyle.Bold);
                    bolum.Width = 220;
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

        //private void button3_Click(object sender, EventArgs e)
        //{
        //    MsgBox msgBox = new MsgBox();

        //    using (AdisyonRepository adisyonRepository = new AdisyonRepository())
        //    {
        //        if(button3.Text =="GÜN BAŞI YAP")
        //        {
        //            MsgBox.baslik = "Gün Başı";
        //            MsgBox.message = "Gün başı yapılacak. Onaylıyor musunuz ?";
        //            MsgBox.BoxButtons = MessageBoxButtons.YesNo;
        //            msgBox.ShowDialog();
        //            if (MsgBox.result == DialogResult.Yes)
        //            {
        //                lbl_gunbasi.Text = DateTime.Now.ToString();
        //                adisyonRepository.GunBasiYapildimiGuncelle("Evet", DateTime.Now);
        //                button3.Text = "GÜN SONU YAP";
        //                button3.BackColor = Color.FromArgb(39,41,40);
        //                button3.Image = Properties.Resources.finish;
        //            }                                      
        //        }
        //        else
        //        {
        //            MsgBox.baslik = "Gün Sonu";
        //            MsgBox.message = "Gün sonu yapılacak. Onaylıyor musunuz ?";
        //            MsgBox.BoxButtons = MessageBoxButtons.YesNo;
        //            msgBox.ShowDialog();
        //            if (MsgBox.result == DialogResult.Yes)
        //            {
        //                adisyonRepository.GunBasiYapildimiGuncelle("Hayır", Convert.ToDateTime(lbl_gunbasi.Text));
        //                adisyonRepository.GunBasiSonuEkleme(Convert.ToDateTime(lbl_gunbasi.Text), DateTime.Now);
        //                button3.Text = "GÜN BAŞI YAP";
        //                button3.BackColor = Color.DarkGreen;
        //                button3.Image = Properties.Resources.start;
        //                lbl_gunbasi.Text = "";
        //            }                    
        //        }
        //    }
        //    GunBasiKontrol();
        //}

        private void button4_Click(object sender, EventArgs e)
        {
            MasaAktar masaAktar = new MasaAktar();
            Rezervasyon.IslemYapilanEkran = "KasaEkrani";
            masaAktar.ShowDialog();
            this.Hide();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                MasalariGetir(adisyonRepository.MasaBolumleriFirstID());
            }
        }
    }
}
