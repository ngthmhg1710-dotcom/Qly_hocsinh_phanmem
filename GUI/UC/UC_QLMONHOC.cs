using BUS;
using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using GUI.Properties;

namespace GUI
{
    public partial class UC_QLMONHOC : UserControl
    {
        public UC_QLMONHOC()
        {
            InitializeComponent();

            // Tắt tự động tạo cột
            this.dataGridViewMon.AutoGenerateColumns = false;
            this.dataGridViewLop.AutoGenerateColumns = false;

            // Gán các sự kiện
            this.Load += UC_QLMONHOC_Load;

            // CHỈ DÙNG CELL CONTENT CLICK
            this.dataGridViewMon.CellContentClick += dataGridViewMon_CellContentClick;
            this.dataGridViewLop.CellContentClick += dataGridViewLop_CellContentClick;

            // --- CẤU HÌNH ICON ---
            if (dataGridViewMon.Columns["colDelete"] is DataGridViewImageColumn)
            {
                ((DataGridViewImageColumn)dataGridViewMon.Columns["colDelete"]).Image = GUI.Properties.Resources.dustbin;
                ((DataGridViewImageColumn)dataGridViewMon.Columns["colDelete"]).ImageLayout = DataGridViewImageCellLayout.Zoom;
            }

            if (dataGridViewLop.Columns["colLopDelete"] is DataGridViewImageColumn)
            {
                ((DataGridViewImageColumn)dataGridViewLop.Columns["colLopDelete"]).Image = GUI.Properties.Resources.dustbin;
                ((DataGridViewImageColumn)dataGridViewLop.Columns["colLopDelete"]).ImageLayout = DataGridViewImageCellLayout.Zoom;
            }

            if (dataGridViewLop.Columns["colLopEdit"] is DataGridViewImageColumn)
            {
                ((DataGridViewImageColumn)dataGridViewLop.Columns["colLopEdit"]).Image = GUI.Properties.Resources.edit;
                ((DataGridViewImageColumn)dataGridViewLop.Columns["colLopEdit"]).ImageLayout = DataGridViewImageCellLayout.Zoom;
            }
        }

        private void UC_QLMONHOC_Load(object sender, EventArgs e)
        {
            LoadDataMonHoc();
            LoadDataLopHoc();
        }

        private void LoadDataMonHoc()
        {
            // 1. Lấy dữ liệu mới từ Database về trước
            DataTable dt = MonHocBUS.GetAllMonHoc();

            // 2. Ngắt kết nối nguồn cũ (Quan trọng nhất)
            dataGridViewMon.DataSource = null;

            // 3. Gán nguồn mới
            dataGridViewMon.DataSource = dt;

            // 4. Vẽ lại lưới ngay lập tức
            dataGridViewMon.Refresh();
        }

        private void LoadDataLopHoc()
        {
            // 1. Lấy dữ liệu mới
            DataTable dt = LopHocBUS.GetAllLop();

            // 2. Ngắt kết nối nguồn cũ
            dataGridViewLop.DataSource = null;

            // 3. Gán nguồn mới
            dataGridViewLop.DataSource = dt;

            // 4. Vẽ lại lưới
            dataGridViewLop.Refresh();
        }
        // =====================================================================
        // CÁC SỰ KIỆN NÚT THÊM
        // =====================================================================
        private void btnThemMon_Click(object sender, EventArgs e)
        {
            formThemMon f = new formThemMon();
            if (f.ShowDialog() == DialogResult.OK)
            {
                LoadDataMonHoc();
            }
        }

