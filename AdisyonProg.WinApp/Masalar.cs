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
    public partial class Masalar : Form
    {
        List<Masa> TumMasalar;
        public Masalar()
        {
            InitializeComponent();
            TumMasalar = new List<Masa>();
        }

        private void Masalar_Load(object sender, EventArgs e)
        {
            using(AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                TumMasalar = adisyonRepository.MasalariGetir();
                if (TumMasalar.Count > 0)
                {
                    TumMasalar.RemoveAt(0);
                }

                for (int i = 0; i < TumMasalar.Count; i++)
                {
                    Button Masa = new Button();
                    Masa.Text = TumMasalar[i].MasaAdi;
                    Masa.BackColor = Color.FromArgb(240, 169, 63);   
                    Masa.ForeColor = Color.White;
                    Masa.Font = new Font("Georgia", 24, FontStyle.Bold);
                    Masa.FlatStyle = FlatStyle.Flat;
                    Masa.FlatAppearance.BorderSize = 0;
                    Masa.Height = 100;
                    Masa.Width = 200;
                    Masa.Margin = new Padding(20, 20, 20, 20);
                    Masa.TabIndex = i;
                    this.flowLayoutPanel1.Controls.Add(Masa);
                    panel2.Controls.Add(flowLayoutPanel1);
                }

                List<MasaBolumleri> masaBolumleri = new List<MasaBolumleri>();
                masaBolumleri = adisyonRepository.MasaBolumleriGetir();
                comboBox1.DataSource = masaBolumleri;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MasaEkle masaEkle = new MasaEkle();
            masaEkle.ShowDialog();
            this.Hide();           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MasaSil masaSil = new MasaSil();
            masaSil.ShowDialog();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MasaBolumEkle masaBolumEkle = new MasaBolumEkle();
            masaBolumEkle.ShowDialog();
            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            string cmb = ((ComboBox)sender).Text;

            using(AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                TumMasalar = adisyonRepository.BolumeGoreMasalariGetir(cmb);

                for (int i = 0; i < TumMasalar.Count; i++)
                {
                    Button Masa = new Button();
                    Masa.Text = TumMasalar[i].MasaAdi;
                    Masa.BackColor = Color.FromArgb(240, 169, 63);
                    Masa.ForeColor = Color.White;
                    Masa.Font = new Font("Georgia", 24, FontStyle.Bold);
                    Masa.FlatStyle = FlatStyle.Flat;
                    Masa.FlatAppearance.BorderSize = 0;
                    Masa.Height = 100;
                    Masa.Width = 200;
                    Masa.Margin = new Padding(20, 20, 20, 20);
                    Masa.TabIndex = i;
                    this.flowLayoutPanel1.Controls.Add(Masa);
                    panel2.Controls.Add(flowLayoutPanel1);
                }
            }
        }

        private void Masalar_FormClosed(object sender, FormClosedEventArgs e)
        {
            YonetimEkrani yonetimEkrani = new YonetimEkrani();
            this.Visible = false;
            yonetimEkrani.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            BolumSil bolumSil = new BolumSil();
            bolumSil.ShowDialog();
            this.Hide();
        }
    }
}
