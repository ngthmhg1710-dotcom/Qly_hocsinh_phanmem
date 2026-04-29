namespace GUI
{
    partial class UC_MON_GV
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_MON_GV));
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.panelLop = new Guna.UI2.WinForms.Guna2Panel();
            this.btnDS = new Guna.UI2.WinForms.Guna2ImageButton();
            this.btnAnh = new Guna.UI2.WinForms.Guna2PictureBox();
            this.guna2GradientPanel1 = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.lblTenLop = new System.Windows.Forms.Label();
            this.lblTenMon = new System.Windows.Forms.Label();
            this.panelLop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAnh)).BeginInit();
            this.guna2GradientPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 30;
            this.guna2Elipse1.TargetControl = this;
            // 
            // panelLop
            // 
            this.panelLop.BackColor = System.Drawing.Color.White;
            this.panelLop.BorderColor = System.Drawing.Color.MidnightBlue;
            this.panelLop.BorderRadius = 15;
            this.panelLop.BorderThickness = 2;
            this.panelLop.Controls.Add(this.btnDS);
            this.panelLop.Controls.Add(this.btnAnh);
            this.panelLop.Controls.Add(this.guna2GradientPanel1);
            this.panelLop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLop.Location = new System.Drawing.Point(0, 0);
            this.panelLop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelLop.Name = "panelLop";
            this.panelLop.Size = new System.Drawing.Size(300, 300);
            this.panelLop.TabIndex = 0;
            // 
            // btnDS
            // 
            this.btnDS.BackColor = System.Drawing.Color.Transparent;
            this.btnDS.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnDS.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDS.HoverState.ImageSize = new System.Drawing.Size(50, 50);
            this.btnDS.Image = ((System.Drawing.Image)(resources.GetObject("btnDS.Image")));
            this.btnDS.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnDS.ImageRotate = 0F;
            this.btnDS.ImageSize = new System.Drawing.Size(50, 50);
            this.btnDS.Location = new System.Drawing.Point(216, 222);
            this.btnDS.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDS.Name = "btnDS";
            this.btnDS.PressedState.ImageSize = new System.Drawing.Size(50, 50);
            this.btnDS.Size = new System.Drawing.Size(64, 54);
            this.btnDS.TabIndex = 1;
            this.btnDS.UseTransparentBackground = true;
            // 
            // btnAnh
            // 
            this.btnAnh.BackColor = System.Drawing.Color.Transparent;
            this.btnAnh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAnh.Image = ((System.Drawing.Image)(resources.GetObject("btnAnh.Image")));
            this.btnAnh.ImageRotate = 0F;
            this.btnAnh.Location = new System.Drawing.Point(148, 19);
            this.btnAnh.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAnh.Name = "btnAnh";
            this.btnAnh.Size = new System.Drawing.Size(132, 156);
            this.btnAnh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btnAnh.TabIndex = 0;
            this.btnAnh.TabStop = false;
            this.btnAnh.UseTransparentBackground = true;
            this.btnAnh.Click += new System.EventHandler(this.guna2PictureBox1_Click);
            // 
            // guna2GradientPanel1
            // 
            this.guna2GradientPanel1.BorderRadius = 15;
            this.guna2GradientPanel1.Controls.Add(this.lblSoLuong);
            this.guna2GradientPanel1.Controls.Add(this.lblTenLop);
            this.guna2GradientPanel1.Controls.Add(this.lblTenMon);
            this.guna2GradientPanel1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(25)))), ((int)(((byte)(101)))));
            this.guna2GradientPanel1.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(59)))), ((int)(((byte)(135)))));
            this.guna2GradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.guna2GradientPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guna2GradientPanel1.Name = "guna2GradientPanel1";
            this.guna2GradientPanel1.Size = new System.Drawing.Size(300, 119);
            this.guna2GradientPanel1.TabIndex = 0;
            // 
            // lblSoLuong
            // 
            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.BackColor = System.Drawing.Color.Transparent;
            this.lblSoLuong.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblSoLuong.ForeColor = System.Drawing.Color.LightGray;
            this.lblSoLuong.Location = new System.Drawing.Point(24, 85);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(80, 31);
            this.lblSoLuong.TabIndex = 3;
            this.lblSoLuong.Text = "Sĩ số: ...";
            // 
            // lblTenLop
            // 
            this.lblTenLop.AutoSize = true;
            this.lblTenLop.BackColor = System.Drawing.Color.Transparent;
            this.lblTenLop.Font = new System.Drawing.Font("Montserrat", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTenLop.ForeColor = System.Drawing.Color.White;
            this.lblTenLop.Location = new System.Drawing.Point(24, 50);
            this.lblTenLop.Name = "lblTenLop";
            this.lblTenLop.Size = new System.Drawing.Size(67, 44);
            this.lblTenLop.TabIndex = 2;
            this.lblTenLop.Text = "5A1";
            // 
            // lblTenMon
            // 
            this.lblTenMon.AutoSize = true;
            this.lblTenMon.BackColor = System.Drawing.Color.Transparent;
            this.lblTenMon.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTenMon.ForeColor = System.Drawing.Color.White;
            this.lblTenMon.Location = new System.Drawing.Point(24, 15);
            this.lblTenMon.Name = "lblTenMon";
            this.lblTenMon.Size = new System.Drawing.Size(75, 38);
            this.lblTenMon.TabIndex = 1;
            this.lblTenMon.Text = "Toán";
            // 
            // UC_MON_GV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelLop);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "UC_MON_GV";
            this.Size = new System.Drawing.Size(300, 300);
            this.panelLop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnAnh)).EndInit();
            this.guna2GradientPanel1.ResumeLayout(false);
            this.guna2GradientPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2Panel panelLop;
        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel1;
        private Guna.UI2.WinForms.Guna2PictureBox btnAnh;
        private Guna.UI2.WinForms.Guna2ImageButton btnDS;

        // Đã sửa tên biến cho khớp với code CS của bạn
        public System.Windows.Forms.Label lblTenLop;
        public System.Windows.Forms.Label lblTenMon;
        public System.Windows.Forms.Label lblSoLuong; // Thêm cái này vì CS có dùng
    }
}