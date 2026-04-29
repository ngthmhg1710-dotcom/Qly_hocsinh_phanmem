// File: Form1.Designer.cs
namespace FlashcardFlipGame
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.guna2ShadowForm1 = new Guna.UI2.WinForms.Guna2ShadowForm(this.components);
            this.navigationPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.lblCardCount = new System.Windows.Forms.Label();
            this.btnPrevious = new Guna.UI2.WinForms.Guna2ImageButton();
            this.btnNext = new Guna.UI2.WinForms.Guna2ImageButton();
            this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.picSfxToggle = new Guna.UI2.WinForms.Guna2PictureBox();
            this.picMusicToggle = new Guna.UI2.WinForms.Guna2PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.flashcardControl1 = new FlashcardFlipGame.FlashcardControl();
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.navigationPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSfxToggle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMusicToggle)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2ShadowForm1
            // 
            this.guna2ShadowForm1.BorderRadius = 25;
            this.guna2ShadowForm1.ShadowColor = System.Drawing.Color.Gainsboro;
            this.guna2ShadowForm1.TargetForm = this;
            // 
            // navigationPanel
            // 
            this.navigationPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.navigationPanel.BackColor = System.Drawing.Color.Transparent;
            this.navigationPanel.BorderRadius = 25;
            this.navigationPanel.Controls.Add(this.lblCardCount);
            this.navigationPanel.Controls.Add(this.btnPrevious);
            this.navigationPanel.Controls.Add(this.btnNext);
            this.navigationPanel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(158)))), ((int)(((byte)(171)))));
            this.navigationPanel.Location = new System.Drawing.Point(263, 115);
            this.navigationPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.navigationPanel.Name = "navigationPanel";
            this.navigationPanel.ShadowDecoration.BorderRadius = 25;
            this.navigationPanel.ShadowDecoration.Enabled = true;
            this.navigationPanel.ShadowDecoration.Shadow = new System.Windows.Forms.Padding(0, 0, 5, 5);
            this.navigationPanel.Size = new System.Drawing.Size(1048, 808);
            this.navigationPanel.TabIndex = 0;
            // 
            // lblCardCount
            // 
            this.lblCardCount.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblCardCount.AutoSize = true;
            this.lblCardCount.BackColor = System.Drawing.Color.Transparent;
            this.lblCardCount.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCardCount.ForeColor = System.Drawing.Color.White;
            this.lblCardCount.Location = new System.Drawing.Point(482, 748);
            this.lblCardCount.Name = "lblCardCount";
            this.lblCardCount.Size = new System.Drawing.Size(93, 38);
            this.lblCardCount.TabIndex = 2;
            this.lblCardCount.Text = "1 / 10";
            this.lblCardCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnPrevious
            // 
            this.btnPrevious.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnPrevious.BackColor = System.Drawing.Color.Transparent;
            this.btnPrevious.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnPrevious.HoverState.ImageSize = new System.Drawing.Size(35, 35);
            this.btnPrevious.Image = ((System.Drawing.Image)(resources.GetObject("btnPrevious.Image")));
            this.btnPrevious.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnPrevious.ImageRotate = 0F;
            this.btnPrevious.ImageSize = new System.Drawing.Size(32, 32);
            this.btnPrevious.Location = new System.Drawing.Point(387, 744);
            this.btnPrevious.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.PressedState.ImageSize = new System.Drawing.Size(30, 30);
            this.btnPrevious.Size = new System.Drawing.Size(62, 56);
            this.btnPrevious.TabIndex = 0;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnNext.BackColor = System.Drawing.Color.Transparent;
            this.btnNext.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
            this.btnNext.HoverState.ImageSize = new System.Drawing.Size(35, 35);
            this.btnNext.Image = ((System.Drawing.Image)(resources.GetObject("btnNext.Image")));
            this.btnNext.ImageOffset = new System.Drawing.Point(0, 0);
            this.btnNext.ImageRotate = 0F;
            this.btnNext.ImageSize = new System.Drawing.Size(32, 32);
            this.btnNext.Location = new System.Drawing.Point(602, 744);
            this.btnNext.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnNext.Name = "btnNext";
            this.btnNext.PressedState.ImageSize = new System.Drawing.Size(30, 30);
            this.btnNext.Size = new System.Drawing.Size(71, 52);
            this.btnNext.TabIndex = 1;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // guna2ControlBox1
            // 
            this.guna2ControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox1.FillColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox1.IconColor = System.Drawing.Color.Gray;
            this.guna2ControlBox1.Location = new System.Drawing.Point(1485, 15);
            this.guna2ControlBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.guna2ControlBox1.Name = "guna2ControlBox1";
            this.guna2ControlBox1.Size = new System.Drawing.Size(51, 36);
            this.guna2ControlBox1.TabIndex = 14;
            // 
            // picSfxToggle
            // 
            this.picSfxToggle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picSfxToggle.BackColor = System.Drawing.Color.Transparent;
            this.picSfxToggle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picSfxToggle.Image = global::GUI.Properties.Resources.sfx_on;
            this.picSfxToggle.ImageRotate = 0F;
            this.picSfxToggle.Location = new System.Drawing.Point(14, 138);
            this.picSfxToggle.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.picSfxToggle.Name = "picSfxToggle";
            this.picSfxToggle.Size = new System.Drawing.Size(63, 46);
            this.picSfxToggle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picSfxToggle.TabIndex = 16;
            this.picSfxToggle.TabStop = false;
            this.picSfxToggle.Click += new System.EventHandler(this.picSfxToggle_Click);
            // 
            // picMusicToggle
            // 
            this.picMusicToggle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picMusicToggle.BackColor = System.Drawing.Color.Transparent;
            this.picMusicToggle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picMusicToggle.Image = global::GUI.Properties.Resources.music_on;
            this.picMusicToggle.ImageRotate = 0F;
            this.picMusicToggle.Location = new System.Drawing.Point(14, 36);
            this.picMusicToggle.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.picMusicToggle.Name = "picMusicToggle";
            this.picMusicToggle.Size = new System.Drawing.Size(53, 46);
            this.picMusicToggle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picMusicToggle.TabIndex = 15;
            this.picMusicToggle.TabStop = false;
            this.picMusicToggle.Click += new System.EventHandler(this.picMusicToggle_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label1.Location = new System.Drawing.Point(14, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 21);
            this.label1.TabIndex = 19;
            this.label1.Text = "Nhạc nền";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label2.Location = new System.Drawing.Point(14, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 21);
            this.label2.TabIndex = 20;
            this.label2.Text = "Âm thanh";
            // 
            // flashcardControl1
            // 
            this.flashcardControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flashcardControl1.BackColor = System.Drawing.Color.Transparent;
            this.flashcardControl1.BackFaceColor = System.Drawing.Color.White;
            this.flashcardControl1.BackText = "Answer";
            this.flashcardControl1.BackTextColor = System.Drawing.Color.MidnightBlue;
            this.flashcardControl1.CornerRadius = 25;
            this.flashcardControl1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.flashcardControl1.Font = new System.Drawing.Font("Segoe UI", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flashcardControl1.ForeColor = System.Drawing.Color.White;
            this.flashcardControl1.FrontFaceColor = System.Drawing.Color.MidnightBlue;
            this.flashcardControl1.FrontText = "Question";
            this.flashcardControl1.Location = new System.Drawing.Point(201, 158);
            this.flashcardControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.flashcardControl1.Name = "flashcardControl1";
            this.flashcardControl1.Size = new System.Drawing.Size(1163, 686);
            this.flashcardControl1.TabIndex = 1;
            this.flashcardControl1.Text = "flashcardControl1";
            // 
            // guna2BorderlessForm1
            // 
            this.guna2BorderlessForm1.BorderRadius = 15;
            this.guna2BorderlessForm1.ContainerControl = this;
            this.guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1565, 968);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.flashcardControl1);
            this.Controls.Add(this.navigationPanel);
            this.Controls.Add(this.picSfxToggle);
            this.Controls.Add(this.picMusicToggle);
            this.Controls.Add(this.guna2ControlBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(623, 479);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Flashcard Song Ngữ (Lật 3D)";
            this.navigationPanel.ResumeLayout(false);
            this.navigationPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSfxToggle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMusicToggle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FlashcardControl flashcardControl1;
        private Guna.UI2.WinForms.Guna2ShadowForm guna2ShadowForm1;
        private Guna.UI2.WinForms.Guna2ImageButton btnPrevious;
        private Guna.UI2.WinForms.Guna2ImageButton btnNext;
        private System.Windows.Forms.Label lblCardCount;
        private Guna.UI2.WinForms.Guna2Panel navigationPanel;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
        private Guna.UI2.WinForms.Guna2PictureBox picMusicToggle;
        private Guna.UI2.WinForms.Guna2PictureBox picSfxToggle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
    }
}