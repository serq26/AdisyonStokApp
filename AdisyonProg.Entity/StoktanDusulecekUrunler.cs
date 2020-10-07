using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdisyonProg.Entity
{
    public class StoktanDusulecekUrunler
    {
        public int ID { get; set; }
        public int MenuUrunID { get; set; }
        public string UrunAdi { get; set; }
        public decimal UrunAdedi { get; set; }
        public string BirimCinsi { get; set; }

        public override string ToString()
        {
            return UrunAdi + " - " + "( " + UrunAdedi + " )" + " " + BirimCinsi;
        }
    }
}
