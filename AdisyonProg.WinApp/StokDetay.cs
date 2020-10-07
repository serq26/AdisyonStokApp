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
    public partial class StokDetay : Form
    {
        public static string urunAdi;
        public StokDetay()
        {
            InitializeComponent();
        }

        private void StokDetay_Load(object sender, EventArgs e)
        {
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                List<Urun> urun = new List<Urun>();
                urun = adisyonRepository.StokGetir(urunAdi);

                for (int i = 0; i < urun.Count; i++)
                {
                    label6.Text = urun[i].UrunAdi;
                    label7.Text = urun[i].UrunFiyati.ToString();
                    label8.Text = Convert.ToInt32(urun[i].UrunStokAdedi).ToString();
                    label9.Text = urun[i].StokGirisTarihi.ToString();
                    label10.Text = urun[i].UrunAciklama;
                    label11.Text = urun[i].BirimCinsi;
                }
            }
        }
    }
}
