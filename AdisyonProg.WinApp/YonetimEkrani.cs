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
using Tulpep.NotificationWindow;

namespace AdisyonProg.WinApp
{
    public partial class YonetimEkrani : Form
    {
        public YonetimEkrani()
        {
            InitializeComponent();

            _thePanel.Location = new Point(
            this.ClientSize.Width / 2 - _thePanel.Size.Width / 2,
            this.ClientSize.Height / 2 - _thePanel.Size.Height / 2);
            _thePanel.Anchor = AnchorStyles.None;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Adisyonlar adisyonlar = new Adisyonlar();
            adisyonlar.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Stok stok = new Stok();
            stok.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
        }

        private void YonetimEkrani_FormClosed(object sender, FormClosedEventArgs e)
        {
            Giris giris = new Giris();
            giris.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Personel personel = new Personel();
            personel.Show();
        }

        private void StokKontrol()
        {
            List<Urun> BitenUrunler = new List<Urun>();

            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                BitenUrunler = adisyonRepository.StokBitenUrunler();
                if (BitenUrunler.Count > 0)
                {
                    pictureBox1.Image = Properties.Resources.notice;
                    for (int i = 0; i < BitenUrunler.Count; i++)
                    {
                        PopupNotifier popup = new PopupNotifier();
                        popup.Image = Properties.Resources.info;
                        popup.TitleText = "Stok Uyarı";
                        popup.ContentFont = new Font("Century Gothic", 14, FontStyle.Bold);
                        popup.ContentText = $"{BitenUrunler[i].UrunAdi} stok bitti..!";
                        popup.BodyColor = Color.Orange;
                        popup.BorderColor = Color.Orange;
                        popup.HeaderColor = Color.Orange;
                        popup.Delay = (i+1) * 1000;
                        popup.Click += Popup_Click;
                        popup.Popup();                        
                    }
                }
            }
        }

        private void Popup_Click(object sender, EventArgs e)
        {
            Bildirimler bildirimler = new Bildirimler();
            bildirimler.ShowDialog();
        }

        private void YonetimEkrani_Load(object sender, EventArgs e)
        {
            StokKontrol();
            timer1.Enabled = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Bildirimler bildirimler = new Bildirimler();
            bildirimler.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Siparisler siparisler = new Siparisler();
            siparisler.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string saat = DateTime.Now.ToShortDateString();
            lbl_tarih.Text = saat;
            lbl_saat.Text = DateTime.Now.ToLongTimeString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TakeAwayRapor takeAwayRapor = new TakeAwayRapor();
            takeAwayRapor.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Raporlar raporlar = new Raporlar();
            raporlar.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Masalar masalar = new Masalar();
            masalar.Show();
            this.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            YaziciAyarla yaziciAyarla = new YaziciAyarla();
            yaziciAyarla.ShowDialog();
        }
    }
}
