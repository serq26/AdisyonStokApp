using AdisyonProg.Core.Database;
using AdisyonProg.Core.Helper;
using AdisyonProg.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AdisyonProg.Core.Repository
{
    public class AdisyonRepository : Globalislemler, IDisposable
    {
        SqlCommand cmd;
        SqlDataReader reader;
        int ReturnValue;
        string ReturnString;
        decimal ReturnDecimal;
        DataAccessLayer DAL;
        DateTime ReturnDateTime;

        public AdisyonRepository()
        {
            DAL = new DataAccessLayer();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public int GarsonEkle(Garson garson)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("GarsonEkle");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Ad", SqlDbType.NVarChar).Value = garson.Ad;
                cmd.Parameters.Add("@Soyad", SqlDbType.NVarChar).Value = garson.Soyad;
                cmd.Parameters.Add("@KullaniciAdi", SqlDbType.NVarChar).Value = garson.KullaniciAdi;
                cmd.Parameters.Add("@Sifre", SqlDbType.NVarChar).Value = garson.Sifre;
                cmd.Parameters.Add("@Telefon", SqlDbType.NVarChar).Value = garson.Telefon;
                cmd.Parameters.Add("@Adres", SqlDbType.NVarChar).Value = garson.Adres;
                cmd.Parameters.Add("@Gorev", SqlDbType.NVarChar).Value = garson.Gorev;
                ReturnValue = DAL.Calistir(cmd);
            });
            return ReturnValue;
        }

        public Garson KullaniciGiris(string sifre)
        {
            Garson garson = new Garson();

            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("KullaniciGirisKontrol");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Sifre", SqlDbType.NVarChar).Value = sifre;
                reader = DAL.VeriGetir(cmd);
                while (reader.Read())
                {
                    garson = new Garson()
                    {
                        GarsonID = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                        Ad = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                        Soyad = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                        KullaniciAdi = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                        Sifre = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                        Telefon = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                        Adres = reader.IsDBNull(6) ? string.Empty : reader.GetString(6)
                    };
                }
                reader.Close();
                DAL.con.Close();
            });
            return garson;
        }

        public List<Garson> PersonelleriGetir()
        {
            List<Garson> personeller = new List<Garson>();

            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("PersonelleriGetir");
                cmd.CommandType = CommandType.StoredProcedure;
                reader = DAL.VeriGetir(cmd);

                while (reader.Read())
                {
                    personeller.Add(new Garson
                    {
                        GarsonID = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                        Ad = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                        Soyad = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                        KullaniciAdi = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                        Sifre = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                        Telefon = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                        Adres = reader.IsDBNull(6) ? string.Empty : reader.GetString(6),
                        Gorev = reader.IsDBNull(7) ? string.Empty : reader.GetString(7)
                    });
                }
                reader.Close();
                DAL.con.Close();
            });
            return personeller;
        }

        public int MenuyeUrunEkle(Menu menu)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("MenuyeUrunEkle");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UrunAdi", SqlDbType.NVarChar).Value = menu.UrunAdi;
                cmd.Parameters.Add("@UrunAciklama", SqlDbType.NVarChar).Value = menu.UrunAciklama;
                cmd.Parameters.Add("@UrunFiyati", SqlDbType.Decimal).Value = menu.UrunFiyati;
                cmd.Parameters.Add("@UrunKategori", SqlDbType.NVarChar).Value = menu.UrunKategori;
                cmd.Parameters.Add("@MaliyetFiyati", SqlDbType.Decimal).Value = menu.MaliyetFiyati;
                cmd.Parameters.Add("@SiparisCikicakYer", SqlDbType.NVarChar).Value = menu.SiparisCikicakYer;
                ReturnValue = DAL.Calistir(cmd);
            });
            return ReturnValue;
        }

        public int StokUrunEkle(Urun urun)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("StokUrunEkle");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UrunAdi", SqlDbType.NVarChar).Value = urun.UrunAdi;
                cmd.Parameters.Add("@UrunFiyati", SqlDbType.Decimal).Value = urun.UrunFiyati;
                cmd.Parameters.Add("@UrunAciklama", SqlDbType.NVarChar).Value = urun.UrunAciklama;
                cmd.Parameters.Add("@UrunAdedi", SqlDbType.Decimal).Value = urun.UrunStokAdedi;
                cmd.Parameters.Add("@StokGirisTarihi", SqlDbType.DateTime).Value = urun.StokGirisTarihi;
                cmd.Parameters.Add("@BirimCinsi", SqlDbType.NVarChar).Value = urun.BirimCinsi;
                ReturnValue = DAL.Calistir(cmd);
            });
            return ReturnValue;
        }

        public int MasaEkle(Masa masa)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("MasaEkle");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@MasaAdi", SqlDbType.NVarChar).Value = masa.MasaAdi;
                cmd.Parameters.Add("@MasaRengi", SqlDbType.NVarChar).Value = masa.MasaRengi;
                cmd.Parameters.Add("@Bolum", SqlDbType.NVarChar).Value = masa.Bolum;
                ReturnValue = DAL.Calistir(cmd);
            });
            return ReturnValue;
        }

        public int MasaSil(string masaAdi, string bolum)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("MasaSil");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@MasaAdi", SqlDbType.NVarChar).Value = masaAdi;
                cmd.Parameters.Add("@Bolum", SqlDbType.NVarChar).Value = bolum;
                ReturnValue = DAL.Calistir(cmd);
            });
            return ReturnValue;
        }

        public int BolumSil(string bolumAdi)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("BolumSil");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@BolumAdi", SqlDbType.NVarChar).Value = bolumAdi;
                ReturnValue = DAL.Calistir(cmd);
            });
            return ReturnValue;
        }

        public List<Masa> MasalariGetir()
        {
            List<Masa> Masalar = new List<Masa>();

            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("TumMasalar");
                cmd.CommandType = CommandType.StoredProcedure;
                reader = DAL.VeriGetir(cmd);

                while (reader.Read())
                {
                    Masalar.Add(new Masa
                    {
                        MasaID = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                        MasaAdi = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                        MasaRengi = reader.IsDBNull(2) ? string.Empty : reader.GetString(2)
                    });
                }
                reader.Close();
                DAL.con.Close();
            });
            return Masalar;
        }

        public List<Masa> BolumeGoreMasalariGetir(string bolum)
        {
            List<Masa> Masalar = new List<Masa>();

            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("BolumeGoreMasalariGetir");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Bolum", SqlDbType.NVarChar).Value = bolum;
                reader = DAL.VeriGetir(cmd);

                while (reader.Read())
                {
                    Masalar.Add(new Masa
                    {
                        MasaID = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                        MasaAdi = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                        MasaRengi = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                        Bolum = reader.IsDBNull(3) ? string.Empty : reader.GetString(3)
                    });
                }
                reader.Close();
                DAL.con.Close();
            });
            return Masalar;
        }

        public List<Menu> MenudekiTumUrunleriGetir()
        {
            List<Menu> TumUrunler = new List<Menu>();
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("MenudekiTumUrunler");
                cmd.CommandType = CommandType.StoredProcedure;
                reader = DAL.VeriGetir(cmd);

                while (reader.Read())
                {
                    TumUrunler.Add(new Menu
                    {
                        MenuUrunID = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                        UrunID = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                        UrunAdi = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                        UrunAciklama = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                        UrunFiyati = reader.IsDBNull(4) ? 0 : reader.GetDecimal(4),
                        UrunKategori = reader.IsDBNull(5) ? string.Empty : reader.GetString(5)
                    });
                }
                reader.Close();
                DAL.con.Close();
            });
            return TumUrunler;
        }

        public List<Menu> KategoriyeGoreUrunGetir(string UrunKategori)
        {
            List<Menu> Urunler = new List<Menu>();

            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("KategoriyeGoreUrunGetir");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UrunKategori", SqlDbType.NVarChar).Value = UrunKategori;
                reader = DAL.VeriGetir(cmd);

                while (reader.Read())
                {
                    Urunler.Add(new Menu
                    {
                        MenuUrunID = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                        UrunID = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                        UrunAdi = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                        UrunAciklama = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                        UrunFiyati = reader.IsDBNull(4) ? 0 : reader.GetDecimal(4),
                        UrunKategori = reader.IsDBNull(5) ? string.Empty : reader.GetString(5)
                    });
                }
                reader.Close();
                DAL.con.Close();
            });
            return Urunler;
        }

        public Menu MenudekiUrunuGetir(int Id, string urunAdi)
        {
            Menu Urun = new Menu();

            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("MenudekiUrunuGetir");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@MenuUrunID", SqlDbType.Int).Value = Id;
                cmd.Parameters.Add("@UrunAdi", SqlDbType.NVarChar).Value = urunAdi;
                reader = DAL.VeriGetir(cmd);

                while (reader.Read())
                {
                    Urun.UrunAdi = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                    Urun.UrunAciklama = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                    Urun.UrunFiyati = reader.IsDBNull(2) ? 0 : reader.GetDecimal(2);
                    Urun.UrunKategori = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                    Urun.MaliyetFiyati = reader.IsDBNull(4) ? 0 : reader.GetDecimal(4);
                }
                reader.Close();
                DAL.con.Close();
            });
            return Urun;
        }

        public int MenuUrunId(string urunAdi)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("MenuUrunId");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UrunAdi", SqlDbType.NVarChar).Value = urunAdi;
                ReturnValue = DAL.CalistirINT(cmd);
            });
            return ReturnValue;
        }

        public int SiparisEkle(Adisyon adisyon)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("SiparisEkle2");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AdisyonID", SqlDbType.Int).Value = adisyon.AdisyonID;
                cmd.Parameters.Add("@AdisyonNo", SqlDbType.Int).Value = adisyon.AdisyonNo;
                cmd.Parameters.Add("@MasaAdi", SqlDbType.NVarChar).Value = adisyon.MasaAdi;
                cmd.Parameters.Add("@Siparis", SqlDbType.NVarChar).Value = adisyon.Siparis;
                cmd.Parameters.Add("@SiparisAdedi", SqlDbType.NVarChar).Value = adisyon.SiparisAdedi;
                cmd.Parameters.Add("@SiparisFiyati", SqlDbType.Decimal).Value = adisyon.SiparisFiyati;
                cmd.Parameters.Add("@SiparisTarihi", SqlDbType.DateTime).Value = adisyon.SiparisTarihi;
                cmd.Parameters.Add("@AdisyonNotu", SqlDbType.NVarChar).Value = adisyon.AdisyonNotu;
                ReturnValue = DAL.Calistir(cmd);
            });
            return ReturnValue;
        }

        public int SiparisNotuEkle(int adisyonId, int adisyonNo, string siparis, string not)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("SiparisNotuEkle");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AdisyonID", SqlDbType.Int).Value = adisyonId;
                cmd.Parameters.Add("@AdisyonNo", SqlDbType.Int).Value = adisyonNo;
                cmd.Parameters.Add("@Siparis", SqlDbType.NVarChar).Value = siparis;
                cmd.Parameters.Add("@AdisyonNotu", SqlDbType.NVarChar).Value = not;
                ReturnValue = DAL.Calistir(cmd);
            });
            return ReturnValue;
        }

        public List<Adisyon> MasadakiSiparisleriGetir(int AdisyonId)
        {
            List<Adisyon> siparisler = new List<Adisyon>();

            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("MasadakiSiparisleriGetir");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AdisyonID", SqlDbType.Int).Value = AdisyonId;
                reader = DAL.VeriGetir(cmd);

                while (reader.Read())
                {
                    siparisler.Add(new Adisyon
                    {
                        AdisyonID = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                        AdisyonNo = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                        MasaAdi = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                        Siparis = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                        SiparisAdedi = reader.IsDBNull(4) ? 0 : reader.GetInt32(4),
                        SiparisFiyati = reader.IsDBNull(5) ? 0 : reader.GetDecimal(5),
                        SiparisTarihi = reader.IsDBNull(6) ? DateTime.MinValue : reader.GetDateTime(6)
                    });
                }
                reader.Close();
                DAL.con.Close();
            });
            return siparisler;
        }

        public int SiparisSil(int adisyonNo, string Siparis)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("SiparisSil");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AdisyonNo", SqlDbType.Int).Value = adisyonNo;
                cmd.Parameters.Add("@Siparis", SqlDbType.NVarChar).Value = Siparis;
                ReturnValue = DAL.Calistir(cmd);
            });
            return ReturnValue;
        }

        public int TakeAwaySiparisSil(int adisyonNo, string Siparis)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("TakeAwaySiparisSil");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AdisyonNo", SqlDbType.Int).Value = adisyonNo;
                cmd.Parameters.Add("@Siparis", SqlDbType.NVarChar).Value = Siparis;
                ReturnValue = DAL.Calistir(cmd);
            });
            return ReturnValue;
        }

        public int SiparisAdetSil(int adisyonId, int adisyonNo, string siparis, int siparisAdedi, decimal siparisFiyati)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("SiparisAdetSil2");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AdisyonID", SqlDbType.Int).Value = adisyonId;
                cmd.Parameters.Add("@AdisyonNo", SqlDbType.Int).Value = adisyonNo;
                cmd.Parameters.Add("@Siparis", SqlDbType.NVarChar).Value = siparis;
                cmd.Parameters.Add("@SiparisAdedi", SqlDbType.Int).Value = siparisAdedi;
                cmd.Parameters.Add("@SiparisFiyati", SqlDbType.Decimal).Value = siparisFiyati;
                ReturnValue = DAL.Calistir(cmd);
            });
            return ReturnValue;
        }

        public int MasaDurumunuGuncelle(int MasaId, string MasaDurumu, DateTime masaSaati, int adisyonNo)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("MasaDurumunuGuncelle2");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@MasaID", SqlDbType.Int).Value = MasaId;
                cmd.Parameters.Add("@MasaRengi", SqlDbType.NVarChar).Value = MasaDurumu;
                cmd.Parameters.Add("@MasaSaati", SqlDbType.DateTime).Value = masaSaati;
                cmd.Parameters.Add("@AdisyonNo", SqlDbType.Int).Value = adisyonNo;
                ReturnValue = DAL.Calistir(cmd);
            });
            return ReturnValue;
        }

        public string MasaDurumunuGetir(int MasaId)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("MasaDurumunuGetir");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@MasaID", SqlDbType.Int).Value = MasaId;
                ReturnString = DAL.CalistirString(cmd);
            });
            return ReturnString;
        }

        public int AdisyonNoGuncelle(int id)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("AdisyonNoGuncelle");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;
                ReturnValue = DAL.Calistir(cmd);
            });
            return ReturnValue;
        }

        public int StokGuncelle(string urunAdi, decimal urunFiyati, decimal urunAdedi, DateTime stokGirisTarihi, string birimCinsi, string aciklama)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("StokGuncelle");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UrunAdi", SqlDbType.NVarChar).Value = urunAdi;
                cmd.Parameters.Add("@UrunFiyati", SqlDbType.Decimal).Value = urunFiyati;
                cmd.Parameters.Add("@UrunAdedi", SqlDbType.Decimal).Value = urunAdedi;
                cmd.Parameters.Add("@StokGirisTarihi", SqlDbType.DateTime).Value = stokGirisTarihi;
                cmd.Parameters.Add("@BirimCinsi", SqlDbType.NVarChar).Value = birimCinsi;
                cmd.Parameters.Add("@UrunAciklama", SqlDbType.NVarChar).Value = aciklama;
                ReturnValue = DAL.Calistir(cmd);
            });
            return ReturnValue;
        }

        public int SıradakiAdisyonNo()
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("SıradakiAdisyonNo");
                cmd.CommandType = CommandType.StoredProcedure;
                ReturnValue = DAL.CalistirINT(cmd);
            });
            return ReturnValue;
        }

        public int MasaAdisyonNumarasiniGetir(string masaAdi)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("MasaAdisyonNumarasiniGetir");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@MasaAdi", SqlDbType.NVarChar).Value = masaAdi;
                ReturnValue = DAL.CalistirINT(cmd);
            });
            return ReturnValue;
        }

        public int MasaAdisyonNo(int masaId)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("MasaAdisyonNo");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@MasaID", SqlDbType.Int).Value = masaId;
                ReturnValue = DAL.CalistirINT(cmd);
            });
            return ReturnValue;
        }

        public int MasaAktar(int adisyonId, int adisyonNo, string masaAdi, int adisyonId2)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("MasaAktar");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AdisyonID", SqlDbType.Int).Value = adisyonId;
                cmd.Parameters.Add("@AdisyonNo", SqlDbType.Int).Value = adisyonNo;
                cmd.Parameters.Add("@MasaAdi", SqlDbType.NVarChar).Value = masaAdi;
                cmd.Parameters.Add("@AdisyonID2", SqlDbType.Int).Value = adisyonId2;
                ReturnValue = DAL.Calistir(cmd);
            });
            return ReturnValue;
        }

        public List<Urun> StokGetir()
        {
            List<Urun> Stoklar = new List<Urun>();

            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("StokGetir");
                cmd.CommandType = CommandType.StoredProcedure;
                reader = DAL.VeriGetir(cmd);

                while (reader.Read())
                {
                    Stoklar.Add(new Urun
                    {
                        UrunID = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                        UrunAdi = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                        UrunFiyati = reader.IsDBNull(2) ? 0 : reader.GetDecimal(2),
                        UrunAciklama = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                        UrunStokAdedi = reader.IsDBNull(4) ? 0 : reader.GetDecimal(4),
                        StokGirisTarihi = reader.IsDBNull(5) ? DateTime.MinValue : reader.GetDateTime(5),
                        BirimCinsi = reader.IsDBNull(6) ? string.Empty : reader.GetString(6)
                    });
                }
                reader.Close();
                DAL.con.Close();
            });
            return Stoklar;
        }

        public List<Urun> StokGetir(string urunAdi)
        {
            List<Urun> Stoklar = new List<Urun>();

            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("StoktaUrunAra");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UrunAdi", SqlDbType.NVarChar).Value = urunAdi;
                reader = DAL.VeriGetir(cmd);

                while (reader.Read())
                {
                    Stoklar.Add(new Urun
                    {
                        UrunID = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                        UrunAdi = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                        UrunFiyati = reader.IsDBNull(2) ? 0 : reader.GetDecimal(2),
                        UrunAciklama = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                        UrunStokAdedi = reader.IsDBNull(4) ? 0 : reader.GetDecimal(4),
                        StokGirisTarihi = reader.IsDBNull(5) ? DateTime.MinValue : reader.GetDateTime(5),
                        BirimCinsi = reader.IsDBNull(6) ? string.Empty : reader.GetString(6)
                    });
                }
                reader.Close();
                DAL.con.Close();
            });
            return Stoklar;
        }

        public decimal MasaHesapDurumu(int adisyonId, int adisyonNo)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("MasaHesapDurumu");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AdisyonID", SqlDbType.Int).Value = adisyonId;
                cmd.Parameters.Add("@AdisyonNo", SqlDbType.Int).Value = adisyonNo;
                ReturnDecimal = DAL.CalistirDecimal(cmd);
            });
            return ReturnDecimal;
        }

        public decimal UrunFiyatiGetir(string urunAdi)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("UrunFiyatiGetir");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UrunAdi", SqlDbType.NVarChar).Value = urunAdi;
                ReturnDecimal = DAL.CalistirDecimal(cmd);
            });
            return ReturnDecimal;
        }

        public decimal UrunMaliyetFiyatiGetir(string urunAdi)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("UrunMaliyetFiyatiGetir");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UrunAdi", SqlDbType.NVarChar).Value = urunAdi;
                ReturnDecimal = DAL.CalistirDecimal(cmd);
            });
            return ReturnDecimal;
        }

        public DateTime MasaSaatiHesapla(int masaId)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("MasaSaatiHesapla");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@MasaID", SqlDbType.Int).Value = masaId;
                ReturnDateTime = DAL.CalistirDateTime(cmd);
            });
            return ReturnDateTime;
        }

        public List<Adisyon> OdemeDetay(int adisyonId)
        {
            List<Adisyon> siparisler = new List<Adisyon>();

            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("OdemeDetay2");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AdisyonID", SqlDbType.Int).Value = adisyonId;
                reader = DAL.VeriGetir(cmd);

                while (reader.Read())
                {
                    siparisler.Add(new Adisyon
                    {
                        Siparis = reader.IsDBNull(0) ? string.Empty : reader.GetString(0),
                        SiparisAdedi = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                        SiparisFiyati = reader.IsDBNull(2) ? 0 : reader.GetDecimal(2)
                    });
                }
                reader.Close();
                DAL.con.Close();
            });
            return siparisler;
        }

        public int MenuUrunIDGetir(string urunAdi)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("MenuUrunIDGetir");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UrunAdi", SqlDbType.NVarChar).Value = urunAdi;
                ReturnValue = DAL.CalistirINT(cmd);
            });
            return ReturnValue;
        }

        public int StokDusulucekUrunEkle(int menuUrunId, string urunAdi, decimal adet, string birim)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("StokDusulucekUrunEkle");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@MenuUrunID", SqlDbType.Int).Value = menuUrunId;
                cmd.Parameters.Add("@UrunAdi", SqlDbType.NVarChar).Value = urunAdi;
                cmd.Parameters.Add("@UrunAdedi", SqlDbType.Decimal).Value = adet;
                cmd.Parameters.Add("@BirimCinsi", SqlDbType.NChar).Value = birim;
                ReturnValue = DAL.Calistir(cmd);
            });
            return ReturnValue;
        }

        public string StokBirimCinsiKontrol(string urunAdi)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("StokBirimCinsiKontrol");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UrunAdi", SqlDbType.NVarChar).Value = urunAdi;
                ReturnString = DAL.CalistirString(cmd);
            });
            return ReturnString;
        }

        public int StoktanEksiltme(string urunAdi, decimal adet)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("StoktanEksiltme");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UrunAdi", SqlDbType.NVarChar).Value = urunAdi;
                cmd.Parameters.Add("@UrunAdedi", SqlDbType.Decimal).Value = adet;
                ReturnValue = DAL.Calistir(cmd);
            });
            return ReturnValue;
        }

        public int OdemeEkle(Odeme odeme)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("OdemeEkle");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@MasaID", SqlDbType.Int).Value = odeme.MasaID;
                cmd.Parameters.Add("@MasaAdi", SqlDbType.NVarChar).Value = odeme.MasaAdi;
                cmd.Parameters.Add("@AdisyonNo", SqlDbType.Int).Value = odeme.AdisyonNo;
                cmd.Parameters.Add("@OdenenSiparis", SqlDbType.NVarChar).Value = odeme.OdenenSiparis;
                cmd.Parameters.Add("@SiparisFiyati", SqlDbType.Decimal).Value = odeme.SiparisFiyati;
                cmd.Parameters.Add("@SiparisAdedi", SqlDbType.Int).Value = odeme.SiparisAdedi;
                cmd.Parameters.Add("@Tarih", SqlDbType.DateTime).Value = odeme.Tarih;
                cmd.Parameters.Add("@OdemeTipi", SqlDbType.NVarChar).Value = odeme.OdemeTipi;
                cmd.Parameters.Add("@Iskonto", SqlDbType.Int).Value = odeme.Iskonto;
                cmd.Parameters.Add("@MasaDurumu", SqlDbType.NVarChar).Value = odeme.MasaDurumu;
                ReturnValue = DAL.Calistir(cmd);
            });
            return ReturnValue;
        }

        public List<StoktanDusulecekUrunler> StoktanDusulecekUrunleriGetir(int id)
        {
            List<StoktanDusulecekUrunler> stoktanDusulecekUrunler = new List<StoktanDusulecekUrunler>();

            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("StokDusulecekUrunleriGetir");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@MenuUrunID", SqlDbType.Int).Value = id;
                reader = DAL.VeriGetir(cmd);

                while (reader.Read())
                {
                    stoktanDusulecekUrunler.Add(new StoktanDusulecekUrunler
                    {
                        UrunAdi = reader.IsDBNull(0) ? string.Empty : reader.GetString(0),
                        UrunAdedi = reader.IsDBNull(1) ? 0 : reader.GetDecimal(1),
                        BirimCinsi = reader.IsDBNull(2) ? string.Empty : reader.GetString(2)
                    });
                }
                reader.Close();
                DAL.con.Close();
            });
            return stoktanDusulecekUrunler;
        }

        public int StokaGeriEkle(string urunAdi, decimal urunAdedi)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("StokaGeriEkle");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UrunAdi", SqlDbType.NVarChar).Value = urunAdi;
                cmd.Parameters.Add("@UrunAdedi", SqlDbType.Decimal).Value = urunAdedi;
                ReturnValue = DAL.Calistir(cmd);
            });
            return ReturnValue;
        }

        public int TakeAwayEkle(Takeaway takeaway)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("TakeAwayEkle");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AdisyonNo", SqlDbType.Int).Value = takeaway.AdisyonNo;
                cmd.Parameters.Add("@Siparis", SqlDbType.NVarChar).Value = takeaway.Siparis;
                cmd.Parameters.Add("@SiparisAdedi", SqlDbType.Int).Value = takeaway.SiparisAdedi;
                cmd.Parameters.Add("@SiparisFiyati", SqlDbType.Decimal).Value = takeaway.SiparisFiyati;
                cmd.Parameters.Add("@SiparisTarihi", SqlDbType.DateTime).Value = takeaway.SiparisTarihi;
                cmd.Parameters.Add("@OdemeYapildimi", SqlDbType.NVarChar).Value = takeaway.OdemeYapildimi;
                ReturnValue = DAL.Calistir(cmd);
            });
            return ReturnValue;
        }

        public List<Takeaway> TakeAwaySiparisleriGetir(int adisyonNo)
        {
            List<Takeaway> siparisler = new List<Takeaway>();

            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("TakeAwaySiparisleriGetir");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AdisyonNo", SqlDbType.Int).Value = adisyonNo;
                reader = DAL.VeriGetir(cmd);

                while (reader.Read())
                {
                    siparisler.Add(new Takeaway
                    {
                        Siparis = reader.IsDBNull(0) ? string.Empty : reader.GetString(0),
                        SiparisAdedi = reader.IsDBNull(1) ? 0 : reader.GetInt32(1)
                    });
                }
                reader.Close();
                DAL.con.Close();
            });
            return siparisler;
        }

        public int MasaIDGetir(string masaAdi)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("MasaIDGetir");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@MasaAdi", SqlDbType.NVarChar).Value = masaAdi;
                ReturnValue = DAL.CalistirINT(cmd);
            });
            return ReturnValue;
        }

        public List<Takeaway> TakeAwayOdemeDetay(int adisyonNo)
        {
            List<Takeaway> siparisler = new List<Takeaway>();

            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("TakeAwayOdemeDetay");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AdisyonNo", SqlDbType.Int).Value = adisyonNo;
                reader = DAL.VeriGetir(cmd);

                while (reader.Read())
                {
                    siparisler.Add(new Takeaway
                    {
                        Siparis = reader.IsDBNull(0) ? string.Empty : reader.GetString(0),
                        SiparisAdedi = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                        SiparisFiyati = reader.IsDBNull(2) ? 0 : reader.GetDecimal(2),
                        OdemeYapildimi = reader.IsDBNull(3) ? string.Empty : reader.GetString(3)
                    });
                }
                reader.Close();
                DAL.con.Close();
            });
            return siparisler;
        }

        public int TakeAwayOdemeGuncelle(int adisyonNo, string odemeYapildimi)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("TakeAwayOdemeGuncelle");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AdisyonNo", SqlDbType.Int).Value = adisyonNo;
                cmd.Parameters.Add("@OdemeYapildimi", SqlDbType.NVarChar).Value = odemeYapildimi;
                ReturnValue = DAL.Calistir(cmd);
            });
            return ReturnValue;
        }

        public int StoktanUrunSil(string urun)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("StoktanUrunSil");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UrunAdi", SqlDbType.NVarChar).Value = urun;
                ReturnValue = DAL.Calistir(cmd);
            });
            return ReturnValue;
        }

        public List<Urun> StokBitenUrunler()
        {
            List<Urun> bitenUrunler = new List<Urun>();

            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("StokBitenUrunler");
                cmd.CommandType = CommandType.StoredProcedure;
                reader = DAL.VeriGetir(cmd);

                while (reader.Read())
                {
                    bitenUrunler.Add(new Urun
                    {
                        UrunID = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                        UrunAdi = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                        UrunFiyati = reader.IsDBNull(2) ? 0 : reader.GetDecimal(2),
                        UrunAciklama = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                        UrunStokAdedi = reader.IsDBNull(4) ? 0 : reader.GetDecimal(4),
                        StokGirisTarihi = reader.IsDBNull(5) ? DateTime.MinValue : reader.GetDateTime(5),
                        BirimCinsi = reader.IsDBNull(6) ? string.Empty : reader.GetString(6)
                    });
                }
                reader.Close();
                DAL.con.Close();
            });
            return bitenUrunler;
        }

        public List<Odeme> OdenmisUrunleriGetir(int adisyonNo, int masaId, string masaDurumu)
        {
            List<Odeme> odenmisUrunler = new List<Odeme>();

            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("OdenmisUrunleriGetir");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AdisyonNo", SqlDbType.Int).Value = adisyonNo;
                cmd.Parameters.Add("@MasaID", SqlDbType.Int).Value = masaId;
                cmd.Parameters.Add("@MasaDurumu", SqlDbType.NVarChar).Value = masaDurumu;
                reader = DAL.VeriGetir(cmd);

                while (reader.Read())
                {
                    odenmisUrunler.Add(new Odeme
                    {
                        Tarih = reader.IsDBNull(0) ? DateTime.MinValue : reader.GetDateTime(0),
                        OdenenSiparis = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                        OdemeTipi = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                        SiparisAdedi = reader.IsDBNull(3) ? 0 : reader.GetInt32(3),
                        SiparisFiyati = reader.IsDBNull(4) ? 0 : reader.GetDecimal(4),
                        Iskonto = reader.IsDBNull(5) ? 0 : reader.GetInt32(5)
                    });
                }
                reader.Close();
                DAL.con.Close();
            });
            return odenmisUrunler;
        }

        public List<Odeme> MasalaraGoreSiparisler(string masaAdi)
        {
            List<Odeme> siparisler = new List<Odeme>();
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("MasalaraGoreSiparisler");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@MasaAdi", SqlDbType.NVarChar).Value = masaAdi;
                reader = DAL.VeriGetir(cmd);

                while (reader.Read())
                {
                    siparisler.Add(new Odeme
                    {
                        MasaAdi = reader.IsDBNull(0) ? string.Empty : reader.GetString(0),
                        OdenenSiparis = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                        SiparisAdedi = reader.IsDBNull(2) ? 0 : reader.GetInt32(2),
                        SiparisFiyati = reader.IsDBNull(3) ? 0 : reader.GetDecimal(3),
                        OdemeTipi = reader.IsDBNull(4) ? string.Empty : reader.GetString(4)
                    });
                }
                reader.Close();
                DAL.con.Close();
            });
            return siparisler;
        }

        public List<Odeme> SiparislereGoreSiparisler()
        {
            List<Odeme> siparisler = new List<Odeme>();
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("SiparislereGoreSiparisler");
                cmd.CommandType = CommandType.StoredProcedure;
                reader = DAL.VeriGetir(cmd);

                while (reader.Read())
                {
                    siparisler.Add(new Odeme
                    {
                        OdenenSiparis = reader.IsDBNull(0) ? string.Empty : reader.GetString(0),
                        SiparisAdedi = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                        SiparisFiyati = reader.IsDBNull(2) ? 0 : reader.GetDecimal(2),
                    });
                }
                reader.Close();
                DAL.con.Close();
            });
            return siparisler;
        }

        public List<Odeme> TarihlereGoreSiparisler(DateTime tarih1, DateTime tarih2)
        {
            List<Odeme> siparisler = new List<Odeme>();
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("TarihlereGoreSiparisler");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Tarih1", SqlDbType.DateTime).Value = tarih1;
                cmd.Parameters.Add("@Tarih2", SqlDbType.DateTime).Value = tarih2;
                reader = DAL.VeriGetir(cmd);

                while (reader.Read())
                {
                    siparisler.Add(new Odeme
                    {
                        OdenenSiparis = reader.IsDBNull(0) ? string.Empty : reader.GetString(0),
                        SiparisAdedi = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                        SiparisFiyati = reader.IsDBNull(2) ? 0 : reader.GetDecimal(2),
                    });
                }
                reader.Close();
                DAL.con.Close();
            });
            return siparisler;
        }
        public List<Odeme> OdenmisAdisyonlariGetir(DateTime tarih1, DateTime tarih2)
        {
            List<Odeme> adisyonlar = new List<Odeme>();
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("OdenmisAdisyonlariGetir");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Tarih1", SqlDbType.DateTime).Value = tarih1;
                cmd.Parameters.Add("@Tarih2", SqlDbType.DateTime).Value = tarih2;
                reader = DAL.VeriGetir(cmd);

                while (reader.Read())
                {
                    adisyonlar.Add(new Odeme
                    {
                        AdisyonNo = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                        SiparisFiyati = reader.IsDBNull(1) ? 0 : reader.GetDecimal(1),
                        SiparisAdedi = reader.IsDBNull(2) ? 0 : reader.GetInt32(2),
                        MasaAdi = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                        Tarih = reader.IsDBNull(4) ? DateTime.MinValue : reader.GetDateTime(4),
                    });
                }
                reader.Close();
                DAL.con.Close();
            });
            return adisyonlar;
        }

        public List<Adisyon> AcikAdisyonlariGetir()
        {
            List<Adisyon> adisyonlar = new List<Adisyon>();

            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("AcikAdisyonlariGetir");
                cmd.CommandType = CommandType.StoredProcedure;
                reader = DAL.VeriGetir(cmd);

                while (reader.Read())
                {
                    adisyonlar.Add(new Adisyon
                    {
                        AdisyonNo = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                        MasaAdi = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                        SiparisFiyati = reader.IsDBNull(2) ? 0 : reader.GetDecimal(2),
                        SiparisAdedi = reader.IsDBNull(3) ? 0 : reader.GetInt32(3),
                        SiparisTarihi = reader.IsDBNull(4) ? DateTime.MinValue : reader.GetDateTime(4)
                    });
                }
                reader.Close();
                DAL.con.Close();
            });
            return adisyonlar;
        }

        public List<Odeme> OdenmisAdisyondakiSiparisler(int adisyonNo)
        {
            List<Odeme> siparisler = new List<Odeme>();
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("OdenmisAdisyondakiSiparisler");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AdisyonNo", SqlDbType.Int).Value = adisyonNo;
                reader = DAL.VeriGetir(cmd);

                while (reader.Read())
                {
                    siparisler.Add(new Odeme
                    {
                        OdenenSiparis = reader.IsDBNull(0) ? string.Empty : reader.GetString(0),
                        SiparisAdedi = reader.IsDBNull(1) ? 0 : reader.GetInt32(1)
                    });
                }
                reader.Close();
                DAL.con.Close();
            });
            return siparisler;
        }

        public List<Adisyon> AcikAdisyondakiSiparisler(int adisyonNo)
        {
            List<Adisyon> siparisler = new List<Adisyon>();
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("AcikAdisyondakiSiparisler");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@AdisyonNo", SqlDbType.Int).Value = adisyonNo;
                reader = DAL.VeriGetir(cmd);

                while (reader.Read())
                {
                    siparisler.Add(new Adisyon
                    {
                        Siparis = reader.IsDBNull(0) ? string.Empty : reader.GetString(0),
                        SiparisAdedi = reader.IsDBNull(1) ? 0 : reader.GetInt32(1)
                    });
                }
                reader.Close();
                DAL.con.Close();
            });
            return siparisler;
        }

        public List<Takeaway> TakeAwayRapor()
        {
            List<Takeaway> takeaways = new List<Takeaway>();

            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("TakeAwayRapor");
                cmd.CommandType = CommandType.StoredProcedure;
                reader = DAL.VeriGetir(cmd);

                while (reader.Read())
                {
                    takeaways.Add(new Takeaway
                    {
                        TakeAwayID = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                        AdisyonNo = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                        Siparis = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                        SiparisAdedi = reader.IsDBNull(3) ? 0 : reader.GetInt32(3),
                        SiparisFiyati = reader.IsDBNull(4) ? 0 : reader.GetDecimal(4),
                        SiparisTarihi = reader.IsDBNull(5) ? DateTime.MinValue : reader.GetDateTime(5)
                    });
                }
                reader.Close();
                DAL.con.Close();
            });
            return takeaways;
        }

        public List<Takeaway> TakeAwayRaporTariheGore(DateTime tarih1, DateTime tarih2)
        {
            List<Takeaway> takeaways = new List<Takeaway>();

            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("TakeAwayRaporTariheGore");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Tarih1", SqlDbType.DateTime).Value = tarih1;
                cmd.Parameters.Add("@Tarih2", SqlDbType.DateTime).Value = tarih2;
                reader = DAL.VeriGetir(cmd);

                while (reader.Read())
                {
                    takeaways.Add(new Takeaway
                    {
                        TakeAwayID = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                        AdisyonNo = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                        Siparis = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                        SiparisAdedi = reader.IsDBNull(3) ? 0 : reader.GetInt32(3),
                        SiparisFiyati = reader.IsDBNull(4) ? 0 : reader.GetDecimal(4),
                        SiparisTarihi = reader.IsDBNull(5) ? DateTime.MinValue : reader.GetDateTime(5)
                    });
                }
                reader.Close();
                DAL.con.Close();
            });
            return takeaways;
        }

        public int GunBasiSonuEkleme(DateTime gunBasi, DateTime gunSonu)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("GunBasiSonuEkleme");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GunBasi", SqlDbType.DateTime).Value = gunBasi;
                cmd.Parameters.Add("@GunSonu", SqlDbType.DateTime).Value = gunSonu;
                ReturnValue = DAL.Calistir(cmd);
            });
            return ReturnValue;
        }

        public int GunBasiYapildimiGuncelle(string gunBasi, DateTime tarih)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("GunBasiYapildimiGuncelle");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GunBasiYapildi", SqlDbType.NVarChar).Value = gunBasi;
                cmd.Parameters.Add("@GunBasi", SqlDbType.DateTime).Value = tarih;
                ReturnValue = DAL.Calistir(cmd);
            });
            return ReturnValue;
        }

        public string GunBasiYapildimiKontrol()
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("GunBasiYapildimiKontrol");
                cmd.CommandType = CommandType.StoredProcedure;
                ReturnString = DAL.CalistirString(cmd);
            });
            return ReturnString;
        }
        public DateTime GunBasiTarih()
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("GunBasiTarih");
                cmd.CommandType = CommandType.StoredProcedure;
                ReturnDateTime = DAL.CalistirDateTime(cmd);
            });
            return ReturnDateTime;
        }

        public List<GunBasiSonu> GunBasiSonuAl(DateTime gunBasi)
        {
            List<GunBasiSonu> tarihler = new List<GunBasiSonu>();

            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("GunBasiSonuAl");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GunBasi", SqlDbType.DateTime).Value = gunBasi;
                reader = DAL.VeriGetir(cmd);

                while (reader.Read())
                {
                    tarihler.Add(new GunBasiSonu
                    {
                        GunBasi = reader.IsDBNull(0) ? DateTime.MinValue : reader.GetDateTime(0),
                        GunSonu = reader.IsDBNull(1) ? DateTime.MinValue : reader.GetDateTime(1)
                    });
                }
                reader.Close();
                DAL.con.Close();
            });
            return tarihler;
        }

        public int AdisyonSayisi(DateTime gunbasi, DateTime gunsonu)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("AdisyonSayisi");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GunBasi", SqlDbType.DateTime).Value = gunbasi;
                cmd.Parameters.Add("@GunSonu", SqlDbType.DateTime).Value = gunsonu;
                ReturnValue = DAL.CalistirINT(cmd);
            });
            return ReturnValue;
        }

        public int SatilanSiparisAdedi(DateTime gunbasi, DateTime gunsonu)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("SatilanSiparisAdedi");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GunBasi", SqlDbType.DateTime).Value = gunbasi;
                cmd.Parameters.Add("@GunSonu", SqlDbType.DateTime).Value = gunsonu;
                ReturnValue = DAL.CalistirINT(cmd);
            });
            return ReturnValue;
        }

        public decimal ToplamSatisTutari(DateTime gunbasi, DateTime gunsonu)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("ToplamSatisTutari");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GunBasi", SqlDbType.DateTime).Value = gunbasi;
                cmd.Parameters.Add("@GunSonu", SqlDbType.DateTime).Value = gunsonu;
                ReturnDecimal = DAL.CalistirDecimal(cmd);
            });
            return ReturnDecimal;
        }

        public decimal GiderHesaplama(string urunAdi, int urunAdedi)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("GiderHesaplama");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UrunAdi", SqlDbType.NVarChar).Value = urunAdi;
                cmd.Parameters.Add("@UrunAdedi", SqlDbType.Int).Value = urunAdedi;
                ReturnDecimal = DAL.CalistirDecimal(cmd);
            });
            return ReturnDecimal;
        }

        public List<Odeme> OdemeHepsiGetir(DateTime gunbasi, DateTime gunsonu)
        {
            List<Odeme> siparisler = new List<Odeme>();

            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("OdemeHepsiGetir");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GunBasi", SqlDbType.DateTime).Value = gunbasi;
                cmd.Parameters.Add("@GunSonu", SqlDbType.DateTime).Value = gunsonu;
                reader = DAL.VeriGetir(cmd);

                while (reader.Read())
                {
                    siparisler.Add(new Odeme
                    {
                        ID = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                        MasaID = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                        MasaAdi = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                        AdisyonNo = reader.IsDBNull(3) ? 0 : reader.GetInt32(3),
                        OdenenSiparis = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                        SiparisFiyati = reader.IsDBNull(5) ? 0 : reader.GetDecimal(5),
                        SiparisAdedi = reader.IsDBNull(6) ? 0 : reader.GetInt32(6),
                        Tarih = reader.IsDBNull(7) ? DateTime.MinValue : reader.GetDateTime(7),
                        OdemeTipi = reader.IsDBNull(8) ? string.Empty : reader.GetString(8),
                        Iskonto = reader.IsDBNull(9) ? 0 : reader.GetInt32(9),
                        MasaDurumu = reader.IsDBNull(10) ? string.Empty : reader.GetString(10)
                    });
                }
                reader.Close();
                DAL.con.Close();
            });
            return siparisler;
        }

        public decimal NakitOdemeMiktari(DateTime gunbasi, DateTime gunsonu)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("NakitOdemeMiktari");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GunBasi", SqlDbType.DateTime).Value = gunbasi;
                cmd.Parameters.Add("@GunSonu", SqlDbType.DateTime).Value = gunsonu;
                ReturnDecimal = DAL.CalistirDecimal(cmd);
            });
            return ReturnDecimal;
        }

        public decimal KrediKartiOdemeMiktari(DateTime gunbasi, DateTime gunsonu)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("KrediKartiOdemeMiktari");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GunBasi", SqlDbType.DateTime).Value = gunbasi;
                cmd.Parameters.Add("@GunSonu", SqlDbType.DateTime).Value = gunsonu;
                ReturnDecimal = DAL.CalistirDecimal(cmd);
            });
            return ReturnDecimal;
        }

        public decimal YemekCekiOdemeMiktari(DateTime gunbasi, DateTime gunsonu)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("YemekCekiOdemeMiktari");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GunBasi", SqlDbType.DateTime).Value = gunbasi;
                cmd.Parameters.Add("@GunSonu", SqlDbType.DateTime).Value = gunsonu;
                ReturnDecimal = DAL.CalistirDecimal(cmd);
            });
            return ReturnDecimal;
        }

        public decimal IkramOdemeMiktari(DateTime gunbasi, DateTime gunsonu)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("IkramOdemeMiktari");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GunBasi", SqlDbType.DateTime).Value = gunbasi;
                cmd.Parameters.Add("@GunSonu", SqlDbType.DateTime).Value = gunsonu;
                ReturnDecimal = DAL.CalistirDecimal(cmd);
            });
            return ReturnDecimal;
        }

        public int GunIslemleriEkle(DateTime gunBasi, DateTime gunSonu, int adisyonSayisi, int siparisAdedi, decimal toplamSatis, decimal gelir, decimal gider, decimal nakit, decimal krediKarti, decimal yemekCeki, decimal ikram)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("GunIslemleriEkle");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GunBasi", SqlDbType.DateTime).Value = gunBasi;
                cmd.Parameters.Add("@GunSonu", SqlDbType.DateTime).Value = gunSonu;
                cmd.Parameters.Add("@AdisyonSayisi", SqlDbType.Int).Value = adisyonSayisi;
                cmd.Parameters.Add("@SatilanSiparisAdedi", SqlDbType.Int).Value = siparisAdedi;
                cmd.Parameters.Add("@ToplamSatis", SqlDbType.Decimal).Value = toplamSatis;
                cmd.Parameters.Add("@Gelir", SqlDbType.Decimal).Value = gelir;
                cmd.Parameters.Add("@Gider", SqlDbType.Decimal).Value = gider;
                cmd.Parameters.Add("@Nakit", SqlDbType.Decimal).Value = nakit;
                cmd.Parameters.Add("@KrediKarti", SqlDbType.Decimal).Value = krediKarti;
                cmd.Parameters.Add("@YemekCeki", SqlDbType.Decimal).Value = yemekCeki;
                cmd.Parameters.Add("@ikram", SqlDbType.Decimal).Value = ikram;
                ReturnValue = DAL.Calistir(cmd);
            });
            return ReturnValue;
        }

        public int MasaBolumEkle(string bolumAdi)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("MasaBolumEkle");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@BolumAdi", SqlDbType.NVarChar).Value = bolumAdi;
                ReturnValue = DAL.Calistir(cmd);
            });
            return ReturnValue;
        }

        public List<MasaBolumleri> MasaBolumleriGetir()
        {
            List<MasaBolumleri> masaBolumleri = new List<MasaBolumleri>();

            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("MasaBolumleriGetir");
                cmd.CommandType = CommandType.StoredProcedure;
                reader = DAL.VeriGetir(cmd);

                while (reader.Read())
                {
                    masaBolumleri.Add(new MasaBolumleri
                    {
                        BolumAdi = reader.IsDBNull(0) ? string.Empty : reader.GetString(0)
                    });
                }
                reader.Close();
                DAL.con.Close();
            });
            return masaBolumleri;
        }

        public string MasaBolumleriFirstID()
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("MasaBolumleriFirstID");
                cmd.CommandType = CommandType.StoredProcedure;
                ReturnString = DAL.CalistirString(cmd);
            });
            return ReturnString;
        }

        public int PersonelSil(int Id, string ad)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("PersonelSil");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GarsonID", SqlDbType.Int).Value = Id;
                cmd.Parameters.Add("@Ad", SqlDbType.NVarChar).Value = ad;
                ReturnValue = DAL.Calistir(cmd);
            });
            return ReturnValue;
        }

        public Garson SecilenPersoneliGetir(int Id, string ad)
        {
            Garson garson = new Garson();

            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("SecilenPersoneliGetir");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GarsonID", SqlDbType.Int).Value = Id;
                cmd.Parameters.Add("@Ad", SqlDbType.NVarChar).Value = ad;
                reader = DAL.VeriGetir(cmd);

                while (reader.Read())
                {
                    garson.GarsonID = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                    garson.Ad = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                    garson.Soyad = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                    garson.KullaniciAdi = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                    garson.Sifre = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                    garson.Telefon = reader.IsDBNull(5) ? string.Empty : reader.GetString(5);
                    garson.Adres = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                    garson.Gorev = reader.IsDBNull(7) ? string.Empty : reader.GetString(7);
                }
                reader.Close();
                DAL.con.Close();
            });
            return garson;
        }

        public int PersonelGuncelle(int Id, string ad, string soyad, string kullaniciAdi, string sifre, string telefon, string adres, string gorev)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("PersonelGuncelle");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@GarsonID", SqlDbType.Int).Value = Id;
                cmd.Parameters.Add("@Ad", SqlDbType.NVarChar).Value = ad;
                cmd.Parameters.Add("@Soyad", SqlDbType.NVarChar).Value = soyad;
                cmd.Parameters.Add("@KullaniciAdi", SqlDbType.NVarChar).Value = kullaniciAdi;
                cmd.Parameters.Add("@Sifre", SqlDbType.NVarChar).Value = sifre;
                cmd.Parameters.Add("@Telefon", SqlDbType.NVarChar).Value = telefon;
                cmd.Parameters.Add("@Adres", SqlDbType.NVarChar).Value = adres;
                cmd.Parameters.Add("@Gorev", SqlDbType.NVarChar).Value = gorev;
                ReturnValue = DAL.Calistir(cmd);
            });
            return ReturnValue;
        }

        public int MenuUrunGuncelle(int id, string ad, string aciklama, decimal fiyat, string kategori, decimal maliyet)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("MenuUrunGuncelle");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@MenuUrunID", SqlDbType.Int).Value = id;
                cmd.Parameters.Add("@UrunAdi", SqlDbType.NVarChar).Value = ad;
                cmd.Parameters.Add("@UrunAciklama", SqlDbType.NVarChar).Value = aciklama;
                cmd.Parameters.Add("@UrunFiyati", SqlDbType.Decimal).Value = fiyat;
                cmd.Parameters.Add("@UrunKategori", SqlDbType.NVarChar).Value = kategori;
                cmd.Parameters.Add("@MaliyetFiyati", SqlDbType.Decimal).Value = maliyet;
                ReturnValue = DAL.Calistir(cmd);
            });
            return ReturnValue;
        }

        public int MenuKategoriEkle(string kategori)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("MenuKategoriEkle");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@KategoriAdi", SqlDbType.NVarChar).Value = kategori;
                ReturnValue = DAL.Calistir(cmd);
            });
            return ReturnValue;
        }

        public List<MenuKategori> MenuKategoriGetir()
        {
            List<MenuKategori> kategoriler = new List<MenuKategori>();
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("MenuKategoriGetir");
                cmd.CommandType = CommandType.StoredProcedure;
                reader = DAL.VeriGetir(cmd);

                while (reader.Read())
                {
                    kategoriler.Add(new MenuKategori
                    {
                        ID = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                        KategoriAdi = reader.IsDBNull(1) ? string.Empty : reader.GetString(1)
                    });
                }
                reader.Close();
                DAL.con.Close();
            });
            return kategoriler;
        }

        public string UrunKategorisiniBul(string urunAdi)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("UrunKategorisiniBul");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UrunAdi", SqlDbType.NVarChar).Value = urunAdi;
                ReturnString = DAL.CalistirString(cmd);
            });
            return ReturnString;
        }

        public Tuple<string, decimal, int, decimal> TmpTable2(string urunAdi, int adisyonNo, int id)
        {
            string kategori = "";
            decimal fiyat = 0;
            int adet = 0;
            decimal toplam = 0;
            Tuple<string, decimal, int, decimal> tuple;

            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("TmpTable2");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UrunAdi", SqlDbType.NVarChar).Value = urunAdi;
                cmd.Parameters.Add("@AdisyonNo", SqlDbType.Int).Value = adisyonNo;
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;
                reader = DAL.VeriGetir(cmd);

                while (reader.Read())
                {
                    kategori = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                    fiyat = reader.IsDBNull(1) ? 0 : reader.GetDecimal(1);
                    adet = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                    toplam = reader.IsDBNull(3) ? 0 : reader.GetDecimal(3);
                }
                reader.Close();
                DAL.con.Close();
            });
            return tuple = new Tuple<string, decimal, int, decimal>(kategori, fiyat, adet, toplam);
        }

        public int SiparisİptalEkle(string siparis, int adet, decimal fiyat, string stoktanDusuldumu, DateTime tarih, decimal maliyet)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("SiparisİptalEkle");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Siparis", SqlDbType.NVarChar).Value = siparis;
                cmd.Parameters.Add("@SiparisAdedi", SqlDbType.Int).Value = adet;
                cmd.Parameters.Add("@SiparisFiyati", SqlDbType.Decimal).Value = fiyat;
                cmd.Parameters.Add("@StoktanDusuldumu", SqlDbType.NVarChar).Value = stoktanDusuldumu;
                cmd.Parameters.Add("@Tarih", SqlDbType.DateTime).Value = tarih;
                cmd.Parameters.Add("@MaliyetFiyati", SqlDbType.Decimal).Value = maliyet;
                ReturnValue = DAL.Calistir(cmd);
            });
            return ReturnValue;
        }

        public List<SiparisIptal> SiparisIptalGetir()
        {
            List<SiparisIptal> siparisler = new List<SiparisIptal>();

            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("SiparisİptalGetir");
                cmd.CommandType = CommandType.StoredProcedure;
                reader = DAL.VeriGetir(cmd);

                while (reader.Read())
                {
                    siparisler.Add(new SiparisIptal
                    {
                        ID = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                        Siparis = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                        SiparisAdedi = reader.IsDBNull(2) ? 0 : reader.GetInt32(2),
                        SiparisFiyati = reader.IsDBNull(3) ? 0 : reader.GetDecimal(3),
                        StoktanDusuldumu = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                        Tarih = reader.IsDBNull(5) ? DateTime.MinValue : reader.GetDateTime(5),
                        MaliyetFiyati = reader.IsDBNull(6) ? 0 : reader.GetDecimal(6)
                    });
                }
            });
            return siparisler;
        }

        public List<SiparisIptal> StoktanDusulenIptaller()
        {
            List<SiparisIptal> siparisler = new List<SiparisIptal>();

            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("StoktanDusulenIptaller");
                cmd.CommandType = CommandType.StoredProcedure;
                reader = DAL.VeriGetir(cmd);

                while (reader.Read())
                {
                    siparisler.Add(new SiparisIptal
                    {
                        ID = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                        Siparis = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                        SiparisAdedi = reader.IsDBNull(2) ? 0 : reader.GetInt32(2),
                        SiparisFiyati = reader.IsDBNull(3) ? 0 : reader.GetDecimal(3),
                        StoktanDusuldumu = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                        Tarih = reader.IsDBNull(5) ? DateTime.MinValue : reader.GetDateTime(5),
                        MaliyetFiyati = reader.IsDBNull(6) ? 0 : reader.GetDecimal(6)
                    });
                }
            });
            return siparisler;
        }

        public List<SiparisIptal> SiparisİptalTariheGore(DateTime tarih1, DateTime tarih2)
        {
            List<SiparisIptal> siparisler = new List<SiparisIptal>();
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("SiparisİptalTariheGore");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Tarih1", SqlDbType.DateTime).Value = tarih1;
                cmd.Parameters.Add("@Tarih2", SqlDbType.DateTime).Value = tarih2;
                reader = DAL.VeriGetir(cmd);

                while (reader.Read())
                {
                    siparisler.Add(new SiparisIptal
                    {
                        ID = reader.IsDBNull(0) ? 0 : reader.GetInt32(0),
                        Siparis = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                        SiparisAdedi = reader.IsDBNull(2) ? 0 : reader.GetInt32(2),
                        SiparisFiyati = reader.IsDBNull(3) ? 0 : reader.GetDecimal(3),
                        StoktanDusuldumu = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                        Tarih = reader.IsDBNull(5) ? DateTime.MinValue : reader.GetDateTime(5),
                        MaliyetFiyati = reader.IsDBNull(6) ? 0 : reader.GetDecimal(6)
                    });
                }
            });
            return siparisler;
        }

        public int YaziciBolumEkle(string bolumAdi)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("YaziciBolumEkle");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@BolumAdi", SqlDbType.NVarChar).Value = bolumAdi;
                ReturnValue = DAL.Calistir(cmd);
            });
            return ReturnValue;
        }

        public List<YaziciBolumleri> YaziciBolumleriGetir()
        {
            List<YaziciBolumleri> yaziciBolumleri = new List<YaziciBolumleri>();
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("YaziciBolumleriGetir");
                cmd.CommandType = CommandType.StoredProcedure;
                reader = DAL.VeriGetir(cmd);

                while (reader.Read())
                {
                    yaziciBolumleri.Add(new YaziciBolumleri
                    {
                        BolumAdi = reader.IsDBNull(0) ? string.Empty : reader.GetString(0)
                    });
                }
                reader.Close();
                DAL.con.Close();
            });
            return yaziciBolumleri;
        }

        public int YaziciAdiEkle(string bolum, string yazici)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("YaziciAdiEkle");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@BolumAdi", SqlDbType.NVarChar).Value = bolum;
                cmd.Parameters.Add("@YaziciAdi", SqlDbType.NVarChar).Value = yazici;
                ReturnValue = DAL.Calistir(cmd);
            });
            return ReturnValue;
        }

        public List<YaziciBolumleri> BolumYaziciGetir()
        {
            List<YaziciBolumleri> yaziciBolumleri = new List<YaziciBolumleri>();
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("BolumYaziciGetir");
                cmd.CommandType = CommandType.StoredProcedure;
                reader = DAL.VeriGetir(cmd);

                while (reader.Read())
                {
                    yaziciBolumleri.Add(new YaziciBolumleri
                    {
                        BolumAdi = reader.IsDBNull(0) ? string.Empty : reader.GetString(0),
                        YaziciAdi = reader.IsDBNull(1) ? string.Empty : reader.GetString(1)
                    });
                }
                reader.Close();
                DAL.con.Close();
            });
            return yaziciBolumleri;
        }

        public int MenuKategoriSil(string kategori)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("MenuKategoriSil");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@KategoriAdi", SqlDbType.NVarChar).Value = kategori;
                ReturnValue = DAL.Calistir(cmd);
            });
            return ReturnValue;
        }

        public int MenudenUrunSil(string urun)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("MenudenUrunSil");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UrunAdi", SqlDbType.NVarChar).Value = urun;
                ReturnValue = DAL.Calistir(cmd);
            });
            return ReturnValue;
        }

        public int StokDusulecekSil(int urunId)
        {
            TryCatchKullan(() =>
            {
                cmd = new SqlCommand("StokDusulecekSil");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@MenuUrunID", SqlDbType.NVarChar).Value = urunId;
                ReturnValue = DAL.Calistir(cmd);
            });
            return ReturnValue;
        }
    }
}
