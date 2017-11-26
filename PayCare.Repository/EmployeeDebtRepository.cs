
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
    public interface IEmployeeDebtRepository
    {
        EmployeeDebt GetById(Guid id);
        EmployeeDebt GetLast();
        EmployeeDebt GetLast(int month, int year);
        EmployeeDebt GetByEmployeeId(Guid employeeId);
        List<EmployeeDebt> GetAll();
        List<EmployeeDebt> GetAll(int month, int year);
        List<EmployeeDebt> Search(string value);
        List<EmployeeDebt> Search(string value, int month, int year);
        void Save(EmployeeDebt employeeDebt);
        void Update(EmployeeDebt employeeDebt);
        void UpdateNotes(EmployeeDebt employeeDebt);
        void Delete(Guid id);
        EmployeeDebt GetByEmployee(string employeeCode, int month, int year);
        bool IsExisted(Guid employeeId, DateTime debtDate);
    }

    public class EmployeeDebtRepository : IEmployeeDebtRepository
    {
        private string tableName = "EmployeeDebt";
        private DataSource ds;
        private IEmployeeRepository employeeRepository;
        private IEmployeeDebtItemRepository employeeDebtItemRepository;
        private ICompanyRepository companyRepository;
        private int cutOffDate;

        public EmployeeDebtRepository(DataSource ds)
        {
            this.ds = ds;
            employeeRepository = EntityContainer.GetType<IEmployeeRepository>();
            employeeDebtItemRepository = EntityContainer.GetType<IEmployeeDebtItemRepository>();
            companyRepository = EntityContainer.GetType<ICompanyRepository>();

            var company = companyRepository.GetById(Guid.Empty);
            cutOffDate = company.SalaryCutOffDate;

        }



        public EmployeeDebt GetById(Guid id)
        {
            EmployeeDebt employeeDebt = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT ed.*, "
                         + "e.EmployeeCode, e.EmployeeName "
                         + "FROM EmployeeDebt ed INNER JOIN Employee e ON ed.EmployeeId = e.ID "
                         + "WHERE "
                         + "ed.ID='{" + id + "}'";

                employeeDebt = em.ExecuteObject<EmployeeDebt>(sql, new EmployeeDebtMapper());
            }

            return employeeDebt;
        }


        public EmployeeDebt GetLast()
        {
            EmployeeDebt employeeDebt = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT TOP 1 ed.*, "
                         + "e.EmployeeCode, e.EmployeeName "
                         + "FROM EmployeeDebt ed INNER JOIN Employee e ON ed.EmployeeId = e.ID "
                        + "ORDER BY ed.DebtDate DESC, e.EmployeeCode ASC";

                employeeDebt = em.ExecuteObject<EmployeeDebt>(sql, new EmployeeDebtMapper());
            }

            return employeeDebt;
        }

        public EmployeeDebt GetLast(int month, int year)
        {
            EmployeeDebt employeeDebt = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT TOP 1 ed.*, "
                         + "e.EmployeeCode, e.EmployeeName "
                         + "FROM EmployeeDebt ed INNER JOIN Employee e ON ed.EmployeeId = e.ID "
                        + "WHERE Month(ed.DebtDate)=" + month + " AND Year(ed.DebtDate)=" + year + " "
                        + "ORDER BY ed.DebtDate DESC, e.EmployeeCode ASC";

                employeeDebt = em.ExecuteObject<EmployeeDebt>(sql, new EmployeeDebtMapper());
            }

            return employeeDebt;
        }


        public EmployeeDebt GetByEmployeeId(Guid employeeId)
        {
            EmployeeDebt employeeDebt = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT ed.*, "
                         + "e.EmployeeCode, e.EmployeeName "
                         + "FROM EmployeeDebt ed INNER JOIN Employee e ON ed.EmployeeId = e.ID "
                        + "WHERE "
                        + "ed.EmployeeId='{" + employeeId + "}' "
                        + "ORDER BY ed.DebtDate DESC, e.EmployeeCode ASC";

                employeeDebt = em.ExecuteObject<EmployeeDebt>(sql, new EmployeeDebtMapper());
            }

            return employeeDebt;
        }


        public List<EmployeeDebt> GetAll()
        {
            List<EmployeeDebt> employeeDebts = new List<EmployeeDebt>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT ed.*, "
                         + "e.EmployeeCode, e.EmployeeName "
                         + "FROM EmployeeDebt ed INNER JOIN Employee e ON ed.EmployeeId = e.ID "
                       + "ORDER BY ed.DebtDate DESC, e.EmployeeCode ASC";

                employeeDebts = em.ExecuteList<EmployeeDebt>(sql, new EmployeeDebtMapper());
            }

            return employeeDebts;
        }

        public List<EmployeeDebt> GetAll(int month, int year)
        {
            List<EmployeeDebt> employeeDebts = new List<EmployeeDebt>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT ed.*, "
                         + "e.EmployeeCode, e.EmployeeName "
                         + "FROM EmployeeDebt ed INNER JOIN Employee e ON ed.EmployeeId = e.ID "
                        + "WHERE Month(ed.DebtDate)=" + month + " AND Year(ed.DebtDate)=" + year + " "
                        + "ORDER BY ed.DebtDate DESC, e.EmployeeCode ASC";

                employeeDebts = em.ExecuteList<EmployeeDebt>(sql, new EmployeeDebtMapper());
            }

            return employeeDebts;
        }

        

        public List<EmployeeDebt> Search(string value)
        {
            List<EmployeeDebt> employeeDebts = new List<EmployeeDebt>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT ed.*, "
                         + "e.EmployeeCode, e.EmployeeName "
                         + "FROM EmployeeDebt ed INNER JOIN Employee e ON ed.EmployeeId = e.ID "
                        + "WHERE "
                        + "(e.EmployeeCode like '%" + value + "%' "
                        + "OR e.EmployeeName like '%" + value + "%') "
                        + "ORDER BY ed.DebtDate DESC, e.EmployeeCode ASC";

                employeeDebts = em.ExecuteList<EmployeeDebt>(sql, new EmployeeDebtMapper());
            }

            return employeeDebts;
        }


        public List<EmployeeDebt> Search(string value, int month, int year)
        {
            List<EmployeeDebt> employeeDebts = new List<EmployeeDebt>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT ed.*, "
                         + "e.EmployeeCode, e.EmployeeName "
                         + "FROM EmployeeDebt ed INNER JOIN Employee e ON ed.EmployeeId = e.ID "
                        + "WHERE "
                        + "(e.EmployeeCode like '%" + value + "%' "
                        + "OR e.EmployeeName like '%" + value + "%') "
                        + "AND Month(ed.DebtDate)=" + month + " AND Year(ed.DebtDate)=" + year + " "
                        + "ORDER BY ed.DebtDate DESC, e.EmployeeCode ASC";

                employeeDebts = em.ExecuteList<EmployeeDebt>(sql, new EmployeeDebtMapper());
            }

            return employeeDebts;
        }

        public void Save(EmployeeDebt employeeDebt)
        {
            Transaction tx = null;

            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    tx = em.BeginTransaction();

                    Guid ID = Guid.NewGuid();

                    string[] fields = { "ID", "EmployeeId", "DebtDate", "TotalAmount", "LongTerm", "Installment", 
                                        "AmountInWords", "Notes","IsStatus",
                                        "CreatedDate","CreatedBy","ModifiedDate","ModifiedBy"};

                    object[] values = { ID, employeeDebt.EmployeeId, 
                                        employeeDebt.DebtDate.ToShortDateString(), employeeDebt.TotalAmount, 
                                        employeeDebt.LongTerm, employeeDebt.Installment, 
                                        employeeDebt.AmountInWords, employeeDebt.Notes, 
                                        employeeDebt.IsStatus==true?1:0,DateTime.Now.ToShortDateString(),
                                        Store.ActiveUser, DateTime.Now.ToShortDateString(),Store.ActiveUser};

                    
                    Query q = new Query().Select(fields).From(tableName).Insert(values);

                    em.ExecuteNonQuery(q.ToSql(),tx);

                    int j = 1;

                    for (int i = 1; i <= employeeDebt.LongTerm; i++)
                    {
                        EmployeeDebtItem debtItem = new EmployeeDebtItem();

                        debtItem.EmployeeDebtId = ID;
                        debtItem.InstallmentCounter = i;
                        debtItem.AmountPerMonth = employeeDebt.Installment;
                        debtItem.IsPaid = false;

                        int debtDay = cutOffDate;
                        int debtMonth = employeeDebt.DebtDate.Month;
                        int debtYear = employeeDebt.DebtDate.Year;

                        int nextMonth=debtMonth + i;
                        int nextYear = 0;

                       
                        if (nextMonth > 12)
                        {
                            nextMonth = j;
                            nextYear = debtYear + 1;

                            j++;
                        }
                        else
                        {
                            nextMonth = debtMonth + i;
                            nextYear = debtYear;
                        }

                        DateTime paymentDate = DateTime.Parse(nextMonth + "/" + debtDay + "/" + nextYear);
                        debtItem.PaymentDate = paymentDate;

                        employeeDebtItemRepository.Save(em, tx, debtItem);

                    }



                    tx.Commit();
                }
            }
            catch (Exception ex)
            {
                tx.Rollback();
                throw ex;
            }

        }


        public void Update(EmployeeDebt employeeDebt)
        {
            Transaction tx = null;

            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    tx = em.BeginTransaction();

                    string[] fields = { "EmployeeId", "DebtDate", "TotalAmount", "LongTerm", "Installment", 
                                        "AmountInWords", "Notes", "IsStatus", "ModifiedDate","ModifiedBy"};

                    object[] values = { employeeDebt.EmployeeId, employeeDebt.DebtDate.ToShortDateString(), 
                                        employeeDebt.TotalAmount, employeeDebt.LongTerm, employeeDebt.Installment, 
                                        employeeDebt.AmountInWords, employeeDebt.Notes, employeeDebt.IsStatus==true?1:0,
                                        DateTime.Now.ToShortDateString(),Store.ActiveUser};

                    Query q = new Query().Select(fields).From(tableName).Update(values)
                        .Where("ID").Equal("{" + employeeDebt.ID + "}");

                    em.ExecuteNonQuery(q.ToSql(),tx);

                    employeeDebtItemRepository.Delete(employeeDebt.ID);
                    int j = 1;

                    for (int i = 1; i <= employeeDebt.LongTerm; i++)
                    {
                        EmployeeDebtItem debtItem = new EmployeeDebtItem();

                        debtItem.EmployeeDebtId = employeeDebt.ID;
                        debtItem.InstallmentCounter = i;
                        debtItem.AmountPerMonth = employeeDebt.Installment;
                        debtItem.IsPaid = false;

                        int debtDay = cutOffDate;
                        int debtMonth = employeeDebt.DebtDate.Month;
                        int debtYear = employeeDebt.DebtDate.Year;

                        int nextMonth = debtMonth + i;
                        int nextYear = 0;


                        if (nextMonth > 12)
                        {
                            nextMonth = j;
                            nextYear = debtYear + 1;

                            j++;
                        }
                        else
                        {
                            nextMonth = debtMonth + i;
                            nextYear = debtYear;
                        }

                        DateTime paymentDate = DateTime.Parse(nextMonth + "/" + debtDay + "/" + nextYear);
                        debtItem.PaymentDate = paymentDate;

                        employeeDebtItemRepository.Save(em, tx, debtItem);

                    }

                    tx.Commit();

                }
            }
            catch (Exception ex)
            {
                tx.Rollback();
                throw ex;
            }

        }

        public void UpdateNotes(EmployeeDebt employeeDebt)
        {
            Transaction tx = null;

            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    tx = em.BeginTransaction();

                    string[] fields = { "Notes", "ModifiedDate","ModifiedBy"};

                    object[] values = { employeeDebt.Notes, 
                                        DateTime.Now.ToShortDateString(),Store.ActiveUser};

                    Query q = new Query().Select(fields).From(tableName).Update(values)
                        .Where("ID").Equal("{" + employeeDebt.ID + "}");

                    em.ExecuteNonQuery(q.ToSql(), tx);

                    tx.Commit();

                }
            }
            catch (Exception ex)
            {
                tx.Rollback();
                throw ex;
            }

        }

        public void Delete(Guid id)
        {
            Transaction tx = null;
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    tx = em.BeginTransaction();

                    Query q = new Query().From(tableName).Delete().Where("ID").Equal("{" + id + "}");
                    
                    em.ExecuteNonQuery(q.ToSql(),tx);
                    employeeDebtItemRepository.Delete(em, tx, id);

                    tx.Commit();

                }
            }
            catch (Exception ex)
            {
                tx.Rollback();
                throw ex;
            }

        }


       
        public EmployeeDebt GetByEmployee(string employeeCode, int month, int year)
        {
            EmployeeDebt employeeDebt = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT ed.*, "
                         + "e.EmployeeCode, e.EmployeeName "
                         + "FROM EmployeeDebt ed INNER JOIN Employee e ON ed.EmployeeId = e.ID "
                        + "WHERE "
                        + "e.EmployeeCode like '%" + employeeCode + "%' "
                        + "AND Month(ed.DebtDate)=" + month + " AND Year(ed.DebtDate)=" + year + " "
                        + "ORDER BY ed.DebtDate DESC, e.EmployeeCode ASC";

                employeeDebt = em.ExecuteObject<EmployeeDebt>(sql, new EmployeeDebtMapper());
            }

            return employeeDebt;

            
        }



        public bool IsExisted(Guid employeeId, DateTime debtDate)
        {
            bool isExisted = false;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From("EmployeeDebt")
                    .Where("EmployeeId").Equal("{" + employeeId + "}")
                    .And("DebtDate=#" + debtDate.ToShortDateString() + "#");
                
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








    }
}
