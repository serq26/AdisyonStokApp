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
    public partial class Menu : Form
    {
        List<Entity.Menu> TumUrunler;
        List<Entity.Menu> KategoriUrunler;
        List<MenuKategori> Kategoriler;

        public Menu()
        {
            InitializeComponent();
            TumUrunler = new List<Entity.Menu>();
            Kategoriler = new List<MenuKategori>();
            KategoriUrunler = new List<Entity.Menu>();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                TumUrunler = adisyonRepository.MenudekiTumUrunleriGetir();

                Kategoriler = adisyonRepository.MenuKategoriGetir();

                for (int i = 0; i < Kategoriler.Count; i++)
                {
                    Button kategori = new Button();
                    kategori.Text = Kategoriler[i].KategoriAdi;
                    kategori.ForeColor = Color.White;
                    kategori.Margin = new Padding(20, 10, 10, 10);
                    kategori.BackColor = Color.DarkRed;
                    kategori.Font = new Font("Century Gothic", 16, FontStyle.Bold);
                    kategori.FlatStyle = FlatStyle.Flat;
                    kategori.FlatAppearance.BorderSize = 0;
                    kategori.Height = 100;
                    kategori.Width = 200;
                    kategori.TabIndex = i;
                    kategori.Click += Kategori_Click;
                    flowLayoutPanel2.Controls.Add(kategori);
                }

                for (int i = 0; i < TumUrunler.Count; i++)
                {
                    Button urun = new Button();
                    urun.Text = TumUrunler[i].UrunAdi;
                    if (i % 2 == 0)
                    {
                        urun.BackColor = Color.FromArgb(240, 169, 63);
                        urun.ForeColor = Color.White;
                        urun.Font = new Font("Georgia", 18, FontStyle.Bold);
                        urun.FlatStyle = FlatStyle.Flat;
                        urun.FlatAppearance.BorderSize = 0;
                    }
                    else
                    {
                        urun.BackColor = Color.AliceBlue;
                        urun.Font = new Font("Georgia", 18, FontStyle.Bold);
                        urun.ForeColor = Color.Black;
                        urun.FlatStyle = FlatStyle.Flat;
                        urun.FlatAppearance.BorderSize = 0;
                    }
                    urun.Height = 100;
                    urun.Width = 200;
                    urun.TabIndex = i;
                    urun.Click += Urun_Click;

                    flowLayoutPanel1.Controls.Add(urun);
                }
            }
        }

        private void Kategori_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            string btn = ((Button)sender).Text;
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                KategoriUrunler = adisyonRepository.KategoriyeGoreUrunGetir(btn);

                TumUrunler = adisyonRepository.MenudekiTumUrunleriGetir();

                for (int i = 0; i < TumUrunler.Count; i++)                
                {
                    if (TumUrunler[i].UrunAciklama == "Hızlı")
                    {
                        Button urun = new Button();
                        urun.Text = TumUrunler[i].UrunAdi;
                        if (i % 2 == 0)
                        {
                            urun.BackColor = Color.FromArgb(240, 169, 63);
                            urun.ForeColor = Color.White;
                            urun.Font = new Font("Georgia", 18, FontStyle.Bold);
                            urun.FlatStyle = FlatStyle.Flat;
                            urun.FlatAppearance.BorderSize = 0;
                        }
                        else
                        {
                            urun.BackColor = Color.AliceBlue;
                            urun.Font = new Font("Georgia", 18, FontStyle.Bold);
                            urun.ForeColor = Color.Black;
                            urun.FlatStyle = FlatStyle.Flat;
                            urun.FlatAppearance.BorderSize = 0;
                        }
                        urun.Height = 100;
                        urun.Width = 200;
                        urun.TabIndex = i;
                        urun.Click += Urun_Click;

                        if (btn == "Hızlı")
                        {
                            flowLayoutPanel1.Controls.Add(urun);
                        }
                    }
                }

                for (int i = 0; i < KategoriUrunler.Count; i++)
                {
                    Button urun = new Button();
                    urun.Text = KategoriUrunler[i].UrunAdi;
                    if (i % 2 == 0)
                    {
                        urun.BackColor = Color.FromArgb(240, 169, 63);
                        urun.ForeColor = Color.White;
                        urun.Font = new Font("Georgia", 18, FontStyle.Bold);
                        urun.FlatStyle = FlatStyle.Flat;
                        urun.FlatAppearance.BorderSize = 0;
                    }
                    else
                    {
                        urun.BackColor = Color.AliceBlue;
                        urun.Font = new Font("Georgia", 18, FontStyle.Bold);
                        urun.ForeColor = Color.Black;
                        urun.FlatStyle = FlatStyle.Flat;
                        urun.FlatAppearance.BorderSize = 0;
                    }
                    urun.Height = 100;
                    urun.Width = 200;
                    urun.TabIndex = i;
                    urun.Click += Urun_Click;

                    flowLayoutPanel1.Controls.Add(urun);
                }
            }           
        }

        private void Urun_Click(object sender, EventArgs e)
        {
            string btn = ((Button)sender).Text;

            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                int id = adisyonRepository.MenuUrunId(btn);
                MenuUrunDetay menuUrunDetay = new MenuUrunDetay();
                MenuUrunDetay.UrunAdi = btn;
                MenuUrunDetay.UrunId = id;
                menuUrunDetay.ShowDialog();
                this.Hide();
            }            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UrunEkleMenu urunEkleMenu = new UrunEkleMenu();
            urunEkleMenu.ShowDialog();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MenuKategoriEkle menuKategoriEkle = new MenuKategoriEkle();
            menuKategoriEkle.ShowDialog();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MenuKategoriSil menuKategoriSil = new MenuKategoriSil();
            menuKategoriSil.ShowDialog();
            this.Hide();
        }
    }
}
