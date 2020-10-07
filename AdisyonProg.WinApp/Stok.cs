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
    public partial class Stok : Form
    {
        public static List<Urun> stok;
        public Stok()
        {
            InitializeComponent();
            stok = new List<Urun>();            
        }

        public void Stok_Load(object sender, EventArgs e)
        {
            StokGetir();
        }

        public void StokGetir()
        {
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                stok = adisyonRepository.StokGetir();
                DuzenOlustur(stok.Count);
            }
        }

        private void SilBtn_Click(object sender, EventArgs e)
        {
            string urun = ((Button)sender).Name;           
            MsgBox msgBox = new MsgBox();
            MsgBox.baslik = "Stok Sil";
            MsgBox.BoxButtons = MessageBoxButtons.YesNo;
            MsgBox.message = $"{urun} stoktan silinsin mi ?";
            msgBox.ShowDialog();

            if(MsgBox.result == DialogResult.Yes)
            {
                using(AdisyonRepository adisyonRepository = new AdisyonRepository())
                {
                    adisyonRepository.StoktanUrunSil(urun);
                    flowLayoutPanel1.Controls.Clear();
                    StokGetir();
                }
            }         
        }

        private void DuzenOlustur(int cnt)
        {
            panel3.Controls.Clear();
            for (int i = 0; i < cnt; i++)
            {
                GroupBox groupBox = new GroupBox();
                groupBox.Text = "";
                groupBox.Height = 175;
                groupBox.Width = 317;

                Label stokAdi = new Label();
                stokAdi.Text = stok[i].UrunAdi;
                stokAdi.ForeColor = Color.White;
                stokAdi.Font = new Font("Century Gothic", 18, FontStyle.Bold);
                stokAdi.Width = 250;
                stokAdi.Height = 30;
                stokAdi.Location = new Point(20, 30);

                Button detayBtn = new Button();
                detayBtn.FlatStyle = FlatStyle.Flat;
                detayBtn.FlatAppearance.BorderSize = 0;
                detayBtn.Image = Properties.Resources.loupe;
                detayBtn.Width = 40;
                detayBtn.Height = 40;
                detayBtn.Name = stokAdi.Text;
                detayBtn.Click += DetayBtn_Click;
                detayBtn.Location = new Point(270,30);
                

                Label stokAdedi = new Label();
                stokAdedi.Text = (Convert.ToInt32(stok[i].UrunStokAdedi)).ToString();
                stokAdedi.ForeColor = Color.DarkOrange;
                stokAdedi.Font = new Font("Century Gothic", 16, FontStyle.Bold);
                stokAdedi.Width = 100;
                stokAdedi.Location = new Point(20, 60);

                Label birimCinsi = new Label();
                birimCinsi.Text = stok[i].BirimCinsi;
                birimCinsi.ForeColor = Color.Wheat;
                birimCinsi.Font = new Font("Century Gothic", 16);
                birimCinsi.Height = 30;
                birimCinsi.Width = 120;
                birimCinsi.Location = new Point(120, 60);

                Label stokBirimFiyati = new Label();
                stokBirimFiyati.Text = stok[i].UrunFiyati.ToString() + "  TL";
                stokBirimFiyati.ForeColor = Color.DarkOrange;
                stokBirimFiyati.Font = new Font("Century Gothic", 14, FontStyle.Italic);
                stokBirimFiyati.Location = new Point(20, 90);

                Button silBtn = new Button();
                silBtn.Text = "SİL";
                silBtn.BackColor = Color.DarkRed;
                silBtn.ForeColor = Color.White;
                silBtn.Font = new Font("Century Gothic", 14, FontStyle.Bold);
                silBtn.Width = 100;
                silBtn.Height = 30;
                silBtn.FlatStyle = FlatStyle.Flat;
                silBtn.FlatAppearance.BorderSize = 0;
                silBtn.Name = stokAdi.Text;
                silBtn.Click += SilBtn_Click;
                silBtn.Location = new Point(10, 140);

                Button updateBtn = new Button();
                updateBtn.Text = "Güncelle";
                updateBtn.BackColor = Color.SteelBlue;
                updateBtn.ForeColor = Color.White;
                updateBtn.Font = new Font("Century Gothic", 14, FontStyle.Bold);
                updateBtn.Width = 150;
                updateBtn.Height = 30;
                updateBtn.FlatStyle = FlatStyle.Flat;
                updateBtn.FlatAppearance.BorderSize = 0;
                updateBtn.Name = stokAdi.Text;
                updateBtn.Click += UpdateBtn_Click; ;
                updateBtn.Location = new Point(120, 140);

                groupBox.Controls.Add(stokAdi);
                groupBox.Controls.Add(silBtn);
                groupBox.Controls.Add(updateBtn);
                groupBox.Controls.Add(detayBtn);
                groupBox.Controls.Add(stokAdedi);
                groupBox.Controls.Add(birimCinsi);
                groupBox.Controls.Add(stokBirimFiyati);

                this.flowLayoutPanel1.Controls.Add(groupBox);
                panel3.Controls.Add(flowLayoutPanel1);
                flowLayoutPanel1.Padding = new Padding(120, 0, 0, 0);
            }
        }

        private void DetayBtn_Click(object sender, EventArgs e)
        {
            StokDetay.urunAdi = ((Button)sender).Name;
            StokDetay stokDetay = new StokDetay();
            stokDetay.ShowDialog();
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            StokGuncelle.urunAdi = ((Button)sender).Name;
            StokGuncelle stokGuncelle = new StokGuncelle();
            stokGuncelle.ShowDialog();            
        }

        public void StoktaUrunAra(string urunAdi)
        {
            flowLayoutPanel1.Controls.Clear();
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                stok = adisyonRepository.StokGetir(urunAdi);
                DuzenOlustur(stok.Count);
            }
        }
        private void btn_ekle_Click(object sender, EventArgs e)
        {
            StokEkle stokEkle = new StokEkle();
            stokEkle.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            StokGetir();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            string urun = txt.Text;
            StoktaUrunAra(urun);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                stok = adisyonRepository.StokBitenUrunler();
                DuzenOlustur(stok.Count);
            }
        }
    }
}
