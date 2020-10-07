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
    public partial class OdemeEkrani : Form
    {
        public static string yonlendirilenEkran;
        public static int masaID;
        public static string masaAdi;
        List<Odeme> odemeListesi;
        List<Odeme> odenmisUrunler;
        List<Odeme> secilenSiparisler;
        List<Adisyon> siparisler;
        List<Takeaway> takeSiparisler;
        decimal urunFiyati;
        decimal secilenUrunFiyati;
        MsgBox MsgBox;
        Control _focusedControl;
        GroupBox groupBox;
        public enum OdemeTipi
        {
            Undefined = 0,
            Nakit = 1,
            KrediKarti = 2,
            YemekCeki = 3,
            İkram = 4
        }
        public static OdemeTipi odemeTipi;
        public static int adisyonNo;

        public OdemeEkrani()
        {
            InitializeComponent();
            odemeListesi = new List<Odeme>();
            odenmisUrunler = new List<Odeme>();
            secilenSiparisler = new List<Odeme>();
            siparisler = new List<Adisyon>();
            MsgBox = new MsgBox();
            _focusedControl = new Control();
            groupBox = new GroupBox();
            takeSiparisler = new List<Takeaway>();

            Label title = new Label();
            title.Text = "Seçilen Siparişler";
            title.ForeColor = Color.White;
            title.Font = new Font("Century Gothic", 14, FontStyle.Bold | FontStyle.Underline);
            title.Width = 250;
            flowLayoutPanel2.Controls.Add(title);
        }

        private void OdemeEkrani_Load(object sender, EventArgs e)
        {
            OdenmisUrunleriGetir();
            DataDoldur();

            if (yonlendirilenEkran == "TakeAway")
            {
                button30.Visible = false;
            }
            else
            {
                button30.Visible = true;
            }
        }

        private void DataDoldur()
        {
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                if (yonlendirilenEkran == "TakeAway")
                {                    
                    takeSiparisler = adisyonRepository.TakeAwayOdemeDetay(adisyonNo);
                    dataGridView1.DataSource = takeSiparisler;
                }
                else if (yonlendirilenEkran == "MasaDetay")
                {
                    siparisler = adisyonRepository.OdemeDetay(masaID);
                    dataGridView1.DataSource = siparisler;
                    dataGridView1.Columns["AdisyonID"].Visible = false;
                    dataGridView1.Columns["MasaAdi"].Visible = false;
                }

                dataGridView1.Columns["AdisyonNo"].Visible = false;
                dataGridView1.Columns["SiparisTarihi"].Visible = false;

                dataGridView1.Columns["SiparisAdedi"].DisplayIndex = 0;
                dataGridView1.Columns["Siparis"].DisplayIndex = 1;
                dataGridView1.Columns["SiparisFiyati"].DisplayIndex = 2;

                dataGridView1.Columns["SiparisAdedi"].Width = 50;
                dataGridView1.Columns["Siparis"].Width = 200;
                dataGridView1.Columns["SiparisFiyati"].Width = 100;

                lbl_toplam.Text = dataGridView1.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["SiparisFiyati"].Value)).ToString();
                lbl_kalan.Text = lbl_toplam.Text;
            }
        }

        private void DataDoldur(List<Adisyon> MasadakiSiparisler)
        {
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                if (yonlendirilenEkran == "TakeAway")
                {
                    List<Takeaway> takeSiparisler = new List<Takeaway>();
                    takeSiparisler = adisyonRepository.TakeAwayOdemeDetay(adisyonNo);
                    dataGridView1.DataSource = takeSiparisler;
                }
                else
                {
                    MasadakiSiparisler = adisyonRepository.OdemeDetay(masaID);
                    dataGridView1.DataSource = MasadakiSiparisler;
                    dataGridView1.Columns["AdisyonID"].Visible = false;
                    dataGridView1.Columns["MasaAdi"].Visible = false;
                }

                dataGridView1.Columns["AdisyonNo"].Visible = false;
                dataGridView1.Columns["SiparisTarihi"].Visible = false;

                dataGridView1.Columns["SiparisAdedi"].DisplayIndex = 0;
                dataGridView1.Columns["Siparis"].DisplayIndex = 1;
                dataGridView1.Columns["SiparisFiyati"].DisplayIndex = 2;

                dataGridView1.Columns["SiparisAdedi"].Width = 50;
                dataGridView1.Columns["Siparis"].Width = 200;
                dataGridView1.Columns["SiparisFiyati"].Width = 100;

                lbl_toplam.Text = dataGridView1.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["SiparisFiyati"].Value)).ToString();
                lbl_kalan.Text = lbl_toplam.Text;
            }
        }

        private void OdenmisUrunleriGetir()
        {
            using(AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                flowLayoutPanel1.Controls.Clear();
                odenmisUrunler.Clear();
                odenmisUrunler = adisyonRepository.OdenmisUrunleriGetir(adisyonRepository.MasaAdisyonNo(masaID), masaID, "Açık");
                //odenmisUrunler = adisyonRepository.OdenmisUrunleriGetir(adisyonNo, masaID, "Açık");

                foreach (Odeme item in odenmisUrunler)
                {
                    Label odemeTipiFiyat = new Label();
                    if (item.Iskonto > 0)
                    {
                        odemeTipiFiyat.Text = item.Tarih.ToString("HH:mm") + "    " + item.OdenenSiparis + "( "+item.SiparisAdedi + " )"+"    " + item.OdemeTipi + "    " + (item.SiparisFiyati * item.SiparisAdedi).ToString() + "  TL" + "   " + "%" + item.Iskonto + " indirim";
                    }
                    else
                    {
                        odemeTipiFiyat.Text = item.Tarih.ToString("HH:mm") + "    " + item.OdenenSiparis + "( " + item.SiparisAdedi + " )" + "    " + item.OdemeTipi + "    " + (item.SiparisFiyati * item.SiparisAdedi).ToString() + "  TL";
                    }
                    odemeTipiFiyat.ForeColor = Color.Wheat;
                    odemeTipiFiyat.Width = 600;
                    odemeTipiFiyat.Font = new Font("Century Gothic", 16, FontStyle.Italic);
                    flowLayoutPanel1.Controls.Add(odemeTipiFiyat);
                } 
            }
        }

        #region SayıButtonları
        private void button1_Click(object sender, EventArgs e)
        {
            if(chc_iskonto.Checked)
            {
                txt_iskonto.Text = txt_iskonto.Text + ((Button)sender).Text;
            }
            else
            {
                txt_parcali.Text = txt_parcali.Text + ((Button)sender).Text;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (chc_iskonto.Checked)
            {
                txt_iskonto.Text = txt_iskonto.Text + ((Button)sender).Text;
            }
            else
            {
                txt_parcali.Text = txt_parcali.Text + ((Button)sender).Text;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (chc_iskonto.Checked)
            {
                txt_iskonto.Text = txt_iskonto.Text + ((Button)sender).Text;
            }
            else
            {
                txt_parcali.Text = txt_parcali.Text + ((Button)sender).Text;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (chc_iskonto.Checked)
            {
                txt_iskonto.Text = txt_iskonto.Text + ((Button)sender).Text;
            }
            else
            {
                txt_parcali.Text = txt_parcali.Text + ((Button)sender).Text;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (chc_iskonto.Checked)
            {
                txt_iskonto.Text = txt_iskonto.Text + ((Button)sender).Text;
            }
            else
            {
                txt_parcali.Text = txt_parcali.Text + ((Button)sender).Text;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (chc_iskonto.Checked)
            {
                txt_iskonto.Text = txt_iskonto.Text + ((Button)sender).Text;
            }
            else
            {
                txt_parcali.Text = txt_parcali.Text + ((Button)sender).Text;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(chc_iskonto.Checked)
            {
                txt_iskonto.Text = txt_iskonto.Text + ((Button)sender).Text;
            }
            else
            {
                txt_parcali.Text = txt_parcali.Text + ((Button)sender).Text;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (chc_iskonto.Checked)
            {
                txt_iskonto.Text = txt_iskonto.Text + ((Button)sender).Text;
            }
            else
            {
                txt_parcali.Text = txt_parcali.Text + ((Button)sender).Text;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (chc_iskonto.Checked)
            {
                txt_iskonto.Text = txt_iskonto.Text + ((Button)sender).Text;
            }
            else
            {
                txt_parcali.Text = txt_parcali.Text + ((Button)sender).Text;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (chc_iskonto.Checked)
            {
                txt_iskonto.Text = txt_iskonto.Text + ((Button)sender).Text;
            }
            else
            {
                txt_parcali.Text = txt_parcali.Text + ((Button)sender).Text;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (chc_iskonto.Checked)
            {
                txt_iskonto.Text = txt_iskonto.Text + ((Button)sender).Text;
            }
            else
            {
                txt_parcali.Text = txt_parcali.Text + ((Button)sender).Text;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (txt_iskonto.Text.Length > 1)
            {
                txt_iskonto.Text = txt_iskonto.Text.Substring(0, txt_iskonto.Text.Length - 1);
            }
            else
            {
                txt_iskonto.Text = string.Empty;
                //txt_iskonto.Text = 0.ToString();
            }

            //if (_focusedControl.Name != null && _focusedControl.Name == txt_parcali.Name)
            //{
            //    if (txt_parcali.Text.Length > 1)
            //    {
            //        txt_parcali.Text = txt_parcali.Text.Substring(0, txt_parcali.Text.Length - 1);
            //    }
            //    else
            //    {
            //        txt_parcali.Text = string.Empty;
            //    }
            //}
            //else if(_focusedControl.Name != null && _focusedControl.Name == txt_iskonto.Name)
            //{
            //    if (txt_iskonto.Text.Length > 1)
            //    {
            //        txt_iskonto.Text = txt_iskonto.Text.Substring(0, txt_iskonto.Text.Length - 1);
            //    }
            //    else
            //    {
            //        txt_iskonto.Text = string.Empty;
            //    }
            //}

        }
        #endregion

        #region HızlıSayılar
        private void button13_Click(object sender, EventArgs e)
        {
            txt_parcali.Text = ((Button)sender).Text +",00";
        }

        private void button14_Click(object sender, EventArgs e)
        {
            txt_parcali.Text = ((Button)sender).Text + ",00";
        }

        private void button15_Click(object sender, EventArgs e)
        {
            txt_parcali.Text = ((Button)sender).Text + ",00";
        }

        private void button16_Click(object sender, EventArgs e)
        {
            txt_parcali.Text = ((Button)sender).Text + ",00";
        }

        private void button17_Click(object sender, EventArgs e)
        {
            txt_parcali.Text = ((Button)sender).Text + ",00";
        }

        private void button18_Click(object sender, EventArgs e)
        {
            txt_parcali.Text = ((Button)sender).Text + ",00";
        }
        #endregion

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[index];

            if (Convert.ToDecimal(selectedRow.Cells["SiparisAdedi"].Value) != 0)
            {
                secilenUrunFiyati = (Convert.ToDecimal(selectedRow.Cells["SiparisFiyati"].Value) / Convert.ToDecimal(selectedRow.Cells["SiparisAdedi"].Value));
            }

            if ((int)selectedRow.Cells["SiparisAdedi"].Value > 0)
            {  
                if(odemeTipi != OdemeTipi.Undefined)
                {
                    using (AdisyonRepository adisyonRepository = new AdisyonRepository())
                    {
                        Odeme odeme = new Odeme();
                        odeme.MasaID = masaID;
                        odeme.MasaAdi = masaAdi;
                        odeme.AdisyonNo = adisyonRepository.MasaAdisyonNumarasiniGetir(masaAdi);
                        //odeme.AdisyonNo = adisyonNo;
                        odeme.OdenenSiparis = selectedRow.Cells["Siparis"].Value.ToString();
                        odeme.SiparisFiyati = (Convert.ToDecimal(selectedRow.Cells["SiparisFiyati"].Value) / Convert.ToDecimal(selectedRow.Cells["SiparisAdedi"].Value));
                        odeme.SiparisAdedi = 1;
                        odeme.Tarih = DateTime.Now;
                        odeme.OdemeTipi = odemeTipi.ToString();
                        //odeme.Iskonto = Convert.ToInt32(txt_iskonto.Text);
                        odeme.MasaDurumu = "Açık";

                        if (odemeListesi.Any(x => (x.OdenenSiparis == selectedRow.Cells["Siparis"].Value.ToString())))
                        {
                            var indexs = odemeListesi.FindIndex(x => (x.OdenenSiparis == selectedRow.Cells["Siparis"].Value.ToString()));
                            odemeListesi[indexs].SiparisAdedi += 1;
                        }
                        else
                        {                            
                            secilenSiparisler.Add(odeme);
                            odemeListesi.Add(odeme);
                        }
                    }
                    SecilenSiparisleriGoster(selectedRow.Cells["Siparis"].Value.ToString());

                    

                    urunFiyati = Convert.ToDecimal(selectedRow.Cells["SiparisFiyati"].Value) / Convert.ToDecimal(selectedRow.Cells["SiparisAdedi"].Value);
                    lbl_tahsil.Text = (Convert.ToDecimal(lbl_tahsil.Text) + urunFiyati).ToString();
                    selectedRow.Cells["SiparisAdedi"].Value = (int)selectedRow.Cells["SiparisAdedi"].Value - 1;
                    
                    selectedRow.Cells["SiparisFiyati"].Value = (decimal)selectedRow.Cells["SiparisFiyati"].Value - urunFiyati;
                    lbl_kalan.Text = (Convert.ToDecimal(lbl_toplam.Text) - Convert.ToDecimal(lbl_tahsil.Text)).ToString();
                    if(chc_iskonto.Checked == false)
                    {
                        secilenUrunFiyati = 0;
                    }
                }
                else
                {
                    MsgBox.baslik = "Uyarı";
                    MsgBox.message = "Ödeme tipi seçiniz..!";
                    MsgBox.BoxButtons = MessageBoxButtons.OK;
                    MsgBox.ShowDialog();
                }               
            }           
        }

        private void SecilenSiparisleriGoster(string siparis)
        {
            Label secilenSiparis = new Label();
            Label secilenSiparisAdedi = new Label();
            int siparisAdedi = 0;
                       
            foreach (var item in secilenSiparisler)
            {
                if(item.OdenenSiparis == siparis)
                {
                    siparisAdedi = item.SiparisAdedi;

                    if (siparisAdedi > 1)
                    {
                        foreach (Control control in flowLayoutPanel2.Controls)
                        {
                            if (control.Name == siparis)
                            {
                                foreach (Control items in control.Controls)
                                {
                                    if (items.Name == siparis)
                                    {
                                        items.Text = " ( " + siparisAdedi + " )";
                                    }
                                }
                            }                            
                        }
                    }
                    else
                    {                       
                        GroupBox grp = new GroupBox();
                        grp.Name = siparis;
                        secilenSiparis.Click += SecilenSiparis_Click;
                        foreach (var sprs in secilenSiparisler)
                        {                            
                            grp.Height = 50;
                            grp.Width = 300;

                            secilenSiparis.Text = sprs.OdenenSiparis;
                            secilenSiparis.ForeColor = Color.White;
                            secilenSiparis.Width =200;
                            secilenSiparis.Font = new Font("Century Gothic", 12, FontStyle.Bold);
                            secilenSiparis.Location = new Point(20, 15);

                            secilenSiparisAdedi.Text = " ( " + sprs.SiparisAdedi + " )";
                            secilenSiparisAdedi.ForeColor = Color.Tomato;
                            secilenSiparisAdedi.Name = sprs.OdenenSiparis;
                            secilenSiparisAdedi.Width = 50;
                            secilenSiparisAdedi.Font = new Font("Century Gothic", 13, FontStyle.Bold);
                            secilenSiparisAdedi.Location = new Point(240, 15);

                            grp.Controls.Add(secilenSiparis);
                            grp.Controls.Add(secilenSiparisAdedi);
                            flowLayoutPanel2.Controls.Add(grp);
                        }
                    }
                }
            }        
        }

        private void TumunuGoster(List<Odeme> tumSiparisler)
        {
            foreach (var item in tumSiparisler)
            {
                Label secilenSiparis = new Label();
                Label secilenSiparisAdedi = new Label();

                GroupBox grp = new GroupBox();
                grp.Name = item.OdenenSiparis;
                secilenSiparis.Click += SecilenSiparis_Click;

                grp.Height = 50;
                grp.Width = 300;

                secilenSiparis.Text = item.OdenenSiparis;
                secilenSiparis.ForeColor = Color.White;
                secilenSiparis.Width = 200;
                secilenSiparis.Font = new Font("Century Gothic", 12, FontStyle.Bold);
                secilenSiparis.Location = new Point(20, 15);

                secilenSiparisAdedi.Text = " ( " + item.SiparisAdedi + " )";
                secilenSiparisAdedi.ForeColor = Color.Tomato;
                secilenSiparisAdedi.Name = item.OdenenSiparis;
                secilenSiparisAdedi.Width = 50;
                secilenSiparisAdedi.Font = new Font("Century Gothic", 13, FontStyle.Bold);
                secilenSiparisAdedi.Location = new Point(240, 15);

                grp.Controls.Add(secilenSiparis);
                grp.Controls.Add(secilenSiparisAdedi);
                flowLayoutPanel2.Controls.Add(grp);

            }
        }

        private void SecilenSiparis_Click(object sender, EventArgs e)
        {
            string txt = ((Label)sender).Text;            

            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                if(item.Cells["Siparis"].Value.ToString() == txt)
                {
                    using(AdisyonRepository adisyonRepository = new AdisyonRepository())
                    {
                        item.Cells["SiparisAdedi"].Value = (int)item.Cells["SiparisAdedi"].Value + 1;
                        item.Cells["SiparisFiyati"].Value = (decimal)item.Cells["SiparisFiyati"].Value + adisyonRepository.UrunFiyatiGetir(txt);

                        lbl_tahsil.Text = (Convert.ToDecimal(lbl_tahsil.Text) - adisyonRepository.UrunFiyatiGetir(txt)).ToString();
                        lbl_kalan.Text = (Convert.ToDecimal(lbl_kalan.Text) + adisyonRepository.UrunFiyatiGetir(txt)).ToString();
                    }                    

                    foreach (Odeme siparis in secilenSiparisler.ToList())
                    {
                        if (siparis.OdenenSiparis == txt)
                        {
                            if (siparis.SiparisAdedi > 1)
                            {
                                siparis.SiparisAdedi -= 1;

                                foreach (Control grps in flowLayoutPanel2.Controls)
                                {
                                    if (grps.Name == txt)
                                    {
                                        foreach (Control items in grps.Controls)
                                        {
                                            if (items.Name == txt)
                                            {
                                                items.Text = " ( " + siparis.SiparisAdedi + " )";
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                secilenSiparisler.Remove(siparis);
                                odemeListesi.Remove(siparis);

                                foreach (Control cnt in flowLayoutPanel2.Controls)
                                {
                                    if (cnt.Name == siparis.OdenenSiparis)
                                    {
                                        flowLayoutPanel2.Controls.RemoveByKey(cnt.Name);
                                    }
                                }
                            }                            
                        }
                    }
                }
            }
        }

        #region OdemeTipiButonları
        private void button25_Click(object sender, EventArgs e)
        {
            odemeTipi = OdemeTipi.Nakit;
            lbl_odeme_tipi.Text = ((Button)sender).Text;
        }

        private void button26_Click(object sender, EventArgs e)
        {
            odemeTipi = OdemeTipi.KrediKarti;
            lbl_odeme_tipi.Text = ((Button)sender).Text;
        }

        private void button28_Click(object sender, EventArgs e)
        {
            odemeTipi = OdemeTipi.YemekCeki;
            lbl_odeme_tipi.Text = ((Button)sender).Text;
        }

        private void button27_Click(object sender, EventArgs e)
        {
            odemeTipi = OdemeTipi.İkram;
            lbl_odeme_tipi.Text = ((Button)sender).Text;
        }

        #endregion


        private void button30_Click(object sender, EventArgs e)
        {
            MsgBox.baslik = "Bilgi";
            MsgBox.BoxButtons = MessageBoxButtons.YesNo;
            MsgBox.message = "Masa Kapatılacak onaylıyor musunuz ?";
            MsgBox.ShowDialog();

            if (MsgBox.result == DialogResult.Yes)
            {
                using (AdisyonRepository adisyonRepository = new AdisyonRepository())
                {
                    foreach (DataGridViewRow item in dataGridView1.Rows)
                    {
                        adisyonRepository.SiparisSil(adisyonNo, item.Cells["Siparis"].Value.ToString());
                    }
                    adisyonRepository.MasaDurumunuGuncelle(masaID, "Kapalı", DateTime.Now, 0);
                    //MasaDetay.masaKapandimi = "Evet";
                    this.Close();
                }
            }            

            //foreach (Odeme item in odemeListesi)
            //{
            //    using(AdisyonRepository adisyonRepository = new AdisyonRepository())
            //    {
            //        item.MasaDurumu = "Kapalı";
            //        adisyonRepository.OdemeEkle(item);
            //    }
            //}           
        }

        private void button31_Click(object sender, EventArgs e)
        {

            if (Convert.ToDecimal(lbl_kalan.Text) <= 0)
            {
                MsgBox.baslik = "Bilgi";
                MsgBox.message = "Hesap ödendi.";
                MsgBox.message = "Hesap ödendi.";
                MsgBox.BoxButtons = MessageBoxButtons.OK;
                MsgBox.ShowDialog();
                txt_parcali.Text = string.Empty;
            }
            else
            {
                lbl_tahsil.Text = (Convert.ToDecimal(lbl_tahsil.Text) + Convert.ToDecimal(txt_parcali.Text)).ToString();
                lbl_kalan.Text = (Convert.ToDecimal(lbl_toplam.Text) - Convert.ToDecimal(lbl_tahsil.Text)).ToString();

                Label odemeTipiFiyat = new Label();
                odemeTipiFiyat.Text = DateTime.Now.ToString("HH:mm") + "    " + odemeTipi.ToString() + "    " + (Convert.ToDecimal(txt_parcali.Text)).ToString("0.00") + "   TL";

                odemeTipiFiyat.ForeColor = Color.Green;
                odemeTipiFiyat.Width = 600;
                odemeTipiFiyat.Font = new Font("Century Gothic", 16, FontStyle.Italic);
                flowLayoutPanel1.Controls.Add(odemeTipiFiyat);
                txt_parcali.Text = string.Empty;
            }           
        }

        private void button19_Click(object sender, EventArgs e)
        {
            txt_parcali.Text = string.Empty;
        }

        private void button23_Click(object sender, EventArgs e)
        {  
            if (chc_iskonto.Checked == true)
            {

                chc_iskonto.Checked = false;
                button23.BackColor = Color.Transparent;
                //secilenUrunFiyati = 0;
                txt_iskonto.Text = 0.ToString();
                txt_iskonto.BackColor = Color.White;
                txt_iskonto.Text = 0.ToString();
            }
            else
            {
                txt_iskonto.Focus();
                txt_iskonto.Text = string.Empty;
                //txt_iskonto.Text = 0.ToString();
                txt_iskonto.ForeColor = Color.White;
                txt_iskonto.BackColor = Color.Blue;
                button23.BackColor = Color.Blue;
                chc_iskonto.Checked = true;
            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            foreach (var item in secilenSiparisler)
            {
                if (item.SiparisFiyati != 0)
                {
                    decimal iskonto = ((item.SiparisFiyati * Convert.ToDecimal(txt_iskonto.Text)) / 100);

                    //lbl_toplam.Text = (Convert.ToDecimal(lbl_toplam.Text) - iskonto).ToString();

                    lbl_tahsil.Text = (Convert.ToDecimal(lbl_tahsil.Text) - iskonto).ToString();

                    item.SiparisFiyati = item.SiparisFiyati - iskonto;

                    item.Iskonto = Convert.ToInt32(txt_iskonto.Text);
                }
                else
                {
                    MsgBox.baslik = "Uyarı";
                    MsgBox.message = "İndirim yapılacak sipariş seçiniz..!";
                    MsgBox.BoxButtons = MessageBoxButtons.OK;
                    MsgBox.ShowDialog();
                }
            }

            txt_iskonto.Text = 0.ToString();
        }

        private void button33_Click(object sender, EventArgs e)
        {
            MsgBox msgBox = new MsgBox();

            if (secilenSiparisler.Count > 0)
            {
                foreach (var item in secilenSiparisler)
                {
                    Label odemeTipiFiyat = new Label();
                    if (item.Iskonto > 0)
                    {
                        odemeTipiFiyat.Text = DateTime.Now.ToString("HH:mm") + "    " + item.OdenenSiparis + " ( " + item.SiparisAdedi + " )" + "    " + odemeTipi.ToString() + "    " + (Convert.ToDecimal(item.SiparisFiyati) * Convert.ToDecimal(item.SiparisAdedi)).ToString() + " TL" + "   " + "%" + item.Iskonto + " indirim";
                    }
                    else
                    {
                        odemeTipiFiyat.Text = DateTime.Now.ToString("HH:mm") + "    " + item.OdenenSiparis + " ( " + item.SiparisAdedi + " )" + "    " + odemeTipi.ToString() + "    " + (Convert.ToDecimal(item.SiparisFiyati) * Convert.ToDecimal(item.SiparisAdedi)).ToString() + " TL";
                    }

                    odemeTipiFiyat.ForeColor = Color.Wheat;
                    odemeTipiFiyat.Width = 700;
                    odemeTipiFiyat.Font = new Font("Century Gothic", 16, FontStyle.Italic);
                    flowLayoutPanel1.Controls.Add(odemeTipiFiyat);
                }
                secilenSiparisler.Clear();
                using (AdisyonRepository adisyonRepository = new AdisyonRepository())
                {
                    //foreach (Odeme item in odemeListesi)
                    //{
                    //    int value = adisyonRepository.OdemeEkle(item);
                    //    {
                    //        if (value > 0)
                    //        {
                    //            adisyonRepository.SiparisAdetSil(item.MasaID, item.AdisyonNo, item.OdenenSiparis, item.SiparisAdedi, item.SiparisFiyati);
                    //        }
                    //    }
                    //}

                    for (int i = 0; i < odemeListesi.Count(); i++)
                    {
                        int value = adisyonRepository.OdemeEkle(odemeListesi[i]);
                        {
                            if (value > 0)
                            {
                                //adisyonRepository.SiparisAdetSil(odemeListesi[i].MasaID, odemeListesi[i].AdisyonNo, odemeListesi[i].OdenenSiparis, odemeListesi[i].SiparisAdedi, odemeListesi[i].SiparisFiyati);
                                adisyonRepository.SiparisAdetSil(odemeListesi[i].MasaID, odemeListesi[i].AdisyonNo, odemeListesi[i].OdenenSiparis, odemeListesi[i].SiparisAdedi, adisyonRepository.UrunFiyatiGetir(odemeListesi[i].OdenenSiparis));
                            }
                        }
                    }

                    MsgBox.baslik = "Ödeme";
                    MsgBox.message = "Ödeme işlemi başarılı...";
                    MsgBox.BoxButtons = MessageBoxButtons.OK;
                    msgBox.ShowDialog();

                    if (yonlendirilenEkran == "TakeAway")
                    {
                        adisyonRepository.TakeAwayOdemeGuncelle(adisyonNo, "Evet");
                        adisyonRepository.AdisyonNoGuncelle(1);
                        TakeAway.AdisyonNumarasi = adisyonRepository.SıradakiAdisyonNo();
                    }

                    DataDoldur(adisyonRepository.OdemeDetay(masaID));

                    odemeListesi.Clear();
                    secilenSiparisler.Clear();
                    takeSiparisler.Clear();
                    flowLayoutPanel2.Controls.Clear();
                    lbl_tahsil.Text = "0,00";
                }
            }
            else

            {
                MsgBox.baslik = "Uyarı";
                MsgBox.BoxButtons = MessageBoxButtons.OK;
                MsgBox.message = "Ödenecek ürün seçiniz..!";
                msgBox.ShowDialog();
            }            
        }

        private void button29_Click(object sender, EventArgs e)
        {
            odemeTipi = OdemeTipi.Undefined;
            //KasaEkrani kasaEkrani = new KasaEkrani();
            //this.Visible = false;
            //kasaEkrani.Show();

            //MasaDetay masaDetay = (MasaDetay)Application.OpenForms["MasaDetay"];
            //masaDetay.DataDoldur();
            this.Close();
        }

        private void txt_parcali_MouseClick(object sender, MouseEventArgs e)
        {
            _focusedControl = (Control)sender;
        }

        private void txt_iskonto_MouseClick(object sender, MouseEventArgs e)
        {
            _focusedControl = (Control)sender;
        }

        private void button22_Click(object sender, EventArgs e)
        {

        }

        private void button24_Click(object sender, EventArgs e)
        {
            if (odemeTipi != OdemeTipi.Undefined)
            {
                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    using (AdisyonRepository adisyonRepository = new AdisyonRepository())
                    {
                        Odeme odeme = new Odeme();
                        odeme.MasaID = masaID;
                        odeme.MasaAdi = masaAdi;
                        odeme.AdisyonNo = adisyonRepository.MasaAdisyonNumarasiniGetir(masaAdi);
                        //odeme.AdisyonNo = adisyonNo;
                        odeme.OdenenSiparis = item.Cells["Siparis"].Value.ToString();
                        odeme.SiparisFiyati = (Convert.ToDecimal(item.Cells["SiparisFiyati"].Value) / Convert.ToDecimal(item.Cells["SiparisAdedi"].Value));
                        odeme.SiparisAdedi = Convert.ToInt32(item.Cells["SiparisAdedi"].Value);
                        odeme.Tarih = DateTime.Now;
                        odeme.OdemeTipi = odemeTipi.ToString();
                        odeme.Iskonto = Convert.ToInt32(txt_iskonto.Text);
                        odeme.MasaDurumu = "Açık";

                        secilenSiparisler.Add(odeme);
                        odemeListesi.Add(odeme);

                        dataGridView1.Rows[item.Index].Cells["SiparisAdedi"].Value = Convert.ToInt32(dataGridView1.Rows[item.Index].Cells["SiparisAdedi"].Value) - (int)odeme.SiparisAdedi;
                        dataGridView1.Rows[item.Index].Cells["SiparisFiyati"].Value = Convert.ToDecimal(dataGridView1.Rows[item.Index].Cells["SiparisFiyati"].Value) - ((decimal)odeme.SiparisFiyati * odeme.SiparisAdedi);

                        //urunFiyati = odeme.SiparisFiyati / Convert.ToDecimal(odeme.SiparisAdedi);
                        urunFiyati = odeme.SiparisFiyati * odeme.SiparisAdedi;
                        lbl_tahsil.Text = (Convert.ToDecimal(lbl_tahsil.Text) + urunFiyati).ToString();
                        lbl_kalan.Text = (Convert.ToDecimal(lbl_toplam.Text) - Convert.ToDecimal(lbl_tahsil.Text)).ToString();

                    }
                }
            }
            else
            {
                MsgBox.baslik = "Uyarı";
                MsgBox.message = "Ödeme tipi seçiniz..!";
                MsgBox.BoxButtons = MessageBoxButtons.OK;
                MsgBox.ShowDialog();
            }
            TumunuGoster(secilenSiparisler);
        }
    }
}
