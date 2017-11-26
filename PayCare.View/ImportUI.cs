using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Excel = Microsoft.Office.Interop.Excel;
using PayCare.Repository;
using EntityMap;
using PayCare.Model;
using System.Globalization;


namespace PayCare.View
{
    public enum ImportType
    {
        Employee, Absence, Overtime, Incentive
    }

    public partial class ImportUI : Form
    {

        private ImportType importType;
        private string fileName;
        private IIncentiveRepository incentiveRepository;
        private IEmployeeRepository employeeRepository;
        private IAbsenceRepository absenceRepository;
        private IOverTimeRepository overTimeRepository;
        private IBranchRepository branchRepository;
        private IDepartmentRepository departmentRepository;
        private IGradeRepository gradeRepository;
        private IOccupationRepository occupationRepository;
        private IPrincipalRepository principalRepository;
        private IInsuranceProgramRepository insuranceProgramRepository;
       
        private IEmployeeDepartmentRepository employeeDepartmentRepository;
        private IEmployeeGradeRepository employeeGradeRepository;
        private IEmployeeOccupationRepository employeeOccupationRepository;
        private IEmployeePrincipalRepository employeePrincipalRepository;
        private IEmployeeStatusRepository employeeStatusRepository;
        private IEmployeeInsuranceRepository employeeInsuranceRepository;
        private IEmployeeSalaryRepository employeeSalaryRepository;

        public ImportUI()
        {
            incentiveRepository = EntityContainer.GetType<IIncentiveRepository>();
            employeeRepository = EntityContainer.GetType<IEmployeeRepository>();
            absenceRepository = EntityContainer.GetType<IAbsenceRepository>();
            overTimeRepository = EntityContainer.GetType<IOverTimeRepository>();
            branchRepository = EntityContainer.GetType<IBranchRepository>();
            departmentRepository = EntityContainer.GetType<IDepartmentRepository>();
            gradeRepository = EntityContainer.GetType<IGradeRepository>();
            occupationRepository = EntityContainer.GetType<IOccupationRepository>();
            principalRepository = EntityContainer.GetType<IPrincipalRepository>();
            insuranceProgramRepository = EntityContainer.GetType<IInsuranceProgramRepository>();

                                    
            employeeDepartmentRepository = EntityContainer.GetType<IEmployeeDepartmentRepository>();
            employeeGradeRepository = EntityContainer.GetType<IEmployeeGradeRepository>();
            employeeOccupationRepository = EntityContainer.GetType<IEmployeeOccupationRepository>();
            employeePrincipalRepository = EntityContainer.GetType<IEmployeePrincipalRepository>();
            employeeStatusRepository = EntityContainer.GetType<IEmployeeStatusRepository>();
            employeeInsuranceRepository = EntityContainer.GetType<IEmployeeInsuranceRepository>();
            employeeSalaryRepository = EntityContainer.GetType<IEmployeeSalaryRepository>();
                 
       
            InitializeComponent();

        }

        
        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "Excel File|*.xlsx";

            dialog.InitialDirectory = "C:";
            dialog.Title = "Import";
            dialog.FileName = this.fileName;

            if (dialog.ShowDialog() == DialogResult.OK)
                fileName = dialog.FileName;
            if (fileName == String.Empty)
                return;

            try
            {
                ImportData(importType,fileName);
                MessageBox.Show("Import sukses!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal import data, kemungkinan data Excel tidak valid", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }           
        }

     


        private void ImportData(ImportType importType,string fileName)
        {
            switch (importType)
            {
                case ImportType.Employee :
                    ImportEmployee(fileName);
                    ImportEmployeeDetail(fileName);
                    break;

                case ImportType.Absence :
                    absenceRepository.Delete(Store.ActiveMonth, Store.ActiveYear);
                    ImportAbsence(fileName);
                    
                    break;

                case ImportType.Overtime :
                    overTimeRepository.Delete(Store.ActiveMonth, Store.ActiveYear);
                    ImportOvertime(fileName);
                    break;

                case ImportType.Incentive:
                    incentiveRepository.Delete(Store.ActiveMonth, Store.ActiveYear);
                    ImportIncentive(fileName);

                    break;

            }
        
        
        }



