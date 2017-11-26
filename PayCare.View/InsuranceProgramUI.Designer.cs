namespace PayCare.View
{
    partial class InsuranceProgramUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InsuranceProgramUI));
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.txtByEmployee = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtProgram = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbEdit = new System.Windows.Forms.ToolStripButton();
            this.tsbCancel = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.txtByCompany = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.txtInsuranceId = new System.Windows.Forms.TextBox();
            this.txtInsurance = new System.Windows.Forms.TextBox();
            this.txtByEmployeeFemale = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lvwData = new System.Windows.Forms.ListView();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsbDelete
            // 
            this.tsbDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsbDelete.Image")));
            this.tsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDelete.Name = "tsbDelete";
            this.tsbDelete.Size = new System.Drawing.Size(23, 22);
            this.tsbDelete.Text = "tsbDelete";
            this.tsbDelete.ToolTipText = "Hapus";
            this.tsbDelete.Click += new System.EventHandler(this.tsbDelete_Click);
            // 
            // txtByEmployee
            // 
            this.txtByEmployee.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtByEmployee.Enabled = false;
            this.txtByEmployee.Location = new System.Drawing.Point(123, 97);
            this.txtByEmployee.MaxLength = 200;
            this.txtByEmployee.Multiline = true;
            this.txtByEmployee.Name = "txtByEmployee";
            this.txtByEmployee.Size = new System.Drawing.Size(83, 20);
            this.txtByEmployee.TabIndex = 140;
            this.txtByEmployee.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtByEmployee.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtByEmployee_KeyPress);
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(371, 125);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(50, 20);
            this.txtID.TabIndex = 144;
            this.txtID.Visible = false;
            // 
            // txtProgram
            // 
            this.txtProgram.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtProgram.Enabled = false;
            this.txtProgram.Location = new System.Drawing.Point(17, 53);
            this.txtProgram.MaxLength = 200;
            this.txtProgram.Name = "txtProgram";
            this.txtProgram.Size = new System.Drawing.Size(479, 20);
            this.txtProgram.TabIndex = 138;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 13);
            this.label5.TabIndex = 143;
            this.label5.Text = "Nama Program Asuransi";
            // 
            // tsbAdd
            // 
            this.tsbAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAdd.Image = ((System.Drawing.Image)(resources.GetObject("tsbAdd.Image")));
            this.tsbAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAdd.Name = "tsbAdd";
            this.tsbAdd.Size = new System.Drawing.Size(23, 22);
            this.tsbAdd.Text = "tsbAdd";
            this.tsbAdd.ToolTipText = "Tambah";
            this.tsbAdd.Click += new System.EventHandler(this.tsbAdd_Click);
            // 
            // tsbEdit
            // 
            this.tsbEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbEdit.Image = ((System.Drawing.Image)(resources.GetObject("tsbEdit.Image")));
            this.tsbEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEdit.Name = "tsbEdit";
            this.tsbEdit.Size = new System.Drawing.Size(23, 22);
            this.tsbEdit.Text = "tsbEdit";
            this.tsbEdit.ToolTipText = "Edit";
            this.tsbEdit.Click += new System.EventHandler(this.tsbEdit_Click);
            // 
            // tsbCancel
            // 
            this.tsbCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCancel.Enabled = false;
            this.tsbCancel.Image = ((System.Drawing.Image)(resources.GetObject("tsbCancel.Image")));
            this.tsbCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCancel.Name = "tsbCancel";
            this.tsbCancel.Size = new System.Drawing.Size(23, 22);
            this.tsbCancel.ToolTipText = "Cancel";
            this.tsbCancel.Click += new System.EventHandler(this.tsbCancel_Click);
            // 
            // tsbSave
            // 
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSave.Enabled = false;
            this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(23, 22);
            this.tsbSave.Text = "Save";
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // txtByCompany
            // 
            this.txtByCompany.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtByCompany.Enabled = false;
            this.txtByCompany.Location = new System.Drawing.Point(17, 97);
            this.txtByCompany.MaxLength = 200;
            this.txtByCompany.Multiline = true;
            this.txtByCompany.Name = "txtByCompany";
            this.txtByCompany.Size = new System.Drawing.Size(95, 20);
            this.txtByCompany.TabIndex = 139;
            this.txtByCompany.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtByCompany.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtByCompany_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(120, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 145;
            this.label1.Text = "Karyawan (%)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 81);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 13);
            this.label7.TabIndex = 142;
            this.label7.Text = "Perusahaan (%)";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAdd,
            this.tsbEdit,
            this.tsbSave,
            this.tsbCancel,
            this.tsbDelete});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(633, 25);
            this.toolStrip1.TabIndex = 141;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // txtInsuranceId
            // 
            this.txtInsuranceId.Location = new System.Drawing.Point(427, 125);
            this.txtInsuranceId.Name = "txtInsuranceId";
            this.txtInsuranceId.Size = new System.Drawing.Size(50, 20);
            this.txtInsuranceId.TabIndex = 149;
            this.txtInsuranceId.Visible = false;
            // 
            // txtInsurance
            // 
            this.txtInsurance.Location = new System.Drawing.Point(483, 125);
            this.txtInsurance.Name = "txtInsurance";
            this.txtInsurance.Size = new System.Drawing.Size(50, 20);
            this.txtInsurance.TabIndex = 150;
            this.txtInsurance.Visible = false;
            // 
            // txtByEmployeeFemale
            // 
            this.txtByEmployeeFemale.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtByEmployeeFemale.Enabled = false;
            this.txtByEmployeeFemale.Location = new System.Drawing.Point(212, 97);
            this.txtByEmployeeFemale.MaxLength = 200;
            this.txtByEmployeeFemale.Multiline = true;
            this.txtByEmployeeFemale.Name = "txtByEmployeeFemale";
            this.txtByEmployeeFemale.Size = new System.Drawing.Size(83, 20);
            this.txtByEmployeeFemale.TabIndex = 152;
            this.txtByEmployeeFemale.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtByEmployeeFemale.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtByEmployeeFemale_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(209, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 13);
            this.label2.TabIndex = 153;
            this.label2.Text = "Karyawan Perempuan (%)";
            // 
            // lvwData
            // 
            this.lvwData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader8,
            this.columnHeader1,
            this.columnHeader4,
            this.columnHeader2,
            this.columnHeader3});
            this.lvwData.FullRowSelect = true;
            this.lvwData.HideSelection = false;
            this.lvwData.Location = new System.Drawing.Point(0, 135);
            this.lvwData.Name = "lvwData";
            this.lvwData.Size = new System.Drawing.Size(631, 204);
            this.lvwData.TabIndex = 154;
            this.lvwData.UseCompatibleStateImageBehavior = false;
            this.lvwData.View = System.Windows.Forms.View.Details;
            this.lvwData.SelectedIndexChanged += new System.EventHandler(this.lvwData_SelectedIndexChanged);
            this.lvwData.DoubleClick += new System.EventHandler(this.lvwData_DoubleClick);
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "ID";
            this.columnHeader8.Width = 0;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Program";
            this.columnHeader1.Width = 299;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Perusahaan (%)";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader4.Width = 89;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Karyawan (%)";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader2.Width = 78;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Karyawan Perempuan (%)";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader3.Width = 135;
            // 
            // InsuranceProgramUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 341);
            this.Controls.Add(this.lvwData);
            this.Controls.Add(this.txtByEmployeeFemale);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtInsurance);
            this.Controls.Add(this.txtInsuranceId);
            this.Controls.Add(this.txtByEmployee);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.txtProgram);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtByCompany);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "InsuranceProgramUI";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Program";
            this.Load += new System.EventHandler(this.InsuranceProgramUI_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton tsbDelete;
        private System.Windows.Forms.TextBox txtByEmployee;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TextBox txtProgram;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripButton tsbAdd;
        private System.Windows.Forms.ToolStripButton tsbEdit;
        private System.Windows.Forms.ToolStripButton tsbCancel;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.TextBox txtByCompany;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TextBox txtInsuranceId;
        private System.Windows.Forms.TextBox txtInsurance;
        private System.Windows.Forms.TextBox txtByEmployeeFemale;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView lvwData;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
    }
}