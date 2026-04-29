namespace GUI
{
    partial class UC_CONGCU
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_CONGCU));
            this.guna2PictureBox1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.btnDongHo = new Guna.UI2.WinForms.Guna2ImageButton();
            this.btnBangTrang = new Guna.UI2.WinForms.Guna2ImageButton();
            this.btnGoiTenNgauNhien = new Guna.UI2.WinForms.Guna2ImageButton();
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2PictureBox1
            // 
            this.guna2PictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.guna2PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("guna2PictureBox1.Image")));
            this.guna2PictureBox1.ImageRotate = 0F;
            this.guna2PictureBox1.Location = new System.Drawing.Point(947, 78);
            this.guna2PictureBox1.Name = "guna2PictureBox1";
            this.guna2PictureBox1.Size = new System.Drawing.Size(535, 722);
            this.guna2PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2PictureBox1.TabIndex = 9;
            this.guna2PictureBox1.TabStop = false;
            this.guna2PictureBox1.UseTransparentBackground = true;
            // 
            // btnDongHo
            // 
            this.btnDongHo.BackColor = System.Drawing.Color.Transparent;
            this.btnDongHo.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnDongHo.HoverState.ImageSize = new System.Drawing.Size(280, 280);
            this.btnDongHo.Image = ((System.Drawing.Image)(resources.GetObject("btnDongHo.Image")));
            this.btnDongHo.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnDongHo.ImageRotate = 0F;
            this.btnDongHo.ImageSize = new System.Drawing.Size(280, 280);
            this.btnDongHo.Location = new System.Drawing.Point(234, 380);
            this.btnDongHo.Name = "btnDongHo";
            this.btnDongHo.PressedState.ImageSize = new System.Drawing.Size(280, 280);
            this.btnDongHo.Size = new System.Drawing.Size(520, 429);
            this.btnDongHo.TabIndex = 7;
            this.btnDongHo.UseTransparentBackground = true;
            this.btnDongHo.Click += new System.EventHandler(this.btnDongHoDemNguoc_Click);
            // 
            // btnBangTrang
            // 
            this.btnBangTrang.BackColor = System.Drawing.Color.Transparent;
            this.btnBangTrang.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnBangTrang.HoverState.ImageSize = new System.Drawing.Size(280, 280);
            this.btnBangTrang.Image = ((System.Drawing.Image)(resources.GetObject("btnBangTrang.Image")));
            this.btnBangTrang.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnBangTrang.ImageRotate = 0F;
            this.btnBangTrang.ImageSize = new System.Drawing.Size(280, 280);
            this.btnBangTrang.Location = new System.Drawing.Point(466, 51);
            this.btnBangTrang.Name = "btnBangTrang";
            this.btnBangTrang.PressedState.ImageSize = new System.Drawing.Size(280, 280);
            this.btnBangTrang.Size = new System.Drawing.Size(550, 398);
            this.btnBangTrang.TabIndex = 6;
            this.btnBangTrang.UseTransparentBackground = true;
            this.btnBangTrang.Click += new System.EventHandler(this.btnBangTrang_Click);
            // 
            // btnGoiTenNgauNhien
            // 
            this.btnGoiTenNgauNhien.BackColor = System.Drawing.Color.Transparent;
            this.btnGoiTenNgauNhien.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnGoiTenNgauNhien.HoverState.ImageSize = new System.Drawing.Size(280, 280);
            this.btnGoiTenNgauNhien.Image = ((System.Drawing.Image)(resources.GetObject("btnGoiTenNgauNhien.Image")));
            this.btnGoiTenNgauNhien.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnGoiTenNgauNhien.ImageRotate = 0F;
            this.btnGoiTenNgauNhien.ImageSize = new System.Drawing.Size(280, 280);
            this.btnGoiTenNgauNhien.Location = new System.Drawing.Point(42, 58);
            this.btnGoiTenNgauNhien.Name = "btnGoiTenNgauNhien";
            this.btnGoiTenNgauNhien.PressedState.ImageSize = new System.Drawing.Size(280, 280);
            this.btnGoiTenNgauNhien.Size = new System.Drawing.Size(506, 366);
            this.btnGoiTenNgauNhien.TabIndex = 5;
            this.btnGoiTenNgauNhien.UseTransparentBackground = true;
            this.btnGoiTenNgauNhien.Click += new System.EventHandler(this.btnGoiTenNgauNhien_Click);
            // 
            // UC_CONGCU
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.guna2PictureBox1);
            this.Controls.Add(this.btnDongHo);
            this.Controls.Add(this.btnBangTrang);
            this.Controls.Add(this.btnGoiTenNgauNhien);
            this.Name = "UC_CONGCU";
            this.Size = new System.Drawing.Size(1545, 909);
            ((System.ComponentModel.ISupportInitialize)(this.guna2PictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2PictureBox guna2PictureBox1;
        private Guna.UI2.WinForms.Guna2ImageButton btnDongHo;
        private Guna.UI2.WinForms.Guna2ImageButton btnBangTrang;
        private Guna.UI2.WinForms.Guna2ImageButton btnGoiTenNgauNhien;
    }
}
