// File: Form1.Designer.cs
namespace RANDOMSO
{
    partial class LSVONGQUAY
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.FlowLayoutPanel pnlContainer;
        private Guna.UI2.WinForms.Guna2Button THEMTHE;
        private Guna.UI2.WinForms.Guna2Button TAO;
        // KHAI BÁO CÁC CONTROL MỚI
        private System.Windows.Forms.Label lblTenBoCauHoi;
        private Guna.UI2.WinForms.Guna2TextBox txtTenBoCauHoi;
        private Guna.UI2.WinForms.Guna2Button btnXemLichSu;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.THEMTHE = new Guna.UI2.WinForms.Guna2Button();
            this.TAO = new Guna.UI2.WinForms.Guna2Button();
            this.lblTenBoCauHoi = new System.Windows.Forms.Label();
            this.txtTenBoCauHoi = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnXemLichSu = new Guna.UI2.WinForms.Guna2Button();
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.SuspendLayout();
            // 
            // pnlContainer
            // 
            this.pnlContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlContainer.AutoScroll = true;
            this.pnlContainer.BackColor = System.Drawing.SystemColors.Control;
            this.pnlContainer.Location = new System.Drawing.Point(14, 100);
            this.pnlContainer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Padding = new System.Windows.Forms.Padding(11, 12, 11, 25);
            this.pnlContainer.Size = new System.Drawing.Size(1548, 747);
            this.pnlContainer.TabIndex = 5;
            // 
            // THEMTHE
            // 
            this.THEMTHE.BorderRadius = 18;
            this.THEMTHE.FillColor = System.Drawing.Color.MidnightBlue;
            this.THEMTHE.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.THEMTHE.ForeColor = System.Drawing.Color.White;
            this.THEMTHE.Location = new System.Drawing.Point(14, 15);
            this.THEMTHE.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.THEMTHE.Name = "THEMTHE";
            this.THEMTHE.Size = new System.Drawing.Size(184, 65);
            this.THEMTHE.TabIndex = 0;
            this.THEMTHE.Text = "Thêm Thẻ";
            this.THEMTHE.Click += new System.EventHandler(this.THEMTHE_Click);
            // 
            // TAO
            // 
            this.TAO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TAO.BorderColor = System.Drawing.Color.MidnightBlue;
            this.TAO.BorderRadius = 18;
            this.TAO.BorderThickness = 2;
            this.TAO.FillColor = System.Drawing.Color.Transparent;
            this.TAO.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.TAO.ForeColor = System.Drawing.Color.MidnightBlue;
            this.TAO.Location = new System.Drawing.Point(1339, 856);
            this.TAO.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TAO.Name = "TAO";
            this.TAO.Size = new System.Drawing.Size(224, 65);
            this.TAO.TabIndex = 3;
            this.TAO.Text = "Tạo Game";
            this.TAO.Click += new System.EventHandler(this.TAO_Click);
            // 
            // lblTenBoCauHoi
            // 
            this.lblTenBoCauHoi.AutoSize = true;
            this.lblTenBoCauHoi.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenBoCauHoi.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblTenBoCauHoi.Location = new System.Drawing.Point(225, 30);
            this.lblTenBoCauHoi.Name = "lblTenBoCauHoi";
            this.lblTenBoCauHoi.Size = new System.Drawing.Size(215, 38);
            this.lblTenBoCauHoi.TabIndex = 1;
            this.lblTenBoCauHoi.Text = "Tên bộ câu hỏi:";
            // 
            // txtTenBoCauHoi
            // 
            this.txtTenBoCauHoi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTenBoCauHoi.BorderRadius = 18;
            this.txtTenBoCauHoi.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenBoCauHoi.DefaultText = "";
            this.txtTenBoCauHoi.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTenBoCauHoi.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTenBoCauHoi.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTenBoCauHoi.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTenBoCauHoi.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTenBoCauHoi.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtTenBoCauHoi.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTenBoCauHoi.Location = new System.Drawing.Point(447, 15);
            this.txtTenBoCauHoi.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTenBoCauHoi.Name = "txtTenBoCauHoi";
            this.txtTenBoCauHoi.PlaceholderText = "Ví dụ: Bài tập về Động vật";
            this.txtTenBoCauHoi.SelectedText = "";
            this.txtTenBoCauHoi.Size = new System.Drawing.Size(826, 65);
            this.txtTenBoCauHoi.TabIndex = 2;
            // 
            // btnXemLichSu
            // 
            this.btnXemLichSu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXemLichSu.BorderRadius = 18;
            this.btnXemLichSu.FillColor = System.Drawing.Color.MidnightBlue;
            this.btnXemLichSu.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnXemLichSu.ForeColor = System.Drawing.Color.White;
            this.btnXemLichSu.Location = new System.Drawing.Point(1292, 15);
            this.btnXemLichSu.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnXemLichSu.Name = "btnXemLichSu";
            this.btnXemLichSu.Size = new System.Drawing.Size(210, 65);
            this.btnXemLichSu.TabIndex = 4;
            this.btnXemLichSu.Text = "Xem Lịch Sử";
            this.btnXemLichSu.Click += new System.EventHandler(this.btnXemLichSu_Click);
            // 
            // guna2BorderlessForm1
            // 
            this.guna2BorderlessForm1.BorderRadius = 15;
            this.guna2BorderlessForm1.ContainerControl = this;
            this.guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // guna2ControlBox1
            // 
            this.guna2ControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox1.FillColor = System.Drawing.Color.White;
            this.guna2ControlBox1.IconColor = System.Drawing.Color.MidnightBlue;
            this.guna2ControlBox1.Location = new System.Drawing.Point(1518, 15);
            this.guna2ControlBox1.Name = "guna2ControlBox1";
            this.guna2ControlBox1.Size = new System.Drawing.Size(45, 29);
            this.guna2ControlBox1.TabIndex = 6;
            // 
            // LSVONGQUAY
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1575, 934);
            this.Controls.Add(this.guna2ControlBox1);
            this.Controls.Add(this.TAO);
            this.Controls.Add(this.btnXemLichSu);
            this.Controls.Add(this.txtTenBoCauHoi);
            this.Controls.Add(this.lblTenBoCauHoi);
            this.Controls.Add(this.THEMTHE);
            this.Controls.Add(this.pnlContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(1122, 736);
            this.Name = "LSVONGQUAY";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tạo câu hỏi - Random số";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
    }
}