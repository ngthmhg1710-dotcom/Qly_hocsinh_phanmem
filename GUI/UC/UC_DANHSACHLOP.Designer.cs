namespace GUI
{
    partial class UC_DANHSACHLOP
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
            this.flpDanhSachLop = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // flpDanhSachLop
            // 
            this.flpDanhSachLop.AutoScroll = true;
            this.flpDanhSachLop.BackColor = System.Drawing.SystemColors.Control;
            this.flpDanhSachLop.Location = new System.Drawing.Point(39, 79);
            this.flpDanhSachLop.Name = "flpDanhSachLop";
            this.flpDanhSachLop.Size = new System.Drawing.Size(1403, 642);
            this.flpDanhSachLop.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(25)))), ((int)(((byte)(101)))));
            this.label1.Location = new System.Drawing.Point(32, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(228, 38);
            this.label1.TabIndex = 2;
            this.label1.Text = "DANH SÁCH LỚP";
            // 
            // UC_DANHSACHLOP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.flpDanhSachLop);
            this.Controls.Add(this.label1);
            this.Name = "UC_DANHSACHLOP";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Size = new System.Drawing.Size(1524, 753);
            this.Load += new System.EventHandler(this.UC_DANHSACHLOP_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flpDanhSachLop;
        private System.Windows.Forms.Label label1;
    }
}
