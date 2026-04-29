// File: LichSuGameForm.cs
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BUS; // Tham chiếu đến Project BUS
using DTO; // Tham chiếu đến Project DTO

namespace GAME1_
{
    public partial class LichSuGameForm : Form
    {
        // 1. Thay đổi kiểu dữ liệu từ CauHoi (cũ) sang CauHoiDTO
        public List<CauHoiHinhAnhDTO> DanhSachCauHoiChoiLai { get; private set; }

        // 2. Khởi tạo đối tượng BUS để xử lý logic
        private readonly NgheChonHinhBUS bus = new NgheChonHinhBUS();

        public LichSuGameForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sự kiện được kích hoạt khi Form được tải lên lần đầu tiên.
        /// </summary>
        private void LichSuGameForm_Load(object sender, EventArgs e)
        {
            LoadHistory();
        }

        #region "Sự Kiện Cho Các Nút Bấm"

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút "Chơi Lại".
        /// </summary>
        private void btnChoiLai_Click(object sender, EventArgs e)
        {
            if (dgvLichSu.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một game từ danh sách để chơi lại.", "Chưa chọn game", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                int gameInstanceId = Convert.ToInt32(dgvLichSu.CurrentRow.Cells["ID_GameInstance"].Value);
                List<CauHoiHinhAnhDTO> questionsToPlay = bus.LayChiTietGame(gameInstanceId);

                if (questionsToPlay != null && questionsToPlay.Count > 0)
                {
                    // 1. Tạo form Game
                    TAOGAME_NGHECHONHINH gameForm = new TAOGAME_NGHECHONHINH(questionsToPlay);

                    // 2. Lấy Form Cha (Form Tạo Câu Hỏi) để ẩn nó đi
                    // (Vì LichSuGameForm được mở bằng ShowDialog(this), nên Owner chính là Form Tạo)
                    Form parentForm = this.Owner;

                    if (parentForm != null)
                    {
                        parentForm.Hide(); // Ẩn Form Tạo

                        // Khi Game đóng -> Hiện lại Form Tạo
                        gameForm.FormClosed += (s, args) => parentForm.Show();
                    }

                    // 3. Hiện Game
                    gameForm.Show();

                    // 4. Đóng Form Lịch Sử
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy dữ liệu câu hỏi.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút "Xóa".
        /// </summary>
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvLichSu.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một game từ danh sách để xóa.", "Chưa chọn game", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Hỏi xác nhận trước khi xóa
            DialogResult confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa vĩnh viễn game này khỏi lịch sử không?",
                                       "Xác nhận xóa",
                                       MessageBoxButtons.YesNo,
                                       MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    int gameInstanceId = Convert.ToInt32(dgvLichSu.CurrentRow.Cells["ID_GameInstance"].Value);

                    // GỌI QUA BUS: Thực hiện xóa game
                    if (bus.XoaGame(gameInstanceId))
                    {
                        MessageBox.Show("Đã xóa game thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Tải lại danh sách sau khi xóa để cập nhật giao diện
                        LoadHistory();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa game: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private int _teacherId;

        public LichSuGameForm(int idGiaoVien)
        {
            InitializeComponent();
            this._teacherId = idGiaoVien; // Lưu ID lại để lát nữa dùng tải dữ liệu
        }
        #endregion
        private void LoadHistory()
        {
            try
            {
                // --- SỬA Ở ĐÂY ---

                // CŨ (SAI): Vẫn dùng UserSession (có thể bị null hoặc sai)
                // dgvLichSu.DataSource = bus.LayLichSuGame(UserSession.TeacherId);

                // MỚI (ĐÚNG): Dùng biến _teacherId mà bạn đã nhận ở Constructor
                // Lưu ý: Nếu hàm BUS của bạn nhận int thì để nguyên _teacherId
                // Nếu hàm BUS nhận string thì dùng _teacherId.ToString()
                dgvLichSu.DataSource = bus.LayLichSuGame(_teacherId);

                // -----------------

                FormatDataGridView();

                // Bỏ chọn tất cả các dòng
                dgvLichSu.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải lịch sử game: " + ex.Message, "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region "Các Hàm Logic Hỗ Trợ"

        /// <summary>
        /// Tải danh sách các game đã tạo thông qua BUS và hiển thị lên DataGridView.
        /// </summary>


        /// <summary>
        /// Định dạng lại các cột của DataGridView để dễ nhìn hơn.
        /// </summary>
        private void FormatDataGridView()
        {
            if (dgvLichSu.Columns.Count == 0) return;

            // KIỂM TRA VÀ ẨN CỘT ID
            if (dgvLichSu.Columns["ID_GameInstance"] != null)
            {
                dgvLichSu.Columns["ID_GameInstance"].Visible = false;
            }

            // Kiểm tra và định dạng các cột còn lại
            if (dgvLichSu.Columns["TenGame"] != null)
            {
                dgvLichSu.Columns["TenGame"].HeaderText = "Tên Bộ Câu Hỏi";
                dgvLichSu.Columns["TenGame"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            if (dgvLichSu.Columns["NgayTao"] != null)
            {
                dgvLichSu.Columns["NgayTao"].HeaderText = "Ngày Tạo";
                dgvLichSu.Columns["NgayTao"].Width = 220;
                dgvLichSu.Columns["NgayTao"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
            }
        }

        #endregion
    }
}