using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdisyonProg.Entity
{
    public class Masa
    {
        public int MasaID { get; set; }
        public string MasaAdi { get; set; }
        public string MasaRengi { get; set; }
        public string Bolum { get; set; }

        public override string ToString()
        {
            return MasaAdi;
        }
    }
}
