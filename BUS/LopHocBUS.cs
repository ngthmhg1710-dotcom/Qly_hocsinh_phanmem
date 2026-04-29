using DAL;
using DTO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace BUS
{
    public class LopHocBUS
    {
        private LopHocDAL dal = new LopHocDAL();

        // =========================================================================
        // PHẦN 1: CÁC HÀM LẤY DỮ LIỆU (READ)
        // =========================================================================

        public static DataTable GetAllLop()
        {
            return LopHocDAL.GetAllLop();
        }

        public static DataTable GetAllLop_Expanded()
        {
            LopHocDAL dalStatic = new LopHocDAL();
            return dalStatic.GetAllLop_Expanded();
        }

        public DataTable GetHocSinhByLop(int maLop)
        {
            return dal.GetHocSinhByLop(maLop);
        }

        public DataTable GetChiTietHocSinhByLop(int maLop, int maMon)
        {
            return dal.GetChiTietHocSinhByLop(maLop, maMon);
        }

        // =========================================================================
        // PHẦN 2: CÁC HÀM THÊM / SỬA / XÓA (CRUD)
        // =========================================================================

        public static string ThemLopTuDong(string khoi)
        {
            DataTable allLop = GetAllLop();
            DataRow[] lopCuaKhoi = allLop.Select($"Khoi = '{khoi}'");
            string tenLopMoi = "";

            if (lopCuaKhoi.Length == 0)
            {
                tenLopMoi = khoi + "A1";
            }
            else
            {
                int maxSo = 0;
                foreach (DataRow row in lopCuaKhoi)
                {
                    string tenLopHienTai = row["TenLop"].ToString();
                    var match = Regex.Match(tenLopHienTai, @"A(\d+)$");
                    if (match.Success)
                    {
                        int soHienTai = int.Parse(match.Groups[1].Value);
                        if (soHienTai > maxSo) maxSo = soHienTai;
                    }
                }
                tenLopMoi = khoi + "A" + (maxSo + 1);
            }

            if (LopHocDAL.InsertLop(tenLopMoi, khoi))
            {
                return tenLopMoi;
            }
            else
            {
                throw new Exception($"Không thể tạo lớp tự động {tenLopMoi} vì tên này đã tồn tại.");
            }
        }

        public static bool ThemLop(string tenLop, string khoi)
        {
            if (string.IsNullOrEmpty(tenLop) || string.IsNullOrEmpty(khoi)) return false;
            try
            {
                return LopHocDAL.InsertLop(tenLop, khoi);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int InsertLop(LopHocDTO lop)
        {
            return dal.InsertLop(lop);
        }

        public static bool InsertLopHoc(string tenLop, string khoi, int soLuong)
        {
            return ThemLop(tenLop, khoi);
        }

        public bool UpdateLop(LopHocDTO lop)
        {
            return dal.UpdateLop(lop);
        }

        public static bool DeleteLopHoc(int maLop)
        {
            if (maLop <= 0) return false;
            try
            {
                return LopHocDAL.DeleteLopHoc(maLop);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            catch
            {
                return false;
            }
        }

        // =========================================================================
        // PHẦN 3: LOGIC BỔ SUNG (CHECK TỒN TẠI KHỐI) - MỚI THÊM
        // =========================================================================

        // 11. Kiểm tra khối đã có lớp nào chưa (để tránh tạo trùng khối)
        public static bool KiemTraKhoiTonTai(string khoi)
        {
            return LopHocDAL.CheckKhoiTonTai(khoi);
        }

        // =========================================================================
        // PHẦN 4: CÁC HÀM KHÁC
        // =========================================================================
        public DataTable GetLopDayCuaGiaoVien(int idGV)
        {
            return dal.GetLopDayCuaGiaoVien(idGV);
        }
        public bool UpdateDiemHocSinh(int stt, int maMon, float gk1, float ck1, float gk2, float ck2)
        {
            return dal.UpdateDiemHocSinh(stt, maMon, gk1, ck1, gk2, ck2);
        }

        public int GetMaMonCuaGiaoVien(int maLop, int idGV)
        {
            return dal.GetMaMonCuaGiaoVien(maLop, idGV);
        }
        public bool UpdateTuyenDuong(int stt, int maMon, bool trangThai)
        {
            return dal.UpdateTuyenDuong(stt, maMon, trangThai);
        }

        public string GetTenLop(int maLop) => dal.GetTenLop(maLop);
        public string GetTenMon(int maMon) => dal.GetTenMon(maMon);
        public DataTable GetThongKeXepLoai(int maLop, int maMon) => dal.GetThongKeXepLoai(maLop, maMon);

        public DataTable GetListLopCuaGV(int idGV) => dal.GetListLopCuaGV(idGV);
        public DataTable GetListMonCuaGVTrongLop(int idGV, int maLop) => dal.GetListMonCuaGVTrongLop(idGV, maLop);

        public bool SaveNote(int idGV, int stt, int maMon, string nhanXet) => dal.SaveNote(idGV, stt, maMon, nhanXet);
        public string GetNoteHocSinh(int stt, int maMon) => dal.GetNoteHocSinh(stt, maMon);

        public DataTable GetLichSuDiemDanh(int stt)
        {
            return DataProvider.ExecuteQuery("sp_GetLichSuDiemDanhByHocSinh", new object[] { stt });
        }
    }
}