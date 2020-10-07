using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdisyonProg.Entity
{
    public class Urun
    {
        public int UrunID { get; set; }
        public string UrunAdi { get; set; }
        public string UrunAciklama { get; set; }
        public decimal UrunFiyati { get; set; }
        public decimal UrunStokAdedi { get; set; }
        public DateTime StokGirisTarihi { get; set; }
        public string BirimCinsi { get; set; }

        public override string ToString()
        {
            return UrunAdi;
        }
    }
}
