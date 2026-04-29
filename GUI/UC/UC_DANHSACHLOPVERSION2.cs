using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BUS;
using DTO;

namespace GUI
{
    public partial class UC_DANHSACHLOPVERSION2 : UserControl
    {
        private LopHocBUS bus = new LopHocBUS();

        // 1. Biến lưu trữ danh sách gốc để tìm kiếm mà không cần gọi lại DB
        private DataTable dtDanhSachLopGoc;

        public event EventHandler<LopHocEventArgs> OnLopHocSelected;

        public class LopHocEventArgs : EventArgs
        {
            public int MaLop { get; set; }
            public int MaMon { get; set; }
        }

        public UC_DANHSACHLOPVERSION2()
        {
            InitializeComponent();
            this.Load += UC_DANHSACHLOPVERSION2_Load;
        }

        private void UC_DANHSACHLOPVERSION2_Load(object sender, EventArgs e)
        {
            LoadDanhSachLopCuaGV();
        }

        public void LoadDanhSachLopCuaGV()
        {
            try
            {
                int idGV = AuthSession.CurrentIdGV;
                if (idGV <= 0) return;

                // Lấy dữ liệu từ DB và lưu vào biến toàn cục
                dtDanhSachLopGoc = bus.GetLopDayCuaGiaoVien(idGV);

                // Gọi hàm hiển thị
                HienThiDanhSachLenGiaoDien(dtDanhSachLopGoc);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        // 2. Tách hàm hiển thị ra riêng để tái sử dụng khi tìm kiếm
        private void HienThiDanhSachLenGiaoDien(DataTable dt)
        {
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.Padding = new Padding(15);

            // Tạm dừng layout để vẽ nhanh hơn
            flowLayoutPanel1.SuspendLayout();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    UC_MON_GV item = new UC_MON_GV();
                    item.Margin = new Padding(15);

                    int maLop = Convert.ToInt32(row["MaLop"]);
                    int maMon = Convert.ToInt32(row["MaMon"]);
                    string tenLop = row["TenLop"].ToString();
                    string tenMon = row["TenMon"].ToString();
                    int sl = (row["SoLuongHocSinh"] != DBNull.Value) ? Convert.ToInt32(row["SoLuongHocSinh"]) : 0;

                    item.SetData(maLop, maMon, tenLop, tenMon, sl);
                    item.OnCardClick += Item_OnCardClick;
                    flowLayoutPanel1.Controls.Add(item);
                }
            }
            else
            {
                Label lbl = new Label();
                lbl.Text = "Không tìm thấy lớp học nào.";
                lbl.Font = new Font("Segoe UI", 12, FontStyle.Italic);
                lbl.ForeColor = Color.Gray;
                lbl.AutoSize = true;
                lbl.Margin = new Padding(30);
                flowLayoutPanel1.Controls.Add(lbl);
            }

            // Tiếp tục vẽ layout
            flowLayoutPanel1.ResumeLayout();
        }

        private void Item_OnCardClick(object sender, EventArgs e)
        {
            UC_MON_GV clickedItem = sender as UC_MON_GV;
            if (clickedItem != null)
            {
                OnLopHocSelected?.Invoke(this, new LopHocEventArgs
                {
                    MaLop = clickedItem.MaLop,
                    MaMon = clickedItem.MaMon
                });
            }
        }

        // 3. Xử lý sự kiện tìm kiếm
        private void txtTimLop_TextChanged(object sender, EventArgs e)
        {
            // Nếu chưa có dữ liệu thì không làm gì
            if (dtDanhSachLopGoc == null) return;

            string keyword = txtTimLop.Text.Trim();

            // Nếu ô tìm kiếm trống, hiển thị lại toàn bộ danh sách gốc
            if (string.IsNullOrEmpty(keyword))
            {
                HienThiDanhSachLenGiaoDien(dtDanhSachLopGoc);
            }
            else
            {
                try
                {
                    // Copy cấu trúc DataTable gốc
                    DataTable dtLoc = dtDanhSachLopGoc.Clone();

                    // Lọc theo TenLop hoặc TenMon (không phân biệt hoa thường)
                    // Cú pháp Select của DataTable giống SQL Where clause
                    string expression = string.Format("TenLop LIKE '%{0}%' OR TenMon LIKE '%{0}%'", keyword);
                    DataRow[] foundRows = dtDanhSachLopGoc.Select(expression);

                    // Đổ dữ liệu đã lọc vào bảng mới
                    foreach (DataRow row in foundRows)
                    {
                        dtLoc.ImportRow(row);
                    }

                    // Hiển thị danh sách đã lọc
                    HienThiDanhSachLenGiaoDien(dtLoc);
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi cú pháp tìm kiếm (ví dụ ký tự đặc biệt ' )
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}