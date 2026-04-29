using System;
using System.Windows.Forms;
using System.IO;
using System.Data;
using System.Collections.Generic; // Dùng cho List
using ExcelDataReader; // Cần cài gói NuGet ExcelDataReader và ExcelDataReader.DataSet
using BUS; // Namespace chứa LopHocBUS và HocSinhBUS

namespace GUI
{
    public partial class UC_ChiTietLop : UserControl
    {
        private int _maLopHienTai;
        private string _tenLopHienTai;
        private ContextMenuStrip _contextMenu;

        public UC_ChiTietLop()
        {
            InitializeComponent();
            KhoiTaoCauHinhGridVaMenu(); // <--- Gọi hàm khởi tạo menu và cấu hình Grid

        }
        private void KhoiTaoCauHinhGridVaMenu()
        {
            // 1. Cấu hình DataGridView
            dgvHocSinh.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Chọn toàn bộ hàng
            dgvHocSinh.MultiSelect = true; // Cho phép chọn nhiều dòng (giữ Ctrl hoặc kéo chuột)

            // 2. Tạo Menu chuột phải
            _contextMenu = new ContextMenuStrip();
            ToolStripMenuItem itemXoa = new ToolStripMenuItem("Xóa các học sinh đã chọn");

            // Thêm icon thùng rác vào menu (nếu bạn có resource, bỏ comment dòng dưới)
            itemXoa.Image = Properties.Resources.dustbin;

            itemXoa.Click += ItemXoaNhieu_Click; // Gán sự kiện click
            _contextMenu.Items.Add(itemXoa);

            // 3. Gán Menu vào DataGridView
            dgvHocSinh.ContextMenuStrip = _contextMenu;

            // 4. Đăng ký sự kiện MouseDown để xử lý logic chọn dòng khi click chuột phải
            dgvHocSinh.CellMouseDown += dgvHocSinh_CellMouseDown;
        }
        private void dgvHocSinh_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    // Kiểm tra xem dòng được click có nằm trong danh sách đang chọn không
                    bool isSelected = false;
                    foreach (DataGridViewRow row in dgvHocSinh.SelectedRows)
                    {
                        if (row.Index == e.RowIndex)
                        {
                            isSelected = true;
                            break;
                        }
                    }

