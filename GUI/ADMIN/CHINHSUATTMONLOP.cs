using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using BUS;
using DTO;

namespace GUI
{
    public partial class CHINHSUATTMONLOP : Form
    {
        private int _ID_GV; // ID giáo viên
        private PhanCongBUS pcBUS = new PhanCongBUS(); // đối tượng BUS

        // Dùng để ánh xạ MaMon và MaLop với index của DataGridView
        private Dictionary<int, int> maMonToRow = new Dictionary<int, int>();
        private Dictionary<int, int> maLopToColumn = new Dictionary<int, int>();

        public CHINHSUATTMONLOP(int idGV)
        {
            InitializeComponent();

            _ID_GV = idGV;

            LoadBangPhanCong(); // tạo bảng checkbox
            LoadPhanCongCu();   // load dữ liệu cũ từ DB
        }

        private void LoadBangPhanCong()
        {
            dgvPhanCong.Columns.Clear();
            dgvPhanCong.Rows.Clear();
            dgvPhanCong.AllowUserToAddRows = false;

            // --- Danh sách môn cố định ---
            List<string> danhSachMon = new List<string>()
            {
                "Tiếng Việt", "Toán", "Đạo đức", "Ngoại ngữ 1",
                "Lịch sử và Địa lí", "Khoa học", "Tin học và Công nghệ",
                "Giáo dục thể chất", "Nghệ thuật (Âm nhạc, Mĩ thuật)",
                "Hoạt động trải nghiệm"
            };

            // --- Danh sách lớp ---
            List<string> danhSachLop = new List<string>();
            for (int khoi = 1; khoi <= 5; khoi++)
                for (int lop = 1; lop <= 10; lop++)
                    danhSachLop.Add($"{khoi}/{lop}");

            // Thêm cột môn
            dgvPhanCong.Columns.Add("MonHoc", "Môn học");

            // Thêm cột lớp
            int colIndex = 1;
            foreach (string lop in danhSachLop)
            {
                var col = new DataGridViewCheckBoxColumn
                {
                    HeaderText = lop,
                    Width = 50
                };
                dgvPhanCong.Columns.Add(col);
                maLopToColumn[colIndex] = colIndex; // lưu index cột
                colIndex++;
            }

            // Thêm dòng môn
            int rowIndex = 0;
            foreach (string mon in danhSachMon)
            {
                List<object> row = new List<object> { mon };
                for (int i = 0; i < danhSachLop.Count; i++)
                    row.Add(false);

                dgvPhanCong.Rows.Add(row.ToArray());
                maMonToRow[rowIndex + 1] = rowIndex; // lưu index dòng
                rowIndex++;
            }
        }

        private void LoadPhanCongCu()
        {
            // Lấy dữ liệu MaMon + MaLop từ DB
            DataTable dtMon = pcBUS.GetMon(_ID_GV); // trả về MaMon
            DataTable dtLop = pcBUS.GetLop(_ID_GV); // trả về MaLop

            // Gán checkbox lớp
            foreach (DataRow rowLop in dtLop.Rows)
            {
                int maLop = Convert.ToInt32(rowLop["MaLop"]);

                // Với mỗi môn, nếu đã phân công, check tương ứng
                foreach (DataRow rowMon in dtMon.Rows)
                {
                    int maMon = Convert.ToInt32(rowMon["MaMon"]);

                    if (maMonToRow.ContainsKey(maMon) && maLopToColumn.ContainsKey(maLop))
                    {
                        int r = maMonToRow[maMon];
                        int c = maLopToColumn[maLop];
                        dgvPhanCong[c, r].Value = true;
                    }
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Xóa phân công cũ
            pcBUS.XoaPhanCong(_ID_GV);

            // Duyệt từng dòng (môn)
            for (int r = 0; r < dgvPhanCong.Rows.Count; r++)
            {
                int maMon = r + 1;
                List<int> dsLopDaChon = new List<int>();

                // Duyệt từng cột (lớp)
                for (int c = 1; c < dgvPhanCong.Columns.Count; c++)
                {
                    bool isChecked = dgvPhanCong[c, r].Value != null && (bool)dgvPhanCong[c, r].Value;
                    if (isChecked)
                        dsLopDaChon.Add(c); // c = MaLop
                }

                if (dsLopDaChon.Count > 0)
                {
                    // Lưu môn
                    pcBUS.LuuDanhSachMon(_ID_GV, new List<int> { maMon });

                    // Lưu lớp
                    List<PhanCongLopDTO> dsLop = dsLopDaChon
                        .Select(l => new PhanCongLopDTO(_ID_GV, l, "Giảng dạy"))
                        .ToList();
                    pcBUS.LuuDanhSachLop(_ID_GV, dsLop);
                }
            }

            MessageBox.Show("Lưu thành công!");
        }

        private void dgvPhanCong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > 0)
                dgvPhanCong.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void CHINHSUATTMONLOP_Load(object sender, EventArgs e)
        {

        }
    }
}
