using AdisyonProg.Core.Repository;
using AdisyonProg.Entity;
using AdisyonProg.Core.Helper;
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
    public partial class StokGuncelle : Form
    {
        public static string urunAdi;

        public StokGuncelle()
        {
            InitializeComponent();
            txt_urun_adi.Text = urunAdi;
        }

        private void StokGuncelle_Load(object sender, EventArgs e)
        {
            using(AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                List<Urun> urun = new List<Urun>();
                urun = adisyonRepository.StokGetir(txt_urun_adi.Text);
                for (int i = 0; i < urun.Count; i++)
                {
                    txt_urun_adedi.Text = urun[i].UrunStokAdedi.ToString();
                    txt_urun_fiyati.Text = urun[i].UrunFiyati.ToString();
                    txt_urun_aciklama.Text = urun[i].UrunAciklama;

                    foreach (RadioButton item in this.Controls.OfType<RadioButton>())
                    {
                        if (item.Text == urun[i].BirimCinsi.Trim())
                        {
                            item.Checked = true;
                        }
                    }
                }
            }            
        }

        //public bool NullControl()
        //{
        //    bool flag = true;

        //    foreach (Control item in this.Controls)
        //    {
        //        if (item is TextBox)
        //        {
        //            if (item.Text == "" || item.Text == null)
        //            {
        //                flag = false;
        //                break;
        //            }                    
        //        }
        //    }

        //    return flag;
        //}

        private void btn_ekle_Click(object sender, EventArgs e)
        {
            MsgBox msgBox = new MsgBox();
            var checkedButton = this.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);

            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                this.Name = "form";
                Form f = this;                    
                bool control = Globalislemler.NullControl(f);

                if (control == true)
                {
                    int returnValue = adisyonRepository.StokGuncelle(txt_urun_adi.Text, Convert.ToDecimal(txt_urun_fiyati.Text), Convert.ToDecimal(txt_urun_adedi.Text), dt_tarih.Value, checkedButton.Text, txt_urun_aciklama.Text);

                    if (returnValue > 0)
                    {
                        MsgBox.baslik = "Stok Güncelleme";
                        MsgBox.message = "Stok Güncelleme işlemi başarılı...";
                        MsgBox.BoxButtons = MessageBoxButtons.OK;
                        msgBox.ShowDialog();

                        if (MsgBox.result == DialogResult.OK)
                        {
                            foreach (var item in this.Controls.OfType<TextBox>())
                            {
                                item.Clear();
                                this.Close();
                            }
                        }
                    }
                    else
                    {
                        MsgBox.baslik = "Hata";
                        MsgBox.message = "Stok Güncelleme işlemi başarısız..!";
                        MsgBox.BoxButtons = MessageBoxButtons.OK;
                        msgBox.ShowDialog();
                    }
                }
                else
                {
                    MsgBox.baslik = "Hata";
                    MsgBox.message = "Tüm alanlar doldurulmalıdır..!";
                    MsgBox.BoxButtons = MessageBoxButtons.OK;
                    msgBox.ShowDialog();
                }
            }
        }

        private void txt_urun_fiyati_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
        }

        private void txt_urun_adedi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
        }
    }
}
