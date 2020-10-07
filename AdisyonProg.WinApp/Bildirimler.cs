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
    public partial class Bildirimler : Form
    {
        public Bildirimler()
        {
            InitializeComponent();
        }

        private void Bildirimler_Load(object sender, EventArgs e)
        {
            List<Urun> BitenUrunler = new List<Urun>();

            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                BitenUrunler = adisyonRepository.StokBitenUrunler();
                if (BitenUrunler.Count > 0)
                {
                    for (int i = 0; i < BitenUrunler.Count; i++)
                    {
                        Label lblBildirim = new Label();
                        lblBildirim.ForeColor = Color.White;
                        lblBildirim.Font = new Font("Century Gothic", 14, FontStyle.Bold);
                        lblBildirim.Width = 500;
                        lblBildirim.Location = new Point(50, 80);
                        lblBildirim.Text = $"-   {BitenUrunler[i].UrunAdi} stok bitti..!";
                        
                        flowLayoutPanel1.Controls.Add(lblBildirim);
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
