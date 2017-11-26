
using System;
using System.Collections.Generic;
using PayCare.Model;

using System.Data;
using PayCare.Repository.Mapping;
using EntityMap;
using System.Data.OleDb;
using System.Configuration;


namespace PayCare.Repository
{
    public interface IPayrollRepository
    {
        Payroll GetById(Guid id);
        Payroll GetLast();
        Payroll GetLast(int month, int year);
        Payroll GetByEmployeeId(Guid employeeId);
        Payroll GetByEmployeeId(Guid employeeId, int month, int year);
        List<Payroll> GetAll();
        List<Payroll> GetTransfered(int month, int year);
        List<Payroll> GetTop(int top,int month, int year);
        List<Payroll> Search(string value);
        List<Payroll> Search(string value, int month, int year);
        void Save(Payroll payroll);
        void Update(Payroll payroll);
        void UpdateValue(Payroll payroll);
        void Delete(Guid id);
        Payroll GetByEmployeeCode(string employeeCode, int month, int year);
        void CalculatePayroll(DateTime effectiveDate, bool isIncentive, bool isOverTime, bool isEmployeeDebt, int month, int year);
        bool IsExisted(Guid employeeId, DateTime payrollDate);
        void UpdateIsPaid(int month, int year, bool paid);
        void UpdateCurrentInfo(int month, int year, EmployeeCurrentInfo currentInfo);
    }

    public class PayrollRepository : IPayrollRepository
    {
        private string tableName = "Payroll";
        private DataSource ds;
        private ICompanyRepository companyRepository;
        private IAbsenceRepository absenceRepository;

        private IEmployeeRepository employeeRepository;
        private IEmployeeSalaryRepository employeeSalaryRepository;
        private IEmployeeDepartmentRepository employeeDepartmentRepository;
        private IEmployeeGradeRepository employeeGradeRepository;
        private IEmployeeOccupationRepository employeeOccupationRepository;
        private IEmployeeStatusRepository employeeStatusRepository;
        private IEmployeePrincipalRepository employeePrincipalRepository;
        private IEmployeeInsuranceRepository employeeInsuranceRepository;
        private IEmployeeFamilyRepository employeeFamilyRepository;

        private IOverTimeRepository overTimeRepository;
        private IIncentiveRepository incentiveRepository;
        private IEmployeeDebtItemRepository employeeDebtItemRepository;
        private IPrincipalItemRepository principalItemRepository;
        private IWorkCalendarRepository workCalendarRepository;

        public PayrollRepository(DataSource ds)
        {
            this.ds = ds;
            companyRepository = EntityContainer.GetType<ICompanyRepository>();
            absenceRepository = EntityContainer.GetType<IAbsenceRepository>();

            employeeRepository = EntityContainer.GetType<IEmployeeRepository>();
            employeeSalaryRepository = EntityContainer.GetType<IEmployeeSalaryRepository>();
            employeeDepartmentRepository = EntityContainer.GetType<IEmployeeDepartmentRepository>();
            employeeGradeRepository = EntityContainer.GetType<IEmployeeGradeRepository>();
            employeeOccupationRepository = EntityContainer.GetType<IEmployeeOccupationRepository>();
            employeeStatusRepository = EntityContainer.GetType<IEmployeeStatusRepository>();
            employeePrincipalRepository = EntityContainer.GetType<IEmployeePrincipalRepository>();
            employeeInsuranceRepository = EntityContainer.GetType<IEmployeeInsuranceRepository>();
            employeeFamilyRepository = EntityContainer.GetType<IEmployeeFamilyRepository>();


            overTimeRepository = EntityContainer.GetType<IOverTimeRepository>();
            incentiveRepository = EntityContainer.GetType<IIncentiveRepository>();
            employeeDebtItemRepository = EntityContainer.GetType<IEmployeeDebtItemRepository>();
            principalItemRepository = EntityContainer.GetType<IPrincipalItemRepository>();
            workCalendarRepository = EntityContainer.GetType<IWorkCalendarRepository>();


        }


        public Payroll GetById(Guid id)
        {
            Payroll payroll = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT p.*, "
                        + "e.EmployeeCode, e.OldEmployeeCode, e.EmployeeName, "
                        + "e.IsTransfer, e.BankName, e.AccountNumber, "
                        + "e.IsInsurance, e.IsTax, e.IsPrincipal, "
                        + "e.IsFuelAllowance "
                        + "FROM Payroll p INNER JOIN Employee e ON p.EmployeeId = e.ID "
                        + "WHERE "
                        + "p.ID='{" + id + "}'";

                //var sql = "SELECT p.* "
                //        + "FROM Payroll p "
                //        + "WHERE "
                //        + "p.ID='{" + id + "}'";

                payroll = em.ExecuteObject<Payroll>(sql, new PayrollMapper());
            }   

            return payroll;
        }


        public Payroll GetLast()
        {
            Payroll payroll = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT TOP 1 p.*, "
                        + "e.EmployeeCode, e.OldEmployeeCode, e.EmployeeName, "
                        + "e.IsTransfer, e.BankName, e.AccountNumber, "
                        + "e.IsInsurance, e.IsTax, e.IsPrincipal, "
                        + "e.IsFuelAllowance "
                        + "FROM Payroll p INNER JOIN Employee e ON p.EmployeeId = e.ID "
                        + "ORDER BY p.PayrollDate DESC, e.EmployeeCode DESC";

                //var sql = "SELECT TOP 1 p.* "
                //      + "FROM Payroll p "
                //      + "ORDER BY p.PayrollDate DESC, p.EmployeeCode DESC";

                payroll = em.ExecuteObject<Payroll>(sql, new PayrollMapper());
            }

