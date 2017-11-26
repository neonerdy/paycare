
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
    public interface IIncentiveRepository
    {
        Incentive GetById(Guid id);
        Incentive GetLast();
        Incentive GetLast(int month, int year);
        Incentive GetByEmployeeId(Guid employeeId);
        IncentiveValue SumByEmployeeId(Guid employeeId, int month, int year);
        List<Incentive> GetAll();
        List<Incentive> GetTransfered(int month, int year);
        List<Incentive> GetAll(int month, int year);
        List<Incentive> Search(string value);
        List<Incentive> Search(string value, int month, int year);
        void Save(Incentive incentive);
        void Update(Incentive incentive);
        void Delete(Guid id);
        void Delete(int month, int year);

        Incentive GetByEmployee(string employeeCode, int month, int year);
        bool IsExisted(Guid employeeId, int month, int year);
        void UpdateIsIncludePayroll(int month, int year, bool paid);
        void UpdateIsPaid(int month, int year, bool paid);
        bool IsIncludePayroll(int month, int year);
        bool IsPaid(int month, int year);
        void UpdateCurrentInfo(int month, int year, EmployeeCurrentInfo currentInfo);
    }

    public class IncentiveRepository : IIncentiveRepository
    {
        private string tableName = "Incentive";
        private DataSource ds;
        private IEmployeeRepository employeeRepository;
        private ICompanyRepository companyRepository;
       
        public IncentiveRepository(DataSource ds)
        {
            this.ds = ds;
            employeeRepository = EntityContainer.GetType<IEmployeeRepository>();
            companyRepository = EntityContainer.GetType<ICompanyRepository>();
            
        }


        public Incentive GetById(Guid id)
        {
            Incentive incentive = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
              
                var sql = "SELECT i.*, "
                        + "e.EmployeeCode, e.EmployeeName "
                        + "FROM Incentive i INNER JOIN Employee e ON i.EmployeeId = e.ID "
                        + "WHERE "
                        + "i.ID='{" + id + "}'";

                incentive = em.ExecuteObject<Incentive>(sql, new IncentiveMapper());
            }

            return incentive;
        }


        public Incentive GetLast()
        {
            Incentive incentive = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                 var sql = "SELECT TOP 1 i.*, "
                        + "e.EmployeeCode, e.EmployeeName "
                        + "FROM Incentive i INNER JOIN Employee e ON i.EmployeeId = e.ID " 
                        + "ORDER BY e.EmployeeCode ASC";

                incentive = em.ExecuteObject<Incentive>(sql, new IncentiveMapper());
            }

            return incentive;
        }

        public Incentive GetLast(int month, int year)
        {
            Incentive incentive = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                 var sql = "SELECT i.*, "
                        + "e.EmployeeCode, e.EmployeeName "
                        + "FROM Incentive i INNER JOIN Employee e ON i.EmployeeId = e.ID "
                        + "WHERE i.MonthPeriod=" + month + " AND i.YearPeriod=" + year + " "
                        + "ORDER BY e.EmployeeCode ASC";

                incentive = em.ExecuteObject<Incentive>(sql, new IncentiveMapper());
            }

            return incentive;
        }


        public Incentive GetByEmployeeId(Guid employeeId)
        {
            Incentive incentive = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                 var sql = "SELECT i.*, "
                        + "e.EmployeeCode, e.EmployeeName "
                        + "FROM Incentive i INNER JOIN Employee e ON i.EmployeeId = e.ID "
                        + "WHERE "
                        + "i.EmployeeId='{" + employeeId + "}' "
                        + "ORDER BY e.EmployeeCode ASC";

                incentive = em.ExecuteObject<Incentive>(sql, new IncentiveMapper());
            }

            return incentive;
        }


        public List<Incentive> GetAll()
        {
            List<Incentive> incentives = new List<Incentive>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT i.*, "
                        + "e.EmployeeCode, e.EmployeeName "
                        + "FROM Incentive i INNER JOIN Employee e ON i.EmployeeId = e.ID "
                        + "ORDER BY e.EmployeeCode ASC";

                incentives = em.ExecuteList<Incentive>(sql, new IncentiveMapper());
            }

            return incentives;
        }


        public List<Incentive> GetTransfered(int month, int year)
        {
            List<Incentive> incentives = new List<Incentive>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                
                string bankName = string.Empty;
                var company = companyRepository.GetById(Guid.Empty);
                
                if (company != null)
                {
                    bankName = company.BankName;
                }

                var sql = "SELECT i.*, "
                        + "e.EmployeeCode, e.EmployeeName "
                        + "FROM Incentive i INNER JOIN Employee e ON i.EmployeeId = e.ID "
                        + "WHERE i.IsTransfer=true AND i.BankName='" + bankName + "' "
                        + "AND i.IsIncludePAyroll=false AND i.IsPaid=false "
                        + "AND i.MonthPeriod=" + month + " AND i.YearPeriod=" + year; 

                incentives = em.ExecuteList<Incentive>(sql, new IncentiveMapper());
            }

            return incentives;
        }


        public List<Incentive> GetAll(int month, int year)
        {
            List<Incentive> incentives = new List<Incentive>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                 var sql = "SELECT i.*, "
                        + "e.EmployeeCode, e.EmployeeName "
                        + "FROM Incentive i INNER JOIN Employee e ON i.EmployeeId = e.ID "
                        + "WHERE i.MonthPeriod=" + month + " AND i.YearPeriod=" + year + " "
                        + "ORDER BY e.EmployeeCode ASC";

                incentives = em.ExecuteList<Incentive>(sql, new IncentiveMapper());
            }

            return incentives;
        }


        public IncentiveValue SumByEmployeeId(Guid employeeId, int month, int year)
        {
            IncentiveValue incentiveValue = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT Sum(i.Amount) AS Amount "
                    + "FROM Incentive i INNER JOIN Employee e ON i.EmployeeId = e.ID "
                        + "WHERE i.EmployeeId='{" + employeeId + "}' "
                        + "AND i.MonthPeriod=" + month + " AND i.YearPeriod=" + year + " ";

                incentiveValue = em.ExecuteObject<IncentiveValue>(sql, new IncentiveValueMapper());
            }

            return incentiveValue;


        }

        public List<Incentive> Search(string value)
        {
            List<Incentive> incentives = new List<Incentive>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                 var sql = "SELECT i.*, "
                        + "e.EmployeeCode, e.EmployeeName "
                        + "FROM Incentive i INNER JOIN Employee e ON i.EmployeeId = e.ID "
                        + "WHERE "
                        + "(e.EmployeeCode like '%" + value + "%' "
                        + "OR e.EmployeeName like '%" + value + "%') "
                        + "ORDER BY e.EmployeeCode ASC";

                incentives = em.ExecuteList<Incentive>(sql, new IncentiveMapper());
            }

            return incentives;
        }


        public List<Incentive> Search(string value, int month, int year)
        {
            List<Incentive> incentives = new List<Incentive>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                 var sql = "SELECT i.*, "
                        + "e.EmployeeCode, e.EmployeeName "
                        + "FROM Incentive i INNER JOIN Employee e ON i.EmployeeId = e.ID "
                        + "WHERE "
                        + "(e.EmployeeCode like '%" + value + "%' "
                        + "OR e.EmployeeName like '%" + value + "%') "
                        + "AND i.MonthPeriod=" + month + " AND i.YearPeriod=" + year + " "
                        + "ORDER BY e.EmployeeCode ASC";

                incentives = em.ExecuteList<Incentive>(sql, new IncentiveMapper());
            }

            return incentives;
        }

        public void Save(Incentive incentive)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    string[] fields = { "ID", "MonthPeriod", "YearPeriod", "EmployeeId",
                                          "Amount", "AmountInWords", "Notes", 
                                          "Branch", "Department",
                                          "IsTransfer", "BankName", "AccountNumber",                                           
                                          "IsIncludePayroll", "IsPaid",
                                          "CreatedDate","CreatedBy","ModifiedDate","ModifiedBy"};

                    object[] values = { Guid.NewGuid(), incentive.MonthPeriod, incentive.YearPeriod, incentive.EmployeeId, 
                                          incentive.Amount, incentive.AmountInWords, incentive.Notes, 
                                          incentive.Branch, incentive.Department,
                                          incentive.IsTransfer==true?1:0, incentive.BankName, incentive.AccountNumber,                                                                                    
                                          incentive.IsIncludePayroll==true?1:0,incentive.IsPaid==true?1:0,
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


        public void Update(Incentive incentive)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {

                    string[] fields = { "MonthPeriod", "YearPeriod", "EmployeeId",
                                          "Amount", "AmountInWords", "Notes", 
                                          "Branch", "Department",
                                          "IsTransfer", "BankName", "AccountNumber",
                                          "IsIncludePayroll", "IsPaid",
                                          "ModifiedDate","ModifiedBy"};

                    object[] values = { incentive.MonthPeriod, incentive.YearPeriod, incentive.EmployeeId, 
                                          incentive.Amount, incentive.AmountInWords, incentive.Notes, 
                                          incentive.Branch, incentive.Department,
                                          incentive.IsTransfer==true?1:0, incentive.BankName, incentive.AccountNumber,                                                                                    
                                          incentive.IsIncludePayroll==true?1:0,incentive.IsPaid==true?1:0,
                                      DateTime.Now.ToShortDateString(),Store.ActiveUser};

                    Query q = new Query().Select(fields).From(tableName).Update(values)
                        .Where("ID").Equal("{" + incentive.ID + "}");

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
                    var q = new Query().From(tableName).Delete()
                        .Where("MonthPeriod").Equal(month)
                        .And("YearPeriod").Equal(year)
                        .And("IsPaid=false").And("IsIncludePayroll=false");

                    em.ExecuteNonQuery(q.ToSql());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


       
        public Incentive GetByEmployee(string employeeCode, int month, int year)
        {
            Incentive incentive = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                 var sql = "SELECT i.*, "
                        + "e.EmployeeCode, e.EmployeeName "
                        + "FROM Incentive i INNER JOIN Employee e ON i.EmployeeId = e.ID "
                        + "WHERE "
                        + "e.EmployeeCode like '%" + employeeCode + "%' "
                        + "AND i.MonthPeriod=" + month + " AND i.YearPeriod=" + year + " "
                        + "ORDER BY e.EmployeeCode ASC";

                incentive = em.ExecuteObject<Incentive>(sql, new IncentiveMapper());
            }

            return incentive;

            
        }



        
        public bool IsExisted(Guid employeeId, int month, int year)
        {
            bool isExisted = false;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From("Incentive")
                    .Where("EmployeeId").Equal("{" + employeeId + "}")
                    .And("MonthPeriod").Equal(month)
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

        public bool IsIncludePayroll(int month, int year)
        {
            bool IsIncludePayroll = false;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From("Incentive")
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
                var q = new Query().From("Incentive")
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
                    string[] columns = { "Branch", "Department",
                                       "BankName", "AccountNumber"};

                    object[] values = { currentInfo.BranchName, currentInfo.DepartmentName,
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
