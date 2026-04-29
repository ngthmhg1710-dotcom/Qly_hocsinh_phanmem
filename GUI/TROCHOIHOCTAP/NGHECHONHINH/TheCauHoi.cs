using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using DTO; // Tham chiếu Project DTO

namespace GAME1_
{
    public partial class TheCauHoi : UserControl
    {
        // Biến lưu trữ các picture box
        private Guna2PictureBox[] pics;
        // Biến lưu control đang được click (để phục vụ việc thay đổi ảnh lẻ)
        private Guna2PictureBox selectedPic = null;

        // Màu sắc quy định
        private readonly Color colorCorrect = Color.FromArgb(102, 255, 102); // Xanh lá (Đáp án đúng)
        private readonly Color colorDefault = Color.RosyBrown;               // Màu mặc định

        // Sự kiện để gửi tín hiệu xóa về cho Form cha
        public event EventHandler XoaTheRequested;

        public TheCauHoi()
        {
            InitializeComponent();
        }

        // --- GÁN SỰ KIỆN SAU KHI UC ĐƯỢC TẢI LÊN ---
        private void TheCauHoi_Load(object sender, EventArgs e)
        {
            // Gom các Guna2PictureBox vào mảng để dễ xử lý
            pics = new Guna2PictureBox[] { this.pic1, this.pic2, this.pic3 };

            // 1. GÁN SỰ KIỆN CLICK CHO CÁC HÌNH (ĐỂ TẢI ẢNH)
            foreach (var pic in pics)
            {
                DateTime lastClick = DateTime.MinValue;

                pic.Click += (s, ev) =>
                {
                    var currentPic = (Guna2PictureBox)s;
                    var now = DateTime.Now;

                    // Logic: Double click hoặc click vào ô trống -> Mở chọn ảnh
                    if ((now - lastClick).TotalMilliseconds < 300 || currentPic.Image == null)
                    {
                        ChonAnh(currentPic);
                        lastClick = DateTime.MinValue;
                        return;
                    }

                    lastClick = now;
                    selectedPic = currentPic; // Lưu lại ô đang focus

                    // Hiệu ứng viền đen để biết đang chọn ô nào (để thay ảnh nếu muốn)
                    foreach (var p in pics)
                    {
                        if (p.FillColor != colorCorrect) p.BorderStyle = BorderStyle.None;
                    }
                    if (selectedPic.FillColor != colorCorrect)
                    {
                        selectedPic.BorderStyle = BorderStyle.FixedSingle;
                    }
                };
            }

            // 2. GÁN SỰ KIỆN CHO NÚT ÂM THANH
            sound.Click += (s, ev) => ChonAmThanh(sound);

            // 3. GÁN SỰ KIỆN CHO NÚT CHỌN ĐÁP ÁN (LOGIC MỚI: XOAY VÒNG)
            btnChonDapAn.Click += (s, ev) =>
            {
                // Tìm xem hiện tại ô nào đang là đáp án đúng (màu xanh)
                int currentIndex = -1;
                for (int i = 0; i < pics.Length; i++)
                {
                    if (pics[i].FillColor == colorCorrect)
                    {
                        currentIndex = i;
                        break;
                    }
                }

                // Tính toán vị trí tiếp theo (Vòng tròn: 0 -> 1 -> 2 -> 0)
                int nextIndex = (currentIndex + 1) % pics.Length;

                // Tô màu cho ô tiếp theo
                ChonDapAnDung(pics, pics[nextIndex]);
            };

            // 4. GÁN SỰ KIỆN CHO NÚT XÓA THẺ
            btnXoa.Click += (s, ev) =>
            {
                XoaTheRequested?.Invoke(this, EventArgs.Empty);
            };
        }

        // --- CÁC HÀM XỬ LÝ LOGIC ---

