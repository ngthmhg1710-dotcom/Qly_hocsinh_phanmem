using BUS;
using DTO;
using DTO.Game; // Đảm bảo có dòng này
using GAME1_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace RANDOMSO
{
    public partial class LSVONGQUAY : Form
    {
        private int questionCounter = 0;
        private XuLyNghiepVu bus = new XuLyNghiepVu();

        public LSVONGQUAY()
        {
            InitializeComponent();
        }

        // ... (Giữ nguyên các hàm quản lý thẻ: THEMTHE, DeleteClicked, RenumberCards) ...
        #region Chức năng quản lý thẻ câu hỏi
        private void THEMTHE_Click(object sender, EventArgs e)
        {
            questionCounter++;
            TheCauHoi newCard = new TheCauHoi(questionCounter);
            newCard.DeleteClicked += TheCauHoi_DeleteClicked;
            pnlContainer.Controls.Add(newCard);
        }

        private void TheCauHoi_DeleteClicked(object sender, EventArgs e)
        {
            if (sender is TheCauHoi cardToDelete)
            {
                pnlContainer.Controls.Remove(cardToDelete);
                cardToDelete.Dispose();
                RenumberCards();
            }
        }

        private void RenumberCards()
        {
            int currentNumber = 1;
            foreach (TheCauHoi card in pnlContainer.Controls.OfType<TheCauHoi>())
            {
                card.UpdateQuestionNumber(currentNumber);
                currentNumber++;
            }
            questionCounter = pnlContainer.Controls.OfType<TheCauHoi>().Count();
        }
        #endregion

        #region Chức năng chính

        private void TAO_Click(object sender, EventArgs e)
        {
            string tenGame = txtTenBoCauHoi.Text.Trim();
            if (string.IsNullOrWhiteSpace(tenGame))
            {
                MessageBox.Show("Vui lòng nhập tên cho bộ câu hỏi!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenBoCauHoi.Focus();
                return;
            }

            // SỬA LỖI AMBIGUOUS: Chỉ định rõ DTO.Game.CauHoiRandomDTO
            List<DTO.Game.CauHoiRandomDTO> validQuestions = pnlContainer.Controls.OfType<TheCauHoi>()
        .Select(card => card.GetQuestionData()) // Hàm này giờ đã trả về đúng kiểu
        .Where(q => !string.IsNullOrWhiteSpace(q.QuestionText) || !string.IsNullOrEmpty(q.ImagePath))
        .ToList();

            if (validQuestions.Count == 0)
            {
                MessageBox.Show("Vui lòng tạo ít nhất một câu hỏi có nội dung!", "Chưa có câu hỏi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bool luuThanhCong = bus.LuuGame(tenGame, DTO.UserSession.TeacherId, validQuestions);

                if (luuThanhCong)
                {
                    GameForm gameForm = new GameForm(validQuestions);

                    // --- LOGIC ĐIỀU HƯỚNG MỚI ---
                    this.Hide(); // 1. Ẩn form Menu hiện tại

                    // 2. Đăng ký sự kiện: Khi GameForm đóng -> Hiện lại Menu này
                    gameForm.FormClosed += (s, args) =>
                    {
                        this.Show();
                        // Tùy chọn: Reset lại các thẻ câu hỏi nếu muốn
                    };

                    gameForm.Show(); // 3. Hiện GameForm
                                     // -----------------------------
                }
                else
                {
                    MessageBox.Show("Không thể lưu game. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi hệ thống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXemLichSu_Click(object sender, EventArgs e)
        {
            frmLichSuGame historyForm = new frmLichSuGame(DTO.UserSession.TeacherId);

            // Mở form lịch sử dưới dạng Dialog (Cửa sổ con)
            // Truyền 'this' để form con biết ai là cha (dùng cho Owner)
            historyForm.ShowDialog(this);
        }

        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}