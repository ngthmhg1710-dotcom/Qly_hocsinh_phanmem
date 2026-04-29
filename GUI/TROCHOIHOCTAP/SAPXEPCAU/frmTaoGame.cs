using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BUS;
using DTO;

namespace SapXepCau
{
    public partial class frmTaoGame : Form
    {
        private int _idGV;
        private GameSapXepCauBUS bus = new GameSapXepCauBUS();
        private List<string> _danhSachCauHoiTam = new List<string>();

        public frmTaoGame(int idGV)
        {
            InitializeComponent();
            _idGV = idGV;
        }

        // 1. THÊM CÂU
        private void btnThemCau_Click(object sender, EventArgs e)
        {
            string cauMoi = txtCauHoi.Text.Trim();

            if (string.IsNullOrEmpty(cauMoi))
            {
                MessageBox.Show("Vui lòng nhập nội dung câu hỏi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCauHoi.Focus();
                return;
            }

            _danhSachCauHoiTam.Add(cauMoi); // Thêm vào danh sách tạm
            CapNhatListHienThi();           // Cập nhật giao diện

            txtCauHoi.Clear();
            txtCauHoi.Focus();
        }

        // 2. XÓA CÂU ĐÃ CHỌN
        private void btnXoaCau_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có dòng nào đang được chọn không
            if (dgvCauHoi.CurrentRow != null && dgvCauHoi.CurrentRow.Index != -1)
            {
                int index = dgvCauHoi.CurrentRow.Index;

                // Xóa trong danh sách tạm
                _danhSachCauHoiTam.RemoveAt(index);

                // Cập nhật lại giao diện (sẽ tự động đánh số lại)
                CapNhatListHienThi();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn câu cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // --- HÀM CẬP NHẬT GIAO DIỆN & ĐÁNH SỐ LẠI ---
        private void CapNhatListHienThi()
        {
            dgvCauHoi.Rows.Clear(); // Xóa sạch lưới cũ

            for (int i = 0; i < _danhSachCauHoiTam.Count; i++)
            {
                // Tự động tạo STT: 0 -> Câu 1, 1 -> Câu 2...
                string stt = $"Câu {i + 1}";
                string noiDung = _danhSachCauHoiTam[i];

                // Thêm dòng mới vào lưới
                dgvCauHoi.Rows.Add(stt, noiDung);
            }

            // Cuộn xuống dòng cuối cùng để dễ nhìn
            if (dgvCauHoi.RowCount > 0)
                dgvCauHoi.FirstDisplayedScrollingRowIndex = dgvCauHoi.RowCount - 1;
        }

        // 3. LƯU VÀ CHƠI NGAY
        private void btnLuuVaChoi_Click(object sender, EventArgs e)
        {
            string tenGame = txtTenGame.Text.Trim();
            if (string.IsNullOrEmpty(tenGame))
            {
                MessageBox.Show("Vui lòng nhập tên bộ game!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_danhSachCauHoiTam.Count == 0)
            {
                MessageBox.Show("Danh sách câu hỏi đang trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Gọi BUS để lưu xuống CSDL
                if (bus.TaoGameMoi(tenGame, _idGV, _danhSachCauHoiTam))
                {
                    MessageBox.Show("Lưu thành công! Đang vào game...", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Lấy game vừa tạo để lấy ID
                    var danhSachGame = bus.LayLichSuGame(_idGV);
                    if (danhSachGame.Count > 0)
                    {
                        var gameMoiNhat = danhSachGame[0]; // Lấy phần tử đầu tiên (mới nhất)

                        // --- QUAN TRỌNG: TRUYỀN THÊM _idGV VÀO ---
                        frmChoiGame f = new frmChoiGame(gameMoiNhat.ID_GameInstance, gameMoiNhat.TenGame, _idGV);

                        f.Show();     // Mở form Game
                        this.Close(); // Đóng form Tạo Game
                    }
                }
                else
                {
                    MessageBox.Show("Lưu thất bại. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 4. XEM LỊCH SỬ
        private void btnXemLichSu_Click(object sender, EventArgs e)
        {
            frmLichSuGame f = new frmLichSuGame(_idGV);
            f.Show();     // Mở form Lịch sử
        }

        private void ResetForm()
        {
            txtTenGame.Clear();
            txtCauHoi.Clear();
            dgvCauHoi.Rows.Clear();
            _danhSachCauHoiTam.Clear();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}