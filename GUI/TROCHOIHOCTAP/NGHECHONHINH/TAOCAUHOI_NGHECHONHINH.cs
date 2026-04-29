// File: TAOCAUHOI_NGHECHONHINH.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BUS; // Tham chiếu lớp nghiệp vụ
using DTO; // Tham chiếu lớp dữ liệu

namespace GAME1_
{
    public partial class TAOCAUHOI_NGHECHONHINH : Form
    {
        // Sử dụng CauHoiDTO từ project DTO thay vì class CauHoi tự định nghĩa
        private List<   CauHoiHinhAnhDTO> danhSachCauHoi = new List<CauHoiHinhAnhDTO>();
        private int _teacherId;
        // Khởi tạo đối tượng BUS
        private readonly NgheChonHinhBUS bus = new NgheChonHinhBUS();

        public TAOCAUHOI_NGHECHONHINH()
        {
            InitializeComponent();
        }

        #region "Sự Kiện Cho Các Nút Bấm"

        /// <summary>
        /// Xử lý sự kiện khi nhấn nút "Thêm Thẻ" để tạo một ô nhập câu hỏi mới.
        /// </summary>
        private void THEMTHE_Click(object sender, EventArgs e)
        {
            // Tính toán số câu hỏi mới dựa trên số lượng thẻ hiện có trong panel
            int soCauHoiMoi = pnlContainer.Controls.OfType<TheCauHoi>().Count() + 1;

            TheCauHoi card = new TheCauHoi();
            card.SetSoCauHoi(soCauHoiMoi);

            // Lắng nghe sự kiện yêu cầu xóa từ User Control TheCauHoi
            card.XoaTheRequested += (s, ev) => {
                pnlContainer.Controls.Remove(card);
                card.Dispose(); // Giải phóng tài nguyên của thẻ đã xóa

                // Gọi hàm cập nhật lại số thứ tự cho tất cả các thẻ còn lại
                UpdateQuestionNumbers();
            };

            pnlContainer.Controls.Add(card);
        }
        public TAOCAUHOI_NGHECHONHINH(int idGiaoVien)
        {
            InitializeComponent();

            // Lưu ID nhận được vào biến riêng của Form này
            this._teacherId = idGiaoVien;
        }
      
        /// <summary>
        /// Xử lý sự kiện khi nhấn nút "TẠO VÀ LƯU GAME".
        /// </summary>
        private void TAO_Click(object sender, EventArgs e)
        {
            // BƯỚC 1: THU THẬP VÀ XÁC THỰC DỮ LIỆU ĐẦU VÀO
            if (!ValidateInput())
            {
                return; // Dừng lại nếu dữ liệu không hợp lệ
            }

            // BƯỚC 2: LƯU BỘ CÂU HỎI THÔNG QUA BUS
            if (!SaveGameToDatabase())
            {
                return; // Dừng lại nếu lưu thất bại
            }

            // BƯỚC 3: KHỞI CHẠY GAME
            StartGame();
        }

        /// <summary>
        /// Mở form Lịch Sử Game, xử lý việc chơi lại game đã tạo.
        /// </summary>
        private void btnLichSu_Click(object sender, EventArgs e)
        {
            int idCanXem = (_teacherId > 0) ? _teacherId : 8;

            using (LichSuGameForm historyForm = new LichSuGameForm(idCanXem))
            {
                // Truyền 'this' để Form Lịch Sử biết ai là cha (để xử lý ẩn/hiện)
                historyForm.ShowDialog(this);
            }
        }

        #endregion

        #region "Các Hàm Logic Hỗ Trợ"

        /// <summary>
        /// Duyệt qua tất cả các thẻ câu hỏi và cập nhật lại số thứ tự của chúng.
        /// </summary>
        private void UpdateQuestionNumbers()
        {
            int index = 1;
            foreach (TheCauHoi card in pnlContainer.Controls.OfType<TheCauHoi>())
            {
                card.SetSoCauHoi(index);
                index++;
            }
        }

        /// <summary>
        /// Thu thập dữ liệu từ các thẻ đã hoàn thành và kiểm tra tính hợp lệ.
        /// </summary>
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtTenGame.Text))
            {
                MessageBox.Show("❌ Vui lòng nhập tên cho bộ câu hỏi này.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenGame.Focus();
                return false;
            }

            danhSachCauHoi.Clear();

            // Duyệt qua tất cả các thẻ câu hỏi trên giao diện
            foreach (TheCauHoi card in pnlContainer.Controls.OfType<TheCauHoi>())
            {
                // Lưu ý: Hàm GetCauHoiDTO() cần được cập nhật trong UserControl TheCauHoi
                CauHoiHinhAnhDTO cauHoi = card.GetCauHoiDTO();

                if (cauHoi != null)
                {
                    danhSachCauHoi.Add(cauHoi);
                }
            }

            if (danhSachCauHoi.Count == 0)
            {
                MessageBox.Show("❌ Không có câu hỏi nào được điền đầy đủ thông tin.\n\nMột câu hỏi hoàn chỉnh cần có: 3 hình, 1 âm thanh và 1 đáp án được chọn.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Gọi xuống tầng BUS để lưu game.
        /// </summary>
        private bool SaveGameToDatabase()
        {
            try
            {
                int idGiaoVien = _teacherId;

                // Nếu ID chưa có hoặc lỗi, gán mặc định là 8 (Hương Nguyễn) để test
                if (idGiaoVien <= 0)
                {
                    idGiaoVien = 8;
                }

                // --- SỬA Ở ĐÂY ---
                // Xóa .ToString() đi. Truyền thẳng biến idGiaoVien (kiểu int) vào.
                bool ketQua = bus.TaoGameMoi(txtTenGame.Text.Trim(), idGiaoVien, danhSachCauHoi);
                // ----------------

                if (ketQua)
                {
                    MessageBox.Show("✔ Đã lưu bộ câu hỏi thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi hệ thống: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Mở form chơi game và truyền danh sách câu hỏi qua.
        /// </summary>
        private void StartGame()
        {
            // Tạo form game với danh sách câu hỏi
            TAOGAME_NGHECHONHINH gameForm = new TAOGAME_NGHECHONHINH(danhSachCauHoi);

            // LOGIC ĐIỀU HƯỚNG:
            this.Hide(); // 1. Ẩn form Tạo

            // 2. Khi Game đóng -> Hiện lại form Tạo
            gameForm.FormClosed += (s, args) => this.Show();

            gameForm.Show(); // 3. Hiện Game
        }

        #endregion
    }
}