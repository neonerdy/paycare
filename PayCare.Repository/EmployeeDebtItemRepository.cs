using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityMap;
using PayCare.Model;
using PayCare.Repository.Mapping;

namespace PayCare.Repository
{
    public interface IEmployeeDebtItemRepository
    {
        EmployeeDebtItem GetById(Guid id);
        EmployeeDebtItem GetLast(Guid employeeDebtId);
        List<EmployeeDebtItem> GetByEmployeeDebtId(Guid employeeDebtId);
        void Save(IEntityManager em, Transaction tx, EmployeeDebtItem employeeDebtItem);
        void Update(EmployeeDebtItem employeeDebtItem);
        void UpdateStatus(Guid debtItemId, DateTime paymentDate, string status);
        void Delete(Guid employeeDebtId);
        void Delete(IEntityManager em, Transaction tx, Guid employeeDebtId);
        EmployeeDebtItemValue SumUnPaidByEmployeeId(Guid employeeId, int month, int year);
        EmployeeDebtItemValue SumPaidByEmployeeId(Guid employeeId, int month, int year);
        void UpdateIsIncludePayroll(int month, int year, bool paid);
        void UpdateIsPaid(int month, int year, bool paid);
        bool IsIncludePayroll(int month, int year);
        bool IsPaid(int month, int year);

    }


    public class EmployeeDebtItemRepository : IEmployeeDebtItemRepository
    {
        private DataSource ds;
        private string tableName = "EmployeeDebtItem";

        public EmployeeDebtItemRepository(DataSource ds)
        {
            this.ds = ds;
        }


        public EmployeeDebtItem GetById(Guid id)
        {
            EmployeeDebtItem employeeDebtItem = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT edi.*, ed.DebtDate "
                        + "FROM EmployeeDebtItem edi INNER JOIN EmployeeDebt ed ON edi.EmployeeDebtId = ed.ID "
                        + "WHERE edi.ID ='{" + id + "}'";

                employeeDebtItem = em.ExecuteObject<EmployeeDebtItem>(sql, new EmployeeDebtItemMapper());
            }

            return employeeDebtItem;
        }


        public EmployeeDebtItem GetLast(Guid employeeDebtId)
        {
            EmployeeDebtItem employeeDebtItem = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT TOP 1 edi.*, ed.DebtDate "
                        + "FROM EmployeeDebtItem edi INNER JOIN EmployeeDebt ed ON edi.EmployeeDebtId = ed.ID "
                        + "WHERE edi.EmployeeDebtId = '{" + employeeDebtId + "}' "
                        + "ORDER BY edi.Counter DESC";

                employeeDebtItem = em.ExecuteObject<EmployeeDebtItem>(sql, new EmployeeDebtItemMapper());
            }

