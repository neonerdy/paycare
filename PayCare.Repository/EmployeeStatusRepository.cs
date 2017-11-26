using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayCare.Model;
using EntityMap;
using PayCare.Repository.Mapping;

namespace PayCare.Repository
{
    public interface IEmployeeStatusRepository
    {
        List<EmployeeStatus> GetByEmployeeId(Guid employeeId);
        EmployeeStatus GetCurrentStatus(Guid employeeId);
        void Save(EmployeeStatus employeeStatus);
        void Save(IEntityManager em, Transaction tx, EmployeeStatus employeeStatus);
        void Update(Guid employeeId, List<EmployeeStatus> employeeStatus);
        void Delete(IEntityManager em, Transaction tx, Guid employeeId);
        EmployeeStatus GetCurrentStatus(Guid employeeId, int month, int year);
        EmployeeStatus GetPreviousStatus(Guid employeeId, int month, int year);
        
    }

    public class EmployeeStatusRepository : IEmployeeStatusRepository
    {
        private string tableName = "EmployeeStatus";
        private DataSource ds;

        public EmployeeStatusRepository(DataSource ds)
        {
            this.ds = ds;
        }
        
              

        public List<EmployeeStatus> GetByEmployeeId(Guid employeeId)
        {
            List<EmployeeStatus> employeeStatus = new List<EmployeeStatus>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                Query q = new Query().From(tableName).Where("EmployeeId").Equal(employeeId)
                    .OrderBy("EffectiveDate DESC");
             
                employeeStatus = em.ExecuteList<EmployeeStatus>(q.ToSql(), new EmployeeStatusMapper());
            }

            return employeeStatus;
        }

        public EmployeeStatus GetCurrentStatus(Guid employeeId)
        {
            EmployeeStatus employeeStatus = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                Query q = new Query().Select("TOP 1 *").From(tableName).Where("EmployeeId").Equal(employeeId)
                    .OrderBy("EffectiveDate DESC");

                employeeStatus = em.ExecuteObject<EmployeeStatus>(q.ToSql(), new EmployeeStatusMapper());
            }

            return employeeStatus;
        }


        public void Save(EmployeeStatus employeeStatus)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    Save(em, null, employeeStatus);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        public void Save(IEntityManager em, Transaction tx, EmployeeStatus employeeStatus)
        {
            string[] columns = { "ID", "EmployeeId", "EffectiveDate", "IsEnd", "EndDate", "Status", "PaymentType" };

            object[] values = { Guid.NewGuid(),employeeStatus.EmployeeId,employeeStatus.EffectiveDate.ToShortDateString(),employeeStatus.IsEnd==true?1:0,
                                employeeStatus.EndDate.ToShortDateString(),employeeStatus.Status,employeeStatus.PaymentType};

            var q = new Query().Select(columns).From(tableName).Insert(values);

            if (tx == null)
            {
                em.ExecuteNonQuery(q.ToSql());
            }
            else
            {
                em.ExecuteNonQuery(q.ToSql(), tx);
            }
        }




        public void Update(Guid employeeId, List<EmployeeStatus> employeeStatus)
        {
            Transaction tx = null;

            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    tx = em.BeginTransaction();

                    Delete(em, tx, employeeId);
                    
                    foreach (var status in employeeStatus)
                    {
                        status.EmployeeId = employeeId;

                        string[] columns = { "ID", "EmployeeId", "EffectiveDate", "IsEnd", "EndDate", "Status", "PaymentType" };

                        object[] values = { Guid.NewGuid(),status.EmployeeId,status.EffectiveDate.ToShortDateString(),status.IsEnd==true?1:0,
                                            status.EndDate.ToShortDateString(),status.Status,status.PaymentType};

                        var q = new Query().Select(columns).From(tableName).Insert(values);

                        em.ExecuteNonQuery(q.ToSql(), tx);
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



        public void Delete(IEntityManager em, Transaction tx, Guid employeeId)
        {
            var q = new Query().From(tableName).Delete()
               .Where("EmployeeId").Equal("{" + employeeId + "}");

            em.ExecuteNonQuery(q.ToSql(), tx);
        }

        public EmployeeStatus GetCurrentStatus(Guid employeeId, int month, int year)
        {
            EmployeeStatus employeeStatus = new EmployeeStatus();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                Query q = new Query().From(tableName)
                    .Where("EmployeeId").Equal("{" + employeeId + "}")
                    .And("month(EffectiveDate)").Equal(month)
                    .And("year(EffectiveDate)").Equal(year)
                    .OrderBy("EffectiveDate DESC");

                employeeStatus = em.ExecuteObject<EmployeeStatus>(q.ToSql(), new EmployeeStatusMapper());
            }

            return employeeStatus;
        }

        public EmployeeStatus GetPreviousStatus(Guid employeeId, int month, int year)
        {
            EmployeeStatus employeeStatus = new EmployeeStatus();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                Query q = new Query().Select("TOP 1 * ").From(tableName)
                    .Where("EmployeeId").Equal("{" + employeeId + "}")
                    .And("month(EffectiveDate)").LessEqualThan(month)
                    .And("year(EffectiveDate)").LessEqualThan(year)
                    .OrderBy("EffectiveDate DESC");

                employeeStatus = em.ExecuteObject<EmployeeStatus>(q.ToSql(), new EmployeeStatusMapper());
            }

            return employeeStatus;
        }


    }
}
