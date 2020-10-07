using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdisyonProg.Entity
{
    public class MenuKategori
    {
        public int ID { get; set; }
        public string KategoriAdi { get; set; }

        public override string ToString()
        {
            return KategoriAdi;
        }
    }
}
