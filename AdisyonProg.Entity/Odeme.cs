using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdisyonProg.Entity
{
    public class Odeme
    {
        public int ID { get; set; }
        public int MasaID { get; set; }
        public string MasaAdi { get; set; }
        public int AdisyonNo { get; set; }
        public string OdenenSiparis { get; set; }
        public decimal SiparisFiyati { get; set; }
        public int SiparisAdedi { get; set; }
        public DateTime Tarih { get; set; }
        public string OdemeTipi { get; set; }
        public int Iskonto { get; set; }
        public string MasaDurumu { get; set; }
    }
}
