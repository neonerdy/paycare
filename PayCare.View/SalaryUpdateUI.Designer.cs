namespace PayCare.View
{
    partial class SalaryUpdateUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SalaryUpdateUI));
            this.label51 = new System.Windows.Forms.Label();
            this.dtpEffectiveDate = new System.Windows.Forms.DateTimePicker();
            this.cboBranch = new System.Windows.Forms.ComboBox();
            this.chkBranch = new System.Windows.Forms.CheckBox();
            this.chkGrade = new System.Windows.Forms.CheckBox();
            this.cboGrade = new System.Windows.Forms.ComboBox();
            this.chkOccupation = new System.Windows.Forms.CheckBox();
            this.cboOccupation = new System.Windows.Forms.ComboBox();
            this.txtSalaryMain = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLunchAllowance = new System.Windows.Forms.TextBox();
            this.txtTransportAllowance = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtVehicleAllowance = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFuelAllowance = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.rbPercentage = new System.Windows.Forms.RadioButton();
            this.rbValue = new System.Windows.Forms.RadioButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbCancel = new System.Windows.Forms.ToolStripButton();
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBranchId = new System.Windows.Forms.TextBox();
            this.txtGradeId = new System.Windows.Forms.TextBox();
            this.txtOccupationId = new System.Windows.Forms.TextBox();
            this.lvwData = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(21, 38);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(79, 13);
            this.label51.TabIndex = 300;
            this.label51.Text = "Tanggal Efektif";
            // 
            // dtpEffectiveDate
            // 
            this.dtpEffectiveDate.CustomFormat = "dd/MM/yyyy";
            this.dtpEffectiveDate.Enabled = false;
            this.dtpEffectiveDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEffectiveDate.Location = new System.Drawing.Point(24, 60);
            this.dtpEffectiveDate.Name = "dtpEffectiveDate";
            this.dtpEffectiveDate.Size = new System.Drawing.Size(135, 20);
            this.dtpEffectiveDate.TabIndex = 299;
            this.dtpEffectiveDate.ValueChanged += new System.EventHandler(this.dtpEffectiveDate_ValueChanged);
            // 
            // cboBranch
            // 
            this.cboBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBranch.Enabled = false;
            this.cboBranch.FormattingEnabled = true;
            this.cboBranch.Location = new System.Drawing.Point(24, 115);
            this.cboBranch.Name = "cboBranch";
            this.cboBranch.Size = new System.Drawing.Size(135, 21);
            this.cboBranch.TabIndex = 302;
            this.cboBranch.SelectedIndexChanged += new System.EventHandler(this.cboBranch_SelectedIndexChanged);
            // 
            // chkBranch
            // 
            this.chkBranch.AutoSize = true;
            this.chkBranch.Enabled = false;
            this.chkBranch.Location = new System.Drawing.Point(24, 96);
            this.chkBranch.Name = "chkBranch";
            this.chkBranch.Size = new System.Drawing.Size(63, 17);
            this.chkBranch.TabIndex = 303;
            this.chkBranch.Text = "Cabang";
            this.chkBranch.UseVisualStyleBackColor = true;
            this.chkBranch.CheckedChanged += new System.EventHandler(this.chkBranch_CheckedChanged);
            // 
            // chkGrade
            // 
            this.chkGrade.AutoSize = true;
            this.chkGrade.Enabled = false;
            this.chkGrade.Location = new System.Drawing.Point(24, 146);
            this.chkGrade.Name = "chkGrade";
            this.chkGrade.Size = new System.Drawing.Size(66, 17);
            this.chkGrade.TabIndex = 305;
            this.chkGrade.Text = "Pangkat";
            this.chkGrade.UseVisualStyleBackColor = true;
            this.chkGrade.CheckedChanged += new System.EventHandler(this.chkGrade_CheckedChanged);
            // 
            // cboGrade
            // 
            this.cboGrade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGrade.Enabled = false;
            this.cboGrade.FormattingEnabled = true;
            this.cboGrade.Location = new System.Drawing.Point(24, 165);
            this.cboGrade.Name = "cboGrade";
            this.cboGrade.Size = new System.Drawing.Size(135, 21);
            this.cboGrade.TabIndex = 304;
            this.cboGrade.SelectedIndexChanged += new System.EventHandler(this.cboGrade_SelectedIndexChanged);
            // 
            // chkOccupation
            // 
            this.chkOccupation.AutoSize = true;
            this.chkOccupation.Enabled = false;
            this.chkOccupation.Location = new System.Drawing.Point(24, 207);
            this.chkOccupation.Name = "chkOccupation";
            this.chkOccupation.Size = new System.Drawing.Size(64, 17);
            this.chkOccupation.TabIndex = 307;
            this.chkOccupation.Text = "Jabatan";
            this.chkOccupation.UseVisualStyleBackColor = true;
            this.chkOccupation.CheckedChanged += new System.EventHandler(this.chkOccupation_CheckedChanged);
            // 
            // cboOccupation
            // 
            this.cboOccupation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOccupation.Enabled = false;
            this.cboOccupation.FormattingEnabled = true;
            this.cboOccupation.Location = new System.Drawing.Point(24, 226);
            this.cboOccupation.Name = "cboOccupation";
            this.cboOccupation.Size = new System.Drawing.Size(135, 21);
            this.cboOccupation.TabIndex = 306;
            this.cboOccupation.SelectedIndexChanged += new System.EventHandler(this.cboOccupation_SelectedIndexChanged);
            // 
            // txtSalaryMain
            // 
            this.txtSalaryMain.Enabled = false;
            this.txtSalaryMain.Location = new System.Drawing.Point(362, 116);
            this.txtSalaryMain.MaxLength = 13;
            this.txtSalaryMain.Name = "txtSalaryMain";
            this.txtSalaryMain.Size = new System.Drawing.Size(81, 20);
            this.txtSalaryMain.TabIndex = 309;
            this.txtSalaryMain.TextChanged += new System.EventHandler(this.txtSalaryMain_TextChanged);
            this.txtSalaryMain.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSalaryMain_KeyPress);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(228, 116);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(59, 13);
            this.label21.TabIndex = 308;
            this.label21.Text = "Gaji Pokok";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(228, 144);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 310;
            this.label1.Text = "Uang Makan/Hari";
            // 
            // txtLunchAllowance
            // 
            this.txtLunchAllowance.Enabled = false;
            this.txtLunchAllowance.Location = new System.Drawing.Point(362, 142);
            this.txtLunchAllowance.MaxLength = 13;
            this.txtLunchAllowance.Name = "txtLunchAllowance";
            this.txtLunchAllowance.Size = new System.Drawing.Size(81, 20);
            this.txtLunchAllowance.TabIndex = 311;
            this.txtLunchAllowance.TextChanged += new System.EventHandler(this.txtLunchAllowance_TextChanged);
            this.txtLunchAllowance.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLunchAllowance_KeyPress);
            // 
            // txtTransportAllowance
            // 
            this.txtTransportAllowance.Enabled = false;
            this.txtTransportAllowance.Location = new System.Drawing.Point(362, 168);
            this.txtTransportAllowance.MaxLength = 13;
            this.txtTransportAllowance.Name = "txtTransportAllowance";
            this.txtTransportAllowance.Size = new System.Drawing.Size(81, 20);
            this.txtTransportAllowance.TabIndex = 313;
            this.txtTransportAllowance.TextChanged += new System.EventHandler(this.txtTransportAllowance_TextChanged);
            this.txtTransportAllowance.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTransportAllowance_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(228, 170);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 312;
            this.label2.Text = "Uang Transport/Hari";
            // 
            // txtVehicleAllowance
            // 
            this.txtVehicleAllowance.Enabled = false;
            this.txtVehicleAllowance.Location = new System.Drawing.Point(362, 220);
            this.txtVehicleAllowance.MaxLength = 13;
            this.txtVehicleAllowance.Name = "txtVehicleAllowance";
            this.txtVehicleAllowance.Size = new System.Drawing.Size(81, 20);
            this.txtVehicleAllowance.TabIndex = 317;
            this.txtVehicleAllowance.TextChanged += new System.EventHandler(this.txtVehicleAllowance_TextChanged);
            this.txtVehicleAllowance.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVehicleAllowance_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(228, 222);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 13);
            this.label3.TabIndex = 316;
            this.label3.Text = "Tunj. Kendaraan/Hari";
            // 
            // txtFuelAllowance
            // 
            this.txtFuelAllowance.Enabled = false;
            this.txtFuelAllowance.Location = new System.Drawing.Point(362, 194);
            this.txtFuelAllowance.MaxLength = 13;
            this.txtFuelAllowance.Name = "txtFuelAllowance";
            this.txtFuelAllowance.Size = new System.Drawing.Size(81, 20);
            this.txtFuelAllowance.TabIndex = 315;
            this.txtFuelAllowance.TextChanged += new System.EventHandler(this.txtFuelAllowance_TextChanged);
            this.txtFuelAllowance.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFuelAllowance_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(228, 196);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 314;
            this.label4.Text = "Tunj. BBM/Hari";
            // 
            // rbPercentage
            // 
            this.rbPercentage.AutoSize = true;
            this.rbPercentage.Checked = true;
            this.rbPercentage.Enabled = false;
            this.rbPercentage.Location = new System.Drawing.Point(231, 60);
            this.rbPercentage.Name = "rbPercentage";
            this.rbPercentage.Size = new System.Drawing.Size(58, 17);
            this.rbPercentage.TabIndex = 318;
            this.rbPercentage.TabStop = true;
            this.rbPercentage.Text = "Persen";
            this.rbPercentage.UseVisualStyleBackColor = true;
            this.rbPercentage.CheckedChanged += new System.EventHandler(this.rbPercentage_CheckedChanged);
            // 
            // rbValue
            // 
            this.rbValue.AutoSize = true;
            this.rbValue.Enabled = false;
            this.rbValue.Location = new System.Drawing.Point(362, 58);
            this.rbValue.Name = "rbValue";
            this.rbValue.Size = new System.Drawing.Size(45, 17);
            this.rbValue.TabIndex = 319;
            this.rbValue.Text = "Nilai";
            this.rbValue.UseVisualStyleBackColor = true;
            this.rbValue.CheckedChanged += new System.EventHandler(this.rbValue_CheckedChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAdd,
            this.tsbSave,
            this.tsbCancel,
            this.tsbDelete});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(650, 25);
            this.toolStrip1.TabIndex = 322;
            this.toolStrip1.Text = "toolStrip1";
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
            // tsbCancel
            // 
            this.tsbCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCancel.Enabled = false;
            this.tsbCancel.Image = ((System.Drawing.Image)(resources.GetObject("tsbCancel.Image")));
            this.tsbCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCancel.Name = "tsbCancel";
            this.tsbCancel.Size = new System.Drawing.Size(23, 22);
            this.tsbCancel.Text = "Cancel";
            this.tsbCancel.Click += new System.EventHandler(this.tsbCancel_Click);
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
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(228, 38);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 331;
            this.label6.Text = "Kenaikan";
            // 
            // txtBranchId
            // 
            this.txtBranchId.Location = new System.Drawing.Point(39, 515);
            this.txtBranchId.Name = "txtBranchId";
            this.txtBranchId.Size = new System.Drawing.Size(135, 20);
            this.txtBranchId.TabIndex = 332;
            // 
            // txtGradeId
            // 
            this.txtGradeId.Location = new System.Drawing.Point(39, 539);
            this.txtGradeId.Name = "txtGradeId";
            this.txtGradeId.Size = new System.Drawing.Size(135, 20);
            this.txtGradeId.TabIndex = 333;
            // 
            // txtOccupationId
            // 
            this.txtOccupationId.Location = new System.Drawing.Point(39, 565);
            this.txtOccupationId.Name = "txtOccupationId";
            this.txtOccupationId.Size = new System.Drawing.Size(135, 20);
            this.txtOccupationId.TabIndex = 334;
            // 
            // lvwData
            // 
            this.lvwData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11});
            this.lvwData.FullRowSelect = true;
            this.lvwData.HideSelection = false;
            this.lvwData.Location = new System.Drawing.Point(0, 276);
            this.lvwData.Name = "lvwData";
            this.lvwData.Size = new System.Drawing.Size(649, 215);
            this.lvwData.TabIndex = 336;
            this.lvwData.UseCompatibleStateImageBehavior = false;
            this.lvwData.View = System.Windows.Forms.View.Details;
            this.lvwData.SelectedIndexChanged += new System.EventHandler(this.lvwData_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ID";
            this.columnHeader1.Width = 0;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Tgl Efektif";
            this.columnHeader2.Width = 104;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Cabang";
            this.columnHeader3.Width = 113;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Pangkat";
            this.columnHeader4.Width = 116;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Jabatan";
            this.columnHeader5.Width = 115;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Tipe";
            this.columnHeader6.Width = 87;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Gaji Pokok";
            this.columnHeader7.Width = 110;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Uang Makan";
            this.columnHeader8.Width = 110;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Uang Transport";
            this.columnHeader9.Width = 110;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Tunj. BBM";
            this.columnHeader10.Width = 110;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Tunj. Kendaraan";
            this.columnHeader11.Width = 110;
            // 
            // SalaryUpdateUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 512);
            this.Controls.Add(this.lvwData);
            this.Controls.Add(this.txtOccupationId);
            this.Controls.Add(this.txtGradeId);
            this.Controls.Add(this.txtBranchId);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.rbValue);
            this.Controls.Add(this.rbPercentage);
            this.Controls.Add(this.txtVehicleAllowance);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtFuelAllowance);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtTransportAllowance);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtLunchAllowance);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSalaryMain);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.chkOccupation);
            this.Controls.Add(this.cboOccupation);
            this.Controls.Add(this.chkGrade);
            this.Controls.Add(this.cboGrade);
            this.Controls.Add(this.chkBranch);
            this.Controls.Add(this.cboBranch);
            this.Controls.Add(this.label51);
            this.Controls.Add(this.dtpEffectiveDate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SalaryUpdateUI";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Update Gaji";
            this.Load += new System.EventHandler(this.SalaryUpdateUI_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.DateTimePicker dtpEffectiveDate;
        private System.Windows.Forms.ComboBox cboBranch;
        private System.Windows.Forms.CheckBox chkBranch;
        private System.Windows.Forms.CheckBox chkGrade;
        private System.Windows.Forms.ComboBox cboGrade;
        private System.Windows.Forms.CheckBox chkOccupation;
        private System.Windows.Forms.ComboBox cboOccupation;
        private System.Windows.Forms.TextBox txtSalaryMain;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLunchAllowance;
        private System.Windows.Forms.TextBox txtTransportAllowance;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtVehicleAllowance;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFuelAllowance;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rbPercentage;
        private System.Windows.Forms.RadioButton rbValue;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbAdd;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripButton tsbCancel;
        private System.Windows.Forms.ToolStripButton tsbDelete;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBranchId;
        private System.Windows.Forms.TextBox txtGradeId;
        private System.Windows.Forms.TextBox txtOccupationId;
        private System.Windows.Forms.ListView lvwData;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
    }
}