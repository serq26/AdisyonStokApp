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
using System.Diagnostics;
using Menu = AdisyonProg.Entity.Menu;
using System.IO;

namespace AdisyonProg.WinApp
{
    public partial class MasaDetay : Form
    {
        public static string MasaAdi;
        public static int MasaId;
        List<Entity.Menu> TumUrunler;
        List<Adisyon> MasadakiSiparisler;
        public static DateTime saat;
        int AdisyonNumarasi;
        List<Adisyon> YeniSiparisler;
        List<MenuKategori> Kategoriler;
        string secilenSiparis = string.Empty;
        //public static string masaKapandimi;
        public static string softIcecek;
        public static List<string> softIcecekler;

        public MasaDetay()
        {
            InitializeComponent();
            TumUrunler = new List<Entity.Menu>();
            MasadakiSiparisler = new List<Adisyon>();
            YeniSiparisler = new List<Adisyon>();
            Kategoriler = new List<MenuKategori>();
            softIcecekler = new List<string>();
            saat = new DateTime();
            TabPageOlustur();
        }

        private void MasaDetay_Load(object sender, EventArgs e)
        {
            lbl_masa_adi.Text = MasaAdi;
            YeniSiparisler.Clear();
            DataDoldur();
            groupBox1.Text = MasaAdi + " " +"Siparişler";
            groupBox1.ForeColor = Color.White;

            using(AdisyonRepository adisyonRepository = new AdisyonRepository())
            {               
                AdisyonNumarasi = adisyonRepository.MasaAdisyonNo(MasaId);
                //GarsonEkrani.AdisyonNo = adisyonRepository.SıradakiAdisyonNo();
                //KasaEkrani.AdisyonNo = adisyonRepository.SıradakiAdisyonNo();
                string MasaDurumu = adisyonRepository.MasaDurumunuGetir(MasaId);
                if(MasaDurumu == "Açık")
                {
                    button2.Text = "Masayı Kapat";
                    button2.BackColor = Color.DarkRed;
                }
            }
        }           

