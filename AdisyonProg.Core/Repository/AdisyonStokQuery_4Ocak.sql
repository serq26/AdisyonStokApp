create database AdisyonStok
go
Use AdisyonStok
go

create table Garson
(
GarsonID int identity(1,1) primary key,
Ad nvarchar(30),
Soyad nvarchar(30),
KullaniciAdi nvarchar(30),
Sifre nvarchar(30),
Telefon nvarchar(30),
Adres nvarchar(100),
ToplamCalismaSaati int
)

create proc GarsonEkle
(
@Ad nvarchar(30),
@Soyad nvarchar(30),
@KullaniciAdi nvarchar(30),
@Sifre nvarchar(30),
@Telefon nvarchar(30),
@Adres nvarchar(100)
)
as
begin
insert into Garson (Ad, Soyad, KullaniciAdi, Sifre,Telefon,Adres) values (@Ad,@Soyad,@KullaniciAdi,@Sifre,@Telefon,@Adres)
end

go

create proc KullaniciGirisKontrol
(
@Sifre nvarchar(30)
)
as
begin
select * from Garson where Sifre = @Sifre
end


create table Kasa
(
KasaID int identity(1,1) primary key,
KasaAdi nvarchar(30),
KasaKullaniciAdi nvarchar(30),
KasaSifre nvarchar(30)
)

create proc KasaEkle
(
@KasaAdi nvarchar(30),
@KasaKullaniciAdi nvarchar(30),
@KasaSifre nvarchar(30)
)
as
begin
insert into Kasa (KasaAdi,KasaKullaniciAdi,KasaSifre) values (@KasaAdi,@KasaKullaniciAdi,@KasaSifre)
end

create proc KasaGirisKontrol
(
@KasaSifre nvarchar(30)
)
as
begin
select * from Kasa where KasaSifre = @KasaSifre
end

create table Urun
(
UrunID int identity(1,1) primary key,
UrunAdi nvarchar(100),
UrunFiyati decimal(18,2),
UrunAciklama nvarchar(max),
UrunAdedi int,
BirimCinsi nvarchar(20),
StokGirisTarihi datetime
)

create proc StokUrunEkle
(
@UrunAdi nvarchar(100),
@UrunFiyati decimal(18,2),
@UrunAciklama nvarchar(max),
@UrunAdedi decimal(18,3),
@BirimCinsi nvarchar(20),
@StokGirisTarihi datetime
)
as
begin
insert into Urun (UrunAdi,UrunFiyati,UrunAciklama,UrunAdedi,BirimCinsi,StokGirisTarihi) values (@UrunAdi,@UrunFiyati,@UrunAciklama,@UrunAdedi,@BirimCinsi,@StokGirisTarihi)
end

create table Menu
(
MenuUrunID int identity(1,1) primary key,
UrunID int,
UrunAdi nvarchar(100) not null,
UrunAciklama nvarchar(max),
UrunFiyati decimal(18,2) not null,
UrunKategori nvarchar(30) not null,
foreign key (UrunID) references Urun(UrunID)
)

create proc MenuyeUrunEkle
(
@UrunAdi nvarchar(100),
@UrunAciklama nvarchar(max),
@UrunFiyati decimal(18,2),
@UrunKategori nvarchar(30)
)
as
begin
if exists(select UrunAdi from Menu where UrunAdi = @UrunAdi)
	begin
		print 'Menüde bu ürün mevcut..!'
	end
else
	begin
		insert into Menu (UrunAdi,UrunAciklama,UrunFiyati,UrunKategori) values (@UrunAdi,@UrunAciklama,@UrunFiyati,@UrunKategori)
	end
end

create table Masalar
(
MasaID int identity(1,1) primary key,
MasaAdi nvarchar(100) not null,
MasaRengi nvarchar(100)
)

create proc MasaEkle
(
@MasaAdi nvarchar(100),
@MasaRengi nvarchar(100)
)
as
begin
insert into Masalar (MasaAdi,MasaRengi) values (@MasaAdi,@MasaRengi)
end