        private void UpdateEmployeeCurrentInfo(Guid employeeId, string branchName,string departmentName,string gradeName,
            string occupationName, string employeeStatus,string paymentType)
        {

            Guid branchId = Guid.Empty;
            Guid departmentId = Guid.Empty;
            Guid gradeId = Guid.Empty;
            int gradeLevel = 0;
            Guid occupationId = Guid.Empty;

            var branch=branchRepository.GetByName(branchName);
            if (branchName!=null)
            {
                branchId=branch.ID;
            }

            var department=departmentRepository.GetByName(departmentName);
            if (department!=null)
            {
                departmentId=department.ID;
            }

            var grade=gradeRepository.GetByName(gradeName);
            if (grade!=null) 
            {
                gradeId=grade.ID;
                gradeLevel=grade.GradeLevel;
            }

            var occupation = occupationRepository.GetByName(occupationName);
            if (occupation != null)
            {
                occupationId = occupation.ID;
            }


            var employeeCurrentInfo = new EmployeeCurrentInfo();

            employeeCurrentInfo.EmployeeId = employeeId;
            employeeCurrentInfo.BranchId = branchId;
            employeeCurrentInfo.BranchName = branchName;
            employeeCurrentInfo.DepartmentId = departmentId;
            employeeCurrentInfo.DepartmentName = departmentName;
            employeeCurrentInfo.GradeId = gradeId;
            employeeCurrentInfo.GradeName = gradeName;
            employeeCurrentInfo.GradeLevel = gradeLevel;
            employeeCurrentInfo.OccupationId = occupationId;
            employeeCurrentInfo.OccupationName = occupationName;
            employeeCurrentInfo.EmployeeStatus = employeeStatus;
            employeeCurrentInfo.PaymentType = paymentType;

            employeeRepository.UpdateCurrentInfo(employeeCurrentInfo);

        }




        private void ImportEmployeeDetail(string fileName)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range range;

            xlApp = new Excel.ApplicationClass();

