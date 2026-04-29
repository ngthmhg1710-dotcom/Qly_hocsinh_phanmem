namespace Calendar
{
    partial class EventDetailsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EventDetailsForm));
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.guna2ShadowForm1 = new Guna.UI2.WinForms.Guna2ShadowForm(this.components);
            this.pnlMain = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlView = new Guna.UI2.WinForms.Guna2Panel();
            this.vsbView = new Guna.UI2.WinForms.Guna2VScrollBar();
            this.flpViewContent = new System.Windows.Forms.FlowLayoutPanel();
            this.lblTitleHeader = new System.Windows.Forms.Label();
            this.pnlTitleContainer = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlColorIndicator = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.lblDescriptionHeader = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.btnEdit = new Guna.UI2.WinForms.Guna2Button();
            this.btnClose = new Guna.UI2.WinForms.Guna2ControlBox();
            this.pnlEdit = new Guna.UI2.WinForms.Guna2Panel();
            this.chkRemindBeforeEdit = new Guna.UI2.WinForms.Guna2CheckBox();
            this.txtDescriptionEdit = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnColorPicker = new Guna.UI2.WinForms.Guna2Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpEndTime = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.dtpStartTime = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.btnCancel = new Guna.UI2.WinForms.Guna2Button();
            this.btnSave = new Guna.UI2.WinForms.Guna2Button();
            this.txtTitleEdit = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnDelete = new Guna.UI2.WinForms.Guna2Button();
            this.pnlMain.SuspendLayout();
            this.pnlView.SuspendLayout();
            this.flpViewContent.SuspendLayout();
            this.pnlTitleContainer.SuspendLayout();
            this.pnlEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2BorderlessForm1
            // 
            this.guna2BorderlessForm1.BorderRadius = 40;
            this.guna2BorderlessForm1.ContainerControl = this;
            this.guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // guna2ShadowForm1
            // 
            this.guna2ShadowForm1.TargetForm = this;
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(23)))), ((int)(((byte)(84)))));
            this.pnlMain.Controls.Add(this.pnlView);
            this.pnlMain.Controls.Add(this.btnEdit);
            this.pnlMain.Controls.Add(this.btnClose);
            this.pnlMain.Controls.Add(this.pnlEdit);
            this.pnlMain.Controls.Add(this.btnDelete);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(533, 450);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlView
            // 
            this.pnlView.Controls.Add(this.vsbView);
            this.pnlView.Controls.Add(this.flpViewContent);
            this.pnlView.Location = new System.Drawing.Point(12, 60);
            this.pnlView.Name = "pnlView";
            this.pnlView.Size = new System.Drawing.Size(509, 378);
            this.pnlView.TabIndex = 6;
            // 
            // vsbView
            // 
            this.vsbView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vsbView.BorderRadius = 4;
            this.vsbView.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.vsbView.HoverState.ThumbColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.vsbView.InUpdate = false;
            this.vsbView.LargeChange = 10;
            this.vsbView.Location = new System.Drawing.Point(495, 0);
            this.vsbView.Name = "vsbView";
            this.vsbView.PressedState.ThumbColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(110)))), ((int)(((byte)(220)))));
            this.vsbView.ScrollbarSize = 8;
            this.vsbView.Size = new System.Drawing.Size(8, 378);
            this.vsbView.TabIndex = 1;
            this.vsbView.ThumbColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.vsbView.ThumbStyle = Guna.UI2.WinForms.Enums.ThumbStyle.Inset;
            this.vsbView.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vsbView_Scroll);
            // 
            // flpViewContent
            // 
            this.flpViewContent.AutoSize = true;
            this.flpViewContent.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpViewContent.Controls.Add(this.lblTitleHeader);
            this.flpViewContent.Controls.Add(this.pnlTitleContainer);
            this.flpViewContent.Controls.Add(this.lblDateTime);
            this.flpViewContent.Controls.Add(this.lblDescriptionHeader);
            this.flpViewContent.Controls.Add(this.lblDescription);
            this.flpViewContent.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpViewContent.Location = new System.Drawing.Point(0, 0);
            this.flpViewContent.MaximumSize = new System.Drawing.Size(485, 0);
            this.flpViewContent.MinimumSize = new System.Drawing.Size(485, 0);
            this.flpViewContent.Name = "flpViewContent";
            this.flpViewContent.Size = new System.Drawing.Size(485, 235);
            this.flpViewContent.TabIndex = 0;
            this.flpViewContent.WrapContents = false;
            // 
            // lblTitleHeader
            // 
            this.lblTitleHeader.AutoSize = true;
            this.lblTitleHeader.Font = new System.Drawing.Font("Montserrat", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTitleHeader.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblTitleHeader.Location = new System.Drawing.Point(3, 15);
            this.lblTitleHeader.Margin = new System.Windows.Forms.Padding(3, 15, 3, 0);
            this.lblTitleHeader.Name = "lblTitleHeader";
            this.lblTitleHeader.Padding = new System.Windows.Forms.Padding(45, 0, 0, 0);
            this.lblTitleHeader.Size = new System.Drawing.Size(151, 34);
            this.lblTitleHeader.TabIndex = 5;
            this.lblTitleHeader.Text = "Tiêu đề:";
            // 
            // pnlTitleContainer
            // 
            this.pnlTitleContainer.AutoSize = true;
            this.pnlTitleContainer.Controls.Add(this.pnlColorIndicator);
            this.pnlTitleContainer.Controls.Add(this.lblTitle);
            this.pnlTitleContainer.Location = new System.Drawing.Point(0, 49);
            this.pnlTitleContainer.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTitleContainer.Name = "pnlTitleContainer";
            this.pnlTitleContainer.Size = new System.Drawing.Size(120, 38);
            this.pnlTitleContainer.TabIndex = 10;
            // 
            // pnlColorIndicator
            // 
            this.pnlColorIndicator.BorderRadius = 4;
            this.pnlColorIndicator.Location = new System.Drawing.Point(15, 6);
            this.pnlColorIndicator.Margin = new System.Windows.Forms.Padding(4);
            this.pnlColorIndicator.Name = "pnlColorIndicator";
            this.pnlColorIndicator.Size = new System.Drawing.Size(24, 22);
            this.pnlColorIndicator.TabIndex = 4;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Montserrat", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(43, 4);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.MaximumSize = new System.Drawing.Size(420, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(73, 34);
            this.lblTitle.TabIndex = 6;
            this.lblTitle.Text = "Title:";
            // 
            // lblDateTime
            // 
            this.lblDateTime.AutoSize = true;
            this.lblDateTime.Font = new System.Drawing.Font("Montserrat", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblDateTime.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblDateTime.Location = new System.Drawing.Point(3, 95);
            this.lblDateTime.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Padding = new System.Windows.Forms.Padding(45, 0, 0, 0);
            this.lblDateTime.Size = new System.Drawing.Size(174, 34);
            this.lblDateTime.TabIndex = 7;
            this.lblDateTime.Text = "Date Time";
            // 
            // lblDescriptionHeader
            // 
            this.lblDescriptionHeader.AutoSize = true;
            this.lblDescriptionHeader.Font = new System.Drawing.Font("Montserrat", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblDescriptionHeader.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblDescriptionHeader.Location = new System.Drawing.Point(3, 144);
            this.lblDescriptionHeader.Margin = new System.Windows.Forms.Padding(3, 15, 3, 0);
            this.lblDescriptionHeader.Name = "lblDescriptionHeader";
            this.lblDescriptionHeader.Padding = new System.Windows.Forms.Padding(45, 0, 0, 0);
            this.lblDescriptionHeader.Size = new System.Drawing.Size(131, 34);
            this.lblDescriptionHeader.TabIndex = 8;
            this.lblDescriptionHeader.Text = "Mô tả:";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Montserrat", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblDescription.ForeColor = System.Drawing.Color.LightGray;
            this.lblDescription.Location = new System.Drawing.Point(3, 186);
            this.lblDescription.Margin = new System.Windows.Forms.Padding(3, 8, 3, 15);
            this.lblDescription.MaximumSize = new System.Drawing.Size(465, 0);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Padding = new System.Windows.Forms.Padding(45, 0, 0, 0);
            this.lblDescription.Size = new System.Drawing.Size(188, 34);
            this.lblDescription.TabIndex = 9;
            this.lblDescription.Text = "Description";
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Animated = true;
            this.btnEdit.BorderRadius = 6;
            this.btnEdit.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(59)))), ((int)(((byte)(135)))));
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.Location = new System.Drawing.Point(351, 15);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(53, 37);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(23)))), ((int)(((byte)(84)))));
            this.btnClose.IconColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(469, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(60, 36);
            this.btnClose.TabIndex = 3;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pnlEdit
            // 
            this.pnlEdit.Controls.Add(this.chkRemindBeforeEdit);
            this.pnlEdit.Controls.Add(this.txtDescriptionEdit);
            this.pnlEdit.Controls.Add(this.btnColorPicker);
            this.pnlEdit.Controls.Add(this.label2);
            this.pnlEdit.Controls.Add(this.label1);
            this.pnlEdit.Controls.Add(this.dtpEndTime);
            this.pnlEdit.Controls.Add(this.dtpStartTime);
            this.pnlEdit.Controls.Add(this.btnCancel);
            this.pnlEdit.Controls.Add(this.btnSave);
            this.pnlEdit.Controls.Add(this.txtTitleEdit);
            this.pnlEdit.Location = new System.Drawing.Point(16, 60);
            this.pnlEdit.Name = "pnlEdit";
            this.pnlEdit.Size = new System.Drawing.Size(501, 375);
            this.pnlEdit.TabIndex = 5;
            // 
            // chkRemindBeforeEdit
            // 
            this.chkRemindBeforeEdit.AutoSize = true;
            this.chkRemindBeforeEdit.CheckedState.BorderRadius = 0;
            this.chkRemindBeforeEdit.CheckedState.BorderThickness = 0;
            this.chkRemindBeforeEdit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.chkRemindBeforeEdit.ForeColor = System.Drawing.Color.Snow;
            this.chkRemindBeforeEdit.Location = new System.Drawing.Point(20, 328);
            this.chkRemindBeforeEdit.Margin = new System.Windows.Forms.Padding(2);
            this.chkRemindBeforeEdit.Name = "chkRemindBeforeEdit";
            this.chkRemindBeforeEdit.Size = new System.Drawing.Size(228, 29);
            this.chkRemindBeforeEdit.TabIndex = 9;
            this.chkRemindBeforeEdit.Text = "nhắc nhở trước 5 phút";
            this.chkRemindBeforeEdit.UncheckedState.BorderRadius = 0;
            this.chkRemindBeforeEdit.UncheckedState.BorderThickness = 0;
            // 
            // txtDescriptionEdit
            // 
            this.txtDescriptionEdit.Animated = true;
            this.txtDescriptionEdit.BorderRadius = 6;
            this.txtDescriptionEdit.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDescriptionEdit.DefaultText = "";
            this.txtDescriptionEdit.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.txtDescriptionEdit.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDescriptionEdit.ForeColor = System.Drawing.Color.White;
            this.txtDescriptionEdit.Location = new System.Drawing.Point(20, 182);
            this.txtDescriptionEdit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDescriptionEdit.Multiline = true;
            this.txtDescriptionEdit.Name = "txtDescriptionEdit";
            this.txtDescriptionEdit.PlaceholderText = "Thêm mô tả (không bắt buộc)";
            this.txtDescriptionEdit.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescriptionEdit.SelectedText = "";
            this.txtDescriptionEdit.Size = new System.Drawing.Size(465, 120);
            this.txtDescriptionEdit.TabIndex = 8;
            // 
            // btnColorPicker
            // 
            this.btnColorPicker.BorderRadius = 6;
            this.btnColorPicker.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnColorPicker.ForeColor = System.Drawing.Color.White;
            this.btnColorPicker.Location = new System.Drawing.Point(356, 117);
            this.btnColorPicker.Name = "btnColorPicker";
            this.btnColorPicker.Size = new System.Drawing.Size(129, 44);
            this.btnColorPicker.TabIndex = 7;
            this.btnColorPicker.Text = "Chọn màu";
            this.btnColorPicker.Click += new System.EventHandler(this.btnColorPicker_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label2.ForeColor = System.Drawing.Color.LightGray;
            this.label2.Location = new System.Drawing.Point(179, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 25);
            this.label2.TabIndex = 6;
            this.label2.Text = "Kết thúc:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label1.ForeColor = System.Drawing.Color.LightGray;
            this.label1.Location = new System.Drawing.Point(16, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "Bắt đầu:";
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.Animated = true;
            this.dtpEndTime.BorderRadius = 6;
            this.dtpEndTime.Checked = true;
            this.dtpEndTime.CustomFormat = "hh:mm:ss tt";
            this.dtpEndTime.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.dtpEndTime.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpEndTime.ForeColor = System.Drawing.Color.White;
            this.dtpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndTime.Location = new System.Drawing.Point(183, 117);
            this.dtpEndTime.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpEndTime.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.ShowUpDown = true;
            this.dtpEndTime.Size = new System.Drawing.Size(154, 44);
            this.dtpEndTime.TabIndex = 4;
            this.dtpEndTime.Value = new System.DateTime(2025, 11, 6, 15, 3, 54, 479);
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.Animated = true;
            this.dtpStartTime.BorderRadius = 6;
            this.dtpStartTime.Checked = true;
            this.dtpStartTime.CustomFormat = "hh:mm:ss tt";
            this.dtpStartTime.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.dtpStartTime.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpStartTime.ForeColor = System.Drawing.Color.White;
            this.dtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartTime.Location = new System.Drawing.Point(20, 117);
            this.dtpStartTime.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpStartTime.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.ShowUpDown = true;
            this.dtpStartTime.Size = new System.Drawing.Size(155, 44);
            this.dtpStartTime.TabIndex = 3;
            this.dtpStartTime.Value = new System.DateTime(2025, 11, 6, 15, 3, 54, 497);
            // 
            // btnCancel
            // 
            this.btnCancel.Animated = true;
            this.btnCancel.BorderRadius = 8;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(264, 318);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(107, 43);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Animated = true;
            this.btnSave.BorderRadius = 8;
            this.btnSave.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(379, 318);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(107, 43);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Lưu";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtTitleEdit
            // 
            this.txtTitleEdit.Animated = true;
            this.txtTitleEdit.BorderRadius = 6;
            this.txtTitleEdit.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTitleEdit.DefaultText = "";
            this.txtTitleEdit.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.txtTitleEdit.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtTitleEdit.ForeColor = System.Drawing.Color.White;
            this.txtTitleEdit.Location = new System.Drawing.Point(20, 17);
            this.txtTitleEdit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTitleEdit.Name = "txtTitleEdit";
            this.txtTitleEdit.PlaceholderText = "Thêm tiêu đề";
            this.txtTitleEdit.SelectedText = "";
            this.txtTitleEdit.Size = new System.Drawing.Size(465, 54);
            this.txtTitleEdit.TabIndex = 0;
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Animated = true;
            this.btnDelete.BorderColor = System.Drawing.Color.White;
            this.btnDelete.BorderRadius = 6;
            this.btnDelete.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(59)))), ((int)(((byte)(135)))));
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Location = new System.Drawing.Point(412, 15);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(53, 37);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // EventDetailsForm
            // 
            this.ClientSize = new System.Drawing.Size(533, 450);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "EventDetailsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chi tiết sự kiện";
            this.Load += new System.EventHandler(this.EventDetailsForm_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlView.ResumeLayout(false);
            this.pnlView.PerformLayout();
            this.flpViewContent.ResumeLayout(false);
            this.flpViewContent.PerformLayout();
            this.pnlTitleContainer.ResumeLayout(false);
            this.pnlTitleContainer.PerformLayout();
            this.pnlEdit.ResumeLayout(false);
            this.pnlEdit.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2ShadowForm guna2ShadowForm1;
        private Guna.UI2.WinForms.Guna2Panel pnlMain;
        private Guna.UI2.WinForms.Guna2Panel pnlEdit;
        private Guna.UI2.WinForms.Guna2Button btnColorPicker;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpEndTime;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpStartTime;
        private Guna.UI2.WinForms.Guna2Button btnCancel;
        private Guna.UI2.WinForms.Guna2Button btnSave;
        private Guna.UI2.WinForms.Guna2TextBox txtTitleEdit;
        private Guna.UI2.WinForms.Guna2ControlBox btnClose;
        private Guna.UI2.WinForms.Guna2Button btnEdit;
        private Guna.UI2.WinForms.Guna2Button btnDelete;
        private Guna.UI2.WinForms.Guna2TextBox txtDescriptionEdit;
        private Guna.UI2.WinForms.Guna2CheckBox chkRemindBeforeEdit;
        private Guna.UI2.WinForms.Guna2Panel pnlView;
        private Guna.UI2.WinForms.Guna2VScrollBar vsbView;
        private System.Windows.Forms.FlowLayoutPanel flpViewContent;
        private System.Windows.Forms.Label lblTitleHeader;
        private Guna.UI2.WinForms.Guna2Panel pnlTitleContainer;
        private Guna.UI2.WinForms.Guna2Panel pnlColorIndicator;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDateTime;
        private System.Windows.Forms.Label lblDescriptionHeader;
        private System.Windows.Forms.Label lblDescription;
    }
}