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
    public partial class StokMenuUrun : Form
    {
        int i = 0;
        public static List<Urun> SecilenUrunler;
        ComboBox comboBox;
        TextBox textBox;
        GroupBox groupBox;
        string birim = "";
        public StokMenuUrun()
        {
            InitializeComponent();
            SecilenUrunler = new List<Urun>();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            i += 1;
            comboBox = new ComboBox();
            textBox = new TextBox();
            groupBox = new GroupBox();

            groupBox.Width = 650;
            groupBox.Height = 358;
            groupBox.Text = "Ürün - " + i.ToString();
            groupBox.ForeColor = Color.White;
            groupBox.Font = new Font("Century Gothic", 12, FontStyle.Bold);
            

            Label label1 = new Label();
            label1.Text = "Stoktan Düşülecek Ürünü Seç:";
            label1.Font = new Font("Century Gothic", 12, FontStyle.Bold);
            label1.Width = 300;
            label1.Location = new Point(10,45);

            Label label2 = new Label();
            label2.Text = "Düşülecek Miktarı Giriniz:";
            label2.Font = new Font("Century Gothic", 12, FontStyle.Bold);
            label2.Width = 255;
            label2.Location = new Point(10,115);

            Label label3 = new Label();
            label3.Text = "Miktar Birimini Seç:";
            label3.Font = new Font("Century Gothic", 12, FontStyle.Bold);
            label3.Width = 195;
            label3.Location = new Point(10,186);

            groupBox.Controls.Add(label1);
            groupBox.Controls.Add(label2);
            groupBox.Controls.Add(label3);

            using(AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                comboBox.Width = 217;
                comboBox.Height = 31;
                comboBox.Location = new Point(325,37);
                List<Urun> urunler = new List<Urun>();
                urunler = adisyonRepository.StokGetir();
                urunler.Insert(0, new Urun { UrunAdi = "Ürün Seçiniz"});
                comboBox.DataSource = urunler;
                comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
                groupBox.Controls.Add(comboBox);
            }

            textBox.Width = 217;
            textBox.Height = 32;
            textBox.Location = new Point(325, 106);
            textBox.KeyPress += TextBox_KeyPress;
            groupBox.Controls.Add(textBox);

            RadioButton radioButton1 = new RadioButton();
            radioButton1.Text = "Kilogram";
            radioButton1.Location = new Point(18,234);
            
            RadioButton radioButton2 = new RadioButton();
            radioButton2.Text = "gram";
            radioButton2.Location = new Point(166,234);
            

            RadioButton radioButton3 = new RadioButton();
            radioButton3.Text = "Litre";
            radioButton3.Location = new Point(289,234);

            RadioButton radioButton4 = new RadioButton();
            radioButton4.Text = "MiliLitre";
            radioButton4.Location = new Point(405,234);

            RadioButton radioButton5 = new RadioButton();
            radioButton5.Text = "Tane";
            radioButton5.Location = new Point(545,234);

            groupBox.Controls.Add(radioButton1);
            groupBox.Controls.Add(radioButton2);
            groupBox.Controls.Add(radioButton3);
            groupBox.Controls.Add(radioButton4);
            groupBox.Controls.Add(radioButton5);

            Button button = new Button();
            button.Text = "OK";
            button.BackColor = Color.Green;
            button.ForeColor = Color.White;
            button.Location = new Point(255,289);
            button.Width = 162;
            button.Height = 46;
            button.Click += Button_Click;
            groupBox.Controls.Add(button);

            flowLayoutPanel1.Controls.Add(groupBox);

            if (comboBox.Text == "Ürün Seçiniz")
            {
                textBox.Enabled = false;
                foreach (RadioButton item in groupBox.Controls.OfType<RadioButton>())
                {
                    item.Enabled = false;
                }
            }
            else
            {
                textBox.Enabled = true;
            }
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
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

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox.Text == "Ürün Seçiniz")
            {
                textBox.Enabled = false;
                foreach (RadioButton item in groupBox.Controls.OfType<RadioButton>())
                {
                    item.Enabled = false;
                }
            }
            else
            {
                textBox.Enabled = true;

                using (AdisyonRepository adisyonRepository = new AdisyonRepository())
                {
                    birim = (adisyonRepository.StokBirimCinsiKontrol(comboBox.Text)).Trim();

                    if (birim.Contains("Litre"))
                    {
                        foreach (RadioButton item in groupBox.Controls.OfType<RadioButton>())
                        {
                            if (item.Text.Contains("Litre"))
                            {
                                item.Enabled = true;
                            }
                            else
                            {
                                item.Enabled = false;
                            }
                        }
                    }
                    else if (birim.Contains("gram"))
                    {
                        foreach (RadioButton item in groupBox.Controls.OfType<RadioButton>())
                        {
                            if (item.Text.Contains("gram"))
                            {
                                item.Enabled = true;
                            }
                            else
                            {
                                item.Enabled = false;
                            }
                        }
                    }
                    else
                    {
                        foreach (RadioButton item in groupBox.Controls.OfType<RadioButton>())
                        {
                            if (item.Text.Contains("Tane"))
                            {
                                item.Enabled = true;
                            }
                            else
                            {
                                item.Enabled = false;
                            }
                        }

                    }
                }
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            MsgBox msgBox = new MsgBox();
            decimal girilenDeğer = 0;
            var checkedButton2 = groupBox.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);

            if (checkedButton2 != null && textBox.Text != null && textBox.Text != "")
            {
                string girilenBirim = checkedButton2.Text;

                girilenDeğer = Convert.ToDecimal(textBox.Text);

                //foreach (RadioButton item in groupBox.Controls.OfType<RadioButton>())
                //{
                switch (birim)
                {
                    case "Kilogram":
                        switch (girilenBirim)
                        {
                            case "gram":
                                girilenDeğer = girilenDeğer / 1000;
                                break;
                            default:
                                break;
                        }
                        break;

                    case "gram":
                        switch (girilenBirim)
                        {
                            case "Kilogram":
                                girilenDeğer = girilenDeğer * 1000;
                                break;
                            default:
                                break;
                        }
                        break;

                    case "Litre":
                        switch (girilenBirim)
                        {
                            case "MiliLitre":
                                girilenDeğer = girilenDeğer / 1000;
                                break;
                            default:
                                break;
                        }
                        break;

                    case "MiliLitre":
                        switch (girilenBirim)
                        {
                            case "Litre":
                                girilenDeğer = girilenDeğer * 1000;
                                break;
                            default:
                                break;
                        }
                        break;

                    default:
                        break;
                }
                //}

                Urun urun = new Urun();
                urun.UrunAdi = comboBox.Text;
                urun.UrunStokAdedi = girilenDeğer;
                var checkedButton = groupBox.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
                //urun.BirimCinsi = checkedButton.Text;
                urun.BirimCinsi = birim;

                if (SecilenUrunler.Any(x => x.UrunAdi == comboBox.Text) == false)
                {
                    SecilenUrunler.Add(urun);
                }
                else
                {
                    MsgBox.baslik = "Hata";
                    MsgBox.message = "Aynı ürün tekrar seçilemez..!";
                    MsgBox.BoxButtons = MessageBoxButtons.OK;
                    msgBox.ShowDialog();
                }
                MsgBox.baslik = "";
                MsgBox.message = "Başarılı";
                MsgBox.BoxButtons = MessageBoxButtons.OK;
                msgBox.ShowDialog();
            }
            else
            {
                MsgBox.baslik = "";
                MsgBox.message = "Tüm alanları boş bırakmadan düzgün bir şekilde doldurunuz..!";
                MsgBox.BoxButtons = MessageBoxButtons.OK;
                msgBox.ShowDialog();
            }
        }

        private void StokMenuUrun_FormClosed(object sender, FormClosedEventArgs e)
        {
            UrunEkleMenu urunEkleMenu = new UrunEkleMenu();
            this.Visible = false;

            for (int i = 0; i < SecilenUrunler.Count; i++)
            {
                urunEkleMenu.label6.Text += SecilenUrunler[i].UrunAdi +" ("+ SecilenUrunler[i].UrunStokAdedi + " - "+ SecilenUrunler[i].BirimCinsi + " )"+" ,";
            }
            urunEkleMenu.Show();
        }

        private void StokMenuUrun_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            UrunEkleMenu urunEkleMenu = new UrunEkleMenu();
            this.Visible = false;

            UrunEkleMenu.StoktanDusulecekUrunler = SecilenUrunler;

            for (int i = 0; i < SecilenUrunler.Count; i++)
            {
                urunEkleMenu.label6.Text += SecilenUrunler[i].UrunAdi + " (" + SecilenUrunler[i].UrunStokAdedi + " - " + SecilenUrunler[i].BirimCinsi + " )" + " ,";
            }
            urunEkleMenu.Show();
        }
    }
}