            xlWorkBook = xlApp.Workbooks.Open(fileName, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(2);

            range = xlWorkSheet.UsedRange;

            for (var row = 2; row <= range.Rows.Count; row++)
            {
                string nik = (string)(range.Cells[row, 1] as Excel.Range).Value2;
                string branch = (string)(range.Cells[row, 2] as Excel.Range).Value2;
                string department = (string)(range.Cells[row, 3] as Excel.Range).Value2;
                string departmentDate = (string)(range.Cells[row, 4] as Excel.Range).Value2;
                string grade = (string)(range.Cells[row, 5] as Excel.Range).Value2;
                string gradeDate = (string)(range.Cells[row, 6] as Excel.Range).Value2;
                string occupation = (string)(range.Cells[row, 7] as Excel.Range).Value2;
                string occupationDate = (string)(range.Cells[row, 8] as Excel.Range).Value2;
                string isTaskForce = (string)(range.Cells[row, 9] as Excel.Range).Value2;
                string principal = (string)(range.Cells[row, 10] as Excel.Range).Value2;
                string principalDate = (string)(range.Cells[row, 11] as Excel.Range).Value2;
                string status = (string)(range.Cells[row, 12] as Excel.Range).Value2;
                string paymentType = (string)(range.Cells[row, 13] as Excel.Range).Value2;
                string statusDate = (string)(range.Cells[row, 14] as Excel.Range).Value2;
                string jamsostekNumber = (string)(range.Cells[row, 15] as Excel.Range).Value2;
                string jamsostekDate = (string)(range.Cells[row, 16] as Excel.Range).Value2;
                

                string salaryDate = (string)(range.Cells[row, 17] as Excel.Range).Value2;
                double mainSalary = (double)(range.Cells[row, 18] as Excel.Range).Value2;
                double occupationAllowance = (double)(range.Cells[row, 19] as Excel.Range).Value2;
                double fixedAllowance = (double)(range.Cells[row, 20] as Excel.Range).Value2;
                double healthAllowance = (double)(range.Cells[row, 21] as Excel.Range).Value2;
                double communicationAllowance = (double)(range.Cells[row, 22] as Excel.Range).Value2;
                double supervisionAllowance = (double)(range.Cells[row, 23] as Excel.Range).Value2;
                double otherAllowance = (double)(range.Cells[row, 24] as Excel.Range).Value2;
                double fuelAllowance = (double)(range.Cells[row, 25] as Excel.Range).Value2;
                double vehicleAllowance = (double)(range.Cells[row, 26] as Excel.Range).Value2;
                double lunchAllowance = (double)(range.Cells[row, 27] as Excel.Range).Value2;
                double transportAllowance = (double)(range.Cells[row, 28] as Excel.Range).Value2;
                                

                if (nik == null) break;
                
                var employee=employeeRepository.GetByCode(nik);

                if (employee!=null)
                {
                    //Department

                    var employeeDepartment = new EmployeeDepartment();
                    
                    employeeDepartment.EmployeeId = employee.ID;
                    employeeDepartment.EffectiveDate = DateTime.ParseExact(departmentDate,
                        "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    employeeDepartment.BranchId = branchRepository.GetByName(branch).ID;
                    employeeDepartment.DepartmentId = departmentRepository.GetByName(department).ID;
                    
                    employeeDepartmentRepository.Save(employeeDepartment);


                    //Grade

                    var employeeGrade = new EmployeeGrade();

                    employeeGrade.EmployeeId = employee.ID;
                    employeeGrade.EffectiveDate = DateTime.ParseExact(gradeDate,
                        "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    employeeGrade.GradeId = gradeRepository.GetByName(grade).ID;

                    employeeGradeRepository.Save(employeeGrade);
                                
                
                    //Occupation

                    var employeeOccupation = new EmployeeOccupation();

                    employeeOccupation.EmployeeId = employee.ID;
                    employeeOccupation.EffectiveDate = DateTime.ParseExact(occupationDate,
                        "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    employeeOccupation.OccupationId = occupationRepository.GetByName(occupation).ID;
                    employeeOccupation.IsTaskForce = isTaskForce.ToUpper() == "Y" ? true : false;

                    employeeOccupationRepository.Save(employeeOccupation);
                

                    //Status

                    var employeeStatus = new EmployeeStatus();

                    employeeStatus.EmployeeId = employee.ID;
                    employeeStatus.EffectiveDate = DateTime.ParseExact(statusDate,
                        "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    employeeStatus.IsEnd = false;
                    employeeStatus.EndDate = DateTime.Now;
                    employeeStatus.Status = status;
                    employeeStatus.PaymentType = paymentType;

                    employeeStatusRepository.Save(employeeStatus);
                    
                    
                    //Insurance

                    var insurancePrograms = insuranceProgramRepository.GetByInsuranceId(Guid.Empty);
                 
                    foreach(var program in insurancePrograms)
                    {
                        var employeeInsurance = new EmployeeInsurance();

                        employeeInsurance.EmployeeId = employee.ID;
                        employeeInsurance.InsuranceId = Guid.Empty;
                        employeeInsurance.InsuranceProgramId = program.ID;

                        employeeInsurance.EffectiveDate = DateTime.ParseExact(jamsostekDate,
                            "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        employeeInsurance.EndDate = DateTime.Now;
                        employeeInsurance.InsuranceNumber = jamsostekNumber;

                        employeeInsuranceRepository.Save(employeeInsurance);
                        
                    }


                    //Salary

                    var employeeSalary = new EmployeeSalary();

                    employeeSalary.EmployeeId = employee.ID;
                    employeeSalary.EffectiveDate = DateTime.ParseExact(salaryDate,
                            "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    employeeSalary.MainSalary = Convert.ToDecimal(mainSalary);
                    employeeSalary.OccupationAllowancePerMonth = Convert.ToDecimal(occupationAllowance);
                    employeeSalary.FixedAllowancePerMonth = Convert.ToDecimal(fixedAllowance);
                    employeeSalary.HealthAllowancePerMonth = Convert.ToDecimal(healthAllowance);
                    employeeSalary.CommunicationAllowancePerMonth = Convert.ToDecimal(communicationAllowance);
                    employeeSalary.SupervisionAllowancePerMonth = Convert.ToDecimal(supervisionAllowance);
                    employeeSalary.OtherAllowance = Convert.ToDecimal(otherAllowance);
                    employeeSalary.FuelAllowancePerDays = Convert.ToDecimal(fuelAllowance);
                    employeeSalary.VehicleAllowancePerDays = Convert.ToDecimal(vehicleAllowance);
                    employeeSalary.LunchAllowancePerDays = Convert.ToDecimal(lunchAllowance);
                    employeeSalary.TransportationAllowancePerDays = Convert.ToDecimal(transportAllowance);
                    employeeSalary.JamsostekAmount = 0;
                    employeeSalary.PersonalDebt = 0;
                    employeeSalary.OtherFee = 0;               
                    
                    employeeSalaryRepository.Save(employeeSalary);


                    UpdateEmployeeCurrentInfo(employee.ID, branch, department, grade, occupation, status, paymentType);
               
                }
              
            }

            xlWorkBook.Close(true, null, null);
            xlApp.Quit();

            ReleaseObject(xlWorkSheet);
            ReleaseObject(xlWorkBook);
            ReleaseObject(xlApp);

        }



        private void ImportEmployee(string fileName)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range range;

            xlApp = new Excel.ApplicationClass();

            xlWorkBook = xlApp.Workbooks.Open(fileName, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            range = xlWorkSheet.UsedRange;
                      
            for (var row = 2; row <= range.Rows.Count; row++)
            {
                string nik = (string)(range.Cells[row, 1] as Excel.Range).Value2;
                string oldNik = (string)(range.Cells[row, 2] as Excel.Range).Value2;
                string name = (string)(range.Cells[row, 3] as Excel.Range).Value2;
                string birthPlace = (string)(range.Cells[row, 4] as Excel.Range).Value2;
                string birthDate = (string)(range.Cells[row, 5] as Excel.Range).Value2;
                string gender = (string)(range.Cells[row, 6] as Excel.Range).Value2;
                string religion = (string)(range.Cells[row,7] as Excel.Range).Value2;
                string transfer = (string)(range.Cells[row, 8] as Excel.Range).Value2;
                string bankName = (string)(range.Cells[row, 9] as Excel.Range).Value2;
                string accountNumber = (string)(range.Cells[row, 10] as Excel.Range).Value2;
                string martialStatus = (string)(range.Cells[row, 11] as Excel.Range).Value2;
                string numberOfChild = (string)(range.Cells[row, 12] as Excel.Range).Value2;
                string isFuelAllowance = (string)(range.Cells[row, 13] as Excel.Range).Value2;
                string isPrincipal = (string)(range.Cells[row, 14] as Excel.Range).Value2;
                string isInsurance = (string)(range.Cells[row, 15] as Excel.Range).Value2;
                string startDate = (string)(range.Cells[row, 16] as Excel.Range).Value2;
                string branch = (string)(range.Cells[row, 17] as Excel.Range).Value2;
              
                if (nik == null) break;

                var employee = new Employee();

                employee.EmployeeCode = nik;
                employee.OldEmployeeCode = oldNik;
                employee.EmployeeName = name;
                employee.BirthPlace = birthPlace;
                employee.BirthDate = DateTime.ParseExact(birthDate,
                        "dd/MM/yyyy", CultureInfo.InvariantCulture);

                employee.Gender = gender.ToUpper() == "L" ? true : false;
                employee.Religion = religion;
                employee.IsTransfer = transfer.ToUpper() == "Y" ? true : false;
                employee.BankName = bankName;
                employee.AccountNumber = accountNumber;
                employee.MaritalStatus = martialStatus.ToUpper() == "K" ? true : false;
                employee.NumberOfChilds = numberOfChild == "-" ? 0 : int.Parse(numberOfChild);
                employee.IsInsurance = isInsurance.ToUpper() == "Y" ? true : false;
                employee.IsTax = false;
                employee.NPWP = string.Empty;
                employee.PTKPId = Guid.Empty;
                employee.IsFuelAllowance = isFuelAllowance.ToUpper() == "Y" ? true : false;
                employee.IsPrincipal = isPrincipal.ToUpper() == "Y" ? true : false;
                employee.StartDate = DateTime.ParseExact(startDate,
                        "dd/MM/yyyy", CultureInfo.InvariantCulture);
                employee.EndDate = DateTime.Now;
                employee.IsActive = true;

                employee.CurrentInfo = new EmployeeCurrentInfo();
                employee.CurrentInfo.BranchName = branch;

                var existedEmployee = employeeRepository.GetByCode(nik);
                if (existedEmployee == null)
                {
                    employeeRepository.SaveHeader(employee);
                }
                else
                {
                    employeeRepository.UpdateHeader(employee);
                }
            }

            xlWorkBook.Close(true, null, null);
            xlApp.Quit();

            ReleaseObject(xlWorkSheet);
            ReleaseObject(xlWorkBook);
            ReleaseObject(xlApp);

        }





        private void ImportAbsence(string fileName)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range range;

            xlApp = new Excel.ApplicationClass();

            xlWorkBook = xlApp.Workbooks.Open(fileName, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            range = xlWorkSheet.UsedRange;

            double month = (double)(range.Cells[1, 2] as Excel.Range).Value2;
            double year = (double)(range.Cells[2, 2] as Excel.Range).Value2;
            
            double total = 0;
            
            for (var row = 4; row <= range.Rows.Count; row++)
            {
                string nik = (string)(range.Cells[row, 1] as Excel.Range).Value2;
                double workDay = (double)(range.Cells[row, 2] as Excel.Range).Value2;
                double onLeaveDay = (double)(range.Cells[row, 3] as Excel.Range).Value2;
                double offDay = (double)(range.Cells[row, 4] as Excel.Range).Value2;

                total = workDay + onLeaveDay + offDay;

                if (nik == null) break;

                var employee = employeeRepository.GetByCode(nik);

                if (employee != null)
                {
                    Guid employeeId = employee.ID;
                    string branch = employee.CurrentInfo.BranchName;
                    string department = employee.CurrentInfo.DepartmentName;
                  
                    var absence = new Absence();

                    absence.MonthPeriod = Convert.ToInt32(month);
                    absence.YearPeriod = Convert.ToInt32(year);
                    absence.AbsenceStartDate = DateTime.Now;
                    absence.AbsenceEndDate = DateTime.Now;
                    absence.EmployeeId = employeeId;
                    absence.WorkDay = Convert.ToInt32(workDay);
                    absence.OnLeaveDay = Convert.ToInt32(onLeaveDay);
                    absence.OffDay = Convert.ToInt32(offDay);
                    absence.Total = Convert.ToInt32(total);
                    absence.Branch = branch;
                    absence.Department = department;

                    absenceRepository.Save(absence);
                }

            }

            xlWorkBook.Close(true, null, null);
            xlApp.Quit();

            ReleaseObject(xlWorkSheet);
            ReleaseObject(xlWorkBook);
            ReleaseObject(xlApp);

        }



        private void ImportOvertime(string fileName)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range range;

            xlApp = new Excel.ApplicationClass();

            xlWorkBook = xlApp.Workbooks.Open(fileName, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            range = xlWorkSheet.UsedRange;

            double month = (double)(range.Cells[1, 2] as Excel.Range).Value2;
            double year = (double)(range.Cells[2, 2] as Excel.Range).Value2;

            int dayType = 0;

            for (var row = 4; row <= range.Rows.Count; row++)
            {
                string nik = (string)(range.Cells[row, 1] as Excel.Range).Value2;
                string type = (string)(range.Cells[row, 2] as Excel.Range).Value2;
                string date = (string)(range.Cells[row, 3] as Excel.Range).Value2;
                string startHour = (string)(range.Cells[row, 4] as Excel.Range).Value2;
                string endHour = (string)(range.Cells[row, 5] as Excel.Range).Value2;
                string notes = (string)(range.Cells[row, 6] as Excel.Range).Value2;

                if (nik == null) break;
               
                if (type == "K")
                {
                    dayType = 0;
                }
                else if (type == "L")
                {
                    dayType = 1;
                }

                var employee = employeeRepository.GetByCode(nik);

                if (employee != null)
                {

                    Guid employeeId = employee.ID;
                    string branch = employee.CurrentInfo.BranchName;
                    string department = employee.CurrentInfo.DepartmentName;
              
                    var overTime = new OverTime();

                    overTime.MonthPeriod = Convert.ToInt32(month);
                    overTime.YearPeriod = Convert.ToInt32(year);
                    overTime.EmployeeId = employeeId;
                    overTime.Branch = branch;
                    overTime.Department = department;
                    overTime.IsIncludePayroll = false;
                    overTime.IsPaid = false;
                    overTime.DayType = dayType;
                    overTime.OverTimeDate = DateTime.ParseExact(date,
                        "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    overTime.StartHour = startHour;
                    overTime.EndHour = endHour;
                    overTime.TotalInMinute = Store.GetTotalOverTimeInMinute(startHour, endHour);
                    overTime.TotalInHour = Store.GetTotalInHour(overTime.TotalInMinute);
                    overTime.Amount = Math.Round(overTimeRepository.CalculateOverTime(overTime.EmployeeId, overTime.TotalInMinute, overTime.DayType), 0);
                    overTime.AmountInWords = Store.GetAmounInWords(Convert.ToInt32(overTime.Amount));
                    overTime.Notes = notes;

                    overTimeRepository.Save(overTime);
                }

            }

            xlWorkBook.Close(true, null, null);
            xlApp.Quit();

            ReleaseObject(xlWorkSheet);
            ReleaseObject(xlWorkBook);
            ReleaseObject(xlApp);

        }







        private void ImportIncentive(string fileName)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range range;

            xlApp = new Excel.ApplicationClass();

            xlWorkBook = xlApp.Workbooks.Open(fileName, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            range = xlWorkSheet.UsedRange;

            double month = (double)(range.Cells[1, 2] as Excel.Range).Value2;
            double year = (double)(range.Cells[2, 2] as Excel.Range).Value2;


            for (var row = 4; row <= range.Rows.Count; row++)
            {
                string nik = (string)(range.Cells[row, 1] as Excel.Range).Value2;
                double amount = (double)(range.Cells[row, 2] as Excel.Range).Value2;
                string notes = (string)(range.Cells[row, 3] as Excel.Range).Value2;

                if (nik == null) break;

                var employee = employeeRepository.GetByCode(nik);

                if (employee != null)
                {
                    Guid employeeId = employee.ID;
                    string branch = employee.CurrentInfo.BranchName;
                    string department = employee.CurrentInfo.DepartmentName;
                    string bankName = employee.CurrentInfo.BankName;
                    string accountNumber = employee.AccountNumber;
                    bool isTransfer = employee.IsTransfer;

                    var incentive = new Incentive();

                    incentive.MonthPeriod = Convert.ToInt32(month);
                    incentive.YearPeriod = Convert.ToInt32(year);
                    incentive.EmployeeId = employeeId;
                    incentive.Branch = branch;
                    incentive.Department = department;
                    incentive.IsTransfer = isTransfer;
                    incentive.BankName = bankName;
                    incentive.AccountNumber = accountNumber;
                    incentive.IsIncludePayroll = false;
                    incentive.IsPaid = false;
                    incentive.Amount = Convert.ToDecimal(amount);
                    incentive.Notes = notes;
                    incentive.AmountInWords = Store.GetAmounInWords(Convert.ToInt32(amount));

                    incentiveRepository.Save(incentive);
                }

            }

            xlWorkBook.Close(true, null, null);
            xlApp.Quit();

            ReleaseObject(xlWorkSheet);
            ReleaseObject(xlWorkBook);
            ReleaseObject(xlApp);
        }



        private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void rbAbsence_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAbsence.Checked)
            {
                importType = ImportType.Absence;
            }
        }


        private void rbOvertime_CheckedChanged(object sender, EventArgs e)
        {
           if (rbOvertime.Checked)
            {
                importType = ImportType.Overtime;
            }
        }


        private void rbIncentive_CheckedChanged(object sender, EventArgs e)
        {
            if (rbIncentive.Checked)
            {
                importType = ImportType.Incentive;
            }
           
        }

        private void rbEmployee_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEmployee.Checked)
            {
                importType = ImportType.Employee;
            }
        }

        private void ImportUI_Load(object sender, EventArgs e)
        {
            importType = ImportType.Employee;
        }



       



    }
}
