
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
    public interface IOverTimeRepository
    {
        OverTime GetById(Guid id);
        OverTime GetLast();
        OverTime GetLast(int month, int year);
        OverTime GetByEmployeeId(Guid employeeId);
        OverTimeValue SumByEmployeeId(Guid employeeId, int month, int year);
        List<OverTime> GetAll();
        List<OverTime> GetAll(int month, int year);
        List<OverTime> Search(string value);
        List<OverTime> Search(string value, int month, int year);
        void Save(OverTime overTime);
        void Update(OverTime overTime);
        void Delete(Guid id);
        void Delete(int month, int year);

        OverTime GetByEmployee(string employeeCode, int month, int year);
        decimal CalculateOverTime(Guid employeeId, int minute, int dayType);
        bool IsExisted(Guid employeeId, DateTime overTimeDate);
        void UpdateIsIncludePayroll(int month, int year, bool paid);
        void UpdateIsPaid(int month, int year, bool paid);
        bool IsIncludePayroll(int month, int year);
        bool IsPaid(int month, int year);
        void UpdateCurrentInfo(int month, int year, EmployeeCurrentInfo currentInfo);

    }

    public class OverTimeRepository : IOverTimeRepository
    {
        private string tableName = "OverTime";
        private DataSource ds;
        private IEmployeeRepository employeeRepository;
        private ICompanyRepository companyRepository;
        private IEmployeeSalaryRepository employeeSalaryRepository;

        public OverTimeRepository(DataSource ds)
        {
            this.ds = ds;
            employeeRepository = EntityContainer.GetType<IEmployeeRepository>();
            employeeSalaryRepository = EntityContainer.GetType<IEmployeeSalaryRepository>();
            companyRepository = EntityContainer.GetType<ICompanyRepository>();
        }


        public OverTime GetById(Guid id)
        {
            OverTime overTime = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT o.*, "
                    + "e.EmployeeCode, e.EmployeeName "
                    + "FROM OverTime o INNER JOIN Employee e ON o.EmployeeId = e.ID "
                    + "WHERE "
                    + "o.ID='{" + id + "}'";

                overTime = em.ExecuteObject<OverTime>(sql, new OverTimeMapper());
            }

            return overTime;
        }


        public OverTime GetLast()
        {
            OverTime overTime = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT TOP 1 o.*, "
                    + "e.EmployeeCode, e.EmployeeName "
                    + "FROM OverTime o INNER JOIN Employee e ON o.EmployeeId = e.ID "
                    + "ORDER BY o.OverTimeDate DESC, e.EmployeeCode ASC";

                overTime = em.ExecuteObject<OverTime>(sql, new OverTimeMapper());
            }

            return overTime;
        }

        public OverTime GetLast(int month, int year)
        {
            OverTime overTime = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT TOP 1 o.*, "
                    + "e.EmployeeCode, e.EmployeeName "
                    + "FROM OverTime o INNER JOIN Employee e ON o.EmployeeId = e.ID "
                    + "WHERE o.MonthPeriod=" + month + " AND o.YearPeriod=" + year + " "
                    + "ORDER BY o.OverTimeDate DESC, e.EmployeeCode ASC";

                overTime = em.ExecuteObject<OverTime>(sql, new OverTimeMapper());
            }

            return overTime;
        }


        public OverTime GetByEmployeeId(Guid employeeId)
        {
            OverTime overTime = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT o.*, "
                    + "e.EmployeeCode, e.EmployeeName "
                    + "FROM OverTime o INNER JOIN Employee e ON o.EmployeeId = e.ID "
                    + "o.EmployeeId='{" + employeeId + "}' "
                    + "ORDER BY o.OverTimeDate DESC, e.EmployeeCode ASC";

                overTime = em.ExecuteObject<OverTime>(sql, new OverTimeMapper());
            }

            return overTime;
        }


        public OverTimeValue SumByEmployeeId(Guid employeeId, int month, int year)
        {
            OverTimeValue overTimeValue = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT Sum(o.Amount) AS Amount "
                    + "FROM OverTime o INNER JOIN Employee e ON o.EmployeeId = e.ID "
                    + "WHERE o.EmployeeId='{" + employeeId + "}' "
                    + "AND o.MonthPeriod=" + month + " AND o.YearPeriod=" + year + " ";

                overTimeValue = em.ExecuteObject<OverTimeValue>(sql, new OverTimeValueMapper());
            }

            return overTimeValue;


        }

        public List<OverTime> GetAll()
        {
            List<OverTime> overTimes = new List<OverTime>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT o.*, "
                    + "e.EmployeeCode, e.EmployeeName "
                    + "FROM OverTime o INNER JOIN Employee e ON o.EmployeeId = e.ID "
                    + "ORDER BY o.OverTimeDate DESC, e.EmployeeCode ASC";

                overTimes = em.ExecuteList<OverTime>(sql, new OverTimeMapper());
            }

            return overTimes;
        }

        public List<OverTime> GetAll(int month, int year)
        {
            List<OverTime> overTimes = new List<OverTime>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT o.*, "
                    + "e.EmployeeCode, e.EmployeeName "
                    + "FROM OverTime o INNER JOIN Employee e ON o.EmployeeId = e.ID "
                    + "WHERE o.MonthPeriod=" + month + " AND o.YearPeriod=" + year + " "
                    + "ORDER BY o.OverTimeDate DESC, e.EmployeeCode ASC";

                overTimes = em.ExecuteList<OverTime>(sql, new OverTimeMapper());
            }

            return overTimes;
        }

        

        public List<OverTime> Search(string value)
        {
            List<OverTime> overTimes = new List<OverTime>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT o.*, "
                    + "e.EmployeeCode, e.EmployeeName "
                    + "FROM OverTime o INNER JOIN Employee e ON o.EmployeeId = e.ID "
                    + "(e.EmployeeCode like '%" + value + "%' "
                    + "OR e.EmployeeName like '%" + value + "%') "
                    + "ORDER BY o.OverTimeDate DESC, e.EmployeeCode ASC";

                overTimes = em.ExecuteList<OverTime>(sql, new OverTimeMapper());
            }

            return overTimes;
        }


        public List<OverTime> Search(string value, int month, int year)
        {
            List<OverTime> overTimes = new List<OverTime>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT o.*, "
                    + "e.EmployeeCode, e.EmployeeName "
                    + "FROM OverTime o INNER JOIN Employee e ON o.EmployeeId = e.ID "
                    + "(e.EmployeeCode like '%" + value + "%' "
                    + "OR e.EmployeeName like '%" + value + "%') "
                    + "AND o.MonthPeriod=" + month + " AND o.YearPeriod=" + year + " "
                    + "ORDER BY o.OverTimeDate DESC, e.EmployeeCode ASC";

                overTimes = em.ExecuteList<OverTime>(sql, new OverTimeMapper());
            }

            return overTimes;
        }

        public void Save(OverTime overTime)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    string[] fields = { "ID", "MonthPeriod", "YearPeriod", "EmployeeId", 
                                        "Branch", "Department","IsIncludePayroll", "IsPaid",
                                        "DayType", "OverTimeDate", "StartHour", "EndHour", "TotalInMinute","TotalInHour",
                                        "Amount", "AmountInWords","Notes", "CreatedDate","CreatedBy","ModifiedDate","ModifiedBy"};

                    object[] values = { Guid.NewGuid(), overTime.MonthPeriod, overTime.YearPeriod, overTime.EmployeeId,
                                        overTime.Branch, overTime.Department, 
                                        overTime.IsIncludePayroll==true?1:0,overTime.IsPaid==true?1:0,
                                        overTime.DayType, overTime.OverTimeDate.ToShortDateString(),
                                        overTime.StartHour, overTime.EndHour, overTime.TotalInMinute, overTime.TotalInHour,overTime.Amount, overTime.AmountInWords, 
                                        overTime.Notes, DateTime.Now.ToShortDateString(),Store.ActiveUser, DateTime.Now.ToShortDateString(),Store.ActiveUser};

                    Query q = new Query().Select(fields).From(tableName).Insert(values);

                    em.ExecuteNonQuery(q.ToSql());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public void Update(OverTime overTime)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {

                    string[] fields = { "MonthPeriod", "YearPeriod", "EmployeeId", 
                                        "Branch", "Department", "IsIncludePayroll", "IsPaid",
                                        "DayType", "OverTimeDate", "StartHour", "EndHour", "TotalInMinute", "TotalInHour",
                                        "Amount", "AmountInWords", "Notes", "CreatedDate","CreatedBy","ModifiedDate","ModifiedBy"};

                    object[] values = { overTime.MonthPeriod, overTime.YearPeriod, overTime.EmployeeId, 
                                        overTime.Branch, overTime.Department, 
                                        overTime.IsIncludePayroll==true?1:0,overTime.IsPaid==true?1:0,                                       
                                        overTime.DayType, overTime.OverTimeDate.ToShortDateString(),
                                        overTime.StartHour, overTime.EndHour, overTime.TotalInMinute, overTime.TotalInHour,
                                        overTime.Amount, overTime.AmountInWords, 
                                        overTime.Notes, DateTime.Now.ToShortDateString(),Store.ActiveUser, DateTime.Now.ToShortDateString(),Store.ActiveUser};


                    Query q = new Query().Select(fields).From(tableName).Update(values)
                        .Where("ID").Equal("{" + overTime.ID + "}");

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


        public void Delete(int month,int year)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    Query q = new Query().From(tableName).Delete()
                        .Where("MonthPeriod").Equal(month)
                        .And("YearPeriod").Equal(year)
                        .And("IsIncludePayroll=false")
                        .And("IsPaid=false");
              
                    em.ExecuteNonQuery(q.ToSql());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



       
        public OverTime GetByEmployee(string employeeCode, int month, int year)
        {
            OverTime overTime = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT o.*, "
                    + "e.EmployeeCode, e.EmployeeName "
                    + "FROM OverTime o INNER JOIN Employee e ON o.EmployeeId = e.ID "
                    + "e.EmployeeCode like '%" + employeeCode + "%' "
                    + "AND o.MonthPeriod=" + month + " AND o.YearPeriod=" + year + " "
                    + "ORDER BY o.OverTimeDate DESC, e.EmployeeCode ASC";

                overTime = em.ExecuteObject<OverTime>(sql, new OverTimeMapper());
            }

            return overTime;

            
        }



        public decimal CalculateOverTime(Guid employeeId, int minute, int dayType )
        {
           
            using (var em = EntityManagerFactory.CreateInstance(ds))
            {

                //company
                int mainSalaryDivider = 0;
                int maximumHour = 0;
                int hourDivider = 0;
                double multiply = 0;
                double multiplyHoliday = 0;

                //employee salary
                decimal mainSalary = 0;
                decimal dailySalary = 0;

                //overTime
                double hours = minute / 60;

                decimal workDayHour = 0;
                decimal holidayHour = 0;
                decimal totalHourValue = 0;
                decimal overTimeTotal = 0;
               

                //ambil pengali & pembagi lembur
                Company company = companyRepository.GetAll();
                if (company != null)
                {
                    mainSalaryDivider =  company.MainSalaryDivider;
                    maximumHour = company.OverTimeMaximumHour;
                    hourDivider = company.OverTimeHourDivider;
                    multiply = company.OverTimeMultiply;
                    multiplyHoliday = company.OverTimeMultiplyHoliday;
                }

                //EMPLOYEE - ambil gaji pokok
                Employee employee = employeeRepository.GetById(employeeId);

                if (employee != null)
                {
                    
                    var salary = employeeSalaryRepository.GetCurrentSalary(employeeId, Store.ActiveMonth, Store.ActiveYear );
                    if (salary != null)
                    {
                        mainSalary = salary.MainSalary;
                    }
                    else
                    {
                        var previousSalary = employeeSalaryRepository.GetPreviousSalary(employeeId, Store.ActiveMonth, Store.ActiveYear);
                        if (previousSalary != null)
                        {
                            mainSalary = previousSalary.MainSalary;
                        }
                        

                     }


                    dailySalary = (1 / Convert.ToDecimal(mainSalaryDivider)) * mainSalary;
                    
                    if (dayType == 0)//hari kerja
                    {
                        workDayHour = Convert.ToDecimal(multiply) * Convert.ToDecimal(hours);
                        if (workDayHour > 16)
                        {
                            workDayHour = 16;
                        }
                        totalHourValue = workDayHour / Convert.ToInt32(hourDivider);
                    }
                    else
                    {
                        holidayHour = Convert.ToDecimal(multiplyHoliday) * Convert.ToDecimal(hours);
                        if (holidayHour > 16)
                        {
                            holidayHour = 16;
                        }
                        totalHourValue = holidayHour  / Convert.ToInt32(hourDivider);
                    }


                    overTimeTotal = Math.Floor((Convert.ToDecimal(dailySalary) * Convert.ToDecimal(totalHourValue)) / 1000) * 1000;
                    

                  }


                return overTimeTotal;
                }
           
          
        }


        public bool IsExisted(Guid employeeId, DateTime overTimeDate)
        {
            bool isExisted = false;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From("OverTime")
                    .Where("EmployeeId").Equal("{" + employeeId + "}")
                    .And("OverTimeDate=#" + overTimeDate.ToShortDateString() + "#");
                
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



        public bool IsIncludePayroll(int month, int year)
        {
            bool IsIncludePayroll = false;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From("OverTime")
                    .Where("IsIncludePayroll = true")
                    .And("MonthPeriod").Equal(month)
                    .And("YearPeriod").Equal(year);

                using (var rdr = em.ExecuteReader(q.ToSql()))
                {
                    if (rdr.Read())
                    {
                        IsIncludePayroll = true;
                    }
                }

            }

            return IsIncludePayroll;

        }

        public bool IsPaid(int month, int year)
        {
            bool IsPaid = false;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From("OverTime")
                    .Where("IsPaid = true")
                    .And("MonthPeriod").Equal(month)
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

        public void UpdateIsIncludePayroll(int month, int year, bool paid)
        {
            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                int status = 0;
                if (paid) status = 1;

                string sql = "UPDATE " + tableName + " SET IsIncludePayroll = " + status + " WHERE MonthPeriod=" + month + " AND YearPeriod=" + year + " ";
                em.ExecuteNonQuery(sql);
            }
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
                    string[] columns = { "Branch", "Department" };

                    object[] values = { currentInfo.BranchName, currentInfo.DepartmentName };

                    var q = new Query().Select(columns).From(tableName).Update(values)
                        .Where("EmployeeId").Equal("{" + currentInfo.EmployeeId + "}")
                        .And("MonthPeriod").GreaterEqualThan(month)
                        .And("YearPeriod").GreaterEqualThan(year);

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
