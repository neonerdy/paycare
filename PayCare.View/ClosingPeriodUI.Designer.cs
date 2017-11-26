namespace PayCare.View
{
    partial class ClosingPeriodUI
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.txtActiveYear = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtActiveMonth = new System.Windows.Forms.TextBox();
            this.lblMonth = new System.Windows.Forms.Label();
            this.rbThr = new System.Windows.Forms.RadioButton();
            this.rbPayroll = new System.Windows.Forms.RadioButton();
            this.lblHolidays = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(197, 151);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(54, 23);
            this.btnCancel.TabIndex = 109;
            this.btnCancel.Text = "Batal";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(137, 151);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(54, 23);
            this.btnOk.TabIndex = 108;
            this.btnOk.Text = "Proses";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtActiveYear
            // 
            this.txtActiveYear.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtActiveYear.Enabled = false;
            this.txtActiveYear.Location = new System.Drawing.Point(24, 86);
            this.txtActiveYear.MaxLength = 200;
            this.txtActiveYear.Multiline = true;
            this.txtActiveYear.Name = "txtActiveYear";
            this.txtActiveYear.Size = new System.Drawing.Size(114, 20);
            this.txtActiveYear.TabIndex = 107;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 106;
            this.label1.Text = "Tahun";
            // 
            // txtActiveMonth
            // 
            this.txtActiveMonth.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtActiveMonth.Enabled = false;
            this.txtActiveMonth.Location = new System.Drawing.Point(145, 86);
            this.txtActiveMonth.MaxLength = 200;
            this.txtActiveMonth.Multiline = true;
            this.txtActiveMonth.Name = "txtActiveMonth";
            this.txtActiveMonth.Size = new System.Drawing.Size(100, 20);
            this.txtActiveMonth.TabIndex = 105;
            // 
            // lblMonth
            // 
            this.lblMonth.AutoSize = true;
            this.lblMonth.Location = new System.Drawing.Point(142, 70);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(34, 13);
            this.lblMonth.TabIndex = 104;
            this.lblMonth.Text = "Bulan";
            // 
            // rbThr
            // 
            this.rbThr.AutoSize = true;
            this.rbThr.Location = new System.Drawing.Point(126, 34);
            this.rbThr.Name = "rbThr";
            this.rbThr.Size = new System.Drawing.Size(48, 17);
            this.rbThr.TabIndex = 184;
            this.rbThr.Text = "THR";
            this.rbThr.UseVisualStyleBackColor = true;
            this.rbThr.CheckedChanged += new System.EventHandler(this.rbThr_CheckedChanged);
            // 
            // rbPayroll
            // 
            this.rbPayroll.AutoSize = true;
            this.rbPayroll.Checked = true;
            this.rbPayroll.Location = new System.Drawing.Point(23, 34);
            this.rbPayroll.Name = "rbPayroll";
            this.rbPayroll.Size = new System.Drawing.Size(78, 17);
            this.rbPayroll.TabIndex = 183;
            this.rbPayroll.TabStop = true;
            this.rbPayroll.Text = "Penggajian";
            this.rbPayroll.UseVisualStyleBackColor = true;
            // 
            // lblHolidays
            // 
            this.lblHolidays.AutoSize = true;
            this.lblHolidays.Location = new System.Drawing.Point(20, 17);
            this.lblHolidays.Name = "lblHolidays";
            this.lblHolidays.Size = new System.Drawing.Size(53, 13);
            this.lblHolidays.TabIndex = 182;
            this.lblHolidays.Text = "Transaksi";
            // 
            // ClosingPeriodUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 204);
            this.Controls.Add(this.rbThr);
            this.Controls.Add(this.rbPayroll);
            this.Controls.Add(this.lblHolidays);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtActiveYear);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtActiveMonth);
            this.Controls.Add(this.lblMonth);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ClosingPeriodUI";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tutup Buku";
            this.Load += new System.EventHandler(this.ClosingPeriodUI_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox txtActiveYear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtActiveMonth;
        private System.Windows.Forms.Label lblMonth;
        private System.Windows.Forms.RadioButton rbThr;
        private System.Windows.Forms.RadioButton rbPayroll;
        private System.Windows.Forms.Label lblHolidays;
    }
}