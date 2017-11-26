using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityMap;
using PayCare.Model;
using PayCare.Repository.Mapping;

namespace PayCare.Repository
{

    public interface IEmployeeSalaryRepository
    {
        List<EmployeeSalary> GetByEmployeeId(Guid employeeId);
        void Save(EmployeeSalary employeeSalary);
        void Save(IEntityManager em, Transaction tx, EmployeeSalary employeeSalary);
        void Update(Guid employeeId, List<EmployeeSalary> employeeSalary);
        void Delete(IEntityManager em, Transaction tx, Guid employeeId);
        EmployeeSalary GetLastSalary(Guid employeeId);
        EmployeeSalary GetCurrentSalary(Guid employeeId, int month, int year);
        EmployeeSalary GetPreviousSalary(Guid employeeId, int month, int year);
        
    }



    public class EmployeeSalaryRepository : IEmployeeSalaryRepository
    {

        private string tableName = "EmployeeSalary";
        private DataSource ds;

        public EmployeeSalaryRepository(DataSource ds)
        {
            this.ds = ds;
        }
                     

        public List<EmployeeSalary> GetByEmployeeId(Guid employeeId)
        {
            List<EmployeeSalary> employeeSalary = new List<EmployeeSalary>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                Query q = new Query().From(tableName).Where("EmployeeId").Equal("{" + employeeId + "}")
                    .OrderBy("EffectiveDate DESC");
             
                employeeSalary = em.ExecuteList<EmployeeSalary>(q.ToSql(), new EmployeeSalaryMapper());
            }

            return employeeSalary;
        }



        public void Save(EmployeeSalary employeeSalary)
        {
            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                Save(em, null, employeeSalary);
            }
        }



        public void Save(IEntityManager em, Transaction tx, EmployeeSalary employeeSalary)
        {
            string[] columns = { "ID", "EmployeeId", "EffectiveDate", "MainSalary", "OccupationAllowancePerMonth", "FixedAllowancePerMonth",
                               "HealthAllowancePerMonth","CommunicationAllowancePerMonth","SupervisionAllowancePerMonth","OtherAllowance",
                               "FuelAllowancePerDays","VehicleAllowancePerDays","LunchAllowancePerDays","TransportationAllowancePerDays",
                               "JamsostekAmount","PersonalDebt","OtherFee"};

            object[] values = { Guid.NewGuid(),employeeSalary.EmployeeId,employeeSalary.EffectiveDate.ToShortDateString(),employeeSalary.MainSalary,
                                employeeSalary.OccupationAllowancePerMonth,employeeSalary.FixedAllowancePerMonth,employeeSalary.HealthAllowancePerMonth,
                                employeeSalary.CommunicationAllowancePerMonth,employeeSalary.SupervisionAllowancePerMonth,employeeSalary.OtherAllowance,
                                employeeSalary.FuelAllowancePerDays,employeeSalary.VehicleAllowancePerDays,employeeSalary.LunchAllowancePerDays,
                                employeeSalary.TransportationAllowancePerDays,employeeSalary.JamsostekAmount,employeeSalary.PersonalDebt,employeeSalary.OtherFee};

            var q = new Query().Select(columns).From(tableName).Insert(values);

            if (tx != null)
            {
                em.ExecuteNonQuery(q.ToSql(), tx);
            }
            else
            {
                em.ExecuteNonQuery(q.ToSql());
            }
        }
        



        public void Update(Guid employeeId, List<EmployeeSalary> employeeSalary)
        {
            Transaction tx = null;

            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    tx = em.BeginTransaction();

                    Delete(em, tx, employeeId);
                    
                    foreach (var salary in employeeSalary)
                    {
                        salary.EmployeeId = employeeId;

                        string[] columns = { "ID", "EmployeeId", "EffectiveDate", "MainSalary", "OccupationAllowancePerMonth", "FixedAllowancePerMonth",
                               "HealthAllowancePerMonth","CommunicationAllowancePerMonth","SupervisionAllowancePerMonth","OtherAllowance",
                               "FuelAllowancePerDays","VehicleAllowancePerDays","LunchAllowancePerDays","TransportationAllowancePerDays",
                               "JamsostekAmount","PersonalDebt","OtherFee"};

                        object[] values = { Guid.NewGuid(),salary.EmployeeId,salary.EffectiveDate.ToShortDateString(),salary.MainSalary,
                                salary.OccupationAllowancePerMonth,salary.FixedAllowancePerMonth,salary.HealthAllowancePerMonth,
                                salary.CommunicationAllowancePerMonth,salary.SupervisionAllowancePerMonth,salary.OtherAllowance,
                                salary.FuelAllowancePerDays,salary.VehicleAllowancePerDays,salary.LunchAllowancePerDays,
                                salary.TransportationAllowancePerDays,salary.JamsostekAmount,salary.PersonalDebt,salary.OtherFee};

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



        public EmployeeSalary GetLastSalary(Guid employeeId)
        {
            EmployeeSalary employeeSalary = new EmployeeSalary();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                Query q = new Query().Select("TOP 1 * ").From(tableName)
                    .Where("EmployeeId").Equal("{" + employeeId + "}")
                    .OrderBy("EffectiveDate DESC");

                employeeSalary = em.ExecuteObject<EmployeeSalary>(q.ToSql(), new EmployeeSalaryMapper());

            }

            return employeeSalary;
        }

        

        public EmployeeSalary GetCurrentSalary(Guid employeeId, int month, int year)
        {
            EmployeeSalary employeeSalary = new EmployeeSalary();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                Query q = new Query().From(tableName)
                    .Where("EmployeeId").Equal("{" + employeeId + "}")
                    .And("month(EffectiveDate)").Equal(month)
                    .And("year(EffectiveDate)").Equal(year)
                    .OrderBy("EffectiveDate DESC");

                employeeSalary = em.ExecuteObject<EmployeeSalary>(q.ToSql(), new EmployeeSalaryMapper());
            }

            return employeeSalary;
        }

        public EmployeeSalary GetPreviousSalary(Guid employeeId, int month, int year)
        {
            EmployeeSalary employeeSalary = new EmployeeSalary();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                Query q = new Query().Select("TOP 1 * ").From(tableName)
                    .Where("EmployeeId").Equal("{" + employeeId + "}")
                    .And("month(EffectiveDate)").LessEqualThan(month)
                    .And("year(EffectiveDate)").LessEqualThan(year)
                    .OrderBy("EffectiveDate DESC");

                employeeSalary = em.ExecuteObject<EmployeeSalary>(q.ToSql(), new EmployeeSalaryMapper());
            }

            return employeeSalary;
        }


    }
}
