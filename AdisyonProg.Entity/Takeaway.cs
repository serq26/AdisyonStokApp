using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdisyonProg.Entity
{
    public class Takeaway
    {
        public int TakeAwayID { get; set; }
        public int AdisyonNo { get; set; }
        public string Siparis { get; set; }
        public int SiparisAdedi { get; set; }
        public decimal SiparisFiyati { get; set; }
        public DateTime SiparisTarihi { get; set; }
        public string OdemeYapildimi { get; set; }
    }
}
