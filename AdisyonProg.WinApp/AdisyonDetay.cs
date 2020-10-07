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
    public partial class AdisyonDetay : Form
    {
        public static int AdisyonNo;
        public static string cmb;
        Label lbl;
        public AdisyonDetay()
        {
            InitializeComponent();
        }

        private void AdisyonDetay_Load(object sender, EventArgs e)
        {
            using(AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                if (cmb == "Sadece Açık Adisyonlar")
                {
                    List<Adisyon> siparisler = new List<Adisyon>();
                    List<Adisyon> lstAdisyon = new List<Adisyon>();
                    siparisler = adisyonRepository.AcikAdisyondakiSiparisler(AdisyonNo);
                    foreach (var item in siparisler)
                    {
                        if (lstAdisyon.Any(x => (x.Siparis == item.Siparis)))
                        {
                            var indexs = lstAdisyon.FindIndex(x => (x.Siparis == item.Siparis));
                            lstAdisyon[indexs].SiparisAdedi += 1;
                        }
                        else
                        {
                            lstAdisyon.Add(item);
                        }
                    }

                    foreach (var item in lstAdisyon)
                    {
                        lbl = new Label();
                        lbl.Text = "- " + item.Siparis + " " + "(" + item.SiparisAdedi.ToString() + ")";
                        lbl.ForeColor = Color.White;
                        lbl.Font = new Font("Century Gothic", 14, FontStyle.Bold);
                        lbl.Width = 400;
                        flowLayoutPanel1.Controls.Add(lbl);
                    }

                }
                else if (cmb == "Ödenmiş Adisyonlar")
                {
                    List<Odeme> siparisler = new List<Odeme>();
                    List<Odeme> lst = new List<Odeme>();
                    siparisler = adisyonRepository.OdenmisAdisyondakiSiparisler(AdisyonNo);
                    foreach (var item in siparisler)
                    {   
                        if (lst.Any(x => (x.OdenenSiparis == item.OdenenSiparis)))
                        {
                            var indexs = lst.FindIndex(x => (x.OdenenSiparis == item.OdenenSiparis));
                            lst[indexs].SiparisAdedi += 1;
                        }
                        else
                        {
                            lst.Add(item);
                        }
                    }

                    foreach (var item in lst)
                    {
                        lbl = new Label();
                        lbl.Text = "- " + item.OdenenSiparis + " " + "(" + item.SiparisAdedi.ToString() + ")";
                        lbl.ForeColor = Color.White;
                        lbl.Font = new Font("Century Gothic", 14, FontStyle.Bold);
                        lbl.Width = 400;
                        flowLayoutPanel1.Controls.Add(lbl);
                    }
                }
                else
                {
                    
                }
            }           
        }
    }
}
