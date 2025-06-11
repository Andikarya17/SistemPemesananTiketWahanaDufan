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

-- Stored Procedure untuk Menambah Data Pengunjung
CREATE PROCEDURE AddPengunjung
    @Nama NVARCHAR(100),
    @Email NVARCHAR(100),
    @Password NVARCHAR(255)
AS
BEGIN
    INSERT INTO Pengunjung (Nama, Email, Password)
    VALUES (@Nama, @Email, @Password);
END;

-- Stored Procedure untuk Memperbarui Data Pengunjung
CREATE PROCEDURE UpdatePengunjung
    @PengunjungID INT,
    @Nama NVARCHAR(100),
    @Email NVARCHAR(100),
    @Password NVARCHAR(255)
AS
BEGIN
    UPDATE Pengunjung
    SET 
        Nama = COALESCE(NULLIF(@Nama, ''), Nama),
        Email = COALESCE(NULLIF(@Email, ''), Email),
        Password = COALESCE(NULLIF(@Password, ''), Password)
    WHERE PengunjungID = @PengunjungID;
END;

-- Stored Procedure untuk Menghapus Pengunjung
CREATE PROCEDURE DeletePengunjung
    @PengunjungID INT
AS
BEGIN
    DELETE FROM Pengunjung WHERE PengunjungID = @PengunjungID;
END;

-- Stored Procedure untuk Menambah Pesanan Tiket
CREATE PROCEDURE AddPesanan
    @PengunjungID INT,
    @WahanaID INT,
    @TanggalKunjungan DATE,
    @JumlahTiket INT,
    @TotalHarga DECIMAL(10,2),
    @MetodePembayaran NVARCHAR(50) = 'Tunai',
    @StatusPesanan NVARCHAR(20) = 'Dipesan'
AS
BEGIN
    INSERT INTO Pesanan (PengunjungID, WahanaID, TanggalKunjungan, JumlahTiket, TotalHarga, MetodePembayaran, StatusPesanan)
    VALUES (@PengunjungID, @WahanaID, @TanggalKunjungan, @JumlahTiket, @TotalHarga, @MetodePembayaran, @StatusPesanan);
END;

-- Stored Procedure untuk Update Status Pesanan
CREATE PROCEDURE UpdatePesananStatus
    @PesananID INT,
    @StatusPesanan NVARCHAR(20)
AS
BEGIN
    UPDATE Pesanan
    SET StatusPesanan = @StatusPesanan
    WHERE PesananID = @PesananID;
END;

-- Stored Procedure untuk Menghapus Data Pesanan
CREATE PROCEDURE DeletePesanan
    @PesananID INT
AS
BEGIN
    DELETE FROM Pesanan WHERE PesananID = @PesananID;
END;


--tambah wahana
CREATE PROCEDURE AddWahana
    @NamaWahana NVARCHAR(100),
    @TipeTiket NVARCHAR(50),
    @HargaTiket DECIMAL(10,2),
    @KapasitasHarian INT,
    @AdminID INT
AS
BEGIN
    INSERT INTO Wahana (NamaWahana, TipeTiket, HargaTiket, KapasitasHarian, AdminID)
    VALUES (@NamaWahana, @TipeTiket, @HargaTiket, @KapasitasHarian, @AdminID);
END;

--update wahana
CREATE PROCEDURE UpdateWahana
    @WahanaID INT,
    @NamaWahana NVARCHAR(100),
    @TipeTiket NVARCHAR(50),
    @HargaTiket DECIMAL(10,2),
    @KapasitasHarian INT,
    @AdminID INT
AS
BEGIN
    UPDATE Wahana
    SET 
        NamaWahana = @NamaWahana,
        TipeTiket = @TipeTiket,
        HargaTiket = @HargaTiket,
        KapasitasHarian = @KapasitasHarian,
        AdminID = @AdminID
    WHERE WahanaID = @WahanaID;
END;

--delete wahana
CREATE PROCEDURE DeleteWahana
    @WahanaID INT
AS
BEGIN
    DELETE FROM Wahana WHERE WahanaID = @WahanaID;
END;

--get wahana list
CREATE PROCEDURE GetWahanaList
AS
BEGIN
    SELECT * FROM Wahana
END

--get pengunjung list
CREATE PROCEDURE GetPengunjungList
AS
BEGIN
    SELECT * FROM Pengunjung
END

--get pesanan list
CREATE PROCEDURE GetPesananList
AS
BEGIN
    SELECT * FROM Pesanan
END



-- Index untuk pencarian cepat
CREATE NONCLUSTERED INDEX idx_Pengunjung_Email ON Pengunjung(Email);
CREATE NONCLUSTERED INDEX idx_Admin_Email ON Admin(Email);
CREATE NONCLUSTERED INDEX idx_Pesanan_PengunjungID ON Pesanan(PengunjungID);
CREATE NONCLUSTERED INDEX idx_Pesanan_WahanaID ON Pesanan(WahanaID);
CREATE NONCLUSTERED INDEX idx_Pesanan_TanggalKunjungan ON Pesanan(TanggalKunjungan);
CREATE NONCLUSTERED INDEX idx_Pesanan_StatusPesanan ON Pesanan(StatusPesanan);
CREATE NONCLUSTERED INDEX idx_Pesanan_StatusTanggal
ON Pesanan(StatusPesanan, TanggalKunjungan);

