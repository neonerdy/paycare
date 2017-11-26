using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using PayCare.Repository;
using EntityMap;
using PayCare.Model;

namespace PayCare.View
{
    public partial class ExportUI : Form
    {
        private IPayrollRepository payrollRepository;
        private ITransferRepository transferRepository;
        private ITHRRepository thrRepository;
        private IIncentiveRepository incentiveRepository;

        private string fileName;

        public ExportUI()
        {
            payrollRepository = EntityContainer.GetType<IPayrollRepository>();
            transferRepository = EntityContainer.GetType<ITransferRepository>();
            thrRepository = EntityContainer.GetType<ITHRRepository>();
            incentiveRepository = EntityContainer.GetType<IIncentiveRepository>();
            
            InitializeComponent();
        }


        private void rbSalary_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSalary.Checked)
            {
                this.Text = "Transfer Gaji";
                fileName = "GAJI " + Store.GetMonthName(Store.ActiveMonth).ToUpper() + " " + Store.ActiveYear.ToString();
            }
        }

        private void rbTHR_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTHR.Checked)
            {
                this.Text = "Transfer THR";
                fileName = "THR " + Store.ActiveYear.ToString();
            }
        }

        private void rbIncentive_CheckedChanged(object sender, EventArgs e)
        {
            if (rbIncentive.Checked)
            {
                this.Text = "Transfer Insentif";
                fileName = "INSENTIF " + Store.GetMonthName(Store.ActiveMonth).ToUpper() + " " + Store.ActiveYear.ToString();
            }
        }




        private int ExportSalary(string fileName)
        {
            using (StreamWriter sw = File.CreateText(fileName))
            {
                var transferedSalary = payrollRepository.GetTransfered(Store.ActiveMonth,Store.ActiveYear);
                int transferedCount = transferedSalary.Count();

                decimal grandTotal = 0;

                foreach (var salary in transferedSalary)
                {
                    sw.WriteLine(salary.AccountNumber + "," + salary.GrandTotal
                        + "," + "GAJI " + Store.GetMonthName(salary.MonthPeriod).ToUpper() + ","
                        + salary.Employee.EmployeeCode);

                    grandTotal = grandTotal + salary.GrandTotal;
                }

                var transfer = new Transfer();

                transfer.TransferType = "GAJI";
                transfer.TransferDate = dtpDate.Value;
                transfer.TotalEmployee = transferedCount;
                transfer.TotalTransfer = grandTotal;

                transferRepository.Save(transfer);

                return transferedCount;

            }
        }



        public int ExportTHR(string fileName)
        {
            using (StreamWriter sw = File.CreateText(fileName))
            {
                var transferedTHR = thrRepository.GetTransfered(Store.ActiveYear);
                int transferedCount = transferedTHR.Count();

                decimal grandTotal = 0;

                foreach (var thr in transferedTHR)
                {
                    sw.WriteLine(thr.AccountNumber + "," + thr.TotalAmount
                        + "," + "THR " + thr.YearPeriod.ToString() + ","
                        + thr.Employee.EmployeeCode);
                    
                    grandTotal = grandTotal + thr.TotalAmount;
                }

                var transfer = new Transfer();

                transfer.TransferType = "THR";
                transfer.TransferDate = dtpDate.Value;
                transfer.TotalEmployee = transferedCount;
                transfer.TotalTransfer = grandTotal;

                transferRepository.Save(transfer);

                return transferedCount;

            }
        }
        

        public int ExportIncentive(string fileName)
        {
            using (StreamWriter sw = File.CreateText(fileName))
            {
                var transferedIncentive = incentiveRepository.GetTransfered(Store.ActiveMonth,Store.ActiveYear);
                int transferedCount = transferedIncentive.Count();

                decimal grandTotal = 0;

                foreach (var incentive in transferedIncentive)
                {
                    sw.WriteLine(incentive.AccountNumber + "," + incentive.Amount
                        + "," + "INSENTIF " + Store.GetMonthName(incentive.MonthPeriod).ToUpper() + " " + incentive.YearPeriod.ToString()
                        + "," + incentive.Employee.EmployeeCode);

                    grandTotal = grandTotal + incentive.Amount;
                }

                var transfer = new Transfer();

                transfer.TransferType = "INSENTIF";
                transfer.TransferDate = dtpDate.Value;
                transfer.TotalEmployee = transferedCount;
                transfer.TotalTransfer = grandTotal;

                transferRepository.Save(transfer);

                return transferedCount;

            }
        }

       
        


        private void btnTransfer_Click(object sender, EventArgs e)
        {
           
            SaveFileDialog dialog = new SaveFileDialog();

            dialog.Filter = "CSV File|*.csv";

            dialog.InitialDirectory = "C:";
            dialog.Title = "Export";
            dialog.FileName = this.fileName;

            if (dialog.ShowDialog() == DialogResult.OK)
                fileName = dialog.FileName;
            if (fileName == String.Empty)
                return;

            try
            {
                ExportData(fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal export data!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }           
        }



        private void ExportData(string fileName)
        {
            if (rbSalary.Checked)
            {
                int result1 = ExportSalary(fileName);
                if (result1 > 0)
                {
                    MessageBox.Show("Export data sukses", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Tidak ada data gaji dengan status transfer", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (rbTHR.Checked)
            {
                int result2 = ExportTHR(fileName);
                if (result2 > 0)
                {
                    MessageBox.Show("Export data sukses", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Tidak ada data THR dengan status transfer", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (rbIncentive.Checked)
            {
                int result3 = ExportIncentive(fileName);
                if (result3 > 0)
                {
                    MessageBox.Show("Export data sukses", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Tidak ada data insentif dengan status transfer", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }



        private void ExportUI_Load(object sender, EventArgs e)
        {
            dtpDate.Value = DateTime.Now;
            fileName = "GAJI " + Store.GetMonthName(Store.ActiveMonth).ToUpper() + " " + Store.ActiveYear.ToString();
        }


    }
}
