using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace AdisyonProg.Entity
{
    public class Adisyon
    {
        public int AdisyonID { get; set; }
        public int AdisyonNo { get; set; }
        public string MasaAdi { get; set; }
        [DisplayName("Sipariş")]
        public string Siparis { get; set; }
        [DisplayName("Sipariş Adedi")]
        public int SiparisAdedi { get; set; }
        public decimal SiparisFiyati { get; set; }
        public DateTime SiparisTarihi { get; set; }
        public string AdisyonNotu { get; set; }
    }
}
