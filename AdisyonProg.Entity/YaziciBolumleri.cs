using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdisyonProg.Entity
{
    public class YaziciBolumleri
    {
        public int ID { get; set; }
        public string BolumAdi { get; set; }
        public string YaziciAdi { get; set; }
        public override string ToString()
        {
            return BolumAdi;
        }
    }
}
