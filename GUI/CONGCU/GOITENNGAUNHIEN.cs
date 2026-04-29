using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data; // Thêm thư viện này
using Guna.UI2.WinForms;
using WMPLib;
using System.IO;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using GUI.Properties;
using DTO; // Gọi thư viện DTO để lấy AuthSession

namespace GUI
{
    public partial class GOITENNGAUNHIEN : Form
    {
        // --- 1. CẤU HÌNH KẾT NỐI ---
        private string connectionString = "Data Source=DESKTOP-C7KPB1H;Initial Catalog=QuanLyHocSinh;Integrated Security=True;";

        // --- 2. HỆ THỐNG ÂM THANH ---
        WindowsMediaPlayer playerSFX = new WindowsMediaPlayer();
        WindowsMediaPlayer playerRunning = new WindowsMediaPlayer();

        // --- 3. BIẾN LOGIC ---
        private List<string> studentList = new List<string>();
        private Random rand = new Random();
        private int tickCount = 0;
        private int minTicks = 30;
        private string finalWinner = "";
        private Font originalFont;

        private List<Color> effectColors = new List<Color>() {
            Color.Red, Color.Gold, Color.Lime, Color.Magenta, Color.Cyan, Color.OrangeRed, Color.DeepPink, Color.BlueViolet
        };

        // --- 4. FONT MONTSERRAT ---
        private PrivateFontCollection customFonts = new PrivateFontCollection();

        public GOITENNGAUNHIEN()
        {
            InitializeComponent();
        }

        // --- 5. LOAD FORM ---
        private void GOITENNGAUNHIEN_Load(object sender, EventArgs e)
        {
            try
            {
                // --- LOAD FONT MONTSERRAT BOLD ---
                byte[] fontData = Properties.Resources.Montserrat;
                IntPtr fontPtr = Marshal.AllocCoTaskMem(fontData.Length);
                Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
                customFonts.AddMemoryFont(fontPtr, fontData.Length);
                Marshal.FreeCoTaskMem(fontPtr);

                // Áp font cho label2
                if (customFonts.Families.Length > 0)
                {
                    label2.Font = new Font(customFonts.Families[0], 28f, FontStyle.Bold);
                }
                originalFont = label2.Font;

                // Cấu hình Label
                label2.AutoSize = true;
                label2.MaximumSize = new Size(guna2Panel3.Width - 30, 0);
                label2.TextAlign = ContentAlignment.MiddleCenter;

                // --- [MỚI] LOAD DATA THEO GIÁO VIÊN ---
                LoadClassDataForTeacher();

                // Đăng ký sự kiện chọn lớp (nếu chưa gán trong Designer)
                cboLop.SelectedIndexChanged -= cboLop_SelectedIndexChanged;
                cboLop.SelectedIndexChanged += cboLop_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo: " + ex.Message);
            }
        }

        // --- HÀM HỖ TRỢ GIAO DIỆN ---
        private void CenterLabelInPanel()
        {
            if (guna2Panel3 != null && label2 != null)
            {
                int x = (guna2Panel3.Width - label2.Width) / 2;
                int y = (guna2Panel3.Height - label2.Height) / 2;
                label2.Location = new Point(x, y);
            }
        }

        // --- [QUAN TRỌNG] LOAD LỚP CỦA GV ĐANG ĐĂNG NHẬP ---
        private void LoadClassDataForTeacher()
        {
            try
            {
                int idGV = AuthSession.CurrentIdGV; // Lấy ID từ Session
                if (idGV <= 0)
                {
                    MessageBox.Show("Không tìm thấy thông tin giáo viên. Vui lòng đăng nhập lại.");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // Lấy danh sách lớp mà GV này được phân công
                    // Join với PhanCongGiangDay để lọc
                    string query = @"
                        SELECT DISTINCT lh.MaLop, lh.TenLop 
                        FROM LopHoc lh
                        JOIN PhanCongGiangDay pc ON lh.MaLop = pc.MaLop
                        WHERE pc.ID_GV = @IDGV
                        ORDER BY lh.TenLop";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@IDGV", idGV);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Gán vào ComboBox Lớp
                    cboLop.DataSource = dt;
                    cboLop.DisplayMember = "TenLop";
                    cboLop.ValueMember = "MaLop";

                    // Trigger chọn lớp đầu tiên để load môn
                    if (cboLop.Items.Count > 0)
                    {
                        cboLop.SelectedIndex = 0;
                        LoadSubjectForTeacher(Convert.ToInt32(cboLop.SelectedValue));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách lớp: " + ex.Message);
            }
        }

        // --- [QUAN TRỌNG] LOAD MÔN CỦA GV TẠI LỚP ĐÓ ---
        private void LoadSubjectForTeacher(int maLop)
        {
            try
            {
                int idGV = AuthSession.CurrentIdGV;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    // Lấy môn mà GV dạy tại lớp đang chọn
                    string query = @"
                        SELECT DISTINCT mh.MaMon, mh.TenMon 
                        FROM MonHoc mh
                        JOIN PhanCongGiangDay pc ON mh.MaMon = pc.MaMon
                        WHERE pc.ID_GV = @IDGV AND pc.MaLop = @MaLop";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@IDGV", idGV);
                    da.SelectCommand.Parameters.AddWithValue("@MaLop", maLop);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Gán vào ComboBox Môn
                    cboMon.DataSource = dt;
                    cboMon.DisplayMember = "TenMon";
                    cboMon.ValueMember = "MaMon";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách môn: " + ex.Message);
            }
        }

