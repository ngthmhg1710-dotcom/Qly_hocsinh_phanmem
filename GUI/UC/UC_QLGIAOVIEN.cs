using BUS;
using DTO;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
// Thư viện Excel
using Excel = Microsoft.Office.Interop.Excel;

namespace GUI
{
    public partial class UC_QLGIAOVIEN : UserControl
    {
        // ==========================================================
        // KHAI BÁO BIẾN TOÀN CỤC
        // ==========================================================
        private int selectedID_GV = -1;       // Lưu ID giáo viên đang chọn
        private string selectedTenGV = "";    // Lưu tên để hiển thị thông báo
        private string currentEmail = "";     // Lưu Email cũ để kiểm tra trùng khi sửa
        private bool isLocked = false;        // Trạng thái tài khoản

        public UC_QLGIAOVIEN()
        {
            InitializeComponent();
        }

        // ==========================================================
        // 1. HÀM LOAD FORM & CẤU HÌNH GIAO DIỆN
        // ==========================================================
        private void UC_QLGIAOVIEN_Load(object sender, EventArgs e)
        {
            SetupDataGridView(dgvGiangDay);
            SetupDataGridView(dgvCaNhan);

            // Cài đặt mặc định
            if (cboGioiTinh.Items.Count > 0) cboGioiTinh.SelectedIndex = 0;
            dtpNgaySinh.Value = DateTime.Now;

            // Load dữ liệu
            LoadDanhSachGiaoVien();

            // Cấu hình trạng thái nút ban đầu (Chế độ Thêm)
            ResetFormState();
        }

        private void SetupDataGridView(Guna.UI2.WinForms.Guna2DataGridView dgv)
        {
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.MultiSelect = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.RowHeadersVisible = false;
            dgv.ColumnHeadersHeight = 40;
            dgv.RowTemplate.Height = 35;
        }

        private void LoadDanhSachGiaoVien()
        {
            try
            {
                DataTable dt = GiaoVienBUS.LayDanhSachGiaoVien();
                dgvGiangDay.DataSource = dt;
                dgvCaNhan.DataSource = dt;

                CaiDatHienThiTabGiangDay();
                CaiDatHienThiTabCaNhan();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        // Cấu hình cột hiển thị cho Tab Giảng Dạy
        private void CaiDatHienThiTabGiangDay()
        {
            if (dgvGiangDay.Columns.Contains("HoTen")) dgvGiangDay.Columns["HoTen"].HeaderText = "Họ và Tên";
            if (dgvGiangDay.Columns.Contains("Email_GV")) dgvGiangDay.Columns["Email_GV"].HeaderText = "Email";

            // Ẩn các cột không cần thiết
            string[] colsToHide = { "ID_GV", "Password_GV", "NgaySinh", "GioiTinh", "SoDienThoai", "Mon", "BiKhoa" };
            foreach (string col in colsToHide)
            {
                if (dgvGiangDay.Columns.Contains(col)) dgvGiangDay.Columns[col].Visible = false;
            }

            if (dgvGiangDay.Columns.Contains("BiKhoa"))
            {
                dgvGiangDay.Columns["BiKhoa"].Visible = true;
                dgvGiangDay.Columns["BiKhoa"].HeaderText = "Trạng Thái";
            }
        }

        // Cấu hình cột hiển thị cho Tab Cá Nhân
        private void CaiDatHienThiTabCaNhan()
        {
            if (dgvCaNhan.Columns.Contains("HoTen")) dgvCaNhan.Columns["HoTen"].HeaderText = "Họ và Tên";
            if (dgvCaNhan.Columns.Contains("NgaySinh")) dgvCaNhan.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
            if (dgvCaNhan.Columns.Contains("GioiTinh")) dgvCaNhan.Columns["GioiTinh"].HeaderText = "Giới Tính";
            if (dgvCaNhan.Columns.Contains("Email_GV")) dgvCaNhan.Columns["Email_GV"].HeaderText = "Email";
            if (dgvCaNhan.Columns.Contains("SoDienThoai")) dgvCaNhan.Columns["SoDienThoai"].HeaderText = "Số Điện Thoại";

            string[] colsToHide = { "ID_GV", "Password_GV", "ChiTietPhanCong", "Mon", "BiKhoa" };
            foreach (string col in colsToHide)
            {
                if (dgvCaNhan.Columns.Contains(col)) dgvCaNhan.Columns[col].Visible = false;
            }
        }

        // ==========================================================
        // 2. XỬ LÝ SỰ KIỆN CLICK VÀO BẢNG (ĐỔ DỮ LIỆU)
        // ==========================================================
        private void dgvGiangDay_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DoDuLieuVaoTextBox(dgvGiangDay);
        }

        private void dgvCaNhan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DoDuLieuVaoTextBox(dgvCaNhan);
        }

