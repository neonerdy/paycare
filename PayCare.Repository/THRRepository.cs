
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
    public interface ITHRRepository
    {
        THR GetById(Guid id);
        THR GetLast();
        THR GetLast(int year);
        THR GetByEmployeeId(Guid employeeId, int year);
        List<THR> GetAll();
        List<THR> GetTransfered(int year);
        List<THR> GetAll(int year);
        List<THR> Search(string value);
        List<THR> Search(string value, int year);
        void Save(THR thr);
        void Update(THR thr);
        void UpdateValue(THR thr);
        void Delete(Guid id);
        THR GetByEmployee(string employeeCode, int year);
        bool IsExisted(Guid employeeId, int year);
        void CalculateTHR(DateTime effectiveDate, string holidays);
        bool IsPaid(string holiday, int year);
        void UpdateIsPaid(int year, bool paid);
        void UpdateCurrentInfo(int year, EmployeeCurrentInfo currentInfo);
        
    }

    public class THRRepository : ITHRRepository
    {
        private string tableName = "THR";
        private DataSource ds;
        private IEmployeeRepository employeeRepository;
        private ICompanyRepository companyRepository;
        private IEmployeeSalaryRepository employeeSalaryRepository;
        private IEmployeeDepartmentRepository employeeDepartmentRepository;
        private IEmployeeGradeRepository employeeGradeRepository;
        private IEmployeeOccupationRepository employeeOccupationRepository;
        private IEmployeeStatusRepository employeeStatusRepository;
      
        private int yearOfWork;
        private int monthOfWork;
        private int daysOfWork;
        
        public THRRepository(DataSource ds)
        {
            this.ds = ds;
        
            employeeRepository = EntityContainer.GetType<IEmployeeRepository>();
            employeeSalaryRepository = EntityContainer.GetType<IEmployeeSalaryRepository>();            
            employeeDepartmentRepository = EntityContainer.GetType<IEmployeeDepartmentRepository>();
            employeeGradeRepository = EntityContainer.GetType<IEmployeeGradeRepository>();
            employeeOccupationRepository = EntityContainer.GetType<IEmployeeOccupationRepository>();
            employeeStatusRepository = EntityContainer.GetType<IEmployeeStatusRepository>();
            companyRepository = EntityContainer.GetType<ICompanyRepository>();
            
        }


        public THR GetById(Guid id)
        {
            THR thr = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
              
                var sql = "SELECT t.*, "
                        + "e.* "
                        + "FROM THR t INNER JOIN Employee e ON t.EmployeeId = e.ID "
                        + "WHERE "
                        + "t.ID='{" + id + "}'";

                thr = em.ExecuteObject<THR>(sql, new THRMapper());
            }

            return thr;
        }


        public THR GetLast()
        {
            THR thr = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                 var sql = "SELECT TOP 1 t.*, "
                        + "e.* "
                        + "FROM THR t INNER JOIN Employee e ON t.EmployeeId = e.ID " 
                        + "ORDER BY e.EmployeeCode ASC";

                thr = em.ExecuteObject<THR>(sql, new THRMapper());
            }

            return thr;
        }

        public THR GetLast(int year)
        {
            THR thr = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                 var sql = "SELECT t.*, "
                        + "e.* "
                        + "FROM THR t INNER JOIN Employee e ON t.EmployeeId = e.ID "
                        + "WHERE t.YearPeriod=" + year + " "
                        + "ORDER BY t.HolidayType ASC, e.EmployeeCode ASC";

                thr = em.ExecuteObject<THR>(sql, new THRMapper());
            }

            return thr;
        }


        public THR GetByEmployeeId(Guid employeeId, int year)
        {
            THR thr = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                 var sql = "SELECT t.*, "
                        + "e.* "
                        + "FROM THR t INNER JOIN Employee e ON t.EmployeeId = e.ID "
                        + "WHERE "
                        + "t.EmployeeId='{" + employeeId + "}' "
                        + "AND t.YearPeriod=" + year + " "
                        + "ORDER BY t.HolidayType ASC, e.EmployeeCode ASC";

                thr = em.ExecuteObject<THR>(sql, new THRMapper());
            }

            return thr;
        }


        public List<THR> GetAll()
        {
            List<THR> thrs = new List<THR>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT t.*, "
                        + "e.* "
                        + "FROM THR t INNER JOIN Employee e ON t.EmployeeId = e.ID "
                        + "ORDER BY t.HolidayType ASC, e.EmployeeCode ASC";

                thrs = em.ExecuteList<THR>(sql, new THRMapper());
            }

            return thrs;
        }



        public List<THR> GetTransfered(int year)
        {
            List<THR> thrs = new List<THR>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string bankName = string.Empty;
                var company = companyRepository.GetById(Guid.Empty);
                
                if (company != null)
                {
                    bankName = company.BankName;
                }

                var sql = "SELECT t.*, "
                        + "e.* "
                        + "FROM THR t INNER JOIN Employee e ON t.EmployeeId = e.ID "
                        + "WHERE t.IsTransfer=true AND t.BankName='" + bankName + "' "
                        + "AND t.YearPeriod=" + year;
                    
                thrs = em.ExecuteList<THR>(sql, new THRMapper());
            }

            return thrs;
        }



        public List<THR> GetAll(int year)
        {
            List<THR> thrs = new List<THR>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                 var sql = "SELECT t.*, "
                        + "e.* "
                        + "FROM THR t INNER JOIN Employee e ON t.EmployeeId = e.ID "
                        + "WHERE t.YearPeriod=" + year + " "
                        + "ORDER BY t.HolidayType ASC, e.EmployeeCode ASC";

                thrs = em.ExecuteList<THR>(sql, new THRMapper());
            }

            return thrs;
        }

        

        public List<THR> Search(string value)
        {
            List<THR> thrs = new List<THR>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                 var sql = "SELECT t.*, "
                        + "e.* "
                        + "FROM THR t INNER JOIN Employee e ON t.EmployeeId = e.ID "
                        + "WHERE "
                        + "(e.EmployeeCode like '%" + value + "%' "
                        + "OR e.EmployeeName like '%" + value + "%') "
                        + "ORDER BY t.HolidayType ASC, e.EmployeeCode ASC";

                thrs = em.ExecuteList<THR>(sql, new THRMapper());
            }

            return thrs;
        }


        public List<THR> Search(string value, int year)
        {
            List<THR> thrs = new List<THR>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                 var sql = "SELECT t.*, "
                        + "e.* "
                        + "FROM THR t INNER JOIN Employee e ON t.EmployeeId = e.ID "
                        + "WHERE "
                        + "(e.EmployeeCode like '%" + value + "%' "
                        + "OR e.EmployeeName like '%" + value + "%') "
                        + "AND t.YearPeriod=" + year + " "
                        + "ORDER BY t.HolidayType ASC, e.EmployeeCode ASC";

                thrs = em.ExecuteList<THR>(sql, new THRMapper());
            }

            return thrs;
        }

        public void Save(THR thr)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    string[] fields = { "ID", "YearPeriod", "EmployeeId",
                                          "Branch", "Department", "Grade", 
                                          "GradeLevel", "Occupation", 
                                          "Status", "PaymentType",
                                          "IsTransfer", "BankName", "AccountNumber",                                           
                                          "HolidayType", "StartDate", "EffectiveDate",
                                          "YearOfWork", "MonthOfWork", "DayOfWork",
                                          "MainSalary", "Amount", "OtherAmount", "TotalAmount", 
                                          "AmountInWords", "IsPaid", 
                                          "CreatedDate","CreatedBy","ModifiedDate","ModifiedBy"};

                    object[] values = { Guid.NewGuid(), thr.YearPeriod, thr.EmployeeId, 
                                          thr.Branch, thr.Department, thr.Grade, 
                                          thr.GradeLevel, thr.Occupation,
                                          thr.Status, thr.PaymentType,
                                          thr.IsTransfer==true?1:0, thr.BankName, thr.AccountNumber,                                                                                                                              
                                          thr.HolidayType, thr.StartDate.ToShortDateString(), thr.EffectiveDate.ToShortDateString(),
                                          thr.YearOfWork, thr.MonthOfWork, thr.DayOfWork,
                                          thr.MainSalary, thr.Amount, thr.OtherAmount, thr.TotalAmount,
                                          thr.AmountInWords, thr.IsPaid==true?1:0,
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


        public void Update(THR thr)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {

                    string[] fields = { "YearPeriod", "EmployeeId",
                                          "Branch", "Department", "Grade", 
                                          "GradeLevel", "Occupation", 
                                          "Status", "PaymentType",
                                          "IsTransfer", "BankName", "AccountNumber",
                                          "HolidayType", "StartDate", "EffectiveDate",
                                          "YearOfWork", "MonthOfWork", "DayOfWork",
                                          "MainSalary", "Amount", "OtherAmount", "TotalAmount", 
                                          "AmountInWords", "IsPaid", 
                                          "CreatedDate","CreatedBy","ModifiedDate","ModifiedBy"};

                    object[] values = { thr.YearPeriod, thr.EmployeeId, 
                                          thr.Branch, thr.Department, thr.Grade, 
                                          thr.GradeLevel, thr.Occupation,
                                          thr.Status, thr.PaymentType,
                                          thr.IsTransfer==true?1:0, thr.BankName, thr.AccountNumber,                                                                                    
                                          thr.HolidayType, thr.StartDate.ToShortDateString(), thr.EffectiveDate.ToShortDateString(),
                                          thr.YearOfWork, thr.MonthOfWork, thr.DayOfWork,
                                          thr.MainSalary, thr.Amount, thr.OtherAmount, thr.TotalAmount,
                                          thr.AmountInWords, thr.IsPaid==true?1:0,
                                      DateTime.Now.ToShortDateString(),Store.ActiveUser, DateTime.Now.ToShortDateString(),Store.ActiveUser};

                    Query q = new Query().Select(fields).From(tableName).Update(values)
                        .Where("ID").Equal("{" + thr.ID + "}");

                    em.ExecuteNonQuery(q.ToSql());

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public void UpdateValue(THR thr)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {

                    string[] fields = { "OtherAmount", "TotalAmount",
                                          "AmountInWords", 
                                          "ModifiedDate","ModifiedBy"};

                    object[] values = { thr.OtherAmount, thr.TotalAmount,
                                          thr.AmountInWords, 
                                          DateTime.Now.ToShortDateString(),Store.ActiveUser};

                    Query q = new Query().Select(fields).From(tableName).Update(values)
                        .Where("ID").Equal("{" + thr.ID + "}");

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


       
        public THR GetByEmployee(string employeeCode, int year)
        {
            THR thr = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                 var sql = "SELECT t.*, "
                        + "e.* "
                        + "FROM THR t INNER JOIN Employee e ON t.EmployeeId = e.ID "
                        + "WHERE "
                        + "e.EmployeeCode like '%" + employeeCode + "%' "
                        + "AND t.YearPeriod=" + year + " "
                        + "ORDER BY t.HolidayType ASC, e.EmployeeCode ASC";

                thr = em.ExecuteObject<THR>(sql, new THRMapper());
            }

            return thr;

            
        }



        
        public bool IsExisted(Guid employeeId, int year)
        {
            bool isExisted = false;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From("THR")
                    .Where("EmployeeId").Equal("{" + employeeId + "}")
                    .And("YearPeriod").Equal(year);
                
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



        public void CalculateYearAndMonth(DateTime startDate,DateTime endDate)
        {
            int inYears = 0;
            int inMonths = 0;
            int inDays = 0;

            inDays = endDate.Day - startDate.Day;
            inMonths = endDate.Month - startDate.Month;
            inYears = endDate.Year - startDate.Year;

            if (inDays < 0)
            {
                inDays += DateTime.DaysInMonth(endDate.Year, endDate.Month);
                inMonths = inMonths--;

                if (inMonths < 0)
                {
                    inMonths += 12;
                    inYears--;
                }
            }
            if (inMonths < 0)
            {
                inMonths += 12;
                inYears--;
            }


            yearOfWork = inYears;
            monthOfWork = inMonths;
            daysOfWork = inDays;

        }



        public void CalculateTHR(DateTime effectiveDate, string holidays)
        {

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                
                int mainSalaryDivider = 0;

                //employee
                DateTime startDate;
           
                string religion;

                int monthPeriod = 0;
                int yearPeriod =0;
                string branch = "";
                string department = "";
                string grade = "";
                int gradeLevel = 0;
                string occupation = "";
                string status = "";
                string paymentType = "";

                bool isTransfer = false;
                string bankName = "";
                string accountNumber = "";

                string holidayType = "";
                decimal mainSalary = 0;
                decimal amount = 0;
                decimal otherAmount = 0;
                decimal totalAmount = 0;
                string amountInWord = "";
                bool isPaid = false;

                //EMPLOYEE
                List<Employee> employees = new List<Employee>();
                if (holidays == "LEBARAN")
                {
                    employees = employeeRepository.GetMoslemEmployee();
                }
                else
                {
                    employees = employeeRepository.GetNonMoslemEmployee();
                }

                foreach (var e in employees)
                {
                    mainSalary = 0;
                    amount = 0;
                    otherAmount = 0;
                    totalAmount = 0;

                    startDate = e.StartDate;
                    isTransfer = e.IsTransfer;
                    bankName = e.BankName;
                    accountNumber = e.AccountNumber;

                    monthPeriod = effectiveDate.Month;
                    yearPeriod = effectiveDate.Year;



                    //LAMA KERJA    
                    CalculateYearAndMonth(startDate, effectiveDate);

                    if (monthOfWork >= 3 || yearOfWork >= 1)
                    {
                        //HARI RAYA
                        religion = e.Religion;

                        if (religion == "Islam")
                        {
                            holidayType = "LEBARAN";
                        }
                        else
                        {
                            holidayType = "NATAL";
                        }



                        //AMBIL BRANCH & DEPT
                        var dept = employeeDepartmentRepository.GetCurrentDepartment(e.ID, monthPeriod, yearPeriod);
                        if (dept != null)
                        {
                            department = dept.DepartmentName;
                            branch = dept.BranchName;
                        }
                        else
                        {
                            var previousDept = employeeDepartmentRepository.GetPreviousDepartment(e.ID, monthPeriod, yearPeriod);
                            if (previousDept != null)
                            {
                                department = previousDept.DepartmentName;
                                branch = previousDept.BranchName;
                            }
                        }

                        //AMBIL GRADE
                        var grades = employeeGradeRepository.GetCurrentGrade(e.ID, monthPeriod, yearPeriod);
                        if (grades != null)
                        {
                            grade = grades.GradeName;
                            gradeLevel = grades.GradeLevel;
                        }
                        else
                        {
                            var previousGrade = employeeGradeRepository.GetPreviousGrade(e.ID, monthPeriod, yearPeriod);
                            if (previousGrade != null)
                            {
                                department = previousGrade.GradeName;
                                gradeLevel = previousGrade.GradeLevel;
                            }
                        }


                        //AMBIL OCCUPATION
                        var occupations = employeeOccupationRepository.GetCurrentOccupation(e.ID, monthPeriod, yearPeriod);
                        if (occupations != null)
                        {
                            occupation = occupations.OccupationName;
                        }
                        else
                        {
                            var previousOccupation = employeeOccupationRepository.GetPreviousOccupation(e.ID, monthPeriod, yearPeriod);
                            if (previousOccupation != null)
                            {
                                occupation = previousOccupation.OccupationName;
                            }
                        }

                        //AMBIL STATUS
                        var statusEmployee = employeeStatusRepository.GetCurrentStatus(e.ID, monthPeriod, yearPeriod);
                        if (statusEmployee != null)
                        {
                            status = statusEmployee.Status;
                            paymentType = statusEmployee.PaymentType;
                        }
                        else
                        {
                            var previousStatus = employeeStatusRepository.GetPreviousStatus(e.ID, monthPeriod, yearPeriod);
                            if (previousStatus != null)
                            {
                                status = previousStatus.Status;
                                paymentType = previousStatus.PaymentType;
                            }
                        }




                        //AMBIL NILAI-NILAI GAJI
                        var salary = employeeSalaryRepository.GetCurrentSalary(e.ID, monthPeriod, yearPeriod);
                        if (salary != null)
                        {
                            mainSalary = salary.MainSalary;
                        }
                        else
                        {
                            var previousSalary = employeeSalaryRepository.GetPreviousSalary(e.ID, monthPeriod, yearPeriod);
                            if (previousSalary != null)
                            {
                                mainSalary = previousSalary.MainSalary;
                            }
                        }


                        //HITUNG THR
                        if (yearOfWork >= 1)
                        {
                            amount = mainSalary;
                        }
                        else if (monthOfWork >= 3 && monthOfWork <= 12)
                        {
                            amount = ((Convert.ToDecimal(monthOfWork)) / 12) * mainSalary;
                        }
                        else if (monthOfWork < 3)
                        {
                            amount = 0;
                        }

                        totalAmount = Math.Round(amount) + otherAmount;

                        if (totalAmount > 0)
                        {
                            string amountInWords = Store.GetAmounInWords(Convert.ToInt32(totalAmount));
                            string firstLetter = amountInWords.Substring(0, 2).Trim().ToUpper();
                            string theRest = amountInWords.Substring(2, amountInWords.Length - 2);
                            amountInWord = firstLetter + theRest + " rupiah";
                        }
                        else
                        {
                            amountInWord = "Nol rupiah";

                        }


                        THR oldTHR = GetByEmployeeId(e.ID, Store.ActiveYear);
                        THR thr = new THR();

                        thr.YearPeriod = yearPeriod;
                        thr.EffectiveDate = effectiveDate;
                        thr.EmployeeId = e.ID;
                        thr.StartDate = startDate;
                        thr.Branch = branch;
                        thr.Department = department;
                        thr.Grade = grade;
                        thr.GradeLevel = gradeLevel;
                        thr.Occupation = occupation;
                        thr.Status = status;
                        thr.PaymentType = paymentType;
                        thr.IsTransfer = isTransfer;
                        thr.BankName = bankName;
                        thr.AccountNumber = accountNumber;
                        thr.HolidayType = holidayType;
                        thr.YearOfWork = yearOfWork;
                        thr.MonthOfWork = Convert.ToInt32(monthOfWork);
                        thr.DayOfWork = daysOfWork;
                        thr.MainSalary = mainSalary;
                        thr.Amount = amount;
                        thr.OtherAmount = otherAmount;
                        thr.TotalAmount = totalAmount;
                        thr.AmountInWords = amountInWord;
                        thr.IsPaid = false;

                        if (oldTHR == null)
                        {
                            Save(thr);
                        }
                        else
                        {
                            thr.ID = oldTHR.ID;
                            thr.Branch = branch;
                            thr.Department = department;
                            thr.Grade = grade;
                            thr.GradeLevel = gradeLevel;
                            thr.Occupation = occupation;
                            thr.Status = status;
                            thr.PaymentType = paymentType;
                            thr.IsTransfer = isTransfer;
                            thr.BankName = bankName;
                            thr.AccountNumber = accountNumber;
                            thr.MainSalary = mainSalary;
                            thr.Amount = amount;
                            thr.OtherAmount = oldTHR.OtherAmount;
                            thr.TotalAmount = amount + thr.OtherAmount;
                            if (thr.TotalAmount > 0)
                            {
                                string amountInWords = Store.GetAmounInWords(Convert.ToInt32(thr.TotalAmount));
                                string firstLetter = amountInWords.Substring(0, 2).Trim().ToUpper();
                                string theRest = amountInWords.Substring(2, amountInWords.Length - 2);
                                thr.AmountInWords = firstLetter + theRest + " rupiah";
                            }
                            else
                            {
                                thr.AmountInWords = "Nol rupiah";

                            }

                            Update(thr);
                        }


                    }
                }



            }


        }

        public bool IsPaid(string holiday, int year)
        {
            bool IsPaid = false;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From("THR")
                    .Where("IsPaid = true")
                    .And("HolidayType").Equal(holiday)
                    .And("YearPeriod").Equal(year);

                using (var rdr = em.ExecuteReader(q.ToSql()))
                {
                    if (rdr.Read())
                    {
                        IsPaid = true;
                    }
                }

            }

            return IsPaid;

        }


        public void UpdateIsPaid(int year, bool paid)
        {
            int status = 0;
            if (paid) status = 1;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string sql = "UPDATE " + tableName + " SET IsPaid = " + status + " WHERE YearPeriod=" + year + " ";
                em.ExecuteNonQuery(sql);
            }

        }

        public void UpdateCurrentInfo(int year, EmployeeCurrentInfo currentInfo)
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
                        .And("YearPeriod").GreaterEqualThan(year)
                        .And("IsPaid = false");

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
