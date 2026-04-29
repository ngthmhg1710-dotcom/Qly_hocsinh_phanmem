using BUS;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GUI
{
    public partial class UC_THONGKE : UserControl
    {
        private int maLopHienTai;
        private int maMonHienTai;
        private LopHocBUS bus = new LopHocBUS();

        public UC_THONGKE()
        {
            InitializeComponent();
        }

        public UC_THONGKE(int maLop, int maMon) : this()
        {
            this.maLopHienTai = maLop;
            this.maMonHienTai = maMon;

            // --- BẬT THANH CUỘN ---
            this.AutoScroll = true; // Cho phép cuộn nếu nội dung tràn
            this.AutoScrollMinSize = new Size(0, 700); // Đặt chiều cao tối thiểu là 700px (hoặc cao hơn nếu cần)
                                                       // ----------------------

            ReloadData();
        }

        private void UC_THONGKE_Load(object sender, EventArgs e)
        {
            // Để trống hoặc gọi ReloadData() nếu cần chắc chắn
        }

        public void ReloadData()
        {
            // Kiểm tra dữ liệu đầu vào
            if (this.maLopHienTai <= 0 || this.maMonHienTai <= 0)
            {
                return;
            }

            UpdateTieuDe();
            VeBieuDo();
        }

        private void UpdateTieuDe()
        {
            try
            {
                string tenLop = bus.GetTenLop(this.maLopHienTai);
                string tenMon = bus.GetTenMon(this.maMonHienTai);
                lblTieuDe.Text = $"THỐNG KÊ KẾT QUẢ LỚP {tenLop.ToUpper()} - MÔN {tenMon.ToUpper()}";
            }
            catch { }
        }

        private void VeBieuDo()
        {
            try
            {
                DataTable dt = bus.GetThongKeXepLoai(this.maLopHienTai, this.maMonHienTai);

                // 1. CẤU HÌNH SERIES
                if (chartThongKe.Series.Count == 0)
                {
                    chartThongKe.Series.Add("Series1");
                    chartThongKe.Series[0].ChartType = SeriesChartType.Pie;
                }

                // ========================================================
                // 🔴 ĐOẠN CODE MỚI: CHỈNH VỊ TRÍ CHÚ THÍCH SANG PHẢI
                // ========================================================
                if (chartThongKe.Legends.Count > 0)
                {
                    Legend l = chartThongKe.Legends[0];

                    // 1. Đưa xuống dưới đáy (Thay vì bên phải) -> Biểu đồ sẽ ra giữa
                    l.Docking = Docking.Bottom;

                    // 2. Căn giữa hàng ngang
                    l.Alignment = StringAlignment.Center;

                    // 3. Đảm bảo chú thích không đè lên biểu đồ
                    l.IsDockedInsideChartArea = false;

                    // 4. Chỉnh font chữ đẹp hơn
                    l.Font = new Font("Segoe UI", 11, FontStyle.Regular);

                    // 5. (Mới) Tạo viền bảng chú thích cho rõ ràng (Tùy chọn)
                    l.BorderColor = Color.LightGray;
                    l.TableStyle = LegendTableStyle.Wide; // Dàn ngang ra cho đẹp
                }

                // --- CẤU HÌNH BIỂU ĐỒ TRÒN CHO TO RA ---
                ChartArea ca = chartThongKe.ChartAreas[0];

                // Tự động căn chỉnh nội dung bên trong cho tối đa diện tích
                ca.Position.Auto = true;

                // Quan trọng: Chỉnh InnerPlotPosition để biểu đồ tròn to ra, không bị khoảng trắng thừa
                ca.InnerPlotPosition = new ElementPosition(5, 5, 90, 90); // (X, Y, Width, Height) %
                // ========================================================

                Series s = chartThongKe.Series[0];
                s.Points.Clear();
                s.LabelFormat = "P1";

                if (dt == null || dt.Rows.Count == 0)
                {
                    s.Points.AddXY("Chưa có dữ liệu", 1);
                    s.Points[0].Color = Color.LightGray;
                    return;
                }

                foreach (DataRow row in dt.Rows)
                {
                    string xepLoai = row["XepLoai"].ToString();
                    int soLuong = Convert.ToInt32(row["SoLuong"]);

                    if (soLuong > 0)
                    {
                        int pIndex = s.Points.AddXY(xepLoai, soLuong);
                        DataPoint p = s.Points[pIndex];

                        p.Label = "#PERCENT"; // Trên bánh chỉ hiện % cho gọn
                        p.LegendText = $"{xepLoai} ({soLuong} HS)"; // Chú thích bên phải hiện đầy đủ

                        switch (xepLoai)
                        {
                            case "Giỏi": p.Color = Color.FromArgb(46, 204, 113); break;
                            case "Khá": p.Color = Color.FromArgb(52, 152, 219); break;
                            case "Trung Bình": p.Color = Color.FromArgb(241, 196, 15); break;
                            case "Yếu": p.Color = Color.FromArgb(231, 76, 60); break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi vẽ biểu đồ: " + ex.Message);
            }
        }
    }
}