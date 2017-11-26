namespace PayCare.View
{
    partial class OccupationUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OccupationUI));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbEdit = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbCancel = new System.Windows.Forms.ToolStripButton();
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.tabOccupation = new System.Windows.Forms.TabControl();
            this.tabDetail = new System.Windows.Forms.TabPage();
            this.txtVehicleAllowance = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtHealthAllowance = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtOccupation = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tabList = new System.Windows.Forms.TabPage();
            this.lvwData = new System.Windows.Forms.ListView();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader14 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.lstCode = new System.Windows.Forms.ListBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbBack = new System.Windows.Forms.ToolStripButton();
            this.tsbNext = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.tabOccupation.SuspendLayout();
            this.tabDetail.SuspendLayout();
            this.tabList.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbBack,
            this.tsbNext,
            this.toolStripSeparator1,
            this.tsbAdd,
            this.tsbEdit,
            this.tsbSave,
            this.tsbCancel,
            this.tsbDelete});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(462, 25);
            this.toolStrip1.TabIndex = 45;
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
            this.tsbCancel.ToolTipText = "Cancel";
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
            // tabOccupation
            // 
            this.tabOccupation.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabOccupation.Controls.Add(this.tabDetail);
            this.tabOccupation.Controls.Add(this.tabList);
            this.tabOccupation.Location = new System.Drawing.Point(6, 28);
            this.tabOccupation.Name = "tabOccupation";
            this.tabOccupation.SelectedIndex = 0;
            this.tabOccupation.Size = new System.Drawing.Size(449, 245);
            this.tabOccupation.TabIndex = 116;
            // 
            // tabDetail
            // 
            this.tabDetail.Controls.Add(this.txtVehicleAllowance);
            this.tabDetail.Controls.Add(this.label3);
            this.tabDetail.Controls.Add(this.txtHealthAllowance);
            this.tabDetail.Controls.Add(this.label2);
            this.tabDetail.Controls.Add(this.chkActive);
            this.tabDetail.Controls.Add(this.label1);
            this.tabDetail.Controls.Add(this.txtNotes);
            this.tabDetail.Controls.Add(this.txtID);
            this.tabDetail.Controls.Add(this.txtCode);
            this.tabDetail.Controls.Add(this.label5);
            this.tabDetail.Controls.Add(this.txtOccupation);
            this.tabDetail.Controls.Add(this.label7);
            this.tabDetail.Location = new System.Drawing.Point(4, 25);
            this.tabDetail.Name = "tabDetail";
            this.tabDetail.Padding = new System.Windows.Forms.Padding(3);
            this.tabDetail.Size = new System.Drawing.Size(441, 216);
            this.tabDetail.TabIndex = 0;
            this.tabDetail.Text = "Detail";
            this.tabDetail.UseVisualStyleBackColor = true;
            // 
            // txtVehicleAllowance
            // 
            this.txtVehicleAllowance.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtVehicleAllowance.Enabled = false;
            this.txtVehicleAllowance.Location = new System.Drawing.Point(182, 72);
            this.txtVehicleAllowance.MaxLength = 13;
            this.txtVehicleAllowance.Name = "txtVehicleAllowance";
            this.txtVehicleAllowance.Size = new System.Drawing.Size(135, 20);
            this.txtVehicleAllowance.TabIndex = 119;
            this.txtVehicleAllowance.TextChanged += new System.EventHandler(this.txtVehicleAllowance_TextChanged);
            this.txtVehicleAllowance.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVehicleAllowance_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(179, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 13);
            this.label3.TabIndex = 127;
            this.label3.Text = "Tunj. Kendaraan/Hari";
            // 
            // txtHealthAllowance
            // 
            this.txtHealthAllowance.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtHealthAllowance.Enabled = false;
            this.txtHealthAllowance.Location = new System.Drawing.Point(27, 72);
            this.txtHealthAllowance.MaxLength = 13;
            this.txtHealthAllowance.Name = "txtHealthAllowance";
            this.txtHealthAllowance.Size = new System.Drawing.Size(135, 20);
            this.txtHealthAllowance.TabIndex = 118;
            this.txtHealthAllowance.TextChanged += new System.EventHandler(this.txtHealthAllowance_TextChanged);
            this.txtHealthAllowance.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtHealthAllowance_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 126;
            this.label2.Text = "Tunj. Kesehatan";
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Enabled = false;
            this.chkActive.Location = new System.Drawing.Point(27, 165);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(47, 17);
            this.chkActive.TabIndex = 121;
            this.chkActive.Text = "Aktif";
            this.chkActive.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 125;
            this.label1.Text = "Keterangan";
            // 
            // txtNotes
            // 
            this.txtNotes.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtNotes.Enabled = false;
            this.txtNotes.Location = new System.Drawing.Point(25, 120);
            this.txtNotes.MaxLength = 200;
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(292, 35);
            this.txtNotes.TabIndex = 120;
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(267, 161);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(50, 20);
            this.txtID.TabIndex = 124;
            this.txtID.Visible = false;
            // 
            // txtCode
            // 
            this.txtCode.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtCode.Enabled = false;
            this.txtCode.Location = new System.Drawing.Point(25, 29);
            this.txtCode.MaxLength = 200;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(137, 20);
            this.txtCode.TabIndex = 116;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 123;
            this.label5.Text = "Kode";
            // 
            // txtOccupation
            // 
            this.txtOccupation.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtOccupation.Enabled = false;
            this.txtOccupation.Location = new System.Drawing.Point(182, 29);
            this.txtOccupation.MaxLength = 200;
            this.txtOccupation.Multiline = true;
            this.txtOccupation.Name = "txtOccupation";
            this.txtOccupation.Size = new System.Drawing.Size(135, 20);
            this.txtOccupation.TabIndex = 117;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(179, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 13);
            this.label7.TabIndex = 122;
            this.label7.Text = "Jabatan";
            // 
            // tabList
            // 
            this.tabList.Controls.Add(this.lvwData);
            this.tabList.Location = new System.Drawing.Point(4, 25);
            this.tabList.Name = "tabList";
            this.tabList.Padding = new System.Windows.Forms.Padding(3);
            this.tabList.Size = new System.Drawing.Size(441, 216);
            this.tabList.TabIndex = 1;
            this.tabList.Text = "List";
            this.tabList.UseVisualStyleBackColor = true;
            // 
            // lvwData
            // 
            this.lvwData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader8,
            this.columnHeader10,
            this.columnHeader14,
            this.columnHeader1,
            this.columnHeader2});
            this.lvwData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwData.FullRowSelect = true;
            this.lvwData.HideSelection = false;
            this.lvwData.Location = new System.Drawing.Point(3, 3);
            this.lvwData.Name = "lvwData";
            this.lvwData.Size = new System.Drawing.Size(435, 210);
            this.lvwData.TabIndex = 113;
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
            // columnHeader10
            // 
            this.columnHeader10.Text = "Kode";
            this.columnHeader10.Width = 67;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Jabatan";
            this.columnHeader14.Width = 134;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Tunj. Kesehatan";
            this.columnHeader1.Width = 98;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Tunj. Kendaraan";
            this.columnHeader2.Width = 107;
            // 
            // lstCode
            // 
            this.lstCode.FormattingEnabled = true;
            this.lstCode.Location = new System.Drawing.Point(12, 294);
            this.lstCode.Name = "lstCode";
            this.lstCode.Size = new System.Drawing.Size(120, 95);
            this.lstCode.TabIndex = 260;
            this.lstCode.SelectedIndexChanged += new System.EventHandler(this.lstCode_SelectedIndexChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbBack
            // 
            this.tsbBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbBack.Image = ((System.Drawing.Image)(resources.GetObject("tsbBack.Image")));
            this.tsbBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBack.Name = "tsbBack";
            this.tsbBack.Size = new System.Drawing.Size(23, 22);
            this.tsbBack.ToolTipText = "Sebelumnya";
            this.tsbBack.Click += new System.EventHandler(this.tsbBack_Click);
            // 
            // tsbNext
            // 
            this.tsbNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNext.Image = ((System.Drawing.Image)(resources.GetObject("tsbNext.Image")));
            this.tsbNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNext.Name = "tsbNext";
            this.tsbNext.Size = new System.Drawing.Size(23, 22);
            this.tsbNext.ToolTipText = "Selanjutnya";
            this.tsbNext.Click += new System.EventHandler(this.tsbNext_Click);
            // 
            // OccupationUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 284);
            this.Controls.Add(this.lstCode);
            this.Controls.Add(this.tabOccupation);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "OccupationUI";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Jabatan";
            this.Load += new System.EventHandler(this.OccupationUI_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabOccupation.ResumeLayout(false);
            this.tabDetail.ResumeLayout(false);
            this.tabDetail.PerformLayout();
            this.tabList.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbAdd;
        private System.Windows.Forms.ToolStripButton tsbEdit;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripButton tsbCancel;
        private System.Windows.Forms.ToolStripButton tsbDelete;
        private System.Windows.Forms.TabControl tabOccupation;
        private System.Windows.Forms.TabPage tabDetail;
        private System.Windows.Forms.TabPage tabList;
        private System.Windows.Forms.TextBox txtVehicleAllowance;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtHealthAllowance;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtOccupation;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListView lvwData;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ListBox lstCode;
        private System.Windows.Forms.ToolStripButton tsbBack;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbNext;
    }
}