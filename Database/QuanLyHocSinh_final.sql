-- =============================================================================
-- I. THIẾT LẬP DATABASE (Làm sạch và Tạo mới)
-- =============================================================================
USE master;
GO
CREATE DATABASE QuanLyHocSinh;
GO
USE QuanLyHocSinh;
GO

-- =============================================================================
-- II. ĐỊNH NGHĨA BẢNG (TABLES) - CẤU TRÚC MỚI
-- =============================================================================
select * from GiaoVien
-- 1. Bảng Admin
CREATE TABLE Admin (
    ID_Admin INT PRIMARY KEY IDENTITY(1,1),
    Email_Admin VARCHAR(100) UNIQUE NOT NULL,
    Password_Admin VARCHAR(100) NOT NULL
);
GO

-- 2. Bảng Giáo Viên (Đã bỏ cột 'Mon' vì 1 GV dạy nhiều môn thì lưu ở bảng phân công)
CREATE TABLE GiaoVien (
    ID_GV INT PRIMARY KEY IDENTITY(1, 1) NOT NULL,
    HoTen NVARCHAR(100) NOT NULL,
    Email_GV VARCHAR(100) UNIQUE NOT NULL,
    Password_GV VARCHAR(100) NOT NULL,
    NgaySinh DATE,
    GioiTinh NVARCHAR(5),
    SoDienThoai VARCHAR(15)
);
GO

-- 3. Bảng Lớp Học
CREATE TABLE LopHoc (
    MaLop INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    TenLop NVARCHAR(50) NOT NULL,
    Khoi NVARCHAR(20) NOT NULL,
    SoLuongHocSinh INT DEFAULT 0,
    TuyenDuong NVARCHAR(200)
);
GO

-- 4. Bảng Môn Học
CREATE TABLE MonHoc (
    MaMon INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    TenMon NVARCHAR(50) NOT NULL
);
GO

-- 5. Bảng PHÂN CÔNG GIẢNG DẠY (THAY THẾ CHO 2 BẢNG CŨ) 🔥
-- Logic: Một dòng xác định rõ GV A dạy Lớp B môn C
CREATE TABLE PhanCongGiangDay (
    ID_PhanCong INT PRIMARY KEY IDENTITY(1,1),
    ID_GV INT,
    MaLop INT,
    MaMon INT,
    VaiTro NVARCHAR(50) DEFAULT N'Bộ Môn', -- Ví dụ: Chủ Nhiệm, Bộ Môn

    -- Ràng buộc: Trong 1 lớp, 1 môn chỉ có 1 giáo viên dạy
    CONSTRAINT UK_PhanCong UNIQUE (MaLop, MaMon),

    FOREIGN KEY (ID_GV) REFERENCES GiaoVien(ID_GV) ON DELETE CASCADE,
    FOREIGN KEY (MaLop) REFERENCES LopHoc(MaLop) ON DELETE CASCADE,
    FOREIGN KEY (MaMon) REFERENCES MonHoc(MaMon) ON DELETE CASCADE
);
GO

-- 6. Bảng Học Sinh
CREATE TABLE HocSinh (
    STT INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    HoTen NVARCHAR(100) NOT NULL,
    NgaySinh DATE,
    GioiTinh NVARCHAR(5),
    LoaiHocSinh NVARCHAR(30),
    DanToc NVARCHAR(20),
    MaLop INT,
    DiemTB FLOAT NULL,
    FOREIGN KEY (MaLop) REFERENCES LopHoc(MaLop)
);
GO

-- 7. Bảng Kết Quả Học Tập
CREATE TABLE KetQuaHocTap (
    STT INT,
    MaMon INT,
    GK1 FLOAT, CK1 FLOAT, GK2 FLOAT, CK2 FLOAT,
    XepLoai NVARCHAR(20),
    PRIMARY KEY (STT, MaMon),
    FOREIGN KEY (STT) REFERENCES HocSinh(STT) ON DELETE CASCADE,
    FOREIGN KEY (MaMon) REFERENCES MonHoc(MaMon) ON DELETE CASCADE
);
GO

-- 8. Bảng Điểm Danh
CREATE TABLE DiemDanh (
    STT INT,
    MaLop INT,
    Ngay DATE NOT NULL,
    Buoi NVARCHAR(20) NOT NULL,
    TinhTrang NVARCHAR(20),
    PRIMARY KEY (STT, MaLop, Ngay, Buoi),
    FOREIGN KEY (STT) REFERENCES HocSinh(STT),
    FOREIGN KEY (MaLop) REFERENCES LopHoc(MaLop)
);
GO

-- 9. Bảng Notes (Ghi chú)

-- Tạo bảng mới có cột MaMon
CREATE TABLE Notes (
    STT INT NOT NULL,           -- Học sinh nào
    MaMon INT NOT NULL,         -- Môn gì (ĐÂY LÀ CÁI CẦN THÊM)
    ID_GV INT,                  -- Giáo viên nào ghi (để biết ai viết)
    NhanXet NVARCHAR(MAX),      -- Nội dung ghi chú (dùng MAX cho thoải mái)
    NgayGhi DATETIME DEFAULT GETDATE(),
    
    -- Khóa chính: Một học sinh, trong một môn học, chỉ có 1 dòng ghi chú
    PRIMARY KEY (STT, MaMon), 
    
    FOREIGN KEY (STT) REFERENCES HocSinh(STT) ON DELETE CASCADE,
    FOREIGN KEY (MaMon) REFERENCES MonHoc(MaMon) ON DELETE CASCADE,
    FOREIGN KEY (ID_GV) REFERENCES GiaoVien(ID_GV)
);
GO
select *from Notes;

-- =============================================================================
-- 2. CẬP NHẬT THỦ TỤC LƯU GHI CHÚ
-- =============================================================================
CREATE OR ALTER PROCEDURE sp_SaveNote
    @ID_GV INT,
    @STT INT,
    @MaMon INT, -- Tham số mới
    @NhanXet NVARCHAR(MAX)
AS
BEGIN
    -- Kiểm tra xem đã có ghi chú cho môn này của học sinh này chưa
    IF EXISTS (SELECT 1 FROM Notes WHERE STT = @STT AND MaMon = @MaMon)
    BEGIN
        -- Có rồi thì CẬP NHẬT nội dung
        UPDATE Notes 
        SET NhanXet = @NhanXet, ID_GV = @ID_GV, NgayGhi = GETDATE()
        WHERE STT = @STT AND MaMon = @MaMon
    END
    ELSE
    BEGIN
        -- Chưa có thì THÊM MỚI
        INSERT INTO Notes (STT, MaMon, ID_GV, NhanXet) 
        VALUES (@STT, @MaMon, @ID_GV, @NhanXet)
    END
END
GO

-- 10. Các bảng phụ (Trò chơi, OTP, Sao Lưu...)
CREATE TABLE TroChoiHocTap (MaTroChoi INT PRIMARY KEY IDENTITY(1,1), TenTroChoi NVARCHAR(100));
CREATE TABLE GiaoVien_TroChoi (ID_GV INT, MaTroChoi INT, PRIMARY KEY(ID_GV, MaTroChoi));
CREATE TABLE HocSinh_TroChoi (STT INT, MaTroChoi INT, DanhGia NVARCHAR(100), KETQUA FLOAT, PRIMARY KEY(STT, MaTroChoi));
CREATE TABLE SaoLuu (LanSaoLuu INT PRIMARY KEY IDENTITY(1,1), TenSL NVARCHAR(100), NgaySaoLuu DATE DEFAULT GETDATE(), NguoiTao NVARCHAR(100));
CREATE TABLE KhoiPhuc (LanKhoiPhuc INT PRIMARY KEY IDENTITY(1,1), TenKP NVARCHAR(100), NgayKhoiPhuc DATE DEFAULT GETDATE(), NguoiThucHien NVARCHAR(100));
CREATE TABLE OTP_Confirm (Email VARCHAR(100) PRIMARY KEY, OTPCode VARCHAR(10), ExpiredAt DATETIME);
GO

-- =============================================================================
-- III. ĐỊNH NGHĨA HÀM (FUNCTIONS)
-- =============================================================================
CREATE OR ALTER FUNCTION fn_SoBuoiNghi (@STT INT) RETURNS INT AS BEGIN DECLARE @SoNghi INT; SELECT @SoNghi = COUNT(*) FROM DiemDanh WHERE STT = @STT AND TinhTrang = N'Vắng'; RETURN @SoNghi; END GO
CREATE OR ALTER FUNCTION fn_TinhDiemTB(@STT INT) RETURNS FLOAT AS BEGIN DECLARE @DiemTB FLOAT; SELECT @DiemTB = AVG(CAST((GK1 + CK1 + GK2 + CK2)/4.0 AS FLOAT)) FROM KetQuaHocTap WHERE STT = @STT; RETURN @DiemTB; END; GO
CREATE OR ALTER FUNCTION fn_XepLoai(@DiemTB FLOAT) RETURNS NVARCHAR(20) AS BEGIN IF @DiemTB >= 8 RETURN N'Giỏi'; IF @DiemTB >= 6.5 RETURN N'Khá'; IF @DiemTB >= 5 RETURN N'Trung Bình'; RETURN N'Yếu'; END; GO
CREATE OR ALTER FUNCTION fn_TyLeXepLoai(@MaLop INT) RETURNS TABLE AS RETURN (SELECT XepLoai, COUNT(*) * 100.0 / (SELECT COUNT(*) FROM HocSinh hs WHERE hs.MaLop = @MaLop) AS TyLe FROM KetQuaHocTap kq JOIN HocSinh hs ON kq.STT = hs.STT WHERE hs.MaLop = @MaLop GROUP BY XepLoai); GO

-- =============================================================================
-- IV. ĐỊNH NGHĨA THỦ TỤC (STORED PROCEDURES)
-- =============================================================================

