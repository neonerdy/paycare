using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Configuration;
using PayCare.Repository;


namespace PayCare.View
{
    public partial class ReportUI : Form
    {
        private ReportParamPeriodUI frmReportParamPeriod;
        private ReportParamYearUI frmReportParamYear;

        public ReportUI()
        {
            InitializeComponent();
        }


        public ReportUI(ReportParamPeriodUI frmReportParamPeriod)
        {
            
            this.frmReportParamPeriod = frmReportParamPeriod;
            InitializeComponent();
        }

        public ReportUI(ReportParamYearUI frmReportParamYear)
        {
            this.frmReportParamYear = frmReportParamYear;
            InitializeComponent();
        }
        public void Login(Table crTable,TableLogOnInfo logOnInfo)
        {
            logOnInfo.ConnectionInfo.ServerName = ConfigurationManager.AppSettings["DatabasePath"] + @"\PAYCARE.mdb";
            logOnInfo.ConnectionInfo.DatabaseName = "";
            logOnInfo.ConnectionInfo.UserID = "";
            logOnInfo.ConnectionInfo.Password="";
            
        }


        private void ReportUI_Load(object sender, EventArgs e)
        {
            var rpt = new ReportDocument();
          
            switch (Store.ActiveReport)
            {
                case "PayrollDetail":
                    rpt.Load(Store.ReportPath + "\\" + Store.ActiveReport + ".rpt");
                    rpt.SummaryInfo.ReportTitle = "Periode : " + Store.GetMonthName(frmReportParamPeriod.PeriodMonth) + " " + frmReportParamPeriod.PeriodYear;

                    rpt.SetParameterValue("Month", frmReportParamPeriod.PeriodMonth);
                    rpt.SetParameterValue("Year", frmReportParamPeriod.PeriodYear);
          
                    break;

                case "PayrollRecap":
                    rpt.Load(Store.ReportPath + "\\" + Store.ActiveReport + ".rpt");
                    rpt.SummaryInfo.ReportTitle = "Periode : " + Store.GetMonthName(frmReportParamPeriod.PeriodMonth) + " " + frmReportParamPeriod.PeriodYear;

                    rpt.SetParameterValue("Month", frmReportParamPeriod.PeriodMonth);
                    rpt.SetParameterValue("Year", frmReportParamPeriod.PeriodYear);

                    break;

                case "PayrollReceipt":
                    rpt.Load(Store.ReportPath + "\\" + Store.ActiveReport + ".rpt");
                    rpt.SummaryInfo.ReportTitle = Store.GetMonthName(frmReportParamPeriod.PeriodMonth) + " " + frmReportParamPeriod.PeriodYear;

                    rpt.SetParameterValue("Month", frmReportParamPeriod.PeriodMonth);
                    rpt.SetParameterValue("Year", frmReportParamPeriod.PeriodYear);

                    break;

                case "AbsenceDetail":
                    rpt.Load(Store.ReportPath + "\\" + Store.ActiveReport + ".rpt");
                    rpt.SummaryInfo.ReportTitle = "Periode : " + frmReportParamYear.PeriodYear;
                    rpt.SetParameterValue("Year", frmReportParamYear.PeriodYear);

                    break;

                case "AbsenceRecap":
                    rpt.Load(Store.ReportPath + "\\" + Store.ActiveReport + ".rpt");
                    rpt.SummaryInfo.ReportTitle = "Periode : " + Store.GetMonthName(frmReportParamPeriod.PeriodMonth) + " " + frmReportParamPeriod.PeriodYear;

                    
                    rpt.SetParameterValue("Month", frmReportParamPeriod.PeriodMonth);
                    rpt.SetParameterValue("Year", frmReportParamPeriod.PeriodYear);

                    break;

                case "OverTimeDetail":
                    rpt.Load(Store.ReportPath + "\\" + Store.ActiveReport + ".rpt");
                    rpt.SummaryInfo.ReportTitle = "Periode : " + Store.GetMonthName(frmReportParamPeriod.PeriodMonth) + " " + frmReportParamPeriod.PeriodYear;

                    rpt.SetParameterValue("Month", frmReportParamPeriod.PeriodMonth);
                    rpt.SetParameterValue("Year", frmReportParamPeriod.PeriodYear);

                    break;
                case "OverTimeRecap":
                    rpt.Load(Store.ReportPath + "\\" + Store.ActiveReport + ".rpt");
                    rpt.SummaryInfo.ReportTitle = "Periode : " + Store.GetMonthName(frmReportParamPeriod.PeriodMonth) + " " + frmReportParamPeriod.PeriodYear;

                    rpt.SetParameterValue("Month", frmReportParamPeriod.PeriodMonth);
                    rpt.SetParameterValue("Year", frmReportParamPeriod.PeriodYear);

                    break;

                case "JamsostekDetail":
                    rpt.Load(Store.ReportPath + "\\" + Store.ActiveReport + ".rpt");
                    rpt.SummaryInfo.ReportTitle = "Periode : " + Store.GetMonthName(frmReportParamPeriod.PeriodMonth) + " " + frmReportParamPeriod.PeriodYear;

                    rpt.SetParameterValue("Month", frmReportParamPeriod.PeriodMonth);
                    rpt.SetParameterValue("Year", frmReportParamPeriod.PeriodYear);

                    break;

                case "JamsostekMember":
                    rpt.Load(Store.ReportPath + "\\" + Store.ActiveReport + ".rpt");
                    rpt.SummaryInfo.ReportTitle = "Per Tanggal : " + DateTime.Now.ToString("d/MM/yyyy");
                    
                    break;

                case "EmployeeDetail":
                    rpt.Load(Store.ReportPath + "\\" + Store.ActiveReport + ".rpt");
                    rpt.SummaryInfo.ReportTitle = "Per Tanggal : " + DateTime.Now.ToString("d/MM/yyyy");
                    
                    break;

                case "EmployeeRecap":
                    rpt.Load(Store.ReportPath + "\\" + Store.ActiveReport + ".rpt");
                    rpt.SummaryInfo.ReportTitle = "Per Tanggal : " + DateTime.Now.ToString("d/MM/yyyy");

                    break;

                case "EmployeeContractEnd":
                    rpt.Load(Store.ReportPath + "\\" + Store.ActiveReport + ".rpt");
                    rpt.SummaryInfo.ReportTitle = "Per Tanggal : " + DateTime.Now.ToString("d/MM/yyyy");

                    break;

                case "WorkCalendar":
                    rpt.Load(Store.ReportPath + "\\" + Store.ActiveReport + ".rpt");
                    rpt.SummaryInfo.ReportTitle = "Per Tanggal : " + DateTime.Now.ToString("d/MM/yyyy");

                    break;

                case "Principal":
                    rpt.Load(Store.ReportPath + "\\" + Store.ActiveReport + ".rpt");
                    rpt.SummaryInfo.ReportTitle = "Per Tanggal : " + DateTime.Now.ToString("d/MM/yyyy");

                    break;

                case "Branch":
                    rpt.Load(Store.ReportPath + "\\" + Store.ActiveReport + ".rpt");
                    rpt.SummaryInfo.ReportTitle = "Per Tanggal : " + DateTime.Now.ToString("d/MM/yyyy");

                    break;

                case "Department":
                    rpt.Load(Store.ReportPath + "\\" + Store.ActiveReport + ".rpt");
                    rpt.SummaryInfo.ReportTitle = "Per Tanggal : " + DateTime.Now.ToString("d/MM/yyyy");

                    break;


                case "Grade":
                    rpt.Load(Store.ReportPath + "\\" + Store.ActiveReport + ".rpt");
                    rpt.SummaryInfo.ReportTitle = "Per Tanggal : " + DateTime.Now.ToString("d/MM/yyyy");

                    break;

                case "Occupation":
                    rpt.Load(Store.ReportPath + "\\" + Store.ActiveReport + ".rpt");
                    rpt.SummaryInfo.ReportTitle = "Per Tanggal : " + DateTime.Now.ToString("d/MM/yyyy");

                    break;

                case "Insurance":
                    rpt.Load(Store.ReportPath + "\\" + Store.ActiveReport + ".rpt");
                    rpt.SummaryInfo.ReportTitle = "Per Tanggal : " + DateTime.Now.ToString("d/MM/yyyy");

                    break;

                case "IncentiveDetail":
                    rpt.Load(Store.ReportPath + "\\" + Store.ActiveReport + ".rpt");
                    rpt.SummaryInfo.ReportTitle = "Periode : " + Store.GetMonthName(frmReportParamPeriod.PeriodMonth) + " " + frmReportParamPeriod.PeriodYear;

                    rpt.SetParameterValue("Month", frmReportParamPeriod.PeriodMonth);
                    rpt.SetParameterValue("Year", frmReportParamPeriod.PeriodYear);

                    break;

                case "IncentiveRecap":
                    rpt.Load(Store.ReportPath + "\\" + Store.ActiveReport + ".rpt");
                    rpt.SummaryInfo.ReportTitle = "Periode : " + Store.GetMonthName(frmReportParamPeriod.PeriodMonth) + " " + frmReportParamPeriod.PeriodYear;

                    rpt.SetParameterValue("Month", frmReportParamPeriod.PeriodMonth);
                    rpt.SetParameterValue("Year", frmReportParamPeriod.PeriodYear);

                    break;


                case "IncentiveReceipt":
                    rpt.Load(Store.ReportPath + "\\" + Store.ActiveReport + ".rpt");
                    rpt.SummaryInfo.ReportTitle = Store.GetMonthName(frmReportParamPeriod.PeriodMonth) + " " + frmReportParamPeriod.PeriodYear;

                    rpt.SetParameterValue("Month", frmReportParamPeriod.PeriodMonth);
                    rpt.SetParameterValue("Year", frmReportParamPeriod.PeriodYear);

                    break;


                case "ThrDetail":
                    rpt.Load(Store.ReportPath + "\\" + Store.ActiveReport + ".rpt");
                    rpt.SummaryInfo.ReportTitle = "Periode : " + frmReportParamYear.PeriodYear;
                    rpt.SetParameterValue("Year", frmReportParamYear.PeriodYear);

                    break;

                case "ThrRecap":
                    rpt.Load(Store.ReportPath + "\\" + Store.ActiveReport + ".rpt");
                    rpt.SummaryInfo.ReportTitle = "Periode : " + frmReportParamYear.PeriodYear;
                    rpt.SetParameterValue("Year", frmReportParamYear.PeriodYear);

                    break;

                case "ThrReceipt":
                    rpt.Load(Store.ReportPath + "\\" + Store.ActiveReport + ".rpt");
                    rpt.SummaryInfo.ReportTitle = Store.GetMonthName(frmReportParamPeriod.PeriodMonth) + " " + frmReportParamPeriod.PeriodYear;

                    rpt.SetParameterValue("Year", frmReportParamPeriod.PeriodYear);

                    break;


                case "EmployeeDebtDetail":
                    rpt.Load(Store.ReportPath + "\\" + Store.ActiveReport + ".rpt");
                    rpt.SummaryInfo.ReportTitle = "Per Tanggal : " + DateTime.Now.ToString("d/MM/yyyy");

                    break;

                case "EmployeeDebtRecap":
                    rpt.Load(Store.ReportPath + "\\" + Store.ActiveReport + ".rpt");
                    rpt.SummaryInfo.ReportTitle = "Per Tanggal : " + DateTime.Now.ToString("d/MM/yyyy");

                    break;

                case "EmployeeDebtUnPaid":
                    rpt.Load(Store.ReportPath + "\\" + Store.ActiveReport + ".rpt");
                    rpt.SummaryInfo.ReportTitle = "Per Tanggal : " + DateTime.Now.ToString("d/MM/yyyy");

                    break;


                case "SalaryUpdate":
                    rpt.Load(Store.ReportPath + "\\" + Store.ActiveReport + ".rpt");
                    rpt.SummaryInfo.ReportTitle = "Per Tanggal : " + DateTime.Now.ToString("d/MM/yyyy");

                    break;
            }


            foreach (Table crTable in rpt.Database.Tables)
            {
                TableLogOnInfo logOnInfo = new TableLogOnInfo();
                logOnInfo = rpt.Database.Tables[crTable.Name].LogOnInfo;

                Login(crTable, logOnInfo);

                rpt.Database.Tables[crTable.Name].ApplyLogOnInfo(logOnInfo);
            }

            crystalReportViewer1.ReportSource = rpt;
            crystalReportViewer1.Refresh();
            

        }
    }
}
