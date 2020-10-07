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
    public partial class Adisyonlar : Form
    {
        List<Odeme> AdisyonlarList;
        List<Adisyon> Acikadisyonlar;
        string cmb;
        public Adisyonlar()
        {
            InitializeComponent();
            AdisyonlarList = new List<Odeme>();
            Acikadisyonlar = new List<Adisyon>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cmb = comboBox1.Text;

            if (cmb == "Sadece Açık Adisyonlar")
            {                
                AcikAdisyonlar();
            }
            else if(cmb == "Ödenmiş Adisyonlar")
            {
                OdenmisAdisyonlar();
            }
            else
            {
                MsgBox msgBox = new MsgBox();
                MsgBox.baslik = "Uyarı";
                MsgBox.message = "Filtre Seçiniz..!";
                MsgBox.BoxButtons = MessageBoxButtons.OK;
            }
        }

        private void Adisyonlar_Load(object sender, EventArgs e)
        {
            OdenmisAdisyonlar();
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.HeaderText = "Siparişler";
            btn.Text = "Siparişleri Görüntüle";
            btn.Name = "btn";
            btn.UseColumnTextForButtonValue = true;
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.BackColor = Color.Red;
            style.ForeColor = Color.White;
            style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            btn.DefaultCellStyle = style;
            dataGridView1.Columns.Add(btn);

            DataGridDuzen();           
        }

        private void DataGridDuzen()
        {
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                if (dataGridView1.Columns[i].Name == "SiparisAdedi")
                {
                    dataGridView1.Columns[i].Width = 120;
                }
                else if (dataGridView1.Columns[i].Name == "Tarih")
                {
                    dataGridView1.Columns[i].Width = 220;
                }
                else
                {
                    dataGridView1.Columns[i].Width = 200;
                }
            }
        }

        private void OdenmisAdisyonlar()
        {
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                AdisyonlarList = adisyonRepository.OdenmisAdisyonlariGetir(dateTimePicker1.Value,dateTimePicker2.Value);
                dataGridView1.DataSource = AdisyonlarList;
                dataGridView1.Columns["MasaID"].Visible = false;
                dataGridView1.Columns["OdenenSiparis"].Visible = false;
                dataGridView1.Columns["OdemeTipi"].Visible = false;
                dataGridView1.Columns["Iskonto"].Visible = false;
                dataGridView1.Columns["MasaDurumu"].Visible = false;
                dataGridView1.Columns["ID"].Visible = false;

                DataGridDuzen();
            }
        }

        private void AcikAdisyonlar()
        {
            using (AdisyonRepository adisyonRepository = new AdisyonRepository())
            {
                Acikadisyonlar = adisyonRepository.AcikAdisyonlariGetir();
                dataGridView1.DataSource = Acikadisyonlar;
                dataGridView1.Columns["AdisyonID"].Visible = false;
                dataGridView1.Columns["Siparis"].Visible = false;
                dataGridView1.Columns["SiparisFiyati"].HeaderText = "Tutar";

                DataGridDuzen();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 11 || e.ColumnIndex == 0)
            {
                foreach (DataGridViewRow drow in dataGridView1.SelectedRows)
                {
                    using (AdisyonRepository adisyonRepository = new AdisyonRepository())
                    {
                        AdisyonDetay adisyonDetay = new AdisyonDetay();
                        AdisyonDetay.AdisyonNo = Convert.ToInt32(drow.Cells["AdisyonNo"].Value);
                        AdisyonDetay.cmb = comboBox1.Text;
                        adisyonDetay.ShowDialog();
                    }
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cmb = ((ComboBox)sender).Text;

            if(cmb == "Sadece Açık Adisyonlar")
            {
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
            }
            else
            {
                dateTimePicker1.Enabled = true;
                dateTimePicker2.Enabled = true;
            }
        
        }
    }
}
