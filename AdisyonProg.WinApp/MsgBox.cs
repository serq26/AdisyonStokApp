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
    public partial class MsgBox : Form
    {       
        public static DialogResult result;
        public static string message;
        public static string baslik;
        public static MessageBoxButtons BoxButtons;

        public MsgBox()
        {
            InitializeComponent();
        }

        private void MsgBox_Load(object sender, EventArgs e)
        {
            Message(message,BoxButtons);
            this.Text = baslik;
        }

        public void Message(string mesaj,MessageBoxButtons buttons)
        {           

            Label lbl = new Label();
            lbl.Text = mesaj;
            lbl.ForeColor = Color.DimGray;
            lbl.Width = 500;
            lbl.Font = new Font("Century Gothic", 12, FontStyle.Bold);
            lbl.AutoSize = false;
            lbl.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            lbl.Location = new Point(0, 50);
            this.Controls.Add(lbl);

            if(buttons == MessageBoxButtons.YesNo)
            {
                
                Button YesButton = new Button();
                YesButton.Text = "YES";
                YesButton.BackColor = Color.ForestGreen;
                YesButton.ForeColor = Color.White;
                YesButton.Font = new Font("Century Gothic", 16, FontStyle.Bold);
                YesButton.Location = new Point(120,125);
                YesButton.Width = 155;
                YesButton.Height = 45;
                YesButton.FlatStyle = FlatStyle.Flat;
                YesButton.FlatAppearance.BorderSize = 0;
                YesButton.Click += YesButton_Click;
                this.Controls.Add(YesButton);

                Button NoButton = new Button();
                NoButton.Text = "NO";
                NoButton.BackColor = Color.Red;
                NoButton.ForeColor = Color.White;
                NoButton.Font = new Font("Century Gothic", 16, FontStyle.Bold);
                NoButton.Location = new Point(360,125);
                NoButton.Width = 155;
                NoButton.Height = 45;
                NoButton.FlatStyle = FlatStyle.Flat;
                NoButton.FlatAppearance.BorderSize = 0;
                NoButton.Click += NoButton_Click;
                this.Controls.Add(NoButton);
            }
            else
            {
                Button OkButton = new Button();
                OkButton.Text = "OK";
                OkButton.BackColor = Color.Green;
                OkButton.ForeColor = Color.White;
                OkButton.Font = new Font("Century Gothic", 16, FontStyle.Bold);
                OkButton.Location = new Point(250,125);
                OkButton.Width = 155;
                OkButton.Height = 45;
                OkButton.FlatStyle = FlatStyle.Flat;
                OkButton.FlatAppearance.BorderSize = 0;
                OkButton.Click += OkButton_Click;
                this.Controls.Add(OkButton);
            }           
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            result = DialogResult.OK;
            this.Close();
        }

        private void NoButton_Click(object sender, EventArgs e)
        {
            result = DialogResult.No;
            this.Close();
        }

        private void YesButton_Click(object sender, EventArgs e)
        {
            result = DialogResult.Yes;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            result = DialogResult.None;
            this.Close();
        }
    }
}
