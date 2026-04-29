// File: Form1.Designer.cs
namespace RANDOMSO
{
    partial class VONGQUAYMAYMAN
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.FlowLayoutPanel pnlContainer;
        private Guna.UI2.WinForms.Guna2Button THEMTHE;
        private Guna.UI2.WinForms.Guna2Button TAO;

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
            this.pnlContainer.BackColor = System.Drawing.Color.White;
            this.pnlContainer.Location = new System.Drawing.Point(14, 100);
            this.pnlContainer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Padding = new System.Windows.Forms.Padding(11, 12, 11, 25);
            this.pnlContainer.Size = new System.Drawing.Size(1535, 839);
            this.pnlContainer.TabIndex = 0;
            // 
            // THEMTHE
            // 
            this.THEMTHE.BorderRadius = 18;
            this.THEMTHE.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.THEMTHE.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.THEMTHE.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.THEMTHE.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.THEMTHE.FillColor = System.Drawing.Color.MidnightBlue;
            this.THEMTHE.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.THEMTHE.ForeColor = System.Drawing.Color.White;
            this.THEMTHE.Location = new System.Drawing.Point(25, 15);
            this.THEMTHE.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.THEMTHE.Name = "THEMTHE";
            this.THEMTHE.Size = new System.Drawing.Size(184, 65);
            this.THEMTHE.TabIndex = 1;
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
            this.TAO.FillColor = System.Drawing.Color.White;
            this.TAO.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TAO.ForeColor = System.Drawing.Color.MidnightBlue;
            this.TAO.Location = new System.Drawing.Point(216, 15);
            this.TAO.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TAO.Name = "TAO";
            this.TAO.Size = new System.Drawing.Size(177, 65);
            this.TAO.TabIndex = 2;
            this.TAO.Text = "TẠO GAME";
            this.TAO.Click += new System.EventHandler(this.TAO_Click);
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
            this.guna2ControlBox1.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(25)))), ((int)(((byte)(101)))));
            this.guna2ControlBox1.Location = new System.Drawing.Point(1504, 12);
            this.guna2ControlBox1.Name = "guna2ControlBox1";
            this.guna2ControlBox1.Size = new System.Drawing.Size(45, 29);
            this.guna2ControlBox1.TabIndex = 9;
            // 
            // VONGQUAYMAYMAN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1562, 954);
            this.Controls.Add(this.guna2ControlBox1);
            this.Controls.Add(this.TAO);
            this.Controls.Add(this.THEMTHE);
            this.Controls.Add(this.pnlContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(1562, 954);
            this.Name = "VONGQUAYMAYMAN";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tạo câu hỏi";
            this.ResumeLayout(false);

        }

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
    }
}