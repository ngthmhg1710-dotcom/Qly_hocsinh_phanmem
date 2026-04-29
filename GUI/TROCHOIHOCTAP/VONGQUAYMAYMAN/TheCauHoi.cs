// File: TheCauHoi.cs
using System;
using System.Windows.Forms;
using DTO.Game; // QUAN TRỌNG: Dùng namespace chứa CauHoiRandomDTO mới

namespace RANDOMSO
{
    public partial class TheCauHoi : UserControl
    {
        // Sử dụng đúng class CauHoiRandomDTO
        private CauHoiRandomDTO _questionData;
        public event EventHandler DeleteClicked;

        public TheCauHoi(int questionNumber)
        {
            InitializeComponent();

            // SỬA LỖI 1: Khởi tạo đúng class CauHoiRandomDTO
            _questionData = new CauHoiRandomDTO
            {
                QuestionNumber = questionNumber
            };

            // Kiểm tra xem label 'lbl' có tồn tại trong Designer không. 
            // Nếu tên label của bạn là lblSoCau hoặc tên khác thì sửa lại ở đây.
            if (lbl != null) lbl.Text = $"Câu hỏi {questionNumber}";

            this.Name = $"TheCauHoi_{questionNumber}";
        }

        public void UpdateQuestionNumber(int newNumber)
        {
            _questionData.QuestionNumber = newNumber;
            if (lbl != null) lbl.Text = $"Câu hỏi {newNumber}";
            this.Name = $"TheCauHoi_{newNumber}";
        }

        // SỬA LỖI 2: Kiểu trả về phải là CauHoiRandomDTO
        public CauHoiRandomDTO GetQuestionData()
        {
            // Cập nhật text mới nhất từ giao diện vào DTO
            // Đảm bảo 'txtCauHoi' là tên đúng của TextBox nhập nội dung
            _questionData.QuestionText = txtCauHoi.Text.Trim();
            return _questionData;
        }

        private void picHinhAnh_Click(object sender, EventArgs e)
        {
            ChonHinhAnh();
        }

        private void btnPlaceholder_Click(object sender, EventArgs e)
        {
            ChonHinhAnh();
        }

        private void ChonHinhAnh()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif";
                ofd.Title = "Chọn hình ảnh cho câu hỏi";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    picHinhAnh.ImageLocation = ofd.FileName;
                    // Lưu đường dẫn vào DTO
                    _questionData.ImagePath = ofd.FileName;
                    btnPlaceholder.Visible = false;
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DeleteClicked?.Invoke(this, EventArgs.Empty);
        }

        private void TheCauHoi_Load(object sender, EventArgs e)
        {
        }

        private void guna2Panel1_Paint_1(object sender, PaintEventArgs e)
        {
        }
    }
}