                    // Nếu click chuột phải vào dòng CHƯA được chọn -> Chọn dòng đó và bỏ chọn các dòng khác
                    // Nếu click chuột phải vào vùng ĐANG được chọn -> Giữ nguyên vùng chọn (để xóa nhiều)
                    if (!isSelected)
                    {
                        dgvHocSinh.ClearSelection();
                        dgvHocSinh.Rows[e.RowIndex].Selected = true;
                    }
                }
            }
        }
        private void ItemXoaNhieu_Click(object sender, EventArgs e)
        {
            if (dgvHocSinh.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một học sinh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int count = dgvHocSinh.SelectedRows.Count;
            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa {count} học sinh đang chọn?", "Xác nhận xóa nhiều", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int success = 0;
                int fail = 0;

                // Duyệt qua danh sách các dòng đã chọn
                foreach (DataGridViewRow row in dgvHocSinh.SelectedRows)
                {
                    if (row.IsNewRow) continue; // Bỏ qua dòng trống cuối cùng (nếu có)

                    try
                    {
                        int stt = Convert.ToInt32(row.Cells["colSTT"].Value ?? row.Cells["STT"].Value);

                        // Gọi BUS xóa
                        if (HocSinhBUS.DeleteHocSinh(stt))
                        {
                            success++;
                        }
                        else
                        {
                            fail++;
                        }
                    }
                    catch
                    {
                        fail++;
                    }
                }

                MessageBox.Show($"Kết quả xóa:\n- Thành công: {success}\n- Thất bại: {fail}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataHocSinh();
            }
        }

        private void btnThemHocSinh_Click(object sender, EventArgs e)
        {
            formThemHocSinh f = new formThemHocSinh(_maLopHienTai);
            if (f.ShowDialog() == DialogResult.OK)
            {
                LoadDataHocSinh();
            }
        }

        public void SetClassInfo(int maLop, string tenLop)
        {
            _maLopHienTai = maLop;
            _tenLopHienTai = tenLop;
            lblTenLop.Text = "DANH SÁCH HỌC SINH LỚP: " + tenLop.ToUpper();

            LoadDataHocSinh();
        }

        private void LoadDataHocSinh()
        {
            try
            {
                LopHocBUS bus = new LopHocBUS();
                dgvHocSinh.DataSource = bus.GetHocSinhByLop(_maLopHienTai);
                FormatGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void FormatGrid()
        {
            // --- CẤU HÌNH CƠ BẢN ---
            dgvHocSinh.RowHeadersVisible = false;
            dgvHocSinh.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Ẩn cột STT/ID và các cột thừa
            string[] colsToHide = { "STT", "colSTT", "MaLop", "TenLop" };
            foreach (string col in colsToHide)
            {
                if (dgvHocSinh.Columns.Contains(col)) dgvHocSinh.Columns[col].Visible = false;
            }

            // Đặt tiêu đề
            if (dgvHocSinh.Columns.Contains("HoTen")) dgvHocSinh.Columns["HoTen"].HeaderText = "HỌ VÀ TÊN";
            if (dgvHocSinh.Columns.Contains("NgaySinh")) dgvHocSinh.Columns["NgaySinh"].HeaderText = "NGÀY SINH";
            if (dgvHocSinh.Columns.Contains("GioiTinh")) dgvHocSinh.Columns["GioiTinh"].HeaderText = "GIỚI TÍNH";
            if (dgvHocSinh.Columns.Contains("DanToc")) dgvHocSinh.Columns["DanToc"].HeaderText = "DÂN TỘC";

            // --- TẠO CỘT SỬA/XÓA NẾU CHƯA CÓ ---
            // Phải tạo cột trước thì đoạn code gán hình bên dưới mới chạy được
            if (!dgvHocSinh.Columns.Contains("colSua"))
            {
                DataGridViewImageColumn imgSua = new DataGridViewImageColumn();
                imgSua.Name = "colSua";
                imgSua.HeaderText = ""; // Không cần tiêu đề
                imgSua.Width = 40;      // Độ rộng nhỏ
                dgvHocSinh.Columns.Add(imgSua);
            }

            if (!dgvHocSinh.Columns.Contains("colXoa"))
            {
                DataGridViewImageColumn imgXoa = new DataGridViewImageColumn();
                imgXoa.Name = "colXoa";
                imgXoa.HeaderText = "";
                imgXoa.Width = 40;
                dgvHocSinh.Columns.Add(imgXoa);
            }

            // ====================================================================
            // --- CẤU HÌNH ICON (THEO STYLE BẠN YÊU CẦU) ---
            // ====================================================================

            // Cột Sửa (Edit)
            if (dgvHocSinh.Columns["colSua"] is DataGridViewImageColumn)
            {
                ((DataGridViewImageColumn)dgvHocSinh.Columns["colSua"]).Image = GUI.Properties.Resources.edit;
                ((DataGridViewImageColumn)dgvHocSinh.Columns["colSua"]).ImageLayout = DataGridViewImageCellLayout.Zoom;
            }

            // Cột Xóa (Delete)
            if (dgvHocSinh.Columns["colXoa"] is DataGridViewImageColumn)
            {
                ((DataGridViewImageColumn)dgvHocSinh.Columns["colXoa"]).Image = GUI.Properties.Resources.dustbin;
                ((DataGridViewImageColumn)dgvHocSinh.Columns["colXoa"]).ImageLayout = DataGridViewImageCellLayout.Zoom;
            }

            // ====================================================================

            // Đẩy 2 cột này về cuối cùng
            if (dgvHocSinh.Columns.Contains("colSua"))
                dgvHocSinh.Columns["colSua"].DisplayIndex = dgvHocSinh.Columns.Count - 2;

            if (dgvHocSinh.Columns.Contains("colXoa"))
                dgvHocSinh.Columns["colXoa"].DisplayIndex = dgvHocSinh.Columns.Count - 1;
        }

        private void dgvHocSinh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvHocSinh.Rows.Count) return;

            int stt = 0;
            string hoTen = "";

            try
            {
                // Lấy thông tin dòng hiện tại
                var row = dgvHocSinh.Rows[e.RowIndex];

                // Lấy STT (xử lý tên cột linh hoạt)
                string colID = dgvHocSinh.Columns.Contains("colSTT") ? "colSTT" : "STT";
                if (dgvHocSinh.Columns.Contains(colID) && row.Cells[colID].Value != null)
                    stt = Convert.ToInt32(row.Cells[colID].Value);

                // Lấy Họ tên
                string colName = dgvHocSinh.Columns.Contains("colHoTen") ? "colHoTen" : "HoTen";
                if (dgvHocSinh.Columns.Contains(colName) && row.Cells[colName].Value != null)
                    hoTen = row.Cells[colName].Value.ToString();
            }
            catch { return; }

            string clickedColName = dgvHocSinh.Columns[e.ColumnIndex].Name;

            // ====================== XÓA ======================
            if (clickedColName == "colXoa")
            {
                if (MessageBox.Show($"Bạn có chắc muốn xóa học sinh: {hoTen}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        if (HocSinhBUS.DeleteHocSinh(stt))
                        {
                            MessageBox.Show("Đã xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDataHocSinh();
                        }
                        else
                        {
                            MessageBox.Show("Xóa thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                }
            }

            // ====================== SỬA ======================
            if (clickedColName == "colSua")
            {
                formSuaHocSinh f = new formSuaHocSinh(stt);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    LoadDataHocSinh();
                }
            }
        }



        private void dgvHocSinh_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void btnNhap_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            openFileDialog.Title = "Chọn file danh sách học sinh";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                try
                {
                    using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            // Cấu hình đọc Excel
                            var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                                {
                                    // LƯU Ý: Nếu file Excel dòng đầu là tên học sinh luôn thì để false
                                    // Nếu dòng đầu là "Họ tên", "Ngày sinh"... thì để true
                                    UseHeaderRow = false
                                }
                            });

                            DataTable dtExcel = result.Tables[0];
                            int successCount = 0;
                            int errorCount = 0;
                            List<string> errorLogs = new List<string>(); // Để lưu tên người bị lỗi

                            foreach (DataRow row in dtExcel.Rows)
                            {
                                string hoTen = "";
                                try
                                {
                                    // 1. Họ Tên
                                    hoTen = row[0].ToString().Trim();
                                    if (string.IsNullOrWhiteSpace(hoTen)) continue;

                                    // 2. Ngày Sinh (Sửa lỗi tại đây)
                                    DateTime ngaySinh = DateTime.Now;
                                    if (row[1] != DBNull.Value)
                                    {
                                        if (row[1] is DateTime)
                                        {
                                            ngaySinh = (DateTime)row[1];
                                        }
                                        else
                                        {
                                            string dateStr = row[1].ToString().Trim();
                                            // Danh sách các định dạng có thể gặp
                                            string[] formats = { "dd/MM/yyyy", "d/M/yyyy", "d/MM/yyyy", "dd/M/yyyy" };

                                            if (!DateTime.TryParseExact(dateStr, formats,
                                                System.Globalization.CultureInfo.InvariantCulture,
                                                System.Globalization.DateTimeStyles.None, out ngaySinh))
                                            {
                                                // Nếu vẫn không được thì thử TryParse thường
                                                DateTime.TryParse(dateStr, out ngaySinh);
                                            }
                                        }
                                    }

                                    // 3. Giới tính
                                    string gioiTinh = row[2] != DBNull.Value ? row[2].ToString().Trim() : "Khác";

                                    // 4. Dân tộc
                                    string danToc = row[3] != DBNull.Value ? row[3].ToString().Trim() : "Kinh";

                                    // Gọi BUS
                                    bool kq = HocSinhBUS.ThemHocSinh(hoTen, ngaySinh, gioiTinh, danToc, _maLopHienTai);
                                    if (kq) successCount++;
                                    else
                                    {
                                        errorCount++;
                                        errorLogs.Add(hoTen + " (Lỗi SQL)");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    errorCount++;
                                    errorLogs.Add(hoTen + " (Lỗi: " + ex.Message + ")");
                                }
                            }

                            string msg = $"Hoàn tất Import!\n- Thành công: {successCount}\n- Thất bại: {errorCount}";
                            if (errorCount > 0)
                            {
                                msg += "\n\nDanh sách lỗi (một số):\n" + string.Join("\n", errorLogs);
                            }

                            MessageBox.Show(msg, "Kết quả", MessageBoxButtons.OK,
                                errorCount > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);

                            LoadDataHocSinh();
                        }
                    }
                }
                catch (IOException)
                {
                    MessageBox.Show("Vui lòng đóng file Excel trước khi Import!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi Import: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
    

    }
}