        private void DoDuLieuVaoTextBox(DataGridView dgv)
        {
            if (dgv.CurrentRow != null && dgv.CurrentRow.Index >= 0)
            {
                try
                {
                    // Lấy ID và Tên
                    if (dgv.CurrentRow.Cells["ID_GV"].Value != DBNull.Value)
                        selectedID_GV = Convert.ToInt32(dgv.CurrentRow.Cells["ID_GV"].Value);

                    selectedTenGV = dgv.CurrentRow.Cells["HoTen"].Value.ToString();
                    txtHoTenGV.Text = selectedTenGV;

                    // Lấy Email
                    if (dgv.Columns.Contains("Email_GV"))
                    {
                        string emailVal = dgv.CurrentRow.Cells["Email_GV"].Value?.ToString();
                        txtEmailGV.Text = emailVal;
                        currentEmail = emailVal; // Lưu lại để check trùng khi update
                    }

                    // Lấy Trạng thái khóa
                    if (dgv.Columns.Contains("BiKhoa") && dgv.CurrentRow.Cells["BiKhoa"].Value != DBNull.Value)
                        isLocked = Convert.ToBoolean(dgv.CurrentRow.Cells["BiKhoa"].Value);
                    else
                        isLocked = false;

                    // Lấy Ngày sinh
                    if (dgv.Columns.Contains("NgaySinh") && dgv.CurrentRow.Cells["NgaySinh"].Value != DBNull.Value)
                        dtpNgaySinh.Value = Convert.ToDateTime(dgv.CurrentRow.Cells["NgaySinh"].Value);
                    else
                        dtpNgaySinh.Value = DateTime.Now;

                    // Lấy Giới tính
                    if (dgv.Columns.Contains("GioiTinh") && dgv.CurrentRow.Cells["GioiTinh"].Value != DBNull.Value)
                        cboGioiTinh.Text = dgv.CurrentRow.Cells["GioiTinh"].Value.ToString();

                    // Lấy SĐT
                    if (dgv.Columns.Contains("SoDienThoai"))
                        txtSoDienThoai.Text = dgv.CurrentRow.Cells["SoDienThoai"].Value?.ToString();

                    // Reset ô mật khẩu (Không hiển thị mật khẩu cũ vì lý do bảo mật)
                    txtMatKhauGV.Text = "";

                    // --- CHUYỂN CHẾ ĐỘ SANG: SỬA ---
                    SetEditMode();
                }
                catch (Exception ex)
                {
                    // Silent fail or log
                }
            }
        }

        // ==========================================================
        // 3. CÁC HÀM XỬ LÝ LOGIC NÚT BẤM
        // ==========================================================

