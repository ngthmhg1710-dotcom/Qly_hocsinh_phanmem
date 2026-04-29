namespace RANDOMSO
{
    partial class GameForm
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.msgDialogGameOver = new Guna.UI2.WinForms.Guna2MessageDialog();
            this.pbWheel = new System.Windows.Forms.PictureBox();
            this.btnSpin = new Guna.UI2.WinForms.Guna2Button();
            this.pnlQuestionArea = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlCurrentQuestion = new Guna.UI2.WinForms.Guna2Panel();
            this.tblQuestionLayout = new System.Windows.Forms.TableLayoutPanel();
            this.picCurrentQuestionImage = new Guna.UI2.WinForms.Guna2PictureBox();
            this.lblQuestionDisplay = new System.Windows.Forms.Label();
            this.btnIncorrect = new Guna.UI2.WinForms.Guna2Button();
            this.btnCorrect = new Guna.UI2.WinForms.Guna2Button();
            this.lblCurrentQuestionTitle = new System.Windows.Forms.Label();
            this.lblTitleRight = new Guna.UI2.WinForms.Guna2Button();
            this.lblPointer = new System.Windows.Forms.Label();
            this.lblScore = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbWheel)).BeginInit();
            this.pnlQuestionArea.SuspendLayout();
            this.pnlCurrentQuestion.SuspendLayout();
            this.tblQuestionLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCurrentQuestionImage)).BeginInit();
            this.SuspendLayout();
            // 
            // msgDialogGameOver
            // 
            this.msgDialogGameOver.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
            this.msgDialogGameOver.Caption = null;
            this.msgDialogGameOver.Icon = Guna.UI2.WinForms.MessageDialogIcon.None;
            this.msgDialogGameOver.Parent = this;
            this.msgDialogGameOver.Style = Guna.UI2.WinForms.MessageDialogStyle.Default;
            this.msgDialogGameOver.Text = null;
            // 
            // pbWheel
            // 
            this.pbWheel.Location = new System.Drawing.Point(45, 150);
            this.pbWheel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pbWheel.Name = "pbWheel";
            this.pbWheel.Size = new System.Drawing.Size(506, 562);
            this.pbWheel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbWheel.TabIndex = 0;
            this.pbWheel.TabStop = false;
            // 
            // btnSpin
            // 
            this.btnSpin.BorderRadius = 25;
            this.btnSpin.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(25)))), ((int)(((byte)(101)))));
            this.btnSpin.Font = new System.Drawing.Font("Montserrat", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnSpin.ForeColor = System.Drawing.Color.White;
            this.btnSpin.Location = new System.Drawing.Point(197, 744);
            this.btnSpin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSpin.Name = "btnSpin";
            this.btnSpin.Size = new System.Drawing.Size(202, 75);
            this.btnSpin.TabIndex = 1;
            this.btnSpin.Text = "Quay";
            this.btnSpin.Click += new System.EventHandler(this.btnSpin_Click);
            // 
            // pnlQuestionArea
            // 
            this.pnlQuestionArea.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlQuestionArea.BackColor = System.Drawing.Color.Transparent;
            this.pnlQuestionArea.BorderRadius = 20;
            this.pnlQuestionArea.Controls.Add(this.pnlCurrentQuestion);
            this.pnlQuestionArea.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(232)))), ((int)(((byte)(249)))));
            this.pnlQuestionArea.Location = new System.Drawing.Point(596, 100);
            this.pnlQuestionArea.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlQuestionArea.Name = "pnlQuestionArea";
            this.pnlQuestionArea.Padding = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.pnlQuestionArea.Size = new System.Drawing.Size(932, 803);
            this.pnlQuestionArea.TabIndex = 2;
            // 
            // pnlCurrentQuestion
            // 
            this.pnlCurrentQuestion.BackColor = System.Drawing.Color.Transparent;
            this.pnlCurrentQuestion.Controls.Add(this.tblQuestionLayout);
            this.pnlCurrentQuestion.Controls.Add(this.btnIncorrect);
            this.pnlCurrentQuestion.Controls.Add(this.btnCorrect);
            this.pnlCurrentQuestion.Controls.Add(this.lblCurrentQuestionTitle);
            this.pnlCurrentQuestion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCurrentQuestion.Location = new System.Drawing.Point(11, 12);
            this.pnlCurrentQuestion.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlCurrentQuestion.Name = "pnlCurrentQuestion";
            this.pnlCurrentQuestion.Size = new System.Drawing.Size(910, 779);
            this.pnlCurrentQuestion.TabIndex = 0;
            // 
            // tblQuestionLayout
            // 
            this.tblQuestionLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tblQuestionLayout.BackColor = System.Drawing.Color.Transparent;
            this.tblQuestionLayout.ColumnCount = 1;
            this.tblQuestionLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblQuestionLayout.Controls.Add(this.picCurrentQuestionImage, 0, 0);
            this.tblQuestionLayout.Controls.Add(this.lblQuestionDisplay, 0, 1);
            this.tblQuestionLayout.Location = new System.Drawing.Point(24, 70);
            this.tblQuestionLayout.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tblQuestionLayout.Name = "tblQuestionLayout";
            this.tblQuestionLayout.RowCount = 2;
            this.tblQuestionLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblQuestionLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblQuestionLayout.Size = new System.Drawing.Size(863, 617);
            this.tblQuestionLayout.TabIndex = 5;
            // 
            // picCurrentQuestionImage
            // 
            this.picCurrentQuestionImage.BorderRadius = 15;
            this.picCurrentQuestionImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picCurrentQuestionImage.ImageRotate = 0F;
            this.picCurrentQuestionImage.Location = new System.Drawing.Point(3, 4);
            this.picCurrentQuestionImage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.picCurrentQuestionImage.Name = "picCurrentQuestionImage";
            this.picCurrentQuestionImage.Size = new System.Drawing.Size(857, 300);
            this.picCurrentQuestionImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCurrentQuestionImage.TabIndex = 2;
            this.picCurrentQuestionImage.TabStop = false;
            // 
            // lblQuestionDisplay
            // 
            this.lblQuestionDisplay.BackColor = System.Drawing.Color.Transparent;
            this.lblQuestionDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblQuestionDisplay.Font = new System.Drawing.Font("Montserrat", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblQuestionDisplay.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblQuestionDisplay.Location = new System.Drawing.Point(3, 308);
            this.lblQuestionDisplay.Name = "lblQuestionDisplay";
            this.lblQuestionDisplay.Padding = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.lblQuestionDisplay.Size = new System.Drawing.Size(857, 309);
            this.lblQuestionDisplay.TabIndex = 3;
            this.lblQuestionDisplay.Text = "Nội dung câu hỏi";
            this.lblQuestionDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnIncorrect
            // 
            this.btnIncorrect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIncorrect.BorderRadius = 15;
            this.btnIncorrect.FillColor = System.Drawing.Color.Maroon;
            this.btnIncorrect.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnIncorrect.ForeColor = System.Drawing.Color.White;
            this.btnIncorrect.Location = new System.Drawing.Point(668, 707);
            this.btnIncorrect.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnIncorrect.Name = "btnIncorrect";
            this.btnIncorrect.Size = new System.Drawing.Size(202, 56);
            this.btnIncorrect.TabIndex = 4;
            this.btnIncorrect.Text = "Sai";
            this.btnIncorrect.Click += new System.EventHandler(this.btnIncorrect_Click);
            // 
            // btnCorrect
            // 
            this.btnCorrect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCorrect.BorderRadius = 15;
            this.btnCorrect.FillColor = System.Drawing.Color.ForestGreen;
            this.btnCorrect.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnCorrect.ForeColor = System.Drawing.Color.White;
            this.btnCorrect.Location = new System.Drawing.Point(39, 707);
            this.btnCorrect.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCorrect.Name = "btnCorrect";
            this.btnCorrect.Size = new System.Drawing.Size(202, 56);
            this.btnCorrect.TabIndex = 3;
            this.btnCorrect.Text = "Đúng";
            this.btnCorrect.Click += new System.EventHandler(this.btnCorrect_Click);
            // 
            // lblCurrentQuestionTitle
            // 
            this.lblCurrentQuestionTitle.AutoSize = true;
            this.lblCurrentQuestionTitle.Font = new System.Drawing.Font("Montserrat", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblCurrentQuestionTitle.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblCurrentQuestionTitle.Location = new System.Drawing.Point(16, 14);
            this.lblCurrentQuestionTitle.Name = "lblCurrentQuestionTitle";
            this.lblCurrentQuestionTitle.Size = new System.Drawing.Size(158, 49);
            this.lblCurrentQuestionTitle.TabIndex = 0;
            this.lblCurrentQuestionTitle.Text = "Câu hỏi ";
            // 
            // lblTitleRight
            // 
            this.lblTitleRight.BorderRadius = 18;
            this.lblTitleRight.Enabled = false;
            this.lblTitleRight.FillColor = System.Drawing.Color.Gray;
            this.lblTitleRight.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTitleRight.ForeColor = System.Drawing.Color.White;
            this.lblTitleRight.Location = new System.Drawing.Point(596, 38);
            this.lblTitleRight.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblTitleRight.Name = "lblTitleRight";
            this.lblTitleRight.Size = new System.Drawing.Size(394, 56);
            this.lblTitleRight.TabIndex = 4;
            this.lblTitleRight.Text = "Nội dung câu hỏi";
            // 
            // lblPointer
            // 
            this.lblPointer.AutoSize = true;
            this.lblPointer.BackColor = System.Drawing.Color.Transparent;
            this.lblPointer.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold);
            this.lblPointer.ForeColor = System.Drawing.Color.Red;
            this.lblPointer.Location = new System.Drawing.Point(267, 100);
            this.lblPointer.Name = "lblPointer";
            this.lblPointer.Size = new System.Drawing.Size(59, 55);
            this.lblPointer.TabIndex = 5;
            this.lblPointer.Text = "▼";
            // 
            // lblScore
            // 
            this.lblScore.BackColor = System.Drawing.Color.Transparent;
            this.lblScore.Font = new System.Drawing.Font("Montserrat", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblScore.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblScore.Location = new System.Drawing.Point(14, 15);
            this.lblScore.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(102, 51);
            this.lblScore.TabIndex = 7;
            this.lblScore.Text = "Điểm: ";
            this.lblScore.Click += new System.EventHandler(this.lblScore_Click_2);
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
            this.guna2ControlBox1.Location = new System.Drawing.Point(1505, 15);
            this.guna2ControlBox1.Name = "guna2ControlBox1";
            this.guna2ControlBox1.Size = new System.Drawing.Size(45, 29);
            this.guna2ControlBox1.TabIndex = 8;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1562, 954);
            this.Controls.Add(this.guna2ControlBox1);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.lblPointer);
            this.Controls.Add(this.lblTitleRight);
            this.Controls.Add(this.pnlQuestionArea);
            this.Controls.Add(this.btnSpin);
            this.Controls.Add(this.pbWheel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(1562, 954);
            this.Name = "GameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vòng Quay Game";
            this.Load += new System.EventHandler(this.GameForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbWheel)).EndInit();
            this.pnlQuestionArea.ResumeLayout(false);
            this.pnlCurrentQuestion.ResumeLayout(false);
            this.pnlCurrentQuestion.PerformLayout();
            this.tblQuestionLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picCurrentQuestionImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbWheel;
        private Guna.UI2.WinForms.Guna2Button btnSpin;
        private Guna.UI2.WinForms.Guna2Panel pnlQuestionArea;
        private Guna.UI2.WinForms.Guna2Button lblTitleRight;
        private System.Windows.Forms.Label lblPointer;
        private Guna.UI2.WinForms.Guna2Panel pnlCurrentQuestion;
        private Guna.UI2.WinForms.Guna2Button btnIncorrect;
        private Guna.UI2.WinForms.Guna2Button btnCorrect;
        private System.Windows.Forms.Label lblCurrentQuestionTitle;
        private System.Windows.Forms.TableLayoutPanel tblQuestionLayout;
        private Guna.UI2.WinForms.Guna2PictureBox picCurrentQuestionImage;
        private System.Windows.Forms.Label lblQuestionDisplay;
        // =========================================================
        // KHAI BÁO BIẾN CHO GUNA2MESSAGEDIALOG
        // =========================================================
        private Guna.UI2.WinForms.Guna2MessageDialog msgDialogGameOver;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblScore;
        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
    }
}