-- --- AUTH (GIỮ NGUYÊN) ---
CREATE OR ALTER PROCEDURE sp_DangNhapGiaoVien @Email_GV VARCHAR(100), @Password_GV VARCHAR(100) AS BEGIN SELECT ID_GV, HoTen, Email_GV, Password_GV FROM GiaoVien WHERE Email_GV = @Email_GV AND Password_GV = @Password_GV; END GO
CREATE OR ALTER PROCEDURE sp_DangNhapAdmin @Email_Admin VARCHAR(100), @Password_Admin VARCHAR(100) AS BEGIN SELECT ID_Admin, Email_Admin, Password_Admin FROM Admin WHERE Email_Admin = @Email_Admin AND Password_Admin = @Password_Admin; END GO
CREATE OR ALTER PROCEDURE sp_KiemTraEmailGiaoVien @Email_GV VARCHAR(100) AS BEGIN IF EXISTS (SELECT 1 FROM GiaoVien WHERE Email_GV = @Email_GV) SELECT N'Hợp lệ' AS ThongBao; ELSE SELECT N'Không tồn tại' AS ThongBao; END GO
CREATE OR ALTER PROCEDURE sp_DoiMatKhau @Email VARCHAR(100), @OldPassword VARCHAR(100), @NewPassword VARCHAR(100), @ConfirmPassword VARCHAR(100) AS BEGIN IF NOT EXISTS (SELECT 1 FROM GiaoVien WHERE Email_GV = @Email AND Password_GV = @OldPassword) BEGIN SELECT N'Sai MK cũ' AS ThongBao; RETURN; END IF @NewPassword <> @ConfirmPassword BEGIN SELECT N'Không khớp' AS ThongBao; RETURN; END UPDATE GiaoVien SET Password_GV = @NewPassword WHERE Email_GV = @Email; SELECT N'Thành công' AS ThongBao; END GO
CREATE OR ALTER PROCEDURE sp_TaoOTP @Email_GV VARCHAR(100) AS BEGIN IF NOT EXISTS (SELECT 1 FROM GiaoVien WHERE Email_GV = @Email_GV) BEGIN SELECT N'Email sai' AS ThongBao; RETURN; END DECLARE @OTP VARCHAR(6) = CAST((ABS(CHECKSUM(NEWID())) % 900000 + 100000) AS VARCHAR(6)); MERGE OTP_Confirm AS t USING (SELECT @Email_GV E) AS s ON t.Email = s.E WHEN MATCHED THEN UPDATE SET OTPCode=@OTP, ExpiredAt=DATEADD(MI,5,GETDATE()) WHEN NOT MATCHED THEN INSERT(Email,OTPCode,ExpiredAt) VALUES(@Email_GV,@OTP,DATEADD(MI,5,GETDATE())); SELECT @OTP AS OTP_TamThoi; END GO
CREATE OR ALTER PROCEDURE sp_XacNhanOTP @Email VARCHAR(100), @OTP VARCHAR(6) AS BEGIN IF EXISTS(SELECT 1 FROM OTP_Confirm WHERE Email=@Email AND OTPCode=@OTP AND ExpiredAt>GETDATE()) SELECT N'Thành công' AS ThongBao; ELSE SELECT N'Thất bại' AS ThongBao; END GO
CREATE OR ALTER PROCEDURE sp_ResetPasswordByEmail @Email_GV VARCHAR(100), @NewPassword VARCHAR(100) AS BEGIN IF EXISTS(SELECT 1 FROM GiaoVien WHERE Email_GV=@Email_GV) BEGIN UPDATE GiaoVien SET Password_GV=@NewPassword WHERE Email_GV=@Email_GV; SELECT N'Thành công'; END ELSE SELECT N'Lỗi'; END GO

-- --- GIÁO VIÊN (CẬP NHẬT) ---
-- 1. Lấy danh sách GV kèm chi tiết phân công (10A1 (Toán), 10A2 (Lý)) 🔥
CREATE OR ALTER PROCEDURE sp_GetAllGiaoVien_ChiTiet
AS
BEGIN
    SELECT 
        GV.ID_GV, GV.HoTen, GV.Email_GV, GV.SoDienThoai,
        ISNULL(STUFF((
            SELECT ', ' + LH.TenLop + N' (' + MH.TenMon + N')'
            FROM PhanCongGiangDay PC
            JOIN LopHoc LH ON PC.MaLop = LH.MaLop
            JOIN MonHoc MH ON PC.MaMon = MH.MaMon
            WHERE PC.ID_GV = GV.ID_GV
            FOR XML PATH('')
        ), 1, 2, ''), N'Chưa phân công') AS ChiTietPhanCong
    FROM GiaoVien GV ORDER BY GV.ID_GV DESC;
END
GO
-- Gọi cái này để tương thích với code cũ
CREATE OR ALTER PROCEDURE sp_GetAllGiaoVien AS EXEC sp_GetAllGiaoVien_ChiTiet; GO 

CREATE OR ALTER PROCEDURE sp_InsertGiaoVien @HoTen NVARCHAR(100), @Email VARCHAR(100), @Password VARCHAR(100), @NgaySinh DATE, @GioiTinh NVARCHAR(5), @SoDienThoai VARCHAR(15) AS INSERT INTO GiaoVien(HoTen, Email_GV, Password_GV, NgaySinh, GioiTinh, SoDienThoai) VALUES (@HoTen, @Email, @Password, @NgaySinh, @GioiTinh, @SoDienThoai); GO
CREATE OR ALTER PROCEDURE sp_UpdateGiaoVien @ID_GV INT, @HoTen NVARCHAR(100), @NgaySinh DATE, @GioiTinh NVARCHAR(5), @SoDienThoai VARCHAR(15) AS UPDATE GiaoVien SET HoTen=@HoTen, NgaySinh=@NgaySinh, GioiTinh=@GioiTinh, SoDienThoai=@SoDienThoai WHERE ID_GV = @ID_GV; GO
CREATE OR ALTER PROCEDURE sp_DeleteGiaoVien @ID_GV INT AS DELETE FROM GiaoVien WHERE ID_GV = @ID_GV; GO

-- --- PHÂN CÔNG GIẢNG DẠY (MỚI) 🔥 ---
-- 1. Xóa phân công cũ của GV (Để chuẩn bị thêm mới)
CREATE OR ALTER PROCEDURE sp_XoaPhanCongCuaGV @ID_GV INT AS DELETE FROM PhanCongGiangDay WHERE ID_GV = @ID_GV; GO

-- 2. Thêm phân công mới (Logic: GV dạy Lớp nào Môn gì)
CREATE OR ALTER PROCEDURE sp_ThemPhanCong @ID_GV INT, @MaLop INT, @MaMon INT AS
BEGIN
    -- Nếu lớp đó môn đó chưa có ai dạy thì thêm vào
    IF NOT EXISTS (SELECT 1 FROM PhanCongGiangDay WHERE MaLop=@MaLop AND MaMon=@MaMon)
        INSERT INTO PhanCongGiangDay(ID_GV, MaLop, MaMon) VALUES (@ID_GV, @MaLop, @MaMon);
    ELSE
        -- Nếu đã có người dạy rồi thì cập nhật thành giáo viên mới
        UPDATE PhanCongGiangDay SET ID_GV = @ID_GV WHERE MaLop = @MaLop AND MaMon = @MaMon;
END
GO

-- 3. Các thủ tục hỗ trợ hiển thị Checkbox cho Form Phân Công
CREATE OR ALTER PROCEDURE sp_GetMonCuaGV @ID_GV INT AS SELECT DISTINCT MaMon FROM PhanCongGiangDay WHERE ID_GV = @ID_GV; GO
CREATE OR ALTER PROCEDURE sp_GetLopCuaGV @ID_GV INT AS SELECT DISTINCT MaLop FROM PhanCongGiangDay WHERE ID_GV = @ID_GV; GO

