using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityMap;
using PayCare.Model;
using PayCare.Repository.Mapping;

namespace PayCare.Repository
{


    public interface IEmployeeInsuranceRepository
    {
        List<EmployeeInsurance> GetByEmployeeId(Guid employeeId);
        void Save(EmployeeInsurance employeeInsurance);
        void Save(IEntityManager em, Transaction tx, EmployeeInsurance employeeInsurance);
        void Update(Guid employeeId, List<EmployeeInsurance> employeeInsurance);
        void Delete(IEntityManager em, Transaction tx, Guid employeeId);
        EmployeeInsurance GetCurrentInsurance(Guid employeeId, int month, int year);
        EmployeeInsurance GetPreviousInsurance(Guid employeeId, int month, int year);
        InsuranceValue GetCurrentValue(Guid employeeId, string insuranceName, int month, int year);
        InsuranceValue GetPreviousValue(Guid employeeId, string insuranceName, int month, int year);
         
    }

    public class EmployeeInsuranceRepository : IEmployeeInsuranceRepository
    {
        private DataSource ds;
        private string tableName = "EmployeeInsurance";

        public EmployeeInsuranceRepository(DataSource ds)
        {
            this.ds = ds;
        }

        public List<EmployeeInsurance> GetByEmployeeId(Guid employeeId)
        {
            List<EmployeeInsurance> employeeInsurances = new List<EmployeeInsurance>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string sql = "SELECT EmployeeInsurance.ID, EmployeeInsurance.EmployeeId, EmployeeInsurance.InsuranceId,"
                        + "Insurance.InsuranceName, EmployeeInsurance.InsuranceProgramId, InsuranceProgram.Program, "
                        + "EmployeeInsurance.EffectiveDate, EmployeeInsurance.EndDate, EmployeeInsurance.InsuranceNumber "
                        + "FROM (EmployeeInsurance INNER JOIN Insurance ON EmployeeInsurance.InsuranceId = Insurance.ID) "
                        + "INNER JOIN InsuranceProgram ON EmployeeInsurance.InsuranceProgramId = InsuranceProgram.ID "
                        + "WHERE EmployeeInsurance.EmployeeId= '{" + employeeId + "}' "
                        + "ORDER BY EmployeeInsurance.EffectiveDate DESC";

                employeeInsurances = em.ExecuteList<EmployeeInsurance>(sql, new EmployeeInsuranceMapper());
            }

            return employeeInsurances;
        }