        private void btnThemLop_Click(object sender, EventArgs e)
        {
            if (dataGridViewLop.CurrentRow == null || dataGridViewLop.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Vui lòng chọn một lớp để thêm lớp tiếp theo, hoặc dùng 'Thêm Khối'.", "Hướng dẫn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string tenLopHienTai = dataGridViewLop.CurrentRow.Cells["colTenLop"].Value.ToString();
            string khoi = "";

            int indexA = tenLopHienTai.IndexOf('A');
            if (indexA > 0) khoi = tenLopHienTai.Substring(0, indexA);
            else khoi = tenLopHienTai.Substring(0, 1);

            ExecuteThemLopTuDong(khoi);
        }

        private void btnThemKhoi_Click(object sender, EventArgs e)
        {
            string khoiMoi = Interaction.InputBox("Nhập tên khối muốn tạo (Chỉ nhập số từ 1 đến 12):", "Thêm Khối Mới", "");
            khoiMoi = khoiMoi.Trim();
            if (string.IsNullOrWhiteSpace(khoiMoi)) return;

            // 1. Kiểm tra 1-12
            int n;
            bool isNumber = int.TryParse(khoiMoi, out n);
            if (!isNumber || n < 1 || n > 12)
            {
                MessageBox.Show("Vui lòng chỉ nhập số nguyên từ 1 đến 12!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Kiểm tra trùng khối (Gọi hàm BUS mới thêm)
            if (LopHocBUS.KiemTraKhoiTonTai(khoiMoi))
            {
                MessageBox.Show($"Khối {khoiMoi} đã tồn tại!\nVui lòng chọn lớp trên lưới và bấm 'Thêm Lớp' để tạo lớp tiếp theo.",
                                "Trùng khối", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            ExecuteThemLopTuDong(khoiMoi);
        }

        private void ExecuteThemLopTuDong(string khoi)
        {
            try
            {
                string tenLopMoi = LopHocBUS.ThemLopTuDong(khoi);
                if (tenLopMoi != null)
                {
                    MessageBox.Show($"Đã thêm thành công lớp: {tenLopMoi}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataLopHoc();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        // =====================================================================
        // XỬ LÝ SỰ KIỆN TRÊN LƯỚI (QUAN TRỌNG: CHỈ GIỮ LẠI 2 HÀM NÀY)
        // =====================================================================

        // 1. XỬ LÝ MÔN HỌC (Dùng CellContentClick)
        private void dataGridViewMon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = (DataGridView)sender;
            if (e.RowIndex < 0) return;

            // Kiểm tra click vào nút/ảnh
            if (grid.Columns[e.ColumnIndex] is DataGridViewImageColumn || grid.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                // XÓA MÔN
                if (grid.Columns[e.ColumnIndex].Name == "colDelete")
                {
                    int maMon = Convert.ToInt32(grid.Rows[e.RowIndex].Cells["colMaMon"].Value);
                    string tenMon = grid.Rows[e.RowIndex].Cells["colTenMon"].Value.ToString();

                    if (MessageBox.Show($"Bạn có chắc chắn muốn xóa môn: {tenMon}?\n(Toàn bộ điểm số và lịch dạy sẽ bị xóa theo!)",
                                        "Cảnh báo quan trọng", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        try
                        {
                            if (MonHocBUS.DeleteMonHoc(maMon))
                            {
                                MessageBox.Show("Đã xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadDataMonHoc();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi: " + ex.Message);
                        }
                    }
                }
            }
        }

        // 2. XỬ LÝ LỚP HỌC (Dùng CellContentClick)
        private void dataGridViewLop_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = (DataGridView)sender;
            if (e.RowIndex < 0) return;

            if (grid.Columns[e.ColumnIndex] is DataGridViewImageColumn || grid.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                int maLop = Convert.ToInt32(grid.Rows[e.RowIndex].Cells["colMaLop"].Value);
                string tenLop = grid.Rows[e.RowIndex].Cells["colTenLop"].Value.ToString();

                // NÚT XÓA LỚP
                if (grid.Columns[e.ColumnIndex].Name == "colLopDelete")
                {
                    if (MessageBox.Show($"Bạn có chắc muốn xóa lớp '{tenLop}'?",
                                        "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        try
                        {
                            // Nếu lớp còn HS -> SQL báo lỗi -> BUS ném lỗi -> Catch bắt được
                            if (LopHocBUS.DeleteLopHoc(maLop))
                            {
                                MessageBox.Show("Xóa lớp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadDataLopHoc();
                            }
                        }
                        catch (Exception ex)
                        {
                            // Hiển thị thông báo chính xác từ SQL
                            MessageBox.Show(ex.Message, "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                    }
                }

                // NÚT SỬA LỚP
                if (grid.Columns[e.ColumnIndex].Name == "colLopEdit")
                {
                    UC_ChiTietLop ucChiTiet = new UC_ChiTietLop();
                    ucChiTiet.SetClassInfo(maLop, tenLop);

                    Panel parentPanel = this.Parent as Panel;
                    if (parentPanel != null)
                    {
                        parentPanel.Controls.Clear();
                        ucChiTiet.Dock = DockStyle.Fill;
                        parentPanel.Controls.Add(ucChiTiet);
                        ucChiTiet.BringToFront();
                    }
                }
            }
        }
    }
}