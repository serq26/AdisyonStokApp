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
using AdisyonProg.Entity;

namespace AdisyonProg.WinApp
{
    public partial class SoftDrink : Form
    {
        List<Entity.Menu> KategoriUrunler;
        List<StoktanDusulecekUrunler> stoktanDusulecekUrunler;
       
        public SoftDrink()
        {
            InitializeComponent();
            KategoriUrunler = new List<Entity.Menu>();
            stoktanDusulecekUrunler = new List<StoktanDusulecekUrunler>();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SoftDrink_Load(object sender, EventArgs e)
        {
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                KategoriUrunler = adisyonRepository.KategoriyeGoreUrunGetir("Soğuk İçecekler");
                comboBox1.DataSource = KategoriUrunler;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                int id = adisyonRepository.MenuUrunIDGetir(comboBox1.Text);
                stoktanDusulecekUrunler = adisyonRepository.StoktanDusulecekUrunleriGetir(id);

                foreach (var item in stoktanDusulecekUrunler)
                {
                    adisyonRepository.StoktanEksiltme(item.UrunAdi, 1);
                }
                //MasaDetay.softIcecek = comboBox1.Text;
                MasaDetay.softIcecekler.Add(comboBox1.Text);
                this.Close();
            }
        }
    }
}
