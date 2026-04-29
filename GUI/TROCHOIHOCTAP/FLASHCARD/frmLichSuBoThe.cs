using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BUS; // Gọi lớp nghiệp vụ
using DTO; // Gọi lớp đối tượng dữ liệu

namespace FlashcardFlipGame
{
    public partial class frmLichSuBoThe : Form
    {
        private int _giaoVienID;
        // Khởi tạo lớp Business Logic để sử dụng
        private FlashcardBUS bus = new FlashcardBUS();

        public frmLichSuBoThe(int giaoVienID)
        {
            InitializeComponent();
            _giaoVienID = giaoVienID;
        }

        private void frmLichSuBoThe_Load(object sender, EventArgs e)
        {
            LoadLichSuBoThe();
        }

        private void LoadLichSuBoThe()
        {
            try
            {
                // GỌI BUS: Lấy dữ liệu lịch sử
                dgvLichSu.DataSource = bus.GetHistory(_giaoVienID);
                dgvLichSu.ClearSelection();

                // Định dạng hiển thị DataGridView
                if (dgvLichSu.Columns["ID_GameInstance"] != null)
                    dgvLichSu.Columns["ID_GameInstance"].Visible = false;

                if (dgvLichSu.Columns["TenBoThe"] != null)
                {
                    dgvLichSu.Columns["TenBoThe"].HeaderText = "Tên Bộ Thẻ";
                    dgvLichSu.Columns["TenBoThe"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
                MessageBox.Show("Lỗi khi tải lịch sử bộ thẻ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnChoiLai_Click(object sender, EventArgs e)
        {
            if (dgvLichSu.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một bộ thẻ để chơi lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy form CreateDeckForm đang mở (Logic giao diện giữ nguyên)
            var createForm = Application.OpenForms["CreateDeckForm"] as CreateDeckForm;
            if (createForm == null)
            {
                MessageBox.Show("Lỗi: Không tìm thấy form tạo bộ thẻ chính.", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int selectedGameID = Convert.ToInt32(dgvLichSu.CurrentRow.Cells["ID_GameInstance"].Value);

            try
            {
                // GỌI BUS: Lấy chi tiết bộ thẻ (BUS đã tự convert sang List<Flashcard>)
                List<Flashcard> cards = bus.GetDeckDetails(selectedGameID);

                if (cards != null && cards.Count > 0)
                {
                    // 1. Tạo form chơi game
                    Form1 gameForm = new Form1(cards);

                    // 2. Đăng ký sự kiện: Khi gameForm đóng, thì hiển thị lại createForm
                    gameForm.FormClosed += (s, args) => {
                        createForm.Show();
                    };

                    // 3. Hiển thị form chơi game
                    gameForm.Show();

                    // 4. Ẩn form tạo game đi
                    createForm.Hide();

                    // 5. Đóng form lịch sử
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Không thể tải được các thẻ cho bộ này hoặc bộ thẻ trống.", "Không tìm thấy dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu bộ thẻ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvLichSu.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một bộ thẻ để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa bộ thẻ này không?\nHành động này không thể hoàn tác.", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    int selectedGameID = Convert.ToInt32(dgvLichSu.CurrentRow.Cells["ID_GameInstance"].Value);

                    // GỌI BUS: Thực hiện xóa
                    bool isDeleted = bus.DeleteDeck(selectedGameID);

                    if (isDeleted)
                    {
                        MessageBox.Show("Đã xóa bộ thẻ thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadLichSuBoThe(); // Tải lại danh sách
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa bộ thẻ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}