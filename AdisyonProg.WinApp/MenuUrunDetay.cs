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
    public partial class MenuUrunDetay : Form
    {
        public static string UrunAdi;
        public static int UrunId;
        Entity.Menu Urun;
        public MenuUrunDetay()
        {
            InitializeComponent();
            Urun = new Entity.Menu();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MenuUrunDetay_Load(object sender, EventArgs e)
        {
            using(AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                
                comboBox1.DataSource = adisyonRepository.MenuKategoriGetir();
                
                Urun = adisyonRepository.MenudekiUrunuGetir(UrunId, UrunAdi);

                txt_urun_adi.Text = Urun.UrunAdi;
                txt_urun_fiyati.Text = Urun.UrunFiyati.ToString();
                txt_maliyet_fiyati.Text = Urun.MaliyetFiyati.ToString();
                comboBox1.Text = Urun.UrunKategori;
                txt_aciklama.Text = Urun.UrunAciklama;

                List<StoktanDusulecekUrunler> stoktanDusulecekUrunlers = new List<StoktanDusulecekUrunler>();
                stoktanDusulecekUrunlers = adisyonRepository.StoktanDusulecekUrunleriGetir(UrunId);

                foreach (var item in stoktanDusulecekUrunlers)
                {
                    item.UrunAdedi = Convert.ToInt32(item.UrunAdedi);
                }
                listBox1.DataSource = stoktanDusulecekUrunlers;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MsgBox msgBox = new MsgBox();
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                int value = adisyonRepository.MenuUrunGuncelle(UrunId, txt_urun_adi.Text, txt_aciklama.Text, Convert.ToDecimal(txt_urun_fiyati.Text), comboBox1.Text, Convert.ToDecimal(txt_maliyet_fiyati.Text));

                if (value > 0)
                {
                    MsgBox.baslik = "Bilgi";
                    MsgBox.BoxButtons = MessageBoxButtons.OK;
                    MsgBox.message = "Ürün başarıyla güncellendi.";
                    msgBox.ShowDialog();
                }
                else
                {
                    MsgBox.baslik = "Hata";
                    MsgBox.BoxButtons = MessageBoxButtons.OK;
                    MsgBox.message = "Ürün güncellenemedi..!";
                    msgBox.ShowDialog();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MsgBox msgBox = new MsgBox();
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                MsgBox.baslik = "Uyarı";
                MsgBox.message = "Ürünü silmek istiyor musunuz ?";
                MsgBox.BoxButtons = MessageBoxButtons.YesNo;
                msgBox.ShowDialog();
                
                if (MsgBox.result == DialogResult.Yes)
                {
                    msgBox.Close();

                    MsgBox msgBox1 = new MsgBox();

                    adisyonRepository.StokDusulecekSil(UrunId);
                    int value = adisyonRepository.MenudenUrunSil(UrunAdi);

                    if (value > 0)
                    {
                        MsgBox.baslik = "Bilgi";
                        MsgBox.message = "Ürün menüden silindi.";
                        MsgBox.BoxButtons = MessageBoxButtons.OK;
                        msgBox1.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MsgBox.baslik = "Hata";
                        MsgBox.message = "Ürün menüden silinemedi..!";
                        MsgBox.BoxButtons = MessageBoxButtons.OK;
                        msgBox1.ShowDialog();
                    }
                }
            }
        }

        void Yonlendir()
        {
            AdisyonProg.WinApp.Menu menu = new AdisyonProg.WinApp.Menu();
            this.Visible = false;
            menu.Show();
        }
        private void MenuUrunDetay_FormClosed(object sender, FormClosedEventArgs e)
        {
            Yonlendir();
        }

        private void button4_Click(object sender, EventArgs e)
        {            
            MsgBox msgBox = new MsgBox();
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                int value = adisyonRepository.MenuUrunGuncelle(UrunId, txt_urun_adi.Text, "Hızlı", Convert.ToDecimal(txt_urun_fiyati.Text), comboBox1.Text, Convert.ToDecimal(txt_maliyet_fiyati.Text));

                if (value > 0)
                {
                    MsgBox.baslik = "Bilgi";
                    MsgBox.BoxButtons = MessageBoxButtons.OK;
                    MsgBox.message = "Ürün Hızlı Menüye Eklendi.";
                    this.Close();
                }
                else
                {
                    MsgBox.baslik = "Hata";
                    MsgBox.BoxButtons = MessageBoxButtons.OK;
                    MsgBox.message = "Ürün eklenemedi..!";
                    msgBox.ShowDialog();
                }
            }          

        }
    }
}
