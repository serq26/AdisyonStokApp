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
    public partial class Siparisler : Form
    {
        List<Odeme> siparisler;
        public Siparisler()
        {
            InitializeComponent();
            siparisler = new List<Odeme>();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            
            using(AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                if(cmb.Text =="Masalara göre")
                {
                    dateTimePicker1.Visible = false;
                    dateTimePicker2.Visible = false;
                    button1.Visible = false;
                    comboBox2.Visible = true;
                    List<Masa> masalar = new List<Masa>();
                    masalar = adisyonRepository.MasalariGetir();
                    if (masalar.Count > 0)
                    {
                        masalar.RemoveAt(0);
                    }
                    comboBox2.DataSource = masalar;
                }
                else if(cmb.Text == "Siparişlere göre")
                {
                    dateTimePicker1.Visible = false;
                    dateTimePicker2.Visible = false;
                    button1.Visible = false;
                    comboBox2.Visible = false;
                    siparisler = adisyonRepository.SiparislereGoreSiparisler();
                    DuzenOlustur(siparisler);
                }
                else if (cmb.Text == "Tarihe göre")
                {
                    dateTimePicker1.Visible = true;
                    dateTimePicker2.Visible = true;
                    button1.Visible = true;
                    comboBox2.Visible = false;
                }
                else
                {
                    comboBox2.Visible = false;
                }                
            }       
        }

        private void DuzenOlustur(List<Odeme> siparisler)
        {
            flowLayoutPanel1.Controls.Clear();
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                var grpSiparisler = siparisler.GroupBy(x => x.OdenenSiparis).Select(Siparis => new { OdenenSiparis = Siparis.Key, SiparisAdedi = Siparis.Count() });

                var deneme = siparisler.GroupBy(x => x.OdenenSiparis).Select(Siparis => new { OdenenSiparis = Siparis.Key, SiparisAdedi = Siparis.Sum(s => s.SiparisAdedi), SiparisTutarı = Siparis.Sum(x => x.SiparisFiyati) });

                foreach (var item in deneme)
                {
                    GroupBox groupBox = new GroupBox();
                    groupBox.Text = "";
                    groupBox.Height = 175;
                    groupBox.Width = 317;

                    Label lbl_ad = new Label();
                    lbl_ad.Text = item.OdenenSiparis;
                    lbl_ad.ForeColor = Color.White;
                    lbl_ad.Font = new Font("Century Gothic", 18, FontStyle.Bold);
                    lbl_ad.Width = 250;
                    lbl_ad.Height = 30;
                    lbl_ad.Location = new Point(20, 30);

                    Label Adet = new Label();
                    Adet.Text ="- " + item.SiparisAdedi.ToString() + " adet" +  " -";
                    Adet.ForeColor = Color.Tomato;
                    Adet.Font = new Font("Century Gothic", 16, FontStyle.Bold);
                    Adet.Width = 140;
                    Adet.Location = new Point(20, 60);


                    //decimal urunFiyati = adisyonRepository.UrunFiyatiGetir(item.OdenenSiparis);
                    decimal urunFiyati = item.SiparisTutarı;
                    decimal urunMaliyetFiyati = adisyonRepository.UrunMaliyetFiyatiGetir(item.OdenenSiparis);
                    Label Fiyat = new Label();
                    Fiyat.Text = "Gelir: " + urunFiyati.ToString() + " - " + "Gider: " + (urunMaliyetFiyati * Convert.ToDecimal(item.SiparisAdedi)).ToString();
                    Fiyat.ForeColor = Color.DarkOrange;
                    Fiyat.Width = 270;
                    Fiyat.Font = new Font("Century Gothic", 14, FontStyle.Italic);
                    Fiyat.Location = new Point(20, 90);

                    Label kazanc = new Label();
                    kazanc.Text = "Kazanç: " + ((urunFiyati) - (urunMaliyetFiyati * Convert.ToDecimal(item.SiparisAdedi))).ToString() + " TL";
                    kazanc.ForeColor = Color.DarkOrange;
                    kazanc.Width = 200;
                    kazanc.Font = new Font("Century Gothic", 15, FontStyle.Bold);
                    kazanc.Location = new Point(20, 120);

                    groupBox.Controls.Add(lbl_ad);
                    groupBox.Controls.Add(Adet);
                    groupBox.Controls.Add(Fiyat);
                    groupBox.Controls.Add(kazanc);

                    this.flowLayoutPanel1.Controls.Add(groupBox);
                    panel2.Controls.Add(flowLayoutPanel1);
                    flowLayoutPanel1.Padding = new Padding(120, 0, 0, 0);
                }                
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            string value = comboBox.Text;

            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {        
                    siparisler = adisyonRepository.MasalaraGoreSiparisler(value);
                    DuzenOlustur(siparisler);               
            }
        }

        private void Siparisler_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                siparisler = adisyonRepository.TarihlereGoreSiparisler(dateTimePicker1.Value,dateTimePicker2.Value);
                DuzenOlustur(siparisler);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Siparisİptalleri siparisİptalleri = new Siparisİptalleri();
            siparisİptalleri.ShowDialog();
        }

    }
}
