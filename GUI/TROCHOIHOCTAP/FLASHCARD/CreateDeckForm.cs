using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using BUS; // Gọi lớp nghiệp vụ
using DTO; // Gọi lớp đối tượng dữ liệu

namespace FlashcardFlipGame
{
    public partial class CreateDeckForm : Form
    {
        // Danh sách chứa các thẻ đang tạo (Dùng Flashcard DTO)
        private List<Flashcard> createdCards = new List<Flashcard>();
        private string currentFrontImagePath = null;
        private string currentBackImagePath = null;
        private int _giaoVienID;

        // Khởi tạo lớp Business Logic
        private FlashcardBUS bus = new FlashcardBUS();

        public CreateDeckForm(int giaoVienID)
        {
            InitializeComponent();
            _giaoVienID = giaoVienID;
            lblFrontImagePath.Text = "";
            lblBackImagePath.Text = "";
        }

        #region Chọn và Thêm Thẻ (Giữ nguyên logic giao diện)
        private void btnAddCard_Click(object sender, EventArgs e)
        {
            string frontText = txtFront.Text.Trim();
            string backText = txtBack.Text.Trim();

            // Kiểm tra đầu vào cơ bản tại GUI
            if ((string.IsNullOrEmpty(frontText) && string.IsNullOrEmpty(currentFrontImagePath)) ||
                (string.IsNullOrEmpty(backText) && string.IsNullOrEmpty(currentBackImagePath)))
            {
                MessageBox.Show("Mỗi mặt của thẻ phải có ít nhất văn bản hoặc hình ảnh!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo đối tượng Flashcard (DTO)
            var newCard = new Flashcard(frontText, backText, currentFrontImagePath, currentBackImagePath);
            createdCards.Add(newCard);

            // Cập nhật giao diện
            CreateAndAddPreviewPanel(newCard);
            UpdateCardNumbers();
            ResetInputFields();
        }

        private void btnBrowseFront_Click(object sender, EventArgs e)
        {
            string selectedFile = SelectImageFile();
            if (!string.IsNullOrEmpty(selectedFile))
            {
                currentFrontImagePath = selectedFile;
                lblFrontImagePath.Text = Path.GetFileName(currentFrontImagePath);
            }
        }

        private void btnBrowseBack_Click(object sender, EventArgs e)
        {
            string selectedFile = SelectImageFile();
            if (!string.IsNullOrEmpty(selectedFile))
            {
                currentBackImagePath = selectedFile;
                lblBackImagePath.Text = Path.GetFileName(currentBackImagePath);
            }
        }
        #endregion

        #region Quản lý Giao diện Preview (Giữ nguyên logic hiển thị)

        private string GetCardContentPreview(string text, string imagePath)
        {
            List<string> parts = new List<string>();
            if (!string.IsNullOrEmpty(imagePath))
            {
                parts.Add($"[{Path.GetFileName(imagePath)}]");
            }
            if (!string.IsNullOrEmpty(text))
            {
                parts.Add(text);
            }
            return string.Join(" ", parts);
        }

        private void CreateAndAddPreviewPanel(Flashcard card)
        {
            var cardPanel = new Guna2Panel
            {
                Width = pnlAddedCards.ClientSize.Width - 15,
                Height = 40,
                Margin = new Padding(5),
                Tag = card,
                BorderRadius = 8,
                FillColor = Color.FromArgb(222, 226, 230)
            };

            var deleteButton = new Button
            {
                Text = "X",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.IndianRed,
                Size = new Size(35, 40),
                Dock = DockStyle.Right,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
            };
            deleteButton.FlatAppearance.BorderSize = 0;
            deleteButton.Click += DeleteButton_Click;

            string frontContent = GetCardContentPreview(card.FrontText, card.FrontImagePath);
            string backContent = GetCardContentPreview(card.BackText, card.BackImagePath);

            var cardLabel = new Label
            {
                Text = $"{frontContent}  ➔  {backContent}",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 10),
                Padding = new Padding(10, 0, 0, 0),
                Name = "cardLabel",
                BackColor = Color.Transparent,
                AutoEllipsis = true
            };

            cardPanel.Controls.Add(cardLabel);
            cardPanel.Controls.Add(deleteButton);
            pnlAddedCards.Controls.Add(cardPanel);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            var deleteButton = (Button)sender;
            var cardPanel = (Control)deleteButton.Parent;
            var cardToDelete = (Flashcard)cardPanel.Tag;

            createdCards.Remove(cardToDelete);
            pnlAddedCards.Controls.Remove(cardPanel);
            cardPanel.Dispose();

            UpdateCardNumbers();
        }

        private void UpdateCardNumbers()
        {
            int counter = 1;
            foreach (Control panel in pnlAddedCards.Controls)
            {
                var label = (Label)panel.Controls["cardLabel"];
                var card = (Flashcard)panel.Tag;

                if (label != null && card != null)
                {
                    string frontContent = GetCardContentPreview(card.FrontText, card.FrontImagePath);
                    string backContent = GetCardContentPreview(card.BackText, card.BackImagePath);
                    label.Text = $"{counter}. {frontContent}  ➔  {backContent}";
                }
                counter++;
            }
        }
        #endregion

        #region Tiện ích và Sự kiện Form (Không thay đổi)
        private string SelectImageFile()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Chọn hình ảnh";
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return openFileDialog.FileName;
                }
            }
            return null;
        }

        private void ResetInputFields()
        {
            txtFront.Clear();
            txtBack.Clear();
            lblFrontImagePath.Text = "";
            lblBackImagePath.Text = "";
            currentFrontImagePath = null;
            currentBackImagePath = null;
            txtFront.Focus();
        }

        private void txtBack_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddCard.PerformClick();
                e.SuppressKeyPress = true;
            }
        }
        #endregion

        #region Xử lý Lưu, Chơi và Lịch sử (ĐÃ CẬP NHẬT LOGIC 3 LỚP)

        // Hàm lưu đã được làm gọn, chuyển logic sang BUS
        private bool SaveDeckToDatabase()
        {
            string tenBoThe = txtDeckName.Text.Trim();
            string message;

            // Gọi lớp BUS để xử lý lưu trữ
            // BUS sẽ tự kiểm tra tên rỗng, danh sách rỗng và gọi DAL
            bool result = bus.SaveDeck(tenBoThe, _giaoVienID, createdCards, out message);

            // Hiển thị thông báo trả về từ BUS
            if (result)
            {
                // Nếu cần thông báo thành công riêng biệt thì làm ở btnSaveDeck_Click
                // Hàm này trả về true để btnStartGame biết là đã lưu xong
                return true;
            }
            else
            {
                MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (string.IsNullOrWhiteSpace(tenBoThe))
                {
                    txtDeckName.Focus();
                }
                return false;
            }
        }

        private void btnSaveDeck_Click(object sender, EventArgs e)
        {
            if (SaveDeckToDatabase())
            {
                MessageBox.Show("Đã lưu bộ thẻ thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            // Thử lưu trước khi chơi
            bool isSaved = SaveDeckToDatabase();

            if (isSaved)
            {
                MessageBox.Show("Bộ thẻ đã được lưu vào lịch sử. Bắt đầu học ngay!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Truyền danh sách thẻ sang Form chơi game
                var gameForm = new Form1(createdCards);

                // Khi đóng game thì hiện lại form này
                gameForm.FormClosed += (s, args) => this.Show();

                gameForm.Show();
                this.Hide();
            }
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            frmLichSuBoThe historyForm = new frmLichSuBoThe(_giaoVienID);
            historyForm.ShowDialog();
        }
        #endregion
    }
}