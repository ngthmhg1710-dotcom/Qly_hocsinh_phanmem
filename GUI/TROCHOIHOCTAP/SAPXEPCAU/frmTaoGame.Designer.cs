namespace SapXepCau
{
    partial class frmTaoGame
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.btnClose = new Guna.UI2.WinForms.Guna2ControlBox();
            this.lblLabelTenGame = new System.Windows.Forms.Label();
            this.txtTenGame = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnXemLichSu = new Guna.UI2.WinForms.Guna2Button();
            this.pnlLeft = new Guna.UI2.WinForms.Guna2Panel();
            this.btnLuuVaChoi = new Guna.UI2.WinForms.Guna2Button();
            this.btnThemCau = new Guna.UI2.WinForms.Guna2Button();
            this.txtCauHoi = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblNhapCau = new System.Windows.Forms.Label();
            this.pnlRight = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Panel3 = new Guna.UI2.WinForms.Guna2Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvCauHoi = new Guna.UI2.WinForms.Guna2DataGridView();
            this.colSTT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNoiDung = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnXoaCau = new Guna.UI2.WinForms.Guna2Button();
            this.pnlLeft.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.guna2Panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCauHoi)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2BorderlessForm1
            // 
            this.guna2BorderlessForm1.BorderRadius = 30;
            this.guna2BorderlessForm1.ContainerControl = this;
            this.guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.FillColor = System.Drawing.Color.Transparent;
            this.btnClose.IconColor = System.Drawing.Color.MidnightBlue;
            this.btnClose.Location = new System.Drawing.Point(1603, 15);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(51, 36);
            this.btnClose.TabIndex = 0;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblLabelTenGame
            // 
            this.lblLabelTenGame.AutoSize = true;
            this.lblLabelTenGame.Font = new System.Drawing.Font("Montserrat", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblLabelTenGame.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(25)))), ((int)(((byte)(101)))));
            this.lblLabelTenGame.Location = new System.Drawing.Point(68, 56);
            this.lblLabelTenGame.Name = "lblLabelTenGame";
            this.lblLabelTenGame.Size = new System.Drawing.Size(215, 44);
            this.lblLabelTenGame.TabIndex = 1;
            this.lblLabelTenGame.Text = "Tên bộ game:";
            // 
            // txtTenGame
            // 
            this.txtTenGame.BorderColor = System.Drawing.Color.Silver;
            this.txtTenGame.BorderRadius = 20;
            this.txtTenGame.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenGame.DefaultText = "";
            this.txtTenGame.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtTenGame.Location = new System.Drawing.Point(290, 48);
            this.txtTenGame.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtTenGame.Name = "txtTenGame";
            this.txtTenGame.PlaceholderText = "Ví dụ: Bài tập sắp xếp câu Unit 1";
            this.txtTenGame.SelectedText = "";
            this.txtTenGame.Size = new System.Drawing.Size(675, 62);
            this.txtTenGame.TabIndex = 2;
            // 
            // btnXemLichSu
            // 
            this.btnXemLichSu.BorderRadius = 20;
            this.btnXemLichSu.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(25)))), ((int)(((byte)(101)))));
            this.btnXemLichSu.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnXemLichSu.ForeColor = System.Drawing.Color.White;
            this.btnXemLichSu.Location = new System.Drawing.Point(1406, 48);
            this.btnXemLichSu.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnXemLichSu.Name = "btnXemLichSu";
            this.btnXemLichSu.Size = new System.Drawing.Size(202, 62);
            this.btnXemLichSu.TabIndex = 3;
            this.btnXemLichSu.Text = "Xem Lịch Sử";
            this.btnXemLichSu.Click += new System.EventHandler(this.btnXemLichSu_Click);
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.Transparent;
            this.pnlLeft.BorderRadius = 40;
            this.pnlLeft.Controls.Add(this.btnLuuVaChoi);
            this.pnlLeft.Controls.Add(this.btnThemCau);
            this.pnlLeft.Controls.Add(this.txtCauHoi);
            this.pnlLeft.Controls.Add(this.lblNhapCau);
            this.pnlLeft.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(25)))), ((int)(((byte)(101)))));
            this.pnlLeft.Location = new System.Drawing.Point(74, 162);
            this.pnlLeft.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(652, 750);
            this.pnlLeft.TabIndex = 4;
            // 
            // btnLuuVaChoi
            // 
            this.btnLuuVaChoi.BorderRadius = 25;
            this.btnLuuVaChoi.FillColor = System.Drawing.Color.White;
            this.btnLuuVaChoi.Font = new System.Drawing.Font("Montserrat", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnLuuVaChoi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(30)))), ((int)(((byte)(92)))));
            this.btnLuuVaChoi.Location = new System.Drawing.Point(68, 600);
            this.btnLuuVaChoi.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnLuuVaChoi.Name = "btnLuuVaChoi";
            this.btnLuuVaChoi.Size = new System.Drawing.Size(518, 88);
            this.btnLuuVaChoi.TabIndex = 3;
            this.btnLuuVaChoi.Text = "Bắt đầu học!";
            this.btnLuuVaChoi.Click += new System.EventHandler(this.btnLuuVaChoi_Click);
            // 
            // btnThemCau
            // 
            this.btnThemCau.BorderColor = System.Drawing.Color.White;
            this.btnThemCau.BorderRadius = 20;
            this.btnThemCau.BorderThickness = 2;
            this.btnThemCau.FillColor = System.Drawing.Color.Transparent;
            this.btnThemCau.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnThemCau.ForeColor = System.Drawing.Color.White;
            this.btnThemCau.Location = new System.Drawing.Point(68, 375);
            this.btnThemCau.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnThemCau.Name = "btnThemCau";
            this.btnThemCau.Size = new System.Drawing.Size(518, 75);
            this.btnThemCau.TabIndex = 2;
            this.btnThemCau.Text = "Thêm câu (+)";
            this.btnThemCau.Click += new System.EventHandler(this.btnThemCau_Click);
            // 
            // txtCauHoi
            // 
            this.txtCauHoi.BorderRadius = 10;
            this.txtCauHoi.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCauHoi.DefaultText = "";
            this.txtCauHoi.Font = new System.Drawing.Font("Montserrat", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtCauHoi.Location = new System.Drawing.Point(68, 262);
            this.txtCauHoi.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtCauHoi.Name = "txtCauHoi";
            this.txtCauHoi.PlaceholderText = "Nhập văn bản...";
            this.txtCauHoi.SelectedText = "";
            this.txtCauHoi.Size = new System.Drawing.Size(518, 62);
            this.txtCauHoi.TabIndex = 1;
            // 
            // lblNhapCau
            // 
            this.lblNhapCau.AutoSize = true;
            this.lblNhapCau.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblNhapCau.ForeColor = System.Drawing.Color.White;
            this.lblNhapCau.Location = new System.Drawing.Point(68, 212);
            this.lblNhapCau.Name = "lblNhapCau";
            this.lblNhapCau.Size = new System.Drawing.Size(257, 38);
            this.lblNhapCau.TabIndex = 0;
            this.lblNhapCau.Text = "Nhập Nội Dung Câu";
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.Color.Transparent;
            this.pnlRight.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(30)))), ((int)(((byte)(92)))));
            this.pnlRight.BorderRadius = 40;
            this.pnlRight.BorderThickness = 2;
            this.pnlRight.Controls.Add(this.guna2Panel3);
            this.pnlRight.Controls.Add(this.dgvCauHoi);
            this.pnlRight.Controls.Add(this.btnXoaCau);
            this.pnlRight.FillColor = System.Drawing.Color.White;
            this.pnlRight.Location = new System.Drawing.Point(692, 162);
            this.pnlRight.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(917, 750);
            this.pnlRight.TabIndex = 5;
            // 
            // guna2Panel3
            // 
            this.guna2Panel3.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel3.BorderRadius = 35;
            this.guna2Panel3.Controls.Add(this.label1);
            this.guna2Panel3.FillColor = System.Drawing.Color.Gainsboro;
            this.guna2Panel3.Location = new System.Drawing.Point(29, 4);
            this.guna2Panel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.guna2Panel3.Name = "guna2Panel3";
            this.guna2Panel3.Size = new System.Drawing.Size(884, 79);
            this.guna2Panel3.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Montserrat", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label1.Location = new System.Drawing.Point(299, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(283, 49);
            this.label1.TabIndex = 0;
            this.label1.Text = "Các Câu Đã Tạo";
            // 
            // dgvCauHoi
            // 
            this.dgvCauHoi.AllowUserToAddRows = false;
            this.dgvCauHoi.AllowUserToDeleteRows = false;
            this.dgvCauHoi.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvCauHoi.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCauHoi.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCauHoi.ColumnHeadersHeight = 4;
            this.dgvCauHoi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvCauHoi.ColumnHeadersVisible = false;
            this.dgvCauHoi.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSTT,
            this.colNoiDung});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 12F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCauHoi.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvCauHoi.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvCauHoi.Location = new System.Drawing.Point(56, 112);
            this.dgvCauHoi.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvCauHoi.MultiSelect = false;
            this.dgvCauHoi.Name = "dgvCauHoi";
            this.dgvCauHoi.ReadOnly = true;
            this.dgvCauHoi.RowHeadersVisible = false;
            this.dgvCauHoi.RowHeadersWidth = 51;
            this.dgvCauHoi.RowTemplate.Height = 40;
            this.dgvCauHoi.Size = new System.Drawing.Size(830, 525);
            this.dgvCauHoi.TabIndex = 4;
            this.dgvCauHoi.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvCauHoi.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvCauHoi.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvCauHoi.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvCauHoi.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvCauHoi.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvCauHoi.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvCauHoi.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.White;
            this.dgvCauHoi.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvCauHoi.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.dgvCauHoi.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvCauHoi.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvCauHoi.ThemeStyle.HeaderStyle.Height = 4;
            this.dgvCauHoi.ThemeStyle.ReadOnly = true;
            this.dgvCauHoi.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvCauHoi.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvCauHoi.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.dgvCauHoi.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvCauHoi.ThemeStyle.RowsStyle.Height = 40;
            this.dgvCauHoi.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvCauHoi.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // colSTT
            // 
            this.colSTT.FillWeight = 30F;
            this.colSTT.HeaderText = "STT";
            this.colSTT.MinimumWidth = 6;
            this.colSTT.Name = "colSTT";
            this.colSTT.ReadOnly = true;
            // 
            // colNoiDung
            // 
            this.colNoiDung.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colNoiDung.HeaderText = "NoiDung";
            this.colNoiDung.MinimumWidth = 6;
            this.colNoiDung.Name = "colNoiDung";
            this.colNoiDung.ReadOnly = true;
            // 
            // btnXoaCau
            // 
            this.btnXoaCau.BorderRadius = 15;
            this.btnXoaCau.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnXoaCau.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnXoaCau.ForeColor = System.Drawing.Color.Red;
            this.btnXoaCau.Location = new System.Drawing.Point(342, 675);
            this.btnXoaCau.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnXoaCau.Name = "btnXoaCau";
            this.btnXoaCau.Size = new System.Drawing.Size(225, 50);
            this.btnXoaCau.TabIndex = 3;
            this.btnXoaCau.Text = "Xóa dòng chọn";
            this.btnXoaCau.Click += new System.EventHandler(this.btnXoaCau_Click);
            // 
            // frmTaoGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1667, 968);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.btnXemLichSu);
            this.Controls.Add(this.txtTenGame);
            this.Controls.Add(this.lblLabelTenGame);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pnlRight);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmTaoGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmTaoGame";
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeft.PerformLayout();
            this.pnlRight.ResumeLayout(false);
            this.guna2Panel3.ResumeLayout(false);
            this.guna2Panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCauHoi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2ControlBox btnClose;
        private System.Windows.Forms.Label lblLabelTenGame;
        private Guna.UI2.WinForms.Guna2TextBox txtTenGame;
        private Guna.UI2.WinForms.Guna2Button btnXemLichSu;
        private Guna.UI2.WinForms.Guna2Panel pnlLeft;
        private System.Windows.Forms.Label lblNhapCau;
        private Guna.UI2.WinForms.Guna2TextBox txtCauHoi;
        private Guna.UI2.WinForms.Guna2Button btnThemCau;
        private Guna.UI2.WinForms.Guna2Button btnLuuVaChoi;
        private Guna.UI2.WinForms.Guna2Panel pnlRight;
        private Guna.UI2.WinForms.Guna2Button btnXoaCau;
        private Guna.UI2.WinForms.Guna2DataGridView dgvCauHoi;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSTT;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNoiDung;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel3;
        private System.Windows.Forms.Label label1;
    }
}