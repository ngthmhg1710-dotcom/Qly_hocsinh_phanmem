using BUS;
using System;
using System.Drawing;
using System.Windows.Forms;
using GUI; // Để nhận diện class QLLOPHOC

namespace GUI
{
    public partial class UC_QLLOPHOC : UserControl
    {
        // 1. KHAI BÁO BIẾN
        private int maLopHienTai;
        private int maMonHienTai;

        // Các UserControl con (Cache lại để không phải new nhiều lần)
        private UC_KQHOCTAP ucKQHocTap;
        private UC_THONGKE ucThongKe;

        // 2. CONSTRUCTOR MẶC ĐỊNH
        public UC_QLLOPHOC()
        {
            InitializeComponent();
        }

        // 3. CONSTRUCTOR NHẬN THAM SỐ (Được gọi từ Form QLLOPHOC)
        public UC_QLLOPHOC(int maLop, int maMon) : this()
        {
            this.maLopHienTai = maLop;
            this.maMonHienTai = maMon;
        }

        // --- SỰ KIỆN LOAD ---
        private void UC_QLLOPHOC_Load(object sender, EventArgs e)
        {
            try
            {
                // Cài đặt Font chữ (nếu có sử dụng FontManager)
                // Nếu không có lớp FontManager, bạn có thể comment đoạn này lại
                btnThongKe.Font = FontManager.GetFontByName("Montserrat", 12);
                btnKQHocTap.Font = FontManager.GetFontByName("Montserrat", 12);
            }
            catch { }

            // Mặc định mở tab Kết Quả Học Tập đầu tiên
            btnKQHocTap_Click(sender, e);
        }

        // =============================================================
        // HÀM CHUYỂN ĐỔI USER CONTROL (QUẢN LÝ TAB)
        // =============================================================
        private void ShowUserControl(UserControl uc)
        {
            // 1. Ẩn tất cả các control đang có trong panelMain
            foreach (Control c in panelMain.Controls)
            {
                c.Visible = false;
            }

            // 2. Cấu hình UC mới
            uc.Dock = DockStyle.Fill;

            // 3. Nếu chưa có trong panel thì thêm vào
            if (!panelMain.Controls.Contains(uc))
            {
                panelMain.Controls.Add(uc);
            }

            // 4. Hiển thị lên trên cùng
            uc.Visible = true;
            uc.BringToFront();
        }

        // =============================================================
        // SỰ KIỆN CLICK MENU TAB
        // =============================================================

        // 1. TAB KẾT QUẢ HỌC TẬP
        private void btnKQHocTap_Click(object sender, EventArgs e)
        {
            // Highlight nút đang chọn (Tuỳ chọn logic UI)
            btnKQHocTap.Checked = true;
            btnThongKe.Checked = false;

            if (ucKQHocTap == null)
            {
                // Truyền Mã Lớp và Mã Môn vào
                ucKQHocTap = new UC_KQHOCTAP(this.maLopHienTai, this.maMonHienTai);
            }
            ShowUserControl(ucKQHocTap);
        }

        // 2. TAB THỐNG KÊ (BIỂU ĐỒ)
        private void btnThongKe_Click(object sender, EventArgs e)
        {
            // Highlight nút đang chọn
            btnKQHocTap.Checked = false;
            btnThongKe.Checked = true;

            if (this.maLopHienTai <= 0)
            {
                MessageBox.Show("Lỗi: Mã lớp chưa được xác định!");
                return;
            }

            if (ucThongKe == null)
            {
                ucThongKe = new UC_THONGKE(this.maLopHienTai, this.maMonHienTai);
            }

            ShowUserControl(ucThongKe);

            // Ép vẽ lại biểu đồ để đảm bảo dữ liệu mới nhất
            ucThongKe.ReloadData();
        }

        // =============================================================
        // CÁC CHỨC NĂNG HỆ THỐNG & ĐIỀU HƯỚNG
        // =============================================================

        private bool isInfoVisible = false;
        public void ToggleButtonInfo()
        {
            isInfoVisible = !isInfoVisible;

            // Logic hiển thị thông tin lớp học (Ví dụ: Tên GVCN, Sĩ số...)
            // Nếu bạn có một Panel thông tin bên phải, hãy code ẩn/hiện nó ở đây.
            if (isInfoVisible)
            {
                // Ví dụ: Hiển thị tên lớp và mã môn
                MessageBox.Show($"Thông tin lớp học:\nMã lớp: {maLopHienTai}\nMã môn: {maMonHienTai}", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Nút Admin (Chuyển sang Form Admin)
        private void guna2Button5_Click(object sender, EventArgs e)
        {
            THONGTINADMIN ttAdmin = new THONGTINADMIN();
            ttAdmin.Show();

            // Ẩn Form cha chứa UserControl này
            this.ParentForm?.Hide();
        }

        // Nút Đăng xuất
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            // CÁCH TỐI ƯU: Gọi về hàm DangXuat() của Form cha (QLLOPHOC)
            // để đảm bảo Form cha đóng hoàn toàn, không chạy ngầm.

            if (this.ParentForm is QLLOPHOC parentForm)
            {
                parentForm.DangXuat();
            }
            else
            {
                // Fallback: Nếu Form cha không phải QLLOPHOC (đề phòng lỗi)
                this.ParentForm?.Hide();
                this.ParentForm?.Close();
                DANGNHAP signInForm = new DANGNHAP();
                signInForm.Show();
            }
        }

        // =============================================================
        // CÁC HÀM RỖNG (GIỮ LẠI ĐỂ TRÁNH LỖI DESIGNER)
        // =============================================================
        private void btnInfor_Paint(object sender, PaintEventArgs e) { }
        private void panelMain_Paint(object sender, PaintEventArgs e) { }
    }
}