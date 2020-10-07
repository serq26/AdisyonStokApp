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

namespace AdisyonProg.WinApp
{
    public partial class Giris : Form
    {
        public string lbl_gunbasi;
        public Giris()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GarsonGiris garsonGiris = new GarsonGiris();
            garsonGiris.Show();
            this.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            KasaGiris kasaGiris = new KasaGiris();
            kasaGiris.Show();
            this.Visible = false;
        }

        private void Giris_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Giris_Load(object sender, EventArgs e)
        {
            GunBasiKontrol();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            YonetimGiris yonetimGiris = new YonetimGiris();
            yonetimGiris.Show();
            this.Visible = false;
        }

        private void GunBasiKontrol()
        {
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                string result = adisyonRepository.GunBasiYapildimiKontrol();
                if (result == "Hayır")
                {
                    button4.Text = "GÜN BAŞI YAP";
                    button4.Image = Properties.Resources.start;
                    button1.Enabled = false;
                    button2.Enabled = false;
                }
                else
                {
                    lbl_gunbasi = adisyonRepository.GunBasiTarih().ToString();
                    button4.Text = "GÜN SONU YAP";
                    button1.Enabled = true;
                    button2.Enabled = true;
                    button4.Text = "       GÜN SONU YAP";
                    button4.BackColor = Color.Maroon;
                    button4.Image = Properties.Resources.finish;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MsgBox msgBox = new MsgBox();

            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                if (button4.Text == "GÜN BAŞI YAP")
                {
                    MsgBox.baslik = "Gün Başı";
                    MsgBox.message = "Gün başı yapılacak. Onaylıyor musunuz ?";
                    MsgBox.BoxButtons = MessageBoxButtons.YesNo;
                    msgBox.ShowDialog();
                    if (MsgBox.result == DialogResult.Yes)
                    {
                        lbl_gunbasi = DateTime.Now.ToString();
                        adisyonRepository.GunBasiYapildimiGuncelle("Evet", DateTime.Now);
                        button4.Text = "      GÜN SONU YAP";
                        button4.BackColor = Color.Maroon;
                        button4.Image = Properties.Resources.finish;
                    }
                }
                else
                {
                    MsgBox.baslik = "Gün Sonu";
                    MsgBox.message = "Gün sonu yapılacak. Onaylıyor musunuz ?";
                    MsgBox.BoxButtons = MessageBoxButtons.YesNo;
                    msgBox.ShowDialog();
                    if (MsgBox.result == DialogResult.Yes)
                    {
                        adisyonRepository.GunBasiYapildimiGuncelle("Hayır", Convert.ToDateTime(lbl_gunbasi));
                        adisyonRepository.GunBasiSonuEkleme(Convert.ToDateTime(lbl_gunbasi), DateTime.Now);
                        button4.Text = "GÜN BAŞI YAP";
                        button4.BackColor = Color.DarkGreen;
                        button4.Image = Properties.Resources.start;
                        lbl_gunbasi = "";
                    }
                }
            }
            GunBasiKontrol();
        }
    }
}
