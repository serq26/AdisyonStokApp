using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdisyonProg.Entity
{
    public class Gunislemleri
    {
        public int ID { get; set; }
        public DateTime GunBasi { get; set; }
        public DateTime GunSonu { get; set; }
        public int AdisyonSayisi { get; set; }
        public int SatilanSiparisAdedi { get; set; }
        public decimal ToplamSatis { get; set; }
        public decimal Gelir { get; set; }
        public decimal Gider { get; set; }
        public decimal Nakit { get; set; }
        public decimal KrediKarti { get; set; }
        public decimal YemekCeki { get; set; }
        public decimal ikram { get; set; }

    }
}