        public void DataDoldur()
        {
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                MasadakiSiparisler = adisyonRepository.MasadakiSiparisleriGetir(MasaId);
                dataGridView1.DataSource = MasadakiSiparisler;

                dataGridView1.Columns["AdisyonID"].Visible = false;
                dataGridView1.Columns["AdisyonNo"].Visible = false;
                dataGridView1.Columns["MasaAdi"].Visible = false;
                dataGridView1.Columns["SiparisTarihi"].Visible = false;
                dataGridView1.Columns["SiparisFiyati"].Visible = false;

                dataGridView1.Columns["Siparis"].DisplayIndex = 0;
                dataGridView1.Columns["SiparisAdedi"].DisplayIndex = 1;                

                dataGridView1.Columns["SiparisAdedi"].Width = 50;
                dataGridView1.Columns["Siparis"].Width = 235;

                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.HeaderText = "İptal";
                btn.Text = "İptal";
                btn.Name = "btn";                
                btn.UseColumnTextForButtonValue = true;                
                DataGridViewCellStyle style = new DataGridViewCellStyle();
                style.BackColor = Color.Red;
                style.ForeColor = Color.White;
                style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                
                btn.DefaultCellStyle = style;                
                dataGridView1.Columns.Add(btn);

                foreach (var item in MasadakiSiparisler)
                {
                    string siparis = item.Siparis;
                    int siparisAdedi = item.SiparisAdedi;
                    bool control = false;
                    int sayac = dataGridView1.RowCount;

                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        if (dataGridView1.Rows[i].Cells["Siparis"].Value.ToString() == siparis)
                        {
                            control = true;
                            int a = Convert.ToInt32(dataGridView1.Rows[i].Cells["SiparisAdedi"].Value);
                            dataGridView1.Rows[i].Cells["SiparisAdedi"].Value = a.ToString();
                        }
                    }
                    if (control == false)
                    {
                        dataGridView1.Rows.Add(siparis);
                        dataGridView1.Rows[sayac].Cells["SiparisAdedi"].Value = item.SiparisAdedi;
                    }
                }
            }
        }        

        private void Urun_Click(object sender, EventArgs e)
        {
            DialogResult result = MasaAcikmi();
            Button secilenUrun = (Button)sender;            

            Adisyon siparis = new Adisyon();
            siparis.Siparis = secilenUrun.Text;
            siparis.SiparisAdedi = Convert.ToInt32(lbl_siparis_adedi.Text);

            if (result == DialogResult.Yes)
            {
                using (AdisyonRepository adisyonRepository = new AdisyonRepository())
                {
                    adisyonRepository.MasaDurumunuGuncelle(MasaId, "Açık", DateTime.Now, adisyonRepository.SıradakiAdisyonNo());
                    adisyonRepository.AdisyonNoGuncelle(1);
                    AdisyonNumarasi = adisyonRepository.MasaAdisyonNo(MasaId);

                    button2.Text = "Masayı Kapat";
                    button2.BackColor = Color.DarkRed;
                    SiparisEkle(secilenUrun.Text);

                    if (YeniSiparisler.Any(x => (x.Siparis == secilenUrun.Text)))
                    {
                        var indexs = YeniSiparisler.FindIndex(x => (x.Siparis == secilenUrun.Text));
                        YeniSiparisler[indexs].SiparisAdedi += 1;
                    }
                    else
                    {
                        YeniSiparisler.Add(siparis);
                    }                    
                }
            }
            else if(result == DialogResult.OK)
            {
                SiparisEkle(secilenUrun.Text);
                if (YeniSiparisler.Any(x => (x.Siparis == secilenUrun.Text)))
                {
                    var indexs = YeniSiparisler.FindIndex(x => (x.Siparis == secilenUrun.Text));
                    YeniSiparisler[indexs].SiparisAdedi += 1;
                }
                else
                {
                    YeniSiparisler.Add(siparis);
                }
            }
            else
            {
                
            }
            lbl_siparis_adedi.Text = (1).ToString();
            if (secilenUrun.Text == "Mantı + Soft İçecek")
            {
                SoftDrink softDrink = new SoftDrink();
                softDrink.ShowDialog();
                DataDoldur();
            }
            else
            {
                softIcecek = string.Empty;
            }
        }

        private DialogResult MasaAcikmi()
        {
            if (button2.Text == "Masayı Aç")
            {
                MsgBox.message = "Masayı açmadan sipariş giremezsiniz. Masa açılsın mı ?";
                MsgBox.baslik = "Masa Aç";
                MsgBox.BoxButtons = MessageBoxButtons.YesNo;
                MsgBox msgBox = new MsgBox();
                msgBox.ShowDialog();
            }
            else
            {
                MsgBox.result = DialogResult.OK;
            }

            return MsgBox.result;            
        }

        private void SiparisEkle(string secilenUrun)
        {
            int sayac = dataGridView1.Rows.Count;
            bool control = false;          

            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                Adisyon adisyon = new Adisyon();
                adisyon.AdisyonID = MasaId;
                //adisyon.AdisyonNo = GarsonEkrani.AdisyonNo;
                adisyon.AdisyonNo = adisyonRepository.MasaAdisyonNo(MasaId);
                adisyon.MasaAdi = MasaAdi;
                adisyon.Siparis = secilenUrun;
                adisyon.SiparisAdedi = Convert.ToInt32(lbl_siparis_adedi.Text);
                adisyon.SiparisFiyati = adisyonRepository.UrunFiyatiGetir(secilenUrun);
                adisyon.SiparisTarihi = DateTime.Now;
                adisyon.AdisyonNotu = txt_siparis_notu.Text;

                int returnValue = adisyonRepository.SiparisEkle(adisyon);

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells["Siparis"].Value.ToString() == secilenUrun)
                    {
                        control = true;
                        int a = Convert.ToInt32(dataGridView1.Rows[i].Cells["SiparisAdedi"].Value);
                        dataGridView1.Rows[i].Cells["SiparisAdedi"].Value = (a + Convert.ToInt32(lbl_siparis_adedi.Text)).ToString();
                    }
                }
                if (control == false)
                {
                    DataDoldur();
                }
            }
        }

        private void TabPageOlustur()
        {
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                Kategoriler = adisyonRepository.MenuKategoriGetir();

                foreach (var item in Kategoriler)
                {
                    if (item.KategoriAdi != "TakeAway")
                    {
                        TabPage tabPage = new TabPage();
                        tabPage.Text = item.KategoriAdi;

                        FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
                        flowLayoutPanel.Dock = DockStyle.Fill;
                        flowLayoutPanel.BackColor = Color.White;
                        flowLayoutPanel.AutoScroll = true;

                        TumUrunler = adisyonRepository.MenudekiTumUrunleriGetir();



                        for (int i = 0; i < TumUrunler.Count; i++)
                        {
                            if (item.KategoriAdi == TumUrunler[i].UrunAciklama)
                            {
                                Button urun = new Button();
                                urun.Text = TumUrunler[i].UrunAdi;
                                urun.BackColor = Color.FromArgb(39, 41, 40);
                                urun.ForeColor = Color.LightGray;
                                urun.Font = new Font("Georgia", 15, FontStyle.Bold);
                                urun.FlatStyle = FlatStyle.Flat;
                                urun.FlatAppearance.BorderSize = 0;
                                urun.Height = 90;
                                urun.Width = 150;
                                urun.Click += Urun_Click;
                                urun.TabIndex = i;
                                flowLayoutPanel.Controls.Add(urun);

                            }
                            else if (item.KategoriAdi == TumUrunler[i].UrunKategori)
                            {
                                Button urun = new Button();
                                urun.Text = TumUrunler[i].UrunAdi;
                                urun.BackColor = Color.FromArgb(39, 41, 40);
                                urun.ForeColor = Color.LightGray;
                                //if (flowLayoutPanel.Controls.Count % 2 == 0)
                                //{
                                //    //urun.BackColor = Color.FromArgb(240, 169, 63);
                                //    //urun.ForeColor = Color.White;   
                                //    urun.BackColor = Color.FromArgb(39,41,40);
                                //    urun.ForeColor = Color.White;
                                //}
                                //else
                                //{
                                //    urun.BackColor = Color.AliceBlue;
                                //    urun.ForeColor = Color.Black;                                
                                //}
                                urun.Font = new Font("Georgia", 15, FontStyle.Bold);
                                urun.FlatStyle = FlatStyle.Flat;
                                urun.FlatAppearance.BorderSize = 0;
                                urun.Height = 90;
                                urun.Width = 150;
                                urun.Click += Urun_Click;
                                urun.TabIndex = i;

                                flowLayoutPanel.Controls.Add(urun);
                            }
                        }
                        tabPage.Controls.Add(flowLayoutPanel);
                        tabControl1.Controls.Add(tabPage);
                    }                   
                }
            }
        }

        //private void TumUrunlerGetir()
        //{   
        //    using (AdisyonRepository adisyonRepository = new AdisyonRepository())
        //    {
        //        TumUrunler = adisyonRepository.MenudekiTumUrunleriGetir();                

        //        for (int i = 0; i < TumUrunler.Count; i++)
        //        {
        //            Button urun = new Button();
        //            urun.Text = TumUrunler[i].UrunAdi;
        //            if (i % 2 == 0)
        //            {
        //                urun.BackColor = Color.FromArgb(240, 169, 63);
        //                urun.ForeColor = Color.White;
        //                urun.Font = new Font("Georgia", 20, FontStyle.Bold);
        //                urun.FlatStyle = FlatStyle.Flat;
        //                urun.FlatAppearance.BorderSize = 0;
        //            }
        //            else
        //            {
        //                urun.BackColor = Color.AliceBlue;
        //                urun.Font = new Font("Georgia", 20, FontStyle.Bold);
        //                urun.ForeColor = Color.Black;
        //                urun.FlatStyle = FlatStyle.Flat;
        //                urun.FlatAppearance.BorderSize = 0;
        //            }
        //            urun.Height = 100;
        //            urun.Width = 200;
        //            urun.Click += Urun_Click;
        //            urun.TabIndex = i;


        //            #region eskiiii
        //            //if(TumUrunler[i].UrunKategori == "Kahveler")
        //            //{
        //            //    this.flowLayoutPanel1.Controls.Add(urun);
        //            //}
        //            //else if(TumUrunler[i].UrunKategori == "Sıcak İçecekler")
        //            //{
        //            //    this.flowLayoutPanel2.Controls.Add(urun);
        //            //}
        //            //else if (TumUrunler[i].UrunKategori == "Soğuk İçecekler")
        //            //{
        //            //    this.flowLayoutPanel3.Controls.Add(urun);
        //            //}
        //            //else if (TumUrunler[i].UrunKategori == "Yemekler")
        //            //{
        //            //    this.flowLayoutPanel4.Controls.Add(urun);
        //            //}
        //            //else if (TumUrunler[i].UrunKategori == "Tatlılar")
        //            //{
        //            //    this.flowLayoutPanel5.Controls.Add(urun);
        //            //}
        //            //else if (TumUrunler[i].UrunKategori == "Kampanyalar")
        //            //{
        //            //    this.flowLayoutPanel6.Controls.Add(urun);
        //            //}
        //            #endregion
        //        }

        //    }
        //}

        private void button2_Click(object sender, EventArgs e)
        {          
            Button MasaAc = (Button)sender;
            saat = DateTime.Now;
            
            if (MasaAc.Text == "Masayı Aç")
            {
                using(AdisyonRepository adisyonRepository = new AdisyonRepository())
                {
                    adisyonRepository.MasaDurumunuGuncelle(MasaId, "Açık", DateTime.Now, adisyonRepository.SıradakiAdisyonNo());
                    adisyonRepository.AdisyonNoGuncelle(1);
                    AdisyonNumarasi = adisyonRepository.MasaAdisyonNo(MasaId);
                }

                MasaAc.Text = "Masayı Kapat";
                MasaAc.BackColor = Color.DarkRed;
            }
            else if(MasaAc.Text == "Masayı Kapat")
            {
                
                MsgBox.message = $"{MasaAdi} kapatılacak. Onaylıyor musunuz?";
                MsgBox.baslik = "Masa Kapatma";
                MsgBox.BoxButtons = MessageBoxButtons.YesNo;
                MsgBox msgBox = new MsgBox();
                msgBox.ShowDialog();

                if(MsgBox.result == DialogResult.Yes)
                {
                    using (AdisyonRepository adisyonRepository = new AdisyonRepository())
                    {
                        foreach (DataGridViewRow item in dataGridView1.Rows)
                        {
                            adisyonRepository.SiparisSil(AdisyonNumarasi, item.Cells["Siparis"].Value.ToString());
                        }
                        DataDoldur();

                        adisyonRepository.MasaDurumunuGuncelle(MasaId, "Kapalı", DateTime.Now, 0);
                    }
                    MasaAc.Text = "Masayı Aç";
                    MasaAc.BackColor = Color.Green;
                }                
            }  
        }

        private void MasaKapandimi()
        {
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                string durum = adisyonRepository.MasaDurumunuGetir(MasaId);

                if (durum == "Kapalı")
                {
                    button2.Text = "Masayı Aç";
                    button2.BackColor = Color.Green;
                }
                else
                {
                    button2.Text = "Masayı Kapat";
                    button2.BackColor = Color.DarkRed;
                }
            }            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OdemeEkrani odemeEkrani = new OdemeEkrani();
            OdemeEkrani.masaID = MasaId;
            OdemeEkrani.masaAdi = MasaAdi;
            OdemeEkrani.yonlendirilenEkran = "MasaDetay";
            OdemeEkrani.adisyonNo = AdisyonNumarasi;
            //KasaEkrani KasaEkrani = (KasaEkrani)Application.OpenForms["KasaEkrani"];
            //KasaEkrani.Visible = false;
            //odemeEkrani.ShowDialog();
            odemeEkrani.ShowDialog();
            DataDoldur();
            MasaKapandimi();
        }

        #region Sayı Butonları
        private void button4_Click(object sender, EventArgs e)
        {
            lbl_siparis_adedi.Text = ((Button)sender).Text;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            lbl_siparis_adedi.Text = ((Button)sender).Text;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            lbl_siparis_adedi.Text = ((Button)sender).Text;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            lbl_siparis_adedi.Text = ((Button)sender).Text;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            lbl_siparis_adedi.Text = ((Button)sender).Text;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            lbl_siparis_adedi.Text = ((Button)sender).Text;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            lbl_siparis_adedi.Text = ((Button)sender).Text;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            lbl_siparis_adedi.Text = ((Button)sender).Text;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            lbl_siparis_adedi.Text = ((Button)sender).Text;
        }

        private void button13_Click(object sender, EventArgs e)
        {
        }

        private void button14_Click(object sender, EventArgs e)
        {
            lbl_siparis_adedi.Text = string.Empty;
        }
        #endregion

        Font Baslik = new Font("Verdana", 15, FontStyle.Bold);
        Font altBaslik = new Font("Verdana", 12, FontStyle.Regular);
        Font icerik = new Font("Verdana", 10);
        SolidBrush sb = new SolidBrush(Color.Black);

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            StringFormat st = new StringFormat();
            st.Alignment = StringAlignment.Near;
            e.Graphics.DrawString("Deneme Cafe",Baslik,sb,220,100,st);

            e.Graphics.DrawString("Ürün Adı                       Adet            Not", altBaslik, sb, 150, 180, st);
            e.Graphics.DrawString("-------------------------------------------------------", altBaslik, sb, 150, 190, st);

            for (int i = 0; i < YeniSiparisler.Count; i++)
            {
                e.Graphics.DrawString(YeniSiparisler[i].Siparis, icerik, sb, 160, 210 + i * 30,st);
                e.Graphics.DrawString(YeniSiparisler[i].SiparisAdedi.ToString(), icerik, sb, 360, 210 + i * 30, st);
                e.Graphics.DrawString(YeniSiparisler[i].AdisyonNotu, icerik, sb, 420, 210 + i * 30, st);
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            //printPreviewDialog1.ShowDialog();

            List<StoktanDusulecekUrunler> stoktanDusulecekUrunler = new List<StoktanDusulecekUrunler>();

            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                //foreach (DataGridViewRow item in dataGridView1.Rows)
                //{
                //    int id = adisyonRepository.MenuUrunIDGetir(item.Cells["Siparis"].Value.ToString());
                //    stoktanDusulecekUrunler = adisyonRepository.StoktanDusulecekUrunleriGetir(id);

                //    foreach (var urun in stoktanDusulecekUrunler)
                //    {
                //        adisyonRepository.StoktanEksiltme(urun.UrunAdi, urun.UrunAdedi);
                //    }
                //}

                foreach (var item in YeniSiparisler)
                {
                    int id = adisyonRepository.MenuUrunIDGetir(item.Siparis);
                    stoktanDusulecekUrunler = adisyonRepository.StoktanDusulecekUrunleriGetir(id);

                    foreach (var urunler in stoktanDusulecekUrunler)
                    {
                        adisyonRepository.StoktanEksiltme(urunler.UrunAdi, urunler.UrunAdedi);
                    }
                }
                MsgBox msgBox = new MsgBox();
                MsgBox.baslik = "Bilgi";
                MsgBox.BoxButtons = MessageBoxButtons.OK;
                MsgBox.message = "Sipariş Gönderildi.";
                msgBox.ShowDialog();
            }
            YeniSiparisler.Clear();
        }

        /// <summary>
        /// DataGridView Sipariş İptal etme
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {           
            secilenSiparis = dataGridView1.SelectedRows[0].Cells["Siparis"].Value.ToString();
            if (e.ColumnIndex == 0)
            {
                foreach (DataGridViewRow drow in dataGridView1.SelectedRows)
                {
                    List<StoktanDusulecekUrunler> stoktanDusulecekUrunler = new List<StoktanDusulecekUrunler>();
                    string siparis = drow.Cells["Siparis"].Value.ToString();
                    decimal adet = Convert.ToDecimal(drow.Cells["SiparisAdedi"].Value);

                    using (AdisyonRepository adisyonRepository = new AdisyonRepository())
                    {
                        AdisyonNumarasi = adisyonRepository.MasaAdisyonNo(MasaId);

                        MsgBox.message = $"Siparişler ({siparis}) stoktan eksilsin mi ?";
                        MsgBox.baslik = "Sipariş İptal";
                        MsgBox.BoxButtons = MessageBoxButtons.YesNo;
                        MsgBox msgBox = new MsgBox();
                        msgBox.ShowDialog();

                        if (MsgBox.result == DialogResult.No)
                        {
                            //adisyonRepository.SiparisSil(AdisyonNumarasi, siparis);
                            adisyonRepository.SiparisAdetSil(MasaId, AdisyonNumarasi, siparis, 1, adisyonRepository.UrunFiyatiGetir(siparis));
                            int id = adisyonRepository.MenuUrunIDGetir(siparis);
                            stoktanDusulecekUrunler = adisyonRepository.StoktanDusulecekUrunleriGetir(id);                            

                            for (int i = 0; i < adet; i++)
                            {
                                foreach (var item in stoktanDusulecekUrunler)
                                {
                                    adisyonRepository.StokaGeriEkle(item.UrunAdi,item.UrunAdedi);
                                }
                            }

                            if (softIcecekler.Count > 0)
                            {
                                softIcecek = softIcecekler.Last();
                                int softId = adisyonRepository.MenuUrunIDGetir(softIcecek);
                                stoktanDusulecekUrunler = adisyonRepository.StoktanDusulecekUrunleriGetir(softId);

                                foreach (var item in stoktanDusulecekUrunler)
                                {
                                    int value = adisyonRepository.StokaGeriEkle(item.UrunAdi, 1);

                                    if (value > 0)
                                    {
                                        softIcecekler.RemoveAt(softIcecekler.Count - 1);
                                    }
                                    else
                                    {
                                        MsgBox.baslik = "Hata";
                                        MsgBox.BoxButtons = MessageBoxButtons.OK;
                                        MsgBox.message = "İşlem başarısız..!";
                                        msgBox.ShowDialog();
                                    }
                                }
                            }
                            DataDoldur();
                        }
                        else if(MsgBox.result == DialogResult.None)
                        {
                            //adisyonRepository.SiparisSil(AdisyonNumarasi, siparis);
                            adisyonRepository.SiparisAdetSil(MasaId, AdisyonNumarasi, siparis, 1, adisyonRepository.UrunFiyatiGetir(siparis));
                            int id = adisyonRepository.MenuUrunIDGetir(siparis);
                            stoktanDusulecekUrunler = adisyonRepository.StoktanDusulecekUrunleriGetir(id);

                            for (int i = 0; i < adet; i++)
                            {
                                foreach (var item in stoktanDusulecekUrunler)
                                {
                                    adisyonRepository.StokaGeriEkle(item.UrunAdi, item.UrunAdedi);
                                }
                            }
                            if (!string.IsNullOrEmpty(softIcecek))
                            {
                                adisyonRepository.StokaGeriEkle(softIcecek, 1);
                            }
                            DataDoldur();
                            MsgBox.result = DialogResult.No;
                        }
                        else 
                        {
                            //adisyonRepository.SiparisSil(AdisyonNumarasi, siparis);
                            adisyonRepository.SiparisAdetSil(MasaId, AdisyonNumarasi, siparis, 1, adisyonRepository.UrunFiyatiGetir(siparis));
                            DataDoldur();
                        }
                        adisyonRepository.SiparisİptalEkle(siparis, Convert.ToInt32(adet), adisyonRepository.UrunFiyatiGetir(siparis), MsgBox.result.ToString(), DateTime.Now,adisyonRepository.UrunMaliyetFiyatiGetir(siparis));
                    }
                }
            }
            else
            {
                lbl_siparis_notu_siparis.Text = secilenSiparis;
                foreach (var item in MasadakiSiparisler)
                {
                    if (item.Siparis == secilenSiparis)
                    {
                        lbl_siparis_saati.Text = item.SiparisTarihi.ToShortTimeString();
                    }
                }
            }
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            //Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.System) + Path.DirectorySeparatorChar + "osk.exe");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MsgBox msgBox = new MsgBox();
            using(AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                if (YeniSiparisler.Any(x => x.Siparis == secilenSiparis))
                {
                    var indexs = YeniSiparisler.FindIndex(x => (x.Siparis == secilenSiparis));
                    YeniSiparisler[indexs].AdisyonNotu = txt_siparis_notu.Text;
                }

                int value = adisyonRepository.SiparisNotuEkle(MasaId, AdisyonNumarasi, secilenSiparis, txt_siparis_notu.Text);

                if (value > 0)
                {
                    MsgBox.baslik = "Bilgi";
                    MsgBox.message = "Sipariş Notu eklendi.";
                    MsgBox.BoxButtons = MessageBoxButtons.OK;
                    msgBox.ShowDialog();
                    txt_siparis_notu.Text = string.Empty;
                }
                else
                {
                    MsgBox.baslik = "Uyarı";
                    MsgBox.message = "Sipariş notu ekleme işlemi başarısız..!";
                    MsgBox.BoxButtons = MessageBoxButtons.OK;
                    msgBox.ShowDialog();
                }
                
            }
        }
    }
}
