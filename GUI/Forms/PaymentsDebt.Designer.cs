namespace GUI.Forms
{
    partial class PaymentsDebt
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
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.pnlSpace10 = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlSpace8 = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlLearners = new Guna.UI2.WinForms.Guna2Panel();
            this.dgvPayments = new Guna.UI2.WinForms.Guna2DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InvoiceCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InvoiceTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AmountOwed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Method = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Phone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlSpace2 = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlSpace1 = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlSpace6 = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlTitle_DateAssign = new Guna.UI2.WinForms.Guna2Panel();
            this.lblAdd = new System.Windows.Forms.Label();
            this.pnlSpace5 = new Guna.UI2.WinForms.Guna2Panel();
            this.toolTip = new Guna.UI2.WinForms.Guna2HtmlToolTip();
            this.btnPrint = new Guna.UI2.WinForms.Guna2Button();
            this.pnlLineTop.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlButtonAdd_Cancel.SuspendLayout();
            this.pnlLearners.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayments)).BeginInit();
            this.pnlTitle_DateAssign.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlLineLeft
            // 
            this.pnlLineLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.pnlLineLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLineLeft.Location = new System.Drawing.Point(0, 25);
            this.pnlLineLeft.Name = "pnlLineLeft";
            this.pnlLineLeft.Size = new System.Drawing.Size(25, 463);
            this.pnlLineLeft.TabIndex = 56;
            // 
            // pnlLineRight
            // 
            this.pnlLineRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.pnlLineRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlLineRight.Location = new System.Drawing.Point(1055, 25);
            this.pnlLineRight.Name = "pnlLineRight";
            this.pnlLineRight.Size = new System.Drawing.Size(25, 463);
            this.pnlLineRight.TabIndex = 58;
            // 
            // pnlLineTop
            // 
            this.pnlLineTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.pnlLineTop.Controls.Add(this.btnMinimizeForm);
            this.pnlLineTop.Controls.Add(this.btnCloseForm);
            this.pnlLineTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLineTop.Location = new System.Drawing.Point(0, 0);
            this.pnlLineTop.Name = "pnlLineTop";
            this.pnlLineTop.Size = new System.Drawing.Size(1080, 25);
            this.pnlLineTop.TabIndex = 55;
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
            this.btnMinimizeForm.Location = new System.Drawing.Point(990, 0);
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
            this.btnCloseForm.Location = new System.Drawing.Point(1035, 0);
            this.btnCloseForm.Name = "btnCloseForm";
            this.btnCloseForm.Size = new System.Drawing.Size(45, 25);
            this.btnCloseForm.TabIndex = 30;
            // 
            // pnlLineBottom
            // 
            this.pnlLineBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.pnlLineBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlLineBottom.Location = new System.Drawing.Point(0, 488);
            this.pnlLineBottom.Name = "pnlLineBottom";
            this.pnlLineBottom.Size = new System.Drawing.Size(1080, 25);
            this.pnlLineBottom.TabIndex = 57;
            // 
            // guna2DragControl1
            // 
            this.guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2DragControl1.TargetControl = this.pnlLineTop;
            this.guna2DragControl1.UseTransparentDrag = true;
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.pnlMain.BorderRadius = 15;
            this.pnlMain.Controls.Add(this.pnlButtonAdd_Cancel);
            this.pnlMain.Controls.Add(this.pnlSpace8);
            this.pnlMain.Controls.Add(this.pnlLearners);
            this.pnlMain.Controls.Add(this.pnlSpace2);
            this.pnlMain.Controls.Add(this.pnlSpace1);
            this.pnlMain.Controls.Add(this.pnlSpace6);
            this.pnlMain.Controls.Add(this.pnlTitle_DateAssign);
            this.pnlMain.Controls.Add(this.pnlSpace5);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.FillColor = System.Drawing.Color.White;
            this.pnlMain.Location = new System.Drawing.Point(25, 25);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1030, 463);
            this.pnlMain.TabIndex = 59;
            // 
            // pnlButtonAdd_Cancel
            // 
            this.pnlButtonAdd_Cancel.Controls.Add(this.btnCancel);
            this.pnlButtonAdd_Cancel.Controls.Add(this.btnPrint);
            this.pnlButtonAdd_Cancel.Controls.Add(this.pnlSpace10);
            this.pnlButtonAdd_Cancel.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlButtonAdd_Cancel.FillColor = System.Drawing.Color.White;
            this.pnlButtonAdd_Cancel.Location = new System.Drawing.Point(0, 358);
            this.pnlButtonAdd_Cancel.Name = "pnlButtonAdd_Cancel";
            this.pnlButtonAdd_Cancel.Size = new System.Drawing.Size(1013, 54);
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
            this.btnCancel.Location = new System.Drawing.Point(863, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Padding = new System.Windows.Forms.Padding(5);
            this.btnCancel.Size = new System.Drawing.Size(150, 54);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "Cancel";
            // 
            // pnlSpace10
            // 
            this.pnlSpace10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.pnlSpace10.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSpace10.FillColor = System.Drawing.Color.White;
            this.pnlSpace10.Location = new System.Drawing.Point(0, 0);
            this.pnlSpace10.Name = "pnlSpace10";
            this.pnlSpace10.Size = new System.Drawing.Size(23, 54);
            this.pnlSpace10.TabIndex = 10;
            // 
            // pnlSpace8
            // 
            this.pnlSpace8.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSpace8.FillColor = System.Drawing.Color.White;
            this.pnlSpace8.Location = new System.Drawing.Point(0, 345);
            this.pnlSpace8.Name = "pnlSpace8";
            this.pnlSpace8.Size = new System.Drawing.Size(1013, 13);
            this.pnlSpace8.TabIndex = 132;
            // 
            // pnlLearners
            // 
            this.pnlLearners.Controls.Add(this.dgvPayments);
            this.pnlLearners.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLearners.FillColor = System.Drawing.Color.White;
            this.pnlLearners.Location = new System.Drawing.Point(0, 75);
            this.pnlLearners.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.pnlLearners.Name = "pnlLearners";
            this.pnlLearners.Size = new System.Drawing.Size(1013, 270);
            this.pnlLearners.TabIndex = 124;
            // 
            // dgvPayments
            // 
            this.dgvPayments.AllowUserToAddRows = false;
            this.dgvPayments.AllowUserToDeleteRows = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            this.dgvPayments.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPayments.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvPayments.ColumnHeadersHeight = 30;
            this.dgvPayments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.InvoiceCode,
            this.InvoiceTo,
            this.AmountOwed,
            this.Method,
            this.Phone,
            this.Email});
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(96)))), ((int)(((byte)(236)))));
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPayments.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgvPayments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPayments.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.dgvPayments.Location = new System.Drawing.Point(0, 0);
            this.dgvPayments.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvPayments.Name = "dgvPayments";
            this.dgvPayments.ReadOnly = true;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPayments.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvPayments.RowHeadersVisible = false;
            this.dgvPayments.RowHeadersWidth = 30;
            this.dgvPayments.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(50)))), ((int)(((byte)(52)))));
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(96)))), ((int)(((byte)(236)))));
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.White;
            this.dgvPayments.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvPayments.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvPayments.RowTemplate.DividerHeight = 2;
            this.dgvPayments.RowTemplate.Height = 45;
            this.dgvPayments.Size = new System.Drawing.Size(1013, 270);
            this.dgvPayments.TabIndex = 44;
            this.dgvPayments.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvPayments.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvPayments.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvPayments.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvPayments.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvPayments.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvPayments.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.dgvPayments.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.dgvPayments.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvPayments.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvPayments.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.DimGray;
            this.dgvPayments.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvPayments.ThemeStyle.HeaderStyle.Height = 30;
            this.dgvPayments.ThemeStyle.ReadOnly = true;
            this.dgvPayments.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvPayments.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvPayments.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvPayments.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dgvPayments.ThemeStyle.RowsStyle.Height = 45;
            this.dgvPayments.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvPayments.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.White;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column2.DataPropertyName = "Space";
            this.Column2.Frozen = true;
            this.Column2.HeaderText = "";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 18;
            // 
            // InvoiceCode
            // 
            this.InvoiceCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.InvoiceCode.DataPropertyName = "InvoiceCode";
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InvoiceCode.DefaultCellStyle = dataGridViewCellStyle9;
            this.InvoiceCode.FillWeight = 53.63456F;
            this.InvoiceCode.Frozen = true;
            this.InvoiceCode.HeaderText = "Invoice";
            this.InvoiceCode.MinimumWidth = 6;
            this.InvoiceCode.Name = "InvoiceCode";
            this.InvoiceCode.ReadOnly = true;
            this.InvoiceCode.Width = 160;
            // 
            // InvoiceTo
            // 
            this.InvoiceTo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.InvoiceTo.DataPropertyName = "InvoiceTo";
            this.InvoiceTo.Frozen = true;
            this.InvoiceTo.HeaderText = "Invoice To";
            this.InvoiceTo.MinimumWidth = 6;
            this.InvoiceTo.Name = "InvoiceTo";
            this.InvoiceTo.ReadOnly = true;
            this.InvoiceTo.Width = 200;
            // 
            // AmountOwed
            // 
            this.AmountOwed.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.AmountOwed.DataPropertyName = "AmountOwed";
            this.AmountOwed.Frozen = true;
            this.AmountOwed.HeaderText = "AmountOwed";
            this.AmountOwed.MinimumWidth = 6;
            this.AmountOwed.Name = "AmountOwed";
            this.AmountOwed.ReadOnly = true;
            this.AmountOwed.Width = 120;
            // 
            // Method
            // 
            this.Method.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Method.DataPropertyName = "Method";
            this.Method.Frozen = true;
            this.Method.HeaderText = "Method";
            this.Method.MinimumWidth = 6;
            this.Method.Name = "Method";
            this.Method.ReadOnly = true;
            this.Method.Width = 130;
            // 
            // Phone
            // 
            this.Phone.DataPropertyName = "Phone";
            this.Phone.HeaderText = "Phone";
            this.Phone.MinimumWidth = 6;
            this.Phone.Name = "Phone";
            this.Phone.ReadOnly = true;
            // 
            // Email
            // 
            this.Email.DataPropertyName = "Email";
            this.Email.HeaderText = "Email";
            this.Email.MinimumWidth = 6;
            this.Email.Name = "Email";
            this.Email.ReadOnly = true;
            // 
            // pnlSpace2
            // 
            this.pnlSpace2.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSpace2.FillColor = System.Drawing.Color.White;
            this.pnlSpace2.Location = new System.Drawing.Point(0, 70);
            this.pnlSpace2.Name = "pnlSpace2";
            this.pnlSpace2.Size = new System.Drawing.Size(1013, 5);
            this.pnlSpace2.TabIndex = 123;
            // 
            // pnlSpace1
            // 
            this.pnlSpace1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSpace1.FillColor = System.Drawing.Color.White;
            this.pnlSpace1.Location = new System.Drawing.Point(0, 55);
            this.pnlSpace1.Name = "pnlSpace1";
            this.pnlSpace1.Size = new System.Drawing.Size(1013, 15);
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
            this.pnlSpace6.Location = new System.Drawing.Point(1013, 55);
            this.pnlSpace6.Name = "pnlSpace6";
            this.pnlSpace6.Size = new System.Drawing.Size(17, 408);
            this.pnlSpace6.TabIndex = 115;
            // 
            // pnlTitle_DateAssign
            // 
            this.pnlTitle_DateAssign.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.pnlTitle_DateAssign.Controls.Add(this.lblAdd);
            this.pnlTitle_DateAssign.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitle_DateAssign.FillColor = System.Drawing.Color.White;
            this.pnlTitle_DateAssign.Location = new System.Drawing.Point(0, 15);
            this.pnlTitle_DateAssign.Name = "pnlTitle_DateAssign";
            this.pnlTitle_DateAssign.Size = new System.Drawing.Size(1030, 40);
            this.pnlTitle_DateAssign.TabIndex = 116;
            // 
            // lblAdd
            // 
            this.lblAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.lblAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAdd.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(50)))), ((int)(((byte)(52)))));
            this.lblAdd.Location = new System.Drawing.Point(0, 0);
            this.lblAdd.Name = "lblAdd";
            this.lblAdd.Size = new System.Drawing.Size(1030, 40);
            this.lblAdd.TabIndex = 117;
            this.lblAdd.Text = "List of Students with Pending Payments";
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
            this.pnlSpace5.Size = new System.Drawing.Size(1030, 15);
            this.pnlSpace5.TabIndex = 15;
            // 
            // toolTip
            // 
            this.toolTip.AllowLinksHandling = true;
            this.toolTip.AutoPopDelay = 3000;
            this.toolTip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(100)))), ((int)(((byte)(119)))));
            this.toolTip.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(100)))), ((int)(((byte)(119)))));
            this.toolTip.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolTip.ForeColor = System.Drawing.Color.White;
            this.toolTip.InitialDelay = 500;
            this.toolTip.MaximumSize = new System.Drawing.Size(0, 0);
            this.toolTip.ReshowDelay = 100;
            this.toolTip.StripAmpersands = true;
            // 
            // btnPrint
            // 
            this.btnPrint.BorderRadius = 5;
            this.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrint.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnPrint.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnPrint.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnPrint.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnPrint.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnPrint.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(211)))), ((int)(((byte)(116)))));
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(23, 0);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Padding = new System.Windows.Forms.Padding(5);
            this.btnPrint.PressedColor = System.Drawing.Color.BlueViolet;
            this.btnPrint.Size = new System.Drawing.Size(133, 54);
            this.btnPrint.TabIndex = 17;
            this.btnPrint.Text = "Print";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // PaymentsDebt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1080, 513);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlLineLeft);
            this.Controls.Add(this.pnlLineRight);
            this.Controls.Add(this.pnlLineTop);
            this.Controls.Add(this.pnlLineBottom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PaymentsDebt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CreateInvoiceForm";
            this.Load += new System.EventHandler(this.PaymentDebtFrom_Load);
            this.pnlLineTop.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlButtonAdd_Cancel.ResumeLayout(false);
            this.pnlLearners.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayments)).EndInit();
            this.pnlTitle_DateAssign.ResumeLayout(false);
            this.ResumeLayout(false);

        }

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
        private Guna.UI2.WinForms.Guna2Panel pnlSpace10;
        private Guna.UI2.WinForms.Guna2Panel pnlSpace8;
        private Guna.UI2.WinForms.Guna2Panel pnlSpace1;
        private Guna.UI2.WinForms.Guna2Panel pnlSpace6;
        private Guna.UI2.WinForms.Guna2Panel pnlSpace5;
        private Guna.UI2.WinForms.Guna2Panel pnlTitle_DateAssign;
        private System.Windows.Forms.Label lblAdd;
        private Guna.UI2.WinForms.Guna2HtmlToolTip toolTip;
        private Guna.UI2.WinForms.Guna2Panel pnlLearners;
        private Guna.UI2.WinForms.Guna2Panel pnlSpace2;
        private Guna.UI2.WinForms.Guna2DataGridView dgvPayments;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvoiceCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvoiceTo;
        private System.Windows.Forms.DataGridViewTextBoxColumn AmountOwed;
        private System.Windows.Forms.DataGridViewTextBoxColumn Method;
        private System.Windows.Forms.DataGridViewTextBoxColumn Phone;
        private System.Windows.Forms.DataGridViewTextBoxColumn Email;
        private Guna.UI2.WinForms.Guna2Button btnPrint;
    }
}