        public void Save(EmployeeInsurance employeeInsurance)
        {
            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                Save(em, null, employeeInsurance);
            }
        }





        public void Save(IEntityManager em, Transaction tx, EmployeeInsurance employeeInsurance)
        {
            string[] columns = { "ID", "EmployeeId", "InsuranceId", "InsuranceProgramId", "EffectiveDate", "EndDate", "InsuranceNumber" };

            object[] values = { Guid.NewGuid(),employeeInsurance.EmployeeId,employeeInsurance.InsuranceId,employeeInsurance.InsuranceProgramId,
                                employeeInsurance.EffectiveDate.ToShortDateString(),employeeInsurance.EndDate.ToShortDateString(),
                                employeeInsurance.InsuranceNumber};

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

        

        public void Update(Guid employeeId, List<EmployeeInsurance> employeeInsurances)
        {
            Transaction tx = null;

            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    tx = em.BeginTransaction();

                    Delete(em, tx, employeeId);
                    
                    foreach (var insurance in employeeInsurances)
                    {
                        insurance.EmployeeId = employeeId;

                        string[] columns = { "ID", "EmployeeId", "InsuranceId", "InsuranceProgramId", "EffectiveDate", "EndDate", "InsuranceNumber" };

                        object[] values = { Guid.NewGuid(),insurance.EmployeeId,insurance.InsuranceId,insurance.InsuranceProgramId,
                                insurance.EffectiveDate.ToShortDateString(),insurance.EndDate.ToShortDateString(),
                                insurance.InsuranceNumber};

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



        public EmployeeInsurance GetCurrentInsurance(Guid employeeId, int month, int year)
        {
            EmployeeInsurance employeeInsurance = new EmployeeInsurance();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                Query q = new Query().From(tableName)
                    .Where("EmployeeId").Equal("{" + employeeId + "}")
                    .And("month(EffectiveDate)").Equal(month)
                    .And("year(EffectiveDate)").Equal(year)
                    .OrderBy("EffectiveDate DESC");

                employeeInsurance = em.ExecuteObject<EmployeeInsurance>(q.ToSql(), new EmployeeInsuranceMapper());
            }

            return employeeInsurance;
        }

      
        public EmployeeInsurance GetPreviousInsurance(Guid employeeId, int month, int year)
        {
            EmployeeInsurance employeeInsurance = new EmployeeInsurance();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                Query q = new Query().Select("TOP 1 * ").From(tableName)
                    .Where("EmployeeId").Equal("{" + employeeId + "}")
                    .And("month(EffectiveDate)").LessEqualThan(month)
                    .And("year(EffectiveDate)").LessEqualThan(year)
                    .OrderBy("EffectiveDate DESC");

                employeeInsurance = em.ExecuteObject<EmployeeInsurance>(q.ToSql(), new EmployeeInsuranceMapper());
            }

            return employeeInsurance;
        }


        public InsuranceValue GetCurrentValue(Guid employeeId, string insuranceName, int month, int year)
        {
            InsuranceValue insuranceValue = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string sql = "SELECT Sum(InsuranceProgram.PercentageByCompany) AS ByCompany, "
                           + "Sum(InsuranceProgram.PercentageByEmployee) AS ByEmployee, "
                           + "Sum(InsuranceProgram.PercentageByEmployeeFemale) AS ByEmployeeFemale "
                           + "FROM (EmployeeInsurance INNER JOIN InsuranceProgram ON EmployeeInsurance.InsuranceProgramId = InsuranceProgram.ID) "
                           + "INNER JOIN Insurance ON InsuranceProgram.InsuranceId = Insurance.ID "
                           + "GROUP BY EmployeeInsurance.EmployeeId, Month([EffectiveDate]), Year([effectiveDate]), Insurance.InsuranceName "
                           + "HAVING (((EmployeeInsurance.EmployeeId)='{" + employeeId + "}') "
                           + "AND Month([EffectiveDate])=" + month + " "
                           + "AND Year([effectiveDate])=" + year + " "
                           + "AND Insurance.InsuranceName='" + insuranceName + "')";

                insuranceValue = em.ExecuteObject<InsuranceValue>(sql, new InsuranceValueMapper());
            }

            return insuranceValue;
        }

        public InsuranceValue GetPreviousValue(Guid employeeId, string insuranceName, int month, int year)
        {
            InsuranceValue insuranceValue = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string sql = "SELECT Sum(InsuranceProgram.PercentageByCompany) AS ByCompany, " 
                           + "Sum(InsuranceProgram.PercentageByEmployee) AS ByEmployee, "
                           + "Sum(InsuranceProgram.PercentageByEmployeeFemale) AS ByEmployeeFemale "
                           + "FROM (EmployeeInsurance INNER JOIN InsuranceProgram ON EmployeeInsurance.InsuranceProgramId = InsuranceProgram.ID) "
                           + "INNER JOIN Insurance ON InsuranceProgram.InsuranceId = Insurance.ID "
                           + "GROUP BY EmployeeInsurance.EmployeeId, Month([EffectiveDate]), Year([effectiveDate]), Insurance.InsuranceName "
                           + "HAVING (((EmployeeInsurance.EmployeeId)='{" + employeeId + "}') "
                           + "AND Month([EffectiveDate])<=" + month + " "
                           + "AND Year([effectiveDate])<=" + year + " "
                           + "AND Insurance.InsuranceName='" + insuranceName + "')";

                insuranceValue = em.ExecuteObject<InsuranceValue>(sql, new InsuranceValueMapper());
            }

            return insuranceValue;
        }




       




    }
}
