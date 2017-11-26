namespace PayCare.View
{
    partial class CompanyUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CompanyUI));
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCompanyName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFax = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBankName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtOverTimeMaximumHour = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtMainSalaryDivider = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtOverTimeMultiplyHoliday = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtOverTimeMultiply = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtOverTimeHourDivider = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.udReportDivider = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label19 = new System.Windows.Forms.Label();
            this.nudCutOffDate = new System.Windows.Forms.NumericUpDown();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udReportDivider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCutOffDate)).BeginInit();
            this.SuspendLayout();
            // 
            // tsbSave
            // 
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(23, 22);
            this.tsbSave.Text = "Save";
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSave,
            this.toolStripSeparator2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(491, 25);
            this.toolStrip1.TabIndex = 92;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(4, 28);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(470, 289);
            this.tabControl1.TabIndex = 186;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.nudCutOffDate);
            this.tabPage1.Controls.Add(this.txtNotes);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.txtEmail);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.txtCompanyName);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.txtFax);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.txtBankName);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.txtPhone);
            this.tabPage1.Controls.Add(this.txtAddress);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.txtCode);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(462, 260);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Info";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtNotes
            // 
            this.txtNotes.BackColor = System.Drawing.Color.White;
            this.txtNotes.Location = new System.Drawing.Point(235, 190);
            this.txtNotes.MaxLength = 200;
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(206, 20);
            this.txtNotes.TabIndex = 188;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(232, 174);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 13);
            this.label10.TabIndex = 197;
            this.label10.Text = "Keterangan";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(232, 121);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(103, 13);
            this.label7.TabIndex = 196;
            this.label7.Text = "Tanggal Cut Off Gaji";
            // 
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.Color.White;
            this.txtEmail.Location = new System.Drawing.Point(22, 190);
            this.txtEmail.MaxLength = 200;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(199, 20);
            this.txtEmail.TabIndex = 184;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 171);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 13);
            this.label8.TabIndex = 195;
            this.label8.Text = "Email";
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.BackColor = System.Drawing.Color.White;
            this.txtCompanyName.Location = new System.Drawing.Point(22, 90);
            this.txtCompanyName.MaxLength = 200;
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new System.Drawing.Size(199, 20);
            this.txtCompanyName.TabIndex = 181;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(19, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 194;
            this.label5.Text = "Nama";
            // 
            // txtFax
            // 
            this.txtFax.BackColor = System.Drawing.Color.White;
            this.txtFax.Location = new System.Drawing.Point(120, 140);
            this.txtFax.MaxLength = 200;
            this.txtFax.Name = "txtFax";
            this.txtFax.Size = new System.Drawing.Size(101, 20);
            this.txtFax.TabIndex = 183;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(117, 124);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 13);
            this.label6.TabIndex = 193;
            this.label6.Text = "Fax";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 191;
            this.label3.Text = "Telepon";
            // 
            // txtBankName
            // 
            this.txtBankName.BackColor = System.Drawing.Color.White;
            this.txtBankName.Location = new System.Drawing.Point(341, 140);
            this.txtBankName.MaxLength = 200;
            this.txtBankName.Name = "txtBankName";
            this.txtBankName.Size = new System.Drawing.Size(100, 20);
            this.txtBankName.TabIndex = 187;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(341, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 192;
            this.label4.Text = "Nama Bank";
            // 
            // txtPhone
            // 
            this.txtPhone.BackColor = System.Drawing.Color.White;
            this.txtPhone.Location = new System.Drawing.Point(22, 140);
            this.txtPhone.MaxLength = 200;
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(92, 20);
            this.txtPhone.TabIndex = 182;
            // 
            // txtAddress
            // 
            this.txtAddress.BackColor = System.Drawing.Color.White;
            this.txtAddress.Location = new System.Drawing.Point(235, 39);
            this.txtAddress.MaxLength = 200;
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(206, 70);
            this.txtAddress.TabIndex = 185;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(232, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 190;
            this.label2.Text = "Alamat";
            // 
            // txtCode
            // 
            this.txtCode.BackColor = System.Drawing.Color.White;
            this.txtCode.Location = new System.Drawing.Point(22, 39);
            this.txtCode.MaxLength = 200;
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(199, 20);
            this.txtCode.TabIndex = 180;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 189;
            this.label1.Text = "Kode";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtOverTimeMaximumHour);
            this.tabPage2.Controls.Add(this.label20);
            this.tabPage2.Controls.Add(this.txtMainSalaryDivider);
            this.tabPage2.Controls.Add(this.label14);
            this.tabPage2.Controls.Add(this.txtOverTimeMultiplyHoliday);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Controls.Add(this.txtOverTimeMultiply);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.txtOverTimeHourDivider);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.udReportDivider);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(462, 318);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Setting";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtOverTimeMaximumHour
            // 
            this.txtOverTimeMaximumHour.BackColor = System.Drawing.Color.White;
            this.txtOverTimeMaximumHour.Location = new System.Drawing.Point(167, 85);
            this.txtOverTimeMaximumHour.MaxLength = 200;
            this.txtOverTimeMaximumHour.Name = "txtOverTimeMaximumHour";
            this.txtOverTimeMaximumHour.Size = new System.Drawing.Size(46, 20);
            this.txtOverTimeMaximumHour.TabIndex = 196;
            this.txtOverTimeMaximumHour.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtOverTimeMaximumHour.TextChanged += new System.EventHandler(this.txtOverTimeMaximumHour_TextChanged);
            this.txtOverTimeMaximumHour.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOverTimeMaximumHour_KeyPress);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(20, 88);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(117, 13);
            this.label20.TabIndex = 197;
            this.label20.Text = "Maksimum Jam Lembur";
            // 
            // txtMainSalaryDivider
            // 
            this.txtMainSalaryDivider.BackColor = System.Drawing.Color.White;
            this.txtMainSalaryDivider.Location = new System.Drawing.Point(167, 33);
            this.txtMainSalaryDivider.MaxLength = 200;
            this.txtMainSalaryDivider.Name = "txtMainSalaryDivider";
            this.txtMainSalaryDivider.Size = new System.Drawing.Size(46, 20);
            this.txtMainSalaryDivider.TabIndex = 194;
            this.txtMainSalaryDivider.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMainSalaryDivider.TextChanged += new System.EventHandler(this.txtMainSalaryDivider_TextChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(20, 33);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(103, 13);
            this.label14.TabIndex = 195;
            this.label14.Text = "Pembagi Gaji Pokok";
            // 
            // txtOverTimeMultiplyHoliday
            // 
            this.txtOverTimeMultiplyHoliday.BackColor = System.Drawing.Color.White;
            this.txtOverTimeMultiplyHoliday.Location = new System.Drawing.Point(167, 138);
            this.txtOverTimeMultiplyHoliday.MaxLength = 200;
            this.txtOverTimeMultiplyHoliday.Name = "txtOverTimeMultiplyHoliday";
            this.txtOverTimeMultiplyHoliday.Size = new System.Drawing.Size(46, 20);
            this.txtOverTimeMultiplyHoliday.TabIndex = 192;
            this.txtOverTimeMultiplyHoliday.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtOverTimeMultiplyHoliday.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOverTimeMultiplyHoliday_KeyPress);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(20, 141);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(128, 13);
            this.label13.TabIndex = 193;
            this.label13.Text = "Pengali Lembur Hari Libur";
            // 
            // txtOverTimeMultiply
            // 
            this.txtOverTimeMultiply.BackColor = System.Drawing.Color.White;
            this.txtOverTimeMultiply.Location = new System.Drawing.Point(167, 112);
            this.txtOverTimeMultiply.MaxLength = 200;
            this.txtOverTimeMultiply.Name = "txtOverTimeMultiply";
            this.txtOverTimeMultiply.Size = new System.Drawing.Size(46, 20);
            this.txtOverTimeMultiply.TabIndex = 190;
            this.txtOverTimeMultiply.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtOverTimeMultiply.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOverTimeMultiply_KeyPress);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(20, 115);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(129, 13);
            this.label12.TabIndex = 191;
            this.label12.Text = "Pengali Lembur Hari Kerja";
            // 
            // txtOverTimeHourDivider
            // 
            this.txtOverTimeHourDivider.BackColor = System.Drawing.Color.White;
            this.txtOverTimeHourDivider.Location = new System.Drawing.Point(167, 59);
            this.txtOverTimeHourDivider.MaxLength = 200;
            this.txtOverTimeHourDivider.Name = "txtOverTimeHourDivider";
            this.txtOverTimeHourDivider.Size = new System.Drawing.Size(46, 20);
            this.txtOverTimeHourDivider.TabIndex = 188;
            this.txtOverTimeHourDivider.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtOverTimeHourDivider.TextChanged += new System.EventHandler(this.txtOverTimeHourDivider_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(20, 62);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(108, 13);
            this.label11.TabIndex = 189;
            this.label11.Text = "Pembagi Jam Lembur";
            // 
            // udReportDivider
            // 
            this.udReportDivider.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.udReportDivider.Location = new System.Drawing.Point(360, 285);
            this.udReportDivider.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.udReportDivider.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udReportDivider.Name = "udReportDivider";
            this.udReportDivider.Size = new System.Drawing.Size(87, 20);
            this.udReportDivider.TabIndex = 186;
            this.udReportDivider.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.udReportDivider.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(357, 269);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 13);
            this.label9.TabIndex = 187;
            this.label9.Text = "Pembagi Laporan";
            this.label9.Visible = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(20, 62);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(108, 13);
            this.label15.TabIndex = 189;
            this.label15.Text = "Pembagi Jam Lembur";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(167, 33);
            this.textBox1.MaxLength = 200;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(46, 20);
            this.textBox1.TabIndex = 194;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(20, 33);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(103, 13);
            this.label16.TabIndex = 195;
            this.label16.Text = "Pembagi Gaji Pokok";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.White;
            this.textBox2.Location = new System.Drawing.Point(167, 138);
            this.textBox2.MaxLength = 200;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(46, 20);
            this.textBox2.TabIndex = 192;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(20, 141);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(128, 13);
            this.label17.TabIndex = 193;
            this.label17.Text = "Pengali Lembur Hari Libur";
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.White;
            this.textBox3.Location = new System.Drawing.Point(167, 112);
            this.textBox3.MaxLength = 200;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(46, 20);
            this.textBox3.TabIndex = 190;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(20, 115);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(129, 13);
            this.label18.TabIndex = 191;
            this.label18.Text = "Pengali Lembur Hari Kerja";
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.Color.White;
            this.textBox4.Location = new System.Drawing.Point(167, 59);
            this.textBox4.MaxLength = 200;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(46, 20);
            this.textBox4.TabIndex = 188;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown1.Location = new System.Drawing.Point(360, 285);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(87, 20);
            this.numericUpDown1.TabIndex = 186;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Visible = false;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(357, 269);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(90, 13);
            this.label19.TabIndex = 187;
            this.label19.Text = "Pembagi Laporan";
            this.label19.Visible = false;
            // 
            // nudCutOffDate
            // 
            this.nudCutOffDate.Location = new System.Drawing.Point(235, 140);
            this.nudCutOffDate.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.nudCutOffDate.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCutOffDate.Name = "nudCutOffDate";
            this.nudCutOffDate.ReadOnly = true;
            this.nudCutOffDate.Size = new System.Drawing.Size(100, 20);
            this.nudCutOffDate.TabIndex = 198;
            this.nudCutOffDate.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // CompanyUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 330);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "CompanyUI";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Perusahaan";
            this.Load += new System.EventHandler(this.CompanyUI_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udReportDivider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCutOffDate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtCompanyName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtFax;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBankName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOverTimeMultiplyHoliday;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtOverTimeMultiply;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtOverTimeHourDivider;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown udReportDivider;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtMainSalaryDivider;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtOverTimeMaximumHour;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.NumericUpDown nudCutOffDate;
    }
}