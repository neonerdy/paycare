namespace PayCare.View
{
    partial class THRUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(THRUI));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbEdit = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbCancel = new System.Windows.Forms.ToolStripButton();
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbHistory = new System.Windows.Forms.ToolStripButton();
            this.label40 = new System.Windows.Forms.Label();
            this.txtPaymentType = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.txtAccount = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.txtBank = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.chkTransfer = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtOccupation = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtGrade = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDepartment = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.txtBranch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkIsPaid = new System.Windows.Forms.CheckBox();
            this.dtpEfective = new System.Windows.Forms.DateTimePicker();
            this.label12 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.txtDaysOfWork = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtMonthOfWork = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.txtTotalAmount = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtOtherAmount = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMainSalary = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtYearOfWork = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGradeLevel = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtEmployeeId = new System.Windows.Forms.TextBox();
            this.lstData = new System.Windows.Forms.ListBox();
            this.lblCode = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAdd,
            this.tsbEdit,
            this.tsbSave,
            this.tsbCancel,
            this.tsbDelete,
            this.toolStripSeparator1,
            this.tsbHistory});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(673, 25);
            this.toolStrip1.TabIndex = 509;
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
            this.tsbAdd.Visible = false;
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbHistory
            // 
            this.tsbHistory.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbHistory.Image = ((System.Drawing.Image)(resources.GetObject("tsbHistory.Image")));
            this.tsbHistory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHistory.Name = "tsbHistory";
            this.tsbHistory.Size = new System.Drawing.Size(23, 22);
            this.tsbHistory.ToolTipText = "History Transaksi";
            this.tsbHistory.Click += new System.EventHandler(this.tsbHistory_Click);
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(154, 188);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(66, 13);
            this.label40.TabIndex = 531;
            this.label40.Text = "Pembayaran";
            // 
            // txtPaymentType
            // 
            this.txtPaymentType.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtPaymentType.Enabled = false;
            this.txtPaymentType.Location = new System.Drawing.Point(156, 204);
            this.txtPaymentType.MaxLength = 200;
            this.txtPaymentType.Name = "txtPaymentType";
            this.txtPaymentType.Size = new System.Drawing.Size(123, 20);
            this.txtPaymentType.TabIndex = 530;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(21, 238);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(91, 13);
            this.label33.TabIndex = 529;
            this.label33.Text = "Cara Pembayaran";
            // 
            // txtAccount
            // 
            this.txtAccount.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtAccount.Enabled = false;
            this.txtAccount.Location = new System.Drawing.Point(157, 298);
            this.txtAccount.Name = "txtAccount";
            this.txtAccount.Size = new System.Drawing.Size(123, 20);
            this.txtAccount.TabIndex = 528;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(154, 282);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(73, 13);
            this.label34.TabIndex = 527;
            this.label34.Text = "No. Rekening";
            // 
            // txtBank
            // 
            this.txtBank.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtBank.Enabled = false;
            this.txtBank.Location = new System.Drawing.Point(24, 298);
            this.txtBank.Name = "txtBank";
            this.txtBank.Size = new System.Drawing.Size(120, 20);
            this.txtBank.TabIndex = 526;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(21, 282);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(63, 13);
            this.label35.TabIndex = 525;
            this.label35.Text = "Nama Bank";
            // 
            // chkTransfer
            // 
            this.chkTransfer.AutoSize = true;
            this.chkTransfer.Enabled = false;
            this.chkTransfer.Location = new System.Drawing.Point(24, 258);
            this.chkTransfer.Name = "chkTransfer";
            this.chkTransfer.Size = new System.Drawing.Size(65, 17);
            this.chkTransfer.TabIndex = 524;
            this.chkTransfer.Text = "Transfer";
            this.chkTransfer.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(20, 184);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(37, 13);
            this.label11.TabIndex = 523;
            this.label11.Text = "Status";
            // 
            // txtStatus
            // 
            this.txtStatus.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtStatus.Enabled = false;
            this.txtStatus.Location = new System.Drawing.Point(24, 203);
            this.txtStatus.MaxLength = 200;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(123, 20);
            this.txtStatus.TabIndex = 522;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(153, 133);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 13);
            this.label9.TabIndex = 521;
            this.label9.Text = "Jabatan";
            // 
            // txtOccupation
            // 
            this.txtOccupation.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtOccupation.Enabled = false;
            this.txtOccupation.Location = new System.Drawing.Point(157, 152);
            this.txtOccupation.MaxLength = 200;
            this.txtOccupation.Name = "txtOccupation";
            this.txtOccupation.Size = new System.Drawing.Size(123, 20);
            this.txtOccupation.TabIndex = 520;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 134);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 13);
            this.label8.TabIndex = 519;
            this.label8.Text = "Pangkat";
            // 
            // txtGrade
            // 
            this.txtGrade.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtGrade.Enabled = false;
            this.txtGrade.Location = new System.Drawing.Point(24, 153);
            this.txtGrade.MaxLength = 200;
            this.txtGrade.Name = "txtGrade";
            this.txtGrade.Size = new System.Drawing.Size(123, 20);
            this.txtGrade.TabIndex = 518;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(153, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 517;
            this.label5.Text = "Departemen";
            // 
            // txtDepartment
            // 
            this.txtDepartment.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtDepartment.Enabled = false;
            this.txtDepartment.Location = new System.Drawing.Point(157, 103);
            this.txtDepartment.MaxLength = 200;
            this.txtDepartment.Name = "txtDepartment";
            this.txtDepartment.Size = new System.Drawing.Size(120, 20);
            this.txtDepartment.TabIndex = 516;
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtName.Enabled = false;
            this.txtName.Location = new System.Drawing.Point(156, 57);
            this.txtName.MaxLength = 200;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(124, 20);
            this.txtName.TabIndex = 515;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 513;
            this.label4.Text = "Cabang";
            // 
            // txtCode
            // 
            this.txtCode.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtCode.Enabled = false;
            this.txtCode.Location = new System.Drawing.Point(21, 58);
            this.txtCode.MaxLength = 200;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(123, 20);
            this.txtCode.TabIndex = 514;
            // 
            // txtBranch
            // 
            this.txtBranch.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtBranch.Enabled = false;
            this.txtBranch.Location = new System.Drawing.Point(21, 104);
            this.txtBranch.MaxLength = 200;
            this.txtBranch.Name = "txtBranch";
            this.txtBranch.Size = new System.Drawing.Size(123, 20);
            this.txtBranch.TabIndex = 512;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(153, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 511;
            this.label1.Text = "Nama";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(18, 42);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(25, 13);
            this.label10.TabIndex = 510;
            this.label10.Text = "NIK";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkIsPaid);
            this.groupBox1.Controls.Add(this.dtpEfective);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.txtDaysOfWork);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.txtMonthOfWork);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.dtpDate);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.txtTotalAmount);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.txtOtherAmount);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtAmount);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtMainSalary);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtYearOfWork);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(295, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(368, 302);
            this.groupBox1.TabIndex = 532;
            this.groupBox1.TabStop = false;
            // 
            // chkIsPaid
            // 
            this.chkIsPaid.AutoSize = true;
            this.chkIsPaid.Checked = true;
            this.chkIsPaid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIsPaid.Enabled = false;
            this.chkIsPaid.Location = new System.Drawing.Point(20, 259);
            this.chkIsPaid.Name = "chkIsPaid";
            this.chkIsPaid.Size = new System.Drawing.Size(110, 17);
            this.chkIsPaid.TabIndex = 480;
            this.chkIsPaid.Text = "Telah Dibayarkan";
            this.chkIsPaid.UseVisualStyleBackColor = true;
            // 
            // dtpEfective
            // 
            this.dtpEfective.CustomFormat = "dd/MM/yyyy";
            this.dtpEfective.Enabled = false;
            this.dtpEfective.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEfective.Location = new System.Drawing.Point(116, 13);
            this.dtpEfective.Name = "dtpEfective";
            this.dtpEfective.Size = new System.Drawing.Size(106, 20);
            this.dtpEfective.TabIndex = 478;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(15, 16);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(79, 13);
            this.label12.TabIndex = 479;
            this.label12.Text = "Tanggal Efektif";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(321, 68);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(24, 13);
            this.label20.TabIndex = 477;
            this.label20.Text = "hari";
            // 
            // txtDaysOfWork
            // 
            this.txtDaysOfWork.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtDaysOfWork.Enabled = false;
            this.txtDaysOfWork.Location = new System.Drawing.Point(281, 65);
            this.txtDaysOfWork.MaxLength = 200;
            this.txtDaysOfWork.Name = "txtDaysOfWork";
            this.txtDaysOfWork.Size = new System.Drawing.Size(37, 20);
            this.txtDaysOfWork.TabIndex = 476;
            this.txtDaysOfWork.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(237, 68);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(33, 13);
            this.label19.TabIndex = 475;
            this.label19.Text = "bulan";
            // 
            // txtMonthOfWork
            // 
            this.txtMonthOfWork.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtMonthOfWork.Enabled = false;
            this.txtMonthOfWork.Location = new System.Drawing.Point(197, 65);
            this.txtMonthOfWork.MaxLength = 200;
            this.txtMonthOfWork.Name = "txtMonthOfWork";
            this.txtMonthOfWork.Size = new System.Drawing.Size(37, 20);
            this.txtMonthOfWork.TabIndex = 474;
            this.txtMonthOfWork.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(158, 68);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(34, 13);
            this.label18.TabIndex = 473;
            this.label18.Text = "tahun";
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "dd/MM/yyyy";
            this.dtpDate.Enabled = false;
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(116, 39);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(106, 20);
            this.dtpDate.TabIndex = 471;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(17, 42);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(81, 13);
            this.label16.TabIndex = 472;
            this.label16.Text = "Tanggal Masuk";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(17, 216);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(57, 13);
            this.label17.TabIndex = 470;
            this.label17.Text = "Total THR";
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtTotalAmount.Enabled = false;
            this.txtTotalAmount.Location = new System.Drawing.Point(251, 233);
            this.txtTotalAmount.MaxLength = 200;
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.Size = new System.Drawing.Size(104, 20);
            this.txtTotalAmount.TabIndex = 469;
            this.txtTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotalAmount.TextChanged += new System.EventHandler(this.txtTotalAmount_TextChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(234, 195);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(13, 13);
            this.label15.TabIndex = 468;
            this.label15.Text = "+";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.Location = new System.Drawing.Point(114, 210);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(130, 1);
            this.panel1.TabIndex = 467;
            // 
            // txtOtherAmount
            // 
            this.txtOtherAmount.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtOtherAmount.Enabled = false;
            this.txtOtherAmount.Location = new System.Drawing.Point(116, 181);
            this.txtOtherAmount.MaxLength = 200;
            this.txtOtherAmount.Name = "txtOtherAmount";
            this.txtOtherAmount.Size = new System.Drawing.Size(104, 20);
            this.txtOtherAmount.TabIndex = 460;
            this.txtOtherAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtOtherAmount.TextChanged += new System.EventHandler(this.txtOtherAmount_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 184);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 13);
            this.label7.TabIndex = 459;
            this.label7.Text = "Tunj. Lain";
            // 
            // txtAmount
            // 
            this.txtAmount.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtAmount.Enabled = false;
            this.txtAmount.Location = new System.Drawing.Point(116, 158);
            this.txtAmount.MaxLength = 200;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(104, 20);
            this.txtAmount.TabIndex = 458;
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmount.TextChanged += new System.EventHandler(this.txtAmount_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 161);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 457;
            this.label6.Text = "Nilai THR";
            // 
            // txtMainSalary
            // 
            this.txtMainSalary.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtMainSalary.Enabled = false;
            this.txtMainSalary.Location = new System.Drawing.Point(116, 91);
            this.txtMainSalary.MaxLength = 200;
            this.txtMainSalary.Name = "txtMainSalary";
            this.txtMainSalary.Size = new System.Drawing.Size(104, 20);
            this.txtMainSalary.TabIndex = 456;
            this.txtMainSalary.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMainSalary.TextChanged += new System.EventHandler(this.txtMainSalary_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 455;
            this.label3.Text = "Gaji Pokok";
            // 
            // txtYearOfWork
            // 
            this.txtYearOfWork.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtYearOfWork.Enabled = false;
            this.txtYearOfWork.Location = new System.Drawing.Point(116, 65);
            this.txtYearOfWork.MaxLength = 200;
            this.txtYearOfWork.Name = "txtYearOfWork";
            this.txtYearOfWork.Size = new System.Drawing.Size(36, 20);
            this.txtYearOfWork.TabIndex = 454;
            this.txtYearOfWork.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 453;
            this.label2.Text = "Lama Kerja";
            // 
            // txtGradeLevel
            // 
            this.txtGradeLevel.Enabled = false;
            this.txtGradeLevel.Location = new System.Drawing.Point(10, 324);
            this.txtGradeLevel.Name = "txtGradeLevel";
            this.txtGradeLevel.Size = new System.Drawing.Size(42, 20);
            this.txtGradeLevel.TabIndex = 535;
            this.txtGradeLevel.Visible = false;
            // 
            // txtID
            // 
            this.txtID.Enabled = false;
            this.txtID.Location = new System.Drawing.Point(102, 324);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(42, 20);
            this.txtID.TabIndex = 533;
            this.txtID.Visible = false;
            // 
            // txtEmployeeId
            // 
            this.txtEmployeeId.Enabled = false;
            this.txtEmployeeId.Location = new System.Drawing.Point(56, 324);
            this.txtEmployeeId.Name = "txtEmployeeId";
            this.txtEmployeeId.Size = new System.Drawing.Size(43, 20);
            this.txtEmployeeId.TabIndex = 534;
            this.txtEmployeeId.Visible = false;
            // 
            // lstData
            // 
            this.lstData.FormattingEnabled = true;
            this.lstData.Location = new System.Drawing.Point(156, 327);
            this.lstData.Name = "lstData";
            this.lstData.Size = new System.Drawing.Size(121, 17);
            this.lstData.TabIndex = 536;
            this.lstData.Visible = false;
            // 
            // lblCode
            // 
            this.lblCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCode.ForeColor = System.Drawing.Color.Maroon;
            this.lblCode.Location = new System.Drawing.Point(479, 3);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(184, 20);
            this.lblCode.TabIndex = 537;
            this.lblCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // THRUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 416);
            this.Controls.Add(this.lblCode);
            this.Controls.Add(this.lstData);
            this.Controls.Add(this.txtGradeLevel);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.txtEmployeeId);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label40);
            this.Controls.Add(this.txtPaymentType);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.txtAccount);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.txtBank);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.chkTransfer);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtOccupation);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtGrade);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtDepartment);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.txtBranch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "THRUI";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "THR";
            this.Load += new System.EventHandler(this.ThrUI_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbHistory;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.TextBox txtPaymentType;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.TextBox txtAccount;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.TextBox txtBank;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.CheckBox chkTransfer;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtOccupation;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtGrade;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDepartment;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.TextBox txtBranch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtTotalAmount;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtOtherAmount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtMainSalary;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtYearOfWork;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtMonthOfWork;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtDaysOfWork;
        private System.Windows.Forms.TextBox txtGradeLevel;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TextBox txtEmployeeId;
        private System.Windows.Forms.ListBox lstData;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.DateTimePicker dtpEfective;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox chkIsPaid;
    }
}