using System;
using System.Drawing;
using System.Windows.Forms;
using DTO;
using BUS;

namespace GUI
{
    public partial class QLLOPHOC : Form
    {
        // Biến lưu trạng thái nếu muốn mở trực tiếp (tùy chọn)
        private int _directMaLop = -1;
        private int _directMaMon = -1;
        private bool _isDirectMode = false;

        // =========================================================
        // 1. CONSTRUCTOR MẶC ĐỊNH
        // =========================================================
        public QLLOPHOC()
        {
            InitializeComponent();
        }

        // =========================================================
        // 2. CONSTRUCTOR MỞ RỘNG (Nếu cần truyền mã lớp từ Dashboard)
        // =========================================================
        public QLLOPHOC(int maLop, int maMon) : this()
        {
            _directMaLop = maLop;
            _directMaMon = maMon;
            _isDirectMode = true;
        }

        // =========================================================
        // 3. HÀM LOAD FORM
        // =========================================================
        private void QLLOPHOC_Load(object sender, EventArgs e)
        {
            // Cài đặt Font (nếu cần, giữ nguyên theo code cũ của bạn)
            /*
            try {
                btnQLLopHoc.Font = FontManager.GetFontByName("Montserrat", 12);
                btnDiemDanh.Font = FontManager.GetFontByName("Montserrat", 12);
                btnCongCu.Font = FontManager.GetFontByName("Montserrat", 12);
                btnTroChoiHocTap.Font = FontManager.GetFontByName("Montserrat", 12);
            } catch { }
            */
            this.KeyPreview = true;   // <--- QUAN TRỌNG NHẤT


            // Điều hướng logic hiển thị
            if (_isDirectMode && _directMaLop > 0)
            {
                // Nếu được gọi kèm mã lớp -> Vào thẳng chi tiết
                HienThiChiTietLop(_directMaLop, _directMaMon);
            }
            else
            {
                // Mặc định -> Vào danh sách lớp
                HienThiDanhSachLop();
            }
        }

        // =========================================================
        // 4. HÀM XỬ LÝ ĐIỀU HƯỚNG (CORE LOGIC)
        // =========================================================

        // Hàm 1: Hiển thị Danh sách lớp (UC_DANHSACHLOPVERSION2)
        private void HienThiDanhSachLop()
        {
            UC_DANHSACHLOPVERSION2 ucList = new UC_DANHSACHLOPVERSION2();

            // Đăng ký sự kiện: Khi chọn 1 lớp ở danh sách -> Mở chi tiết lớp đó
            // Lưu ý: Bạn cần đảm bảo file UC_DANHSACHLOPVERSION2.cs đã có sự kiện OnLopHocSelected như hướng dẫn trước
            ucList.OnLopHocSelected += (s, args) =>
            {
                HienThiChiTietLop(args.MaLop, args.MaMon);
            };

            ShowUserControl(ucList);

            // Cập nhật trạng thái nút Menu
            btnQLLopHoc.Checked = true;
            _isDirectMode = false; // Reset trạng thái
        }

        // Hàm 2: Hiển thị Chi tiết lớp (UC_QLLOPHOC)
        public void HienThiChiTietLop(int maLop, int maMon)
        {
            // Khởi tạo UserControl chi tiết
            UC_QLLOPHOC ucDetail = new UC_QLLOPHOC(maLop, maMon);

            ShowUserControl(ucDetail);
        }

        // Hàm phụ trợ: Xóa control cũ, thêm control mới vào Panel
        private void ShowUserControl(UserControl control)
        {
            // Dọn dẹp bộ nhớ: Dispose control cũ
            if (panelMain.Controls.Count > 0)
            {
                Control oldCtrl = panelMain.Controls[0];
                panelMain.Controls.Remove(oldCtrl); // Gỡ khỏi giao diện trước
                oldCtrl.Dispose(); // Xóa hoàn toàn khỏi bộ nhớ
            }

            // Ép bộ dọn rác chạy ngay lập tức (Giúp giảm RAM khi thao tác nhiều)
            GC.Collect();
            GC.WaitForPendingFinalizers();

            panelMain.Controls.Clear();
            control.Dock = DockStyle.Fill;
            panelMain.Controls.Add(control);
        }

        // =========================================================
        // 5. HÀM ĐĂNG XUẤT (GỌI TỪ NÚT HOẶC TỪ USERCONTROL CON)
        // =========================================================
        public void DangXuat()
        {
            this.Hide();

            // Dọn dẹp UserControl hiện tại trước khi thoát
            if (panelMain.Controls.Count > 0)
            {
                panelMain.Controls[0].Dispose();
            }

            // Ép dọn rác bộ nhớ lần cuối
            GC.Collect();
            GC.WaitForPendingFinalizers();

            DANGNHAP signInForm = new DANGNHAP();
            signInForm.Show();

            this.Close();
        }

