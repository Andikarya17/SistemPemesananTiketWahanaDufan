-- Membuat database
CREATE DATABASE TiketwahanDufan2;

-- Tabel Admin
CREATE TABLE Admin (
    AdminID INT IDENTITY(1,1) PRIMARY KEY,
    Nama NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    Password NVARCHAR(255) NOT NULL
);


-- Tabel Pengunjung
CREATE TABLE Pengunjung (
    PengunjungID INT IDENTITY(1,1) PRIMARY KEY,
    Nama NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    Password NVARCHAR(255) NOT NULL
);


-- Tabel Wahana (dengan relasi ke Admin)
CREATE TABLE Wahana (
    WahanaID INT IDENTITY(1,1) PRIMARY KEY,
    NamaWahana NVARCHAR(100) NOT NULL,
    TipeTiket NVARCHAR(20) CHECK (TipeTiket IN ('Reguler', 'Fast Track')) NOT NULL,
    HargaTiket DECIMAL(10,2) NOT NULL,
    KapasitasHarian INT NOT NULL,
    AdminID INT,
    FOREIGN KEY (AdminID) REFERENCES Admin(AdminID) ON DELETE SET NULL
);


-- Tabel Pesanan (dengan relasi ke Admin)
CREATE TABLE Pesanan (
    PesananID INT IDENTITY(1,1) PRIMARY KEY,
    PengunjungID INT NOT NULL,
    WahanaID INT NOT NULL,
    AdminID INT,
    TanggalKunjungan DATE NOT NULL,
    JumlahTiket INT NOT NULL,
    TotalHarga DECIMAL(10,2) NOT NULL,
    MetodePembayaran NVARCHAR(50) NOT NULL,
    StatusPesanan NVARCHAR(20) CHECK (StatusPesanan IN ('Dipesan', 'Dibatalkan', 'Selesai')) DEFAULT 'Dipesan',
    FOREIGN KEY (PengunjungID) REFERENCES Pengunjung(PengunjungID) ON DELETE CASCADE,
    FOREIGN KEY (WahanaID) REFERENCES Wahana(WahanaID) ON DELETE CASCADE,
    FOREIGN KEY (AdminID) REFERENCES Admin(AdminID) ON DELETE SET NULL
);