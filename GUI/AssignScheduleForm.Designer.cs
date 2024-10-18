namespace GUI
{
    partial class AssignScheduleForm
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
			this.pnlLineLeft = new Guna.UI2.WinForms.Guna2Panel();
			this.pnlLineRight = new Guna.UI2.WinForms.Guna2Panel();
			this.pnlLineTop = new Guna.UI2.WinForms.Guna2Panel();
			this.btnMinimizeForm = new Guna.UI2.WinForms.Guna2ControlBox();
			this.btnCloseForm = new Guna.UI2.WinForms.Guna2ControlBox();
			this.pnlLineBottom = new Guna.UI2.WinForms.Guna2Panel();
			this.pnlMain = new Guna.UI2.WinForms.Guna2Panel();
			this.pnlButtonAdd_Cancel = new Guna.UI2.WinForms.Guna2Panel();
			this.btnCancel = new Guna.UI2.WinForms.Guna2Button();
			this.btnAdd = new Guna.UI2.WinForms.Guna2Button();
			this.pnlSpace10 = new Guna.UI2.WinForms.Guna2Panel();
			this.pnlSpace8 = new Guna.UI2.WinForms.Guna2Panel();
			this.pnlSession = new Guna.UI2.WinForms.Guna2Panel();
			this.cboSessions = new Guna.UI2.WinForms.Guna2ComboBox();
			this.lblSession = new System.Windows.Forms.Label();
			this.pnlSpace7 = new Guna.UI2.WinForms.Guna2Panel();
			this.pnlVehicles = new Guna.UI2.WinForms.Guna2Panel();
			this.cboVehicles = new Guna.UI2.WinForms.Guna2ComboBox();
			this.lblVehicles = new System.Windows.Forms.Label();
			this.pnlSpace4 = new Guna.UI2.WinForms.Guna2Panel();
			this.pnlTeachers = new Guna.UI2.WinForms.Guna2Panel();
			this.txtTotalAmount = new Guna.UI2.WinForms.Guna2TextBox();
			this.txtVND = new Guna.UI2.WinForms.Guna2TextBox();
			this.lblTotalAmount = new System.Windows.Forms.Label();
			this.pnlSpace3 = new Guna.UI2.WinForms.Guna2Panel();
			this.pnlLearners = new Guna.UI2.WinForms.Guna2Panel();
			this.guna2ComboBox1 = new Guna.UI2.WinForms.Guna2ComboBox();
			this.lblLearners = new System.Windows.Forms.Label();
			this.pnlSpace2 = new Guna.UI2.WinForms.Guna2Panel();
			this.pnlCourse = new Guna.UI2.WinForms.Guna2Panel();
			this.cboCourses = new Guna.UI2.WinForms.Guna2ComboBox();
			this.lblCourses = new System.Windows.Forms.Label();
			this.pnlSpace1 = new Guna.UI2.WinForms.Guna2Panel();
			this.pnlSpace6 = new Guna.UI2.WinForms.Guna2Panel();
			this.pnlTitle_DateAssign = new Guna.UI2.WinForms.Guna2Panel();
			this.lblDateAssign = new System.Windows.Forms.Label();
			this.pnlSpace9 = new Guna.UI2.WinForms.Guna2Panel();
			this.lblAdd = new System.Windows.Forms.Label();
			this.pnlSpace5 = new Guna.UI2.WinForms.Guna2Panel();
			this.dragControl = new Guna.UI2.WinForms.Guna2DragControl(this.components);
			this.shadowForm = new Guna.UI2.WinForms.Guna2ShadowForm(this.components);
			this.pnlLineTop.SuspendLayout();
			this.pnlMain.SuspendLayout();
			this.pnlButtonAdd_Cancel.SuspendLayout();
			this.pnlSession.SuspendLayout();
			this.pnlVehicles.SuspendLayout();
			this.pnlTeachers.SuspendLayout();
			this.pnlLearners.SuspendLayout();
			this.pnlCourse.SuspendLayout();
			this.pnlTitle_DateAssign.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlLineLeft
			// 
			this.pnlLineLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
			this.pnlLineLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnlLineLeft.Location = new System.Drawing.Point(0, 25);
			this.pnlLineLeft.Name = "pnlLineLeft";
			this.pnlLineLeft.Size = new System.Drawing.Size(25, 350);
			this.pnlLineLeft.TabIndex = 52;
			// 
			// pnlLineRight
			// 
			this.pnlLineRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
			this.pnlLineRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.pnlLineRight.Location = new System.Drawing.Point(475, 25);
			this.pnlLineRight.Name = "pnlLineRight";
			this.pnlLineRight.Size = new System.Drawing.Size(25, 350);
			this.pnlLineRight.TabIndex = 54;
			// 
			// pnlLineTop
			// 
			this.pnlLineTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
			this.pnlLineTop.Controls.Add(this.btnMinimizeForm);
			this.pnlLineTop.Controls.Add(this.btnCloseForm);
			this.pnlLineTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlLineTop.Location = new System.Drawing.Point(0, 0);
			this.pnlLineTop.Name = "pnlLineTop";
			this.pnlLineTop.Size = new System.Drawing.Size(500, 25);
			this.pnlLineTop.TabIndex = 51;
			// 
			// btnMinimizeForm
			// 
			this.btnMinimizeForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
			this.btnMinimizeForm.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
			this.btnMinimizeForm.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnMinimizeForm.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnMinimizeForm.FillColor = System.Drawing.Color.Transparent;
			this.btnMinimizeForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnMinimizeForm.ForeColor = System.Drawing.Color.Black;
			this.btnMinimizeForm.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
			this.btnMinimizeForm.HoverState.IconColor = System.Drawing.Color.Black;
			this.btnMinimizeForm.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(50)))), ((int)(((byte)(52)))));
			this.btnMinimizeForm.Location = new System.Drawing.Point(410, 0);
			this.btnMinimizeForm.Name = "btnMinimizeForm";
			this.btnMinimizeForm.Size = new System.Drawing.Size(45, 25);
			this.btnMinimizeForm.TabIndex = 31;
			// 
			// btnCloseForm
			// 
			this.btnCloseForm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
			this.btnCloseForm.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnCloseForm.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnCloseForm.FillColor = System.Drawing.Color.Transparent;
			this.btnCloseForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCloseForm.ForeColor = System.Drawing.Color.Black;
			this.btnCloseForm.HoverState.FillColor = System.Drawing.Color.Red;
			this.btnCloseForm.HoverState.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
			this.btnCloseForm.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(50)))), ((int)(((byte)(52)))));
			this.btnCloseForm.Location = new System.Drawing.Point(455, 0);
			this.btnCloseForm.Name = "btnCloseForm";
			this.btnCloseForm.Size = new System.Drawing.Size(45, 25);
			this.btnCloseForm.TabIndex = 30;
			// 
			// pnlLineBottom
			// 
			this.pnlLineBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
			this.pnlLineBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlLineBottom.Location = new System.Drawing.Point(0, 375);
			this.pnlLineBottom.Name = "pnlLineBottom";
			this.pnlLineBottom.Size = new System.Drawing.Size(500, 25);
			this.pnlLineBottom.TabIndex = 53;
			// 
			// pnlMain
			// 
			this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
			this.pnlMain.BorderRadius = 15;
			this.pnlMain.Controls.Add(this.pnlButtonAdd_Cancel);
			this.pnlMain.Controls.Add(this.pnlSpace8);
			this.pnlMain.Controls.Add(this.pnlSession);
			this.pnlMain.Controls.Add(this.pnlSpace7);
			this.pnlMain.Controls.Add(this.pnlVehicles);
			this.pnlMain.Controls.Add(this.pnlSpace4);
			this.pnlMain.Controls.Add(this.pnlTeachers);
			this.pnlMain.Controls.Add(this.pnlSpace3);
			this.pnlMain.Controls.Add(this.pnlLearners);
			this.pnlMain.Controls.Add(this.pnlSpace2);
			this.pnlMain.Controls.Add(this.pnlCourse);
			this.pnlMain.Controls.Add(this.pnlSpace1);
			this.pnlMain.Controls.Add(this.pnlSpace6);
			this.pnlMain.Controls.Add(this.pnlTitle_DateAssign);
			this.pnlMain.Controls.Add(this.pnlSpace5);
			this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlMain.FillColor = System.Drawing.Color.White;
			this.pnlMain.Location = new System.Drawing.Point(25, 25);
			this.pnlMain.Name = "pnlMain";
			this.pnlMain.Size = new System.Drawing.Size(450, 350);
			this.pnlMain.TabIndex = 55;
			// 
			// pnlButtonAdd_Cancel
			// 
			this.pnlButtonAdd_Cancel.Controls.Add(this.btnCancel);
			this.pnlButtonAdd_Cancel.Controls.Add(this.btnAdd);
			this.pnlButtonAdd_Cancel.Controls.Add(this.pnlSpace10);
			this.pnlButtonAdd_Cancel.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlButtonAdd_Cancel.FillColor = System.Drawing.Color.White;
			this.pnlButtonAdd_Cancel.Location = new System.Drawing.Point(0, 305);
			this.pnlButtonAdd_Cancel.Name = "pnlButtonAdd_Cancel";
			this.pnlButtonAdd_Cancel.Size = new System.Drawing.Size(433, 35);
			this.pnlButtonAdd_Cancel.TabIndex = 133;
			// 
			// btnCancel
			// 
			this.btnCancel.BorderRadius = 5;
			this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnCancel.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnCancel.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnCancel.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnCancel.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnCancel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(100)))), ((int)(((byte)(119)))));
			this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.btnCancel.ForeColor = System.Drawing.Color.White;
			this.btnCancel.Location = new System.Drawing.Point(283, 0);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
			this.btnCancel.Size = new System.Drawing.Size(150, 35);
			this.btnCancel.TabIndex = 19;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.BorderRadius = 5;
			this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnAdd.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
			this.btnAdd.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
			this.btnAdd.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
			this.btnAdd.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
			this.btnAdd.Dock = System.Windows.Forms.DockStyle.Left;
			this.btnAdd.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(178)))), ((int)(((byte)(255)))));
			this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.btnAdd.ForeColor = System.Drawing.Color.White;
			this.btnAdd.Location = new System.Drawing.Point(25, 0);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Padding = new System.Windows.Forms.Padding(5);
			this.btnAdd.Size = new System.Drawing.Size(150, 35);
			this.btnAdd.TabIndex = 17;
			this.btnAdd.Text = "Add";
			// 
			// pnlSpace10
			// 
			this.pnlSpace10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
			this.pnlSpace10.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnlSpace10.FillColor = System.Drawing.Color.White;
			this.pnlSpace10.Location = new System.Drawing.Point(0, 0);
			this.pnlSpace10.Name = "pnlSpace10";
			this.pnlSpace10.Size = new System.Drawing.Size(25, 35);
			this.pnlSpace10.TabIndex = 10;
			// 
			// pnlSpace8
			// 
			this.pnlSpace8.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlSpace8.FillColor = System.Drawing.Color.White;
			this.pnlSpace8.Location = new System.Drawing.Point(0, 290);
			this.pnlSpace8.Name = "pnlSpace8";
			this.pnlSpace8.Size = new System.Drawing.Size(433, 15);
			this.pnlSpace8.TabIndex = 132;
			// 
			// pnlSession
			// 
			this.pnlSession.Controls.Add(this.cboSessions);
			this.pnlSession.Controls.Add(this.lblSession);
			this.pnlSession.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlSession.FillColor = System.Drawing.Color.White;
			this.pnlSession.Location = new System.Drawing.Point(0, 250);
			this.pnlSession.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
			this.pnlSession.Name = "pnlSession";
			this.pnlSession.Size = new System.Drawing.Size(433, 40);
			this.pnlSession.TabIndex = 131;
			// 
			// cboSessions
			// 
			this.cboSessions.BackColor = System.Drawing.Color.White;
			this.cboSessions.BorderRadius = 5;
			this.cboSessions.Cursor = System.Windows.Forms.Cursors.Hand;
			this.cboSessions.DisabledState.BorderColor = System.Drawing.Color.White;
			this.cboSessions.DisabledState.FillColor = System.Drawing.Color.White;
			this.cboSessions.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(50)))), ((int)(((byte)(52)))));
			this.cboSessions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cboSessions.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cboSessions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSessions.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(221)))), ((int)(((byte)(226)))));
			this.cboSessions.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(221)))), ((int)(((byte)(226)))));
			this.cboSessions.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
			this.cboSessions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(50)))), ((int)(((byte)(52)))));
			this.cboSessions.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
			this.cboSessions.ItemHeight = 30;
			this.cboSessions.Items.AddRange(new object[] {
            "7H30 - 9H30",
            "9H30 - 11H30",
            "13H - 15H",
            "15H - 17H"});
			this.cboSessions.Location = new System.Drawing.Point(109, 0);
			this.cboSessions.Name = "cboSessions";
			this.cboSessions.Size = new System.Drawing.Size(324, 36);
			this.cboSessions.StartIndex = 0;
			this.cboSessions.TabIndex = 8;
			// 
			// lblSession
			// 
			this.lblSession.BackColor = System.Drawing.Color.White;
			this.lblSession.Dock = System.Windows.Forms.DockStyle.Left;
			this.lblSession.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblSession.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(137)))), ((int)(((byte)(137)))));
			this.lblSession.Location = new System.Drawing.Point(0, 0);
			this.lblSession.Name = "lblSession";
			this.lblSession.Size = new System.Drawing.Size(109, 40);
			this.lblSession.TabIndex = 1;
			this.lblSession.Text = "     Sessions: ";
			this.lblSession.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnlSpace7
			// 
			this.pnlSpace7.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlSpace7.FillColor = System.Drawing.Color.White;
			this.pnlSpace7.Location = new System.Drawing.Point(0, 245);
			this.pnlSpace7.Name = "pnlSpace7";
			this.pnlSpace7.Size = new System.Drawing.Size(433, 5);
			this.pnlSpace7.TabIndex = 130;
			// 
			// pnlVehicles
			// 
			this.pnlVehicles.Controls.Add(this.cboVehicles);
			this.pnlVehicles.Controls.Add(this.lblVehicles);
			this.pnlVehicles.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlVehicles.FillColor = System.Drawing.Color.White;
			this.pnlVehicles.Location = new System.Drawing.Point(0, 205);
			this.pnlVehicles.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
			this.pnlVehicles.Name = "pnlVehicles";
			this.pnlVehicles.Size = new System.Drawing.Size(433, 40);
			this.pnlVehicles.TabIndex = 129;
			// 
			// cboVehicles
			// 
			this.cboVehicles.BackColor = System.Drawing.Color.White;
			this.cboVehicles.BorderRadius = 5;
			this.cboVehicles.Cursor = System.Windows.Forms.Cursors.Hand;
			this.cboVehicles.DisabledState.BorderColor = System.Drawing.Color.White;
			this.cboVehicles.DisabledState.FillColor = System.Drawing.Color.White;
			this.cboVehicles.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(50)))), ((int)(((byte)(52)))));
			this.cboVehicles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cboVehicles.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cboVehicles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboVehicles.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(221)))), ((int)(((byte)(226)))));
			this.cboVehicles.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(221)))), ((int)(((byte)(226)))));
			this.cboVehicles.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
			this.cboVehicles.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(50)))), ((int)(((byte)(52)))));
			this.cboVehicles.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
			this.cboVehicles.ItemHeight = 30;
			this.cboVehicles.Items.AddRange(new object[] {
            "Toyota Supra",
            "Huyndai",
            "Bus 1"});
			this.cboVehicles.Location = new System.Drawing.Point(109, 0);
			this.cboVehicles.Name = "cboVehicles";
			this.cboVehicles.Size = new System.Drawing.Size(324, 36);
			this.cboVehicles.StartIndex = 0;
			this.cboVehicles.TabIndex = 6;
			// 
			// lblVehicles
			// 
			this.lblVehicles.BackColor = System.Drawing.Color.White;
			this.lblVehicles.Dock = System.Windows.Forms.DockStyle.Left;
			this.lblVehicles.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblVehicles.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(137)))), ((int)(((byte)(137)))));
			this.lblVehicles.Location = new System.Drawing.Point(0, 0);
			this.lblVehicles.Name = "lblVehicles";
			this.lblVehicles.Size = new System.Drawing.Size(109, 40);
			this.lblVehicles.TabIndex = 0;
			this.lblVehicles.Text = "     Vehicles: ";
			this.lblVehicles.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnlSpace4
			// 
			this.pnlSpace4.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlSpace4.FillColor = System.Drawing.Color.White;
			this.pnlSpace4.Location = new System.Drawing.Point(0, 200);
			this.pnlSpace4.Name = "pnlSpace4";
			this.pnlSpace4.Size = new System.Drawing.Size(433, 5);
			this.pnlSpace4.TabIndex = 128;
			// 
			// pnlTeachers
			// 
			this.pnlTeachers.Controls.Add(this.txtTotalAmount);
			this.pnlTeachers.Controls.Add(this.txtVND);
			this.pnlTeachers.Controls.Add(this.lblTotalAmount);
			this.pnlTeachers.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlTeachers.FillColor = System.Drawing.Color.White;
			this.pnlTeachers.Location = new System.Drawing.Point(0, 160);
			this.pnlTeachers.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
			this.pnlTeachers.Name = "pnlTeachers";
			this.pnlTeachers.Size = new System.Drawing.Size(433, 40);
			this.pnlTeachers.TabIndex = 126;
			// 
			// txtTotalAmount
			// 
			this.txtTotalAmount.BackColor = System.Drawing.Color.White;
			this.txtTotalAmount.BorderRadius = 5;
			this.txtTotalAmount.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtTotalAmount.DefaultText = "14.900.000";
			this.txtTotalAmount.DisabledState.BorderColor = System.Drawing.Color.White;
			this.txtTotalAmount.DisabledState.FillColor = System.Drawing.Color.White;
			this.txtTotalAmount.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(50)))), ((int)(((byte)(52)))));
			this.txtTotalAmount.DisabledState.PlaceholderForeColor = System.Drawing.Color.White;
			this.txtTotalAmount.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtTotalAmount.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
			this.txtTotalAmount.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
			this.txtTotalAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(50)))), ((int)(((byte)(52)))));
			this.txtTotalAmount.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
			this.txtTotalAmount.Location = new System.Drawing.Point(109, 0);
			this.txtTotalAmount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.txtTotalAmount.Name = "txtTotalAmount";
			this.txtTotalAmount.Padding = new System.Windows.Forms.Padding(0, 11, 0, 11);
			this.txtTotalAmount.PasswordChar = '\0';
			this.txtTotalAmount.PlaceholderText = "";
			this.txtTotalAmount.SelectedText = "";
			this.txtTotalAmount.Size = new System.Drawing.Size(256, 40);
			this.txtTotalAmount.TabIndex = 34;
			// 
			// txtVND
			// 
			this.txtVND.BackColor = System.Drawing.Color.White;
			this.txtVND.BorderRadius = 5;
			this.txtVND.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtVND.DefaultText = "VND";
			this.txtVND.DisabledState.BorderColor = System.Drawing.Color.White;
			this.txtVND.DisabledState.FillColor = System.Drawing.Color.White;
			this.txtVND.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(50)))), ((int)(((byte)(52)))));
			this.txtVND.DisabledState.PlaceholderForeColor = System.Drawing.Color.White;
			this.txtVND.Dock = System.Windows.Forms.DockStyle.Right;
			this.txtVND.Enabled = false;
			this.txtVND.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
			this.txtVND.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
			this.txtVND.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(50)))), ((int)(((byte)(52)))));
			this.txtVND.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
			this.txtVND.Location = new System.Drawing.Point(365, 0);
			this.txtVND.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.txtVND.Name = "txtVND";
			this.txtVND.PasswordChar = '\0';
			this.txtVND.PlaceholderText = "";
			this.txtVND.SelectedText = "";
			this.txtVND.Size = new System.Drawing.Size(68, 40);
			this.txtVND.TabIndex = 35;
			// 
			// lblTotalAmount
			// 
			this.lblTotalAmount.BackColor = System.Drawing.Color.White;
			this.lblTotalAmount.Dock = System.Windows.Forms.DockStyle.Left;
			this.lblTotalAmount.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTotalAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(137)))), ((int)(((byte)(137)))));
			this.lblTotalAmount.Location = new System.Drawing.Point(0, 0);
			this.lblTotalAmount.Name = "lblTotalAmount";
			this.lblTotalAmount.Size = new System.Drawing.Size(109, 40);
			this.lblTotalAmount.TabIndex = 1;
			this.lblTotalAmount.Text = "     Amount: ";
			this.lblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnlSpace3
			// 
			this.pnlSpace3.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlSpace3.FillColor = System.Drawing.Color.White;
			this.pnlSpace3.Location = new System.Drawing.Point(0, 155);
			this.pnlSpace3.Name = "pnlSpace3";
			this.pnlSpace3.Size = new System.Drawing.Size(433, 5);
			this.pnlSpace3.TabIndex = 125;
			// 
			// pnlLearners
			// 
			this.pnlLearners.Controls.Add(this.guna2ComboBox1);
			this.pnlLearners.Controls.Add(this.lblLearners);
			this.pnlLearners.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlLearners.FillColor = System.Drawing.Color.White;
			this.pnlLearners.Location = new System.Drawing.Point(0, 115);
			this.pnlLearners.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
			this.pnlLearners.Name = "pnlLearners";
			this.pnlLearners.Size = new System.Drawing.Size(433, 40);
			this.pnlLearners.TabIndex = 124;
			// 
			// guna2ComboBox1
			// 
			this.guna2ComboBox1.BackColor = System.Drawing.Color.White;
			this.guna2ComboBox1.BorderRadius = 5;
			this.guna2ComboBox1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.guna2ComboBox1.DisabledState.BorderColor = System.Drawing.Color.White;
			this.guna2ComboBox1.DisabledState.FillColor = System.Drawing.Color.White;
			this.guna2ComboBox1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(50)))), ((int)(((byte)(52)))));
			this.guna2ComboBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.guna2ComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.guna2ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.guna2ComboBox1.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(221)))), ((int)(((byte)(226)))));
			this.guna2ComboBox1.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(221)))), ((int)(((byte)(226)))));
			this.guna2ComboBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
			this.guna2ComboBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(50)))), ((int)(((byte)(52)))));
			this.guna2ComboBox1.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
			this.guna2ComboBox1.ItemHeight = 30;
			this.guna2ComboBox1.Items.AddRange(new object[] {
            "Hoang Vu",
            "Thanh Cong",
            "Xuan Duoc"});
			this.guna2ComboBox1.Location = new System.Drawing.Point(109, 0);
			this.guna2ComboBox1.Name = "guna2ComboBox1";
			this.guna2ComboBox1.Size = new System.Drawing.Size(324, 36);
			this.guna2ComboBox1.StartIndex = 0;
			this.guna2ComboBox1.TabIndex = 4;
			// 
			// lblLearners
			// 
			this.lblLearners.BackColor = System.Drawing.Color.White;
			this.lblLearners.Dock = System.Windows.Forms.DockStyle.Left;
			this.lblLearners.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblLearners.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(137)))), ((int)(((byte)(137)))));
			this.lblLearners.Location = new System.Drawing.Point(0, 0);
			this.lblLearners.Name = "lblLearners";
			this.lblLearners.Size = new System.Drawing.Size(109, 40);
			this.lblLearners.TabIndex = 0;
			this.lblLearners.Text = "     Learners: ";
			this.lblLearners.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnlSpace2
			// 
			this.pnlSpace2.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlSpace2.FillColor = System.Drawing.Color.White;
			this.pnlSpace2.Location = new System.Drawing.Point(0, 110);
			this.pnlSpace2.Name = "pnlSpace2";
			this.pnlSpace2.Size = new System.Drawing.Size(433, 5);
			this.pnlSpace2.TabIndex = 123;
			// 
			// pnlCourse
			// 
			this.pnlCourse.Controls.Add(this.cboCourses);
			this.pnlCourse.Controls.Add(this.lblCourses);
			this.pnlCourse.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlCourse.FillColor = System.Drawing.Color.White;
			this.pnlCourse.Location = new System.Drawing.Point(0, 70);
			this.pnlCourse.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
			this.pnlCourse.Name = "pnlCourse";
			this.pnlCourse.Size = new System.Drawing.Size(433, 40);
			this.pnlCourse.TabIndex = 122;
			// 
			// cboCourses
			// 
			this.cboCourses.BackColor = System.Drawing.Color.White;
			this.cboCourses.BorderRadius = 5;
			this.cboCourses.Cursor = System.Windows.Forms.Cursors.Hand;
			this.cboCourses.DisabledState.BorderColor = System.Drawing.Color.White;
			this.cboCourses.DisabledState.FillColor = System.Drawing.Color.White;
			this.cboCourses.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(50)))), ((int)(((byte)(52)))));
			this.cboCourses.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cboCourses.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cboCourses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboCourses.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(221)))), ((int)(((byte)(226)))));
			this.cboCourses.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(221)))), ((int)(((byte)(226)))));
			this.cboCourses.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
			this.cboCourses.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(50)))), ((int)(((byte)(52)))));
			this.cboCourses.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
			this.cboCourses.ItemHeight = 30;
			this.cboCourses.Items.AddRange(new object[] {
            "B-12312321",
            "C-12312313",
            "D-34234234",
            "E-12341231"});
			this.cboCourses.Location = new System.Drawing.Point(109, 0);
			this.cboCourses.Name = "cboCourses";
			this.cboCourses.Size = new System.Drawing.Size(324, 36);
			this.cboCourses.StartIndex = 0;
			this.cboCourses.TabIndex = 2;
			// 
			// lblCourses
			// 
			this.lblCourses.BackColor = System.Drawing.Color.White;
			this.lblCourses.Dock = System.Windows.Forms.DockStyle.Left;
			this.lblCourses.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblCourses.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(137)))), ((int)(((byte)(137)))));
			this.lblCourses.Location = new System.Drawing.Point(0, 0);
			this.lblCourses.Name = "lblCourses";
			this.lblCourses.Size = new System.Drawing.Size(109, 40);
			this.lblCourses.TabIndex = 0;
			this.lblCourses.Text = "     Courses: ";
			this.lblCourses.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnlSpace1
			// 
			this.pnlSpace1.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlSpace1.FillColor = System.Drawing.Color.White;
			this.pnlSpace1.Location = new System.Drawing.Point(0, 55);
			this.pnlSpace1.Name = "pnlSpace1";
			this.pnlSpace1.Size = new System.Drawing.Size(433, 15);
			this.pnlSpace1.TabIndex = 117;
			// 
			// pnlSpace6
			// 
			this.pnlSpace6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
			this.pnlSpace6.BorderRadius = 15;
			this.pnlSpace6.CustomizableEdges.BottomLeft = false;
			this.pnlSpace6.CustomizableEdges.TopLeft = false;
			this.pnlSpace6.CustomizableEdges.TopRight = false;
			this.pnlSpace6.Dock = System.Windows.Forms.DockStyle.Right;
			this.pnlSpace6.FillColor = System.Drawing.Color.White;
			this.pnlSpace6.Location = new System.Drawing.Point(433, 55);
			this.pnlSpace6.Name = "pnlSpace6";
			this.pnlSpace6.Size = new System.Drawing.Size(17, 295);
			this.pnlSpace6.TabIndex = 115;
			// 
			// pnlTitle_DateAssign
			// 
			this.pnlTitle_DateAssign.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
			this.pnlTitle_DateAssign.Controls.Add(this.lblDateAssign);
			this.pnlTitle_DateAssign.Controls.Add(this.pnlSpace9);
			this.pnlTitle_DateAssign.Controls.Add(this.lblAdd);
			this.pnlTitle_DateAssign.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlTitle_DateAssign.FillColor = System.Drawing.Color.White;
			this.pnlTitle_DateAssign.Location = new System.Drawing.Point(0, 15);
			this.pnlTitle_DateAssign.Name = "pnlTitle_DateAssign";
			this.pnlTitle_DateAssign.Size = new System.Drawing.Size(450, 40);
			this.pnlTitle_DateAssign.TabIndex = 116;
			// 
			// lblDateAssign
			// 
			this.lblDateAssign.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
			this.lblDateAssign.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblDateAssign.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDateAssign.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(50)))), ((int)(((byte)(52)))));
			this.lblDateAssign.Location = new System.Drawing.Point(285, 0);
			this.lblDateAssign.Name = "lblDateAssign";
			this.lblDateAssign.Size = new System.Drawing.Size(148, 40);
			this.lblDateAssign.TabIndex = 115;
			this.lblDateAssign.Text = "01/01/2024";
			this.lblDateAssign.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// pnlSpace9
			// 
			this.pnlSpace9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
			this.pnlSpace9.Dock = System.Windows.Forms.DockStyle.Right;
			this.pnlSpace9.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
			this.pnlSpace9.Location = new System.Drawing.Point(433, 0);
			this.pnlSpace9.Name = "pnlSpace9";
			this.pnlSpace9.Size = new System.Drawing.Size(17, 40);
			this.pnlSpace9.TabIndex = 116;
			// 
			// lblAdd
			// 
			this.lblAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
			this.lblAdd.Dock = System.Windows.Forms.DockStyle.Left;
			this.lblAdd.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblAdd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(50)))), ((int)(((byte)(52)))));
			this.lblAdd.Location = new System.Drawing.Point(0, 0);
			this.lblAdd.Name = "lblAdd";
			this.lblAdd.Size = new System.Drawing.Size(285, 40);
			this.lblAdd.TabIndex = 114;
			this.lblAdd.Text = "   Assign Schedule";
			// 
			// pnlSpace5
			// 
			this.pnlSpace5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
			this.pnlSpace5.BorderRadius = 15;
			this.pnlSpace5.CustomizableEdges.BottomLeft = false;
			this.pnlSpace5.CustomizableEdges.BottomRight = false;
			this.pnlSpace5.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlSpace5.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
			this.pnlSpace5.Location = new System.Drawing.Point(0, 0);
			this.pnlSpace5.Name = "pnlSpace5";
			this.pnlSpace5.Size = new System.Drawing.Size(450, 15);
			this.pnlSpace5.TabIndex = 15;
			// 
			// dragControl
			// 
			this.dragControl.DockIndicatorTransparencyValue = 0.6D;
			this.dragControl.TargetControl = this.pnlLineTop;
			this.dragControl.UseTransparentDrag = true;
			// 
			// AssignScheduleForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(500, 400);
			this.Controls.Add(this.pnlMain);
			this.Controls.Add(this.pnlLineLeft);
			this.Controls.Add(this.pnlLineRight);
			this.Controls.Add(this.pnlLineTop);
			this.Controls.Add(this.pnlLineBottom);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "AssignScheduleForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "AssignScheduleForm";
			this.Load += new System.EventHandler(this.AssignScheduleForm_Load);
			this.pnlLineTop.ResumeLayout(false);
			this.pnlMain.ResumeLayout(false);
			this.pnlButtonAdd_Cancel.ResumeLayout(false);
			this.pnlSession.ResumeLayout(false);
			this.pnlVehicles.ResumeLayout(false);
			this.pnlTeachers.ResumeLayout(false);
			this.pnlLearners.ResumeLayout(false);
			this.pnlCourse.ResumeLayout(false);
			this.pnlTitle_DateAssign.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlLineLeft;
        private Guna.UI2.WinForms.Guna2Panel pnlLineRight;
        private Guna.UI2.WinForms.Guna2Panel pnlLineTop;
        private Guna.UI2.WinForms.Guna2ControlBox btnMinimizeForm;
        private Guna.UI2.WinForms.Guna2ControlBox btnCloseForm;
        private Guna.UI2.WinForms.Guna2Panel pnlLineBottom;
        private Guna.UI2.WinForms.Guna2Panel pnlMain;
        private Guna.UI2.WinForms.Guna2Panel pnlSpace6;
        private Guna.UI2.WinForms.Guna2Panel pnlSpace5;
        private Guna.UI2.WinForms.Guna2Panel pnlTitle_DateAssign;
        private System.Windows.Forms.Label lblAdd;
        private System.Windows.Forms.Label lblDateAssign;
        private Guna.UI2.WinForms.Guna2DragControl dragControl;
        private Guna.UI2.WinForms.Guna2ShadowForm shadowForm;
        private Guna.UI2.WinForms.Guna2Panel pnlSpace1;
        private Guna.UI2.WinForms.Guna2Panel pnlCourse;
        private System.Windows.Forms.Label lblCourses;
        private Guna.UI2.WinForms.Guna2ComboBox cboCourses;
        private Guna.UI2.WinForms.Guna2Panel pnlSpace2;
        private Guna.UI2.WinForms.Guna2Panel pnlLearners;
        private System.Windows.Forms.Label lblLearners;
        private Guna.UI2.WinForms.Guna2ComboBox guna2ComboBox1;
        private Guna.UI2.WinForms.Guna2Panel pnlTeachers;
        private Guna.UI2.WinForms.Guna2Panel pnlVehicles;
        private System.Windows.Forms.Label lblVehicles;
        private Guna.UI2.WinForms.Guna2Panel pnlSpace4;
        private Guna.UI2.WinForms.Guna2Panel pnlSpace3;
        private Guna.UI2.WinForms.Guna2ComboBox cboVehicles;
        private Guna.UI2.WinForms.Guna2Panel pnlSpace7;
        private Guna.UI2.WinForms.Guna2Panel pnlSession;
        private Guna.UI2.WinForms.Guna2Panel pnlSpace9;
        private Guna.UI2.WinForms.Guna2Panel pnlSpace8;
        private Guna.UI2.WinForms.Guna2Panel pnlButtonAdd_Cancel;
        private Guna.UI2.WinForms.Guna2Button btnCancel;
        private Guna.UI2.WinForms.Guna2Button btnAdd;
        private Guna.UI2.WinForms.Guna2Panel pnlSpace10;
        private System.Windows.Forms.Label lblSession;
        private Guna.UI2.WinForms.Guna2ComboBox cboSessions;
		private System.Windows.Forms.Label lblTotalAmount;
		private Guna.UI2.WinForms.Guna2TextBox txtTotalAmount;
		private Guna.UI2.WinForms.Guna2TextBox txtVND;
	}
}