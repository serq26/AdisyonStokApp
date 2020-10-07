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
    public partial class BolumSil : Form
    {
        public BolumSil()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MsgBox msgBox = new MsgBox();

            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                int returnValue = adisyonRepository.BolumSil(comboBox1.Text);

                if (returnValue > 0)
                {
                    MsgBox.baslik = "Bölüm Sil";
                    MsgBox.BoxButtons = MessageBoxButtons.OK;
                    MsgBox.message = "Bölüm Silindi...";
                    msgBox.ShowDialog();
                    this.Close();                    
                }
                else
                {
                    MsgBox.baslik = "Bölüm Sil";
                    MsgBox.BoxButtons = MessageBoxButtons.OK;
                    MsgBox.message = "Bölüm Silme İşlemi Başarısız..!";
                    msgBox.ShowDialog();
                }
            }
        }

        private void BolumSil_Load(object sender, EventArgs e)
        {
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                List<MasaBolumleri> masaBolumleri = new List<MasaBolumleri>();
                masaBolumleri = adisyonRepository.MasaBolumleriGetir();
                comboBox1.DataSource = masaBolumleri;
            }
        }

        private void BolumSil_FormClosed(object sender, FormClosedEventArgs e)
        {
            Masalar masalar = new Masalar();
            this.Visible = false;
            masalar.Show();
        }
    }
}