        // --- NÚT THÊM ---
        private void btnThemTTGV_Click(object sender, EventArgs e)
        {
            // Kiểm tra: Nếu đang ở chế độ sửa thì không cho thêm (hoặc báo lỗi)
            if (selectedID_GV != -1)
            {
                MessageBox.Show("Vui lòng nhấn nút 'Làm mới' trước khi thêm giáo viên mới!", "Sai thao tác", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string hoTen = txtHoTenGV.Text.Trim();
            string email = txtEmailGV.Text.Trim();
            string matKhau = txtMatKhauGV.Text.Trim();
            string sdt = txtSoDienThoai.Text.Trim();
            DateTime ngaySinh = dtpNgaySinh.Value;
            string gioiTinh = cboGioiTinh.Text;

            // Validate
            if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đủ: Họ tên, Email và Mật khẩu!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            GiaoVienDTO gv = new GiaoVienDTO
            {
                HoTen = hoTen,
                Email_GV = email,
                Password_GV = matKhau,
                SoDienThoai = sdt,
                NgaySinh = ngaySinh,
                GioiTinh = gioiTinh
            };

            try
            {
                if (GiaoVienBUS.ThemGiaoVien(gv))
                {
                    MessageBox.Show("Thêm giáo viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachGiaoVien();
                    ResetFormState();
                }
                else
                {
                    MessageBox.Show("Email này đã tồn tại trong hệ thống!", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        // --- NÚT SỬA (Bạn cần thêm nút này vào Designer và link sự kiện) ---
        private void btnSuaTTGV_Click(object sender, EventArgs e)
        {
            if (selectedID_GV == -1)
            {
                MessageBox.Show("Vui lòng chọn giáo viên cần sửa từ danh sách!", "Chưa chọn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string hoTen = txtHoTenGV.Text.Trim();
            string email = txtEmailGV.Text.Trim();
            string sdt = txtSoDienThoai.Text.Trim();
            DateTime ngaySinh = dtpNgaySinh.Value;
            string gioiTinh = cboGioiTinh.Text;

            if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Họ tên và Email không được để trống!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo DTO cập nhật
            GiaoVienDTO gv = new GiaoVienDTO
            {
                ID_GV = selectedID_GV,
                HoTen = hoTen,
                Email_GV = email,
                SoDienThoai = sdt,
                NgaySinh = ngaySinh,
                GioiTinh = gioiTinh
            };

            // Gọi BUS
            string ketQua = GiaoVienBUS.SuaGiaoVienFull(gv);

            if (ketQua == "DuplicateEmail")
            {
                MessageBox.Show("Email này đã thuộc về giáo viên khác!", "Trùng lặp", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmailGV.Focus();
            }
            else if (ketQua == "Success")
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSachGiaoVien();
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại! Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- NÚT LÀM MỚI / RESET (Bạn cần thêm nút này vào Designer) ---
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ResetFormState();
        }

        // --- NÚT KHÓA / MỞ TÀI KHOẢN ---
        private void btnKhoaMo_Click(object sender, EventArgs e)
        {
            if (selectedID_GV == -1) return;

            bool trangThaiMoi = !isLocked;
            string actionName = trangThaiMoi ? "KHÓA" : "MỞ KHÓA";

            if (MessageBox.Show($"Bạn có chắc chắn muốn {actionName} tài khoản của {selectedTenGV}?",
                                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (GiaoVienBUS.KhoaMoTaiKhoan(selectedID_GV, trangThaiMoi))
                {
                    MessageBox.Show($"Đã {actionName} thành công!");
                    LoadDanhSachGiaoVien();
                    ResetFormState();
                }
                else
                {
                    MessageBox.Show("Thao tác thất bại!");
                }
            }
        }

        // ==========================================================
        // 4. CÁC HÀM HỖ TRỢ TRẠNG THÁI (UI LOGIC)
        // ==========================================================

        // Đưa form về trạng thái chuẩn bị Thêm Mới
        private void ResetFormState()
        {
            // Xóa trắng input
            txtHoTenGV.Text = "";
            txtEmailGV.Text = "";
            txtMatKhauGV.Text = "";
            txtSoDienThoai.Text = "";
            dtpNgaySinh.Value = DateTime.Now;
            if (cboGioiTinh.Items.Count > 0) cboGioiTinh.SelectedIndex = 0;

            // Reset biến
            selectedID_GV = -1;
            selectedTenGV = "";
            currentEmail = "";
            isLocked = false;

            // Cấu hình nút
            btnThemTTGV.Enabled = true;     // Cho phép thêm

            // Nếu bạn đã thêm btnSuaTTGV vào Designer, hãy bỏ comment dòng dưới
            // btnSuaTTGV.Enabled = false;  // Không cho sửa khi chưa chọn

            //btnKhoaMo.Enabled = false;      // Không cho khóa khi chưa chọn
            //btnKhoaMo.Text = "KHÓA TÀI KHOẢN";
            //btnKhoaMo.FillColor = Color.Firebrick;

            txtHoTenGV.Focus();
        }

        // Đưa form về trạng thái Đang Sửa
        private void SetEditMode()
        {
            btnThemTTGV.Enabled = false;    // Tắt nút thêm để tránh lỗi logic

            // Nếu bạn đã thêm btnSuaTTGV vào Designer, hãy bỏ comment dòng dưới
            // btnSuaTTGV.Enabled = true;   // Bật nút sửa

            //btnKhoaMo.Enabled = true;
            //UpdateNutKhoaUI();
        }

        //private void UpdateNutKhoaUI()
        //{
        //    if (isLocked)
        //    {
        //        btnKhoaMo.Text = "MỞ KHÓA";
        //        btnKhoaMo.FillColor = Color.Green;
        //        btnKhoaMo.FillColor2 = Color.LimeGreen;
        //    }
        //    else
        //    {
        //        btnKhoaMo.Text = "KHÓA TÀI KHOẢN";
        //        btnKhoaMo.FillColor = Color.Firebrick;
        //        btnKhoaMo.FillColor2 = Color.Red;
        //    }
        //}

        // ==========================================================
        // 5. CÁC CHỨC NĂNG PHỤ TRỢ (TÌM KIẾM, XUẤT EXCEL, PHÂN CÔNG)
        // ==========================================================

        private void txtTimGV_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtTimGV.Text.Trim();
                DataTable dt = dgvGiangDay.DataSource as DataTable;
                if (dt != null)
                {
                    string filter = string.Format("HoTen LIKE '%{0}%' OR Email_GV LIKE '%{0}%' OR SoDienThoai LIKE '%{0}%'",
                                                  keyword.Replace("'", "''"));
                    dt.DefaultView.RowFilter = filter;
                }
            }
            catch { }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            if (selectedID_GV == -1)
            {
                MessageBox.Show("Vui lòng chọn giáo viên cần phân công!", "Lưu ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            frmPhanCongGiangDay frm = new frmPhanCongGiangDay(selectedID_GV, selectedTenGV);
            frm.ShowDialog();
            LoadDanhSachGiaoVien();
        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            if (dgvGiangDay.Rows.Count == 0) return;
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Excel Files (*.xlsx)|*.xlsx",
                FileName = $"DanhSachGiaoVien_{DateTime.Now:ddMMyyyy_HHmm}"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                XuatFileChuyenNghiep(sfd.FileName);
            }
        }

        private void XuatFileChuyenNghiep(string filePath)
        {
            Excel.Application excelApp = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet sheetGiangDay = null;
            Excel.Worksheet sheetCaNhan = null;

            try
            {
                excelApp = new Excel.Application();
                workbook = excelApp.Workbooks.Add(Type.Missing);

                // Sheet 1: Giảng dạy
                sheetGiangDay = (Excel.Worksheet)workbook.Sheets[1];
                FormatSheetPro(sheetGiangDay, dgvGiangDay, "Phân Công", "DANH SÁCH GIÁO VIÊN");

                // Sheet 2: Cá nhân
                sheetCaNhan = (Excel.Worksheet)workbook.Worksheets.Add(After: workbook.Sheets[workbook.Sheets.Count]);
                FormatSheetPro(sheetCaNhan, dgvCaNhan, "Chi Tiết Cá Nhân", "HỒ SƠ GIÁO VIÊN");

                workbook.SaveAs(filePath);
                MessageBox.Show("Xuất Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất Excel: " + ex.Message);
            }
            finally
            {
                // Dọn dẹp bộ nhớ COM
                if (sheetGiangDay != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(sheetGiangDay);
                if (sheetCaNhan != null) System.Runtime.InteropServices.Marshal.ReleaseComObject(sheetCaNhan);
                if (workbook != null) { workbook.Close(false); System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook); }
                if (excelApp != null) { excelApp.Quit(); System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp); }
                GC.Collect();
            }
        }

        private void FormatSheetPro(Excel.Worksheet sheet, DataGridView dgv, string sheetName, string titleText)
        {
            sheet.Name = sheetName;

            // Header
            int colCount = 0;
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                if (dgv.Columns[i].Visible)
                {
                    colCount++;
                    sheet.Cells[3, colCount] = dgv.Columns[i].HeaderText;
                    // Format Header
                    Excel.Range cell = sheet.Cells[3, colCount];
                    cell.Font.Bold = true;
                    cell.Interior.Color = System.Drawing.Color.LightGray;
                    cell.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                }
            }

            // Data
            int rowIdx = 4;
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                int colIdx = 1;
                for (int j = 0; j < dgv.Columns.Count; j++)
                {
                    if (dgv.Columns[j].Visible)
                    {
                        object val = dgv.Rows[i].Cells[j].Value;
                        // Thêm dấu ' để tránh Excel tự format ngày tháng/số sai
                        sheet.Cells[rowIdx, colIdx] = "'" + (val?.ToString() ?? "");
                        colIdx++;
                    }
                }
                rowIdx++;
            }

            // Title
            Excel.Range titleRange = sheet.Range[sheet.Cells[1, 1], sheet.Cells[1, colCount]];
            titleRange.Merge();
            titleRange.Value = titleText;
            titleRange.Font.Size = 16;
            titleRange.Font.Bold = true;
            titleRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            sheet.Columns.AutoFit();
        }

        // ==========================================================
        // 6. CÁC HÀM SỰ KIỆN RỖNG (Để tránh lỗi Designer)
        // ==========================================================
        private void guna2TextBox1_TextChanged(object sender, EventArgs e) { }
        private void guna2TextBox2_TextChanged(object sender, EventArgs e) { }
        private void guna2TextBox3_TextChanged(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void guna2PictureBox1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { txtHoTenGV.Focus(); }
        private void label10_Click(object sender, EventArgs e) { txtEmailGV.Focus(); }
        private void label3_Click(object sender, EventArgs e) { txtMatKhauGV.Focus(); }
        private void txtTimGV_TextChanged(object sender, EventArgs e) { }
    }
}