-- --- LỚP HỌC (CẬP NHẬT ĐỂ DÙNG BẢNG MỚI) ---
-----------------------------------------
-- XÓA PROC sp_GetAllLop NẾU ĐÃ TỒN TẠI
-----------------------------------------
IF OBJECT_ID('dbo.sp_GetAllLop', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_GetAllLop;
GO

-----------------------------------------
-- TẠO PROC sp_GetAllLop
-----------------------------------------
CREATE PROCEDURE dbo.sp_GetAllLop
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        L.MaLop, 
        L.TenLop, 
        L.Khoi, 
        L.SoLuongHocSinh,

        -- Giáo viên đại diện
        ISNULL((
            SELECT TOP 1 GV.HoTen
            FROM PhanCongGiangDay PC
            JOIN GiaoVien GV ON PC.ID_GV = GV.ID_GV
            WHERE PC.MaLop = L.MaLop
        ), N'Chưa có GV') AS TenGiaoVien

    FROM LopHoc L;
END
GO
CREATE OR ALTER PROCEDURE sp_UpdateLop @MaLop INT, @TenLop NVARCHAR(50), @Khoi NVARCHAR(20), @SoLuongHocSinh INT AS UPDATE LopHoc SET TenLop = @TenLop, Khoi = @Khoi, SoLuongHocSinh = @SoLuongHocSinh WHERE MaLop = @MaLop; GO
CREATE OR ALTER PROCEDURE sp_DeleteLopHoc @MaLop INT AS DELETE FROM LopHoc WHERE MaLop = @MaLop; GO
CREATE OR ALTER PROCEDURE sp_GetListLopSimple AS SELECT MaLop, TenLop FROM LopHoc; GO

-- --- MÔN HỌC ---
CREATE OR ALTER PROCEDURE sp_GetAllMonHoc AS SELECT MaMon, TenMon FROM MonHoc; GO
-- Nếu procedure tồn tại thì xoá
IF OBJECT_ID('sp_InsertMonHoc', 'P') IS NOT NULL
    DROP PROCEDURE sp_InsertMonHoc;
GO

-- Tạo lại procedure mới
CREATE PROCEDURE sp_InsertMonHoc
    @TenMon NVARCHAR(50)
AS
BEGIN
    INSERT INTO MonHoc (TenMon)
    VALUES (@TenMon);
END
GO

-- --- HỌC SINH & KẾT QUẢ & THỐNG KÊ (GIỮ NGUYÊN NHƯNG UPDATE LOGIC ĐẾM) ---
CREATE OR ALTER PROCEDURE sp_GetHocSinhByLop @MaLop INT AS SELECT hs.STT, hs.HoTen, hs.NgaySinh, hs.GioiTinh, hs.DanToc, lh.TenLop FROM HocSinh hs JOIN LopHoc lh ON lh.MaLop = hs.MaLop WHERE hs.MaLop = @MaLop; GO
CREATE OR ALTER PROCEDURE sp_InsertHocSinh @HoTen NVARCHAR(100), @NgaySinh DATE, @GioiTinh NVARCHAR(5), @MaLop INT, @DanToc NVARCHAR(10) AS INSERT INTO HocSinh(HoTen, NgaySinh, GioiTinh, DanToc, MaLop) VALUES (@HoTen, @NgaySinh, @GioiTinh, @DanToc, @MaLop); GO
CREATE OR ALTER PROCEDURE sp_DeleteHocSinh @STT INT AS DELETE FROM HocSinh WHERE STT = @STT; GO
CREATE OR ALTER PROCEDURE sp_InsertKetQua @STT INT, @MaMon INT, @GK1 FLOAT, @CK1 FLOAT, @GK2 FLOAT, @CK2 FLOAT, @XepLoai NVARCHAR(20) AS INSERT INTO KetQuaHocTap(STT, MaMon, GK1, CK1, GK2, CK2, XepLoai) VALUES (@STT, @MaMon, @GK1, @CK1, @GK2, @CK2, @XepLoai); GO
CREATE OR ALTER PROCEDURE sp_CountGV_HS_ByLop @MaLop INT AS SELECT (SELECT COUNT(DISTINCT ID_GV) FROM PhanCongGiangDay WHERE MaLop = @MaLop) AS SoGiaoVien, (SELECT COUNT(*) FROM HocSinh WHERE MaLop = @MaLop) AS SoHocSinh; GO

-- =============================================================================
-- V. TRIGGERS
-- =============================================================================
CREATE OR ALTER TRIGGER trg_CapNhatKetQua ON KetQuaHocTap AFTER INSERT, UPDATE AS BEGIN UPDATE hs SET DiemTB = sub.DiemTB FROM HocSinh hs JOIN (SELECT STT, AVG(CAST((GK1 + CK1 + GK2 + CK2)/4.0 AS FLOAT)) AS DiemTB FROM KetQuaHocTap GROUP BY STT) sub ON hs.STT = sub.STT WHERE hs.STT IN (SELECT DISTINCT STT FROM inserted); END; GO
CREATE OR ALTER TRIGGER trg_UpdateXepLoai ON KetQuaHocTap AFTER INSERT, UPDATE AS BEGIN UPDATE kq SET XepLoai = dbo.fn_XepLoai((kq.GK1 + kq.CK1 + kq.GK2 + kq.CK2) / 4.0) FROM KetQuaHocTap kq INNER JOIN inserted i ON kq.STT = i.STT; END; GO
CREATE OR ALTER TRIGGER trg_PreventDelete_MonHoc ON MonHoc INSTEAD OF DELETE AS BEGIN IF EXISTS (SELECT 1 FROM deleted d JOIN KetQuaHocTap kq ON d.MaMon = kq.MaMon) BEGIN RAISERROR(N'Không thể xóa môn khi có điểm.',16,1); ROLLBACK; RETURN; END DELETE FROM MonHoc WHERE MaMon IN (SELECT MaMon FROM deleted); END; GO
CREATE OR ALTER TRIGGER trg_UpdateSoLuongHS ON HocSinh AFTER INSERT, DELETE AS BEGIN SET NOCOUNT ON; WITH AffectedLop AS (SELECT MaLop FROM inserted UNION SELECT MaLop FROM deleted), Counts AS (SELECT MaLop, COUNT(*) AS cnt FROM HocSinh WHERE MaLop IN (SELECT MaLop FROM AffectedLop) GROUP BY MaLop) UPDATE l SET SoLuongHocSinh = ISNULL(c.cnt, 0) FROM LopHoc l LEFT JOIN Counts c ON l.MaLop = c.MaLop WHERE l.MaLop IN (SELECT MaLop FROM AffectedLop); END; GO
USE QuanLyHocSinh;
GO

-- =============================================
-- TẠO CÁC THỦ TỤC QUẢN LÝ LỚP HỌC
-- =============================================

-- 1. Lấy danh sách tất cả lớp học (Sửa lại logic lấy tên GV từ bảng Phân Công mới)
CREATE OR ALTER PROCEDURE sp_GetAllLop
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        L.MaLop, 
        L.TenLop, 
        L.Khoi, 
        L.SoLuongHocSinh,
        -- Lấy tên 1 giáo viên bất kỳ đang dạy lớp này để hiển thị (đại diện)
        ISNULL((
            SELECT TOP 1 GV.HoTen 
            FROM PhanCongGiangDay PC 
            JOIN GiaoVien GV ON PC.ID_GV = GV.ID_GV 
            WHERE PC.MaLop = L.MaLop
        ), N'Chưa phân công') AS TenGiaoVien,
        -- Lấy danh sách các môn đang được dạy tại lớp này
        ISNULL((
             SELECT COUNT(DISTINCT MaMon) 
             FROM PhanCongGiangDay 
             WHERE MaLop = L.MaLop
        ), 0) AS SoMonHoc
    FROM LopHoc L;
END
GO
-- 1. Thủ tục Thêm Môn Học (Sửa lỗi bạn đang gặp)

-- Thêm ràng buộc: Không cho phép trùng tên môn
ALTER TABLE MonHoc
ADD CONSTRAINT UQ_TenMon UNIQUE (TenMon);
GO
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_InsertMonHoc')
    DROP PROCEDURE sp_InsertMonHoc
GO

CREATE PROCEDURE sp_InsertMonHoc
    @TenMon NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT OFF; -- Quan trọng: Để trả về số dòng thay đổi cho C#

    -- 1. Chuẩn hóa chuỗi: Cắt khoảng trắng đầu/cuối
    SET @TenMon = LTRIM(RTRIM(@TenMon));

    -- 2. Kiểm tra trùng (Không phân biệt hoa thường)
    -- SQL Server mặc định so sánh '=' là không phân biệt hoa thường
    IF EXISTS (SELECT 1 FROM MonHoc WHERE TenMon = @TenMon)
    BEGIN
        -- Nếu đã tồn tại thì không làm gì cả -> C# sẽ nhận được 0 dòng thay đổi
        RETURN; 
    END

    -- 3. Nếu chưa có thì mới thêm
    INSERT INTO MonHoc (TenMon) VALUES (@TenMon);
END
GO

-- 3. Thủ tục Xóa Môn Học
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_InsertMonHoc')
    DROP PROCEDURE sp_InsertMonHoc;
GO

CREATE PROCEDURE dbo.sp_InsertMonHoc
    @TenMon NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    -- 1. Chuẩn hóa: Cắt khoảng trắng thừa hai đầu
    SET @TenMon = LTRIM(RTRIM(@TenMon));

    -- 2. Kiểm tra trùng lặp (Case-Insensitive)
    -- SQL Server mặc định so sánh '=' là không phân biệt hoa thường.
    -- Nhưng để chắc chắn 100% với mọi cấu hình, ta dùng LOWER()
    IF EXISTS (SELECT 1 FROM MonHoc WHERE LOWER(TenMon) = LOWER(@TenMon))
    BEGIN
        -- Nếu trùng, không làm gì cả.
        -- Lúc này số dòng ảnh hưởng (RowsAffected) = 0.
        -- Code C# (ExecuteNonQuery) sẽ trả về false.
        RETURN; 
    END

    -- 3. Nếu chưa có thì thêm mới
    INSERT INTO MonHoc (TenMon) VALUES (@TenMon);
END
GO



-- 4. Thủ tục Sửa tên Môn Học
CREATE OR ALTER PROCEDURE sp_UpdateMonHoc
    @MaMon INT,
    @TenMon NVARCHAR(50)
AS
BEGIN
    UPDATE MonHoc SET TenMon = @TenMon WHERE MaMon = @MaMon;
END
GO
-- 2. Thêm Lớp
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_ThemLop')
    DROP PROCEDURE sp_ThemLop
GO

CREATE PROCEDURE sp_ThemLop
    @TenLop NVARCHAR(50),
    @Khoi NVARCHAR(20),
    @SoLuong INT = 0 -- Mặc định là 0 nếu không truyền
AS
BEGIN
    SET NOCOUNT OFF; -- Để trả về số dòng thay đổi cho C# bắt được

    -- 1. Chuẩn hóa
    SET @TenLop = LTRIM(RTRIM(@TenLop));
    SET @Khoi = LTRIM(RTRIM(@Khoi));

    -- 2. Kiểm tra trùng tên lớp
    IF EXISTS (SELECT 1 FROM LopHoc WHERE TenLop = @TenLop)
    BEGIN
        -- Nếu trùng thì dừng lại, không thêm
        RETURN;
    END

    -- 3. Thêm lớp mới
    INSERT INTO LopHoc (TenLop, Khoi, SoLuongHocSinh) 
    VALUES (@TenLop, @Khoi, @SoLuong);
END
GO

-- 3. Sửa Lớp
CREATE OR ALTER PROCEDURE sp_UpdateLop 
    @MaLop INT, 
    @TenLop NVARCHAR(50), 
    @Khoi NVARCHAR(20), 
    @SoLuongHocSinh INT 
AS 
BEGIN
    UPDATE LopHoc 
    SET TenLop = @TenLop, Khoi = @Khoi, SoLuongHocSinh = @SoLuongHocSinh 
    WHERE MaLop = @MaLop;
END
GO

-- 4. Xóa Lớp
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_DeleteLopHoc')
    DROP PROCEDURE sp_DeleteLopHoc
GO

CREATE PROCEDURE sp_DeleteLopHoc
    @MaLop INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRAN;

        -- 1. Kiểm tra xem lớp có học sinh không
        DECLARE @SoHocSinh INT;
        -- Kiểm tra bảng HocSinh có tồn tại không
        IF OBJECT_ID('dbo.HocSinh', 'U') IS NOT NULL
        BEGIN
            SELECT @SoHocSinh = COUNT(*) FROM HocSinh WHERE MaLop = @MaLop;
            
            IF (@SoHocSinh > 0)
            BEGIN
                -- Nếu còn học sinh thì rollback và báo lỗi
                ROLLBACK TRAN;
                RAISERROR (N'Không thể xóa lớp này vì vẫn còn %d học sinh.', 16, 1, @SoHocSinh);
                RETURN;
            END
        END

        -- 2. Xóa dữ liệu trong bảng Phân Công Giảng Dạy (Dọn đường)
        IF OBJECT_ID('dbo.PhanCongGiangDay', 'U') IS NOT NULL
            DELETE FROM PhanCongGiangDay WHERE MaLop = @MaLop;

        -- 3. Xóa Lớp học
        DELETE FROM LopHoc WHERE MaLop = @MaLop;

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        -- Nếu có lỗi thì hoàn tác
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        
        DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrMsg, 16, 1);
    END CATCH
