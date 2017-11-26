using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayCare.Model;
using EntityMap;
using PayCare.Repository.Mapping;

namespace PayCare.Repository
{

    public interface IEmployeeOccupationRepository
    {
        List<EmployeeOccupation> GetByEmployeeId(Guid employeeId);
        EmployeeOccupation GetCurrentOccupation(Guid employeeId);
        void Save(EmployeeOccupation employeeOccupation);
        void Save(IEntityManager em, Transaction tx, EmployeeOccupation employeeOccupation);
        void Update(Guid employeeId, List<EmployeeOccupation> employeeOccupation);
        void Delete(IEntityManager em, Transaction tx, Guid employeeId);
        EmployeeOccupation GetCurrentOccupation(Guid employeeId, int month, int year);
        EmployeeOccupation GetPreviousOccupation(Guid employeeId, int month, int year);
        
    }

    public class EmployeeOccupationRepository : IEmployeeOccupationRepository
    {
        private DataSource ds;
        private string tableName = "EmployeeOccupation";

        public EmployeeOccupationRepository(DataSource ds)
        {
            this.ds = ds;
        }

        public List<EmployeeOccupation> GetByEmployeeId(Guid employeeId)
        {
            List<EmployeeOccupation> employeeOccupations = new List<EmployeeOccupation>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string sql = "SELECT eo.*,o.OccupationName FROM EmployeeOccupation eo "
                            + "INNER JOIN Occupation o ON eo.OccupationId = o.ID "
                            + "WHERE eo.EmployeeId='{" + employeeId + "}' "
                            +"ORDER BY eo.EffectiveDate DESC";
               
                employeeOccupations = em.ExecuteList<EmployeeOccupation>(sql, new EmployeeOccupationMapper());

            }

            return employeeOccupations;
        }


        public EmployeeOccupation GetCurrentOccupation(Guid employeeId)
        {

            EmployeeOccupation employeeOccupation = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string sql = "SELECT TOP 1 eo.*,o.OccupationName FROM EmployeeOccupation eo "
                            + "INNER JOIN Occupation o ON eo.OccupationId = o.ID "
                            + "WHERE eo.EmployeeId='{" + employeeId + "}' "
                            + "ORDER BY eo.EffectiveDate DESC";

                employeeOccupation = em.ExecuteObject<EmployeeOccupation>(sql, new EmployeeOccupationMapper());
               
            }

            return employeeOccupation;
        }


        public void Save(EmployeeOccupation employeeOccupation)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    Save(em, null, employeeOccupation);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        
        }



        public void Save(IEntityManager em, Transaction tx, EmployeeOccupation employeeOccupation)
        {
            string[] columns = { "ID", "EmployeeId", "EffectiveDate", "OccupationId", "IsTaskForce" };

            object[] values = { Guid.NewGuid(),employeeOccupation.EmployeeId,employeeOccupation.EffectiveDate.ToShortDateString(),
                                employeeOccupation.OccupationId, employeeOccupation.IsTaskForce==true?1:0};

            var q = new Query().Select(columns).From(tableName).Insert(values);


            if (tx == null)
            {
                em.ExecuteNonQuery(q.ToSql());
            }
            else
            {
                em.ExecuteNonQuery(q.ToSql(),tx);
            }
        }



        public void Update(Guid employeeId, List<EmployeeOccupation> employeeOccupations)
        {
            Transaction tx = null;

            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    tx = em.BeginTransaction();

                    Delete(em, tx, employeeId);
                    
                    foreach (var occupation in employeeOccupations)
                    {
                        occupation.EmployeeId = employeeId;

                        string[] columns = { "ID", "EmployeeId", "EffectiveDate", "OccupationId", "IsTaskForce" };

                        object[] values = { Guid.NewGuid(),occupation.EmployeeId,occupation.EffectiveDate.ToShortDateString(),
                                            occupation.OccupationId,occupation.IsTaskForce==true?1:0};

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


        public EmployeeOccupation GetCurrentOccupation(Guid employeeId, int month, int year)
        {
            EmployeeOccupation employeeOccupation = new EmployeeOccupation();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string sql = "SELECT eo.*,o.OccupationName FROM EmployeeOccupation eo "
                          + "INNER JOIN Occupation o ON eo.OccupationId = o.ID "
                          + "WHERE eo.EmployeeId='{" + employeeId + "}' "
                          + "AND month(eo.EffectiveDate)=" + month + " "
                          + "AND year(eo.EffectiveDate)=" + year;

                employeeOccupation = em.ExecuteObject<EmployeeOccupation>(sql, new EmployeeOccupationMapper());
            }

            return employeeOccupation;
        }

        public EmployeeOccupation GetPreviousOccupation(Guid employeeId, int month, int year)
        {
            EmployeeOccupation employeeOccupation = new EmployeeOccupation();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string sql = "SELECT TOP 1 eo.*,o.OccupationName FROM EmployeeOccupation eo "
                          + "INNER JOIN Occupation o ON eo.OccupationId = o.ID "
                          + "WHERE eo.EmployeeId='{" + employeeId + "}' "
                          + "AND month(eo.EffectiveDate) <=" + month + " "
                          + "AND year(eo.EffectiveDate) <=" + year;

                employeeOccupation = em.ExecuteObject<EmployeeOccupation>(sql, new EmployeeOccupationMapper());
            }

            return employeeOccupation;
        }


    }
}
