namespace GUI.ReportViewers
{
    partial class TeacherListRV
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.teacherDTOBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.shadowForm = new Guna.UI2.WinForms.Guna2ShadowForm(this.components);
            this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.pnlLineTop = new Guna.UI2.WinForms.Guna2Panel();
            this.btnMinimizeForm = new Guna.UI2.WinForms.Guna2ControlBox();
            this.btnCloseForm = new Guna.UI2.WinForms.Guna2ControlBox();
            this.guna2ResizeForm1 = new Guna.UI2.WinForms.Guna2ResizeForm(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.teacherDTOBindingSource)).BeginInit();
            this.guna2Panel2.SuspendLayout();
            this.pnlLineTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // teacherDTOBindingSource
            // 
            this.teacherDTOBindingSource.DataSource = typeof(DTO.TeacherDTO);
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.Controls.Add(this.reportViewer1);
            this.guna2Panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel2.Location = new System.Drawing.Point(0, 30);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.Size = new System.Drawing.Size(1267, 823);
            this.guna2Panel2.TabIndex = 38;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "TeacherDataSet";
            reportDataSource1.Value = this.teacherDTOBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "GUI.Reports.TeacherListReport.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1267, 823);
            this.reportViewer1.TabIndex = 0;
            // 
            // guna2DragControl1
            // 
            this.guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2DragControl1.TargetControl = this.pnlLineTop;
            this.guna2DragControl1.UseTransparentDrag = true;
            // 
            // pnlLineTop
            // 
            this.pnlLineTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.pnlLineTop.Controls.Add(this.btnMinimizeForm);
            this.pnlLineTop.Controls.Add(this.btnCloseForm);
            this.pnlLineTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLineTop.Location = new System.Drawing.Point(0, 0);
            this.pnlLineTop.Name = "pnlLineTop";
            this.pnlLineTop.Size = new System.Drawing.Size(1267, 30);
            this.pnlLineTop.TabIndex = 39;
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
            this.btnMinimizeForm.Location = new System.Drawing.Point(1168, 0);
            this.btnMinimizeForm.Name = "btnMinimizeForm";
            this.btnMinimizeForm.Size = new System.Drawing.Size(49, 30);
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
            this.btnCloseForm.Location = new System.Drawing.Point(1217, 0);
            this.btnCloseForm.Name = "btnCloseForm";
            this.btnCloseForm.Size = new System.Drawing.Size(50, 30);
            this.btnCloseForm.TabIndex = 30;
            // 
            // guna2ResizeForm1
            // 
            this.guna2ResizeForm1.TargetForm = this;
            // 
            // TeacherListRV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1267, 853);
            this.Controls.Add(this.guna2Panel2);
            this.Controls.Add(this.pnlLineTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TeacherListRV";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TeacherListRV";
            this.Load += new System.EventHandler(this.TeacherListRV_Load);
            ((System.ComponentModel.ISupportInitialize)(this.teacherDTOBindingSource)).EndInit();
            this.guna2Panel2.ResumeLayout(false);
            this.pnlLineTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private Guna.UI2.WinForms.Guna2ShadowForm shadowForm;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
        private Guna.UI2.WinForms.Guna2Panel pnlLineTop;
        private Guna.UI2.WinForms.Guna2ControlBox btnMinimizeForm;
        private Guna.UI2.WinForms.Guna2ControlBox btnCloseForm;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource teacherDTOBindingSource;
        private Guna.UI2.WinForms.Guna2ResizeForm guna2ResizeForm1;
    }
}