            return employeeDebtItem;
        }

        public List<EmployeeDebtItem> GetByEmployeeDebtId(Guid employeeDebtId)
        {
            List<EmployeeDebtItem> employeeDebtItems = new List<EmployeeDebtItem>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string sql = "SELECT edi.*, ed.DebtDate "
                           + "FROM EmployeeDebtItem edi INNER JOIN EmployeeDebt ed ON edi.EmployeeDebtId = ed.ID "
                           + "WHERE edi.EmployeeDebtId='{" + employeeDebtId + "}' "
                           + "ORDER BY edi.InstallmentCounter";
                            

                employeeDebtItems = em.ExecuteList<EmployeeDebtItem>(sql, new EmployeeDebtItemMapper());
            }

            return employeeDebtItems;
        }



        public void Save(IEntityManager em, Transaction tx, EmployeeDebtItem employeeDebtItem)
        {
            string[] columns = { "ID", "EmployeeDebtId", "InstallmentCounter",
                                   "AmountPerMonth", 
                                   "IsPaid", "IsIncludePayroll",
                                 "PaymentDate"};

            object[] values = { Guid.NewGuid(),employeeDebtItem.EmployeeDebtId, employeeDebtItem.InstallmentCounter,
                                employeeDebtItem.AmountPerMonth,
                                employeeDebtItem.IsPaid==true?1:0,employeeDebtItem.IsIncludePayroll==true?1:0,
                                employeeDebtItem.PaymentDate.ToShortDateString()};
            
            var q = new Query().Select(columns).From(tableName).Insert(values);

            em.ExecuteNonQuery(q.ToSql(),tx);

        }



        public void Update(EmployeeDebtItem employeeDebtItem)
        {
            try
            {

                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    string[] columns = { "EmployeeDebtId", "InstallmentCounter", 
                                           "AmountPerMonth", 
                                           "IsPaid", "IsIncludePayroll",
                                           "PaymentDate" };

                    object[] values = { employeeDebtItem.EmployeeDebtId, employeeDebtItem.InstallmentCounter,
                                        employeeDebtItem.AmountPerMonth,
                                        employeeDebtItem.IsPaid==true?1:0, employeeDebtItem.IsIncludePayroll==true?1:0,
                                        employeeDebtItem.PaymentDate.ToShortDateString()};

                    var q = new Query().Select(columns).From(tableName).Update(values)
                        .Where("ID").Equal("{" + employeeDebtItem.ID + "}");

                    em.ExecuteNonQuery(q.ToSql());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void UpdateStatus(Guid debtItemId,DateTime paymentDate,string status)
        {
            try
            {

                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    bool isPaid=false;

                    if (status == "Lunas")
                        isPaid = true;

                    string[] columns = { "IsPaid", "PaymentDate" };

                    object[] values = { isPaid==true?1:0, paymentDate.ToShortDateString()};

                    var q = new Query().Select(columns).From(tableName).Update(values)
                        .Where("ID").Equal("{" + debtItemId + "}");

                    em.ExecuteNonQuery(q.ToSql());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        

        public void Delete(Guid employeeDebtId)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    var q = new Query().From(tableName).Delete().Where("EmployeeDebtId")
                        .Equal("{" + employeeDebtId + "}");

                    em.ExecuteNonQuery(q.ToSql());
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Delete(IEntityManager em, Transaction tx, Guid employeeDebtId)
        {
               
            var q = new Query().From(tableName).Delete().Where("EmployeeDebtId")
                .Equal("{" + employeeDebtId + "}");

            em.ExecuteNonQuery(q.ToSql(),tx);
      
        }

        
        public EmployeeDebtItemValue SumUnPaidByEmployeeId(Guid employeeId, int month, int year)
        {
            EmployeeDebtItemValue employeeDebtItemValue = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT Sum(edi.AmountPerMonth) AS Amount "
                        + "FROM EmployeeDebt ed INNER JOIN EmployeeDebtItem edi ON ed.ID = edi.EmployeeDebtId "
                        + "WHERE ed.EmployeeId='{" + employeeId + "}' "
                        + "AND edi.IsPaid = False  "
                        + "AND Month(edi.PaymentDate)=" + month + " AND Year(edi.PaymentDate)=" + year + " ";

                employeeDebtItemValue = em.ExecuteObject<EmployeeDebtItemValue>(sql, new EmployeeDebtItemValueMapper());
            }

            return employeeDebtItemValue;


        }


        public EmployeeDebtItemValue SumPaidByEmployeeId(Guid employeeId, int month, int year)
        {
            EmployeeDebtItemValue employeeDebtItemValue = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT Sum(EmployeeDebtItem.AmountPerMonth) AS Amount "
                        + "FROM EmployeeDebt ed INNER JOIN EmployeeDebtItem edi ON ed.ID = edi.EmployeeDebtId "
                        + "WHERE ed.EmployeeId='{" + employeeId + "}' "
                        + "AND EmployeeDebtItem.IsPaid = true  "
                        + "AND Month(edi.PaymentDate)=" + month + " AND Year(edi.PaymentDate)=" + year + " ";

                employeeDebtItemValue = em.ExecuteObject<EmployeeDebtItemValue>(sql, new EmployeeDebtItemValueMapper());
            }

            return employeeDebtItemValue;


        }

        public bool IsIncludePayroll(int month, int year)
        {
            bool IsIncludePayroll = false;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From("EmployeeDebtItem")
                    .Where("IsIncludePayroll = true")
                    .And("Month(PaymentDate)").Equal(month)
                    .And("Year(PaymentDate)").Equal(year);

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
                var q = new Query().From("EmployeeDebtItem")
                    .Where("IsPaid = true")
                    .And("Month(PaymentDate)").Equal(month)
                    .And("Year(PaymentDate)").Equal(year);

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

                string sql = "UPDATE " + tableName + " SET IsIncludePayroll = " + status + " WHERE Month(PaymentDate)=" + month + " AND Year(PaymentDate)=" + year + " ";
                em.ExecuteNonQuery(sql);
            }
        }

        public void UpdateIsPaid(int month, int year, bool paid)
        {
            int status = 0;
            if (paid) status = 1;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string sql = "UPDATE " + tableName + " SET IsPaid = " + status + " WHERE Month(PaymentDate)=" + month + " AND Year(PaymentDate)=" + year + " ";
                em.ExecuteNonQuery(sql);
            }

        }
      




    }

   
}
