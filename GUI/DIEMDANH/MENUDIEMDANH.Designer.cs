namespace GUI.DIEMDANH
{
    partial class MENUDIEMDANH
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.guna2GradientPanel1 = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.guna2Separator1 = new Guna.UI2.WinForms.Guna2Separator();
            this.lblDiemDanh = new System.Windows.Forms.Label();
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.btnTaoQR = new Guna.UI2.WinForms.Guna2Button();
            this.btnCameraDiemDanh = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // guna2GradientPanel1
            // 
            this.guna2GradientPanel1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(25)))), ((int)(((byte)(101)))));
            this.guna2GradientPanel1.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(59)))), ((int)(((byte)(135)))));
            this.guna2GradientPanel1.Location = new System.Drawing.Point(-10, 84);
            this.guna2GradientPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guna2GradientPanel1.Name = "guna2GradientPanel1";
            this.guna2GradientPanel1.Size = new System.Drawing.Size(605, 50);
            this.guna2GradientPanel1.TabIndex = 34;
            // 
            // guna2ControlBox1
            // 
            this.guna2ControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox1.FillColor = System.Drawing.Color.White;
            this.guna2ControlBox1.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(25)))), ((int)(((byte)(101)))));
            this.guna2ControlBox1.Location = new System.Drawing.Point(527, 11);
            this.guna2ControlBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guna2ControlBox1.Name = "guna2ControlBox1";
            this.guna2ControlBox1.Size = new System.Drawing.Size(45, 29);
            this.guna2ControlBox1.TabIndex = 132;
            // 
            // guna2Separator1
            // 
            this.guna2Separator1.FillThickness = 2;
            this.guna2Separator1.Location = new System.Drawing.Point(41, 206);
            this.guna2Separator1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.guna2Separator1.Name = "guna2Separator1";
            this.guna2Separator1.Size = new System.Drawing.Size(500, 12);
            this.guna2Separator1.TabIndex = 131;
            // 
            // lblDiemDanh
            // 
            this.lblDiemDanh.AutoSize = true;
            this.lblDiemDanh.BackColor = System.Drawing.Color.Transparent;
            this.lblDiemDanh.Font = new System.Drawing.Font("Montserrat", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblDiemDanh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(25)))), ((int)(((byte)(101)))));
            this.lblDiemDanh.Location = new System.Drawing.Point(176, 29);
            this.lblDiemDanh.Name = "lblDiemDanh";
            this.lblDiemDanh.Size = new System.Drawing.Size(223, 49);
            this.lblDiemDanh.TabIndex = 128;
            this.lblDiemDanh.Text = "ĐIỂM DANH";
            // 
            // guna2BorderlessForm1
            // 
            this.guna2BorderlessForm1.BorderRadius = 35;
            this.guna2BorderlessForm1.ContainerControl = this;
            this.guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // btnTaoQR
            // 
            this.btnTaoQR.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnTaoQR.BackColor = System.Drawing.Color.Transparent;
            this.btnTaoQR.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnTaoQR.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnTaoQR.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnTaoQR.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnTaoQR.FillColor = System.Drawing.Color.Transparent;
            this.btnTaoQR.Font = new System.Drawing.Font("Montserrat", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnTaoQR.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(37)))), ((int)(((byte)(102)))));
            this.btnTaoQR.Location = new System.Drawing.Point(-17, 142);
            this.btnTaoQR.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnTaoQR.Name = "btnTaoQR";
            this.btnTaoQR.Size = new System.Drawing.Size(606, 58);
            this.btnTaoQR.TabIndex = 133;
            this.btnTaoQR.Text = "Tạo QR điểm danh";
            this.btnTaoQR.UseTransparentBackground = true;
            this.btnTaoQR.Click += new System.EventHandler(this.btnTaoQR_Click);
            // 
            // btnCameraDiemDanh
            // 
            this.btnCameraDiemDanh.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCameraDiemDanh.BackColor = System.Drawing.Color.Transparent;
            this.btnCameraDiemDanh.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCameraDiemDanh.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCameraDiemDanh.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCameraDiemDanh.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCameraDiemDanh.FillColor = System.Drawing.Color.Transparent;
            this.btnCameraDiemDanh.Font = new System.Drawing.Font("Montserrat", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnCameraDiemDanh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(37)))), ((int)(((byte)(102)))));
            this.btnCameraDiemDanh.Location = new System.Drawing.Point(68, 224);
            this.btnCameraDiemDanh.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCameraDiemDanh.Name = "btnCameraDiemDanh";
            this.btnCameraDiemDanh.Size = new System.Drawing.Size(450, 58);
            this.btnCameraDiemDanh.TabIndex = 134;
            this.btnCameraDiemDanh.Text = "Camera Điểm danh";
            this.btnCameraDiemDanh.UseTransparentBackground = true;
            this.btnCameraDiemDanh.Click += new System.EventHandler(this.btnCameraDiemDanh_Click);
            // 
            // MENUCONGCU_DIEMDANH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(584, 302);
            this.Controls.Add(this.btnTaoQR);
            this.Controls.Add(this.btnCameraDiemDanh);
            this.Controls.Add(this.guna2ControlBox1);
            this.Controls.Add(this.guna2GradientPanel1);
            this.Controls.Add(this.guna2Separator1);
            this.Controls.Add(this.lblDiemDanh);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MENUCONGCU_DIEMDANH";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MENUCONGCU_DIEMDANH";
            this.Load += new System.EventHandler(this.MENUCONGCU_DIEMDANH_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel1;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
        private Guna.UI2.WinForms.Guna2Separator guna2Separator1;
        private System.Windows.Forms.Label lblDiemDanh;
        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2Button btnTaoQR;
        private Guna.UI2.WinForms.Guna2Button btnCameraDiemDanh;
    }
}