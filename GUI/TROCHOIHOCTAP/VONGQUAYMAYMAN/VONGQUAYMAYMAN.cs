using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DTO.Game; // Bắt buộc phải có
using DTO;

namespace RANDOMSO
{
    public partial class VONGQUAYMAYMAN : Form
    {
        private int questionCounter = 0;

        public VONGQUAYMAYMAN()
        {
            InitializeComponent();
        }

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

        private void TAO_Click(object sender, EventArgs e)
        {
            // SỬA LỖI Ở ĐÂY:
            // 1. Đổi List<Question> thành List<CauHoiRandomDTO>
            List<CauHoiRandomDTO> validQuestions = new List<CauHoiRandomDTO>();

            foreach (TheCauHoi card in pnlContainer.Controls.OfType<TheCauHoi>())
            {
                // 2. Đổi kiểu dữ liệu biến nhận về thành CauHoiRandomDTO
                // Lưu ý: Đảm bảo file TheCauHoi.cs đã sửa hàm GetQuestionData() trả về CauHoiRandomDTO như các bước trước
                CauHoiRandomDTO currentQuestionData = card.GetQuestionData();

                if (!string.IsNullOrWhiteSpace(currentQuestionData.QuestionText) || !string.IsNullOrEmpty(currentQuestionData.ImagePath))
                {
                    validQuestions.Add(currentQuestionData);
                }
            }

            if (validQuestions.Count == 0)
            {
                MessageBox.Show("Vui lòng tạo ít nhất một câu hỏi có nội dung (văn bản hoặc hình ảnh) trước khi bắt đầu game!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 3. Bây giờ validQuestions đã đúng kiểu dữ liệu mà GameForm yêu cầu
            GameForm gameForm = new GameForm(validQuestions);
            gameForm.Show();
            this.Hide();
        }
    }
}