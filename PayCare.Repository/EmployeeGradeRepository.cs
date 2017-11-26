using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayCare.Model;
using EntityMap;
using PayCare.Repository.Mapping;

namespace PayCare.Repository
{

    public interface IEmployeeGradeRepository
    {
        List<EmployeeGrade> GetByEmployeeId(Guid employeeId);
        EmployeeGrade GetCurrentGrade(Guid employeeId);
        void Save(EmployeeGrade employeeGrade);
        void Save(IEntityManager em, Transaction tx, EmployeeGrade employeeGrade);
        void Update(Guid employeeId, List<EmployeeGrade> employeeGrade);
        void Delete(IEntityManager em, Transaction tx, Guid employeeId);
        EmployeeGrade GetCurrentGrade(Guid employeeId, int month, int year);
        EmployeeGrade GetPreviousGrade(Guid employeeId, int month, int year);
        
    }

    public class EmployeeGradeRepository : IEmployeeGradeRepository 
    {

        private string tableName = "EmployeeGrade";
        private DataSource ds;

        public EmployeeGradeRepository(DataSource ds)
        {
            this.ds = ds;
        }

        
        public List<EmployeeGrade> GetByEmployeeId(Guid employeeId)
        {
            List<EmployeeGrade> employeeGrades = new List<EmployeeGrade>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string sql = "SELECT eg.*,g.GradeName,g.GradeLevel FROM EmployeeGrade eg "
                           + "INNER JOIN Grade g ON eg.GradeId = g.ID "
                           + "WHERE eg.EmployeeId='{" + employeeId + "}' "
                           + "ORDER BY eg.EffectiveDate DESC";

                employeeGrades = em.ExecuteList<EmployeeGrade>(sql, new EmployeeGradeMapper());

            }

            return employeeGrades;
        }


        public EmployeeGrade GetCurrentGrade(Guid employeeId)
        {
            EmployeeGrade employeeGrade = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string sql = "SELECT TOP 1 eg.*,g.GradeName,g.GradeLevel FROM EmployeeGrade eg "
                           + "INNER JOIN Grade g ON eg.GradeId = g.ID "
                           + "WHERE eg.EmployeeId='{" + employeeId + "}' "
                           + "ORDER BY eg.EffectiveDate DESC";

                employeeGrade = em.ExecuteObject<EmployeeGrade>(sql, new EmployeeGradeMapper());
              

            }

            return employeeGrade;
        }


        public void Save(EmployeeGrade employeeGrade)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    Save(em, null, employeeGrade);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        
        }

        

        public void Save(IEntityManager em, Transaction tx, EmployeeGrade employeeGrade)
        {
            string[] columns = { "ID", "EmployeeId", "EffectiveDate", "GradeId" };

            object[] values = { Guid.NewGuid(),employeeGrade.EmployeeId,employeeGrade.EffectiveDate.ToShortDateString(),
                                employeeGrade.GradeId};

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
        


        public void Update(Guid employeeId, List<EmployeeGrade> employeeGrades)
        {
            Transaction tx = null;

            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    tx = em.BeginTransaction();

                    Delete(em, tx, employeeId);
                    foreach (var grade in employeeGrades)
                    {
                        grade.EmployeeId = employeeId;

                        string[] columns = { "ID", "EmployeeId", "EffectiveDate", "GradeId" };

                        object[] values = { Guid.NewGuid(),grade.EmployeeId,grade.EffectiveDate.ToShortDateString(),
                                            grade.GradeId};

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

        public EmployeeGrade GetCurrentGrade(Guid employeeId, int month, int year)
        {
            EmployeeGrade employeeGrade = new EmployeeGrade();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string sql = "SELECT eg.*,g.GradeName,g.GradeLevel FROM EmployeeGrade eg "
                     + "INNER JOIN Grade g ON eg.GradeId = g.ID "
                     + "WHERE eg.EmployeeId='{" + employeeId + "}' "
                     + "AND month(eg.EffectiveDate)=" + month + " "
                     + "AND year(eg.EffectiveDate)=" + year;

                employeeGrade = em.ExecuteObject<EmployeeGrade>(sql, new EmployeeGradeMapper());
            }

            return employeeGrade;
        }

        public EmployeeGrade GetPreviousGrade(Guid employeeId, int month, int year)
        {
            EmployeeGrade employeeGrade = new EmployeeGrade();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string sql = "SELECT TOP 1 eg.*,g.GradeName,g.GradeLevel FROM EmployeeGrade eg "
                    + "INNER JOIN Grade g ON eg.GradeId = g.ID "
                    + "WHERE eg.EmployeeId='{" + employeeId + "}' "
                    + "AND month(eg.EffectiveDate) <=" + month + " "
                    + "AND year(eg.EffectiveDate) <=" + year;

                   employeeGrade = em.ExecuteObject<EmployeeGrade>(sql, new EmployeeGradeMapper());
            }

            return employeeGrade;
        }



    }
}
