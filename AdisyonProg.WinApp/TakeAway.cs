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
using Menu = AdisyonProg.Entity.Menu;

namespace AdisyonProg.WinApp
{
    public partial class TakeAway : Form
    {
        List<Takeaway> YeniSiparisler;
        List<Entity.Menu> TumUrunler;
        public static int AdisyonNumarasi;
        List<Takeaway> Siparisler;
        List<MenuKategori> Kategoriler;

        public TakeAway()
        {
            InitializeComponent();
            TumUrunler = new List<Entity.Menu>();
            YeniSiparisler = new List<Takeaway>();
            Siparisler = new List<Takeaway>();
            Kategoriler = new List<MenuKategori>();
            TabPageOlustur();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                OdemeEkrani odemeEkrani = new OdemeEkrani();
                OdemeEkrani.masaID = adisyonRepository.MasaIDGetir("TakeAway");
                OdemeEkrani.masaAdi = "TakeAway";
                OdemeEkrani.adisyonNo = AdisyonNumarasi;
                OdemeEkrani.yonlendirilenEkran = "TakeAway";
                odemeEkrani.ShowDialog();
            }
                DataDoldur();
        }

        #region Yazdırma İşlemleri

        //Font Baslik = new Font("Verdana", 15, FontStyle.Bold);
        //Font altBaslik = new Font("Verdana", 12, FontStyle.Regular);
        //Font icerik = new Font("Verdana", 10);
        //SolidBrush sb = new SolidBrush(Color.Black);

        //private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        //{
        //    StringFormat st = new StringFormat();
        //    st.Alignment = StringAlignment.Near;
        //    e.Graphics.DrawString("Deneme Cafe", Baslik, sb, 320, 100, st);

        //    e.Graphics.DrawString("Ürün Adı                       Adet", altBaslik, sb, 250, 180, st);
        //    e.Graphics.DrawString("-------------------------------------------", altBaslik, sb, 250, 190, st);

        //    for (int i = 0; i < YeniSiparisler.Count; i++)
        //    {
        //        e.Graphics.DrawString(YeniSiparisler[i].Siparis, icerik, sb, 260, 210 + i * 30, st);
        //        e.Graphics.DrawString(YeniSiparisler[i].SiparisAdedi.ToString(), icerik, sb, 460, 210 + i * 30, st);
        //    }
        //}

        //private void button16_Click(object sender, EventArgs e)
        //{

        //    printPreviewDialog1.ShowDialog();
        //    List<StoktanDusulecekUrunler> stoktanDusulecekUrunler = new List<StoktanDusulecekUrunler>();

        //    using (AdisyonRepository adisyonRepository = new AdisyonRepository())
        //    {
        //        foreach (var item in YeniSiparisler)
        //        {
        //            SiparisEkle(item.Siparis);///////////////
        //            int id = adisyonRepository.MenuUrunIDGetir(item.Siparis);
        //            stoktanDusulecekUrunler = adisyonRepository.StoktanDusulecekUrunleriGetir(id);

        //            foreach (var urunler in stoktanDusulecekUrunler)
        //            {
        //                adisyonRepository.StoktanEksiltme(urunler.UrunAdi, urunler.UrunAdedi);
        //            }
        //        }
        //        adisyonRepository.MasaDurumunuGuncelle(0, "Açık", DateTime.Now, adisyonRepository.SıradakiAdisyonNo());
        //        adisyonRepository.AdisyonNoGuncelle(1);
        //    }

        //    YeniSiparisler.Clear();
        //}              

