using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdisyonProg.Entity
{
    public class Garson
    {
        [DisplayName(" ")]
        public int GarsonID { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        [DisplayName("Kullanıcı Adı")]
        public string KullaniciAdi { get; set; }
        [DisplayName("Şifre")]
        public string Sifre { get; set; }
        public string Telefon { get; set; }
        public string Adres { get; set; }

        [DisplayName("Görevi")]
        public string Gorev { get; set; }
    }
}
