

//.........................................................
//
// PAYCARE ( Payroll System )
//
// Version : 1.0
// Created Date : 01-10-2013
//
// (c) XERIS, All Right Reserved
//
//.........................................................




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
using BizCare.View;

namespace PayCare.View
{
    public enum FormMode
    {
        View, Add, Edit
    }

    public enum TabActive
    {
        Personal,Family,Department,Grade,Occupation,Principal,Status,Insurance,Salary
    }



    public partial class MainUI : Form
    {
        private ICompanyRepository companyRepository;
        private IAbsenceRepository absenceRepository;
        private IWorkCalendarRepository workCalendarRepository;
        private IPayrollRepository payrollRepository;
        private IUserAccessRepository userAccessRepository;

        public MainUI()
        {
            InitializeComponent();
            companyRepository = EntityContainer.GetType<ICompanyRepository>();
            absenceRepository = EntityContainer.GetType<IAbsenceRepository>();
            workCalendarRepository = EntityContainer.GetType<IWorkCalendarRepository>();
            payrollRepository = EntityContainer.GetType<IPayrollRepository>();
            userAccessRepository = EntityContainer.GetType<IUserAccessRepository>();
        }

        public string Statusbar
        {
            get { return tslLogin.Text; }
            set { tslLogin.Text = value; }
        }

        private void mnuCompany_Click(object sender, EventArgs e)
        {
        }

        private void tsbEmployee_Click(object sender, EventArgs e)
        {
          

            var frmEmployee = new EmployeeUI();
            frmEmployee.MdiParent = this;

            frmEmployee.Show();
            
        }


        private void mnuWorkCalendar_Click(object sender, EventArgs e)
        {
            var frmWorkCalendar = new WorkCalendarUI();
            frmWorkCalendar.MdiParent = this;

            frmWorkCalendar.Show();
        }


        private void MainUI_Load(object sender, EventArgs e)
        {


            Store.ActiveMonth = DateTime.Now.Month;
            Store.ActiveYear = DateTime.Now.Year;
            Store.ActiveUser = "admin";
                        
            tslLogin.Text = "Periode : " + Store.GetMonthName(Store.ActiveMonth)
              + " " + Store.ActiveYear + "  |  User : " + Store.ActiveUser;

          
            Store.CutOffDate = companyRepository.GetById(Guid.Empty).SalaryCutOffDate;
            Store.IsPeriodClosed = workCalendarRepository.IsPeriodClosed(Store.ActiveMonth, Store.ActiveYear);
            Store.IsThrClosed = workCalendarRepository.IsThrClosed(Store.ActiveYear);
            Store.IsAdministrator = true;

          

        }

        private void mnuOccupation_Click(object sender, EventArgs e)
        {
            var frmOccupation = new OccupationUI();
            frmOccupation.MdiParent = this;

            frmOccupation.Show();
        }

        private void mnuGrade_Click(object sender, EventArgs e)
        {
            var frmGrade = new GradeUI();
            frmGrade.MdiParent = this;

            frmGrade.Show();
        }

        private void mnuInsurance_Click(object sender, EventArgs e)
        {
            var frmInsurance = new InsuranceUI();
            
            frmInsurance.MdiParent = this;
            frmInsurance.Show();

        }

        private void mnuUser_Click(object sender, EventArgs e)
        {
            var frmUser = new UserUI();
            frmUser.MdiParent = this;

            frmUser.Show();

        }