            return payroll;
        }

        public Payroll GetLast(int month, int year)
        {
            Payroll payroll = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT TOP 1 p.*, e.* "
                        + "FROM Payroll p INNER JOIN Employee e ON p.EmployeeId = e.ID "
                        + "WHERE "
                        + "p.MonthPeriod=" + month + " AND p.YearPeriod=" + year + " "
                        + "ORDER BY p.PayrollDate DESC, e.EmployeeCode DESC";

                //var sql = "SELECT TOP 1 p.* "
                //       + "FROM Payroll p "
                //       + "WHERE "
                //       + "p.MonthPeriod=" + month + " AND p.YearPeriod=" + year + " "
                //       + "ORDER BY p.PayrollDate DESC, p.EmployeeCode DESC";


                payroll = em.ExecuteObject<Payroll>(sql, new PayrollMapper());
            }

            return payroll;
        }


        public Payroll GetByEmployeeId(Guid employeeId)
        {
            Payroll payroll = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT p.*, e.* "
                        + "WHERE "
                        + "p.EmployeeId='{" + employeeId + "}' "
                        + "ORDER BY p.PayrollDate DESC, e.EmployeeCode DESC";

                //var sql = "SELECT p.* "
                //        + "WHERE "
                //        + "p.EmployeeId='{" + employeeId + "}' "
                //        + "ORDER BY p.PayrollDate DESC, p.EmployeeCode DESC";


                payroll = em.ExecuteObject<Payroll>(sql, new PayrollMapper());
            }

            return payroll;
        }

        public Payroll GetByEmployeeId(Guid employeeId, int month, int year)
        {
            Payroll payroll = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT p.*,e.* "
                        + "FROM Payroll p INNER JOIN Employee e ON p.EmployeeId = e.ID "
                        + "WHERE "
                        + "p.EmployeeId='{" + employeeId + "}' "
                        + "AND p.MonthPeriod=" + month + " AND p.YearPeriod=" + year + " "
                        + "ORDER BY p.PayrollDate DESC, e.EmployeeCode DESC";

                //var sql = "SELECT p.* "
                //     + "FROM Payroll p "
                //     + "WHERE "
                //     + "p.EmployeeId='{" + employeeId + "}' "
                //     + "AND p.MonthPeriod=" + month + " AND p.YearPeriod=" + year + " "
                //     + "ORDER BY p.PayrollDate DESC, p.EmployeeCode DESC";

                payroll = em.ExecuteObject<Payroll>(sql, new PayrollMapper());
            }

            return payroll;
        }

        public List<Payroll> GetAll()
        {
            List<Payroll> payrolls = new List<Payroll>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT p.*,e.* "
                        + "FROM Payroll p INNER JOIN Employee e ON p.EmployeeId = e.ID "
                        + "ORDER BY p.PayrollDate DESC, e.EmployeeCode DESC";

                //var sql = "SELECT p.* "
                //   + "FROM Payroll p "
                //   + "ORDER BY p.PayrollDate DESC, p.EmployeeCode DESC";

                payrolls = em.ExecuteList<Payroll>(sql, new PayrollMapper());
            }

            return payrolls;
        }


        public List<Payroll> GetTransfered(int month,int year)
        {
            List<Payroll> payrolls = new List<Payroll>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string bankName = string.Empty;
                var company = companyRepository.GetById(Guid.Empty);
                if (company != null)
                {
                    bankName = company.BankName;
                }

                var sql = "SELECT p.*,e.* "
                        + "FROM Payroll p INNER JOIN Employee e ON p.EmployeeId = e.ID "
                        + "WHERE p.IsTransfer=true AND p.BankName='" + bankName + "' "
                        + "AND p.MonthPeriod=" + month + " AND p.YearPeriod=" + year;

                //var sql = "SELECT p.* "
                // + "FROM Payroll p "
                // + "WHERE p.IsTransfer=true AND p.BankName='" + bankName + "' "
                // + "AND p.MonthPeriod=" + month + " AND p.YearPeriod=" + year;

                payrolls = em.ExecuteList<Payroll>(sql, new PayrollMapper());
            }

            return payrolls;
        } 


        public List<Payroll> GetTop(int top,int month, int year)
        {
            List<Payroll> payrolls = new List<Payroll>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT TOP " + top.ToString() + " p.*, e.* "
                        + "FROM Payroll p INNER JOIN Employee e ON p.EmployeeId = e.ID "
                        + "WHERE p.MonthPeriod=" + month + " AND p.YearPeriod=" + year + " "
                        + "ORDER BY p.PayrollDate DESC, e.EmployeeCode DESC";

                //var sql = "SELECT TOP " + top.ToString() + " p.* "
                //       + "FROM Payroll p "
                //       + "WHERE p.MonthPeriod=" + month + " AND p.YearPeriod=" + year + " "
                //       + "ORDER BY p.PayrollDate DESC, p.EmployeeCode DESC";

                payrolls = em.ExecuteList<Payroll>(sql, new PayrollMapper());
            }

            return payrolls;
        }

        

        public List<Payroll> Search(string value)
        {
            List<Payroll> payrolls = new List<Payroll>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT p.*, e.* "
                        + "FROM Payroll p INNER JOIN Employee e ON p.EmployeeId = e.ID "
                        + "WHERE "
                        + "(e.EmployeeCode like '%" + value + "%' "
                        + "OR e.EmployeeName like '%" + value + "%') "
                        + "ORDER BY p.PayrollDate DESC, e.EmployeeCode DESC";

                //var sql = "SELECT p.* "
                //       + "FROM Payroll p "
                //       + "WHERE "
                //       + "(p.EmployeeCode like '%" + value + "%' "
                //       + "OR p.EmployeeName like '%" + value + "%') "
                //       + "ORDER BY p.PayrollDate DESC, p.EmployeeCode DESC";

                payrolls = em.ExecuteList<Payroll>(sql, new PayrollMapper());
            }

            return payrolls;
        }


        public List<Payroll> Search(string value, int month, int year)
        {
            List<Payroll> payrolls = new List<Payroll>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
               var sql = "SELECT p.*, e.* "
                        + "FROM Payroll p INNER JOIN Employee e ON p.EmployeeId = e.ID "
                        + "WHERE "
                        + "(e.EmployeeCode like '%" + value + "%' "
                        + "OR e.EmployeeName like '%" + value + "%') "
                        + "AND p.MonthPeriod=" + month + " AND p.YearPeriod=" + year + " "
                        + "ORDER BY p.PayrollDate DESC, p.EmployeeCode DESC";

                payrolls = em.ExecuteList<Payroll>(sql, new PayrollMapper());
            }

            return payrolls;
        }

        public void Save(Payroll payroll)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    string[] fields = { "ID", "MonthPeriod", "YearPeriod", "PayrollDate", 
                                          "EmployeeId", 
                                          "Branch", "Department", "Grade", 
                                          "GradeLevel", "Occupation", 
                                          "IsTransfer", "BankName", "AccountNumber", 
                                          "WorkDay", "OnLeaveDay", "OffDay", "TotalDay",
                                          "IsPrincipal", "Principal", "PrincipalMainSalary", 
                                          "PrincipalLunch", "PrincipalTransport",
                                          "Status", "PaymentType",
                                          "MainSalary", "MainSalaryValue", 
                                          "OccupationAllowancePerMonth", "FixedAllowancePerMonth", 
                                          "HealthAllowancePerMonth", "CommunicationAllowancePerMonth", "SupervisionAllowancePerMonth",
                                          "OtherAllowance", "TotalFixedAllowance",
                                          "IsFuelAllowance", "FuelAllowance", "FuelValue", "FuelDay", "TotalFuel",
                                          "VehicleAllowance", "VehicleValue", "VehicleDay", "TotalVehicle",
                                          "LunchAllowance", "LunchValue", "LunchDay", "TotalLunch",
                                          "TransportationAllowance", "TransportationValue", "TransportationDay", "TotalTransportation",
                                          "IsIncentive", "Incentive", 
                                          "IsOverTime", "OverTime",
                                          "TotalNonFixedAllowance", 
                                          "IsInsurance", "InsuranceEmployeePercentage", "InsuranceEmployeeAmount", 
                                          "PersonalDebt", 
                                          "IsTax", "TaxAmount",
                                          "OtherFee", "TotalFee",
                                          "GrandTotal", "AmountInWords", 
                                          "InsuranceCompanyPercentage", "InsuranceCompanyAmount",
                                          "IsPaid",
                                          "CreatedDate","CreatedBy","ModifiedDate","ModifiedBy"};

                    object[] values = { Guid.NewGuid(), payroll.MonthPeriod, payroll.YearPeriod, payroll.PayrollDate.ToShortDateString(), 
                                          payroll.EmployeeId, 
                                          payroll.Branch, payroll.Department, payroll.Grade, 
                                          payroll.GradeLevel, payroll.Occupation, 
                                          payroll.IsTransfer==true?1:0, payroll.BankName, payroll.AccountNumber,
                                          payroll.WorkDay, payroll.OnLeaveDay, payroll.OffDay, payroll.TotalDay,
                                          payroll.IsPrincipal==true?1:0,payroll.Principal, payroll.PrincipalMainSalary,
                                          payroll.PrincipalLunch, payroll.PrincipalTransport,
                                          payroll.Status, payroll.PaymentType,
                                          payroll.MainSalary, payroll.MainSalaryValue,
                                          payroll.OccupationAllowancePerMonth, payroll.FixedAllowancePerMonth,
                                          payroll.HealthAllowancePerMonth, payroll.CommunicationAllowancePerMonth, payroll.SupervisionAllowancePerMonth,
                                          payroll.OtherAllowance, payroll.TotalFixedAllowance,
                                          payroll.IsFuelAllowance==true?1:0, payroll.FuelAllowance, payroll.FuelValue, payroll.FuelDay, payroll.TotalFuel,
                                          payroll.VehicleAllowance, payroll.VehicleValue, payroll.VehicleDay, payroll.TotalVehicle,
                                          payroll.LunchAllowance, payroll.LunchValue,  payroll.LunchDay, payroll.TotalLunch,
                                          payroll.TransportationAllowance, payroll.TransportationValue, payroll.TransportationDay, payroll.TotalTransportation,
                                          payroll.IsIncentive==true?1:0, payroll.Incentive, 
                                          payroll.IsOverTime==true?1:0,payroll.OverTime,
                                          payroll.TotalNonFixedAllowance,
                                          payroll.IsInsurance==true?1:0,payroll.InsuranceEmployeePercentage, payroll.InsuranceEmployeeAmount, 
                                          payroll.PersonalDebt, 
                                          payroll.IsTax==true?1:0,payroll.TaxAmount,
                                          payroll.OtherFee, payroll.TotalFee,
                                          payroll.GrandTotal, payroll.AmountInWords, 
                                          payroll.InsuranceCompanyPercentage, payroll.InsuranceCompanyAmount,
                                          payroll.IsPaid==true?1:0,
                                          DateTime.Now.ToShortDateString(),Store.ActiveUser, DateTime.Now.ToShortDateString(),Store.ActiveUser};

                    
                    Query q = new Query().Select(fields).From(tableName).Insert(values);

                    em.ExecuteNonQuery(q.ToSql());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public void Update(Payroll payroll)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {

                    string[] fields = { "MonthPeriod", "YearPeriod", "PayrollDate", 
                                          "EmployeeId", 
                                          "Branch", "Department", "Grade", 
                                          "GradeLevel", "Occupation", 
                                          "IsTransfer", "BankName", "AccountNumber", 
                                          "WorkDay", "OnLeaveDay", "OffDay", "TotalDay",
                                          "IsPrincipal", "Principal", "PrincipalMainSalary", 
                                          "PrincipalLunch", "PrincipalTransport",
                                          "Status", "PaymentType",
                                          "MainSalary", "MainSalaryValue", 
                                          "OccupationAllowancePerMonth", "FixedAllowancePerMonth", 
                                          "HealthAllowancePerMonth", "CommunicationAllowancePerMonth", "SupervisionAllowancePerMonth",
                                          "OtherAllowance", "TotalFixedAllowance",
                                          "IsFuelAllowance", "FuelAllowance", "FuelValue", "FuelDay", "TotalFuel",
                                          "VehicleAllowance", "VehicleValue", "VehicleDay", "TotalVehicle",
                                          "LunchAllowance", "LunchValue", "LunchDay", "TotalLunch",
                                          "TransportationAllowance", "TransportationValue", "TransportationDay", "TotalTransportation",
                                          "IsIncentive", "Incentive", 
                                          "IsOverTime", "OverTime",
                                          "TotalNonFixedAllowance", 
                                          "IsInsurance", "InsuranceEmployeePercentage", "InsuranceEmployeeAmount", 
                                          "PersonalDebt", 
                                          "IsTax", "TaxAmount",
                                          "OtherFee", "TotalFee",
                                          "GrandTotal", "AmountInWords", 
                                          "InsuranceCompanyPercentage", "InsuranceCompanyAmount",
                                          "IsPaid",
                                          "ModifiedDate","ModifiedBy"};

                    object[] values = { payroll.MonthPeriod, payroll.YearPeriod, payroll.PayrollDate.ToShortDateString(), 
                                          payroll.EmployeeId, 
                                          payroll.Branch, payroll.Department, payroll.Grade, 
                                          payroll.GradeLevel, payroll.Occupation, 
                                          payroll.IsTransfer==true?1:0, payroll.BankName, payroll.AccountNumber,
                                          payroll.WorkDay, payroll.OnLeaveDay, payroll.OffDay, payroll.TotalDay,
                                          payroll.IsPrincipal==true?1:0,payroll.Principal, payroll.PrincipalMainSalary,
                                          payroll.PrincipalLunch, payroll.PrincipalTransport,
                                          payroll.Status, payroll.PaymentType,
                                          payroll.MainSalary, payroll.MainSalaryValue,
                                          payroll.OccupationAllowancePerMonth, payroll.FixedAllowancePerMonth,
                                          payroll.HealthAllowancePerMonth, payroll.CommunicationAllowancePerMonth, payroll.SupervisionAllowancePerMonth,
                                          payroll.OtherAllowance, payroll.TotalFixedAllowance,
                                          payroll.IsFuelAllowance==true?1:0, payroll.FuelAllowance, payroll.FuelValue, payroll.FuelDay, payroll.TotalFuel,
                                          payroll.VehicleAllowance, payroll.VehicleValue, payroll.VehicleDay, payroll.TotalVehicle,
                                          payroll.LunchAllowance, payroll.LunchValue,  payroll.LunchDay, payroll.TotalLunch,
                                          payroll.TransportationAllowance, payroll.TransportationValue, payroll.TransportationDay, payroll.TotalTransportation,
                                          payroll.IsIncentive==true?1:0, payroll.Incentive, 
                                          payroll.IsOverTime==true?1:0,payroll.OverTime,
                                          payroll.TotalNonFixedAllowance,
                                          payroll.IsInsurance==true?1:0,payroll.InsuranceEmployeePercentage, payroll.InsuranceEmployeeAmount, 
                                          payroll.PersonalDebt, 
                                          payroll.IsTax==true?1:0,payroll.TaxAmount,
                                          payroll.OtherFee, payroll.TotalFee,
                                          payroll.GrandTotal, payroll.AmountInWords, 
                                          payroll.InsuranceCompanyPercentage, payroll.InsuranceCompanyAmount,
                                          payroll.IsPaid==true?1:0,
                                          DateTime.Now.ToShortDateString(),Store.ActiveUser};

                    

                    Query q = new Query().Select(fields).From(tableName).Update(values)
                        .Where("ID").Equal("{" + payroll.ID + "}");

                    em.ExecuteNonQuery(q.ToSql());

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void UpdateValue(Payroll payroll)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {

                    string[] fields = { "OtherAllowance", "TotalFixedAllowance",
                                          "FuelDay", "TotalFuel",
                                          "VehicleDay", "TotalVehicle",
                                          "LunchDay", "TotalLunch",
                                          "TransportationDay", "TotalTransportation",
                                          "TotalNonFixedAllowance", 
                                          "PersonalDebt", 
                                          "OtherFee", "TotalFee",
                                          "GrandTotal", "AmountInWords", 
                                          "ModifiedDate","ModifiedBy"};

                    object[] values = { payroll.OtherAllowance, payroll.TotalFixedAllowance,
                                          payroll.FuelDay, payroll.TotalFuel,
                                          payroll.VehicleDay, payroll.TotalVehicle,
                                          payroll.LunchDay, payroll.TotalLunch,
                                          payroll.TransportationDay, payroll.TotalTransportation,
                                          payroll.TotalNonFixedAllowance,
                                          payroll.PersonalDebt, 
                                          payroll.OtherFee, payroll.TotalFee,
                                          payroll.GrandTotal, payroll.AmountInWords, 
                                          DateTime.Now.ToShortDateString(),Store.ActiveUser};

                    Query q = new Query().Select(fields).From(tableName).Update(values)
                        .Where("ID").Equal("{" + payroll.ID + "}");

                    em.ExecuteNonQuery(q.ToSql());

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Delete(Guid id)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    Query q = new Query().From(tableName).Delete().Where("ID").Equal("{" + id + "}");
                    em.ExecuteNonQuery(q.ToSql());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


       
        public Payroll GetByEmployeeCode(string employeeCode, int month, int year)
        {
            Payroll payroll = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT p.*, "
                        + "e.EmployeeCode, e.EmployeeName, "
                        + "e.IsTransfer, e.BankName, e.AccountNumber, "
                        + "e.IsInsurance, e.IsTax, e.IsPrincipal, "
                        + "e.IsFuelAllowance "
                        + "FROM Payroll p INNER JOIN Employee e ON p.EmployeeId = e.ID "
                        + "WHERE "
                        + "e.EmployeeCode like '%" + employeeCode + "%' "
                        + "AND p.MonthPeriod=" + month + " AND p.YearPeriod=" + year + " "
                        + "ORDER BY p.PayrollDate DESC, e.EmployeeCode DESC";

                //var sql = "SELECT p.* "
                //      + "FROM Payroll p "
                //      + "WHERE "
                //      + "p.EmployeeCode like '%" + employeeCode + "%' "
                //      + "AND p.MonthPeriod=" + month + " AND p.YearPeriod=" + year + " "
                //      + "ORDER BY p.PayrollDate DESC, p.EmployeeCode DESC";

                payroll = em.ExecuteObject<Payroll>(sql, new PayrollMapper());
            }

            return payroll;

            
        }



        public void CalculatePayroll(DateTime effectiveDate, bool isIncentive, bool isOverTime, bool isEmployeeDebt, int month, int year)
        {

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                DateTime payrollDate = effectiveDate;

                //company
                int mainSalaryDivider = 0;

                //employee
                DateTime startDate;
                int workingDays = 0;
                string employeeCode = "";
                string oldEmployeeCode = "";
                string employeeName = "";
                bool gender = true;
                string branch = "";
                string department = "";
                string grade = "";
                int gradeLevel = 0;
                string occupation = "";
                string status = "";
                string paymentType = "";
                bool isFuelAllowance = false;

                int workDay = 0;
                int onLeaveDay = 0;
                int offDay = 0;
                int totalDay = 0;

                bool isInsurance = false;
                bool isFamilyInsurance = false;
                bool isTax = false;
                bool isTransfer = false;
                string bankName = "";
                string accountNumber = "";

                bool isPrincipal = false;
                Guid principalId = new Guid();
                string principal = "";
                decimal principalMainSalary = 0;
                decimal principalDailySalary = 0;
                decimal principalLunch = 0;
                decimal principalTransport = 0;

                decimal dailySalary = 0;
                decimal mainSalary = 0;
                decimal mainSalaryValue = 0;
                decimal occupationAllowance = 0;
                decimal fixedAllowance = 0;
                decimal healthAllowance = 0;
                decimal communicationAllowance = 0;
                decimal supervisionAllowance = 0;
                decimal otherAllowance=0;
                decimal totalFixed = 0;

                decimal fuelAllowance = 0;
                decimal fuelValue = 0;
                int fuelDay = 0;
                decimal totalFuel = 0;

                decimal vehicleAllowance = 0;
                decimal vehicleValue = 0;
                int vehicleDay = 0;
                decimal totalVehicle = 0;

                decimal lunchAllowance = 0;
                decimal lunchValue = 0;
                int lunchDay = 0;
                decimal totalLunch = 0;

                decimal transportationAllowance = 0;
                decimal transportationValue = 0;
                int transportDay = 0;
                decimal totalTransport = 0;

                decimal incentive = 0;

                decimal overTime = 0;

                decimal totalNonFixed = 0;

                double insuranceEmployeePercentage = 0;
                decimal insuranceEmployeeAmount = 0;
                double insuranceEmployeeFemalePercentage = 0;
                decimal insuranceEmployeeFemaleAmount = 0;
                double insuranceCompanyPercentage = 0;
                decimal insuranceCompanyAmount = 0;

                decimal personalDebt = 0;
                decimal tax = 0;
                decimal otherFee = 0;
                decimal totalFee = 0;

                decimal grandTotal = 0;
                string amountInWord = "";
                bool isProcess = false;

                //company
                Company company = companyRepository.GetAll();
                if (company != null)
                {
                    mainSalaryDivider = company.MainSalaryDivider;

                }



                //work calendar
                WorkCalendar workCalendar = workCalendarRepository.GetByMonthYear(Store.ActiveMonth, Store.ActiveYear);
                if (workCalendar != null)
                {
                    workingDays = workCalendar.WorkDay;


                }


                //EMPLOYEE
                List<Employee> employees = employeeRepository.GetActiveEmployee();

                foreach (var e in employees)
                {
                    startDate = e.StartDate;
                    gender = e.Gender;
                    employeeCode = e.EmployeeCode;
                    oldEmployeeCode = e.OldEmployeeCode;
                    employeeName = e.EmployeeName;


                    //TimeSpan days = effectiveDate.Subtract(startDate);
                    //if (days.TotalDays > 0)
                    //{
                    //    workingDays = Convert.ToInt32(days.TotalDays);
                    //}
                    //else
                    //{
                    //    workingDays = 0;
                    //}


                    workDay = 0;
                    onLeaveDay = 0;
                    offDay = 0;
                    totalDay = 0;

                    principal = "";
                    principalMainSalary = 0;
                    principalDailySalary = 0;
                    principalLunch = 0;
                    principalTransport = 0;

                    dailySalary = 0;
                    mainSalary = 0;
                    mainSalaryValue = 0;
                    occupationAllowance = 0;
                    fixedAllowance = 0;
                    healthAllowance = 0;
                    communicationAllowance = 0;
                    supervisionAllowance = 0;
                    otherAllowance = 0;
                    totalFixed = 0;
                    fuelAllowance = 0;
                    fuelValue = 0;
                    fuelDay = 0;
                    totalFuel = 0;
                    vehicleAllowance = 0;
                    vehicleValue = 0;
                    vehicleDay = 0;
                    totalVehicle = 0;
                    lunchAllowance = 0;
                    lunchValue = 0;
                    lunchDay = 0;
                    totalLunch = 0;
                    transportationAllowance = 0;
                    transportationValue = 0;
                    transportDay = 0;
                    totalTransport = 0;
                    incentive = 0;
                    overTime = 0;
                    totalNonFixed = 0;
                    insuranceEmployeePercentage = 0;
                    insuranceEmployeeAmount = 0;
                    insuranceEmployeeFemalePercentage = 0;
                    insuranceEmployeeFemaleAmount = 0;
                    insuranceCompanyPercentage = 0;
                    insuranceCompanyAmount = 0;
                    personalDebt = 0;
                    tax = 0;
                    otherFee = 0;
                    totalFee = 0;
                    grandTotal = 0;

                    isFuelAllowance = e.IsFuelAllowance;
                    isPrincipal = e.IsPrincipal;
                    isInsurance = e.IsInsurance;
                    isTax = e.IsTax;
                    isTransfer = e.IsTransfer;
                    
                    bankName = e.BankName;
                    accountNumber = e.AccountNumber;


                    //AMBIL BRANCH & DEPT
                    var dept = employeeDepartmentRepository.GetCurrentDepartment(e.ID, Store.ActiveMonth, Store.ActiveYear);
                    if (dept != null)
                    {
                        department = dept.DepartmentName;
                        branch = dept.BranchName;
                    }
                    else
                    {
                        var previousDept = employeeDepartmentRepository.GetPreviousDepartment(e.ID, Store.ActiveMonth, Store.ActiveYear);
                        if (previousDept != null)
                        {
                            department = previousDept.DepartmentName;
                            branch = previousDept.BranchName;                            
                        }
                    }

                    //AMBIL GRADE
                    var grades = employeeGradeRepository.GetCurrentGrade(e.ID, Store.ActiveMonth, Store.ActiveYear);
                    if (grades != null)
                    {
                        grade = grades.GradeName;
                        gradeLevel = grades.GradeLevel;
                    }
                    else
                    {
                        var previousGrade = employeeGradeRepository.GetPreviousGrade(e.ID, Store.ActiveMonth, Store.ActiveYear);
                        if (previousGrade != null)
                        {
                            department = previousGrade.GradeName;
                            gradeLevel = previousGrade.GradeLevel;
                        }
                    }


                    //AMBIL OCCUPATION
                    var occupations = employeeOccupationRepository.GetCurrentOccupation(e.ID, Store.ActiveMonth, Store.ActiveYear);
                    if (occupations != null)
                    {
                        occupation  = occupations.OccupationName;                        
                    }
                    else
                    {
                        var previousOccupation = employeeOccupationRepository.GetPreviousOccupation(e.ID, Store.ActiveMonth, Store.ActiveYear);
                        if (previousOccupation != null)
                        {
                            occupation = previousOccupation.OccupationName;
                        }
                    }

                    //AMBIL STATUS
                    var statusEmployee = employeeStatusRepository.GetCurrentStatus(e.ID, Store.ActiveMonth, Store.ActiveYear);
                    if (statusEmployee != null)
                    {
                        status = statusEmployee.Status;
                        paymentType = statusEmployee.PaymentType;
                    }
                    else
                    {
                        var previousStatus = employeeStatusRepository.GetPreviousStatus(e.ID, Store.ActiveMonth, Store.ActiveYear);
                        if (previousStatus != null)
                        {
                            status = previousStatus.Status;
                            paymentType = previousStatus.PaymentType;
                        }
                    }


                    //AMBIL PRINCIPAL
                    int countPrincipal = employeePrincipalRepository.CountActivePrincipal(e.ID, Store.ActiveMonth, Store.ActiveYear);
                    if (isPrincipal == true)
                    {
                        //jika principal lebih dari 2 maka ditanggung perusahaan
                        if (countPrincipal == 1)
                        {

                            isPrincipal = true;

                            //ambil id & nama principal
                            var currentPrincipal = employeePrincipalRepository.GetCurrentPrincipal(e.ID, Store.ActiveMonth, Store.ActiveYear);
                            if (currentPrincipal != null)
                            {
                                principalId = currentPrincipal.PrincipalId;
                                principal = currentPrincipal.PrincipalName;
                            }
                            else
                            {
                                var previousPrincipal = employeePrincipalRepository.GetPreviousPrincipal(e.ID, Store.ActiveMonth, Store.ActiveYear);
                                if (previousPrincipal != null)
                                {
                                    principalId = previousPrincipal.PrincipalId;
                                    principal = previousPrincipal.PrincipalName;
                                }
                            }

                            //ambil nilai principal
                            var currentPrincipalValues = principalItemRepository.GetCurrentPrincipal(principalId, Store.ActiveMonth, Store.ActiveYear);
                            if (currentPrincipalValues != null)
                            {
                                principalMainSalary = currentPrincipalValues.MainSalary;
                                principalDailySalary = principalMainSalary / mainSalaryDivider;
                                principalLunch = currentPrincipalValues.LunchAllowance;
                                principalTransport = currentPrincipalValues.TransportationAllowance;
                            }
                            else
                            {
                                var previousPrincipalValues = principalItemRepository.GetPreviousPrincipal(principalId, Store.ActiveMonth, Store.ActiveYear);
                                if (previousPrincipalValues != null)
                                {
                                    principalMainSalary = previousPrincipalValues.MainSalary;
                                    principalDailySalary = principalMainSalary / mainSalaryDivider;
                                    principalLunch = previousPrincipalValues.LunchAllowance;
                                    principalTransport = previousPrincipalValues.TransportationAllowance;
                                }
                            }


                        }
                        else
                        {
                            isPrincipal = false;
                        }
                    }
                    
                    //AMBIL NILAI-NILAI GAJI
                    var salary = employeeSalaryRepository.GetCurrentSalary(e.ID, Store.ActiveMonth, Store.ActiveYear);
                    if (salary != null)
                    {
                        mainSalary = salary.MainSalary;
                        dailySalary = mainSalary / mainSalaryDivider;
                        occupationAllowance = salary.OccupationAllowancePerMonth;
                        fixedAllowance = salary.FixedAllowancePerMonth;
                        healthAllowance = salary.HealthAllowancePerMonth;
                        communicationAllowance = salary.CommunicationAllowancePerMonth;
                        supervisionAllowance = salary.SupervisionAllowancePerMonth;
                        otherAllowance = salary.OtherAllowance;
                        fuelAllowance = salary.FuelAllowancePerDays;
                        vehicleAllowance = salary.VehicleAllowancePerDays;
                        lunchAllowance = salary.LunchAllowancePerDays;
                        transportationAllowance = salary.TransportationAllowancePerDays;
                        personalDebt = salary.PersonalDebt;
                        otherFee = salary.OtherFee;

                    }
                    else
                    {
                        var previousSalary = employeeSalaryRepository.GetPreviousSalary(e.ID, Store.ActiveMonth, Store.ActiveYear);
                        if (previousSalary != null)
                        {
                            mainSalary = previousSalary.MainSalary;
                            dailySalary = mainSalary / mainSalaryDivider;
                        
                            occupationAllowance = previousSalary.OccupationAllowancePerMonth;
                            fixedAllowance = previousSalary.FixedAllowancePerMonth;
                            healthAllowance = previousSalary.HealthAllowancePerMonth;
                            communicationAllowance = previousSalary.CommunicationAllowancePerMonth;
                            supervisionAllowance = previousSalary.SupervisionAllowancePerMonth;
                            otherAllowance = previousSalary.OtherAllowance;
                            fuelAllowance = previousSalary.FuelAllowancePerDays;
                            vehicleAllowance = previousSalary.VehicleAllowancePerDays;
                            lunchAllowance = previousSalary.LunchAllowancePerDays;
                            transportationAllowance = previousSalary.TransportationAllowancePerDays;
                            personalDebt = previousSalary.PersonalDebt;
                            otherFee = previousSalary.OtherFee;
                        }
                    }


                     //AMBIL ABSEN
                    var absence = absenceRepository.GetByEmployeeId(e.ID, Store.ActiveMonth, Store.ActiveYear);
                    if (absence != null)
                    {
                        workDay = absence.WorkDay;
                        onLeaveDay = absence.OnLeaveDay;
                        offDay = absence.OffDay;
                        totalDay = absence.Total;

                        fuelDay = absence.WorkDay;
                        vehicleDay = absence.WorkDay;
                        lunchDay = absence.WorkDay + absence.OnLeaveDay;
                        transportDay = absence.WorkDay + absence.OnLeaveDay;
                    }


                    //principal lebih dari satu ditanggung perusahaan
                    if (fuelDay >= workingDays)
                    {
                        if (countPrincipal != 1)
                        {
                            mainSalaryValue = mainSalary;
                            lunchValue = lunchAllowance;
                            transportationValue = transportationAllowance;

                        }
                        else
                        {
                            mainSalaryValue = principalMainSalary;
                            lunchValue = principalLunch;
                            transportationValue = principalTransport;
                        }
                    }
                    else
                    {
                        if (countPrincipal != 1)
                        {
                            mainSalaryValue = dailySalary * fuelDay;
                            lunchValue = lunchAllowance;
                            transportationValue = transportationAllowance;
                        }
                        else
                        {
                            mainSalaryValue = principalDailySalary * fuelDay;
                            lunchValue = principalLunch;
                            transportationValue = principalTransport;
                        }
                    }

                    //TOTAL FIXED
                    totalFixed = mainSalaryValue + occupationAllowance + fixedAllowance + healthAllowance + communicationAllowance + supervisionAllowance + otherAllowance;
                   

                    //NON FIXED
                    fuelValue = fuelAllowance;
                    vehicleValue = vehicleAllowance;
                    if (isFuelAllowance == false)
                    {
                        fuelValue = 0;
                        vehicleValue = 0;
                    }
                    totalFuel = fuelValue * fuelDay;
                    totalVehicle = vehicleValue * vehicleDay;
                    totalLunch = lunchValue * lunchDay;
                    totalTransport = transportationValue * transportDay;

                  


                    //AMBIL LEMBUR
                    if (isOverTime == true)
                    {
                        var overTimes = overTimeRepository.SumByEmployeeId(e.ID, Store.ActiveMonth, Store.ActiveYear);
                        if (overTimes != null)
                        {
                            overTime = overTimes.amount;
                        }
                    }


                    //AMBIL INCENTIVE
                    if (isIncentive == true)
                    {
                        var incentives = incentiveRepository.SumByEmployeeId(e.ID, Store.ActiveMonth, Store.ActiveYear);
                        if (incentives != null)
                        {
                            incentive = incentives.amount;
                        }
                    }
                    

                    //TOTAL NON FIXED
                    totalNonFixed = totalFuel + totalVehicle + totalLunch + totalTransport + overTime + incentive;


                    //CEK KELUARGA IKUT INSURANCE                    
                    isFamilyInsurance = employeeFamilyRepository.IsFamilyInsurance(e.ID);
                    

                    //POTONGAN
                    //AMBIL INSURANCE                    
                    var jamsostek = employeeInsuranceRepository.GetCurrentValue(e.ID, "Jamsostek", Store.ActiveMonth, Store.ActiveYear);
                    if (jamsostek != null)
                    {
                        insuranceEmployeePercentage = jamsostek.ByEmployee;
                        insuranceEmployeeFemalePercentage = jamsostek.ByEmployeeFemale;
                        insuranceCompanyPercentage = jamsostek.ByCompany;
                    }
                    else
                    {
                        var previousJamsostek = employeeInsuranceRepository.GetPreviousValue(e.ID, "Jamsostek", Store.ActiveMonth, Store.ActiveYear);
                        if (previousJamsostek != null)
                        {
                            insuranceEmployeePercentage = previousJamsostek.ByEmployee;
                            insuranceEmployeeFemalePercentage = previousJamsostek.ByEmployeeFemale;
                            insuranceCompanyPercentage = previousJamsostek.ByCompany;
                        }
                    }

                    insuranceEmployeeAmount = Convert.ToDecimal(insuranceEmployeePercentage / 100) * mainSalary;
                    insuranceEmployeeFemaleAmount = Convert.ToDecimal(insuranceEmployeeFemalePercentage / 100) * mainSalary;
                    insuranceCompanyAmount = Convert.ToDecimal(insuranceCompanyPercentage / 100) * mainSalary;

                    //jika perempuan & keluarganya diasuransikan maka pakai hitungan female
                    if (isFamilyInsurance == true && gender == false)
                    {
                        insuranceEmployeePercentage = insuranceEmployeeFemalePercentage;
                        insuranceEmployeeAmount = insuranceEmployeeFemaleAmount;
                    }


                    //AMBIL PIUTANG
                    if (isEmployeeDebt == true)
                    {
                        var debtItems = employeeDebtItemRepository.SumUnPaidByEmployeeId(e.ID, Store.ActiveMonth, Store.ActiveYear);
                        if (debtItems != null)
                        {
                            personalDebt = debtItems.Amount;
                        }
                    }

                    totalFee = insuranceEmployeeAmount + personalDebt;

                    grandTotal = (totalFixed + totalNonFixed) - totalFee;

                    if (grandTotal > 0)
                    {
                        string amountInWords = Store.GetAmounInWords(Convert.ToInt32(grandTotal));
                        string firstLetter = amountInWords.Substring(0, 2).Trim().ToUpper();
                        string theRest = amountInWords.Substring(2, amountInWords.Length - 2);
                        amountInWord = firstLetter + theRest + " rupiah";
                    }
                    else
                    {
                        amountInWord = "Nol rupiah";

                    }

                    
                    Payroll oldPayroll = GetByEmployeeId(e.ID, Store.ActiveMonth, Store.ActiveYear);
                    Payroll payroll = new Payroll();

                    payroll.MonthPeriod = Store.ActiveMonth;
                    payroll.YearPeriod = Store.ActiveYear;
                    payroll.PayrollDate = payrollDate;
                    payroll.EmployeeId = e.ID;
                    payroll.Branch = branch;
                    payroll.Department = department;
                    payroll.Grade = grade;
                    payroll.GradeLevel = gradeLevel;
                    payroll.Occupation = occupation;
                    payroll.Status = status;
                    payroll.PaymentType = paymentType;
                    payroll.WorkDay = workDay;
                    payroll.OnLeaveDay = onLeaveDay;
                    payroll.OffDay = offDay;
                    payroll.TotalDay = totalDay;

                    payroll.IsTransfer = isTransfer;
                    payroll.BankName = bankName;
                    payroll.AccountNumber = accountNumber;

                    payroll.IsPrincipal = isPrincipal;
                    payroll.Principal = principal;
                    payroll.PrincipalTransport = principalTransport;
                    payroll.PrincipalLunch = principalLunch;
                    payroll.PrincipalMainSalary = principalMainSalary;

                    payroll.MainSalary = mainSalary;
                    payroll.MainSalaryValue = mainSalaryValue;
                    payroll.OccupationAllowancePerMonth = occupationAllowance;
                    payroll.FixedAllowancePerMonth = fixedAllowance;
                    payroll.HealthAllowancePerMonth = healthAllowance;
                    payroll.CommunicationAllowancePerMonth = communicationAllowance;
                    payroll.SupervisionAllowancePerMonth = supervisionAllowance;
                    payroll.OtherAllowance = otherAllowance; //*
                    payroll.TotalFixedAllowance = totalFixed;

                    payroll.IsFuelAllowance = isFuelAllowance;
                    payroll.FuelAllowance = fuelAllowance;
                    payroll.FuelValue = fuelValue;
                    payroll.FuelDay = fuelDay; //*
                    payroll.TotalFuel = totalFuel; //*
                    payroll.VehicleAllowance = vehicleAllowance;
                    payroll.VehicleValue = vehicleValue;
                    payroll.VehicleDay = vehicleDay; //*
                    payroll.TotalVehicle = totalVehicle; //*
                    payroll.LunchAllowance = lunchAllowance;
                    payroll.LunchValue = lunchValue; 
                    payroll.LunchDay = lunchDay; //*
                    payroll.TotalLunch = totalLunch; //*
                    payroll.TransportationAllowance = transportationAllowance;
                    payroll.TransportationValue = transportationValue;
                    payroll.TransportationDay = transportDay; //*
                    payroll.TotalTransportation = totalTransport; //*

                    payroll.IsIncentive = isIncentive;
                    payroll.Incentive = incentive;

                    payroll.IsOverTime = isOverTime;
                    payroll.OverTime = overTime;
                    payroll.TotalNonFixedAllowance = totalNonFixed;

                    payroll.IsInsurance = isInsurance;
                    payroll.InsuranceEmployeePercentage = insuranceEmployeePercentage;
                    payroll.InsuranceEmployeeAmount = insuranceEmployeeAmount;
                    payroll.InsuranceCompanyPercentage = insuranceCompanyPercentage;
                    payroll.InsuranceCompanyAmount = insuranceCompanyAmount;
                    
                    payroll.PersonalDebt = personalDebt;

                    payroll.IsTax = isTax;
                    payroll.TaxAmount = tax;
                    payroll.OtherFee = otherFee;
                    payroll.TotalFee = totalFee;
                    payroll.GrandTotal = grandTotal;
                    payroll.AmountInWords = amountInWord;
                    payroll.IsPaid = isProcess; 

                    if (oldPayroll == null)
                    {
                        Save(payroll);
                    }
                    else
                    {
                        payroll.ID = oldPayroll.ID;
                        payroll.IsTransfer = payroll.IsTransfer;
                        payroll.BankName = payroll.BankName;
                        payroll.AccountNumber = payroll.AccountNumber;
                        payroll.OtherAllowance = oldPayroll.OtherAllowance;
                        payroll.TotalFixedAllowance = payroll.MainSalary + payroll.OccupationAllowancePerMonth + payroll.FixedAllowancePerMonth + payroll.HealthAllowancePerMonth + payroll.CommunicationAllowancePerMonth + payroll.SupervisionAllowancePerMonth + payroll.OtherAllowance;
                        payroll.TotalNonFixedAllowance = payroll.TotalFuel + payroll.TotalVehicle + payroll.TotalLunch + payroll.TotalTransportation + payroll.OverTime + payroll.Incentive;
                        payroll.PersonalDebt = payroll.PersonalDebt;
                        payroll.OtherFee = oldPayroll.OtherFee;
                        payroll.TotalFee = payroll.InsuranceEmployeeAmount + payroll.PersonalDebt + payroll.TaxAmount + payroll.OtherFee;
                        payroll.GrandTotal = (payroll.TotalFixedAllowance + payroll.TotalNonFixedAllowance) - payroll.TotalFee;
                        if (payroll.GrandTotal > 0)
                        {
                            string amountInWords = Store.GetAmounInWords(Convert.ToInt32(payroll.GrandTotal));
                            string firstLetter = amountInWords.Substring(0, 2).Trim().ToUpper();
                            string theRest = amountInWords.Substring(2, amountInWords.Length - 2);
                            payroll.AmountInWords = firstLetter + theRest + " rupiah";
                        }
                        else
                        {
                            payroll.AmountInWords = "Nol rupiah";

                        }

                        Update(payroll);
                    }



                }


                
            }


        }




        public bool IsExisted(Guid employeeId, DateTime payrollDate)
        {
            bool isExisted = false;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From("Payroll")
                    .Where("EmployeeId").Equal("{" + employeeId + "}")
                    .And("PayrollDate=#" + payrollDate.ToShortDateString() + "#");
                
                using (var rdr = em.ExecuteReader(q.ToSql()))
                {
                    if (rdr.Read())
                    {
                        isExisted = true;
                    }
                }

            }

            return isExisted;

        }

        public void UpdateIsPaid(int month, int year, bool paid)
        {
            int status = 0;
            if (paid) status = 1;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string sql = "UPDATE " + tableName + " SET IsPaid = " + status + " WHERE MonthPeriod=" + month + " AND YearPeriod=" + year + " ";
                em.ExecuteNonQuery(sql);
            }

        }

        public void UpdateCurrentInfo(int month, int year, EmployeeCurrentInfo currentInfo)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    string[] columns = { "Branch", "Department",
                                       "Grade", "GradeLevel",
                                       "Occupation", "Status", "PaymentType",
                                       "BankName", "AccountNumber"};

                    object[] values = { currentInfo.BranchName, currentInfo.DepartmentName,
                                      currentInfo.GradeName, currentInfo.GradeLevel,
                                      currentInfo.OccupationName, currentInfo.EmployeeStatus, currentInfo.PaymentType,
                                      currentInfo.BankName, currentInfo.AccountNumber};

                    var q = new Query().Select(columns).From(tableName).Update(values)
                        .Where("EmployeeId").Equal("{" + currentInfo.EmployeeId + "}")
                        .And("MonthPeriod").GreaterEqualThan(month)
                        .And("YearPeriod").GreaterEqualThan(year)
                        .And("IsPaid = false"); ;

                    em.ExecuteNonQuery(q.ToSql());

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }








    }
}

