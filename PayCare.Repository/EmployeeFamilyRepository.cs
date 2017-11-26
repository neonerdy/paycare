using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayCare.Model;
using EntityMap;
using PayCare.Repository.Mapping;

namespace PayCare.Repository
{
    public interface IEmployeeFamilyRepository
    {
        List<EmployeeFamily> GetByEmployeeId(Guid employeeId);
        void Save(IEntityManager em,Transaction tx,EmployeeFamily employeeFamily);
        void Update(Guid employeeId, List<EmployeeFamily> employeeFamily);
        void Delete(IEntityManager em, Transaction tx, Guid employeeId);
        bool IsFamilyInsurance(Guid employeeId);
    }


    public class EmployeeFamilyRepository : IEmployeeFamilyRepository
    {
        private DataSource ds;
        private string tableName = "EmployeeFamily";

        public EmployeeFamilyRepository(DataSource ds)
        {
            this.ds = ds;
        }



        #region IEmployeeFamilyRepository Members

        public List<EmployeeFamily> GetByEmployeeId(Guid employeeId)
        {
            List<EmployeeFamily> employeeFamily = new List<EmployeeFamily>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From(tableName).Where("EmployeeId")
                    .Equal("{" + employeeId + "}");

                employeeFamily = em.ExecuteList<EmployeeFamily>(q.ToSql(),
                    new EmployeeFamilyMapper());

            }

            return employeeFamily;
        }



        public void Save(IEntityManager em,Transaction tx,EmployeeFamily employeeFamily)
        {
            string[] columns = {"ID","EmployeeId","FamilyName","Status","Education","BirthPlace",
                               "BirthDate","Job","IsInsurance"};

            object[] values = { Guid.NewGuid(),employeeFamily.EmployeeId,employeeFamily.FamilyName,
                              employeeFamily.Status,employeeFamily.Education,employeeFamily.BirthPlace,
                              employeeFamily.BirthDate.ToShortDateString(),employeeFamily.Job,
                              employeeFamily.IsInsurance==true?1:0};

            var q = new Query().Select(columns).From(tableName).Insert(values);

            em.ExecuteNonQuery(q.ToSql(),tx);
        }


        public void Update(Guid employeeId, List<EmployeeFamily> employeeFamily)
        {
            Transaction tx = null;

            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    tx = em.BeginTransaction();

                    Delete(em, tx, employeeId);

                    foreach (var ef in employeeFamily)
                    {
                        ef.EmployeeId = employeeId;

                        string[] columns = {"ID","EmployeeId","FamilyName","Status","Education","BirthPlace",
                               "BirthDate","Job","IsInsurance"};

                        object[] values = { Guid.NewGuid(),ef.EmployeeId,ef.FamilyName,
                              ef.Status,ef.Education,ef.BirthPlace,
                              ef.BirthDate.ToShortDateString(),ef.Job,
                              ef.IsInsurance==true?1:0};

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


        public void Delete(IEntityManager em,Transaction tx, Guid employeeId)
        {
            var q = new Query().From(tableName).Delete()
                .Where("EmployeeId").Equal("{" + employeeId + "}");
            
            em.ExecuteNonQuery(q.ToSql(),tx);
        }


        public bool IsFamilyInsurance(Guid employeeId)
        {
            bool isInsurance = false;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From("EmployeeFamily")
                    .Where("IsInsurance=true")
                    .And("EmployeeId").Equal("{" + employeeId + "}");

                using (var rdr = em.ExecuteReader(q.ToSql()))
                {
                    if (rdr.Read())
                    {
                        isInsurance = true;
                    }
                }

            }

            return isInsurance;

        }

        #endregion
    }
}
