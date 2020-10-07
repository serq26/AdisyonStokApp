using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdisyonProg.Entity
{
    public class SiparisIptal
    {
        public int ID { get; set; }
        public string Siparis { get; set; }
        public int SiparisAdedi { get; set; }
        public decimal SiparisFiyati { get; set; }
        public string StoktanDusuldumu { get; set; }
        public DateTime Tarih { get; set; }
        public decimal MaliyetFiyati { get; set; }
    }
}
