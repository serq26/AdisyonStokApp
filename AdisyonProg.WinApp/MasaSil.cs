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
    public partial class MasaSil : Form
    {
        public MasaSil()
        {
            InitializeComponent();
        }

        private void MasaSil_Load(object sender, EventArgs e)
        {
            using(AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                List<MasaBolumleri> masaBolumleri = new List<MasaBolumleri>();
                masaBolumleri = adisyonRepository.MasaBolumleriGetir();
                comboBox2.DataSource = masaBolumleri;

                List<Masa> masalar = new List<Masa>();
                masalar = adisyonRepository.BolumeGoreMasalariGetir(comboBox2.Text);
                //masalar.RemoveAt(0);
                comboBox1.DataSource = masalar;                
            }
        }

        private void btn_ekle_Click(object sender, EventArgs e)
        {
            MsgBox msgBox = new MsgBox();
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                int returnValue = adisyonRepository.MasaSil(comboBox1.Text,comboBox2.Text);

                if(returnValue>0)
                {
                    MsgBox.baslik = "Masa Sil";
                    MsgBox.BoxButtons = MessageBoxButtons.OK;
                    MsgBox.message = "Masa Silindi...";
                    msgBox.ShowDialog();

                    if (MsgBox.result == DialogResult.OK)
                    {
                        this.Close();
                    }
                }
                else
                {
                    MsgBox.baslik = "Masa Sil";
                    MsgBox.BoxButtons = MessageBoxButtons.OK;
                    MsgBox.message = "Masa Silme İşlemi Başarısız..!";
                    msgBox.ShowDialog();

                    if (MsgBox.result == DialogResult.OK)
                    {
                        this.Close();
                    }
                }
            }
        }

        private void MasaSil_FormClosed(object sender, FormClosedEventArgs e)
        {
                Masalar masalar = new Masalar();
                this.Visible = false;
                masalar.Show(); 
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cmb = ((ComboBox)sender).Text;

            using(AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                comboBox1.DataSource = adisyonRepository.BolumeGoreMasalariGetir(cmb);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
