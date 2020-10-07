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
    public partial class Raporlar : Form
    {
        List<GunBasiSonu> tarihler;
        DateTime gunBasi;
        DateTime gunSonu;
        int adisyonSayisi;
        int satilanSiparisAdedi;
        decimal toplamSatis;
        decimal gelir;
        decimal gider;
        decimal nakit;
        decimal krediKarti;
        decimal yemekCeki;
        decimal ikram;
        decimal kdv;
        List<Tuple<string, decimal, int, decimal>> urunSatis;
        string whichReport = "";

        public Raporlar()
        {
            InitializeComponent();
            tarihler = new List<GunBasiSonu>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            whichReport = "Gün Sonu Raporu";
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                tarihler = adisyonRepository.GunBasiSonuAl(adisyonRepository.GunBasiTarih());

                if (tarihler.Count > 0 && tarihler != null)
                {
                    foreach (var item in tarihler)
                    {
                        gunBasi = item.GunBasi;
                        gunSonu = item.GunSonu;
                    }

                    adisyonSayisi = adisyonRepository.AdisyonSayisi(gunBasi, gunSonu);
                    satilanSiparisAdedi = adisyonRepository.SatilanSiparisAdedi(gunBasi, gunSonu);
                    toplamSatis = adisyonRepository.ToplamSatisTutari(gunBasi, gunSonu);
                    List<Odeme> odenenSiparisler = new List<Odeme>();
                    odenenSiparisler = adisyonRepository.OdemeHepsiGetir(gunBasi, gunSonu);

                    foreach (var item in odenenSiparisler)
                    {
                        gider += adisyonRepository.GiderHesaplama(item.OdenenSiparis, item.SiparisAdedi);
                    }
                    gelir = toplamSatis - gider;
                    nakit = adisyonRepository.NakitOdemeMiktari(gunBasi, gunSonu);
                    krediKarti = adisyonRepository.KrediKartiOdemeMiktari(gunBasi, gunSonu);
                    yemekCeki = adisyonRepository.YemekCekiOdemeMiktari(gunBasi, gunSonu);
                    ikram = adisyonRepository.IkramOdemeMiktari(gunBasi, gunSonu);
                    kdv = Convert.ToDecimal((toplamSatis * 18) / 100);

                    adisyonRepository.GunIslemleriEkle(gunBasi, gunSonu, adisyonSayisi, satilanSiparisAdedi, toplamSatis, gelir, gider, nakit, krediKarti, yemekCeki, ikram);

                    GunSonuRaporuDuzenle(toplamSatis, gelir, gider, nakit, krediKarti, yemekCeki, ikram, adisyonSayisi, satilanSiparisAdedi);
                }
                else
                {
                    MsgBox msgBox = new MsgBox();
                    MsgBox.baslik = "Uyarı";
                    MsgBox.message = "Raporu görüntülemek için 'Gün Sonu Yap'mayı unutmayın..!";
                    MsgBox.BoxButtons = MessageBoxButtons.OK;
                    msgBox.ShowDialog();
                }
            }
        }

        private void GunSonuRaporuDuzenle(decimal toplamsatis_tutars, decimal gelir_tutars, decimal gider_tutars, decimal nakit_tutars, decimal kredi_tutars, decimal yemek_tutars, decimal ikram_tutars, int adisyonSayisi, int siparisAdedi)
        {
            Label lbl_main = new Label();
            lbl_main.Text = whichReport;
            lbl_main.ForeColor = Color.White;
            lbl_main.Font = new Font("Century Gothic", 12, FontStyle.Bold);
            lbl_main.Width = 200;
            lbl_main.Location = new Point(315, 100);

            Label lbl_gnbs = new Label();
            lbl_gnbs.ForeColor = Color.White;
            lbl_gnbs.Font = new Font("Century Gothic", 12, FontStyle.Bold);
            lbl_gnbs.Width = 400;
            lbl_gnbs.Location = new Point(300, 120);

            if (whichReport == "Genel Satış Raporu")
            {
                lbl_gnbs.Text = dateTimePicker1.Value.ToShortDateString() + " - " + dateTimePicker2.Value.ToShortDateString();
            }
            else
            {
                lbl_gnbs.Text = gunBasi.ToString() + " - " + gunSonu.ToString();
            }

            GroupBox groupBox = new GroupBox();
            groupBox.Height = 660;
            groupBox.Width = 500;
            groupBox.Location = new Point(150, 150);

            Label lbl_baslik = new Label();
            lbl_baslik.Text = "Genel Bilgiler";
            lbl_baslik.ForeColor = Color.White;
            lbl_baslik.BackColor = Color.DimGray;
            lbl_baslik.Font = new Font("Century Gothic", 16, FontStyle.Bold);
            lbl_baslik.TextAlign = ContentAlignment.MiddleCenter;
            lbl_baslik.MinimumSize = new Size(500, 50);

            Panel panel1_2 = new Panel();
            panel1_2.BackColor = Color.White;
            panel1_2.Height = 55;
            panel1_2.Width = 500;
            panel1_2.Location = new Point(0, 50);

            Panel panel2_2 = new Panel();
            panel2_2.BackColor = Color.White;
            panel2_2.Height = 55;
            panel2_2.Width = 500;
            panel2_2.Location = new Point(0, 106);

            Panel panel3 = new Panel();
            panel3.BackColor = Color.White;
            panel3.Height = 55;
            panel3.Width = 500;
            panel3.Location = new Point(0, 162);

            Panel panel4 = new Panel();
            panel4.BackColor = Color.White;
            panel4.Height = 55;
            panel4.Width = 500;
            panel4.Location = new Point(0, 218);

            Panel panel5 = new Panel();
            panel5.BackColor = Color.White;
            panel5.Height = 55;
            panel5.Width = 500;
            panel5.Location = new Point(0, 274);

            Panel panel6 = new Panel();
            panel6.BackColor = Color.White;
            panel6.Height = 55;
            panel6.Width = 500;
            panel6.Location = new Point(0, 330);

            Panel panel7 = new Panel();
            panel7.BackColor = Color.White;
            panel7.Height = 55;
            panel7.Width = 500;
            panel7.Location = new Point(0, 386);

            Panel panel8 = new Panel();
            panel8.BackColor = Color.White;
            panel8.Height = 55;
            panel8.Width = 500;
            panel8.Location = new Point(0, 442);

            Panel panel9 = new Panel();
            panel9.BackColor = Color.White;
            panel9.Height = 55;
            panel9.Width = 500;
            panel9.Location = new Point(0, 498);

            Panel panel10 = new Panel();
            panel10.BackColor = Color.White;
            panel10.Height = 55;
            panel10.Width = 500;
            panel10.Location = new Point(0, 554);

            Panel panel11 = new Panel();
            panel11.BackColor = Color.White;
            panel11.Height = 55;
            panel11.Width = 500;
            panel11.Location = new Point(0, 610);


            Label lbl_satis = new Label();
            lbl_satis.Text = "Toplam Satış Tutarı:";
            lbl_satis.ForeColor = Color.DimGray;
            lbl_satis.Font = new Font("Century Gothic", 12, FontStyle.Bold);
            lbl_satis.Width = 300;
            lbl_satis.Location = new Point(6, 14);
            panel1_2.Controls.Add(lbl_satis);

            Label satis = new Label();
            satis.ForeColor = Color.Black;
            satis.Text = toplamsatis_tutars.ToString();
            satis.Font = new Font("Century Gothic", 13, FontStyle.Bold);
            satis.Location = new Point(425, 14);
            satis.Width = 150;
            panel1_2.Controls.Add(satis);


            Label lbl_gelir = new Label();
            lbl_gelir.Text = "Gelir:";
            lbl_gelir.ForeColor = Color.DimGray;
            lbl_gelir.Font = new Font("Century Gothic", 12, FontStyle.Bold);
            lbl_gelir.Width = 300;
            lbl_gelir.Location = new Point(6, 14);
            panel2_2.Controls.Add(lbl_gelir);

            Label gelir_tutar = new Label();
            gelir_tutar.ForeColor = Color.Black;
            gelir_tutar.Text = gelir_tutars.ToString();
            gelir_tutar.Font = new Font("Century Gothic", 13, FontStyle.Bold);
            gelir_tutar.Location = new Point(425, 14);
            gelir_tutar.Width = 150;
            panel2_2.Controls.Add(gelir_tutar);

            Label lbl_gider = new Label();
            lbl_gider.Text = "Gider:";
            lbl_gider.ForeColor = Color.DimGray;
            lbl_gider.Font = new Font("Century Gothic", 12, FontStyle.Bold);
            lbl_gider.Width = 300;
            lbl_gider.Location = new Point(6, 14);
            panel3.Controls.Add(lbl_gider);

            Label gider_tutar = new Label();
            gider_tutar.ForeColor = Color.Black;
            gider_tutar.Text = gider_tutars.ToString();
            gider_tutar.Font = new Font("Century Gothic", 13, FontStyle.Bold);
            gider_tutar.Location = new Point(425, 14);
            gider_tutar.Width = 150;
            panel3.Controls.Add(gider_tutar);

            Label lbl_nakit = new Label();
            lbl_nakit.Text = "Nakit:";
            lbl_nakit.ForeColor = Color.DimGray;
            lbl_nakit.Font = new Font("Century Gothic", 12, FontStyle.Bold);
            lbl_nakit.Width = 300;
            lbl_nakit.Location = new Point(6, 14);
            panel4.Controls.Add(lbl_nakit);

            Label nakit_tutar = new Label();
            nakit_tutar.ForeColor = Color.Black;
            nakit_tutar.Text = nakit_tutars.ToString();
            nakit_tutar.Font = new Font("Century Gothic", 13, FontStyle.Bold);
            nakit_tutar.Location = new Point(425, 14);
            nakit_tutar.Width = 150;
            panel4.Controls.Add(nakit_tutar);

            Label lbl_kredi = new Label();
            lbl_kredi.Text = "Kredi Kartı:";
            lbl_kredi.ForeColor = Color.DimGray;
            lbl_kredi.Font = new Font("Century Gothic", 12, FontStyle.Bold);
            lbl_kredi.Width = 300;
            lbl_kredi.Location = new Point(6, 14);
            panel5.Controls.Add(lbl_kredi);

            Label kredi_tutar = new Label();
            kredi_tutar.ForeColor = Color.Black;
            kredi_tutar.Text = kredi_tutars.ToString();
            kredi_tutar.Font = new Font("Century Gothic", 13, FontStyle.Bold);
            kredi_tutar.Location = new Point(425, 14);
            kredi_tutar.Width = 150;
            panel5.Controls.Add(kredi_tutar);


            Label lbl_yemek = new Label();
            lbl_yemek.Text = "Yemek Çeki:";
            lbl_yemek.ForeColor = Color.DimGray;
            lbl_yemek.Font = new Font("Century Gothic", 12, FontStyle.Bold);
            lbl_yemek.Width = 300;
            lbl_yemek.Location = new Point(6, 14);
            panel6.Controls.Add(lbl_yemek);

            Label yemek_tutar = new Label();
            yemek_tutar.Text = yemek_tutars.ToString();
            yemek_tutar.ForeColor = Color.Black;
            yemek_tutar.Font = new Font("Century Gothic", 13, FontStyle.Bold);
            yemek_tutar.Location = new Point(425, 14);
            yemek_tutar.Width = 150;
            panel6.Controls.Add(yemek_tutar);


            Label lbl_ikram = new Label();
            lbl_ikram.Text = "İkram:";
            lbl_ikram.ForeColor = Color.DimGray;
            lbl_ikram.Font = new Font("Century Gothic", 12, FontStyle.Bold);
            lbl_ikram.Width = 300;
            lbl_ikram.Location = new Point(6, 14);
            panel7.Controls.Add(lbl_ikram);

            Label ikram_tutar = new Label();
            ikram_tutar.ForeColor = Color.Black;
            ikram_tutar.Text = ikram_tutars.ToString();
            ikram_tutar.Font = new Font("Century Gothic", 13, FontStyle.Bold);
            ikram_tutar.Location = new Point(425, 14);
            ikram_tutar.Width = 150;
            panel7.Controls.Add(ikram_tutar);


            Label lbl_adisyon_sayisi = new Label();
            lbl_adisyon_sayisi.Text = "Adisyon Sayısı:";
            lbl_adisyon_sayisi.ForeColor = Color.DimGray;
            lbl_adisyon_sayisi.Font = new Font("Century Gothic", 12, FontStyle.Bold);
            lbl_adisyon_sayisi.Width = 300;
            lbl_adisyon_sayisi.Location = new Point(6, 14);
            panel8.Controls.Add(lbl_adisyon_sayisi);

            Label adisyon_sayisi = new Label();
            adisyon_sayisi.ForeColor = Color.Black;
            adisyon_sayisi.Text = adisyonSayisi.ToString();
            adisyon_sayisi.Font = new Font("Century Gothic", 13, FontStyle.Bold);
            adisyon_sayisi.Location = new Point(425, 14);
            adisyon_sayisi.Width = 150;
            panel8.Controls.Add(adisyon_sayisi);


            Label lbl_siparis_adedi = new Label();
            lbl_siparis_adedi.Text = "Satılan Sipariş Adedi:";
            lbl_siparis_adedi.ForeColor = Color.DimGray;
            lbl_siparis_adedi.Font = new Font("Century Gothic", 12, FontStyle.Bold);
            lbl_siparis_adedi.Width = 300;
            lbl_siparis_adedi.Location = new Point(6, 14);
            panel9.Controls.Add(lbl_siparis_adedi);


            Label siparis_adedi = new Label();
            siparis_adedi.ForeColor = Color.Black;
            siparis_adedi.Text = siparisAdedi.ToString();
            siparis_adedi.Font = new Font("Century Gothic", 13, FontStyle.Bold);
            siparis_adedi.Location = new Point(425, 14);
            siparis_adedi.Width = 150;
            panel9.Controls.Add(siparis_adedi);

            Label lbl_kdv = new Label();
            lbl_kdv.Text = "Toplam Kdv:";
            lbl_kdv.ForeColor = Color.Black;
            lbl_kdv.Font = new Font("Century Gothic", 12, FontStyle.Bold);
            lbl_kdv.Width = 300;
            lbl_kdv.Location = new Point(6, 14);
            panel10.Controls.Add(lbl_kdv);

            Label kdvs = new Label();
            kdvs.ForeColor = Color.Black;
            kdvs.Text = kdv.ToString();
            kdvs.Font = new Font("Century Gothic", 13, FontStyle.Bold);
            kdvs.Location = new Point(425, 14);
            kdvs.Width = 150;
            panel10.Controls.Add(kdvs);

            Label lbl_kazanc = new Label();
            lbl_kazanc.Text = "Kazanç:";
            lbl_kazanc.ForeColor = Color.Black;
            lbl_kazanc.Font = new Font("Century Gothic", 14, FontStyle.Bold);
            lbl_kazanc.Width = 300;
            lbl_kazanc.Location = new Point(6, 14);
            panel11.Controls.Add(lbl_kazanc);


            Label kazanc = new Label();
            kazanc.ForeColor = Color.DarkGreen;
            kazanc.Text = (gelir - gider).ToString();
            kazanc.Font = new Font("Century Gothic", 14, FontStyle.Bold);
            kazanc.Location = new Point(425, 14);
            kazanc.Width = 150;
            panel11.Controls.Add(kazanc);

            panel2.Controls.Add(lbl_gnbs);
            panel2.Controls.Add(lbl_main);
            panel2.Controls.Add(groupBox);
            groupBox.Controls.Add(lbl_baslik);
            groupBox.Controls.Add(panel1_2);
            groupBox.Controls.Add(panel2_2);
            groupBox.Controls.Add(panel3);
            groupBox.Controls.Add(panel4);
            groupBox.Controls.Add(panel5);
            groupBox.Controls.Add(panel6);
            groupBox.Controls.Add(panel7);
            groupBox.Controls.Add(panel8);
            groupBox.Controls.Add(panel9);
            groupBox.Controls.Add(panel10);
            groupBox.Controls.Add(panel11);
        }

        Font Baslik = new Font("Verdana", 15, FontStyle.Bold);
        Font altBaslik = new Font("Verdana", 12, FontStyle.Regular);
        Font icerik = new Font("Verdana", 10);
        SolidBrush sb = new SolidBrush(Color.Black);
        Pen myPen = new Pen(Color.Black);
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            StringFormat st = new StringFormat();
            st.Alignment = StringAlignment.Near;
            if (whichReport == "Gün Sonu Raporu")
            {
                e.Graphics.DrawString("Gün Sonu Raporu", Baslik, sb, 320, 100, st);
            }
            else
            {
                e.Graphics.DrawString("Genel Satış Raporu", Baslik, sb, 320, 100, st);
            }
            e.Graphics.DrawString($"{dateTimePicker1.Value.ToShortDateString()} - {dateTimePicker2.Value.ToShortDateString()}", altBaslik, sb, 330, 130);

            e.Graphics.DrawLine(myPen, 250, 180, 600, 180);
            e.Graphics.DrawLine(myPen, 250, 180, 250, 550);
            e.Graphics.DrawLine(myPen, 600, 180, 600, 550);
            e.Graphics.DrawLine(myPen, 250, 550, 600, 550);


            e.Graphics.DrawString("-------------------------------------------", altBaslik, sb, 250, 190, st);

            e.Graphics.DrawString("Toplam Satış Tutarı:", icerik, sb, 260, 210 + 0 * 30, st);
            e.Graphics.DrawString(toplamSatis.ToString(), icerik, sb, 540, 210 + 0 * 30, st);

            e.Graphics.DrawString("Gelir:", icerik, sb, 260, 210 + 1 * 30, st);
            e.Graphics.DrawString(gelir.ToString(), icerik, sb, 540, 210 + 1 * 30, st);

            e.Graphics.DrawString("Gider:", icerik, sb, 260, 210 + 2 * 30, st);
            e.Graphics.DrawString(gider.ToString(), icerik, sb, 540, 210 + 2 * 30, st);

            e.Graphics.DrawString("Nakit:", icerik, sb, 260, 210 + 3 * 30, st);
            e.Graphics.DrawString(nakit.ToString(), icerik, sb, 540, 210 + 3 * 30, st);

            e.Graphics.DrawString("Kredi Kartı:", icerik, sb, 260, 210 + 4 * 30, st);
            e.Graphics.DrawString(krediKarti.ToString(), icerik, sb, 540, 210 + 4 * 30, st);

            e.Graphics.DrawString("Yemek Çeki:", icerik, sb, 260, 210 + 5 * 30, st);
            e.Graphics.DrawString(yemekCeki.ToString(), icerik, sb, 540, 210 + 5 * 30, st);

            e.Graphics.DrawString("İkram:", icerik, sb, 260, 210 + 6 * 30, st);
            e.Graphics.DrawString(ikram.ToString(), icerik, sb, 540, 210 + 6 * 30, st);

            e.Graphics.DrawString("Adisyon Sayısı:", icerik, sb, 260, 210 + 7 * 30, st);
            e.Graphics.DrawString(adisyonSayisi.ToString(), icerik, sb, 540, 210 + 7 * 30, st);

            e.Graphics.DrawString("Satılan Sipariş Adedi:", icerik, sb, 260, 210 + 8 * 30, st);
            e.Graphics.DrawString(satilanSiparisAdedi.ToString(), icerik, sb, 540, 210 + 8 * 30, st);

            e.Graphics.DrawString("Toplam Kdv:", icerik, sb, 260, 210 + 9 * 30, st);
            e.Graphics.DrawString(satilanSiparisAdedi.ToString(), icerik, sb, 540, 210 + 9 * 30, st);

            e.Graphics.DrawString("Kazanç::", icerik, sb, 260, 210 + 10 * 30, st);
            e.Graphics.DrawString((gelir - gider).ToString(), icerik, sb, 540, 210 + 10 * 30, st);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (whichReport == "Gün Sonu Raporu")
            {
                printPreviewDialog1.ShowDialog();
            }
            else if (whichReport == "Ürün Satış Raporu")
            {
                printPreviewDialog2.ShowDialog();
            }
            else if (whichReport == "Genel Satış Raporu")
            {
                printPreviewDialog1.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            whichReport = "Ürün Satış Raporu";
            panel2.Controls.Clear();
            Tuple<string, decimal, int, decimal> tuple;
            urunSatis = new List<Tuple<string, decimal, int, decimal>>();

            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                List<Odeme> odenenSiparisler = new List<Odeme>();
                odenenSiparisler = adisyonRepository.OdemeHepsiGetir(dateTimePicker1.Value, dateTimePicker2.Value);

                foreach (Odeme item in odenenSiparisler)
                {
                    tuple = adisyonRepository.TmpTable2(item.OdenenSiparis, item.AdisyonNo, item.ID);
                    urunSatis.Add(tuple);
                }
            }

            var fiyatlar = urunSatis.GroupBy(Urun => Urun.Item1)
                .Select(
                x => new
                {
                    Key = x.Key,
                    Value = x.Sum(s => s.Item4)
                });


            Label lbl_main = new Label();
            lbl_main.Text = "Ürün Satış Raporu";
            lbl_main.ForeColor = Color.White;
            lbl_main.Font = new Font("Century Gothic", 12, FontStyle.Bold);
            lbl_main.Width = 200;
            lbl_main.Location = new Point(300, 100);
            panel2.Controls.Add(lbl_main);

            Label lbl_gnbs = new Label();
            lbl_gnbs.ForeColor = Color.White;
            lbl_gnbs.Text = dateTimePicker1.Value.ToShortDateString() + " - " + dateTimePicker2.Value.ToShortDateString();
            lbl_gnbs.Font = new Font("Century Gothic", 12, FontStyle.Bold);
            lbl_gnbs.Width = 400;
            lbl_gnbs.Location = new Point(280, 120);
            panel2.Controls.Add(lbl_gnbs);

            GroupBox groupBox = new GroupBox();
            groupBox.Width = 500;
            groupBox.Location = new Point(150, 150);
            groupBox.Height = fiyatlar.Count() * 70;
            panel2.Controls.Add(groupBox);

            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
            flowLayoutPanel.Dock = DockStyle.Fill;
            groupBox.Controls.Add(flowLayoutPanel);

            for (int i = 0; i < fiyatlar.ToList().Count; i++)
            {
                Panel panel = new Panel();
                panel.BackColor = Color.White;
                panel.Height = 55;
                panel.Width = 500;

                Label lbl_kategori = new Label();
                lbl_kategori.Text = fiyatlar.ToList()[i].Key;
                lbl_kategori.ForeColor = Color.DimGray;
                lbl_kategori.Font = new Font("Century Gothic", 12, FontStyle.Bold);
                lbl_kategori.Width = 300;
                lbl_kategori.Location = new Point(6, 14);
                panel.Controls.Add(lbl_kategori);

                Label tutar = new Label();
                tutar.ForeColor = Color.Black;
                tutar.Text = fiyatlar.ToList()[i].Value.ToString();
                tutar.Font = new Font("Century Gothic", 13, FontStyle.Bold);
                tutar.Location = new Point(425, 14);
                tutar.Width = 150;
                panel.Controls.Add(tutar);

                flowLayoutPanel.Controls.Add(panel);
            }
        }

        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            var fiyatlar = urunSatis.GroupBy(Urun => Urun.Item1)
                 .Select(
                 x => new
                 {
                     Key = x.Key,
                     Value = x.Sum(s => s.Item4)
                 });

            StringFormat st = new StringFormat();
            st.Alignment = StringAlignment.Near;
            e.Graphics.DrawString("Ürün Satış Raporu", Baslik, sb, 320, 100, st);

            e.Graphics.DrawString($"{dateTimePicker1.Value.ToShortDateString()} - {dateTimePicker2.Value.ToShortDateString()}", altBaslik, sb, 330, 130);

            e.Graphics.DrawLine(myPen, 250, 180, 600, 180);
            e.Graphics.DrawLine(myPen, 250, 180, 250, 550);
            e.Graphics.DrawLine(myPen, 600, 180, 600, 550);
            e.Graphics.DrawLine(myPen, 250, 550, 600, 550);


            e.Graphics.DrawString("----------------------------------------------", altBaslik, sb, 250, 190, st);

            for (int i = 0; i < fiyatlar.ToList().Count; i++)
            {
                e.Graphics.DrawString(fiyatlar.ToList()[i].Key, icerik, sb, 260, 210 + i * 30, st);
                e.Graphics.DrawString(fiyatlar.ToList()[i].Value.ToString(), icerik, sb, 540, 210 + i * 30, st);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            whichReport = "Genel Satış Raporu";
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                adisyonSayisi = adisyonRepository.AdisyonSayisi(dateTimePicker1.Value, dateTimePicker2.Value);
                satilanSiparisAdedi = adisyonRepository.SatilanSiparisAdedi(dateTimePicker1.Value, dateTimePicker2.Value);
                toplamSatis = adisyonRepository.ToplamSatisTutari(dateTimePicker1.Value, dateTimePicker2.Value);
                List<Odeme> odenenSiparisler = new List<Odeme>();
                odenenSiparisler = adisyonRepository.OdemeHepsiGetir(dateTimePicker1.Value, dateTimePicker2.Value);

                foreach (var item in odenenSiparisler)
                {
                    gider += adisyonRepository.GiderHesaplama(item.OdenenSiparis, item.SiparisAdedi);
                }
                gelir = toplamSatis - gider;
                nakit = adisyonRepository.NakitOdemeMiktari(dateTimePicker1.Value.Date, dateTimePicker2.Value);
                krediKarti = adisyonRepository.KrediKartiOdemeMiktari(dateTimePicker1.Value.Date, dateTimePicker2.Value);
                yemekCeki = adisyonRepository.YemekCekiOdemeMiktari(dateTimePicker1.Value.Date, dateTimePicker2.Value);
                ikram = adisyonRepository.IkramOdemeMiktari(dateTimePicker1.Value.Date, dateTimePicker2.Value);
                kdv = Convert.ToDecimal((toplamSatis * 18) / 100);

                //adisyonRepository.GunIslemleriEkle(dateTimePicker1.Value, dateTimePicker2.Value, adisyonSayisi, satilanSiparisAdedi, toplamSatis, gelir, gider, nakit, krediKarti, yemekCeki, ikram);

                GunSonuRaporuDuzenle(toplamSatis, gelir, gider, nakit, krediKarti, yemekCeki, ikram, adisyonSayisi, satilanSiparisAdedi);

                //else
                //{
                //    MsgBox msgBox = new MsgBox();
                //    MsgBox.baslik = "Uyarı";
                //    MsgBox.message = "Raporu görüntülemek için 'Gün Sonu Yap'mayı unutmayın..!";
                //    MsgBox.BoxButtons = MessageBoxButtons.OK;
                //    msgBox.ShowDialog();
                //}
            }
        }

        private void Raporlar_Load(object sender, EventArgs e)
        {
        }        
    }
}