END
GO


-- 5. Lấy lớp đơn giản (Cho ComboBox)
CREATE OR ALTER PROCEDURE sp_GetListLopSimple
AS
BEGIN
    SELECT MaLop, TenLop FROM LopHoc;
END
GO
ALTER TABLE GiaoVien ADD BiKhoa BIT DEFAULT 0;
GO
CREATE OR ALTER PROCEDURE sp_KhoaMoTaiKhoan
    @ID_GV INT,
    @TrangThai BIT -- 1 là Khóa, 0 là Mở
AS
BEGIN
    UPDATE GiaoVien SET BiKhoa = @TrangThai WHERE ID_GV = @ID_GV;
END
GO

-- 4. QUAN TRỌNG: Cập nhật thủ tục Đăng Nhập để chặn tài khoản bị khóa
CREATE OR ALTER PROCEDURE sp_DangNhapGiaoVien 
    @Email_GV VARCHAR(100), 
    @Password_GV VARCHAR(100) 
AS 
BEGIN 
    -- Kiểm tra xem có tài khoản nào đúng Email + Pass không
    DECLARE @ID INT;
    SELECT @ID = ID_GV FROM GiaoVien WHERE Email_GV = @Email_GV AND Password_GV = @Password_GV;

    IF @ID IS NOT NULL
    BEGIN
        -- Kiểm tra xem có bị khóa không
        IF EXISTS (SELECT 1 FROM GiaoVien WHERE ID_GV = @ID AND BiKhoa = 1)
        BEGIN
            -- Trả về bảng rỗng hoặc không trả về gì để báo lỗi đăng nhập
            RETURN; 
        END
        
        -- Nếu không bị khóa thì trả về thông tin đăng nhập
        SELECT ID_GV, HoTen, Email_GV, Password_GV FROM GiaoVien WHERE ID_GV = @ID;
    END
END
GO
CREATE OR ALTER PROCEDURE sp_GetAllGiaoVien_ChiTiet
AS
BEGIN
    SELECT 
        GV.ID_GV, 
        GV.HoTen, 
        GV.Email_GV, 
        GV.SoDienThoai,
		GV.NgaySinh,   -- Thêm cái này
        GV.GioiTinh,
		ISNULL(GV.BiKhoa, 0) AS BiKhoa,

        -- Kỹ thuật gộp chuỗi: "10A1 (Toán), 10A2 (Lý)"
        -- Lấy dữ liệu từ bảng PhanCongGiangDay mới
        ISNULL(STUFF((
            SELECT ', ' + LH.TenLop + N' (' + MH.TenMon + N')'
            FROM PhanCongGiangDay PC
            JOIN LopHoc LH ON PC.MaLop = LH.MaLop
            JOIN MonHoc MH ON PC.MaMon = MH.MaMon
            WHERE PC.ID_GV = GV.ID_GV
            FOR XML PATH('')
        ), 1, 2, ''), N'Chưa phân công') AS ChiTietPhanCong

    FROM GiaoVien GV
    ORDER BY GV.ID_GV DESC;
END
GO
CREATE OR ALTER PROCEDURE sp_XoaPhanCongCuaGV
    @ID_GV INT
AS
BEGIN
    DELETE FROM PhanCongGiangDay WHERE ID_GV = @ID_GV;
END
GO

-- 2. Thủ tục Thêm phân công mới (Chắc chắn bạn cũng sẽ cần cái này)
CREATE OR ALTER PROCEDURE sp_ThemPhanCong
    @ID_GV INT,
    @MaLop INT,
    @MaMon INT
AS
BEGIN
    -- Kiểm tra:
    -- Nếu Lớp đó + Môn đó đã có ai dạy rồi -> Cập nhật thành GV này
    IF EXISTS (SELECT 1 FROM PhanCongGiangDay WHERE MaLop = @MaLop AND MaMon = @MaMon)
    BEGIN
        UPDATE PhanCongGiangDay 
        SET ID_GV = @ID_GV 
        WHERE MaLop = @MaLop AND MaMon = @MaMon;
    END
    ELSE
    -- Nếu chưa có ai dạy -> Thêm mới
    BEGIN
        INSERT INTO PhanCongGiangDay(ID_GV, MaLop, MaMon) 
        VALUES (@ID_GV, @MaLop, @MaMon);
    END
