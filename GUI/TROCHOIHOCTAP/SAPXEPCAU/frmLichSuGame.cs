using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BUS; // Gọi BUS
using DTO; // Gọi DTO

namespace SapXepCau
{
    public partial class frmLichSuGame : Form
    {
        private int _giaoVienID;
        private GameSapXepCauBUS bus = new GameSapXepCauBUS();

        public frmLichSuGame(int giaoVienID)
        {
            InitializeComponent();
            _giaoVienID = giaoVienID;
        }

        private void frmLichSuGame_Load(object sender, EventArgs e)
        {
            LoadLichSuGame();
        }

        private void LoadLichSuGame()
        {
            try
            {
                dgvLichSu.DataSource = bus.LayLichSuGame(_giaoVienID);
                dgvLichSu.ClearSelection();

                // Ẩn cột ID không cần thiết
                if (dgvLichSu.Columns["ID_GameInstance"] != null)
                    dgvLichSu.Columns["ID_GameInstance"].Visible = false;
                if (dgvLichSu.Columns["ID_GV"] != null)
                    dgvLichSu.Columns["ID_GV"].Visible = false;

                // Đổi tên cột hiển thị cho đẹp
                if (dgvLichSu.Columns["TenGame"] != null)
                {
                    dgvLichSu.Columns["TenGame"].HeaderText = "Tên Bài Tập";
                    dgvLichSu.Columns["TenGame"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                if (dgvLichSu.Columns["NgayTao"] != null)
                {
                    dgvLichSu.Columns["NgayTao"].HeaderText = "Ngày Tạo";
                    dgvLichSu.Columns["NgayTao"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                    dgvLichSu.Columns["NgayTao"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải lịch sử: " + ex.Message);
            }
        }

        // --- NÚT CHƠI LẠI ---
        private void btnChoiLai_Click(object sender, EventArgs e)
        {
            if (dgvLichSu.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn bài tập để chơi!");
                return;
            }

            int idGame = Convert.ToInt32(dgvLichSu.CurrentRow.Cells["ID_GameInstance"].Value);
            string tenGame = dgvLichSu.CurrentRow.Cells["TenGame"].Value.ToString();

            // Mở form chơi game và truyền ID Giáo viên vào
            // (Để khi chơi xong hoặc đóng game, nó biết quay về trang Tạo Game của giáo viên này)
            frmChoiGame f = new frmChoiGame(idGame, tenGame, _giaoVienID);

            f.Show();     // Hiển thị form Game
            this.Close(); // Đóng form Lịch sử
        }

        // --- NÚT XÓA ---
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvLichSu.CurrentRow == null) return;

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa bài tập này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                int idGame = Convert.ToInt32(dgvLichSu.CurrentRow.Cells["ID_GameInstance"].Value);

                if (bus.XoaGame(idGame))
                {
                    MessageBox.Show("Đã xóa thành công!");
                    LoadLichSuGame(); // Tải lại danh sách
                }
                else
                {
                    MessageBox.Show("Lỗi khi xóa bài tập!");
                }
            }
        }

        // --- NÚT ĐÓNG ---
        private void btnClose_Click(object sender, EventArgs e)
        {
            // Khi đóng lịch sử, quay lại trang Tạo Game
            frmTaoGame f = new frmTaoGame(_giaoVienID);
            f.Show();
            this.Close();
        }
    }
}