        #endregion
        private void TabPageOlustur()
        {
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                Kategoriler = adisyonRepository.MenuKategoriGetir();

                foreach (var item in Kategoriler)
                {
                    if (item.KategoriAdi == "TakeAway")
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
                            if (item.KategoriAdi == TumUrunler[i].UrunKategori)
                            {
                                Button urun = new Button();
                                urun.Text = TumUrunler[i].UrunAdi;
                                urun.BackColor = Color.FromArgb(39, 41, 40);
                                urun.ForeColor = Color.LightGray;
                                //if (flowLayoutPanel.Controls.Count % 2 == 0)
                                //{
                                //    urun.BackColor = Color.FromArgb(240, 169, 63);
                                //    urun.ForeColor = Color.White;
                                //}
                                //else
                                //{
                                //    urun.BackColor = Color.AliceBlue;
                                //    urun.ForeColor = Color.Black;
                                //}
                                urun.Font = new Font("Georgia", 14, FontStyle.Bold);
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

        private void Urun_Click(object sender, EventArgs e)
        {
            Button secilenUrun = (Button)sender;

            Takeaway siparis = new Takeaway();
            siparis.Siparis = secilenUrun.Text;
            siparis.SiparisAdedi = 1;

            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                //adisyonRepository.AdisyonNoGuncelle(1);
                SiparisEkle(secilenUrun.Text);
                YeniSiparisler.Add(siparis);
            }
            DataDoldur();
            lbl_siparis_adedi.Text = (1).ToString();
        }

        private void SiparisEkle(string secilenUrun)
        {
            int sayac = dataGridView1.Rows.Count;
            bool control = false;

            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                Takeaway takeaway = new Takeaway();
                takeaway.AdisyonNo= AdisyonNumarasi;
                takeaway.Siparis = secilenUrun;
                takeaway.SiparisAdedi = Convert.ToInt32(lbl_siparis_adedi.Text);
                takeaway.SiparisFiyati = adisyonRepository.UrunFiyatiGetir(secilenUrun);
                takeaway.SiparisTarihi = DateTime.Now;
                takeaway.OdemeYapildimi = "Hayır";

                int returnValue = adisyonRepository.TakeAwayEkle(takeaway);

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

        private void DataDoldur()
        {
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                Siparisler = adisyonRepository.TakeAwaySiparisleriGetir(AdisyonNumarasi);
                dataGridView1.DataSource = Siparisler;

                dataGridView1.Columns["TakeAwayID"].Visible = false;
                dataGridView1.Columns["AdisyonNo"].Visible = false;
                dataGridView1.Columns["SiparisTarihi"].Visible = false;
                dataGridView1.Columns["SiparisFiyati"].Visible = false;

                dataGridView1.Columns["Siparis"].DisplayIndex = 0;
                dataGridView1.Columns["SiparisAdedi"].DisplayIndex = 1;

                dataGridView1.Columns["SiparisAdedi"].Width = 55;
                dataGridView1.Columns["Siparis"].Width = 240;

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

                foreach (var item in Siparisler)
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

        private void TakeAway_Load(object sender, EventArgs e)
        {
            YeniSiparisler.Clear();
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                //GarsonEkrani.AdisyonNo = adisyonRepository.SıradakiAdisyonNo();
                //adisyonRepository.MasaDurumunuGuncelle(0, "Açık", DateTime.Now, adisyonRepository.SıradakiAdisyonNo());

                adisyonRepository.MasaDurumunuGuncelle(adisyonRepository.MasaIDGetir("TakeAway"), "Açık", DateTime.Now, adisyonRepository.SıradakiAdisyonNo());
                adisyonRepository.AdisyonNoGuncelle(1);
                AdisyonNumarasi = adisyonRepository.MasaAdisyonNo(adisyonRepository.MasaIDGetir("TakeAway"));
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                foreach (DataGridViewRow drow in dataGridView1.SelectedRows)
                {
                    List<StoktanDusulecekUrunler> stoktanDusulecekUrunler = new List<StoktanDusulecekUrunler>();
                    string siparis = drow.Cells["Siparis"].Value.ToString();
                    decimal adet = Convert.ToDecimal(drow.Cells["SiparisAdedi"].Value);

                    using (AdisyonRepository adisyonRepository = new AdisyonRepository())
                    {
                        MsgBox.message = "Siparişler stoktan eksilsin mi ?";
                        MsgBox.baslik = "Sipariş İptal";
                        MsgBox.BoxButtons = MessageBoxButtons.YesNo;
                        MsgBox msgBox = new MsgBox();
                        msgBox.ShowDialog();

                        if (MsgBox.result == DialogResult.Yes)
                        {
                            adisyonRepository.TakeAwaySiparisSil(AdisyonNumarasi, siparis);
                            int id = adisyonRepository.MenuUrunIDGetir(siparis);
                            stoktanDusulecekUrunler = adisyonRepository.StoktanDusulecekUrunleriGetir(id);

                            for (int i = 0; i < adet; i++)
                            {
                                foreach (var item in stoktanDusulecekUrunler)
                                {
                                    adisyonRepository.StokaGeriEkle(item.UrunAdi, item.UrunAdedi);
                                }
                            }
                            DataDoldur();
                        }
                        else if (MsgBox.result == DialogResult.None)
                        {

                        }
                        else
                        {
                            adisyonRepository.TakeAwaySiparisSil(AdisyonNumarasi, siparis);
                            DataDoldur();
                        }
                    }
                }
            }
        }

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

        private void button14_Click(object sender, EventArgs e)
        {
            lbl_siparis_adedi.Text = string.Empty;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            List<StoktanDusulecekUrunler> stoktanDusulecekUrunler = new List<StoktanDusulecekUrunler>();

            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
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
    }
}