        private void mnuUserAcceess_Click(object sender, EventArgs e)
        {
            var frmUserAccess = new UserAccessUI();
            frmUserAccess.MdiParent = this;

            frmUserAccess.Show();
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mnuCompany_Click_1(object sender, EventArgs e)
        {
            var frmCompany = new CompanyUI();
            frmCompany.MdiParent = this;

            frmCompany.Show();
        }

        private void mnuPrincipal_Click(object sender, EventArgs e)
        {
            var frmPrincipal = new PrincipalUI();
            frmPrincipal.MdiParent = this;

            frmPrincipal.Show();
        }

        private void mnuBranch_Click(object sender, EventArgs e)
        {
            var frmBranch = new BranchUI();
            frmBranch.MdiParent = this;

            frmBranch.Show();
        }

        private void mnuDepartment_Click(object sender, EventArgs e)
        {
            var frmDepartment = new DepartmentUI();
            frmDepartment.MdiParent = this;

            frmDepartment.Show();
        }

        private void mnuPTKP_Click(object sender, EventArgs e)
        {
            var frmPTKP = new PTKPUI();
            frmPTKP.MdiParent = this;

            frmPTKP.Show();
        }

        private void tsbAbsence_Click(object sender, EventArgs e)
        {
            if (Store.IsPeriodClosed == false)
            {
                absenceRepository.GenerateAbsence(Store.ActiveMonth, Store.ActiveYear);
            }
       
            var frmAbsence = new AbsenceUI();
            frmAbsence.MdiParent = this;

            frmAbsence.Show();
        }

        private void tsbOverTime_Click(object sender, EventArgs e)
        {
            var frmOverTime = new OverTimeUI();
            frmOverTime.MdiParent = this;

            frmOverTime.Show();
        }

        private void tsbSalary_Click(object sender, EventArgs e)
        {

            if (Store.IsPeriodClosed == false)
            {
                Store.ActiveForm = "Payroll";

                var frmParamDate = new ParamDateUI();
                frmParamDate.MdiParent = this;
                frmParamDate.Text = "Proses Gaji";

                frmParamDate.Show();

            }
            else
            {
                var frmPayrollUI = new PayrollUI();
                frmPayrollUI.MdiParent = this;
                frmPayrollUI.Show();
            }
        }

      
      
        private void mnuRptPayrollDetail_Click(object sender, EventArgs e)
        {
            var userAccess=userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser 
                && u.ObjectName == "Gaji -> Rinci" && u.IsOpen);
            if (isAllowed || Store.IsAdministrator)
            {
                Store.ActiveReport = "PayrollDetail";

                var frmReportParamPeriodUI = new ReportParamPeriodUI();
                frmReportParamPeriodUI.Show();
                frmReportParamPeriodUI.Text = "Gaji - Rinci";
            }
            else
            {
                MessageBox.Show("Anda tidak dapat membuka laporan ini", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void mnuRptPayrollRecap_Click(object sender, EventArgs e)
        {
                var userAccess=userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser 
                && u.ObjectName == "Gaji -> Rekap" && u.IsOpen);
            if (isAllowed || Store.IsAdministrator)
            {

                Store.ActiveReport = "PayrollRecap";

                var frmReportParamPeriodUI = new ReportParamPeriodUI();
                frmReportParamPeriodUI.Show();
                frmReportParamPeriodUI.Text = "Gaji - Rekap";
            }
            else
            {
                MessageBox.Show("Anda tidak dapat membuka laporan ini", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void mnuRptPayrollReceipt_Click(object sender, EventArgs e)
        {
             var userAccess=userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser 
                && u.ObjectName == "Gaji -> Slip" && u.IsOpen);
            if (isAllowed || Store.IsAdministrator)
            {

                Store.ActiveReport = "PayrollReceipt";

                var frmReportParamPeriodUI = new ReportParamPeriodUI();
                frmReportParamPeriodUI.Show();
                frmReportParamPeriodUI.Text = "Gaji - Slip";
            }
            else
            {
                MessageBox.Show("Anda tidak dapat membuka laporan ini", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void mnuRptOVerTimeDetail_Click(object sender, EventArgs e)
        {
                     var userAccess=userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser 
                && u.ObjectName == "Lembur -> Rinci" && u.IsOpen);
            if (isAllowed || Store.IsAdministrator)
            {
                Store.ActiveReport = "OverTimeDetail";

                var frmReportParamPeriodUI = new ReportParamPeriodUI();
                frmReportParamPeriodUI.Show();
                frmReportParamPeriodUI.Text = "Lembur - Rinci";
            }
            else
            {
                MessageBox.Show("Anda tidak dapat membuka laporan ini", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void mnuRptOVerTimeRecap_Click(object sender, EventArgs e)
        {
            var userAccess=userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser 
                && u.ObjectName == "Lembur -> Rekap" && u.IsOpen);
            if (isAllowed || Store.IsAdministrator)
            {

                Store.ActiveReport = "OverTimeRecap";

                var frmReportParamPeriodUI = new ReportParamPeriodUI();
                frmReportParamPeriodUI.Show();
                frmReportParamPeriodUI.Text = "Lembur - Rekap";
            }
            else
            {
                MessageBox.Show("Anda tidak dapat membuka laporan ini", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

       
        private void mnuRptEmployeeDetail_Click(object sender, EventArgs e)
        {
            var userAccess=userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser 
                && u.ObjectName == "Karyawan -> Biodata" && u.IsOpen);
            if (isAllowed || Store.IsAdministrator)
            {
                Store.ActiveReport = "EmployeeDetail";

                var frm1 = new ReportUI();
                frm1.Show();
                frm1.Text = "Biodata Karyawan";
            }
            else
            {
                MessageBox.Show("Anda tidak dapat membuka laporan ini", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           
        }

        private void mnuRptEmployeeRecap_Click(object sender, EventArgs e)
        {
            var userAccess=userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser 
                && u.ObjectName == "Karyawan -> Rekap" && u.IsOpen);
            if (isAllowed || Store.IsAdministrator)
            {
                Store.ActiveReport = "EmployeeRecap";

                var frm1 = new ReportUI();
                frm1.Show();
                frm1.Text = "Karyawan - Rekap";
            }
            else
            {
                MessageBox.Show("Anda tidak dapat membuka laporan ini", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           
        }

        private void mnuRptJamsostekDetail_Click(object sender, EventArgs e)
        {
              var userAccess=userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser 
                && u.ObjectName == "Jamsostek -> Rinci" && u.IsOpen);
            if (isAllowed || Store.IsAdministrator)
            {
                Store.ActiveReport = "JamsostekDetail";

                var frmReportParamPeriodUI = new ReportParamPeriodUI();
                frmReportParamPeriodUI.Show();
                frmReportParamPeriodUI.Text = "Jamsostek - Rinci";
            }
            else
            {
                MessageBox.Show("Anda tidak dapat membuka laporan ini", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void mnuRptJamsostekMember_Click(object sender, EventArgs e)
        {
            var userAccess=userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser 
                && u.ObjectName == "Jamsostek -> Peserta" && u.IsOpen);
            if (isAllowed || Store.IsAdministrator)
            {
                Store.ActiveReport = "JamsostekMember";

                var frm1 = new ReportUI();

                frm1.Show();
                frm1.Text = "Jamsostek - Peserta";
            }
            else
            {
                MessageBox.Show("Anda tidak dapat membuka laporan ini", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void mnuLogOff_Click(object sender, EventArgs e)
        {
            var frmLogin = new LoginUI(this);
            frmLogin.ShowDialog();
        }

      
        private void tsbEmployeeDebt_Click(object sender, EventArgs e)
        {
            var frmEmployeeDebt = new EmployeeDebtUI();
            frmEmployeeDebt.MdiParent = this;

            frmEmployeeDebt.Show();
        }

        private void tsbIncentive_Click(object sender, EventArgs e)
        {
            var frmIncentive = new IncentiveUI();
            frmIncentive.MdiParent = this;

            frmIncentive.Show();
        }


        private void tsbTHR_Click(object sender, EventArgs e)
        {
            if (Store.IsThrClosed == false)
            {
                Store.ActiveForm = "THR";

                var frmParamDate = new ParamDateUI();
                frmParamDate.MdiParent = this;
                frmParamDate.Text = "Proses THR";

                frmParamDate.Show();
            }
            else
            {
                var frmThr = new THRUI();
                frmThr.MdiParent = this;
                frmThr.Show();
            }
        }

        private void mnuRptWorkCalendar_Click(object sender, EventArgs e)
        {
            var userAccess=userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser 
                && u.ObjectName == "Master -> Kalender Kerja" && u.IsOpen);
            if (isAllowed || Store.IsAdministrator)
            {
                Store.ActiveReport = "WorkCalendar";

                var frm1 = new ReportUI();
                frm1.Show();
                frm1.Text = "Kalendar Kerja";
            }
            else
            {
                MessageBox.Show("Anda tidak dapat membuka laporan ini", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void mnuRptInsurance_Click(object sender, EventArgs e)
        {
                  var userAccess=userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser 
                && u.ObjectName == "Master -> Asuransi" && u.IsOpen);
            if (isAllowed || Store.IsAdministrator)
            {
                Store.ActiveReport = "Insurance";

                var frm1 = new ReportUI();
                frm1.Show();
                frm1.Text = "Asuransi";
            }
            else
            {
                MessageBox.Show("Anda tidak dapat membuka laporan ini", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void mnuRptOccupation_Click(object sender, EventArgs e)
        {
                var userAccess=userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser 
                && u.ObjectName == "Master -> Jabatan" && u.IsOpen);
            if (isAllowed || Store.IsAdministrator)
            {
                Store.ActiveReport = "Occupation";

                var frm1 = new ReportUI();
                frm1.Show();
                frm1.Text = "Jabatan";
            }
            else
            {
                MessageBox.Show("Anda tidak dapat membuka laporan ini", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void mnuRptGrade_Click(object sender, EventArgs e)
        {
               var userAccess=userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser 
                && u.ObjectName == "Master -> Golongan" && u.IsOpen);
            if (isAllowed || Store.IsAdministrator)
            {
                Store.ActiveReport = "Grade";

                var frm1 = new ReportUI();
                frm1.Show();
                frm1.Text = "Golongan";
            }
            else
            {
                MessageBox.Show("Anda tidak dapat membuka laporan ini", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void mnuRptDepartment_Click(object sender, EventArgs e)
        {
              var userAccess=userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser 
                && u.ObjectName == "Master -> Departemen" && u.IsOpen);
            if (isAllowed || Store.IsAdministrator)
            {
                Store.ActiveReport = "Department";

                var frm1 = new ReportUI();
                frm1.Show();
                frm1.Text = "Departemen";
            }
            else
            {
                MessageBox.Show("Anda tidak dapat membuka laporan ini", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void mnuRptBranch_Click(object sender, EventArgs e)
        {
             var userAccess=userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser 
                && u.ObjectName == "Master -> Cabang" && u.IsOpen);
            if (isAllowed || Store.IsAdministrator)
            {
                Store.ActiveReport = "Branch";

                var frm1 = new ReportUI();
                frm1.Show();
                frm1.Text = "Cabang";
            }
            else
            {
                MessageBox.Show("Anda tidak dapat membuka laporan ini", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void mnuRptPrincipal_Click(object sender, EventArgs e)
        {
            var userAccess=userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser 
                && u.ObjectName == "Master -> Principal" && u.IsOpen);
            if (isAllowed || Store.IsAdministrator)
            {
                Store.ActiveReport = "Principal";

                var frm1 = new ReportUI();
                frm1.Show();
                frm1.Text = "Principal";
            }
            else
            {
                MessageBox.Show("Anda tidak dapat membuka laporan ini", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void mnuRptIncentiveDetail_Click(object sender, EventArgs e)
        {
                var userAccess=userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser 
                && u.ObjectName == "Insentif -> Rinci" && u.IsOpen);
            if (isAllowed || Store.IsAdministrator)
            {
                Store.ActiveReport = "IncentiveDetail";

                var frmReportParamPeriodUI = new ReportParamPeriodUI();
                frmReportParamPeriodUI.Show();
                frmReportParamPeriodUI.Text = "Insentif - Rinci";
            }
            else
            {
                MessageBox.Show("Anda tidak dapat membuka laporan ini", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void mnuRptIncentiveRecap_Click(object sender, EventArgs e)
        {
                      var userAccess=userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser 
                && u.ObjectName == "Insentif -> Rekap" && u.IsOpen);
            if (isAllowed || Store.IsAdministrator)
            {
                Store.ActiveReport = "IncentiveRecap";

                var frmReportParamPeriodUI = new ReportParamPeriodUI();
                frmReportParamPeriodUI.Show();
                frmReportParamPeriodUI.Text = "Insentif - Rekap";
            }
            else
            {
                MessageBox.Show("Anda tidak dapat membuka laporan ini", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void mnuRptThrDetail_Click(object sender, EventArgs e)
        {
             var userAccess=userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser 
                && u.ObjectName == "THR -> Rinci" && u.IsOpen);
            if (isAllowed || Store.IsAdministrator)
            {
                Store.ActiveReport = "ThrDetail";

                var frmReportParamYearUI = new ReportParamYearUI();
                frmReportParamYearUI.Show();
                frmReportParamYearUI.Text = "THR - Rinci";
            }
            else
            {
                MessageBox.Show("Anda tidak dapat membuka laporan ini", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void mnuRptThrRecap_Click(object sender, EventArgs e)
        {
            var userAccess=userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser 
                && u.ObjectName == "THR -> Rekap" && u.IsOpen);
            if (isAllowed || Store.IsAdministrator)
            {
                Store.ActiveReport = "ThrRecap";

                var frmReportParamYearUI = new ReportParamYearUI();
                frmReportParamYearUI.Show();
                frmReportParamYearUI.Text = "THR - Rekap";
            }
            else
            {
                MessageBox.Show("Anda tidak dapat membuka laporan ini", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void mnuRptEmployeeDebtDetail_Click(object sender, EventArgs e)
        {
            var userAccess=userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser 
                && u.ObjectName == "Piutang Karyawan -> Rinci" && u.IsOpen);
            if (isAllowed || Store.IsAdministrator)
            {
                Store.ActiveReport = "EmployeeDebtDetail";

                var frm1 = new ReportUI();
                frm1.Show();
                frm1.Text = "Piutang Karyawan - Rinci";
            }
            else
            {
                MessageBox.Show("Anda tidak dapat membuka laporan ini", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void mnuRptEmployeeDebtRecap_Click(object sender, EventArgs e)
        {
             var userAccess=userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser 
                && u.ObjectName == "Piutang Karyawan -> Rekap" && u.IsOpen);
            if (isAllowed || Store.IsAdministrator)
            {
                Store.ActiveReport = "EmployeeDebtRecap";

                var frm1 = new ReportUI();
                frm1.Show();
                frm1.Text = "Piutang Karyawan - Rekap";
            }
            else
            {
                MessageBox.Show("Anda tidak dapat membuka laporan ini", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void mnuRptEmployeeDebtUnPaid_Click(object sender, EventArgs e)
        {
               var userAccess=userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser 
                && u.ObjectName == "Piutang Karyawan -> Belum Lunas" && u.IsOpen);
            if (isAllowed || Store.IsAdministrator)
            {
                Store.ActiveReport = "EmployeeDebtUnPaid";

                var frm1 = new ReportUI();
                frm1.Show();
                frm1.Text = "Piutang Karyawan - Belum Lunas";
            }
            else
            {
                MessageBox.Show("Anda tidak dapat membuka laporan ini", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void mnuExport_Click(object sender, EventArgs e)
        {
            var frmExport = new ExportUI();
            frmExport.ShowDialog();


        }

        private void mnuRptThrReceipt_Click(object sender, EventArgs e)
        {
               var userAccess=userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser 
                && u.ObjectName == "THR -> Slip" && u.IsOpen);
            if (isAllowed || Store.IsAdministrator)
            {
                Store.ActiveReport = "ThrReceipt";

                var frmReportParamPeriodUI = new ReportParamPeriodUI();
                frmReportParamPeriodUI.Show();
                frmReportParamPeriodUI.Text = "THR - Slip";
            }
            else
            {
                MessageBox.Show("Anda tidak dapat membuka laporan ini", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void mnuRptIncentiveReceipt_Click(object sender, EventArgs e)
        {
         var userAccess=userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser 
                && u.ObjectName == "Insentif -> Slip" && u.IsOpen);
            if (isAllowed || Store.IsAdministrator)
            {
                Store.ActiveReport = "IncentiveReceipt";

                var frmReportParamPeriodUI = new ReportParamPeriodUI();
                frmReportParamPeriodUI.Show();
                frmReportParamPeriodUI.Text = "Insentif - Slip";
            }
            else
            {
                MessageBox.Show("Anda tidak dapat membuka laporan ini", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void mnuRptEmployeeContractEnd_Click(object sender, EventArgs e)
        {
               var userAccess=userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser 
                && u.ObjectName == "Karyawan -> Habis Kontrak" && u.IsOpen);
            if (isAllowed || Store.IsAdministrator)
            {
                Store.ActiveReport = "EmployeeContractEnd";

                var frm1 = new ReportUI();
                frm1.Show();
                frm1.Text = "Karyawan Habis Kontrak";
            }
            else
            {
                MessageBox.Show("Anda tidak dapat membuka laporan ini", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void mnuRptAbsenceRecap_Click(object sender, EventArgs e)
        {
                    var userAccess=userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser 
                && u.ObjectName == "Absensi -> Rekap" && u.IsOpen);
            if (isAllowed || Store.IsAdministrator)
            {
                Store.ActiveReport = "AbsenceRecap";

                var frmReportParamPeriodUI = new ReportParamPeriodUI();
                frmReportParamPeriodUI.Show();
                frmReportParamPeriodUI.Text = "Absensi - Rekap";
            }
            else
            {
                MessageBox.Show("Anda tidak dapat membuka laporan ini", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void mnuRptAbsenceDetail_Click(object sender, EventArgs e)
        {
                 var userAccess=userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser 
                && u.ObjectName == "Absensi -> Rinci" && u.IsOpen);
            if (isAllowed || Store.IsAdministrator)
            {
                Store.ActiveReport = "AbsenceDetail";

                var frmReportParamYearUI = new ReportParamYearUI();
                frmReportParamYearUI.Show();
                frmReportParamYearUI.Text = "Absensi - Rinci";
            }
            else
            {
                MessageBox.Show("Anda tidak dapat membuka laporan ini", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void mnuClosingPeriod_Click(object sender, EventArgs e)
        {
            var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "Tutup Buku" && u.IsOpen);

            if (isAllowed || Store.IsAdministrator)
            {
                var frmClosingPeriod = new ClosingPeriodUI(this);
                frmClosingPeriod.ShowDialog();


            }
            else
            {
                MessageBox.Show("Anda tidak dapat membuka form ini", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void mnuUpdateSalary_Click(object sender, EventArgs e)
        {
            var frmUpdateSalary = new SalaryUpdateUI();

            frmUpdateSalary.MdiParent = this;
            frmUpdateSalary.Show();

        }

        private void mnuBackup_Click(object sender, EventArgs e)
        {

        }

     

        private void mnuRptSalaryUpdate_Click_1(object sender, EventArgs e)
        {
            var userAccess = userAccessRepository.GetAll();

            bool isAllowed = userAccess.Exists(u => u.FullName == Store.ActiveUser
                && u.ObjectName == "Gaji - Update Gaji" && u.IsOpen);
            if (isAllowed || Store.IsAdministrator)
            {
                Store.ActiveReport = "SalaryUpdate";

                var frm1 = new ReportUI();
                frm1.Show();
                frm1.Text = "Update Gaji";
            }
            else
            {
                MessageBox.Show("Anda tidak dapat membuka form ini", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void mnuImport_Click(object sender, EventArgs e)
        {
            var frmImport = new ImportUI();
            frmImport.ShowDialog();
        }

    


    
    }
}