        // Sự kiện: Khi chọn lớp khác -> Tự động load lại môn tương ứng
        private void cboLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLop.SelectedValue != null && int.TryParse(cboLop.SelectedValue.ToString(), out int maLop))
            {
                LoadSubjectForTeacher(maLop);
            }
        }

        // --- LẤY DANH SÁCH HỌC SINH THEO MÃ LỚP ---
        private List<string> GetStudentsInClass(int maLop)
        {
            List<string> students = new List<string>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT HoTen FROM HocSinh WHERE MaLop = @MaLop";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MaLop", maLop);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                                students.Add(reader["HoTen"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy DS học sinh: " + ex.Message);
            }
            return students;
        }

        // --- 6. BẮT ĐẦU RANDOM ---
        private void btnThem_Click(object sender, EventArgs e)
        {
            // Kiểm tra ràng buộc
            if (cboLop.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Lớp!");
                return;
            }
            if (cboMon.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Môn!");
                return;
            }

            int maLop = Convert.ToInt32(cboLop.SelectedValue);

            // Lấy danh sách HS
            studentList = GetStudentsInClass(maLop);

            if (studentList.Count == 0)
            {
                MessageBox.Show("Lớp này chưa có học sinh nào!");
                return;
            }

            // --- LOGIC RANDOM CŨ GIỮ NGUYÊN ---
            label2.Font = originalFont;
            label2.ForeColor = Color.FromArgb(39, 37, 102);
            finalWinner = studentList[rand.Next(studentList.Count)];
            tickCount = 0;

            // Click Sound
            string pathClick = GetSoundFile("click.mp3", Properties.Resources.click2);
            if (File.Exists(pathClick))
            {
                playerSFX.URL = pathClick;
                playerSFX.controls.play();
            }

            // Running Sound
            string pathRun = GetSoundFile("random.mp3", Properties.Resources.random);
            if (File.Exists(pathRun))
            {
                playerRunning.URL = pathRun;
                playerRunning.settings.setMode("loop", true);
                playerRunning.controls.play();
            }

            timer1.Interval = 80;
            timer1.Start();
        }

        // --- 7. TIMER CHẠY ---
        private void Timer1_Tick(object sender, EventArgs e)
        {
            tickCount++;

            label2.Text = studentList[rand.Next(studentList.Count)];
            CenterLabelInPanel();
            label2.ForeColor = (tickCount % 2 == 0) ? Color.DimGray : Color.Black;

            if (tickCount > minTicks * 0.7)
                timer1.Interval = (int)(timer1.Interval * 1.12);

            if (timer1.Interval > 600 && tickCount > minTicks)
            {
                timer1.Stop();
                playerRunning.controls.stop();
                PerformWinnerEffect();
            }
        }

        // --- 8. HIỆU ỨNG CHIẾN THẮNG ---
        private async void PerformWinnerEffect()
        {
            label2.Text = finalWinner;
            CenterLabelInPanel();

            string pathWin = GetSoundFile("win.mp3", Properties.Resources.win);
            if (File.Exists(pathWin))
            {
                playerSFX.URL = pathWin;
                playerSFX.controls.play();
            }

            float maxAddSize = (finalWinner.Length > 15) ? 6 : 15;
            float currentSize = originalFont.Size - 6;
            float targetSize = originalFont.Size + maxAddSize;
            label2.Font = new Font(customFonts.Families[0], currentSize, FontStyle.Bold);
            CenterLabelInPanel();

            while (currentSize < targetSize)
            {
                currentSize += 2;
                label2.Font = new Font(customFonts.Families[0], currentSize, FontStyle.Bold);
                CenterLabelInPanel();
                await Task.Delay(25);
            }

            Point centerPoint = label2.Location;
            for (int i = 0; i < 25; i++)
            {
                int offsetX = rand.Next(-6, 7);
                int offsetY = rand.Next(-6, 7);

                label2.Location = new Point(centerPoint.X + offsetX, centerPoint.Y + offsetY);
                label2.ForeColor = effectColors[rand.Next(effectColors.Count)];

                await Task.Delay(40);
            }

            label2.Location = centerPoint;
            label2.ForeColor = Color.Red;
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            // Nút Refresh
            LoadClassDataForTeacher();
            MessageBox.Show("Đã cập nhật danh sách lớp và môn học!");
        }

        // --- 9. NÚT TẮT FORM: STOP TOÀN BỘ ÂM THANH ---
        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            try
            {
                playerSFX.controls.stop();
                playerRunning.controls.stop();
            }
            catch { }

            this.Close();
        }

        // --- 10. HÀM HỖ TRỢ FILE ---
        private string GetSoundFile(string filename, System.IO.Stream resourceStream)
        {
            try
            {
                string tempPath = Path.Combine(Path.GetTempPath(), "MyRandomAppSounds");
                if (!Directory.Exists(tempPath))
                    Directory.CreateDirectory(tempPath);

                string filePath = Path.Combine(tempPath, filename);
                if (resourceStream != null)
                {
                    using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                        resourceStream.CopyTo(fileStream);
                }
                return filePath;
            }
            catch
            {
                return Path.Combine(Path.GetTempPath(), "MyRandomAppSounds", filename);
            }
        }
    }
}