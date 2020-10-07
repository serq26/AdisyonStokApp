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
    public partial class Personel : Form
    {
        List<Garson> PersonelListesi;
        string secilenPersonel;
        int secilenId = 0;
        public Personel()
        {
            InitializeComponent();
            PersonelListesi = new List<Garson>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GarsonKayitFormu kayitFormu = new GarsonKayitFormu();
            kayitFormu.Show();
            this.Hide();
        }

        private void Personel_Load(object sender, EventArgs e)
        {           


            DataDoldur();   
        }
        
        private void DataDoldur()
        {
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                PersonelListesi = adisyonRepository.PersonelleriGetir();
                dataGridView1.DataSource = PersonelListesi;
                dataGridView1.Columns["KullaniciAdi"].Visible = false;
                dataGridView1.Columns["Sifre"].Visible = false;

                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    if (dataGridView1.Columns[i].Name == "GarsonID")
                    {
                        dataGridView1.Columns[i].Width = 50;
                    }
                    else if (dataGridView1.Columns[i].Name == "Adres")
                    {
                        dataGridView1.Columns[i].Width = 400;
                    }
                    else
                    {
                        dataGridView1.Columns[i].Width = 150;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (secilenPersonel != null)
            {
                MsgBox msgBox = new MsgBox();
                using(AdisyonRepository adisyonRepository = new AdisyonRepository())
                {
                   int value = adisyonRepository.PersonelSil(secilenId, secilenPersonel);

                    if (value > 0)
                    {
                        MsgBox.baslik = "Bilgi";
                        MsgBox.BoxButtons = MessageBoxButtons.OK;
                        MsgBox.message = "Personel başarıyla silindi.";
                        msgBox.ShowDialog();
                        DataDoldur();
                    }
                    else
                    {
                        MsgBox.baslik = "Hata";
                        MsgBox.BoxButtons = MessageBoxButtons.OK;
                        MsgBox.message = "Personel silinemedi..!";
                        msgBox.ShowDialog();
                    }
                }
            }
            else
            {
                MsgBox msgBox = new MsgBox();
                MsgBox.message = "Silmek istediğiniz personeli listeden seçiniz..!";
                MsgBox.baslik = "Uyarı";
                MsgBox.BoxButtons = MessageBoxButtons.OK;
                msgBox.ShowDialog();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow drow in dataGridView1.SelectedRows)
            {
                secilenPersonel = drow.Cells["Ad"].Value.ToString();
                secilenId = Convert.ToInt32(drow.Cells["GarsonID"].Value);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (secilenPersonel != null)
            {
                PersonelGuncelle personelGuncelle = new PersonelGuncelle();
                PersonelGuncelle.secilenID = secilenId;
                PersonelGuncelle.secilenPersonel = secilenPersonel;
                personelGuncelle.ShowDialog();
                this.Hide();
            }
            else
            {
                MsgBox msgBox = new MsgBox();
                MsgBox.message = "Güncellemek istediğiniz personeli listeden seçiniz..!";
                MsgBox.baslik = "Uyarı";
                MsgBox.BoxButtons = MessageBoxButtons.OK;
                msgBox.ShowDialog();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataDoldur();
        }
    }
}