create proc TumMasalar
as
begin
select * from Masalar
end

create proc KategoriyeGoreUrunGetir
(
@UrunKategori nvarchar(30)
)
as
begin
select * from Menu where UrunKategori = @UrunKategori
end

create proc MenudekiTumUrunler
as 
begin
select * from Menu
end

create table Adisyon
(
AdisyonID int,
AdisyonNo int not null,
MasaAdi nvarchar(100) not null,
Siparis nvarchar(100) not null,
SiparisAdedi int not null,
SiparisFiyati decimal(18,2) not null,
SiparisTarihi datetime not null
foreign key (AdisyonID) references Masalar(MasaID)
)


create table MasaHesaplari
(
HesapID int identity(1,1) primary key,
AdisyonID int not null,
MasaAdi nvarchar(50) not null,
AdisyonTutari decimal(18,2)
)

create proc SiparisEkle
(
@AdisyonID int,
@AdisyonNo int,
@MasaAdi nvarchar(100),
@Siparis nvarchar(100),
@SiparisAdedi int,
@SiparisFiyati decimal(18,2),
@SiparisTarihi datetime
)
as
begin
insert into Adisyon (AdisyonID,AdisyonNo,MasaAdi,Siparis,SiparisAdedi,SiparisFiyati,SiparisTarihi) values (@AdisyonID,@AdisyonNo,@MasaAdi,@Siparis,@SiparisAdedi,@SiparisFiyati,@SiparisTarihi)
end

create proc SiparisEkle2
(
@AdisyonID int,
@AdisyonNo int,
@MasaAdi nvarchar(100),
@Siparis nvarchar(100),
@SiparisAdedi int,
@SiparisFiyati decimal(18,2),
@SiparisTarihi datetime
)
as
begin
if exists(select Siparis from Adisyon where Siparis = @Siparis and AdisyonNo = @AdisyonNo)
	begin
		update Adisyon set SiparisAdedi = SiparisAdedi + @SiparisAdedi, SiparisFiyati = SiparisFiyati + (@SiparisFiyati * @SiparisAdedi) where Siparis = @Siparis and AdisyonNo = @AdisyonNo
	end
	else
	begin
		insert into Adisyon (AdisyonID,AdisyonNo,MasaAdi,Siparis,SiparisAdedi, SiparisFiyati,SiparisTarihi) values (@AdisyonID,@AdisyonNo,@MasaAdi,@Siparis,@SiparisAdedi, @SiparisAdedi * @SiparisFiyati,@SiparisTarihi)
	end
end


create proc SiparisSil
(
@AdisyonNo int,
@Siparis nvarchar(100)
)
as
begin
delete from Adisyon where AdisyonNo = @AdisyonNo AND Siparis = @Siparis
end

create proc MasadakiSiparisleriGetir
(
@AdisyonID int
)
as
begin
select * from Adisyon where AdisyonID = @AdisyonID
end

create proc MasaDurumunuGuncelle
(
@MasaID int,
@MasaRengi nvarchar(100),
@MasaSaati datetime
)
as
begin
update Masalar set MasaRengi = @MasaRengi, MasaSaati = @MasaSaati where MasaID = @MasaID
end

create proc MasaDurumunuGuncelle2
(
@MasaID int,
@MasaRengi nvarchar(100),
@MasaSaati datetime,
@AdisyonNo int
)
as
begin
update Masalar set MasaRengi = @MasaRengi, MasaSaati = @MasaSaati, AdisyonNo = @AdisyonNo where MasaID = @MasaID
end


create proc MasaDurumunuGetir
(
@MasaID int
)
as
begin
select MasaRengi from Masalar where MasaID = @MasaID
end

create table AdisyonNumarasi
(
ID int,
AdisyonNo int
)

create proc SýradakiAdisyonNo
as
begin
select AdisyonNo from AdisyonNumarasi where ID = 1
end

