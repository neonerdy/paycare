using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EntityMap;

using PayCare.Repository;
using PayCare.Model;

namespace PayCare.View
{
    public partial class ClosingPeriodUI : Form
    {
        private MainUI frmMain;
        private FormMode formMode;
        private IWorkCalendarRepository workCalendarRepository;
        private IIncentiveRepository incentiveRepository;
        private ITHRRepository thrRepository;
        private IPayrollRepository payrollRepository;
        private IEmployeeDebtItemRepository employeeDebtItemRepository;
        private IOverTimeRepository overTimeRepository;

        public ClosingPeriodUI()
        {
            InitializeComponent();
        }

        public ClosingPeriodUI(MainUI frmMain)
        {
            InitializeComponent();
            this.frmMain = frmMain;

            workCalendarRepository = EntityContainer.GetType<IWorkCalendarRepository>();
            incentiveRepository = EntityContainer.GetType<IIncentiveRepository>();
            payrollRepository = EntityContainer.GetType<IPayrollRepository>();
            employeeDebtItemRepository = EntityContainer.GetType<IEmployeeDebtItemRepository>();
            overTimeRepository = EntityContainer.GetType<IOverTimeRepository>();
            
        }

        private void ClosingPeriodUI_Load(object sender, EventArgs e)
        {
            txtActiveMonth.Text = Store.GetMonthName(Store.ActiveMonth);
            txtActiveYear.Text = Store.ActiveYear.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Anda yakin ingin menutup Periode : " + Store.GetMonthName(Store.ActiveMonth) + " " + Store.ActiveYear + "'", "Perhatian",
                             MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                Application.UseWaitCursor = true;

                if (rbPayroll.Checked == true)
                {
                    workCalendarRepository.UpdateIsPeriod(Store.ActiveMonth, Store.ActiveYear, true);
                    incentiveRepository.UpdateIsPaid(Store.ActiveMonth, Store.ActiveYear, true);
                    payrollRepository.UpdateIsPaid(Store.ActiveMonth, Store.ActiveYear, true);
                    overTimeRepository.UpdateIsPaid(Store.ActiveMonth, Store.ActiveYear, true);
                    employeeDebtItemRepository.UpdateIsPaid(Store.ActiveMonth, Store.ActiveYear, true);

                    Application.UseWaitCursor = false;
                    MessageBox.Show("Proses Tutup Buku \n\n Periode : " + Store.GetMonthName(Store.ActiveMonth) + " " + Store.ActiveYear + "' \n\n Sukses", "Sukses",
                                 MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    workCalendarRepository.UpdateIsThr(Store.ActiveYear, true);
                    thrRepository.UpdateIsPaid(Store.ActiveYear, true);

                    Application.UseWaitCursor = false;
                    MessageBox.Show("Proses Tutup Buku THR \n\n Periode : " + Store.ActiveYear + "' \n\n Sukses", "Sukses",
                                 MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                

               
                this.Close();
            }
        }

        private void rbThr_CheckedChanged(object sender, EventArgs e)
        {
            if (rbThr.Checked == true)
            {
                lblMonth.Visible = false;
                txtActiveMonth.Visible = false;
            }
            else
            {
                lblMonth.Visible = true;
                txtActiveMonth.Visible = true;
            }
        }


       




        






    }
}
