using AdisyonProg.Core.Helper;
using AdisyonProg.Core.Repository;
using AdisyonProg.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdisyonProg.WinApp
{
    public partial class StokEkle : Form
    {
        public StokEkle()
        {
            InitializeComponent();
        }

        private void btn_ekle_Click(object sender, EventArgs e)
        {
            MsgBox msgBox = new MsgBox();

            this.Name = "form";
            Form f = this;
            bool control = Globalislemler.NullControl(f);

            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                if (control == true)
                {
                    Urun urun = new Urun();
                    urun.UrunAdi = txt_urun_adi.Text;
                    urun.StokGirisTarihi = dt_tarih.Value;
                    urun.UrunStokAdedi = int.Parse(txt_urun_adedi.Text);
                    urun.UrunAciklama = txt_urun_aciklama.Text;
                    //urun.UrunFiyati = Decimal.Parse(txt_urun_fiyati.Text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
                    urun.UrunFiyati = Convert.ToDecimal(txt_urun_fiyati.Text);
                    if (this.Controls.OfType<RadioButton>().Any(r => r.Checked) == true)
                    {
                        var checkedButton = this.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
                        urun.BirimCinsi = checkedButton.Text;
                    }
                    else
                    {
                        MsgBox.baslik = "Uyarı";
                        MsgBox.BoxButtons = MessageBoxButtons.OK;
                        MsgBox.message = "Birim cinsi seçiniz..!";
                        msgBox.ShowDialog();
                    }

                    int returnValue = adisyonRepository.StokUrunEkle(urun);

                    if (returnValue > 0)
                    {
                        MsgBox.baslik = "Kayıt";
                        MsgBox.message = "Ürün ekleme işlemi başarılı...";
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
                        MsgBox.message = "Ürün ekleme işlemi başarısız..!";
                        MsgBox.BoxButtons = MessageBoxButtons.OK;
                        msgBox.ShowDialog();
                    }
                }
                else
                {
                    MsgBox.baslik = "Hata";
                    MsgBox.message = "Tüm alanları doldurunuz..!";
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