create proc AdisyonNoGuncelle
(
@ID int
)
as
begin
update AdisyonNumarasi set AdisyonNo = AdisyonNo+1 where ID = 1
end



create proc MasaAdisyonNumarasiniGetir
(
@MasaAdi nvarchar(100)
)
as
begin
declare @HasExist int
set @HasExist = 0
select @HasExist = AdisyonNo from Adisyon where MasaAdi = @MasaAdi
if @HasExist = 0
	begin
	select 0
	end 
else
	begin
	select @HasExist	
	end
end

create proc MasaAdisyonNo
(
@MasaID int
)
as
begin
select AdisyonNo from Masalar where MasaID = @MasaID
end


create proc MasaAktar
(
@AdisyonID int,
@AdisyonNo int,
@MasaAdi nvarchar(100),
@AdisyonID2 int
)
as
begin
update Adisyon 
set 
AdisyonID = @AdisyonID2,
AdisyonNo = @AdisyonNo, 
MasaAdi = @MasaAdi 
where 
AdisyonID = @AdisyonID
end

create proc StokGetir
as
begin
select * from Urun
end


create proc MasaHesapDurumu
(
@AdisyonID int,
@AdisyonNo int
)
as
begin
declare @ReturnValue decimal(18,2)
set @ReturnValue = 0
if(@AdisyonNo = 0)
	begin
     select @ReturnValue
	end
else
	begin
	  select Sum(SiparisFiyati) from Adisyon where AdisyonID = @AdisyonID and AdisyonNo = @AdisyonNo	  
	end
end

create proc UrunFiyatiGetir
(
@UrunAdi nvarchar(100)
)
as
begin
select UrunFiyati from Menu where UrunAdi = @UrunAdi
end

create proc StoktaUrunAra
(
@UrunAdi nvarchar(100)
)
as
begin
select * from Urun where UrunAdi like @UrunAdi + '%'
end

create proc MasaSaatiHesapla
(
@MasaID int
)
as
begin
select MasaSaati from Masalar where MasaID = @MasaID
end

create proc OdemeDetay
(
@AdisyonID int
)
as
begin
select Siparis , Count(*) as SiparisAdedi, SiparisFiyati from Adisyon where AdisyonID = @AdisyonID group by Siparis, SiparisFiyati
end

create proc OdemeDetay2
(
@AdisyonID int
)
as
begin
select Siparis ,SiparisAdedi, SiparisFiyati from Adisyon where AdisyonID = @AdisyonID group by Siparis, SiparisAdedi, SiparisFiyati
end



create table StoktanDusulecekUrunler
(
ID int identity(1,1) primary key,
MenuUrunID int,
UrunAdi nvarchar(100),
UrunAdedi int,
BirimCinsi nchar(20)
foreign key (MenuUrunID) references Menu(MenuUrunID)
)

create proc MenuUrunIDGetir
(
@UrunAdi nvarchar(100)
)
as
begin
select MenuUrunID from Menu where UrunAdi = @UrunAdi
end

create proc StokDusulucekUrunEkle
(
@MenuUrunID int,
@UrunAdi nvarchar(100),
@UrunAdedi decimal(18,3),
@BirimCinsi nchar(20)
)
as
begin
insert into StoktanDusulecekUrunler (MenuUrunID,UrunAdi,UrunAdedi,BirimCinsi) values (@MenuUrunID, @UrunAdi,@UrunAdedi,@BirimCinsi)
end

create proc StokBirimCinsiKontrol
(
@UrunAdi nvarchar(100)
)
as
begin
select BirimCinsi from Urun where UrunAdi = @UrunAdi
end

create proc StoktanEksiltme
(
@UrunAdi nvarchar(100),
@UrunAdedi decimal(18,3)
)
as
begin
update Urun set UrunAdedi -= @UrunAdedi where UrunAdi = @UrunAdi
end

select * from Masalar
select * from Adisyon 
select * from AdisyonNumarasi
select * from Urun
select * from Menu

