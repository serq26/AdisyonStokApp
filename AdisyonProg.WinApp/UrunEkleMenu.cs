using AdisyonProg.Core.Helper;
using AdisyonProg.Core.Repository;
using AdisyonProg.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Menu = AdisyonProg.Entity.Menu;

namespace AdisyonProg.WinApp
{
    public partial class UrunEkleMenu : Form
    {
        public static List<Urun> StoktanDusulecekUrunler;
        public UrunEkleMenu()
        {
            InitializeComponent();
            StoktanDusulecekUrunler = new List<Urun>();

        }

        private void btn_ekle_Click(object sender, EventArgs e)
        {
            MsgBox msgBox = new MsgBox();
            Entity.Menu menu = new Entity.Menu();

            this.Name = "form";
            Form f = this;
            bool control = Globalislemler.NullControl(f);

            if (control == true)
            {
                menu.UrunAdi = txt_urun_adi.Text;
                menu.UrunAciklama = txt_urun_aciklama.Text;
                menu.UrunFiyati = Convert.ToDecimal(txt_urun_fiyati.Text);
                menu.UrunKategori = cmb_urun_kategori.Text;
                menu.MaliyetFiyati = Convert.ToDecimal(txt_maliyet.Text);
                //menu.MaliyetFiyati = Decimal.Parse(txt_maliyet.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
                menu.SiparisCikicakYer = comboBox1.Text;

                using (AdisyonRepository adisyonRepository = new AdisyonRepository())
                {
                    int returnValue = adisyonRepository.MenuyeUrunEkle(menu);

                    int urunID = adisyonRepository.MenuUrunIDGetir(txt_urun_adi.Text);

                    //StokMenuUrun stokMenuUrun = new StokMenuUrun();

                    //if (StokMenuUrun.SecilenUrunler.Count > 0)
                    //{
                    //    for (int i = 0; i < StokMenuUrun.SecilenUrunler.Count; i++)
                    //    {
                    //        adisyonRepository.StokDusulucekUrunEkle(urunID, StokMenuUrun.SecilenUrunler[i].UrunAdi, StokMenuUrun.SecilenUrunler[i].UrunStokAdedi, StokMenuUrun.SecilenUrunler[i].BirimCinsi);
                    //    }
                    //}
                    if (StoktanDusulecekUrunler.Count > 0)
                    {
                        for (int i = 0; i < StoktanDusulecekUrunler.Count; i++)
                        {
                            adisyonRepository.StokDusulucekUrunEkle(urunID, StokMenuUrun.SecilenUrunler[i].UrunAdi, StokMenuUrun.SecilenUrunler[i].UrunStokAdedi, StokMenuUrun.SecilenUrunler[i].BirimCinsi);
                        }
                    }
                    if (returnValue > 0)
                    {
                        MsgBox.baslik = "Kayıt";
                        MsgBox.message = "Ürün ekleme işlemi başarılı...";
                        MsgBox.BoxButtons = MessageBoxButtons.OK;
                        msgBox.ShowDialog();

                        StoktanDusulecekUrunler.Clear();

                        txt_maliyet.Text = string.Empty;
                        txt_urun_aciklama.Text = string.Empty;
                        txt_urun_adi.Text = string.Empty;
                        txt_urun_fiyati.Text = string.Empty;
                        txt_urun_aciklama.Text = string.Empty;
                        label6.Text = string.Empty;
                    }
                    else
                    {
                        MsgBox.baslik = "Hata";
                        MsgBox.message = "Ürün ekleme işlemi başarısız..!";
                        MsgBox.BoxButtons = MessageBoxButtons.OK;
                        msgBox.ShowDialog();
                    }

                }
            }
            else
            {
                MsgBox.baslik = "Hata";
                MsgBox.message = "Tüm alanları doldurunuz..!";
                MsgBox.BoxButtons = MessageBoxButtons.OK;
                msgBox.ShowDialog();
            }          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StokMenuUrun stokMenuUrun = new StokMenuUrun();
            stokMenuUrun.ShowDialog();
            this.Hide();
        }

        private void UrunEkleMenu_Load(object sender, EventArgs e)
        {

            //StoktanDusulecekUrunler = StokMenuUrun.SecilenUrunler;

            using(AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                cmb_urun_kategori.DataSource = adisyonRepository.MenuKategoriGetir();
                comboBox1.DataSource = adisyonRepository.YaziciBolumleriGetir();
            }
        }

        void Yonlendir()
        {
            AdisyonProg.WinApp.Menu menu = new AdisyonProg.WinApp.Menu();
            this.Visible = false;
            menu.Show();
        }

        private void UrunEkleMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Yonlendir();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt_maliyet_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
        }

        private void txt_urun_fiyati_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
        }
    }
}