END
GO
-----------------------------------------
-- XÓA PROC sp_GetHocSinhByLop NẾU TỒN TẠI
-----------------------------------------
IF OBJECT_ID('dbo.sp_GetHocSinhByLop', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_GetHocSinhByLop;
GO

-----------------------------------------
-- TẠO LẠI PROC sp_GetHocSinhByLop
-----------------------------------------
CREATE PROCEDURE dbo.sp_GetHocSinhByLop
    @MaLop INT
AS
BEGIN
    SELECT 
        hs.STT, 
        hs.HoTen, 
        hs.NgaySinh, 
        hs.GioiTinh, 
        hs.DanToc, 
        lh.TenLop 
    FROM HocSinh hs 
    JOIN LopHoc lh ON lh.MaLop = hs.MaLop 
    WHERE hs.MaLop = @MaLop;
END
GO

IF OBJECT_ID('dbo.sp_InsertHocSinh', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_InsertHocSinh;
GO

CREATE PROCEDURE dbo.sp_InsertHocSinh
    @HoTen NVARCHAR(100),
    @NgaySinh DATE,
    @GioiTinh NVARCHAR(5),
    @MaLop INT,
    @DanToc NVARCHAR(20)
AS
BEGIN
    INSERT INTO HocSinh(HoTen, NgaySinh, GioiTinh, MaLop, DanToc) 
    VALUES (@HoTen, @NgaySinh, @GioiTinh, @MaLop, @DanToc);
END
GO

CREATE OR ALTER PROCEDURE sp_InsertHocSinh
    @HoTen NVARCHAR(100),
    @NgaySinh DATE,
    @GioiTinh NVARCHAR(5),
    @MaLop INT,
    @DanToc NVARCHAR(20)
AS
BEGIN
    INSERT INTO HocSinh(HoTen, NgaySinh, GioiTinh, MaLop, DanToc) 
    VALUES (@HoTen, @NgaySinh, @GioiTinh, @MaLop, @DanToc);
END
GO

-- Nếu procedure tồn tại thì xoá
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_InsertGiaoVien')
    DROP PROCEDURE sp_InsertGiaoVien
GO

CREATE PROCEDURE sp_InsertGiaoVien
    @HoTen NVARCHAR(100),
    @Email_GV VARCHAR(100),
    @Password_GV VARCHAR(100)
    -- Thêm các cột khác nếu có (SĐT, Ngày sinh...)
AS
BEGIN
    -- 1. KIỂM TRA TRÙNG EMAIL
    -- Chỉ cần tìm xem Email này đã tồn tại trong bảng chưa
    IF EXISTS (SELECT 1 FROM GiaoVien WHERE Email_GV = @Email_GV)
    BEGIN
        -- Nếu có rồi -> Báo lỗi
        RAISERROR (N'Email này đã tồn tại! Vui lòng chọn Email khác.', 16, 1);
        RETURN;
    END

    -- 2. THÊM MỚI
    INSERT INTO GiaoVien (HoTen, Email_GV, Password_GV, BiKhoa)
    VALUES (@HoTen, @Email_GV, @Password_GV, 0); -- Mặc định BiKhoa = 0 (false)
END
GO

CREATE OR ALTER PROCEDURE sp_UpdateHocSinh
    @STT INT,
    @HoTen NVARCHAR(100),
    @NgaySinh DATE,
    @GioiTinh NVARCHAR(5),
    @DanToc NVARCHAR(20)
AS
BEGIN
    UPDATE HocSinh 
    SET HoTen = @HoTen, 
        NgaySinh = @NgaySinh, 
        GioiTinh = @GioiTinh, 
        DanToc = @DanToc
    WHERE STT = @STT;
END
GO
CREATE OR ALTER PROCEDURE sp_GetHocSinhByID
    @STT INT
AS
BEGIN
    SELECT * FROM HocSinh WHERE STT = @STT;
END
GO
CREATE OR ALTER PROCEDURE sp_GetLopDayCuaGiaoVien
    @ID_GV INT
AS
BEGIN
    SELECT 
        LH.MaLop,
        LH.TenLop,
        LH.SoLuongHocSinh,
        MH.TenMon,
        PC.MaMon, -- THÊM CỘT NÀY ĐỂ CODE C# BIẾT LỚP NÀY DẠY MÔN GÌ
        PC.VaiTro
    FROM PhanCongGiangDay PC
    JOIN LopHoc LH ON PC.MaLop = LH.MaLop
    JOIN MonHoc MH ON PC.MaMon = MH.MaMon
    WHERE PC.ID_GV = @ID_GV
END
GO

CREATE OR ALTER TRIGGER trg_UpdateSoLuongHS 
ON HocSinh 
AFTER INSERT, DELETE, UPDATE
AS 
BEGIN
    SET NOCOUNT ON;
    
    -- Danh sách các lớp bị ảnh hưởng (có học sinh thêm vào, xóa đi, hoặc chuyển lớp)
    WITH AffectedLop AS (
        SELECT MaLop FROM inserted WHERE MaLop IS NOT NULL
        UNION 
        SELECT MaLop FROM deleted WHERE MaLop IS NOT NULL
    )
    -- Cập nhật lại số lượng cho các lớp đó
    UPDATE L
    SET SoLuongHocSinh = (SELECT COUNT(*) FROM HocSinh HS WHERE HS.MaLop = L.MaLop)
    FROM LopHoc L
    WHERE L.MaLop IN (SELECT MaLop FROM AffectedLop);
END;
GO
USE QuanLyHocSinh;
GO

-- 1. CẬP NHẬT THỦ TỤC LẤY DANH SÁCH (TÍCH HỢP TÍNH TOÁN)
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[KetQuaHocTap]') AND name = 'TuyenDuong')
BEGIN
    ALTER TABLE KetQuaHocTap ADD TuyenDuong BIT DEFAULT 0;
END
GO

-- 2. Sửa lại thủ tục lấy dữ liệu (Lấy cột thật thay vì tính toán)
-- Xoá procedure nếu đã tồn tại
IF OBJECT_ID('sp_GetChiTietHocSinhByLop', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetChiTietHocSinhByLop;
GO

-- Tạo lại procedure
CREATE PROCEDURE sp_GetChiTietHocSinhByLop
    @MaLop INT,
    @MaMon INT
AS
BEGIN
    WITH BangDiem AS (
        SELECT 
            hs.STT,
            hs.HoTen,
            kq.GK1, kq.CK1,
            CAST((ISNULL(kq.GK1, 0) + ISNULL(kq.CK1, 0)) / 2.0 AS DECIMAL(10, 1)) AS DiemHK1,
            kq.GK2, kq.CK2,
            CAST((ISNULL(kq.GK2, 0) + ISNULL(kq.CK2, 0)) / 2.0 AS DECIMAL(10, 1)) AS DiemHK2,

            -- ĐTB năm (HK2 hệ số 2)
            CAST(
                (
                    ((ISNULL(kq.GK1, 0) + ISNULL(kq.CK1, 0)) / 2.0) + 
                    (((ISNULL(kq.GK2, 0) + ISNULL(kq.CK2, 0)) / 2.0) * 2)
                ) / 3.0 
            AS DECIMAL(10, 1)) AS DiemTB,

            -- Lấy đúng ghi chú theo môn
            (SELECT TOP 1 NhanXet 
             FROM Notes n 
             WHERE n.STT = hs.STT AND n.MaMon = @MaMon) AS Notes,

            ISNULL(kq.TuyenDuong, 0) AS TuyenDuong
        FROM HocSinh hs
        LEFT JOIN KetQuaHocTap kq 
            ON hs.STT = kq.STT AND kq.MaMon = @MaMon
        WHERE hs.MaLop = @MaLop
    )
    SELECT *,
        CASE 
            WHEN DiemTB IS NULL THEN N''
            WHEN DiemTB >= 8.0 THEN N'Giỏi'
            WHEN DiemTB >= 6.5 THEN N'Khá'
            WHEN DiemTB >= 5.0 THEN N'Trung Bình'
            ELSE N'Yếu'
        END AS XepLoai
    FROM BangDiem;
END
GO


-- =============================================================================
-- 4. THỦ TỤC LẤY 1 NOTE CỤ THỂ (Dùng cho Form Note Center)
-- =============================================================================
CREATE OR ALTER PROCEDURE sp_GetNoteHocSinh
    @STT INT,
    @MaMon INT
AS
BEGIN
    SELECT NhanXet FROM Notes WHERE STT = @STT AND MaMon = @MaMon
END
GO

-- 3. Tạo thủ tục cập nhật riêng cho Tuyên Dương (Để code chạy nhanh)
CREATE OR ALTER PROCEDURE sp_UpdateTuyenDuong
    @STT INT,
    @MaMon INT,
    @TrangThai BIT
AS
BEGIN
    -- Nếu chưa có dòng kết quả thì thêm mới
    IF NOT EXISTS (SELECT 1 FROM KetQuaHocTap WHERE STT = @STT AND MaMon = @MaMon)
    BEGIN
        INSERT INTO KetQuaHocTap(STT, MaMon, TuyenDuong) VALUES (@STT, @MaMon, @TrangThai)
    END
    ELSE
    -- Nếu có rồi thì cập nhật cột TuyenDuong
    BEGIN
        UPDATE KetQuaHocTap SET TuyenDuong = @TrangThai WHERE STT = @STT AND MaMon = @MaMon
    END
END
GO

-- 2. THỦ TỤC CẬP NHẬT ĐIỂM (GIỮ NGUYÊN NHƯ BẠN ĐÃ CÓ - CHỈ ĐẢM BẢO NÓ TỒN TẠI)
CREATE OR ALTER PROCEDURE sp_UpdateDiemHocSinh
    @STT INT,
    @MaMon INT, 
    @GK1 FLOAT, @CK1 FLOAT, @GK2 FLOAT, @CK2 FLOAT
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM KetQuaHocTap WHERE STT = @STT AND MaMon = @MaMon)
    BEGIN
        INSERT INTO KetQuaHocTap(STT, MaMon, GK1, CK1, GK2, CK2) 
        VALUES (@STT, @MaMon, @GK1, @CK1, @GK2, @CK2)
    END
    ELSE
    BEGIN
        UPDATE KetQuaHocTap 
        SET GK1 = @GK1, CK1 = @CK1, GK2 = @GK2, CK2 = @CK2
        WHERE STT = @STT AND MaMon = @MaMon
    END
END
GO
CREATE OR ALTER PROCEDURE sp_ThongKeXepLoai
    @MaLop INT,
    @MaMon INT
AS
BEGIN
    -- 1. Tính điểm trung bình cho từng học sinh
    WITH BangDiem AS (
        SELECT 
            hs.STT,
            -- Công thức: (TB_HK1 + TB_HK2 * 2) / 3
            CAST(
                (
                    ((ISNULL(kq.GK1, 0) + ISNULL(kq.CK1, 0)) / 2.0) -- TB HK1
                    + 
                    (((ISNULL(kq.GK2, 0) + ISNULL(kq.CK2, 0)) / 2.0) * 2) -- TB HK2 nhân 2
                ) / 3.0 
            AS DECIMAL(10, 1)) AS DiemTB
        FROM HocSinh hs
        LEFT JOIN KetQuaHocTap kq ON hs.STT = kq.STT AND kq.MaMon = @MaMon
        WHERE hs.MaLop = @MaLop
    )
    -- 2. Đếm số lượng theo xếp loại
    SELECT 
        XepLoai, 
        COUNT(*) as SoLuong
    FROM (
        SELECT 
            CASE 
                WHEN DiemTB >= 8.0 THEN N'Giỏi'
                WHEN DiemTB >= 6.5 THEN N'Khá'
                WHEN DiemTB >= 5.0 THEN N'Trung Bình'
                ELSE N'Yếu' 
            END AS XepLoai
        FROM BangDiem
    ) AS PhanLoai
    GROUP BY XepLoai
END
GO
-- =============================================================================
-- VI. DỮ LIỆU MẪU (Sample Data)
-- =============================================================================

INSERT INTO Admin (Email_Admin, Password_Admin) VALUES ('admin@gmail.com', 'admin123');

-- 3. Thêm Giáo Viên
INSERT INTO GiaoVien (HoTen, Email_GV, Password_GV) VALUES 
(N'Trần Phúc', 'tranphuc@gmail.com', '123456'),
(N'Hương Nguyễn', 'huongkiute@gmail.com', '123456'),
(N'Thanh Ngôn', 'thanhngon@gmail.com', '123456');

GO
PRINT N'=== CÀI ĐẶT DB HOÀN TẤT VỚI LOGIC PHÂN CÔNG MỚI ===';
-- Kiểm tra kết quả hiển thị
EXEC sp_GetAllGiaoVien_ChiTiet;
select  *FROM Admin;
select  *FROM GiaoVien;
select  *FROM MonHoc;

select  *FROM PhanCongGiangDay;
select  *FROM HocSinh;
select  *FROM LopHoc;
-- -----------------------------------------------------------------------------
-- 1. TẠO LẠI BẢNG LichHen VỚI CỘT MỚI
-- -----------------------------------------------------------------------------

CREATE TABLE LichHen (
    ID_LichHen INT PRIMARY KEY IDENTITY(1,1),
    ID_GV INT NOT NULL,
    TieuDe NVARCHAR(MAX) NOT NULL,
    MoTa NVARCHAR(MAX),
    ThoiGianBatDau DATETIME NOT NULL,
    ThoiGianKetThuc DATETIME NOT NULL,
    MauSuKien VARCHAR(50), -- Lưu mã màu ARGB dưới dạng chuỗi số nguyên

    -- THAY ĐỔI QUAN TRỌNG: Thêm cột mới để lưu thời gian nhắc nhở cụ thể.
    -- Cho phép NULL vì không phải sự kiện nào cũng có nhắc nhở.
    ThoiGianNhacNho DATETIME NULL,

    CONSTRAINT FK_LichHen_GiaoVien FOREIGN KEY (ID_GV) REFERENCES GiaoVien(ID_GV) ON DELETE CASCADE
);
GO

PRINT 'Đã tạo lại bảng LichHen với cột ThoiGianNhacNho thành công.';
GO

-- -----------------------------------------------------------------------------
-- 2. CẬP NHẬT CÁC STORED PROCEDURE
-- -----------------------------------------------------------------------------

-- LẤY SỰ KIỆN CỦA GIÁO VIÊN ĐÃ ĐĂNG NHẬP
CREATE OR ALTER PROCEDURE sp_GetLichHenByGiaoVien
    @ID_GV INT
AS
BEGIN
    SET NOCOUNT ON;
    -- THÊM: Lấy thêm cột ThoiGianNhacNho
    SELECT ID_LichHen, TieuDe, MoTa, ThoiGianBatDau, ThoiGianKetThuc, MauSuKien, ThoiGianNhacNho
    FROM LichHen
    WHERE ID_GV = @ID_GV;
END
GO
PRINT 'Đã cập nhật sp_GetLichHenByGiaoVien.';
GO

-- THÊM SỰ KIỆN MỚI
CREATE OR ALTER PROCEDURE sp_InsertLichHen
    @ID_GV INT,
    @TieuDe NVARCHAR(MAX),
    @MoTa NVARCHAR(MAX),
    @ThoiGianBatDau DATETIME,
    @ThoiGianKetThuc DATETIME,
    @MauSuKien VARCHAR(50),
    @ThoiGianNhacNho DATETIME = NULL -- THÊM: Tham số mới, mặc định là NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO LichHen (ID_GV, TieuDe, MoTa, ThoiGianBatDau, ThoiGianKetThuc, MauSuKien, ThoiGianNhacNho)
    VALUES (@ID_GV, @TieuDe, @MoTa, @ThoiGianBatDau, @ThoiGianKetThuc, @MauSuKien, @ThoiGianNhacNho);
END
GO
PRINT 'Đã cập nhật sp_InsertLichHen.';
GO

-- CẬP NHẬT (SỬA) MỘT SỰ KIỆN ĐÃ CÓ
CREATE OR ALTER PROCEDURE sp_UpdateLichHen
    @ID_LichHen INT,
    @TieuDe NVARCHAR(MAX),
    @MoTa NVARCHAR(MAX),
    @ThoiGianBatDau DATETIME,
    @ThoiGianKetThuc DATETIME,
    @MauSuKien VARCHAR(50),
    @ThoiGianNhacNho DATETIME = NULL -- THÊM: Tham số mới
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE LichHen
    SET
        TieuDe = @TieuDe,
        MoTa = @MoTa,
        ThoiGianBatDau = @ThoiGianBatDau,
        ThoiGianKetThuc = @ThoiGianKetThuc,
        MauSuKien = @MauSuKien,
        ThoiGianNhacNho = @ThoiGianNhacNho -- THÊM: Cập nhật cột mới
    WHERE
        ID_LichHen = @ID_LichHen;
END
GO
PRINT 'Đã cập nhật sp_UpdateLichHen.';
GO

-- XÓA MỘT SỰ KIỆN (Không cần thay đổi)
CREATE OR ALTER PROCEDURE sp_DeleteLichHen
    @ID_LichHen INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM LichHen
    WHERE ID_LichHen = @ID_LichHen;
END
GO
PRINT 'sp_DeleteLichHen vẫn giữ nguyên và hợp lệ.';
GO
SELECT * FROM LichHen;

-- =============================================================================
-- I. ĐỊNH NGHĨA BẢNG MỚI CHO LỊCH SỬ GAME
-- =============================================================================

-- Bảng lưu thông tin chung về mỗi lần tạo game
CREATE TABLE LichSuGame_NgheChonHinh (
    ID_GameInstance INT PRIMARY KEY IDENTITY(1,1),
    TenGame NVARCHAR(200) NOT NULL,
    NgayTao DATETIME DEFAULT GETDATE(),
    ID_GV INT NOT NULL, -- Ai đã tạo game này
    CONSTRAINT FK_LichSuGame_GiaoVien FOREIGN KEY (ID_GV) REFERENCES GiaoVien(ID_GV) ON DELETE CASCADE
);
GO

-- Bảng lưu chi tiết các câu hỏi cho mỗi game instance
CREATE TABLE CauHoi_NgheChonHinh (
    ID_CauHoi INT PRIMARY KEY IDENTITY(1,1),
    ID_GameInstance INT NOT NULL,
    DuongDanAnh1 NVARCHAR(MAX) NOT NULL,
    DuongDanAnh2 NVARCHAR(MAX) NOT NULL,
    DuongDanAnh3 NVARCHAR(MAX) NOT NULL,
    DuongDanAmThanh NVARCHAR(MAX) NOT NULL,
    DapAnDung INT NOT NULL, -- Sẽ là 0, 1, hoặc 2
    CONSTRAINT FK_CauHoi_LichSuGame FOREIGN KEY (ID_GameInstance) REFERENCES LichSuGame_NgheChonHinh(ID_GameInstance) ON DELETE CASCADE
);
GO

PRINT 'Đã tạo bảng LichSuGame_NgheChonHinh và CauHoi_NgheChonHinh thành công.';
GO

-- =============================================================================
-- II. ĐỊNH NGHĨA STORED PROCEDURE CHO LỊCH SỬ GAME
-- =============================================================================

-- 1. Định nghĩa một kiểu dữ liệu bảng để gửi danh sách câu hỏi từ C# sang SQL
--    Điều này hiệu quả hơn nhiều so với việc gọi lệnh INSERT nhiều lần.
CREATE TYPE dbo.CauHoiTableType AS TABLE (
    DuongDanAnh1 NVARCHAR(MAX),
    DuongDanAnh2 NVARCHAR(MAX),
    DuongDanAnh3 NVARCHAR(MAX),
    DuongDanAmThanh NVARCHAR(MAX),
    DapAnDung INT
);
GO

-- 2. Stored Procedure để LƯU một game mới và các câu hỏi của nó
CREATE OR ALTER PROCEDURE sp_LuuGame_NgheChonHinh
    @TenGame NVARCHAR(200),
    @ID_GV INT,
    @DanhSachCauHoi dbo.CauHoiTableType READONLY -- Sử dụng kiểu dữ liệu bảng vừa tạo
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @NewGameInstanceID INT;

    -- Bắt đầu một transaction để đảm bảo tất cả các hoạt động thành công hoặc không gì cả
    BEGIN TRANSACTION;
    
    BEGIN TRY
        -- Thêm bản ghi vào bảng lịch sử game chính
        INSERT INTO LichSuGame_NgheChonHinh (TenGame, ID_GV)
        VALUES (@TenGame, @ID_GV);

        -- Lấy ID của game vừa được tạo
        SET @NewGameInstanceID = SCOPE_IDENTITY();

        -- Thêm tất cả các câu hỏi vào bảng chi tiết câu hỏi, liên kết với ID game vừa tạo
        INSERT INTO CauHoi_NgheChonHinh (ID_GameInstance, DuongDanAnh1, DuongDanAnh2, DuongDanAnh3, DuongDanAmThanh, DapAnDung)
        SELECT 
            @NewGameInstanceID,
            ch.DuongDanAnh1,
            ch.DuongDanAnh2,
            ch.DuongDanAnh3,
            ch.DuongDanAmThanh,
            ch.DapAnDung
        FROM @DanhSachCauHoi ch;

        -- Nếu mọi thứ thành công, commit transaction
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Nếu có lỗi, rollback tất cả các thay đổi
        ROLLBACK TRANSACTION;
        -- Ném lại lỗi để ứng dụng C# có thể bắt được
        THROW;
    END CATCH
END
GO

-- 3. Stored Procedure để LẤY danh sách các game đã tạo bởi một giáo viên
CREATE OR ALTER PROCEDURE sp_GetLichSuGame_NgheChonHinh
    @ID_GV INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ID_GameInstance, TenGame, NgayTao
    FROM LichSuGame_NgheChonHinh
    WHERE ID_GV = @ID_GV
    ORDER BY NgayTao DESC; -- Sắp xếp game mới nhất lên đầu
END
GO

-- 4. Stored Procedure để LẤY chi tiết các câu hỏi của một game cụ thể để chơi lại
CREATE OR ALTER PROCEDURE sp_GetChiTietGame_NgheChonHinh
    @ID_GameInstance INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT DuongDanAnh1, DuongDanAnh2, DuongDanAnh3, DuongDanAmThanh, DapAnDung
    FROM CauHoi_NgheChonHinh
    WHERE ID_GameInstance = @ID_GameInstance;
END
GO
CREATE OR ALTER PROCEDURE [dbo].[sp_XoaGame_NgheChonHinh]
    @ID_GameInstance INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;

    BEGIN TRY
        -- 1. Xóa các câu hỏi trong bảng chi tiết trước
        -- Tên bảng đúng dựa trên sp_LuuGame của bạn là: CauHoi_NgheChonHinh
        DELETE FROM CauHoi_NgheChonHinh 
        WHERE ID_GameInstance = @ID_GameInstance;

        -- 2. Sau đó xóa thông tin game trong bảng lịch sử
        -- Tên bảng đúng dựa trên sp_LuuGame của bạn là: LichSuGame_NgheChonHinh
        DELETE FROM LichSuGame_NgheChonHinh 
        WHERE ID_GameInstance = @ID_GameInstance;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO

PRINT 'Đã tạo các Stored Procedure cho lịch sử game thành công.';
GO

SELECT * FROM LichSuGame_NgheChonHinh;
SELECT * FROM CauHoi_NgheChonHinh;
-- =============================================================================
-- I. ĐỊNH NGHĨA BẢNG
-- =============================================================================

-- 1. Bảng lưu thông tin chung về mỗi lần tạo game
CREATE TABLE LichSuGame_RandomSo (
    ID_GameInstance INT PRIMARY KEY IDENTITY(1,1),
    TenGame NVARCHAR(200) NOT NULL,
    NgayTao DATETIME DEFAULT GETDATE(),
    ID_GV INT NOT NULL,
    -- Khóa ngoại liên kết tới bảng GiaoVien, tự động xóa game nếu giáo viên bị xóa
    CONSTRAINT FK_LichSuGameRS_GiaoVien FOREIGN KEY (ID_GV) REFERENCES GiaoVien(ID_GV) ON DELETE CASCADE
);
GO

-- 2. Bảng lưu chi tiết các câu hỏi cho mỗi game
CREATE TABLE CauHoi_RandomSo (
    ID_CauHoi INT PRIMARY KEY IDENTITY(1,1),
    ID_GameInstance INT NOT NULL,
    SoThuTu INT NOT NULL,
    NoiDungVanBan NVARCHAR(MAX),
    DuongDanAnh NVARCHAR(MAX),
    -- Khóa ngoại liên kết tới bảng LichSuGame, tự động xóa câu hỏi nếu game bị xóa
    CONSTRAINT FK_CauHoiRS_LichSuGame FOREIGN KEY (ID_GameInstance) REFERENCES LichSuGame_RandomSo(ID_GameInstance) ON DELETE CASCADE
);
GO

PRINT 'Đã tạo 2 bảng LichSuGame_RandomSo và CauHoi_RandomSo thành công.';
GO


-- =============================================================================
-- II. ĐỊNH NGHĨA KIỂU DỮ LIỆU BẢNG (TABLE TYPE)
-- Dùng để gửi danh sách câu hỏi từ C# sang SQL hiệu quả.
-- =============================================================================

CREATE TYPE dbo.CauHoiRandomSoTableType AS TABLE (
    SoThuTu INT,
    NoiDungVanBan NVARCHAR(MAX),
    DuongDanAnh NVARCHAR(MAX)
);
GO

PRINT 'Đã tạo kiểu dữ liệu bảng CauHoiRandomSoTableType thành công.';
GO


-- =============================================================================
-- III. ĐỊNH NGHĨA CÁC THỦ TỤC LƯU TRỮ (STORED PROCEDURES)
-- =============================================================================

-- 1. Thủ tục để LƯU một game mới vào lịch sử
CREATE OR ALTER PROCEDURE sp_LuuGame_RandomSo
    @TenGame NVARCHAR(200),
    @ID_GV INT,
    @DanhSachCauHoi dbo.CauHoiRandomSoTableType READONLY
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @NewGameInstanceID INT;

    BEGIN TRANSACTION;
    BEGIN TRY
        -- Thêm vào bảng chính
        INSERT INTO LichSuGame_RandomSo (TenGame, ID_GV)
        VALUES (@TenGame, @ID_GV);

        -- Lấy ID vừa tạo
        SET @NewGameInstanceID = SCOPE_IDENTITY();

        -- Thêm tất cả câu hỏi vào bảng chi tiết
        INSERT INTO CauHoi_RandomSo (ID_GameInstance, SoThuTu, NoiDungVanBan, DuongDanAnh)
        SELECT 
            @NewGameInstanceID,
            ch.SoThuTu,
            ch.NoiDungVanBan,
            ch.DuongDanAnh
        FROM @DanhSachCauHoi ch;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO
PRINT 'Đã tạo thủ tục sp_LuuGame_RandomSo.';
GO

-- 2. Thủ tục để LẤY DANH SÁCH các game đã lưu của một giáo viên
CREATE OR ALTER PROCEDURE sp_GetLichSuGame_RandomSo
    @ID_GV INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ID_GameInstance, TenGame, NgayTao
    FROM LichSuGame_RandomSo
    WHERE ID_GV = @ID_GV
    ORDER BY NgayTao DESC;
END
GO
PRINT 'Đã tạo thủ tục sp_GetLichSuGame_RandomSo.';
GO

-- 3. Thủ tục để LẤY CHI TIẾT các câu hỏi của một game để chơi lại
CREATE OR ALTER PROCEDURE sp_GetChiTietGame_RandomSo
    @ID_GameInstance INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        SoThuTu AS QuestionNumber, 
        NoiDungVanBan AS QuestionText, 
        DuongDanAnh AS ImagePath
    FROM CauHoi_RandomSo
    WHERE ID_GameInstance = @ID_GameInstance
    ORDER BY SoThuTu ASC;
END
GO
PRINT 'Đã tạo thủ tục sp_GetChiTietGame_RandomSo.';
GO

-- 4. Thủ tục để XÓA một game đã lưu khỏi lịch sử
CREATE OR ALTER PROCEDURE sp_XoaGame_RandomSo
    @ID_GameInstance INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM LichSuGame_RandomSo
    WHERE ID_GameInstance = @ID_GameInstance;
END
GO
PRINT 'Đã tạo thủ tục sp_XoaGame_RandomSo.';
GO

PRINT '--- Script hoàn tất ---';
GO

-- Lệnh kiểm tra nhanh (tùy chọn)
SELECT * FROM LichSuGame_RandomSo;
SELECT * FROM CauHoi_RandomSo;

-- =============================================================================
-- I. ĐỊNH NGHĨA BẢNG CHO LỊCH SỬ GAME FLASHCARD
-- =============================================================================

-- Bảng lưu thông tin chung về mỗi bộ thẻ đã tạo
CREATE TABLE LichSuGame_Flashcard (
    ID_GameInstance INT PRIMARY KEY IDENTITY(1,1),
    TenBoThe NVARCHAR(200) NOT NULL,
    NgayTao DATETIME DEFAULT GETDATE(),
    ID_GV INT NOT NULL,
    CONSTRAINT FK_LichSuFlashcard_GiaoVien FOREIGN KEY (ID_GV) REFERENCES GiaoVien(ID_GV) ON DELETE CASCADE
);
GO

-- Bảng lưu chi tiết từng thẻ flashcard cho mỗi bộ thẻ
CREATE TABLE ChiTietThe_Flashcard (
    ID_The INT PRIMARY KEY IDENTITY(1,1),
    ID_GameInstance INT NOT NULL,
    FrontText NVARCHAR(MAX),
    BackText NVARCHAR(MAX),
    FrontImagePath NVARCHAR(MAX),
    BackImagePath NVARCHAR(MAX),
    CONSTRAINT FK_ChiTietThe_LichSu FOREIGN KEY (ID_GameInstance) REFERENCES LichSuGame_Flashcard(ID_GameInstance) ON DELETE CASCADE
);
GO

PRINT 'Đã tạo bảng LichSuGame_Flashcard và ChiTietThe_Flashcard thành công.';
GO

-- =============================================================================
-- II. ĐỊNH NGHĨA KIỂU DỮ LIỆU BẢNG (TABLE TYPE)
-- Dùng để gửi danh sách thẻ từ C# sang SQL hiệu quả.
-- =============================================================================

CREATE TYPE dbo.FlashcardTableType AS TABLE (
    FrontText NVARCHAR(MAX),
    BackText NVARCHAR(MAX),
    FrontImagePath NVARCHAR(MAX),
    BackImagePath NVARCHAR(MAX)
);
GO

PRINT 'Đã tạo kiểu dữ liệu bảng FlashcardTableType thành công.';
GO

-- =============================================================================
-- III. ĐỊNH NGHĨA CÁC THỦ TỤC LƯU TRỮ (STORED PROCEDURES)
-- =============================================================================

-- 1. Thủ tục để LƯU một bộ thẻ mới vào lịch sử
CREATE OR ALTER PROCEDURE sp_LuuBoThe_Flashcard
    @TenBoThe NVARCHAR(200),
    @ID_GV INT,
    @DanhSachThe dbo.FlashcardTableType READONLY
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @NewGameInstanceID INT;

    BEGIN TRANSACTION;
    BEGIN TRY
        -- Thêm vào bảng chính
        INSERT INTO LichSuGame_Flashcard (TenBoThe, ID_GV)
        VALUES (@TenBoThe, @ID_GV);

        -- Lấy ID vừa tạo
        SET @NewGameInstanceID = SCOPE_IDENTITY();

        -- Thêm tất cả thẻ vào bảng chi tiết
        INSERT INTO ChiTietThe_Flashcard (ID_GameInstance, FrontText, BackText, FrontImagePath, BackImagePath)
        SELECT
            @NewGameInstanceID,
            the.FrontText,
            the.BackText,
            the.FrontImagePath,
            the.BackImagePath
        FROM @DanhSachThe the;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO
PRINT 'Đã tạo thủ tục sp_LuuBoThe_Flashcard.';
GO

-- 2. Thủ tục để LẤY DANH SÁCH các bộ thẻ đã lưu của một giáo viên
CREATE OR ALTER PROCEDURE sp_GetLichSuBoThe_Flashcard
    @ID_GV INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT ID_GameInstance, TenBoThe, NgayTao
    FROM LichSuGame_Flashcard
    WHERE ID_GV = @ID_GV
    ORDER BY NgayTao DESC;
END
GO
PRINT 'Đã tạo thủ tục sp_GetLichSuBoThe_Flashcard.';
GO

-- 3. Thủ tục để LẤY CHI TIẾT các thẻ của một bộ thẻ để chơi lại
CREATE OR ALTER PROCEDURE sp_GetChiTietBoThe_Flashcard
    @ID_GameInstance INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT
        FrontText,
        BackText,
        FrontImagePath,
        BackImagePath
    FROM ChiTietThe_Flashcard
    WHERE ID_GameInstance = @ID_GameInstance;
END
GO
PRINT 'Đã tạo thủ tục sp_GetChiTietBoThe_Flashcard.';
GO

-- 4. Thủ tục để XÓA một bộ thẻ đã lưu khỏi lịch sử
CREATE OR ALTER PROCEDURE sp_XoaBoThe_Flashcard
    @ID_GameInstance INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM LichSuGame_Flashcard
    WHERE ID_GameInstance = @ID_GameInstance;
END
GO
PRINT 'Đã tạo thủ tục sp_XoaBoThe_Flashcard.';
GO

PRINT '--- Script cho Lịch sử Game Flashcard hoàn tất ---';
GO

SELECT * FROM LichSuGame_Flashcard;
SELECT * FROM ChiTietThe_Flashcard;

USE QuanLyHocSinh;
GO

-- =============================================================================
-- I. ĐỊNH NGHĨA BẢNG CHO LỊCH SỬ GAME SẮP XẾP CÂU
-- =============================================================================

-- 1. Bảng lưu thông tin chung về mỗi lần tạo game
CREATE TABLE LichSuGame_SapXepCau (
    ID_GameInstance INT PRIMARY KEY IDENTITY(1,1),
    TenGame NVARCHAR(200) NOT NULL,
    NgayTao DATETIME DEFAULT GETDATE(),
    ID_GV INT NOT NULL,
    -- Khóa ngoại liên kết tới bảng GiaoVien, tự động xóa game nếu giáo viên bị xóa
    CONSTRAINT FK_LichSuSXC_GiaoVien FOREIGN KEY (ID_GV) REFERENCES GiaoVien(ID_GV) ON DELETE CASCADE
);
GO
PRINT 'Đã tạo bảng LichSuGame_SapXepCau thành công.';
GO

-- 2. Bảng lưu chi tiết các câu hỏi (câu đúng) cho mỗi game
CREATE TABLE CauHoi_SapXepCau (
    ID_CauHoi INT PRIMARY KEY IDENTITY(1,1),
    ID_GameInstance INT NOT NULL,
    NoiDungCau NVARCHAR(MAX) NOT NULL,
    -- Khóa ngoại liên kết tới bảng LichSuGame, tự động xóa câu hỏi nếu game bị xóa
    CONSTRAINT FK_CauHoiSXC_LichSuGame FOREIGN KEY (ID_GameInstance) REFERENCES LichSuGame_SapXepCau(ID_GameInstance) ON DELETE CASCADE
);
GO
PRINT 'Đã tạo bảng CauHoi_SapXepCau thành công.';
GO


-- =============================================================================
-- II. ĐỊNH NGHĨA KIỂU DỮ LIỆU BẢNG (TABLE TYPE)
-- Dùng để gửi danh sách các câu từ C# sang SQL hiệu quả.
-- =============================================================================

CREATE TYPE dbo.SapXepCauTableType AS TABLE (
    NoiDungCau NVARCHAR(MAX) NOT NULL
);
GO
PRINT 'Đã tạo kiểu dữ liệu bảng SapXepCauTableType thành công.';
GO


-- =============================================================================
-- III. ĐỊNH NGHĨA CÁC THỦ TỤC LƯU TRỮ (STORED PROCEDURES)
-- =============================================================================

-- 1. Thủ tục để LƯU một game mới vào lịch sử
CREATE OR ALTER PROCEDURE sp_LuuGame_SapXepCau
    @TenGame NVARCHAR(200),
    @ID_GV INT,
    @DanhSachCauHoi dbo.SapXepCauTableType READONLY
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @NewGameInstanceID INT;

    BEGIN TRANSACTION;
    BEGIN TRY
        -- Thêm vào bảng chính để lấy ID
        INSERT INTO LichSuGame_SapXepCau (TenGame, ID_GV)
        VALUES (@TenGame, @ID_GV);

        -- Lấy ID của game vừa được tạo
        SET @NewGameInstanceID = SCOPE_IDENTITY();

        -- Thêm tất cả các câu từ Table Type vào bảng chi tiết
        INSERT INTO CauHoi_SapXepCau (ID_GameInstance, NoiDungCau)
        SELECT 
            @NewGameInstanceID,
            ch.NoiDungCau
        FROM @DanhSachCauHoi ch;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW; -- Ném lỗi ra để C# có thể bắt được
    END CATCH
END
GO
PRINT 'Đã tạo thủ tục sp_LuuGame_SapXepCau.';
GO

-- 2. Thủ tục để LẤY DANH SÁCH các game đã lưu của một giáo viên
CREATE OR ALTER PROCEDURE sp_GetLichSuGame_SapXepCau
    @ID_GV INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        ID_GameInstance, 
        TenGame, 
        NgayTao
    FROM LichSuGame_SapXepCau
    WHERE ID_GV = @ID_GV
    ORDER BY NgayTao DESC; -- Sắp xếp game mới nhất lên đầu
END
GO
PRINT 'Đã tạo thủ tục sp_GetLichSuGame_SapXepCau.';
GO

-- 3. Thủ tục để LẤY CHI TIẾT các câu của một game để chơi lại
CREATE OR ALTER PROCEDURE sp_GetChiTietGame_SapXepCau
    @ID_GameInstance INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        NoiDungCau
    FROM CauHoi_SapXepCau
    WHERE ID_GameInstance = @ID_GameInstance;
END
GO
PRINT 'Đã tạo thủ tục sp_GetChiTietGame_SapXepCau.';
GO

-- 4. Thủ tục để XÓA một game đã lưu khỏi lịch sử
CREATE OR ALTER PROCEDURE sp_XoaGame_SapXepCau
    @ID_GameInstance INT
AS
BEGIN
    SET NOCOUNT ON;
    -- Do đã có ràng buộc ON DELETE CASCADE, chỉ cần xóa bản ghi ở bảng chính
    -- là tất cả các câu hỏi liên quan ở bảng chi tiết sẽ tự động bị xóa.
    DELETE FROM LichSuGame_SapXepCau
    WHERE ID_GameInstance = @ID_GameInstance;
END
GO
PRINT 'Đã tạo thủ tục sp_XoaGame_SapXepCau.';
GO

PRINT '--- Script cho Lịch sử Game Sắp Xếp Câu đã hoàn tất ---';
GO
-- -----------------------------------------------------------------------------
--  QUẢN LÝ ĐIỂM DANH
-- -----------------------------------------------------------------------------
-- 1. CHỌN ĐÚNG DATABASE (Sửa tên QuanLyHocSinh nếu tên DB của bạn khác)
USE [QuanLyHocSinh]; 
GO

-- 2. KIỂM TRA & THÊM CỘT 'MaMon' (Chỉ thêm nếu chưa có)
IF COL_LENGTH('DiemDanh', 'MaMon') IS NULL
BEGIN
    PRINT 'Dang them cot MaMon...';
    ALTER TABLE DiemDanh ADD MaMon INT;
END
ELSE
BEGIN
    PRINT 'Cot MaMon da ton tai. Bo qua buoc nay.';
END
GO

-- 3. KIỂM TRA & THÊM CỘT 'ThoiGian' (Chỉ thêm nếu chưa có)
IF COL_LENGTH('DiemDanh', 'ThoiGian') IS NULL
BEGIN
    PRINT 'Dang them cot ThoiGian...';
    ALTER TABLE DiemDanh ADD ThoiGian NVARCHAR(20);
END
ELSE
BEGIN
    PRINT 'Cot ThoiGian da ton tai. Bo qua buoc nay.';
END
GO

-- 4. BÂY GIỜ MỚI TẠO THỦ TỤC INSERT (Lúc này chắc chắn bảng đã đủ cột)
USE [QuanLyHocSinh];
GO

-- Xóa procedure nếu tồn tại
IF OBJECT_ID('dbo.sp_InsertDiemDanh', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_InsertDiemDanh;
GO

-- Tạo hoặc sửa procedure
CREATE PROCEDURE dbo.sp_InsertDiemDanh
    @STT INT,
    @MaLop INT,
    @MaMon INT,          
    @Ngay DATE,
    @Buoi NVARCHAR(20),
    @TinhTrang NVARCHAR(50),
    @ThoiGian NVARCHAR(20) 
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO DiemDanh(STT, MaLop, MaMon, Ngay, Buoi, TinhTrang, ThoiGian)
    VALUES (@STT, @MaLop, @MaMon, @Ngay, @Buoi, @TinhTrang, @ThoiGian);
END
GO

PRINT '=== THANH CONG! DA CAP NHAT XONG DB ===';


-- 3. Cập nhật thủ tục GET (Để hiển thị được dữ liệu lên bảng)
USE QuanLyHocSinh;
GO

-- Xóa procedure nếu tồn tại
IF OBJECT_ID('dbo.sp_GetDiemDanhByLop', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_GetDiemDanhByLop;
GO

-- Tạo lại procedure
CREATE PROCEDURE dbo.sp_GetDiemDanhByLop
    @MaLop INT,
    @MaMon INT,
    @Ngay DATE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        hs.STT, 
        hs.HoTen, 
        dd.Buoi, 
        dd.TinhTrang, 
        dd.ThoiGian
    FROM DiemDanh dd
    JOIN HocSinh hs ON dd.STT = hs.STT
    WHERE dd.MaLop = @MaLop 
      AND dd.MaMon = @MaMon 
      AND dd.Ngay = @Ngay;
END
GO

PRINT '=== DA TAO XONG sp_GetDiemDanhByLop ===';
USE QuanLyHocSinh; -- Chọn cơ sở dữ liệu phù hợp
GO

-- Nếu đã tồn tại thủ tục thì xóa
IF OBJECT_ID('dbo.sp_UpdateGiaoVienFull', 'P') IS NOT NULL
    DROP PROCEDURE dbo.sp_UpdateGiaoVienFull;
GO
ALTER PROCEDURE dbo.sp_UpdateGiaoVienFull
    @ID_GV INT,
    @HoTen NVARCHAR(100),
    @Email VARCHAR(100),
    @SoDienThoai VARCHAR(20),
    @NgaySinh DATE,
    @GioiTinh NVARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    -- 1. Kiểm tra: Email mới có trùng với giáo viên khác không
    IF EXISTS (SELECT 1 FROM GiaoVien WHERE Email_GV = @Email AND ID_GV != @ID_GV)
    BEGIN
        SELECT 0; -- Trả về 0: Trùng Email
        RETURN;
    END

    -- 2. Cập nhật thông tin giáo viên
    UPDATE GiaoVien
    SET HoTen = @HoTen,
        Email_GV = @Email,
        SoDienThoai = @SoDienThoai,
        NgaySinh = @NgaySinh,
        GioiTinh = @GioiTinh
    WHERE ID_GV = @ID_GV;

    SELECT 1; -- Trả về 1: Thành công
END
GO