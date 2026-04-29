namespace SapXepCau
{
    partial class frmChoiGame
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
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.pnlHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.lblCauSo = new System.Windows.Forms.Label();
            this.btnClose = new Guna.UI2.WinForms.Guna2ControlBox();
            this.lblTenGame = new System.Windows.Forms.Label();
            this.pnlTargetContainer = new Guna.UI2.WinForms.Guna2Panel();
            this.flpTarget = new System.Windows.Forms.FlowLayoutPanel();
            this.lblHuongDan = new System.Windows.Forms.Label();
            this.flpSource = new System.Windows.Forms.FlowLayoutPanel();
            this.btnKiemTra = new Guna.UI2.WinForms.Guna2Button();
            this.btnTiepTuc = new Guna.UI2.WinForms.Guna2Button();
            this.lblThongBao = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlTargetContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2BorderlessForm1
            // 
            this.guna2BorderlessForm1.BorderRadius = 30;
            this.guna2BorderlessForm1.ContainerControl = this;
            this.guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(30)))), ((int)(((byte)(92)))));
            this.pnlHeader.Controls.Add(this.lblCauSo);
            this.pnlHeader.Controls.Add(this.btnClose);
            this.pnlHeader.Controls.Add(this.lblTenGame);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1667, 150);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblCauSo
            // 
            this.lblCauSo.AutoSize = true;
            this.lblCauSo.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblCauSo.ForeColor = System.Drawing.Color.LightGray;
            this.lblCauSo.Location = new System.Drawing.Point(774, 89);
            this.lblCauSo.Name = "lblCauSo";
            this.lblCauSo.Size = new System.Drawing.Size(118, 38);
            this.lblCauSo.TabIndex = 2;
            this.lblCauSo.Text = "Câu 1 / 5";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.FillColor = System.Drawing.Color.Transparent;
            this.btnClose.IconColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(1603, 15);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(51, 36);
            this.btnClose.TabIndex = 1;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblTenGame
            // 
            this.lblTenGame.Font = new System.Drawing.Font("Montserrat", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenGame.ForeColor = System.Drawing.Color.White;
            this.lblTenGame.Location = new System.Drawing.Point(0, 25);
            this.lblTenGame.Name = "lblTenGame";
            this.lblTenGame.Size = new System.Drawing.Size(1667, 51);
            this.lblTenGame.TabIndex = 0;
            this.lblTenGame.Text = "TÊN BÀI TẬP";
            this.lblTenGame.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlTargetContainer
            // 
            this.pnlTargetContainer.BackColor = System.Drawing.Color.Transparent;
            this.pnlTargetContainer.BorderRadius = 20;
            this.pnlTargetContainer.Controls.Add(this.flpTarget);
            this.pnlTargetContainer.Controls.Add(this.lblHuongDan);
            this.pnlTargetContainer.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
            this.pnlTargetContainer.Location = new System.Drawing.Point(158, 200);
            this.pnlTargetContainer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlTargetContainer.Name = "pnlTargetContainer";
            this.pnlTargetContainer.ShadowDecoration.Depth = 5;
            this.pnlTargetContainer.ShadowDecoration.Enabled = true;
            this.pnlTargetContainer.Size = new System.Drawing.Size(1350, 250);
            this.pnlTargetContainer.TabIndex = 1;
            // 
            // flpTarget
            // 
            this.flpTarget.BackColor = System.Drawing.Color.Transparent;
            this.flpTarget.Location = new System.Drawing.Point(22, 62);
            this.flpTarget.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.flpTarget.Name = "flpTarget";
            this.flpTarget.Size = new System.Drawing.Size(1305, 162);
            this.flpTarget.TabIndex = 1;
            // 
            // lblHuongDan
            // 
            this.lblHuongDan.AutoSize = true;
            this.lblHuongDan.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHuongDan.ForeColor = System.Drawing.Color.Gray;
            this.lblHuongDan.Location = new System.Drawing.Point(22, 19);
            this.lblHuongDan.Name = "lblHuongDan";
            this.lblHuongDan.Size = new System.Drawing.Size(251, 30);
            this.lblHuongDan.TabIndex = 0;
            this.lblHuongDan.Text = "Sắp xếp câu trả lời tại đây:";
            // 
            // flpSource
            // 
            this.flpSource.BackColor = System.Drawing.Color.White;
            this.flpSource.Location = new System.Drawing.Point(158, 500);
            this.flpSource.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.flpSource.Name = "flpSource";
            this.flpSource.Size = new System.Drawing.Size(1350, 250);
            this.flpSource.TabIndex = 2;
            // 
            // btnKiemTra
            // 
            this.btnKiemTra.BorderRadius = 20;
            this.btnKiemTra.FillColor = System.Drawing.Color.MidnightBlue;
            this.btnKiemTra.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKiemTra.ForeColor = System.Drawing.Color.White;
            this.btnKiemTra.Location = new System.Drawing.Point(608, 850);
            this.btnKiemTra.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnKiemTra.Name = "btnKiemTra";
            this.btnKiemTra.Size = new System.Drawing.Size(225, 75);
            this.btnKiemTra.TabIndex = 3;
            this.btnKiemTra.Text = "KIỂM TRA";
            this.btnKiemTra.Click += new System.EventHandler(this.btnKiemTra_Click);
            // 
            // btnTiepTuc
            // 
            this.btnTiepTuc.BorderColor = System.Drawing.Color.MidnightBlue;
            this.btnTiepTuc.BorderRadius = 20;
            this.btnTiepTuc.BorderThickness = 2;
            this.btnTiepTuc.FillColor = System.Drawing.Color.White;
            this.btnTiepTuc.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTiepTuc.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnTiepTuc.Location = new System.Drawing.Point(855, 850);
            this.btnTiepTuc.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnTiepTuc.Name = "btnTiepTuc";
            this.btnTiepTuc.PressedColor = System.Drawing.Color.AliceBlue;
            this.btnTiepTuc.Size = new System.Drawing.Size(225, 75);
            this.btnTiepTuc.TabIndex = 4;
            this.btnTiepTuc.Text = "TIẾP TỤC";
            this.btnTiepTuc.Visible = false;
            this.btnTiepTuc.Click += new System.EventHandler(this.btnTiepTuc_Click);
            // 
            // lblThongBao
            // 
            this.lblThongBao.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThongBao.ForeColor = System.Drawing.Color.Gray;
            this.lblThongBao.Location = new System.Drawing.Point(0, 775);
            this.lblThongBao.Name = "lblThongBao";
            this.lblThongBao.Size = new System.Drawing.Size(1667, 38);
            this.lblThongBao.TabIndex = 5;
            this.lblThongBao.Text = "Hãy sắp xếp các từ bên trên thành câu hoàn chỉnh";
            this.lblThongBao.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmChoiGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1667, 968);
            this.Controls.Add(this.lblThongBao);
            this.Controls.Add(this.btnTiepTuc);
            this.Controls.Add(this.btnKiemTra);
            this.Controls.Add(this.flpSource);
            this.Controls.Add(this.pnlTargetContainer);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmChoiGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Play Game";
            this.Load += new System.EventHandler(this.frmChoiGame_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlTargetContainer.ResumeLayout(false);
            this.pnlTargetContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2Panel pnlHeader;
        private System.Windows.Forms.Label lblTenGame;
        private Guna.UI2.WinForms.Guna2ControlBox btnClose;
        private System.Windows.Forms.Label lblCauSo;
        private Guna.UI2.WinForms.Guna2Panel pnlTargetContainer;
        private System.Windows.Forms.Label lblHuongDan;
        private System.Windows.Forms.FlowLayoutPanel flpTarget;
        private System.Windows.Forms.FlowLayoutPanel flpSource;
        private Guna.UI2.WinForms.Guna2Button btnKiemTra;
        private Guna.UI2.WinForms.Guna2Button btnTiepTuc;
        private System.Windows.Forms.Label lblThongBao;
    }
}