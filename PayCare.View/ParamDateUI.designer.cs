namespace PayCare.View
{
    partial class ParamDateUI
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
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.rbNatal = new System.Windows.Forms.RadioButton();
            this.rbLebaran = new System.Windows.Forms.RadioButton();
            this.lblHolidays = new System.Windows.Forms.Label();
            this.chkIsIncentive = new System.Windows.Forms.CheckBox();
            this.chkIsOverTime = new System.Windows.Forms.CheckBox();
            this.chkIsEmployeeDebt = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "dd/MM/yyyy";
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(40, 38);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(129, 20);
            this.dtpDate.TabIndex = 172;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(37, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 173;
            this.label3.Text = "Tanggal Efektif";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(222, 173);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(54, 23);
            this.btnCancel.TabIndex = 178;
            this.btnCancel.Text = "Batal";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(150, 173);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(54, 23);
            this.btnOk.TabIndex = 177;
            this.btnOk.Text = "Proses";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // rbNatal
            // 
            this.rbNatal.AutoSize = true;
            this.rbNatal.Location = new System.Drawing.Point(118, 115);
            this.rbNatal.Name = "rbNatal";
            this.rbNatal.Size = new System.Drawing.Size(50, 17);
            this.rbNatal.TabIndex = 181;
            this.rbNatal.Text = "Natal";
            this.rbNatal.UseVisualStyleBackColor = true;
            // 
            // rbLebaran
            // 
            this.rbLebaran.AutoSize = true;
            this.rbLebaran.Checked = true;
            this.rbLebaran.Location = new System.Drawing.Point(44, 115);
            this.rbLebaran.Name = "rbLebaran";
            this.rbLebaran.Size = new System.Drawing.Size(64, 17);
            this.rbLebaran.TabIndex = 180;
            this.rbLebaran.TabStop = true;
            this.rbLebaran.Text = "Lebaran";
            this.rbLebaran.UseVisualStyleBackColor = true;
            // 
            // lblHolidays
            // 
            this.lblHolidays.AutoSize = true;
            this.lblHolidays.Location = new System.Drawing.Point(41, 102);
            this.lblHolidays.Name = "lblHolidays";
            this.lblHolidays.Size = new System.Drawing.Size(54, 13);
            this.lblHolidays.TabIndex = 179;
            this.lblHolidays.Text = "Hari Raya";
            // 
            // chkIsIncentive
            // 
            this.chkIsIncentive.AutoSize = true;
            this.chkIsIncentive.Location = new System.Drawing.Point(222, 75);
            this.chkIsIncentive.Name = "chkIsIncentive";
            this.chkIsIncentive.Size = new System.Drawing.Size(60, 17);
            this.chkIsIncentive.TabIndex = 497;
            this.chkIsIncentive.Text = "Insentif";
            this.chkIsIncentive.UseVisualStyleBackColor = true;
            // 
            // chkIsOverTime
            // 
            this.chkIsOverTime.AutoSize = true;
            this.chkIsOverTime.Checked = true;
            this.chkIsOverTime.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIsOverTime.Location = new System.Drawing.Point(40, 75);
            this.chkIsOverTime.Name = "chkIsOverTime";
            this.chkIsOverTime.Size = new System.Drawing.Size(61, 17);
            this.chkIsOverTime.TabIndex = 498;
            this.chkIsOverTime.Text = "Lembur";
            this.chkIsOverTime.UseVisualStyleBackColor = true;
            // 
            // chkIsEmployeeDebt
            // 
            this.chkIsEmployeeDebt.AutoSize = true;
            this.chkIsEmployeeDebt.Checked = true;
            this.chkIsEmployeeDebt.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIsEmployeeDebt.Location = new System.Drawing.Point(105, 75);
            this.chkIsEmployeeDebt.Name = "chkIsEmployeeDebt";
            this.chkIsEmployeeDebt.Size = new System.Drawing.Size(111, 17);
            this.chkIsEmployeeDebt.TabIndex = 499;
            this.chkIsEmployeeDebt.Text = "Piutang karyawan";
            this.chkIsEmployeeDebt.UseVisualStyleBackColor = true;
            // 
            // ParamDateUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 208);
            this.Controls.Add(this.chkIsEmployeeDebt);
            this.Controls.Add(this.chkIsOverTime);
            this.Controls.Add(this.chkIsIncentive);
            this.Controls.Add(this.rbNatal);
            this.Controls.Add(this.rbLebaran);
            this.Controls.Add(this.lblHolidays);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ParamDateUI";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Proses Penggajian";
            this.Load += new System.EventHandler(this.ParamDateUI_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.RadioButton rbNatal;
        private System.Windows.Forms.RadioButton rbLebaran;
        private System.Windows.Forms.Label lblHolidays;
        private System.Windows.Forms.CheckBox chkIsIncentive;
        private System.Windows.Forms.CheckBox chkIsOverTime;
        private System.Windows.Forms.CheckBox chkIsEmployeeDebt;
    }
}