using BUS;
using DTO;
using GUI.DIEMDANH;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace GUI
{
    public partial class UC_KQHOCTAP : UserControl
    {
        private int maLopHienTai;
        private int maMonHienTai;
        private LopHocBUS bus = new LopHocBUS();
        private DataTable dtHocSinhOriginal;

        // [QUAN TRỌNG] Biến lưu ảnh dùng chung để tránh tràn bộ nhớ
        private Image _imgDiemDanh;

        public UC_KQHOCTAP()
        {
            InitializeComponent();

            // Load ảnh 1 lần duy nhất vào RAM
            try
            {
                _imgDiemDanh = GUI.Properties.Resources.DiemDanh;
            }
            catch { _imgDiemDanh = null; }
        }

        public UC_KQHOCTAP(int maLop, int maMon) : this()
        {
            this.maLopHienTai = maLop;
            this.maMonHienTai = maMon;
        }

        private void UC_KQHOCTAP_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadDanhSachHocSinh();
            CapNhatTieuDe();
            DangKySuKien();
        }

        private void SetupDataGridView()
        {
            dgvDanhSachHS.ReadOnly = false;
            dgvDanhSachHS.AllowUserToAddRows = false;
            dgvDanhSachHS.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDanhSachHS.MultiSelect = false;
            dgvDanhSachHS.RowTemplate.Height = 35;
            dgvDanhSachHS.RowHeadersVisible = false;
        }

        private void CapNhatTieuDe()
        {
            try
            {
                if (this.maLopHienTai > 0 && this.maMonHienTai > 0)
                {
                    string tenLop = bus.GetTenLop(this.maLopHienTai);
                    string tenMon = bus.GetTenMon(this.maMonHienTai);
                    if (lblTieuDe != null)
                        lblTieuDe.Text = $"DANH SÁCH HỌC SINH LỚP {tenLop.ToUpper()} - MÔN {tenMon.ToUpper()}";
                }
            }
            catch { }
        }

        private void DangKySuKien()
        {
            if (txtTimKiem != null) txtTimKiem.TextChanged += TxtTimKiem_TextChanged;

            if (btnNhap != null)
            {
                btnNhap.Click -= BtnNhap_Click;
                btnNhap.Click += BtnNhap_Click;
            }

            if (btnXuat != null)
            {
                btnXuat.Click -= BtnXuat_Click;
                btnXuat.Click += BtnXuat_Click;
            }

            dgvDanhSachHS.CurrentCellDirtyStateChanged += DgvDanhSachHS_CurrentCellDirtyStateChanged;
            dgvDanhSachHS.CellValueChanged += DgvDanhSachHS_CellValueChanged;
            dgvDanhSachHS.DataBindingComplete += DgvDanhSachHS_DataBindingComplete;
            dgvDanhSachHS.CellFormatting += DgvDanhSachHS_CellFormatting;
            dgvDanhSachHS.CellContentClick += DgvDanhSachHS_CellContentClick;

        }
        private void DgvDanhSachHS_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // 1. Kiểm tra dòng hợp lệ (không bấm vào header)
            if (e.RowIndex < 0) return;

            // 2. Kiểm tra xem có bấm đúng vào cột hình ảnh "colDiemDanh" không
            // (Lưu ý: "colDiemDanh" là tên cột bạn đặt lúc tạo cột ảnh trong code LoadDanhSachHocSinh)
            if (dgvDanhSachHS.Columns[e.ColumnIndex].Name == "colDiemDanh")
            {
                try
                {
                    // 3. Lấy dữ liệu của dòng đó
                    DataGridViewRow row = dgvDanhSachHS.Rows[e.RowIndex];

                    // Lấy STT và Tên HS (Cột STT và HoTen phải tồn tại trong GridView)
                    int sttHS = Convert.ToInt32(row.Cells["STT"].Value);
                    string tenHS = row.Cells["HoTen"].Value.ToString();

                    // 4. KHỞI TẠO VÀ GỌI FORM CHI TIẾT (Lúc này "0 references" sẽ biến mất)
                    FRM_CHITIET_DIEMDANH frm = new FRM_CHITIET_DIEMDANH(sttHS, tenHS);
                    frm.ShowDialog(); // Hiện form lên
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi mở lịch sử: " + ex.Message);
                }
            }
        }

        public void LoadDanhSachHocSinh()
        {
            try
            {
                dgvDanhSachHS.CellValueChanged -= DgvDanhSachHS_CellValueChanged;

                dtHocSinhOriginal = bus.GetChiTietHocSinhByLop(this.maLopHienTai, this.maMonHienTai);

                dgvDanhSachHS.AutoGenerateColumns = false;
                dgvDanhSachHS.Columns.Clear();
                dgvDanhSachHS.DataSource = dtHocSinhOriginal;

                // --- CẤU HÌNH CỘT ---
                AddColumn("STT", "STT", 40);
                AddColumn("HoTen", "Họ và Tên", 150);
                AddColumn("GK1", "GK1", 50);
                AddColumn("CK1", "CK1", 50);

                dgvDanhSachHS.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "DiemHK1",
                    HeaderText = "TB HK1",
                    Width = 60,
                    ReadOnly = true,
                    DefaultCellStyle = new DataGridViewCellStyle { Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.Blue, Alignment = DataGridViewContentAlignment.MiddleCenter }
                });

                AddColumn("GK2", "GK2", 50);
                AddColumn("CK2", "CK2", 50);

                dgvDanhSachHS.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "DiemHK2",
                    HeaderText = "TB HK2",
                    Width = 60,
                    ReadOnly = true,
                    DefaultCellStyle = new DataGridViewCellStyle { Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.Blue, Alignment = DataGridViewContentAlignment.MiddleCenter }
                });

                dgvDanhSachHS.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "DiemTB",
                    HeaderText = "ĐTB Năm",
                    Width = 70,
                    ReadOnly = true,
                    DefaultCellStyle = new DataGridViewCellStyle { Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.Red, Alignment = DataGridViewContentAlignment.MiddleCenter }
                });

                dgvDanhSachHS.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "XepLoai",
                    HeaderText = "Xếp Loại",
                    Width = 80,
                    ReadOnly = true,
                    DefaultCellStyle = new DataGridViewCellStyle { Font = new Font("Segoe UI", 9, FontStyle.Bold), Alignment = DataGridViewContentAlignment.MiddleCenter }
                });

                DataGridViewCheckBoxColumn colTuyenDuong = new DataGridViewCheckBoxColumn
                {
                    DataPropertyName = "TuyenDuong",
                    HeaderText = "Tuyên Dương",
                    Width = 80,
                    TrueValue = true,
                    FalseValue = false
                };
                dgvDanhSachHS.Columns.Add(colTuyenDuong);
                AddColumn("Notes", "Ghi chú", 100);

                dgvDanhSachHS.Columns.Add(new DataGridViewImageColumn
                {
                    Name = "colDiemDanh",
                    HeaderText = "Đ.Danh",
                    Width = 60,
                    ImageLayout = DataGridViewImageCellLayout.Zoom,
                    ReadOnly = true
                });

                dgvDanhSachHS.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvDanhSachHS.CellValueChanged += DgvDanhSachHS_CellValueChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách: " + ex.Message);
            }
        }

        private void AddColumn(string dataName, string header, int width)
        {
            dgvDanhSachHS.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = dataName,             // <--- QUAN TRỌNG: Phải có dòng này mới gọi row.Cells["STT"] được
                DataPropertyName = dataName,
                HeaderText = header,
                Width = width,
                ReadOnly = true
            });
        }

        private void DgvDanhSachHS_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvDanhSachHS.Columns[e.ColumnIndex].DataPropertyName == "XepLoai" && e.Value != null)
            {
                string xepLoai = e.Value.ToString();
                switch (xepLoai)
                {
                    case "Giỏi": e.CellStyle.ForeColor = Color.Green; break;
                    case "Khá": e.CellStyle.ForeColor = Color.Blue; break;
                    case "Trung Bình": e.CellStyle.ForeColor = Color.Orange; break;
                    case "Yếu": case "Kém": e.CellStyle.ForeColor = Color.Red; break;
                }
            }
        }

        private void BtnNhap_Click(object sender, EventArgs e)
        {
            if (dgvDanhSachHS.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một học sinh!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            MoFormNhapDiem(dgvDanhSachHS.SelectedRows[0]);
        }

        private void MoFormNhapDiem(DataGridViewRow row)
        {
            if (this.maMonHienTai <= 0) return;

            int sttCanChon = Convert.ToInt32(row.Cells[0].Value);
            string ten = row.Cells[1].Value.ToString();
            string gk1 = GetSafeValue(row, "GK1");
            string ck1 = GetSafeValue(row, "CK1");
            string gk2 = GetSafeValue(row, "GK2");
            string ck2 = GetSafeValue(row, "CK2");

            FRM_NHAPDIEM frm = new FRM_NHAPDIEM(sttCanChon, this.maMonHienTai, ten, gk1, ck1, gk2, ck2);
            frm.ShowDialog();

            int scrollIndex = dgvDanhSachHS.FirstDisplayedScrollingRowIndex;
            LoadDanhSachHocSinh();
            ChonLaiDongTheoSTT(sttCanChon);

            if (scrollIndex >= 0 && scrollIndex < dgvDanhSachHS.Rows.Count)
                dgvDanhSachHS.FirstDisplayedScrollingRowIndex = scrollIndex;
        }
        private void ChonLaiDongTheoSTT(int stt)
        {
            dgvDanhSachHS.ClearSelection();
            foreach (DataGridViewRow row in dgvDanhSachHS.Rows)
            {
                if (row.Cells[0].Value != null && Convert.ToInt32(row.Cells[0].Value) == stt)
                {
                    row.Selected = true;
                    if (row.Cells[1].Visible) dgvDanhSachHS.CurrentCell = row.Cells[1];
                    return;
                }
            }
        }

        private string GetSafeValue(DataGridViewRow row, string colName)
        {
            foreach (DataGridViewCell cell in row.Cells)
                if (cell.OwningColumn.DataPropertyName == colName)
                    return cell.Value?.ToString() ?? "";
            return "";
        }

        private void DgvDanhSachHS_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dgvDanhSachHS.Columns[e.ColumnIndex].DataPropertyName == "TuyenDuong")
            {
                try
                {
                    DataGridViewRow row = dgvDanhSachHS.Rows[e.RowIndex];
                    int stt = Convert.ToInt32(row.Cells[0].Value);
                    object val = row.Cells[e.ColumnIndex].Value;
                    bool isChecked = (val != null && val != DBNull.Value && (bool)val == true);
                    if (this.maMonHienTai > 0)
                        bus.UpdateTuyenDuong(stt, this.maMonHienTai, isChecked);
                }
                catch { }
            }
        }

        private void DgvDanhSachHS_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvDanhSachHS.IsCurrentCellDirty)
                dgvDanhSachHS.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void TxtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (dtHocSinhOriginal == null) return;
            string filter = string.Format("HoTen LIKE '%{0}%'", (sender as Control).Text.Trim());
            dtHocSinhOriginal.DefaultView.RowFilter = filter;
        }

        private void DgvDanhSachHS_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (_imgDiemDanh == null) return;
            foreach (DataGridViewRow row in dgvDanhSachHS.Rows)
            {
                if (row.IsNewRow) continue;
                var cell = row.Cells["colDiemDanh"];
                if (cell != null) cell.Value = _imgDiemDanh;
            }
        }

        // ====================================================================================
        // PHẦN XUẤT EXCEL NÂNG CAO - ĐÃ ĐƯỢC LÀM ĐẸP
        // ====================================================================================

        // 1. Hàm làm sạch tên file (Xóa ký tự lạ)
        private string CleanFileName(string name)
        {
            // Thay thế các ký tự không được phép trong tên file bằng dấu _
            foreach (char c in System.IO.Path.GetInvalidFileNameChars())
            {
                name = name.Replace(c, '_');
            }
            // Xóa khoảng trắng luôn cho gọn (tùy chọn)
            return name.Replace(" ", "");
        }

        // 2. Hàm kiểm tra file có đang bị khóa (đang mở) không
        private bool IsFileLocked(string filePath)
        {
            // Nếu file chưa tồn tại thì chắc chắn không bị khóa
            if (!System.IO.File.Exists(filePath)) return false;

            try
            {
                // Cố gắng mở file ở chế độ độc quyền
                using (System.IO.FileStream stream = System.IO.File.Open(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (System.IO.IOException)
            {
                // Nếu lỗi IOException nghĩa là file đang bị dùng bởi chương trình khác
                return true;
            }
            return false;
        }
        private void BtnXuat_Click(object sender, EventArgs e)
        {
            if (dgvDanhSachHS.Rows.Count == 0) return;

            // 1. LẤY THÔNG TIN
            string tenLopShow = "Lop_Unknown";
            string tenMonShow = "Mon_Unknown";

            // --- [QUAN TRỌNG] LẤY TÊN GIÁO VIÊN TỪ SESSION ---
            string tenGiaoVien = DTO.AuthSession.CurrentTenGV;

            // Kiểm tra nếu chưa có tên (ví dụ chạy debug thẳng mà ko qua login)
            if (string.IsNullOrEmpty(tenGiaoVien))
            {
                tenGiaoVien = "Giáo Viên Bộ Môn"; // Tên mặc định
            }
            // -------------------------------------------------

            try
            {
                if (maLopHienTai > 0) tenLopShow = bus.GetTenLop(maLopHienTai);
                if (maMonHienTai > 0) tenMonShow = bus.GetTenMon(maMonHienTai);
            }
            catch { }

            // 2. TẠO TÊN FILE
            string fileLop = CleanFileName(tenLopShow);
            string fileMon = CleanFileName(tenMonShow);
            string defaultFileName = $"KetQua_{fileLop}_{fileMon}_{DateTime.Now:ddMMyyyy_HHmm}";

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files (*.xlsx)|*.xlsx|PDF Files (*.pdf)|*.pdf";
            sfd.FileName = defaultFileName;
            sfd.Title = "Xuất bảng điểm học sinh";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if (IsFileLocked(sfd.FileName))
                {
                    MessageBox.Show("File này đang mở. Vui lòng đóng lại trước!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Truyền tên giáo viên lấy được vào hàm xuất
                XuatFileKetQuaPro(sfd.FileName, tenLopShow, tenMonShow, tenGiaoVien);
            }
        }

        private void XuatFileKetQuaPro(string filePath, string tenLop, string tenMon, string tenGiaoVien)
        {
            Excel.Application excelApp = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet sheetBangDiem = null;

            bool isPdf = (System.IO.Path.GetExtension(filePath).ToLower() == ".pdf");

            try
            {
                excelApp = new Excel.Application();
                excelApp.Visible = false;
                excelApp.DisplayAlerts = false;

                workbook = excelApp.Workbooks.Add(Type.Missing);
                sheetBangDiem = (Excel.Worksheet)workbook.Sheets[1];

                // Tiêu đề hiển thị (Sẽ không bị dính chữ nữa vì dùng biến tenLop, tenMon gốc)
                string title = $"BẢNG ĐIỂM LỚP {tenLop.ToUpper()} - MÔN {tenMon.ToUpper()}";

                FormatSheetHocSinh(sheetBangDiem, dgvDanhSachHS, "BangDiem", title);

                if (isPdf)
                {
                    // Truyền tên giáo viên vào để setup Footer
                    SetupPageLayoutHS(excelApp, sheetBangDiem, title, tenGiaoVien);

                    workbook.ExportAsFixedFormat(
                        Excel.XlFixedFormatType.xlTypePDF,
                        filePath,
                        Excel.XlFixedFormatQuality.xlQualityStandard,
                        true, false, Type.Missing, Type.Missing, true
                    );
                }
                else
                {
                    workbook.SaveAs(filePath);
                    MessageBox.Show("Xuất file Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất file: " + ex.Message);
            }
            finally
            {
                if (sheetBangDiem != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(sheetBangDiem);
                if (workbook != null) { workbook.Close(false); System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook); }
                if (excelApp != null) { excelApp.Quit(); System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp); }
                GC.Collect();
            }
        }

        // --- HÀM CẤU HÌNH TRANG IN (HEADER/FOOTER) ---
        private void SetupPageLayoutHS(Excel.Application app, Excel.Worksheet sheet, string headerTitle, string tenGV)
        {
            try
            {
                Excel.PageSetup ps = sheet.PageSetup;
                ps.Orientation = Excel.XlPageOrientation.xlLandscape;
                ps.PaperSize = Excel.XlPaperSize.xlPaperA4;
                ps.Zoom = false;
                ps.FitToPagesWide = 1;
                ps.FitToPagesTall = false;

                ps.TopMargin = app.InchesToPoints(0.75);
                ps.BottomMargin = app.InchesToPoints(0.75);
                ps.LeftMargin = app.InchesToPoints(0.5);
                ps.RightMargin = app.InchesToPoints(0.25);
                ps.CenterHorizontally = true;

                // Header
                ps.LeftHeader = "&\"Times New Roman,Bold\"TRƯỜNG THPT ABC";
                ps.CenterHeader = $"&\"Times New Roman,Bold\"&12 {headerTitle}";
                ps.RightHeader = "&\"Times New Roman,Italic\"Ngày in: &D";

                // Footer (Đã sửa để hiện tên giáo viên)
                ps.LeftFooter = $"&\"Times New Roman,Regular\"Người lập bảng: {tenGV}";
                ps.CenterFooter = "&\"Times New Roman,Regular\"Trang &P / &N";

                ps.PrintGridlines = false;
            }
            catch { }
        }

        // --- HÀM FORMAT GIỮ NGUYÊN (Chỉ copy lại để đảm bảo đầy đủ) ---
        private void FormatSheetHocSinh(Excel.Worksheet sheet, DataGridView dgv, string sheetName, string titleText)
        {
            sheet.Name = sheetName;
            int totalCols = 0;
            foreach (DataGridViewColumn col in dgv.Columns)
                if (col.Visible && !(col is DataGridViewImageColumn)) totalCols++;

            if (totalCols == 0) return;

            // Tiêu đề lớn
            Excel.Range titleRange = sheet.Range[sheet.Cells[1, 1], sheet.Cells[1, totalCols]];
            titleRange.Merge();
            titleRange.Value = titleText;
            titleRange.Font.Name = "Times New Roman";
            titleRange.Font.Size = 16;
            titleRange.Font.Bold = true;
            titleRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            titleRange.RowHeight = 40;

            // Header cột
            int headerRow = 3;
            int excelCol = 1;
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                if (!dgv.Columns[i].Visible || dgv.Columns[i] is DataGridViewImageColumn) continue;
                Excel.Range cell = sheet.Cells[headerRow, excelCol];
                cell.Value = dgv.Columns[i].HeaderText.ToUpper();
                cell.Font.Bold = true;
                cell.Font.Name = "Times New Roman";
                cell.Font.Size = 10;
                cell.Font.Color = System.Drawing.Color.Black;
                cell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
                cell.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                cell.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                cell.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                cell.WrapText = true;
                excelCol++;
            }

            // Dữ liệu
            if (dgv.Rows.Count > 0)
            {
                object[,] arrData = new object[dgv.Rows.Count, totalCols];
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    int c = 0;
                    for (int j = 0; j < dgv.Columns.Count; j++)
                    {
                        if (!dgv.Columns[j].Visible || dgv.Columns[j] is DataGridViewImageColumn) continue;
                        object val = dgv.Rows[i].Cells[j].Value;
                        string colName = dgv.Columns[j].DataPropertyName;

                        if (colName == "TuyenDuong") arrData[i, c] = (val != null && (bool)val) ? "x" : "";
                        else arrData[i, c] = "'" + (val?.ToString() ?? "");
                        c++;
                    }
                }

                int startRow = headerRow + 1;
                int endRow = startRow + dgv.Rows.Count - 1;
                Excel.Range dataRange = sheet.Range[sheet.Cells[startRow, 1], sheet.Cells[endRow, totalCols]];
                dataRange.Value = arrData;

                Excel.Range tableRange = sheet.Range[sheet.Cells[headerRow, 1], sheet.Cells[endRow, totalCols]];
                tableRange.Font.Name = "Times New Roman";
                tableRange.Font.Size = 11;
                tableRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                tableRange.Borders.Weight = Excel.XlBorderWeight.xlThin;

                dataRange.RowHeight = 20;
                dataRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                dataRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                int currentVisCol = 0;
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    if (!col.Visible || col is DataGridViewImageColumn) continue;
                    currentVisCol++;
                    if (col.DataPropertyName == "HoTen" || col.DataPropertyName == "Notes")
                    {
                        sheet.Range[sheet.Cells[startRow, currentVisCol], sheet.Cells[endRow, currentVisCol]].HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                        sheet.Range[sheet.Cells[startRow, currentVisCol], sheet.Cells[endRow, currentVisCol]].IndentLevel = 1;
                    }
                }
            }

            // Fix lỗi co cột
            sheet.Columns.AutoFit();
            for (int i = 1; i <= totalCols; i++)
            {
                Excel.Range col = (Excel.Range)sheet.Columns[i];
                double currentWidth = (double)col.ColumnWidth;
                if (currentWidth < 10) col.ColumnWidth = 10;
                string headerText = ((Excel.Range)sheet.Cells[headerRow, i]).Value.ToString().ToUpper();
                if (headerText.Contains("TÊN") || headerText.Contains("GHI CHÚ"))
                {
                    if (currentWidth < 30) col.ColumnWidth = 30;
                }
            }
            sheet.PageSetup.PrintTitleRows = string.Format("${0}:${0}", headerRow);
        }

        // --- CÁC HÀM RỖNG TRÁNH LỖI DESIGNER ---
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void btnGhiChuNhanh_Click(object sender, EventArgs e)
        {
            FRM_QUANLY_NOTE frm = new FRM_QUANLY_NOTE();
            frm.ShowDialog();
            LoadDanhSachHocSinh();
        }
    }
}