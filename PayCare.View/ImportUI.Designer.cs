namespace PayCare.View
{
    partial class ImportUI
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
            this.rbEmployee = new System.Windows.Forms.RadioButton();
            this.rbAbsence = new System.Windows.Forms.RadioButton();
            this.rbOvertime = new System.Windows.Forms.RadioButton();
            this.rbIncentive = new System.Windows.Forms.RadioButton();
            this.btnImport = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rbEmployee
            // 
            this.rbEmployee.AutoSize = true;
            this.rbEmployee.Location = new System.Drawing.Point(18, 25);
            this.rbEmployee.Name = "rbEmployee";
            this.rbEmployee.Size = new System.Drawing.Size(72, 17);
            this.rbEmployee.TabIndex = 0;
            this.rbEmployee.TabStop = true;
            this.rbEmployee.Text = "Karyawan";
            this.rbEmployee.UseVisualStyleBackColor = true;
            this.rbEmployee.CheckedChanged += new System.EventHandler(this.rbEmployee_CheckedChanged);
            // 
            // rbAbsence
            // 
            this.rbAbsence.AutoSize = true;
            this.rbAbsence.Location = new System.Drawing.Point(18, 48);
            this.rbAbsence.Name = "rbAbsence";
            this.rbAbsence.Size = new System.Drawing.Size(55, 17);
            this.rbAbsence.TabIndex = 1;
            this.rbAbsence.TabStop = true;
            this.rbAbsence.Text = "Absen";
            this.rbAbsence.UseVisualStyleBackColor = true;
            this.rbAbsence.CheckedChanged += new System.EventHandler(this.rbAbsence_CheckedChanged);
            // 
            // rbOvertime
            // 
            this.rbOvertime.AutoSize = true;
            this.rbOvertime.Location = new System.Drawing.Point(119, 25);
            this.rbOvertime.Name = "rbOvertime";
            this.rbOvertime.Size = new System.Drawing.Size(60, 17);
            this.rbOvertime.TabIndex = 2;
            this.rbOvertime.TabStop = true;
            this.rbOvertime.Text = "Lembur";
            this.rbOvertime.UseVisualStyleBackColor = true;
            this.rbOvertime.CheckedChanged += new System.EventHandler(this.rbOvertime_CheckedChanged);
            // 
            // rbIncentive
            // 
            this.rbIncentive.AutoSize = true;
            this.rbIncentive.Location = new System.Drawing.Point(120, 48);
            this.rbIncentive.Name = "rbIncentive";
            this.rbIncentive.Size = new System.Drawing.Size(59, 17);
            this.rbIncentive.TabIndex = 3;
            this.rbIncentive.TabStop = true;
            this.rbIncentive.Text = "Insentif";
            this.rbIncentive.UseVisualStyleBackColor = true;
            this.rbIncentive.CheckedChanged += new System.EventHandler(this.rbIncentive_CheckedChanged);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(243, 19);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 457;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // ImportUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 118);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.rbIncentive);
            this.Controls.Add(this.rbOvertime);
            this.Controls.Add(this.rbAbsence);
            this.Controls.Add(this.rbEmployee);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ImportUI";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import";
            this.Load += new System.EventHandler(this.ImportUI_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbEmployee;
        private System.Windows.Forms.RadioButton rbAbsence;
        private System.Windows.Forms.RadioButton rbOvertime;
        private System.Windows.Forms.RadioButton rbIncentive;
        private System.Windows.Forms.Button btnImport;
    }
}