        // =========================================================
        // 6. CÁC SỰ KIỆN MENU BÊN TRÁI
        // =========================================================

        private void btnQlLopHoc_Click(object sender, EventArgs e)
        {
            // Khi nhấn nút này -> Luôn quay về Danh sách lớp
            HienThiDanhSachLop();
        }

        private void btnDiemDanh_Click(object sender, EventArgs e)
        {
            ShowUserControl(new UC_DIEMDANH());
        }

        private void btnCongCu_Click(object sender, EventArgs e)
        {
            ShowUserControl(new UC_CONGCU());
        }

        private void btnTroChoiHocTap_Click(object sender, EventArgs e)
        {
            ShowUserControl(new UC_TROCHOIHOCTAP());
        }

        // =========================================================
        // 7. CÁC SỰ KIỆN HỆ THỐNG (HEADER, AVATAR)
        // =========================================================

        private void guna2PictureBox6_Click(object sender, EventArgs e)
        {
            TRANGCHU trangChu = new TRANGCHU();
            trangChu.Show();
            this.Hide();
        }

        private void btnAvt_Click(object sender, EventArgs e)
        {
            // Hiển thị Popup Menu
            MENU_CHINHSUA popupMenu = new MENU_CHINHSUA();
            Point screenPoint = btnAvt.PointToScreen(Point.Empty);

            popupMenu.InforButtonClicked += (s, ev) =>
            {
                popupMenu.Close();
                this.Hide();
                THONGTINADMIN ttAdmin = new THONGTINADMIN();
                ttAdmin.Show();
            };

            int x = screenPoint.X - popupMenu.Width + btnAvt.Width;
            int y = screenPoint.Y + btnAvt.Height + 5;
            popupMenu.Location = new Point(x, y);

            popupMenu.Show();
            popupMenu.Activate();
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            // Nút Đăng xuất trên thanh Menu
            DangXuat();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            // Nút thông tin Admin
            THONGTINADMIN ttAdmin = new THONGTINADMIN();
            ttAdmin.Show();
            this.Hide();
        }

        // Nút Info (Chữ i): Hiển thị thông tin lớp/môn học nếu đang ở màn hình chi tiết
        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            if (panelMain.Controls.Count > 0 && panelMain.Controls[0] is UC_QLLOPHOC currentUC)
            {
                currentUC.ToggleButtonInfo();
            }
            else
            {
                // Có thể thêm thông báo hoặc bỏ qua nếu không phải màn hình chi tiết
                // MessageBox.Show("Chức năng này chỉ hiển thị khi đang xem chi tiết lớp học.");
            }
        }

        // =========================================================
        // 8. CÁC HÀM KHÁC (RỖNG ĐỂ TRÁNH LỖI DESIGNER)
        // =========================================================
        private void guna2TextBox1_TextChanged(object sender, EventArgs e) { }
        private void panelMain_Paint(object sender, PaintEventArgs e) { }

        public void ShowDashboardOnSignIn()
        {
            btnQLLopHoc.PerformClick();
        }

        private void QLLOPHOC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                FRM_QUANLY_NOTE frm = new FRM_QUANLY_NOTE();
                frm.ShowDialog();

                // Reload nếu đang ở KQ học tập
                if (panelMain.Controls.Count > 0 && panelMain.Controls[0] is UC_KQHOCTAP uc)
                {
                    uc.LoadDanhSachHocSinh();
                }

                e.Handled = true;
            }
        }

        private void btnNote_Click(object sender, EventArgs e)
        {
            FRM_QUANLY_NOTE frm = new FRM_QUANLY_NOTE();
            frm.ShowDialog();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Console.WriteLine("Pressed key: " + keyData); // hoặc Debug.WriteLine

            if (keyData == Keys.D1 || keyData == Keys.F12) // Bắt cả số 1 và F12
            {
                FRM_QUANLY_NOTE frm = new FRM_QUANLY_NOTE();
                frm.ShowDialog();

                // Reload nếu đang ở UC_KQHOCTAP
                if (panelMain.Controls.Count > 0 && panelMain.Controls[0] is UC_KQHOCTAP uc)
                {
                    uc.LoadDanhSachHocSinh();
                }

                return true; // Chặn luôn không cho control con xử lý
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }


    }
}