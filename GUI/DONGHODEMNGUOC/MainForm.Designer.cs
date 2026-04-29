namespace Calendar
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.notificationTimer = new System.Windows.Forms.Timer(this.components);
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.pnlSidebar = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlSidebarContainer = new Guna.UI2.WinForms.Guna2Panel();
            this.btnQuickAdd = new Guna.UI2.WinForms.Guna2Button();
            this.cmbQuickTime = new Guna.UI2.WinForms.Guna2ComboBox();
            this.flpDayOfWeek = new System.Windows.Forms.FlowLayoutPanel();
            this.lblSun = new System.Windows.Forms.Label();
            this.lblMon = new System.Windows.Forms.Label();
            this.lblTue = new System.Windows.Forms.Label();
            this.lblWed = new System.Windows.Forms.Label();
            this.lblThu = new System.Windows.Forms.Label();
            this.lblFri = new System.Windows.Forms.Label();
            this.lblSat = new System.Windows.Forms.Label();
            this.guna2Separator1 = new Guna.UI2.WinForms.Guna2Separator();
            this.flpCalendarDays = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlMiniCalHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.lblCalMonthYear = new System.Windows.Forms.Label();
            this.btnCalNext = new Guna.UI2.WinForms.Guna2Button();
            this.btnCalPrev = new Guna.UI2.WinForms.Guna2Button();
            this.btnShiftEvening = new Guna.UI2.WinForms.Guna2Button();
            this.btnShiftAfternoon = new Guna.UI2.WinForms.Guna2Button();
            this.btnShiftMorning = new Guna.UI2.WinForms.Guna2Button();
            this.pnlContent = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlScrollContainer = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlCanvas = new System.Windows.Forms.Panel();
            this.pnlTop = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.lblMonthYear = new System.Windows.Forms.Label();
            this.btnToday = new Guna.UI2.WinForms.Guna2Button();
            this.btnNext = new Guna.UI2.WinForms.Guna2Button();
            this.btnPrevious = new Guna.UI2.WinForms.Guna2Button();
            this.scrollTimer = new System.Windows.Forms.Timer(this.components);
            this.guna2ShadowForm1 = new Guna.UI2.WinForms.Guna2ShadowForm(this.components);
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.pnlSidebar.SuspendLayout();
            this.pnlSidebarContainer.SuspendLayout();
            this.flpDayOfWeek.SuspendLayout();
            this.pnlMiniCalHeader.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlScrollContainer.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.IsSplitterFixed = true;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.pnlSidebar);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.pnlContent);
            this.splitContainer.Size = new System.Drawing.Size(1896, 1171);
            this.splitContainer.SplitterDistance = 300;
            this.splitContainer.SplitterWidth = 6;
            this.splitContainer.TabIndex = 0;
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(25)))), ((int)(((byte)(101)))));
            this.pnlSidebar.Controls.Add(this.pnlSidebarContainer);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 0);
            this.pnlSidebar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Padding = new System.Windows.Forms.Padding(15);
            this.pnlSidebar.Size = new System.Drawing.Size(300, 1171);
            this.pnlSidebar.TabIndex = 0;
            // 
            // pnlSidebarContainer
            // 
            this.pnlSidebarContainer.Controls.Add(this.btnQuickAdd);
            this.pnlSidebarContainer.Controls.Add(this.cmbQuickTime);
            this.pnlSidebarContainer.Controls.Add(this.flpDayOfWeek);
            this.pnlSidebarContainer.Controls.Add(this.guna2Separator1);
            this.pnlSidebarContainer.Controls.Add(this.flpCalendarDays);
            this.pnlSidebarContainer.Controls.Add(this.pnlMiniCalHeader);
            this.pnlSidebarContainer.Controls.Add(this.btnShiftEvening);
            this.pnlSidebarContainer.Controls.Add(this.btnShiftAfternoon);
            this.pnlSidebarContainer.Controls.Add(this.btnShiftMorning);
            this.pnlSidebarContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSidebarContainer.Location = new System.Drawing.Point(15, 15);
            this.pnlSidebarContainer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlSidebarContainer.Name = "pnlSidebarContainer";
            this.pnlSidebarContainer.Size = new System.Drawing.Size(270, 1141);
            this.pnlSidebarContainer.TabIndex = 7;
            // 
            // btnQuickAdd
            // 
            this.btnQuickAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuickAdd.BorderRadius = 10;
            this.btnQuickAdd.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(59)))), ((int)(((byte)(135)))));
            this.btnQuickAdd.Font = new System.Drawing.Font("Montserrat", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnQuickAdd.ForeColor = System.Drawing.Color.White;
            this.btnQuickAdd.Location = new System.Drawing.Point(0, 861);
            this.btnQuickAdd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnQuickAdd.Name = "btnQuickAdd";
            this.btnQuickAdd.Size = new System.Drawing.Size(270, 69);
            this.btnQuickAdd.TabIndex = 9;
            this.btnQuickAdd.Text = "Tạo Hẹn Giờ Nhanh";
            this.btnQuickAdd.Click += new System.EventHandler(this.btnQuickAdd_Click);
            // 
            // cmbQuickTime
            // 
            this.cmbQuickTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbQuickTime.BackColor = System.Drawing.Color.Transparent;
            this.cmbQuickTime.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(25)))), ((int)(((byte)(101)))));
            this.cmbQuickTime.BorderRadius = 10;
            this.cmbQuickTime.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbQuickTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQuickTime.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(59)))), ((int)(((byte)(135)))));
            this.cmbQuickTime.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbQuickTime.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cmbQuickTime.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbQuickTime.ForeColor = System.Drawing.Color.White;
            this.cmbQuickTime.ItemHeight = 30;
            this.cmbQuickTime.Location = new System.Drawing.Point(0, 798);
            this.cmbQuickTime.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbQuickTime.Name = "cmbQuickTime";
            this.cmbQuickTime.Size = new System.Drawing.Size(269, 36);
            this.cmbQuickTime.TabIndex = 8;
            // 
            // flpDayOfWeek
            // 
            this.flpDayOfWeek.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpDayOfWeek.Controls.Add(this.lblSun);
            this.flpDayOfWeek.Controls.Add(this.lblMon);
            this.flpDayOfWeek.Controls.Add(this.lblTue);
            this.flpDayOfWeek.Controls.Add(this.lblWed);
            this.flpDayOfWeek.Controls.Add(this.lblThu);
            this.flpDayOfWeek.Controls.Add(this.lblFri);
            this.flpDayOfWeek.Controls.Add(this.lblSat);
            this.flpDayOfWeek.Location = new System.Drawing.Point(0, 78);
            this.flpDayOfWeek.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.flpDayOfWeek.Name = "flpDayOfWeek";
            this.flpDayOfWeek.Size = new System.Drawing.Size(270, 38);
            this.flpDayOfWeek.TabIndex = 6;
            // 
            // lblSun
            // 
            this.lblSun.Font = new System.Drawing.Font("Montserrat", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblSun.ForeColor = System.Drawing.Color.LightGray;
            this.lblSun.Location = new System.Drawing.Point(0, 0);
            this.lblSun.Margin = new System.Windows.Forms.Padding(0);
            this.lblSun.Name = "lblSun";
            this.lblSun.Size = new System.Drawing.Size(60, 31);
            this.lblSun.TabIndex = 0;
            this.lblSun.Text = "CN";
            this.lblSun.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMon
            // 
            this.lblMon.Font = new System.Drawing.Font("Montserrat", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblMon.ForeColor = System.Drawing.Color.LightGray;
            this.lblMon.Location = new System.Drawing.Point(60, 0);
            this.lblMon.Margin = new System.Windows.Forms.Padding(0);
            this.lblMon.Name = "lblMon";
            this.lblMon.Size = new System.Drawing.Size(60, 31);
            this.lblMon.TabIndex = 1;
            this.lblMon.Text = "T2";
            this.lblMon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTue
            // 
            this.lblTue.Font = new System.Drawing.Font("Montserrat", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTue.ForeColor = System.Drawing.Color.LightGray;
            this.lblTue.Location = new System.Drawing.Point(120, 0);
            this.lblTue.Margin = new System.Windows.Forms.Padding(0);
            this.lblTue.Name = "lblTue";
            this.lblTue.Size = new System.Drawing.Size(60, 31);
            this.lblTue.TabIndex = 2;
            this.lblTue.Text = "T3";
            this.lblTue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWed
            // 
            this.lblWed.Font = new System.Drawing.Font("Montserrat", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblWed.ForeColor = System.Drawing.Color.LightGray;
            this.lblWed.Location = new System.Drawing.Point(180, 0);
            this.lblWed.Margin = new System.Windows.Forms.Padding(0);
            this.lblWed.Name = "lblWed";
            this.lblWed.Size = new System.Drawing.Size(60, 31);
            this.lblWed.TabIndex = 3;
            this.lblWed.Text = "T4";
            this.lblWed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblThu
            // 
            this.lblThu.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThu.ForeColor = System.Drawing.Color.LightGray;
            this.lblThu.Location = new System.Drawing.Point(0, 31);
            this.lblThu.Margin = new System.Windows.Forms.Padding(0);
            this.lblThu.Name = "lblThu";
            this.lblThu.Size = new System.Drawing.Size(60, 31);
            this.lblThu.TabIndex = 4;
            this.lblThu.Text = "T5";
            this.lblThu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFri
            // 
            this.lblFri.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFri.ForeColor = System.Drawing.Color.LightGray;
            this.lblFri.Location = new System.Drawing.Point(60, 31);
            this.lblFri.Margin = new System.Windows.Forms.Padding(0);
            this.lblFri.Name = "lblFri";
            this.lblFri.Size = new System.Drawing.Size(60, 31);
            this.lblFri.TabIndex = 5;
            this.lblFri.Text = "T6";
            this.lblFri.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSat
            // 
            this.lblSat.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSat.ForeColor = System.Drawing.Color.LightGray;
            this.lblSat.Location = new System.Drawing.Point(120, 31);
            this.lblSat.Margin = new System.Windows.Forms.Padding(0);
            this.lblSat.Name = "lblSat";
            this.lblSat.Size = new System.Drawing.Size(60, 31);
            this.lblSat.TabIndex = 6;
            this.lblSat.Text = "T7";
            this.lblSat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // guna2Separator1
            // 
            this.guna2Separator1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2Separator1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.guna2Separator1.Location = new System.Drawing.Point(0, 508);
            this.guna2Separator1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.guna2Separator1.Name = "guna2Separator1";
            this.guna2Separator1.Size = new System.Drawing.Size(270, 15);
            this.guna2Separator1.TabIndex = 5;
            // 
            // flpCalendarDays
            // 
            this.flpCalendarDays.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpCalendarDays.Location = new System.Drawing.Point(0, 122);
            this.flpCalendarDays.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.flpCalendarDays.Name = "flpCalendarDays";
            this.flpCalendarDays.Size = new System.Drawing.Size(270, 369);
            this.flpCalendarDays.TabIndex = 4;
            this.flpCalendarDays.Paint += new System.Windows.Forms.PaintEventHandler(this.flpCalendarDays_Paint);
            // 
            // pnlMiniCalHeader
            // 
            this.pnlMiniCalHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMiniCalHeader.Controls.Add(this.lblCalMonthYear);
            this.pnlMiniCalHeader.Controls.Add(this.btnCalNext);
            this.pnlMiniCalHeader.Controls.Add(this.btnCalPrev);
            this.pnlMiniCalHeader.Location = new System.Drawing.Point(0, 5);
            this.pnlMiniCalHeader.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlMiniCalHeader.Name = "pnlMiniCalHeader";
            this.pnlMiniCalHeader.Size = new System.Drawing.Size(270, 60);
            this.pnlMiniCalHeader.TabIndex = 3;
            // 
            // lblCalMonthYear
            // 
            this.lblCalMonthYear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lblCalMonthYear.Font = new System.Drawing.Font("Montserrat", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblCalMonthYear.ForeColor = System.Drawing.Color.White;
            this.lblCalMonthYear.Location = new System.Drawing.Point(13, 12);
            this.lblCalMonthYear.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCalMonthYear.Name = "lblCalMonthYear";
            this.lblCalMonthYear.Size = new System.Drawing.Size(246, 35);
            this.lblCalMonthYear.TabIndex = 2;
            this.lblCalMonthYear.Text = "Tháng 10 2025";
            this.lblCalMonthYear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCalNext
            // 
            this.btnCalNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCalNext.BorderRadius = 6;
            this.btnCalNext.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(59)))), ((int)(((byte)(135)))));
            this.btnCalNext.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCalNext.ForeColor = System.Drawing.Color.White;
            this.btnCalNext.Image = ((System.Drawing.Image)(resources.GetObject("btnCalNext.Image")));
            this.btnCalNext.Location = new System.Drawing.Point(214, 8);
            this.btnCalNext.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCalNext.Name = "btnCalNext";
            this.btnCalNext.Size = new System.Drawing.Size(53, 46);
            this.btnCalNext.TabIndex = 1;
            this.btnCalNext.Click += new System.EventHandler(this.btnCalNext_Click);
            // 
            // btnCalPrev
            // 
            this.btnCalPrev.BorderRadius = 6;
            this.btnCalPrev.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(59)))), ((int)(((byte)(135)))));
            this.btnCalPrev.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCalPrev.ForeColor = System.Drawing.Color.White;
            this.btnCalPrev.Image = ((System.Drawing.Image)(resources.GetObject("btnCalPrev.Image")));
            this.btnCalPrev.Location = new System.Drawing.Point(4, 8);
            this.btnCalPrev.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCalPrev.Name = "btnCalPrev";
            this.btnCalPrev.Size = new System.Drawing.Size(53, 46);
            this.btnCalPrev.TabIndex = 0;
            this.btnCalPrev.Click += new System.EventHandler(this.btnCalPrev_Click);
            // 
            // btnShiftEvening
            // 
            this.btnShiftEvening.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShiftEvening.BorderRadius = 10;
            this.btnShiftEvening.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(59)))), ((int)(((byte)(135)))));
            this.btnShiftEvening.Font = new System.Drawing.Font("Montserrat", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnShiftEvening.ForeColor = System.Drawing.Color.White;
            this.btnShiftEvening.Location = new System.Drawing.Point(0, 695);
            this.btnShiftEvening.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnShiftEvening.Name = "btnShiftEvening";
            this.btnShiftEvening.Size = new System.Drawing.Size(270, 69);
            this.btnShiftEvening.TabIndex = 2;
            this.btnShiftEvening.Text = "Thêm Ca Tối";
            this.btnShiftEvening.Click += new System.EventHandler(this.btnShiftEvening_Click);
            // 
            // btnShiftAfternoon
            // 
            this.btnShiftAfternoon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShiftAfternoon.BorderRadius = 10;
            this.btnShiftAfternoon.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(59)))), ((int)(((byte)(135)))));
            this.btnShiftAfternoon.Font = new System.Drawing.Font("Montserrat", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnShiftAfternoon.ForeColor = System.Drawing.Color.White;
            this.btnShiftAfternoon.Location = new System.Drawing.Point(0, 618);
            this.btnShiftAfternoon.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnShiftAfternoon.Name = "btnShiftAfternoon";
            this.btnShiftAfternoon.Size = new System.Drawing.Size(270, 69);
            this.btnShiftAfternoon.TabIndex = 1;
            this.btnShiftAfternoon.Text = "Thêm Ca Chiều";
            this.btnShiftAfternoon.Click += new System.EventHandler(this.btnShiftAfternoon_Click);
            // 
            // btnShiftMorning
            // 
            this.btnShiftMorning.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShiftMorning.BorderRadius = 10;
            this.btnShiftMorning.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(59)))), ((int)(((byte)(135)))));
            this.btnShiftMorning.Font = new System.Drawing.Font("Montserrat", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnShiftMorning.ForeColor = System.Drawing.Color.White;
            this.btnShiftMorning.Location = new System.Drawing.Point(0, 539);
            this.btnShiftMorning.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnShiftMorning.Name = "btnShiftMorning";
            this.btnShiftMorning.Size = new System.Drawing.Size(270, 69);
            this.btnShiftMorning.TabIndex = 0;
            this.btnShiftMorning.Text = "Thêm Ca Sáng";
            this.btnShiftMorning.Click += new System.EventHandler(this.btnShiftMorning_Click);
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.pnlContent.Controls.Add(this.pnlScrollContainer);
            this.pnlContent.Controls.Add(this.pnlTop);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 0);
            this.pnlContent.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1590, 1171);
            this.pnlContent.TabIndex = 0;
            // 
            // pnlScrollContainer
            // 
            this.pnlScrollContainer.AutoScroll = true;
            this.pnlScrollContainer.BackColor = System.Drawing.Color.White;
            this.pnlScrollContainer.Controls.Add(this.pnlCanvas);
            this.pnlScrollContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlScrollContainer.Location = new System.Drawing.Point(0, 92);
            this.pnlScrollContainer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlScrollContainer.Name = "pnlScrollContainer";
            this.pnlScrollContainer.Size = new System.Drawing.Size(1590, 1079);
            this.pnlScrollContainer.TabIndex = 1;
            // 
            // pnlCanvas
            // 
            this.pnlCanvas.BackColor = System.Drawing.Color.White;
            this.pnlCanvas.Location = new System.Drawing.Point(-13, 0);
            this.pnlCanvas.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlCanvas.Name = "pnlCanvas";
            this.pnlCanvas.Size = new System.Drawing.Size(1427, 2261);
            this.pnlCanvas.TabIndex = 0;
            this.pnlCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlCanvas_Paint);
            this.pnlCanvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlCanvas_MouseDown);
            this.pnlCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlCanvas_MouseMove);
            this.pnlCanvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlCanvas_MouseUp);
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(25)))), ((int)(((byte)(101)))));
            this.pnlTop.Controls.Add(this.guna2ControlBox1);
            this.pnlTop.Controls.Add(this.lblMonthYear);
            this.pnlTop.Controls.Add(this.btnToday);
            this.pnlTop.Controls.Add(this.btnNext);
            this.pnlTop.Controls.Add(this.btnPrevious);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1590, 92);
            this.pnlTop.TabIndex = 0;
            // 
            // guna2ControlBox1
            // 
            this.guna2ControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(25)))), ((int)(((byte)(101)))));
            this.guna2ControlBox1.IconColor = System.Drawing.Color.White;
            this.guna2ControlBox1.Location = new System.Drawing.Point(1522, 12);
            this.guna2ControlBox1.Name = "guna2ControlBox1";
            this.guna2ControlBox1.Size = new System.Drawing.Size(56, 34);
            this.guna2ControlBox1.TabIndex = 4;
            // 
            // lblMonthYear
            // 
            this.lblMonthYear.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblMonthYear.Font = new System.Drawing.Font("Montserrat Black", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblMonthYear.ForeColor = System.Drawing.Color.White;
            this.lblMonthYear.Location = new System.Drawing.Point(623, 28);
            this.lblMonthYear.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMonthYear.Name = "lblMonthYear";
            this.lblMonthYear.Size = new System.Drawing.Size(345, 39);
            this.lblMonthYear.TabIndex = 3;
            this.lblMonthYear.Text = "November, 2025";
            this.lblMonthYear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnToday
            // 
            this.btnToday.BorderRadius = 10;
            this.btnToday.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(59)))), ((int)(((byte)(135)))));
            this.btnToday.Font = new System.Drawing.Font("Montserrat", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnToday.ForeColor = System.Drawing.Color.White;
            this.btnToday.Location = new System.Drawing.Point(202, 19);
            this.btnToday.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(150, 59);
            this.btnToday.TabIndex = 2;
            this.btnToday.Text = "Hôm nay";
            this.btnToday.Click += new System.EventHandler(this.btnToday_Click);
            // 
            // btnNext
            // 
            this.btnNext.BorderRadius = 6;
            this.btnNext.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(59)))), ((int)(((byte)(135)))));
            this.btnNext.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnNext.ForeColor = System.Drawing.Color.White;
            this.btnNext.Image = ((System.Drawing.Image)(resources.GetObject("btnNext.Image")));
            this.btnNext.Location = new System.Drawing.Point(111, 19);
            this.btnNext.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(68, 59);
            this.btnNext.TabIndex = 1;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.BorderRadius = 6;
            this.btnPrevious.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(59)))), ((int)(((byte)(135)))));
            this.btnPrevious.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnPrevious.ForeColor = System.Drawing.Color.White;
            this.btnPrevious.Image = ((System.Drawing.Image)(resources.GetObject("btnPrevious.Image")));
            this.btnPrevious.Location = new System.Drawing.Point(35, 19);
            this.btnPrevious.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(68, 59);
            this.btnPrevious.TabIndex = 0;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // scrollTimer
            // 
            this.scrollTimer.Interval = 50;
            this.scrollTimer.Tick += new System.EventHandler(this.scrollTimer_Tick);
            // 
            // guna2ShadowForm1
            // 
            this.guna2ShadowForm1.TargetForm = this;
            // 
            // guna2BorderlessForm1
            // 
            this.guna2BorderlessForm1.BorderRadius = 30;
            this.guna2BorderlessForm1.ContainerControl = this;
            this.guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1896, 1171);
            this.Controls.Add(this.splitContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(1641, 972);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Calendar Application";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.pnlSidebar.ResumeLayout(false);
            this.pnlSidebarContainer.ResumeLayout(false);
            this.flpDayOfWeek.ResumeLayout(false);
            this.pnlMiniCalHeader.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlScrollContainer.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer notificationTimer;
        private System.Windows.Forms.SplitContainer splitContainer;
        private Guna.UI2.WinForms.Guna2Panel pnlSidebar;
        private Guna.UI2.WinForms.Guna2Panel pnlContent;
        private Guna.UI2.WinForms.Guna2Panel pnlTop;
        private Guna.UI2.WinForms.Guna2Panel pnlScrollContainer;
        private System.Windows.Forms.Panel pnlCanvas;
        private Guna.UI2.WinForms.Guna2Button btnPrevious;
        private Guna.UI2.WinForms.Guna2Button btnNext;
        private Guna.UI2.WinForms.Guna2Button btnToday;
        private System.Windows.Forms.Label lblMonthYear;
        private Guna.UI2.WinForms.Guna2Button btnShiftMorning;
        private Guna.UI2.WinForms.Guna2Button btnShiftEvening;
        private Guna.UI2.WinForms.Guna2Button btnShiftAfternoon;
        private Guna.UI2.WinForms.Guna2Panel pnlMiniCalHeader;
        private System.Windows.Forms.Label lblCalMonthYear;
        private Guna.UI2.WinForms.Guna2Button btnCalNext;
        private Guna.UI2.WinForms.Guna2Button btnCalPrev;
        private System.Windows.Forms.FlowLayoutPanel flpCalendarDays;
        private Guna.UI2.WinForms.Guna2Separator guna2Separator1;
        private System.Windows.Forms.Timer scrollTimer;
        private Guna.UI2.WinForms.Guna2ShadowForm guna2ShadowForm1;
        private System.Windows.Forms.FlowLayoutPanel flpDayOfWeek;
        private System.Windows.Forms.Label lblSun;
        private System.Windows.Forms.Label lblMon;
        private System.Windows.Forms.Label lblTue;
        private System.Windows.Forms.Label lblWed;
        private System.Windows.Forms.Label lblThu;
        private System.Windows.Forms.Label lblFri;
        private System.Windows.Forms.Label lblSat;
        private Guna.UI2.WinForms.Guna2Panel pnlSidebarContainer;
        private Guna.UI2.WinForms.Guna2Button btnQuickAdd;
        private Guna.UI2.WinForms.Guna2ComboBox cmbQuickTime;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
    }
}