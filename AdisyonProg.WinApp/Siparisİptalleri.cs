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
    public partial class Siparisİptalleri : Form
    {
        List<SiparisIptal> siparisler;
        public Siparisİptalleri()
        {
            InitializeComponent();
            siparisler = new List<SiparisIptal>();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Siparisİptalleri_Load(object sender, EventArgs e)
        {
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                DuzenOlustur(adisyonRepository.SiparisIptalGetir());
            }
        }

        private void DuzenOlustur(List<SiparisIptal> siparisler)
        {
            flowLayoutPanel1.Controls.Clear();
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                var grpSiparisler = siparisler.GroupBy(x => x.Siparis).Select(Siparis => new { Siparis = Siparis.Key, SiparisAdedi = Siparis.Count()});

                var deneme = siparisler.GroupBy(x => x.Siparis).Select(Siparis => new { Siparis = Siparis.Key, SiparisAdedi = Siparis.Sum(s => s.SiparisAdedi) });

                foreach (var item in deneme)
                {
                    GroupBox groupBox = new GroupBox();
                    groupBox.Text = "";
                    groupBox.Height = 175;
                    groupBox.Width = 317;

                    Label lbl_ad = new Label();
                    lbl_ad.Text = item.Siparis;
                    lbl_ad.ForeColor = Color.White;
                    lbl_ad.Font = new Font("Century Gothic", 18, FontStyle.Bold);
                    lbl_ad.Width = 250;
                    lbl_ad.Height = 30;
                    lbl_ad.Location = new Point(20, 30);

                    Label Adet = new Label();
                    Adet.Text = "- " + item.SiparisAdedi.ToString() + " adet" + " -";
                    Adet.ForeColor = Color.Tomato;
                    Adet.Font = new Font("Century Gothic", 16, FontStyle.Bold);
                    Adet.Width = 140;
                    Adet.Location = new Point(20, 60);


                    decimal urunFiyati = adisyonRepository.UrunFiyatiGetir(item.Siparis);
                    decimal urunMaliyetFiyati = adisyonRepository.UrunMaliyetFiyatiGetir(item.Siparis);


                    Label kazanc = new Label();
                 
                    kazanc.Text = "Maliyet Fiyatı: " + (adisyonRepository.UrunMaliyetFiyatiGetir(item.Siparis)).ToString() + " TL";
                    kazanc.ForeColor = Color.DarkOrange;
                    kazanc.Width = 280;
                    kazanc.Font = new Font("Century Gothic", 15, FontStyle.Bold);
                    kazanc.Location = new Point(20, 120);

                    groupBox.Controls.Add(lbl_ad);
                    groupBox.Controls.Add(Adet);
                    //groupBox.Controls.Add(Fiyat);
                    groupBox.Controls.Add(kazanc);

                    this.flowLayoutPanel1.Controls.Add(groupBox);
                    panel2.Controls.Add(flowLayoutPanel1);
                    flowLayoutPanel1.Padding = new Padding(120, 0, 0, 0);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                siparisler = adisyonRepository.SiparisİptalTariheGore(dateTimePicker1.Value, dateTimePicker2.Value);
                DuzenOlustur(siparisler);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string txt = ((ComboBox)sender).Text;
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                List<SiparisIptal> stoktanDusulmeyenler = new List<SiparisIptal>();

                if (txt == "Stoktan Düşenler")
                {
                    siparisler = adisyonRepository.StoktanDusulenIptaller();
                    DuzenOlustur(siparisler);
                }
                else
                {
                    foreach (var item in adisyonRepository.SiparisIptalGetir())
                    {
                        if (item.StoktanDusuldumu == "No")
                        {
                            stoktanDusulmeyenler.Add(item);
                        }
                    }
                    DuzenOlustur(stoktanDusulmeyenler);
                }
            }
        }
    }
}
