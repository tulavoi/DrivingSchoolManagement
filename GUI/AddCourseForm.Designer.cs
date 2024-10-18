namespace GUI
{
    partial class AddCourseForm
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
			this.shadowForm = new Guna.UI2.WinForms.Guna2ShadowForm(this.components);
			this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
			this.pnlMain = new Guna.UI2.WinForms.Guna2Panel();
			this.pnlButtonAdd_Cancel = new Guna.UI2.WinForms.Guna2Panel();
			this.btnCancel = new Guna.UI2.WinForms.Guna2Button();
			this.btnAdd = new Guna.UI2.WinForms.Guna2Button();
			this.pnlSpace10 = new Guna.UI2.WinForms.Guna2Panel();
			this.pnlSpace9 = new Guna.UI2.WinForms.Guna2Panel();
			this.pnlSpace8 = new Guna.UI2.WinForms.Guna2Panel();
			this.pnlName = new Guna.UI2.WinForms.Guna2Panel();
			this.txtName = new Guna.UI2.WinForms.Guna2TextBox();
			this.lblName = new System.Windows.Forms.Label();
			this.pnlSpace7 = new Guna.UI2.WinForms.Guna2Panel();
			this.pnlDuration = new Guna.UI2.WinForms.Guna2Panel();
			this.txtDurationInHours = new Guna.UI2.WinForms.Guna2TextBox();
			this.txtHour = new Guna.UI2.WinForms.Guna2TextBox();
			this.lblDurationInHours = new System.Windows.Forms.Label();
			this.pnlSpace3 = new Guna.UI2.WinForms.Guna2Panel();
			this.pnlFee = new Guna.UI2.WinForms.Guna2Panel();
			this.txtFee = new Guna.UI2.WinForms.Guna2TextBox();
			this.txtVND = new Guna.UI2.WinForms.Guna2TextBox();
			this.lblFee = new System.Windows.Forms.Label();
			this.pnlSpace2 = new Guna.UI2.WinForms.Guna2Panel();
			this.pnlLicense = new Guna.UI2.WinForms.Guna2Panel();
			this.cboLicense = new Guna.UI2.WinForms.Guna2ComboBox();
			this.lblLicense = new System.Windows.Forms.Label();
			this.pnlSpace1 = new Guna.UI2.WinForms.Guna2Panel();
			this.pnlSpace6 = new Guna.UI2.WinForms.Guna2Panel();
			this.lblAdd = new System.Windows.Forms.Label();
			this.pnlSpace5 = new Guna.UI2.WinForms.Guna2Panel();
			this.pnlLineTop.SuspendLayout();
			this.pnlMain.SuspendLayout();
			this.pnlButtonAdd_Cancel.SuspendLayout();
			this.pnlName.SuspendLayout();
			this.pnlDuration.SuspendLayout();
			this.pnlFee.SuspendLayout();
			this.pnlLicense.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlLineLeft
			// 
			this.pnlLineLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
			this.pnlLineLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnlLineLeft.Location = new System.Drawing.Point(0, 25);
			this.pnlLineLeft.Name = "pnlLineLeft";
			this.pnlLineLeft.Size = new System.Drawing.Size(25, 340);
			this.pnlLineLeft.TabIndex = 48;
			// 
			// pnlLineRight
			// 
			this.pnlLineRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
			this.pnlLineRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.pnlLineRight.Location = new System.Drawing.Point(435, 25);
			this.pnlLineRight.Name = "pnlLineRight";
			this.pnlLineRight.Size = new System.Drawing.Size(25, 340);
			this.pnlLineRight.TabIndex = 50;
			// 
			// pnlLineTop
			// 
			this.pnlLineTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
			this.pnlLineTop.Controls.Add(this.btnMinimizeForm);
			this.pnlLineTop.Controls.Add(this.btnCloseForm);
			this.pnlLineTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlLineTop.Location = new System.Drawing.Point(0, 0);
			this.pnlLineTop.Name = "pnlLineTop";
			this.pnlLineTop.Size = new System.Drawing.Size(460, 25);
			this.pnlLineTop.TabIndex = 47;
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
			this.btnMinimizeForm.Location = new System.Drawing.Point(370, 0);
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
			this.btnCloseForm.Location = new System.Drawing.Point(415, 0);
			this.btnCloseForm.Name = "btnCloseForm";
			this.btnCloseForm.Size = new System.Drawing.Size(45, 25);
			this.btnCloseForm.TabIndex = 30;
			// 
			// pnlLineBottom
			// 
			this.pnlLineBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
			this.pnlLineBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlLineBottom.Location = new System.Drawing.Point(0, 365);
			this.pnlLineBottom.Name = "pnlLineBottom";
			this.pnlLineBottom.Size = new System.Drawing.Size(460, 25);
			this.pnlLineBottom.TabIndex = 49;
			// 
			// guna2DragControl1
			// 
			this.guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
			this.guna2DragControl1.TargetControl = this.pnlLineTop;
			this.guna2DragControl1.UseTransparentDrag = true;
			// 
			// pnlMain
			// 
			this.pnlMain.BorderRadius = 15;
			this.pnlMain.Controls.Add(this.pnlButtonAdd_Cancel);
			this.pnlMain.Controls.Add(this.pnlSpace9);
			this.pnlMain.Controls.Add(this.pnlSpace8);
			this.pnlMain.Controls.Add(this.pnlName);
			this.pnlMain.Controls.Add(this.pnlSpace7);
			this.pnlMain.Controls.Add(this.pnlDuration);
			this.pnlMain.Controls.Add(this.pnlSpace3);
			this.pnlMain.Controls.Add(this.pnlFee);
			this.pnlMain.Controls.Add(this.pnlSpace2);
			this.pnlMain.Controls.Add(this.pnlLicense);
			this.pnlMain.Controls.Add(this.pnlSpace1);
			this.pnlMain.Controls.Add(this.pnlSpace6);
			this.pnlMain.Controls.Add(this.lblAdd);
			this.pnlMain.Controls.Add(this.pnlSpace5);
			this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlMain.FillColor = System.Drawing.Color.White;
			this.pnlMain.Location = new System.Drawing.Point(25, 25);
			this.pnlMain.Name = "pnlMain";
			this.pnlMain.Size = new System.Drawing.Size(410, 340);
			this.pnlMain.TabIndex = 51;
			// 
			// pnlButtonAdd_Cancel
			// 
			this.pnlButtonAdd_Cancel.Controls.Add(this.btnCancel);
			this.pnlButtonAdd_Cancel.Controls.Add(this.btnAdd);
			this.pnlButtonAdd_Cancel.Controls.Add(this.pnlSpace10);
			this.pnlButtonAdd_Cancel.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlButtonAdd_Cancel.FillColor = System.Drawing.Color.White;
			this.pnlButtonAdd_Cancel.Location = new System.Drawing.Point(0, 275);
			this.pnlButtonAdd_Cancel.Name = "pnlButtonAdd_Cancel";
			this.pnlButtonAdd_Cancel.Size = new System.Drawing.Size(393, 35);
			this.pnlButtonAdd_Cancel.TabIndex = 129;
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
			this.btnCancel.Location = new System.Drawing.Point(243, 0);
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
			// pnlSpace9
			// 
			this.pnlSpace9.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlSpace9.FillColor = System.Drawing.Color.White;
			this.pnlSpace9.Location = new System.Drawing.Point(0, 250);
			this.pnlSpace9.Name = "pnlSpace9";
			this.pnlSpace9.Size = new System.Drawing.Size(393, 25);
			this.pnlSpace9.TabIndex = 128;
			// 
			// pnlSpace8
			// 
			this.pnlSpace8.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlSpace8.FillColor = System.Drawing.Color.White;
			this.pnlSpace8.Location = new System.Drawing.Point(0, 245);
			this.pnlSpace8.Name = "pnlSpace8";
			this.pnlSpace8.Size = new System.Drawing.Size(393, 5);
			this.pnlSpace8.TabIndex = 126;
			// 
			// pnlName
			// 
			this.pnlName.Controls.Add(this.txtName);
			this.pnlName.Controls.Add(this.lblName);
			this.pnlName.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlName.FillColor = System.Drawing.Color.White;
			this.pnlName.Location = new System.Drawing.Point(0, 205);
			this.pnlName.Name = "pnlName";
			this.pnlName.Size = new System.Drawing.Size(393, 40);
			this.pnlName.TabIndex = 125;
			// 
			// txtName
			// 
			this.txtName.BackColor = System.Drawing.Color.White;
			this.txtName.BorderRadius = 5;
			this.txtName.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtName.DefaultText = "B-154126092024";
			this.txtName.DisabledState.BorderColor = System.Drawing.Color.White;
			this.txtName.DisabledState.FillColor = System.Drawing.Color.White;
			this.txtName.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(50)))), ((int)(((byte)(52)))));
			this.txtName.DisabledState.PlaceholderForeColor = System.Drawing.Color.White;
			this.txtName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtName.Enabled = false;
			this.txtName.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
			this.txtName.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
			this.txtName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(50)))), ((int)(((byte)(52)))));
			this.txtName.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
			this.txtName.Location = new System.Drawing.Point(125, 0);
			this.txtName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.txtName.Name = "txtName";
			this.txtName.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
			this.txtName.PasswordChar = '\0';
			this.txtName.PlaceholderText = "";
			this.txtName.SelectedText = "";
			this.txtName.Size = new System.Drawing.Size(268, 40);
			this.txtName.TabIndex = 31;
			// 
			// lblName
			// 
			this.lblName.BackColor = System.Drawing.Color.White;
			this.lblName.Dock = System.Windows.Forms.DockStyle.Left;
			this.lblName.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(137)))), ((int)(((byte)(137)))));
			this.lblName.Location = new System.Drawing.Point(0, 0);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(125, 40);
			this.lblName.TabIndex = 30;
			this.lblName.Text = "     Name: ";
			this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnlSpace7
			// 
			this.pnlSpace7.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlSpace7.FillColor = System.Drawing.Color.White;
			this.pnlSpace7.Location = new System.Drawing.Point(0, 200);
			this.pnlSpace7.Name = "pnlSpace7";
			this.pnlSpace7.Size = new System.Drawing.Size(393, 5);
			this.pnlSpace7.TabIndex = 124;
			// 
			// pnlDuration
			// 
			this.pnlDuration.Controls.Add(this.txtDurationInHours);
			this.pnlDuration.Controls.Add(this.txtHour);
			this.pnlDuration.Controls.Add(this.lblDurationInHours);
			this.pnlDuration.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlDuration.FillColor = System.Drawing.Color.White;
			this.pnlDuration.Location = new System.Drawing.Point(0, 160);
			this.pnlDuration.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
			this.pnlDuration.Name = "pnlDuration";
			this.pnlDuration.Size = new System.Drawing.Size(393, 40);
			this.pnlDuration.TabIndex = 121;
			// 
			// txtDurationInHours
			// 
			this.txtDurationInHours.BackColor = System.Drawing.Color.White;
			this.txtDurationInHours.BorderRadius = 5;
			this.txtDurationInHours.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtDurationInHours.DefaultText = "130";
			this.txtDurationInHours.DisabledState.BorderColor = System.Drawing.Color.White;
			this.txtDurationInHours.DisabledState.FillColor = System.Drawing.Color.White;
			this.txtDurationInHours.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(50)))), ((int)(((byte)(52)))));
			this.txtDurationInHours.DisabledState.PlaceholderForeColor = System.Drawing.Color.White;
			this.txtDurationInHours.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtDurationInHours.Enabled = false;
			this.txtDurationInHours.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
			this.txtDurationInHours.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
			this.txtDurationInHours.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(50)))), ((int)(((byte)(52)))));
			this.txtDurationInHours.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
			this.txtDurationInHours.Location = new System.Drawing.Point(125, 0);
			this.txtDurationInHours.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.txtDurationInHours.Name = "txtDurationInHours";
			this.txtDurationInHours.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
			this.txtDurationInHours.PasswordChar = '\0';
			this.txtDurationInHours.PlaceholderText = "";
			this.txtDurationInHours.SelectedText = "";
			this.txtDurationInHours.Size = new System.Drawing.Size(157, 40);
			this.txtDurationInHours.TabIndex = 31;
			// 
			// txtHour
			// 
			this.txtHour.BackColor = System.Drawing.Color.White;
			this.txtHour.BorderRadius = 5;
			this.txtHour.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtHour.DefaultText = "Hour(s)";
			this.txtHour.DisabledState.BorderColor = System.Drawing.Color.White;
			this.txtHour.DisabledState.FillColor = System.Drawing.Color.White;
			this.txtHour.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(50)))), ((int)(((byte)(52)))));
			this.txtHour.DisabledState.PlaceholderForeColor = System.Drawing.Color.White;
			this.txtHour.Dock = System.Windows.Forms.DockStyle.Right;
			this.txtHour.Enabled = false;
			this.txtHour.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
			this.txtHour.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
			this.txtHour.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(50)))), ((int)(((byte)(52)))));
			this.txtHour.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
			this.txtHour.Location = new System.Drawing.Point(282, 0);
			this.txtHour.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.txtHour.Name = "txtHour";
			this.txtHour.PasswordChar = '\0';
			this.txtHour.PlaceholderText = "";
			this.txtHour.SelectedText = "";
			this.txtHour.Size = new System.Drawing.Size(111, 40);
			this.txtHour.TabIndex = 32;
			// 
			// lblDurationInHours
			// 
			this.lblDurationInHours.BackColor = System.Drawing.Color.White;
			this.lblDurationInHours.Dock = System.Windows.Forms.DockStyle.Left;
			this.lblDurationInHours.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDurationInHours.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(137)))), ((int)(((byte)(137)))));
			this.lblDurationInHours.Location = new System.Drawing.Point(0, 0);
			this.lblDurationInHours.Name = "lblDurationInHours";
			this.lblDurationInHours.Size = new System.Drawing.Size(125, 40);
			this.lblDurationInHours.TabIndex = 30;
			this.lblDurationInHours.Text = "     Duration: ";
			this.lblDurationInHours.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnlSpace3
			// 
			this.pnlSpace3.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlSpace3.FillColor = System.Drawing.Color.White;
			this.pnlSpace3.Location = new System.Drawing.Point(0, 155);
			this.pnlSpace3.Name = "pnlSpace3";
			this.pnlSpace3.Size = new System.Drawing.Size(393, 5);
			this.pnlSpace3.TabIndex = 120;
			// 
			// pnlFee
			// 
			this.pnlFee.Controls.Add(this.txtFee);
			this.pnlFee.Controls.Add(this.txtVND);
			this.pnlFee.Controls.Add(this.lblFee);
			this.pnlFee.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlFee.FillColor = System.Drawing.Color.White;
			this.pnlFee.Location = new System.Drawing.Point(0, 115);
			this.pnlFee.Name = "pnlFee";
			this.pnlFee.Size = new System.Drawing.Size(393, 40);
			this.pnlFee.TabIndex = 119;
			// 
			// txtFee
			// 
			this.txtFee.BackColor = System.Drawing.Color.White;
			this.txtFee.BorderRadius = 5;
			this.txtFee.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtFee.DefaultText = "14.900.000";
			this.txtFee.DisabledState.BorderColor = System.Drawing.Color.White;
			this.txtFee.DisabledState.FillColor = System.Drawing.Color.White;
			this.txtFee.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(50)))), ((int)(((byte)(52)))));
			this.txtFee.DisabledState.PlaceholderForeColor = System.Drawing.Color.White;
			this.txtFee.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtFee.Enabled = false;
			this.txtFee.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
			this.txtFee.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
			this.txtFee.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(50)))), ((int)(((byte)(52)))));
			this.txtFee.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
			this.txtFee.Location = new System.Drawing.Point(125, 0);
			this.txtFee.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.txtFee.Name = "txtFee";
			this.txtFee.PasswordChar = '\0';
			this.txtFee.PlaceholderText = "";
			this.txtFee.SelectedText = "";
			this.txtFee.Size = new System.Drawing.Size(157, 40);
			this.txtFee.TabIndex = 27;
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
			this.txtVND.Location = new System.Drawing.Point(282, 0);
			this.txtVND.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.txtVND.Name = "txtVND";
			this.txtVND.PasswordChar = '\0';
			this.txtVND.PlaceholderText = "";
			this.txtVND.SelectedText = "";
			this.txtVND.Size = new System.Drawing.Size(111, 40);
			this.txtVND.TabIndex = 28;
			// 
			// lblFee
			// 
			this.lblFee.BackColor = System.Drawing.Color.White;
			this.lblFee.Dock = System.Windows.Forms.DockStyle.Left;
			this.lblFee.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblFee.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(137)))), ((int)(((byte)(137)))));
			this.lblFee.Location = new System.Drawing.Point(0, 0);
			this.lblFee.Name = "lblFee";
			this.lblFee.Size = new System.Drawing.Size(125, 40);
			this.lblFee.TabIndex = 26;
			this.lblFee.Text = "     Fee: ";
			this.lblFee.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnlSpace2
			// 
			this.pnlSpace2.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlSpace2.FillColor = System.Drawing.Color.White;
			this.pnlSpace2.Location = new System.Drawing.Point(0, 110);
			this.pnlSpace2.Name = "pnlSpace2";
			this.pnlSpace2.Size = new System.Drawing.Size(393, 5);
			this.pnlSpace2.TabIndex = 118;
			// 
			// pnlLicense
			// 
			this.pnlLicense.Controls.Add(this.cboLicense);
			this.pnlLicense.Controls.Add(this.lblLicense);
			this.pnlLicense.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlLicense.FillColor = System.Drawing.Color.White;
			this.pnlLicense.Location = new System.Drawing.Point(0, 70);
			this.pnlLicense.Name = "pnlLicense";
			this.pnlLicense.Size = new System.Drawing.Size(393, 40);
			this.pnlLicense.TabIndex = 117;
			// 
			// cboLicense
			// 
			this.cboLicense.BackColor = System.Drawing.Color.White;
			this.cboLicense.BorderRadius = 5;
			this.cboLicense.Cursor = System.Windows.Forms.Cursors.Hand;
			this.cboLicense.DisabledState.BorderColor = System.Drawing.Color.White;
			this.cboLicense.DisabledState.FillColor = System.Drawing.Color.White;
			this.cboLicense.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(50)))), ((int)(((byte)(52)))));
			this.cboLicense.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cboLicense.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cboLicense.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboLicense.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(221)))), ((int)(((byte)(226)))));
			this.cboLicense.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(221)))), ((int)(((byte)(226)))));
			this.cboLicense.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
			this.cboLicense.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(50)))), ((int)(((byte)(52)))));
			this.cboLicense.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
			this.cboLicense.ItemHeight = 30;
			this.cboLicense.Items.AddRange(new object[] {
            "B",
            "C",
            "D",
            "E"});
			this.cboLicense.Location = new System.Drawing.Point(125, 0);
			this.cboLicense.Name = "cboLicense";
			this.cboLicense.Size = new System.Drawing.Size(268, 36);
			this.cboLicense.StartIndex = 0;
			this.cboLicense.TabIndex = 4;
			// 
			// lblLicense
			// 
			this.lblLicense.BackColor = System.Drawing.Color.White;
			this.lblLicense.Dock = System.Windows.Forms.DockStyle.Left;
			this.lblLicense.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblLicense.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(137)))), ((int)(((byte)(137)))));
			this.lblLicense.Location = new System.Drawing.Point(0, 0);
			this.lblLicense.Name = "lblLicense";
			this.lblLicense.Size = new System.Drawing.Size(125, 40);
			this.lblLicense.TabIndex = 3;
			this.lblLicense.Text = "     License: ";
			this.lblLicense.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnlSpace1
			// 
			this.pnlSpace1.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlSpace1.FillColor = System.Drawing.Color.White;
			this.pnlSpace1.Location = new System.Drawing.Point(0, 55);
			this.pnlSpace1.Name = "pnlSpace1";
			this.pnlSpace1.Size = new System.Drawing.Size(393, 15);
			this.pnlSpace1.TabIndex = 116;
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
			this.pnlSpace6.Location = new System.Drawing.Point(393, 55);
			this.pnlSpace6.Name = "pnlSpace6";
			this.pnlSpace6.Size = new System.Drawing.Size(17, 285);
			this.pnlSpace6.TabIndex = 115;
			// 
			// lblAdd
			// 
			this.lblAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
			this.lblAdd.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblAdd.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblAdd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(50)))), ((int)(((byte)(52)))));
			this.lblAdd.Location = new System.Drawing.Point(0, 15);
			this.lblAdd.Name = "lblAdd";
			this.lblAdd.Size = new System.Drawing.Size(410, 40);
			this.lblAdd.TabIndex = 114;
			this.lblAdd.Text = "   Add Course";
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
			this.pnlSpace5.Size = new System.Drawing.Size(410, 15);
			this.pnlSpace5.TabIndex = 15;
			// 
			// AddCourseForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
			this.ClientSize = new System.Drawing.Size(460, 390);
			this.Controls.Add(this.pnlMain);
			this.Controls.Add(this.pnlLineLeft);
			this.Controls.Add(this.pnlLineRight);
			this.Controls.Add(this.pnlLineTop);
			this.Controls.Add(this.pnlLineBottom);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "AddCourseForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "AddCourseForm";
			this.Load += new System.EventHandler(this.AddCourseForm_Load);
			this.pnlLineTop.ResumeLayout(false);
			this.pnlMain.ResumeLayout(false);
			this.pnlButtonAdd_Cancel.ResumeLayout(false);
			this.pnlName.ResumeLayout(false);
			this.pnlDuration.ResumeLayout(false);
			this.pnlFee.ResumeLayout(false);
			this.pnlLicense.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlLineLeft;
        private Guna.UI2.WinForms.Guna2Panel pnlLineRight;
        private Guna.UI2.WinForms.Guna2Panel pnlLineTop;
        private Guna.UI2.WinForms.Guna2ControlBox btnMinimizeForm;
        private Guna.UI2.WinForms.Guna2ControlBox btnCloseForm;
        private Guna.UI2.WinForms.Guna2Panel pnlLineBottom;
        private Guna.UI2.WinForms.Guna2ShadowForm shadowForm;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
        private Guna.UI2.WinForms.Guna2Panel pnlMain;
        private Guna.UI2.WinForms.Guna2Panel pnlButtonAdd_Cancel;
        private Guna.UI2.WinForms.Guna2Button btnCancel;
        private Guna.UI2.WinForms.Guna2Button btnAdd;
        private Guna.UI2.WinForms.Guna2Panel pnlSpace10;
        private Guna.UI2.WinForms.Guna2Panel pnlSpace9;
        private Guna.UI2.WinForms.Guna2Panel pnlSpace8;
        private Guna.UI2.WinForms.Guna2Panel pnlName;
        private Guna.UI2.WinForms.Guna2Panel pnlSpace7;
        private Guna.UI2.WinForms.Guna2Panel pnlDuration;
        private Guna.UI2.WinForms.Guna2Panel pnlSpace3;
        private Guna.UI2.WinForms.Guna2Panel pnlFee;
        private Guna.UI2.WinForms.Guna2Panel pnlSpace2;
        private Guna.UI2.WinForms.Guna2Panel pnlLicense;
        private Guna.UI2.WinForms.Guna2Panel pnlSpace1;
        private Guna.UI2.WinForms.Guna2Panel pnlSpace6;
        private System.Windows.Forms.Label lblAdd;
        private Guna.UI2.WinForms.Guna2Panel pnlSpace5;
        private Guna.UI2.WinForms.Guna2ComboBox cboLicense;
        private System.Windows.Forms.Label lblLicense;
        private Guna.UI2.WinForms.Guna2TextBox txtHour;
        private Guna.UI2.WinForms.Guna2TextBox txtDurationInHours;
        private System.Windows.Forms.Label lblDurationInHours;
        private Guna.UI2.WinForms.Guna2TextBox txtFee;
        private Guna.UI2.WinForms.Guna2TextBox txtVND;
        private System.Windows.Forms.Label lblFee;
        private Guna.UI2.WinForms.Guna2TextBox txtName;
        private System.Windows.Forms.Label lblName;
    }
}