using AdisyonProg.Core.Helper;
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
    public partial class MasaEkle : Form
    {
        public MasaEkle()
        {
            InitializeComponent();
        }       

        private void btn_ekle_Click(object sender, EventArgs e)
        {
            MsgBox msgBox = new MsgBox();

            this.Name = "form";
            Form f = this;
            bool control = Globalislemler.NullControl(f);

            if (control == true)
            {
                Masa masa = new Masa();
                masa.MasaAdi = txt_masa_adi.Text;
                masa.MasaRengi = "Kapalı";
                masa.Bolum = comboBox1.Text;


                using (AdisyonRepository adisyonRepository = new AdisyonRepository())
                {
                    int returnValue = adisyonRepository.MasaEkle(masa);

                    if (returnValue > 0)
                    {
                        MsgBox.baslik = "Masa Ekle";
                        MsgBox.message = "Masa ekleme işlemi başarılı...";
                        MsgBox.BoxButtons = MessageBoxButtons.OK;
                        msgBox.ShowDialog(); this.Close();

                    }
                    else
                    {
                        MsgBox.baslik = "Hata";
                        MsgBox.message = "Masa ekleme işlemi başarısız..!";
                        MsgBox.BoxButtons = MessageBoxButtons.OK;
                        msgBox.ShowDialog();
                    }
                }
            }
            else
            {
                MsgBox.baslik = "Hata";
                MsgBox.message = "Tüm alanları doldurunuz...!";
                MsgBox.BoxButtons = MessageBoxButtons.OK;
                msgBox.ShowDialog();
            }           
        }

        private void MasaEkle_FormClosed(object sender, FormClosedEventArgs e)
        {
                Masalar masalar = new Masalar();
                this.Visible = false;
                masalar.Show();                       
        }

        private void MasaEkle_Load(object sender, EventArgs e)
        {
            using(AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                List<MasaBolumleri> masaBolumleris = new List<MasaBolumleri>();
                masaBolumleris = adisyonRepository.MasaBolumleriGetir();
                comboBox1.DataSource = masaBolumleris;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