        /// <summary>
        /// Hàm tô màu xanh cho đáp án đúng và reset các ô còn lại
        /// </summary>
        private void ChonDapAnDung(Guna2PictureBox[] pics, Guna2PictureBox dung)
        {
            foreach (var p in pics)
            {
                // Reset về mặc định
                p.FillColor = colorDefault;
                p.ShadowDecoration.Enabled = false;
                p.BorderStyle = BorderStyle.None;
            }

            // Highlight đáp án đúng
            dung.FillColor = colorCorrect;
            dung.ShadowDecoration.Enabled = true;
            dung.ShadowDecoration.Color = Color.FromArgb(0, 180, 0);
            dung.ShadowDecoration.Depth = 25;
            dung.ShadowDecoration.Shadow = new Padding(7);
        }

        /// <summary>
        /// Hàm chọn ảnh (Đã sửa logic: Chọn 3 ảnh thì tự điền vào 3 ô)
        /// </summary>
        private void ChonAnh(Guna2PictureBox targetPic)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Chọn hình ảnh (Giữ Ctrl để chọn nhiều hình)";
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                ofd.Multiselect = true; // Cho phép chọn nhiều

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string[] files = ofd.FileNames;

                    // --- LOGIC MỚI Ở ĐÂY ---

                    // TRƯỜNG HỢP 1: Nếu người dùng chọn đủ 3 ảnh (hoặc hơn)
                    // Thì điền thẳng vào Ô 1, Ô 2, Ô 3 luôn (bất kể họ đang bấm vào ô nào)
                    if (files.Length >= 3)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            SetImageToPic(pics[i], files[i]);
                        }
                    }
                    // TRƯỜNG HỢP 2: Nếu chọn 1 hoặc 2 ảnh
                    // Thì điền bắt đầu từ ô họ đang bấm
                    else
                    {
                        int startIndex = Array.IndexOf(pics, targetPic);
                        if (startIndex == -1) startIndex = 0;

                        for (int i = 0; i < files.Length; i++)
                        {
                            // Kiểm tra để không bị tràn (VD: Bấm ô 3 mà chọn 2 ảnh thì chỉ điền được 1 cái)
                            if (startIndex + i < pics.Length)
                            {
                                SetImageToPic(pics[startIndex + i], files[i]);
                            }
                        }
                    }
                }
            }
        }

        private void SetImageToPic(Guna2PictureBox p, string filePath)
        {
            try
            {
                if (p.Image != null) p.Image.Dispose();
                p.Image = Image.FromFile(filePath);
                p.SizeMode = PictureBoxSizeMode.StretchImage;
                p.Tag = filePath;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải ảnh: {filePath}\n{ex.Message}");
            }
        }

        private void ChonAmThanh(Guna2PictureBox pic)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Chọn tệp âm thanh";
                ofd.Filter = "Audio Files|*.mp3;*.wav;*.m4a;*.wma";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pic.Tag = ofd.FileName;
                    MessageBox.Show("Đã chọn âm thanh: " + System.IO.Path.GetFileName(ofd.FileName));
                }
            }
        }

        // --- CÁC HÀM PUBLIC ĐỂ FORM CHA GỌI ---

        public void SetSoCauHoi(int so)
        {
            lbl.Text = $"Câu hỏi {so}";
        }

        public CauHoiHinhAnhDTO GetCauHoiDTO()
        {
            string soundPath = "";
            int dapAn = -1;
            var picPaths = new List<string>();

            // 1. Lấy đường dẫn ảnh và đáp án
            for (int i = 0; i < this.pics.Length; i++)
            {
                if (this.pics[i].Tag == null) return null; // Chưa có ảnh

                picPaths.Add(this.pics[i].Tag.ToString());

                if (this.pics[i].FillColor == colorCorrect)
                {
                    dapAn = i;
                }
            }

            // 2. Lấy âm thanh
            if (this.sound.Tag == null) return null;
            soundPath = this.sound.Tag.ToString();

            // 3. Trả về DTO
            if (picPaths.Count == 3 && !string.IsNullOrEmpty(soundPath) && dapAn != -1)
            {
                return new CauHoiHinhAnhDTO(picPaths, soundPath, dapAn);
            }

            return null;
        }

        // Các event click rỗng do Designer sinh ra
        private void pic1_Click(object sender, EventArgs e) { }
        private void btnChonDapAn_Click(object sender, EventArgs e) { }
    }
}