// File: TAOCAUHOI_NGHECHONHINH.Designer.cs
namespace GAME1_
{
    partial class TAOCAUHOI_NGHECHONHINH
    {
        private System.ComponentModel.IContainer components = null;

        // --- KHAI BÁO CÁC CONTROL TRÊN FORM ---
        private System.Windows.Forms.FlowLayoutPanel pnlContainer;
        private Guna.UI2.WinForms.Guna2Button THEMTHE;
        private Guna.UI2.WinForms.Guna2Button TAO;
        private Guna.UI2.WinForms.Guna2TextBox txtTenGame;
        private System.Windows.Forms.Label labelTenGame;
        private Guna.UI2.WinForms.Guna2Button btnLichSu; // Nút mới
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.THEMTHE = new Guna.UI2.WinForms.Guna2Button();
            this.TAO = new Guna.UI2.WinForms.Guna2Button();
            this.txtTenGame = new Guna.UI2.WinForms.Guna2TextBox();
            this.labelTenGame = new System.Windows.Forms.Label();
            this.btnLichSu = new Guna.UI2.WinForms.Guna2Button();
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
            this.pnlContainer.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlContainer.Location = new System.Drawing.Point(14, 88);
            this.pnlContainer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(1497, 673);
            this.pnlContainer.TabIndex = 5;
            // 
            // THEMTHE
            // 
            this.THEMTHE.BorderRadius = 18;
            this.THEMTHE.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.THEMTHE.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.THEMTHE.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.THEMTHE.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.THEMTHE.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(25)))), ((int)(((byte)(101)))));
            this.THEMTHE.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.TAO.BorderColor = System.Drawing.Color.MidnightBlue;
            this.TAO.BorderRadius = 18;
            this.TAO.BorderThickness = 2;
            this.TAO.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.TAO.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.TAO.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.TAO.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.TAO.FillColor = System.Drawing.Color.Transparent;
            this.TAO.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TAO.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(59)))), ((int)(((byte)(135)))));
            this.TAO.Location = new System.Drawing.Point(1227, 769);
            this.TAO.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TAO.Name = "TAO";
            this.TAO.Size = new System.Drawing.Size(284, 65);
            this.TAO.TabIndex = 3;
            this.TAO.Text = "TẠO VÀ LƯU GAME";
            this.TAO.Click += new System.EventHandler(this.TAO_Click);
            // 
            // txtTenGame
            // 
            this.txtTenGame.BorderRadius = 18;
            this.txtTenGame.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenGame.DefaultText = "";
            this.txtTenGame.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTenGame.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTenGame.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTenGame.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTenGame.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTenGame.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenGame.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTenGame.Location = new System.Drawing.Point(461, 15);
            this.txtTenGame.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.txtTenGame.Name = "txtTenGame";
            this.txtTenGame.PlaceholderText = "Ví dụ: Bài tập về Động vật";
            this.txtTenGame.SelectedText = "";
            this.txtTenGame.Size = new System.Drawing.Size(755, 65);
            this.txtTenGame.TabIndex = 2;
            // 
            // labelTenGame
            // 
            this.labelTenGame.AutoSize = true;
            this.labelTenGame.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTenGame.ForeColor = System.Drawing.Color.MidnightBlue;
            this.labelTenGame.Location = new System.Drawing.Point(232, 30);
            this.labelTenGame.Name = "labelTenGame";
            this.labelTenGame.Size = new System.Drawing.Size(215, 38);
            this.labelTenGame.TabIndex = 1;
            this.labelTenGame.Text = "Tên bộ câu hỏi:";
            // 
            // btnLichSu
            // 
            this.btnLichSu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLichSu.BorderRadius = 18;
            this.btnLichSu.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLichSu.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLichSu.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLichSu.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLichSu.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(25)))), ((int)(((byte)(101)))));
            this.btnLichSu.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnLichSu.ForeColor = System.Drawing.Color.White;
            this.btnLichSu.Location = new System.Drawing.Point(1237, 13);
            this.btnLichSu.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnLichSu.Name = "btnLichSu";
            this.btnLichSu.Size = new System.Drawing.Size(223, 65);
            this.btnLichSu.TabIndex = 4;
            this.btnLichSu.Text = "Xem Lịch Sử";
            this.btnLichSu.Click += new System.EventHandler(this.btnLichSu_Click);
            // 
            // guna2BorderlessForm1
            // 
            this.guna2BorderlessForm1.BorderRadius = 30;
            this.guna2BorderlessForm1.ContainerControl = this;
            this.guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // guna2ControlBox1
            // 
            this.guna2ControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox1.BackColor = System.Drawing.Color.MidnightBlue;
            this.guna2ControlBox1.FillColor = System.Drawing.Color.White;
            this.guna2ControlBox1.IconColor = System.Drawing.Color.MidnightBlue;
            this.guna2ControlBox1.Location = new System.Drawing.Point(1466, 12);
            this.guna2ControlBox1.Name = "guna2ControlBox1";
            this.guna2ControlBox1.Size = new System.Drawing.Size(45, 29);
            this.guna2ControlBox1.TabIndex = 6;
            // 
            // TAOCAUHOI_NGHECHONHINH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1524, 850);
            this.Controls.Add(this.guna2ControlBox1);
            this.Controls.Add(this.btnLichSu);
            this.Controls.Add(this.TAO);
            this.Controls.Add(this.txtTenGame);
            this.Controls.Add(this.labelTenGame);
            this.Controls.Add(this.THEMTHE);
            this.Controls.Add(this.pnlContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "TAOCAUHOI_NGHECHONHINH";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tạo câu hỏi - Nghe chọn hình";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
    }
}