// File: TheCauHoi.Designer.cs
namespace GAME1_
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TheCauHoi));
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.btnXoa = new Guna.UI2.WinForms.Guna2Button();
            this.btnChonDapAn = new Guna.UI2.WinForms.Guna2Button();
            this.sound = new Guna.UI2.WinForms.Guna2PictureBox();
            this.pic3 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.pic2 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.pic1 = new Guna.UI2.WinForms.Guna2PictureBox();
            this.lbl = new System.Windows.Forms.Label();
            this.guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sound)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic1)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel1.BorderRadius = 20;
            this.guna2Panel1.BorderThickness = 2;
            this.guna2Panel1.Controls.Add(this.btnXoa);
            this.guna2Panel1.Controls.Add(this.btnChonDapAn);
            this.guna2Panel1.Controls.Add(this.sound);
            this.guna2Panel1.Controls.Add(this.pic3);
            this.guna2Panel1.Controls.Add(this.pic2);
            this.guna2Panel1.Controls.Add(this.pic1);
            this.guna2Panel1.Controls.Add(this.lbl);
            this.guna2Panel1.CustomBorderColor = System.Drawing.Color.MidnightBlue;
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel1.FillColor = System.Drawing.Color.Gainsboro;
            this.guna2Panel1.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.ShadowDecoration.BorderRadius = 20;
            this.guna2Panel1.ShadowDecoration.Color = System.Drawing.Color.Silver;
            this.guna2Panel1.ShadowDecoration.Depth = 10;
            this.guna2Panel1.ShadowDecoration.Enabled = true;
            this.guna2Panel1.Size = new System.Drawing.Size(919, 200);
            this.guna2Panel1.TabIndex = 0;
            // 
            // btnXoa
            // 
            this.btnXoa.BorderRadius = 11;
            this.btnXoa.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnXoa.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnXoa.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnXoa.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnXoa.FillColor = System.Drawing.Color.Maroon;
            this.btnXoa.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(704, 119);
            this.btnXoa.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(192, 44);
            this.btnXoa.TabIndex = 6;
            this.btnXoa.Text = "Xóa";
            // 
            // btnChonDapAn
            // 
            this.btnChonDapAn.BorderRadius = 14;
            this.btnChonDapAn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnChonDapAn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnChonDapAn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnChonDapAn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnChonDapAn.FillColor = System.Drawing.Color.MidnightBlue;
            this.btnChonDapAn.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnChonDapAn.ForeColor = System.Drawing.Color.White;
            this.btnChonDapAn.Location = new System.Drawing.Point(704, 56);
            this.btnChonDapAn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnChonDapAn.Name = "btnChonDapAn";
            this.btnChonDapAn.Size = new System.Drawing.Size(192, 44);
            this.btnChonDapAn.TabIndex = 5;
            this.btnChonDapAn.Text = "Chọn Đáp Án";
            this.btnChonDapAn.Click += new System.EventHandler(this.btnChonDapAn_Click);
            // 
            // sound
            // 
            this.sound.BackColor = System.Drawing.Color.Transparent;
            this.sound.BorderRadius = 15;
            this.sound.Cursor = System.Windows.Forms.Cursors.Hand;
            this.sound.Image = ((System.Drawing.Image)(resources.GetObject("sound.Image")));
            this.sound.ImageRotate = 0F;
            this.sound.Location = new System.Drawing.Point(555, 39);
            this.sound.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.sound.Name = "sound";
            this.sound.Size = new System.Drawing.Size(124, 134);
            this.sound.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.sound.TabIndex = 7;
            this.sound.TabStop = false;
            // 
            // pic3
            // 
            this.pic3.BackColor = System.Drawing.Color.Transparent;
            this.pic3.BorderRadius = 15;
            this.pic3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic3.FillColor = System.Drawing.Color.RosyBrown;
            this.pic3.Image = ((System.Drawing.Image)(resources.GetObject("pic3.Image")));
            this.pic3.ImageRotate = 0F;
            this.pic3.Location = new System.Drawing.Point(369, 39);
            this.pic3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pic3.Name = "pic3";
            this.pic3.Size = new System.Drawing.Size(138, 135);
            this.pic3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic3.TabIndex = 8;
            this.pic3.TabStop = false;
            // 
            // pic2
            // 
            this.pic2.BackColor = System.Drawing.Color.Transparent;
            this.pic2.BorderRadius = 15;
            this.pic2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic2.FillColor = System.Drawing.Color.RosyBrown;
            this.pic2.Image = ((System.Drawing.Image)(resources.GetObject("pic2.Image")));
            this.pic2.ImageRotate = 0F;
            this.pic2.Location = new System.Drawing.Point(191, 39);
            this.pic2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pic2.Name = "pic2";
            this.pic2.Size = new System.Drawing.Size(128, 134);
            this.pic2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic2.TabIndex = 9;
            this.pic2.TabStop = false;
            // 
            // pic1
            // 
            this.pic1.BackColor = System.Drawing.Color.Transparent;
            this.pic1.BorderRadius = 15;
            this.pic1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic1.Image = ((System.Drawing.Image)(resources.GetObject("pic1.Image")));
            this.pic1.ImageRotate = 0F;
            this.pic1.Location = new System.Drawing.Point(25, 39);
            this.pic1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pic1.Name = "pic1";
            this.pic1.Size = new System.Drawing.Size(128, 134);
            this.pic1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic1.TabIndex = 10;
            this.pic1.TabStop = false;
            this.pic1.Click += new System.EventHandler(this.pic1_Click);
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.BackColor = System.Drawing.Color.Transparent;
            this.lbl.Font = new System.Drawing.Font("Montserrat", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lbl.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lbl.Location = new System.Drawing.Point(3, 6);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(117, 34);
            this.lbl.TabIndex = 0;
            this.lbl.Text = "Câu hỏi 1";
            // 
            // TheCauHoi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.guna2Panel1);
            this.Margin = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.Name = "TheCauHoi";
            this.Size = new System.Drawing.Size(919, 200);
            this.Load += new System.EventHandler(this.TheCauHoi_Load);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sound)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.Label lbl;
        private Guna.UI2.WinForms.Guna2PictureBox sound;
        private Guna.UI2.WinForms.Guna2PictureBox pic3;
        private Guna.UI2.WinForms.Guna2PictureBox pic2;
        private Guna.UI2.WinForms.Guna2PictureBox pic1;
        private Guna.UI2.WinForms.Guna2Button btnXoa;
        private Guna.UI2.WinForms.Guna2Button btnChonDapAn;
    }
}