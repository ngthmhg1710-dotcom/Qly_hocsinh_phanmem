// File: TheCauHoi.Designer.cs
namespace RANDOMSO
{
    partial class TheCauHoi
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.btnPlaceholder = new Guna.UI2.WinForms.Guna2Button();
            this.txtCauHoi = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnXoa = new Guna.UI2.WinForms.Guna2Button();
            this.picHinhAnh = new Guna.UI2.WinForms.Guna2PictureBox();
            this.lbl = new System.Windows.Forms.Label();
            this.guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHinhAnh)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel1.BorderColor = System.Drawing.Color.MidnightBlue;
            this.guna2Panel1.BorderRadius = 20;
            this.guna2Panel1.BorderThickness = 1;
            this.guna2Panel1.Controls.Add(this.btnPlaceholder);
            this.guna2Panel1.Controls.Add(this.txtCauHoi);
            this.guna2Panel1.Controls.Add(this.btnXoa);
            this.guna2Panel1.Controls.Add(this.picHinhAnh);
            this.guna2Panel1.Controls.Add(this.lbl);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel1.FillColor = System.Drawing.Color.White;
            this.guna2Panel1.Location = new System.Drawing.Point(11, 12);
            this.guna2Panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.ShadowDecoration.BorderRadius = 22;
            this.guna2Panel1.ShadowDecoration.Color = System.Drawing.Color.DarkGray;
            this.guna2Panel1.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(5, 5, 8, 8);
            this.guna2Panel1.Size = new System.Drawing.Size(709, 263);
            this.guna2Panel1.TabIndex = 0;
            this.guna2Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.guna2Panel1_Paint_1);
            // 
            // btnPlaceholder
            // 
            this.btnPlaceholder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPlaceholder.BorderRadius = 15;
            this.btnPlaceholder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPlaceholder.FillColor = System.Drawing.Color.WhiteSmoke;
            this.btnPlaceholder.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
            this.btnPlaceholder.ForeColor = System.Drawing.Color.Gray;
            this.btnPlaceholder.Location = new System.Drawing.Point(464, 61);
            this.btnPlaceholder.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnPlaceholder.Name = "btnPlaceholder";
            this.btnPlaceholder.Size = new System.Drawing.Size(214, 132);
            this.btnPlaceholder.TabIndex = 12;
            this.btnPlaceholder.Text = "Thêm hình";
            this.btnPlaceholder.Click += new System.EventHandler(this.btnPlaceholder_Click);
            // 
            // txtCauHoi
            // 
            this.txtCauHoi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCauHoi.BorderRadius = 10;
            this.txtCauHoi.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCauHoi.DefaultText = "";
            this.txtCauHoi.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.txtCauHoi.Location = new System.Drawing.Point(26, 61);
            this.txtCauHoi.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtCauHoi.Multiline = true;
            this.txtCauHoi.Name = "txtCauHoi";
            this.txtCauHoi.PlaceholderText = "Nhập nội dung câu hỏi tại đây...";
            this.txtCauHoi.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCauHoi.SelectedText = "";
            this.txtCauHoi.Size = new System.Drawing.Size(415, 175);
            this.txtCauHoi.TabIndex = 11;
            // 
            // btnXoa
            // 
            this.btnXoa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXoa.BorderRadius = 11;
            this.btnXoa.FillColor = System.Drawing.Color.Maroon;
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(519, 214);
            this.btnXoa.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(101, 38);
            this.btnXoa.TabIndex = 6;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // picHinhAnh
            // 
            this.picHinhAnh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picHinhAnh.BackColor = System.Drawing.Color.White;
            this.picHinhAnh.BorderRadius = 15;
            this.picHinhAnh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picHinhAnh.ImageRotate = 0F;
            this.picHinhAnh.Location = new System.Drawing.Point(464, 61);
            this.picHinhAnh.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.picHinhAnh.Name = "picHinhAnh";
            this.picHinhAnh.Size = new System.Drawing.Size(214, 132);
            this.picHinhAnh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picHinhAnh.TabIndex = 10;
            this.picHinhAnh.TabStop = false;
            this.picHinhAnh.Click += new System.EventHandler(this.picHinhAnh_Click);
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.BackColor = System.Drawing.Color.Transparent;
            this.lbl.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lbl.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lbl.Location = new System.Drawing.Point(20, 18);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(121, 32);
            this.lbl.TabIndex = 0;
            this.lbl.Text = "Câu hỏi 1";
            // 
            // TheCauHoi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.guna2Panel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "TheCauHoi";
            this.Padding = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.Size = new System.Drawing.Size(731, 287);
            this.Load += new System.EventHandler(this.TheCauHoi_Load);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHinhAnh)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.Label lbl;
        private Guna.UI2.WinForms.Guna2PictureBox picHinhAnh;
        private Guna.UI2.WinForms.Guna2Button btnXoa;
        private Guna.UI2.WinForms.Guna2TextBox txtCauHoi;
        private Guna.UI2.WinForms.Guna2Button btnPlaceholder;
    }
}