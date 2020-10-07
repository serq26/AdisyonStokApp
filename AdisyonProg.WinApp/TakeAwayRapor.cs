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
    public partial class TakeAwayRapor : Form
    {
        public TakeAwayRapor()
        {
            InitializeComponent();
        }

        private void TakeAwayRapor_Load(object sender, EventArgs e)
        {
            using(AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                DuzenOlustur(adisyonRepository.TakeAwayRapor());
            }
        }
        private void DuzenOlustur(List<Takeaway> siparisler)
        {
            flowLayoutPanel1.Controls.Clear();
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                //var grpSiparisler = siparisler.GroupBy(x => x.Siparis).Select(Siparis => new { OdenenSiparis = Siparis.Key, SiparisAdedi = Siparis.Count() });
                var grpSiparisler = siparisler.GroupBy(i => i.Siparis).Select(i => new { Siparis = i.Key, Total = i.Sum(item => item.SiparisAdedi) });

                foreach (var item in grpSiparisler)
                {
                    GroupBox groupBox = new GroupBox();
                    groupBox.Text = "";
                    groupBox.Height = 175;
                    groupBox.Width = 317;

                    Label lbl_ad = new Label();
                    //lbl_ad.Text = item.OdenenSiparis;
                    lbl_ad.Text = item.Siparis;
                    lbl_ad.ForeColor = Color.White;
                    lbl_ad.Font = new Font("Century Gothic", 18, FontStyle.Bold);
                    lbl_ad.Width = 250;
                    lbl_ad.Height = 30;
                    lbl_ad.Location = new Point(20, 30);

                    Label Adet = new Label();
                    Adet.Text = "- " + item.Total.ToString() + " adet" + " -";
                    Adet.ForeColor = Color.Tomato;
                    Adet.Font = new Font("Century Gothic", 16, FontStyle.Bold);
                    Adet.Width = 140;
                    Adet.Location = new Point(20, 60);

                    decimal urunFiyati = adisyonRepository.UrunFiyatiGetir(item.Siparis);
                    decimal urunMaliyetFiyati = adisyonRepository.UrunMaliyetFiyatiGetir(item.Siparis);
                    //decimal urunFiyati = adisyonRepository.UrunFiyatiGetir(item.OdenenSiparis);
                    //decimal urunMaliyetFiyati = adisyonRepository.UrunMaliyetFiyatiGetir(item.OdenenSiparis);
                    Label Fiyat = new Label();
                    Fiyat.Text = "Gelir: " + (urunFiyati * Convert.ToDecimal(item.Total)).ToString() + " - " + "Gider: " + (urunMaliyetFiyati * Convert.ToDecimal(item.Total)).ToString();
                    Fiyat.ForeColor = Color.DarkOrange;
                    Fiyat.Width = 270;
                    Fiyat.Font = new Font("Century Gothic", 14, FontStyle.Italic);
                    Fiyat.Location = new Point(20, 90);

                    Label kazanc = new Label();
                    kazanc.Text = "Kazanç: " + ((urunFiyati * Convert.ToDecimal(item.Total)) - (urunMaliyetFiyati * Convert.ToDecimal(item.Total))).ToString() + " TL";
                    kazanc.ForeColor = Color.DarkOrange;
                    kazanc.Width = 200;
                    kazanc.Font = new Font("Century Gothic", 15, FontStyle.Bold);
                    kazanc.Location = new Point(20, 120);

                    groupBox.Controls.Add(lbl_ad);
                    groupBox.Controls.Add(Adet);
                    groupBox.Controls.Add(Fiyat);
                    groupBox.Controls.Add(kazanc);

                    this.flowLayoutPanel1.Controls.Add(groupBox);
                    flowLayoutPanel1.Padding = new Padding(120, 0, 0, 0);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                DuzenOlustur(adisyonRepository.TakeAwayRaporTariheGore(dateTimePicker1.Value, dateTimePicker2.Value));
            }
        }
    }
}
