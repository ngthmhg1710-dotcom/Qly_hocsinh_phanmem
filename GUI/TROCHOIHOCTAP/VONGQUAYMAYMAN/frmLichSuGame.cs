// File: frmLichSuGame.cs
using BUS; // Thêm tham chiếu BUS
using DTO; // Thêm tham chiếu DTO
using DTO.Game; // Thêm tham chiếu DTO

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RANDOMSO
{
    public partial class frmLichSuGame : Form
    {
        private int _giaoVienID;

        // Khởi tạo lớp nghiệp vụ
        private XuLyNghiepVu bus = new XuLyNghiepVu();

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
                // GỌI BUS: Lấy danh sách lịch sử dưới dạng List<LichSuGameDTO>
                List<LichSuGameDTO> historyList = bus.LayDanhSachLichSu(_giaoVienID);

                // Đổ dữ liệu vào DataGridView
                dgvLichSuGame.DataSource = historyList;

                // Bỏ chọn tất cả các dòng ban đầu
                dgvLichSuGame.ClearSelection();

                // Tùy chỉnh hiển thị cột (Các tên cột dựa trên Property của LichSuGameDTO)
                if (dgvLichSuGame.Columns["ID_GameInstance"] != null)
                    dgvLichSuGame.Columns["ID_GameInstance"].Visible = false;

                if (dgvLichSuGame.Columns["TenGame"] != null)
                {
                    dgvLichSuGame.Columns["TenGame"].HeaderText = "Tên Bộ Câu Hỏi";
                    dgvLichSuGame.Columns["TenGame"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

                if (dgvLichSuGame.Columns["NgayTao"] != null)
                {
                    dgvLichSuGame.Columns["NgayTao"].HeaderText = "Ngày Tạo";
                    dgvLichSuGame.Columns["NgayTao"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                    dgvLichSuGame.Columns["NgayTao"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải lịch sử game: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvLichSuGame.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một game để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa bộ câu hỏi này không?\nHành động này không thể hoàn tác.", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    // Lấy ID từ dòng đang chọn
                    int selectedGameID = Convert.ToInt32(dgvLichSuGame.CurrentRow.Cells["ID_GameInstance"].Value);

                    // GỌI BUS: Thực hiện xóa
                    bool ketQua = bus.XoaGame(selectedGameID);

                    if (ketQua)
                    {
                        MessageBox.Show("Đã xóa bộ câu hỏi thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadLichSuGame(); // Tải lại danh sách
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa game: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Trong frmLichSuGame.cs

        private void btnChoiLai_Click(object sender, EventArgs e)
        {
            if (dgvLichSuGame.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một game để chơi lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int selectedGameID = Convert.ToInt32(dgvLichSuGame.CurrentRow.Cells["ID_GameInstance"].Value);
                List<CauHoiRandomDTO> questions = bus.LayChiTietGame(selectedGameID);

                if (questions != null && questions.Count > 0)
                {
                    GameForm gameForm = new GameForm(questions);

                    // --- LOGIC ĐIỀU HƯỚNG TỪ LỊCH SỬ ---

                    // 1. Tìm Form Menu Chính (LSVONGQUAY) đang mở
                    Form mainMenu = Application.OpenForms["LSVONGQUAY"];

                    if (mainMenu != null)
                    {
                        mainMenu.Hide(); // Ẩn Menu chính đi

                        // Khi Game đóng -> Hiện lại Menu chính
                        gameForm.FormClosed += (s, args) => mainMenu.Show();
                    }

                    // 2. Hiện Game
                    gameForm.Show();

                    // 3. Đóng form Lịch sử lại (vì đã vào game rồi)
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy câu hỏi nào.", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu game: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}