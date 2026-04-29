// DayCell.Designer.cs
namespace Calendar // Đã đổi tên
{
    partial class DayCell
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

        private void InitializeComponent()
        {
            this.lblDayNumber = new System.Windows.Forms.Label();
            this.flpEvents = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // ... (Phần còn lại của file designer giống hệt như trước) ...
            this.lblDayNumber.AutoSize = true;
            this.lblDayNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDayNumber.Location = new System.Drawing.Point(3, 0);
            this.lblDayNumber.Name = "lblDayNumber";
            this.lblDayNumber.Size = new System.Drawing.Size(29, 20);
            this.lblDayNumber.TabIndex = 0;
            this.lblDayNumber.Text = "00";
            // 
            // flpEvents
            // 
            this.flpEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpEvents.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpEvents.Location = new System.Drawing.Point(3, 23);
            this.flpEvents.Name = "flpEvents";
            this.flpEvents.Size = new System.Drawing.Size(114, 64);
            this.flpEvents.TabIndex = 1;
            // 
            // DayCell
            // 
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.flpEvents);
            this.Controls.Add(this.lblDayNumber);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "DayCell";
            this.Size = new System.Drawing.Size(120, 90);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblDayNumber;
        private System.Windows.Forms.FlowLayoutPanel flpEvents;
    }
}