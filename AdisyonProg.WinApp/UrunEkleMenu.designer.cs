namespace AdisyonProg.WinApp
{
    partial class UrunEkleMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UrunEkleMenu));
            this.label1 = new System.Windows.Forms.Label();
            this.txt_urun_adi = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_urun_aciklama = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_urun_fiyati = new System.Windows.Forms.TextBox();
            this.btn_ekle = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cmb_urun_kategori = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_maliyet = new System.Windows.Forms.TextBox();
            this.lbl_maliyet = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(616, 188);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ürün Adı:";
            // 
            // txt_urun_adi
            // 
            this.txt_urun_adi.Location = new System.Drawing.Point(779, 179);
            this.txt_urun_adi.Margin = new System.Windows.Forms.Padding(4);
            this.txt_urun_adi.Name = "txt_urun_adi";
            this.txt_urun_adi.Size = new System.Drawing.Size(290, 32);
            this.txt_urun_adi.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(616, 355);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "Menü Fiyatı:";
            // 
            // txt_urun_aciklama
            // 
            this.txt_urun_aciklama.Location = new System.Drawing.Point(779, 563);
            this.txt_urun_aciklama.Margin = new System.Windows.Forms.Padding(4);
            this.txt_urun_aciklama.Multiline = true;
            this.txt_urun_aciklama.Name = "txt_urun_aciklama";
            this.txt_urun_aciklama.Size = new System.Drawing.Size(290, 84);
            this.txt_urun_aciklama.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(611, 563);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(160, 23);
            this.label3.TabIndex = 0;
            this.label3.Text = "Ürün Açıklama:";
            // 
            // txt_urun_fiyati
            // 
            this.txt_urun_fiyati.Location = new System.Drawing.Point(779, 346);
            this.txt_urun_fiyati.Margin = new System.Windows.Forms.Padding(4);
            this.txt_urun_fiyati.Name = "txt_urun_fiyati";
            this.txt_urun_fiyati.Size = new System.Drawing.Size(290, 32);
            this.txt_urun_fiyati.TabIndex = 3;
            this.txt_urun_fiyati.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_urun_fiyati_KeyPress);
            // 
            // btn_ekle
            // 
            this.btn_ekle.BackColor = System.Drawing.Color.Green;
            this.btn_ekle.FlatAppearance.BorderSize = 0;
            this.btn_ekle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ekle.Font = new System.Drawing.Font("Century Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btn_ekle.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_ekle.Location = new System.Drawing.Point(779, 655);
            this.btn_ekle.Margin = new System.Windows.Forms.Padding(4);
            this.btn_ekle.Name = "btn_ekle";
            this.btn_ekle.Size = new System.Drawing.Size(290, 59);
            this.btn_ekle.TabIndex = 6;
            this.btn_ekle.Text = "EKLE";
            this.btn_ekle.UseVisualStyleBackColor = false;
            this.btn_ekle.Click += new System.EventHandler(this.btn_ekle_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(616, 434);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(146, 23);
            this.label4.TabIndex = 0;
            this.label4.Text = "Ürün Kategori:";
            // 
            // cmb_urun_kategori
            // 
            this.cmb_urun_kategori.FormattingEnabled = true;
            this.cmb_urun_kategori.Location = new System.Drawing.Point(780, 424);
            this.cmb_urun_kategori.Margin = new System.Windows.Forms.Padding(4);
            this.cmb_urun_kategori.Name = "cmb_urun_kategori";
            this.cmb_urun_kategori.Size = new System.Drawing.Size(289, 31);
            this.cmb_urun_kategori.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(616, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(317, 23);
            this.label5.TabIndex = 4;
            this.label5.Text = "Stoktan Düşülecek Ürünleri Seç:";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(947, 21);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 45);
            this.button1.TabIndex = 0;
            this.button1.Text = "SEÇ";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Tomato;
            this.label6.Location = new System.Drawing.Point(204, 105);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 23);
            this.label6.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 105);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(160, 23);
            this.label7.TabIndex = 7;
            this.label7.Text = "Seçilen Ürünler:";
            // 
            // txt_maliyet
            // 
            this.txt_maliyet.Location = new System.Drawing.Point(779, 259);
            this.txt_maliyet.Margin = new System.Windows.Forms.Padding(4);
            this.txt_maliyet.Name = "txt_maliyet";
            this.txt_maliyet.Size = new System.Drawing.Size(290, 32);
            this.txt_maliyet.TabIndex = 2;
            this.txt_maliyet.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_maliyet_KeyPress);
            // 
            // lbl_maliyet
            // 
            this.lbl_maliyet.AutoSize = true;
            this.lbl_maliyet.Location = new System.Drawing.Point(616, 268);
            this.lbl_maliyet.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_maliyet.Name = "lbl_maliyet";
            this.lbl_maliyet.Size = new System.Drawing.Size(145, 23);
            this.lbl_maliyet.TabIndex = 8;
            this.lbl_maliyet.Text = "Maliyet Fiyatı:";
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(1725, -2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(47, 48);
            this.button3.TabIndex = 10;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(825, 495);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(244, 31);
            this.comboBox1.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(616, 505);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(201, 23);
            this.label8.TabIndex = 11;
            this.label8.Text = "Siparişin Çıkağı Yer:";
            // 
            // UrunEkleMenu
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(41)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(1775, 742);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.txt_maliyet);
            this.Controls.Add(this.lbl_maliyet);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmb_urun_kategori);
            this.Controls.Add(this.btn_ekle);
            this.Controls.Add(this.txt_urun_fiyati);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_urun_aciklama);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_urun_adi);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UrunEkleMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UrunEkleMenu";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UrunEkleMenu_FormClosed);
            this.Load += new System.EventHandler(this.UrunEkleMenu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_urun_adi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_urun_aciklama;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_urun_fiyati;
        private System.Windows.Forms.Button btn_ekle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmb_urun_kategori;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_maliyet;
        private System.Windows.Forms.Label lbl_maliyet;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label8;
    }
}