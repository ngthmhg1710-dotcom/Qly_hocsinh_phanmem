// File: CreateDeckForm.Designer.cs (Phiên bản đã thêm nút Lịch sử)
namespace FlashcardFlipGame
{
    partial class CreateDeckForm
    {
        private System.ComponentModel.IContainer components = null;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
        private Guna.UI2.WinForms.Guna2TextBox txtFront;
        private Guna.UI2.WinForms.Guna2TextBox txtBack;
        private Guna.UI2.WinForms.Guna2Button btnAddCard;
        private Guna.UI2.WinForms.Guna2Button btnStartGame;
        private System.Windows.Forms.FlowLayoutPanel pnlAddedCards;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2Button btnBrowseFront;
        private Guna.UI2.WinForms.Guna2Button btnBrowseBack;
        private System.Windows.Forms.Label lblFrontImagePath;
        private System.Windows.Forms.Label lblBackImagePath;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel3;
        private Guna.UI2.WinForms.Guna2TextBox txtDeckName;
        private System.Windows.Forms.Label lblDeckName;
        private Guna.UI2.WinForms.Guna2ShadowForm guna2ShadowForm1;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2Button btnHistory; // <<< NÚT MỚI ĐƯỢC KHAI BÁO

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
            this.components = new System.ComponentModel.Container();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlAddedCards = new System.Windows.Forms.FlowLayoutPanel();
            this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.txtFront = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtBack = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnAddCard = new Guna.UI2.WinForms.Guna2Button();
            this.btnStartGame = new Guna.UI2.WinForms.Guna2Button();
            this.btnBrowseFront = new Guna.UI2.WinForms.Guna2Button();
            this.btnBrowseBack = new Guna.UI2.WinForms.Guna2Button();
            this.lblFrontImagePath = new System.Windows.Forms.Label();
            this.lblBackImagePath = new System.Windows.Forms.Label();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.btnHistory = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Panel3 = new Guna.UI2.WinForms.Guna2Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDeckName = new System.Windows.Forms.Label();
            this.txtDeckName = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2ShadowForm1 = new Guna.UI2.WinForms.Guna2ShadowForm(this.components);
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.guna2Panel1.SuspendLayout();
            this.guna2Panel2.SuspendLayout();
            this.guna2Panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(36, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(278, 31);
            this.label2.TabIndex = 4;
            this.label2.Text = "Nhập Nội Dung Mặt Trước";
            // 
            // pnlAddedCards
            // 
            this.pnlAddedCards.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlAddedCards.AutoScroll = true;
            this.pnlAddedCards.BackColor = System.Drawing.Color.White;
            this.pnlAddedCards.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlAddedCards.Location = new System.Drawing.Point(53, 90);
            this.pnlAddedCards.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlAddedCards.Name = "pnlAddedCards";
            this.pnlAddedCards.Padding = new System.Windows.Forms.Padding(6);
            this.pnlAddedCards.Size = new System.Drawing.Size(701, 646);
            this.pnlAddedCards.TabIndex = 0;
            this.pnlAddedCards.WrapContents = false;
            // 
            // guna2ControlBox1
            // 
            this.guna2ControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox1.FillColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox1.IconColor = System.Drawing.Color.MidnightBlue;
            this.guna2ControlBox1.Location = new System.Drawing.Point(1603, 8);
            this.guna2ControlBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.guna2ControlBox1.Name = "guna2ControlBox1";
            this.guna2ControlBox1.Size = new System.Drawing.Size(51, 36);
            this.guna2ControlBox1.TabIndex = 13;
            // 
            // txtFront
            // 
            this.txtFront.BorderRadius = 8;
            this.txtFront.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtFront.DefaultText = "";
            this.txtFront.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtFront.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtFront.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtFront.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtFront.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtFront.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtFront.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtFront.Location = new System.Drawing.Point(30, 94);
            this.txtFront.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtFront.Name = "txtFront";
            this.txtFront.PlaceholderText = "Nhập văn bản...";
            this.txtFront.SelectedText = "";
            this.txtFront.Size = new System.Drawing.Size(411, 64);
            this.txtFront.TabIndex = 1;
            // 
            // txtBack
            // 
            this.txtBack.BorderRadius = 8;
            this.txtBack.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtBack.DefaultText = "";
            this.txtBack.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtBack.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtBack.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtBack.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtBack.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtBack.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtBack.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtBack.Location = new System.Drawing.Point(30, 269);
            this.txtBack.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtBack.Name = "txtBack";
            this.txtBack.PlaceholderText = "Nhập văn bản...";
            this.txtBack.SelectedText = "";
            this.txtBack.Size = new System.Drawing.Size(411, 66);
            this.txtBack.TabIndex = 5;
            this.txtBack.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBack_KeyDown);
            // 
            // btnAddCard
            // 
            this.btnAddCard.BackColor = System.Drawing.Color.Transparent;
            this.btnAddCard.BorderColor = System.Drawing.Color.White;
            this.btnAddCard.BorderRadius = 8;
            this.btnAddCard.BorderThickness = 2;
            this.btnAddCard.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAddCard.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAddCard.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAddCard.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAddCard.FillColor = System.Drawing.Color.MidnightBlue;
            this.btnAddCard.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnAddCard.ForeColor = System.Drawing.Color.White;
            this.btnAddCard.Location = new System.Drawing.Point(112, 450);
            this.btnAddCard.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAddCard.Name = "btnAddCard";
            this.btnAddCard.Size = new System.Drawing.Size(411, 62);
            this.btnAddCard.TabIndex = 9;
            this.btnAddCard.Text = "Thêm thẻ (+)";
            this.btnAddCard.Click += new System.EventHandler(this.btnAddCard_Click);
            // 
            // btnStartGame
            // 
            this.btnStartGame.BackColor = System.Drawing.Color.Transparent;
            this.btnStartGame.BorderColor = System.Drawing.Color.DimGray;
            this.btnStartGame.BorderRadius = 20;
            this.btnStartGame.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnStartGame.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnStartGame.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnStartGame.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnStartGame.FillColor = System.Drawing.Color.White;
            this.btnStartGame.Font = new System.Drawing.Font("Montserrat", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnStartGame.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnStartGame.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(115)))), ((int)(((byte)(71)))));
            this.btnStartGame.Location = new System.Drawing.Point(112, 585);
            this.btnStartGame.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnStartGame.Name = "btnStartGame";
            this.btnStartGame.PressedColor = System.Drawing.Color.White;
            this.btnStartGame.ShadowDecoration.BorderRadius = 12;
            this.btnStartGame.ShadowDecoration.Depth = 5;
            this.btnStartGame.ShadowDecoration.Enabled = true;
            this.btnStartGame.Size = new System.Drawing.Size(411, 64);
            this.btnStartGame.TabIndex = 10;
            this.btnStartGame.Text = "Bắt đầu học!";
            this.btnStartGame.Click += new System.EventHandler(this.btnStartGame_Click);
            // 
            // btnBrowseFront
            // 
            this.btnBrowseFront.BorderRadius = 8;
            this.btnBrowseFront.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnBrowseFront.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnBrowseFront.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnBrowseFront.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnBrowseFront.FillColor = System.Drawing.Color.White;
            this.btnBrowseFront.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnBrowseFront.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnBrowseFront.Location = new System.Drawing.Point(464, 90);
            this.btnBrowseFront.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnBrowseFront.Name = "btnBrowseFront";
            this.btnBrowseFront.Size = new System.Drawing.Size(199, 67);
            this.btnBrowseFront.TabIndex = 2;
            this.btnBrowseFront.Text = "Chọn ảnh...";
            this.btnBrowseFront.Click += new System.EventHandler(this.btnBrowseFront_Click);
            // 
            // btnBrowseBack
            // 
            this.btnBrowseBack.BorderRadius = 8;
            this.btnBrowseBack.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnBrowseBack.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnBrowseBack.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnBrowseBack.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnBrowseBack.FillColor = System.Drawing.Color.White;
            this.btnBrowseBack.Font = new System.Drawing.Font("Montserrat", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnBrowseBack.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnBrowseBack.Location = new System.Drawing.Point(464, 269);
            this.btnBrowseBack.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnBrowseBack.Name = "btnBrowseBack";
            this.btnBrowseBack.Size = new System.Drawing.Size(205, 66);
            this.btnBrowseBack.TabIndex = 6;
            this.btnBrowseBack.Text = "Chọn ảnh...";
            this.btnBrowseBack.Click += new System.EventHandler(this.btnBrowseBack_Click);
            // 
            // lblFrontImagePath
            // 
            this.lblFrontImagePath.AutoEllipsis = true;
            this.lblFrontImagePath.Font = new System.Drawing.Font("Montserrat", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrontImagePath.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblFrontImagePath.Location = new System.Drawing.Point(36, 162);
            this.lblFrontImagePath.Name = "lblFrontImagePath";
            this.lblFrontImagePath.Size = new System.Drawing.Size(366, 29);
            this.lblFrontImagePath.TabIndex = 3;
            this.lblFrontImagePath.Text = "Chưa có ảnh nào được chọn";
            // 
            // lblBackImagePath
            // 
            this.lblBackImagePath.AutoEllipsis = true;
            this.lblBackImagePath.Font = new System.Drawing.Font("Montserrat", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBackImagePath.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblBackImagePath.Location = new System.Drawing.Point(36, 344);
            this.lblBackImagePath.Name = "lblBackImagePath";
            this.lblBackImagePath.Size = new System.Drawing.Size(366, 29);
            this.lblBackImagePath.TabIndex = 7;
            this.lblBackImagePath.Text = "Chưa có ảnh nào được chọn";
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel1.BorderRadius = 30;
            this.guna2Panel1.Controls.Add(this.label3);
            this.guna2Panel1.Controls.Add(this.btnBrowseFront);
            this.guna2Panel1.Controls.Add(this.lblFrontImagePath);
            this.guna2Panel1.Controls.Add(this.lblBackImagePath);
            this.guna2Panel1.Controls.Add(this.btnBrowseBack);
            this.guna2Panel1.Controls.Add(this.txtBack);
            this.guna2Panel1.Controls.Add(this.txtFront);
            this.guna2Panel1.Controls.Add(this.btnStartGame);
            this.guna2Panel1.Controls.Add(this.btnAddCard);
            this.guna2Panel1.Controls.Add(this.label2);
            this.guna2Panel1.FillColor = System.Drawing.Color.MidnightBlue;
            this.guna2Panel1.ForeColor = System.Drawing.Color.Transparent;
            this.guna2Panel1.Location = new System.Drawing.Point(124, 142);
            this.guna2Panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(690, 762);
            this.guna2Panel1.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(36, 222);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(258, 31);
            this.label3.TabIndex = 11;
            this.label3.Text = "Nhập Nội Dung Mặt Sau";
            // 
            // btnHistory
            // 
            this.btnHistory.BackColor = System.Drawing.Color.Transparent;
            this.btnHistory.BorderColor = System.Drawing.Color.DimGray;
            this.btnHistory.BorderRadius = 20;
            this.btnHistory.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnHistory.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnHistory.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnHistory.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnHistory.FillColor = System.Drawing.Color.MidnightBlue;
            this.btnHistory.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnHistory.ForeColor = System.Drawing.Color.White;
            this.btnHistory.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnHistory.Location = new System.Drawing.Point(1323, 48);
            this.btnHistory.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.PressedColor = System.Drawing.Color.White;
            this.btnHistory.ShadowDecoration.BorderRadius = 12;
            this.btnHistory.ShadowDecoration.Depth = 5;
            this.btnHistory.ShadowDecoration.Enabled = true;
            this.btnHistory.Size = new System.Drawing.Size(219, 69);
            this.btnHistory.TabIndex = 13;
            this.btnHistory.Text = "Xem Lịch Sử";
            this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel2.BorderColor = System.Drawing.Color.MidnightBlue;
            this.guna2Panel2.BorderRadius = 30;
            this.guna2Panel2.BorderThickness = 2;
            this.guna2Panel2.Controls.Add(this.guna2Panel3);
            this.guna2Panel2.Controls.Add(this.pnlAddedCards);
            this.guna2Panel2.FillColor = System.Drawing.Color.White;
            this.guna2Panel2.Location = new System.Drawing.Point(767, 142);
            this.guna2Panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(775, 762);
            this.guna2Panel2.TabIndex = 15;
            // 
            // guna2Panel3
            // 
            this.guna2Panel3.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel3.BorderRadius = 25;
            this.guna2Panel3.Controls.Add(this.label1);
            this.guna2Panel3.FillColor = System.Drawing.Color.Gainsboro;
            this.guna2Panel3.Location = new System.Drawing.Point(45, 4);
            this.guna2Panel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.guna2Panel3.Name = "guna2Panel3";
            this.guna2Panel3.Size = new System.Drawing.Size(727, 79);
            this.guna2Panel3.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Montserrat", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label1.Location = new System.Drawing.Point(191, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(453, 49);
            this.label1.TabIndex = 0;
            this.label1.Text = "Các Thẻ Flashcard Đã Tạo";
            // 
            // lblDeckName
            // 
            this.lblDeckName.AutoSize = true;
            this.lblDeckName.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeckName.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblDeckName.Location = new System.Drawing.Point(127, 66);
            this.lblDeckName.Name = "lblDeckName";
            this.lblDeckName.Size = new System.Drawing.Size(163, 38);
            this.lblDeckName.TabIndex = 16;
            this.lblDeckName.Text = "Tên bộ thẻ:";
            // 
            // txtDeckName
            // 
            this.txtDeckName.BorderRadius = 15;
            this.txtDeckName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDeckName.DefaultText = "";
            this.txtDeckName.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtDeckName.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtDeckName.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtDeckName.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtDeckName.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtDeckName.Font = new System.Drawing.Font("Montserrat", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.txtDeckName.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtDeckName.Location = new System.Drawing.Point(303, 60);
            this.txtDeckName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDeckName.Name = "txtDeckName";
            this.txtDeckName.PlaceholderText = "Ví dụ: Từ vựng tiếng Anh chủ đề Động vật";
            this.txtDeckName.SelectedText = "";
            this.txtDeckName.Size = new System.Drawing.Size(704, 56);
            this.txtDeckName.TabIndex = 17;
            // 
            // guna2ShadowForm1
            // 
            this.guna2ShadowForm1.BorderRadius = 20;
            this.guna2ShadowForm1.TargetForm = this;
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 30;
            this.guna2Elipse1.TargetControl = this.pnlAddedCards;
            // 
            // guna2BorderlessForm1
            // 
            this.guna2BorderlessForm1.BorderRadius = 15;
            this.guna2BorderlessForm1.ContainerControl = this;
            this.guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // CreateDeckForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1667, 968);
            this.Controls.Add(this.btnHistory);
            this.Controls.Add(this.txtDeckName);
            this.Controls.Add(this.lblDeckName);
            this.Controls.Add(this.guna2ControlBox1);
            this.Controls.Add(this.guna2Panel1);
            this.Controls.Add(this.guna2Panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "CreateDeckForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tạo bộ thẻ Flashcard";
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.guna2Panel2.ResumeLayout(false);
            this.guna2Panel3.ResumeLayout(false);
            this.guna2Panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
    }
}