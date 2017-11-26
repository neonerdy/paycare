using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayCare.Model;
using EntityMap;
using PayCare.Repository.Mapping;

namespace PayCare.Repository
{

    public interface IEmployeeDepartmentRepository
    {
        List<EmployeeDepartment> GetByEmployeeId(Guid employeeId);
        EmployeeDepartment GetCurrentDepartment(Guid employeeId);
        EmployeeDepartment GetCurrentDepartment(Guid employeeId, int month, int year);
        EmployeeDepartment GetPreviousDepartment(Guid employeeId, int month, int year);
        void Save(EmployeeDepartment employeeDepartment);
        void Save(IEntityManager em, Transaction tx, EmployeeDepartment employeeDepartment);
        void Update(Guid employeeId, List<EmployeeDepartment> employeeDepartment);
        void Delete(IEntityManager em, Transaction tx, Guid employeeId);
    }

    public class EmployeeDepartmentRepository : IEmployeeDepartmentRepository
    {
        private string tableName = "EmployeeDepartment";
        private DataSource ds;

        public EmployeeDepartmentRepository(DataSource ds)
        {
            this.ds = ds;
        }
        
        
        public List<EmployeeDepartment> GetByEmployeeId(Guid employeeId)
        {
            List<EmployeeDepartment> employeeDepartments = new List<EmployeeDepartment>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string sql = "SELECT  ed.*,b.BranchName,d.DepartmentName FROM (EmployeeDepartment ed "
                     + "INNER JOIN Branch b ON ed.BranchId = b.ID) INNER JOIN Department d "
                     + "ON ed.DepartmentId = d.ID "
                     + "WHERE ed.EmployeeId='{" + employeeId + "}' "
                     + "ORDER BY ed.EffectiveDate DESC";

                   employeeDepartments = em.ExecuteList<EmployeeDepartment>(sql, new EmployeeDepartmentMapper());
            }

            return employeeDepartments;
        }


        public EmployeeDepartment GetCurrentDepartment(Guid employeeId)
        {
            EmployeeDepartment employeeDepartment = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string sql = "SELECT  TOP 1 ed.*,b.BranchName,d.DepartmentName FROM (EmployeeDepartment ed "
                     + "INNER JOIN Branch b ON ed.BranchId = b.ID) INNER JOIN Department d "
                     + "ON ed.DepartmentId = d.ID "
                     + "WHERE ed.EmployeeId='{" + employeeId + "}' "
                     + "ORDER BY ed.EffectiveDate DESC";

                employeeDepartment = em.ExecuteObject<EmployeeDepartment>(sql, new EmployeeDepartmentMapper());

            }

            return employeeDepartment;
        }


        public EmployeeDepartment GetCurrentDepartment(Guid employeeId, int month, int year)
        {
            EmployeeDepartment employeeDepartment = new EmployeeDepartment();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string sql = "SELECT  ed.*,b.BranchName,d.DepartmentName FROM (EmployeeDepartment ed "
                           + "INNER JOIN Branch b ON ed.BranchId = b.ID) INNER JOIN Department d "
                           + "ON ed.DepartmentId = d.ID "
                           + "WHERE ed.EmployeeId='{" + employeeId + "}' "
                           + "AND month(ed.EffectiveDate)=" + month + " "
                           + "AND year(ed.EffectiveDate)=" + year;

                employeeDepartment = em.ExecuteObject<EmployeeDepartment>(sql, new EmployeeDepartmentMapper());
            }

            return employeeDepartment;
        }


        public EmployeeDepartment GetPreviousDepartment(Guid employeeId, int month, int year)
        {
            EmployeeDepartment employeeDepartment = new EmployeeDepartment();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string sql = "SELECT  TOP 1 ed.*,b.BranchName,d.DepartmentName FROM (EmployeeDepartment ed "
                           + "INNER JOIN Branch b ON ed.BranchId = b.ID) INNER JOIN Department d "
                           + "ON ed.DepartmentId = d.ID "
                           + "WHERE ed.EmployeeId='{" + employeeId + "}' "
                           + "AND month(ed.EffectiveDate) <=" + month + " "
                           + "AND year(ed.EffectiveDate) <=" + year;

                employeeDepartment = em.ExecuteObject<EmployeeDepartment>(sql, new EmployeeDepartmentMapper());
            }

            return employeeDepartment;
        }


        public void Save(EmployeeDepartment employeeDepartment)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    Save(em, null, employeeDepartment);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
         }


        public void Save(IEntityManager em, Transaction tx, EmployeeDepartment employeeDepartment)
        {
            string[] columns = { "ID", "EmployeeId", "EffectiveDate", "BranchId", "DepartmentId" };

            object[] values = { Guid.NewGuid(),employeeDepartment.EmployeeId,employeeDepartment.EffectiveDate.ToShortDateString(),
                                employeeDepartment.BranchId,employeeDepartment.DepartmentId};

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



        public void Update(Guid employeeId, List<EmployeeDepartment> employeeDepartment)
        {
            Transaction tx = null;

            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    tx = em.BeginTransaction();

                    Delete(em, tx, employeeId);
                    foreach (var ed in employeeDepartment)
                    {
                        ed.EmployeeId = employeeId;

                        string[] columns = { "ID", "EmployeeId", "EffectiveDate", "BranchId", "DepartmentId" };

                        object[] values = { Guid.NewGuid(), ed.EmployeeId, ed.EffectiveDate, ed.BranchId, ed.DepartmentId };

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


       



    }
}
