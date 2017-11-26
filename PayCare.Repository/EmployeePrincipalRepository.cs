using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityMap;
using PayCare.Model;
using PayCare.Repository.Mapping;

namespace PayCare.Repository
{

    public interface IEmployeePrincipalRepository
    {
        List<EmployeePrincipal> GetByEmployeeId(Guid employeeId);
        void Save(IEntityManager em, Transaction tx, EmployeePrincipal employeePrincipal);
        void Update(Guid employeeId, List<EmployeePrincipal> employeePrincipal);
        void Delete(IEntityManager em, Transaction tx, Guid employeeId);
        EmployeePrincipal GetCurrentPrincipal(Guid employeeId, int month, int year);
        EmployeePrincipal GetPreviousPrincipal(Guid employeeId, int month, int year);
        int CountActivePrincipal(Guid employeeId, int month, int year);
    }


    public class EmployeePrincipalRepository : IEmployeePrincipalRepository
    {
        private DataSource ds;
        private string tableName = "EmployeePrincipal";

        public EmployeePrincipalRepository(DataSource ds)
        {
            this.ds = ds;
        }

        public List<EmployeePrincipal> GetByEmployeeId(Guid employeeId)
        {
            List<EmployeePrincipal> employeePrincipals = new List<EmployeePrincipal>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string sql = "SELECT ep.*,p.PrincipalName FROM Principal p "
                           + "INNER JOIN EmployeePrincipal ep ON p.ID = ep.PrincipalId "
                           + "WHERE ep.EmployeeId='{" + employeeId + "}' "
                           + "ORDER BY ep.EffectiveDate DESC";
                            

                employeePrincipals = em.ExecuteList<EmployeePrincipal>(sql, new EmployeePrincipalMapper());
            }

            return employeePrincipals;
        }



        public void Save(IEntityManager em, Transaction tx, EmployeePrincipal employeePrincipal)
        {
            string[] columns = { "ID", "EmployeeId", "EffectiveDate", "PrincipalId", "IsActive"};

            object[] values = { Guid.NewGuid(),employeePrincipal.EmployeeId,employeePrincipal.EffectiveDate.ToShortDateString(),
                                employeePrincipal.PrincipalId,employeePrincipal.IsActive==true?1:0};

            var q = new Query().Select(columns).From(tableName).Insert(values);

            em.ExecuteNonQuery(q.ToSql(), tx);
        }



        public void Update(Guid employeeId, List<EmployeePrincipal> employeePrincipals)
        {
            Transaction tx = null;

            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    tx = em.BeginTransaction();

                    Delete(em, tx, employeeId);
                    
                    foreach (var principal in employeePrincipals)
                    {
                        principal.EmployeeId = employeeId;

                        string[] columns = { "ID", "EmployeeId", "EffectiveDate", "PrincipalId", "IsActive" };

                        object[] values = { Guid.NewGuid(),principal.EmployeeId,principal.EffectiveDate.ToShortDateString(),
                                            principal.PrincipalId,principal.IsActive==true?1:0};

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

        public EmployeePrincipal GetCurrentPrincipal(Guid employeeId, int month, int year)
        {
            EmployeePrincipal employeePrincipal = new EmployeePrincipal();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string sql = "SELECT ep.*,p.PrincipalName FROM Principal p "
                          + "INNER JOIN EmployeePrincipal ep ON p.ID = ep.PrincipalId "
                          + "WHERE ep.EmployeeId='{" + employeeId + "}' "
                          + "AND month(ep.EffectiveDate)=" + month + " "
                          + "AND year(ep.EffectiveDate)=" + year;

                employeePrincipal = em.ExecuteObject<EmployeePrincipal>(sql, new EmployeePrincipalMapper());
            }

            return employeePrincipal;
        }

        public EmployeePrincipal GetPreviousPrincipal(Guid employeeId, int month, int year)
        {
            EmployeePrincipal employeePrincipal = new EmployeePrincipal();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string sql = "SELECT TOP 1 ep.*,p.PrincipalName FROM Principal p "
                          + "INNER JOIN EmployeePrincipal ep ON p.ID = ep.PrincipalId "
                          + "WHERE ep.EmployeeId='{" + employeeId + "}' "
                          + "AND month(ep.EffectiveDate) <=" + month + " "
                          + "AND year(ep.EffectiveDate) <=" + year;
                
                employeePrincipal = em.ExecuteObject<EmployeePrincipal>(sql, new EmployeePrincipalMapper());
            }

            return employeePrincipal;
        }

        

        public int CountActivePrincipal(Guid employeeId, int month, int year)
        {
            List<EmployeePrincipal> employeePrincipal = new List<EmployeePrincipal>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string sql = "SELECT ep.*,p.PrincipalName FROM Principal p "
                          + "INNER JOIN EmployeePrincipal ep ON p.ID = ep.PrincipalId "
                          + "WHERE ep.EmployeeId='{" + employeeId + "}' "
                          + "AND ep.IsActive = true "
                          + "AND month(ep.EffectiveDate)=" + month + " "
                          + "AND year(ep.EffectiveDate)=" + year;

                employeePrincipal = em.ExecuteList<EmployeePrincipal>(sql, new EmployeePrincipalMapper());
            }

            return employeePrincipal.Count();
        }



    }

   
}
