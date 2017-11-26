namespace PayCare.View
{
    partial class ExportUI
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
            this.rbSalary = new System.Windows.Forms.RadioButton();
            this.rbTHR = new System.Windows.Forms.RadioButton();
            this.rbIncentive = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.btnTransfer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "dd/MM/yyyy";
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(25, 88);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(184, 20);
            this.dtpDate.TabIndex = 267;
            // 
            // rbSalary
            // 
            this.rbSalary.AutoSize = true;
            this.rbSalary.Checked = true;
            this.rbSalary.Location = new System.Drawing.Point(25, 31);
            this.rbSalary.Name = "rbSalary";
            this.rbSalary.Size = new System.Drawing.Size(43, 17);
            this.rbSalary.TabIndex = 268;
            this.rbSalary.TabStop = true;
            this.rbSalary.Text = "Gaji";
            this.rbSalary.UseVisualStyleBackColor = true;
            this.rbSalary.CheckedChanged += new System.EventHandler(this.rbSalary_CheckedChanged);
            // 
            // rbTHR
            // 
            this.rbTHR.AutoSize = true;
            this.rbTHR.Location = new System.Drawing.Point(85, 31);
            this.rbTHR.Name = "rbTHR";
            this.rbTHR.Size = new System.Drawing.Size(48, 17);
            this.rbTHR.TabIndex = 269;
            this.rbTHR.Text = "THR";
            this.rbTHR.UseVisualStyleBackColor = true;
            this.rbTHR.CheckedChanged += new System.EventHandler(this.rbTHR_CheckedChanged);
            // 
            // rbIncentive
            // 
            this.rbIncentive.AutoSize = true;
            this.rbIncentive.Location = new System.Drawing.Point(150, 31);
            this.rbIncentive.Name = "rbIncentive";
            this.rbIncentive.Size = new System.Drawing.Size(59, 17);
            this.rbIncentive.TabIndex = 270;
            this.rbIncentive.Text = "Insentif";
            this.rbIncentive.UseVisualStyleBackColor = true;
            this.rbIncentive.CheckedChanged += new System.EventHandler(this.rbIncentive_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 271;
            this.label1.Text = "Tanggal Transfer";
            // 
            // btnTransfer
            // 
            this.btnTransfer.Location = new System.Drawing.Point(134, 148);
            this.btnTransfer.Name = "btnTransfer";
            this.btnTransfer.Size = new System.Drawing.Size(75, 23);
            this.btnTransfer.TabIndex = 272;
            this.btnTransfer.Text = "Transfer";
            this.btnTransfer.UseVisualStyleBackColor = true;
            this.btnTransfer.Click += new System.EventHandler(this.btnTransfer_Click);
            // 
            // ExportUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(253, 203);
            this.Controls.Add(this.btnTransfer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbIncentive);
            this.Controls.Add(this.rbTHR);
            this.Controls.Add(this.rbSalary);
            this.Controls.Add(this.dtpDate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ExportUI";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transfer Gaji";
            this.Load += new System.EventHandler(this.ExportUI_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.RadioButton rbSalary;
        private System.Windows.Forms.RadioButton rbTHR;
        private System.Windows.Forms.RadioButton rbIncentive;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnTransfer;
    }
}