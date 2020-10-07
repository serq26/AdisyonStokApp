using AdisyonProg.Core.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;
using AdisyonProg.Entity;

namespace AdisyonProg.WinApp
{
    public partial class YaziciAyarla : Form
    {
        List<YaziciBolumleri> yazicilar;
        public YaziciAyarla()
        {
            InitializeComponent();
            yazicilar = new List<YaziciBolumleri>();  
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void YaziciAyarla_Load(object sender, EventArgs e)
        {
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                comboBox1.DataSource = adisyonRepository.YaziciBolumleriGetir();

                foreach (string yazici in PrinterSettings.InstalledPrinters)
                {
                    comboBox2.Items.Add(yazici);
                }

                yazicilar = adisyonRepository.BolumYaziciGetir();
                DataGetir(yazicilar);
            }
        }
        private void DataGetir(List<YaziciBolumleri> list)
        {
            flowLayoutPanel1.Controls.Clear();
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                foreach (var item in list)
                {
                    Label lbl_bolum = new Label();
                    lbl_bolum.Text = item.BolumAdi + " - ";
                    lbl_bolum.ForeColor = Color.White;
                    lbl_bolum.Font = new Font("Century Gothic", 16, FontStyle.Bold);
                    lbl_bolum.Width = 200;
                    lbl_bolum.Margin = new Padding(20, 0, 0, 20);

                    Label lbl_yazici = new Label();
                    lbl_yazici.Text = item.YaziciAdi;
                    lbl_yazici.ForeColor = Color.White;
                    lbl_yazici.Font = new Font("Century Gothic", 16, FontStyle.Bold);
                    lbl_yazici.Width = 200;

                    flowLayoutPanel1.Controls.Add(lbl_bolum);
                    flowLayoutPanel1.Controls.Add(lbl_yazici);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            YaziciBolumEkle yaziciBolumEkle = new YaziciBolumEkle();
            yaziciBolumEkle.ShowDialog();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {            
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                int value = adisyonRepository.YaziciAdiEkle(comboBox1.Text, comboBox2.Text);

                MsgBox msgBox = new MsgBox();
                if (value > 0)
                {
                    MsgBox.baslik = "Bilgi";
                    MsgBox.BoxButtons = MessageBoxButtons.OK;
                    MsgBox.message = "İşlem başarılı.";
                    msgBox.ShowDialog();
                    yazicilar = adisyonRepository.BolumYaziciGetir();
                    DataGetir(yazicilar);
                }
                else
                {
                    MsgBox.baslik = "Uyarı";
                    MsgBox.BoxButtons = MessageBoxButtons.OK;
                    MsgBox.message = "İşlem başarısız..!";
                    msgBox.ShowDialog();
                }
            }  
        }
    }
}
