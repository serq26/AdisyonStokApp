using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdisyonProg.Entity
{
    public class Menu
    {
        public int MenuUrunID { get; set; }
        public int UrunID { get; set; }
        public string UrunAdi { get; set; }
        public string UrunAciklama { get; set; }
        public decimal MaliyetFiyati { get; set; }
        public decimal UrunFiyati { get; set; }
        public string UrunKategori { get; set; }
        public string SiparisCikicakYer { get; set; }

        public override string ToString()
        {
            return UrunAdi;
        }
    }
}
