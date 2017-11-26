using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PayCare.Repository;
using EntityMap;

namespace PayCare.View
{
    public partial class ParamDateUI : Form
    {
        private IPayrollRepository payrollRepository;
        private IIncentiveRepository incentiveRepository;
        private IOverTimeRepository overTimeRepository;
        private ITHRRepository thrRepository;
        private ICompanyRepository companyRepository;
        private IEmployeeDebtItemRepository employeeDebtItemRepository;

        public ParamDateUI()
        {
            InitializeComponent();
            payrollRepository = EntityContainer.GetType<IPayrollRepository>();
            thrRepository = EntityContainer.GetType<ITHRRepository>();
            companyRepository = EntityContainer.GetType<ICompanyRepository>();
            incentiveRepository = EntityContainer.GetType<IIncentiveRepository>();
            overTimeRepository = EntityContainer.GetType<IOverTimeRepository>();
            employeeDebtItemRepository = EntityContainer.GetType<IEmployeeDebtItemRepository>();
        }


        public DateTime BeginDate
        {
            get { return dtpDate.Value; }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
          
                 switch (Store.ActiveForm)
                {

                    case "Payroll":
                        bool isIncentive = false;
                        bool isOverTime = false;
                        bool isEmployeeDebt = false;

                        if (chkIsIncentive.Checked)
                        {
                            isIncentive = true;
                        }

                        if (chkIsOverTime.Checked)
                        {
                            isOverTime = true;
                        }

                        if (chkIsEmployeeDebt.Checked)
                        {
                            isEmployeeDebt = true;
                        }

                        payrollRepository.CalculatePayroll(dtpDate.Value, isIncentive, isOverTime, isEmployeeDebt, Store.ActiveMonth, Store.ActiveYear);
                        incentiveRepository.UpdateIsIncludePayroll(Store.ActiveMonth, Store.ActiveYear, isIncentive);
                        overTimeRepository.UpdateIsIncludePayroll(Store.ActiveMonth, Store.ActiveYear, isOverTime);
                        employeeDebtItemRepository.UpdateIsIncludePayroll(Store.ActiveMonth, Store.ActiveYear, isEmployeeDebt);

                        var frmPayroll = new PayrollUI();
                        frmPayroll.MdiParent = this.MdiParent;
                        frmPayroll.Show();

                        break;

                    case "THR":
                        string hariRaya = "";
                        if (rbLebaran.Checked == true)
                        {
                            hariRaya = "LEBARAN";
                        }
                        else
                        {
                            hariRaya = "NATAL";
                        }

                        if (thrRepository.IsPaid(hariRaya, Store.ActiveYear))
                        {
                            MessageBox.Show("Tidak dapat proses THR\n\n Periode : " + Store.ActiveYear + "\n\n" + "Sudah dibayarkan", "Perhatian",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            thrRepository.CalculateTHR(dtpDate.Value, hariRaya);
                        }
                        var frmTHR = new THRUI();
                        frmTHR.MdiParent = this.MdiParent;
                        frmTHR.Show();
                        

                            break;
                        
                
                }
           

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ParamDateUI_Load(object sender, EventArgs e)
        {
            int month = Store.ActiveMonth;
            int year = Store.ActiveYear;
            int day = 1;

            var company = companyRepository.GetAll();
            if (company != null)
            {
                day = company.SalaryCutOffDate;
            }

            DateTime dtDate = new DateTime(year, month, day);
            dtpDate.Value = dtDate;



            switch (Store.ActiveForm)
            {

                case "Payroll":
                    chkIsIncentive.Visible = true;
                    chkIsOverTime.Visible = true;
                    chkIsEmployeeDebt.Visible = true;
                    lblHolidays.Visible = false;
                    rbLebaran.Visible = false;
                    rbNatal.Visible = false;

                    break;

                case "THR":
                    chkIsIncentive.Visible = false;
                    chkIsOverTime.Visible = false;
                    chkIsEmployeeDebt.Visible = false;
                    lblHolidays.Visible = true;
                    rbLebaran.Visible = true;
                    rbNatal.Visible = true;

                    break;

            }

        }

    
    }
}
