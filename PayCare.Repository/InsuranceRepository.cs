using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityMap;
using PayCare.Model;
using PayCare.Repository.Mapping;

namespace PayCare.Repository
{
    public interface IInsuranceRepository
    {
        string GenerateGradeCode();
        Insurance GetById(Guid id);
        Insurance GetByCode(string code);
        Insurance GetByName(string name);
        List<Insurance> GetAll();
        List<string> GetAllCode();
        List<Insurance> GetActiveInsurance();
        Insurance GetFirst();
        void Save(Insurance insurance);
        void Update(Insurance insurance);
        void Delete(Guid id);
        bool IsInsuranceNameExisted(string name);
        bool IsInsuranceCodeExisted(string code);
        string IsInsuranceUsedByEmployee(Guid insuranceId);
    }


    public class InsuranceRepository : IInsuranceRepository
    {
        private DataSource ds;
        private IInsuranceProgramRepository insuranceProgramRepository;

        private string tableName = "Insurance";

        public InsuranceRepository(DataSource ds)
        {
            this.ds = ds;
            insuranceProgramRepository = EntityContainer.GetType<IInsuranceProgramRepository>();
        }


        public string GenerateGradeCode()
        {
            int counter = 0;

            string code = string.Empty;
            string lastInsuranceCode = string.Empty;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().Select("TOP 1 *").From(tableName).OrderBy("InsuranceCode DESC");

                var insurance = em.ExecuteObject<Insurance>(q.ToSql(), new InsuranceMapper());
                if (insurance != null)
                {
                    lastInsuranceCode = insurance.InsuranceCode;
                    counter = counter + 1;

                    code = (Convert.ToInt32(lastInsuranceCode) + counter).ToString("D3");
                }
                else
                {
                    code = "001";
                }
            }

            return code;
        }


        public Insurance GetById(Guid id)
        {
            Insurance insurance = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From(tableName)
                    .Where("ID")
                    .Equal("{" + id + "}");
                
                insurance = em.ExecuteObject<Insurance>(q.ToSql(), new InsuranceMapper());
            }

            return insurance;
        }


        public Insurance GetByCode(string code)
        {
            Insurance insurance = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From(tableName)
                    .Where("InsuranceCode").Equal(code);

                insurance = em.ExecuteObject<Insurance>(q.ToSql(), new InsuranceMapper());
            }

            return insurance;
        }



        public Insurance GetByName(string name)
        {
            Insurance insurance = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From(tableName)
                    .Where("InsuranceName").Equal(name);

                insurance = em.ExecuteObject<Insurance>(q.ToSql(), new InsuranceMapper());
            }

            return insurance;
        }


        public List<Insurance> GetAll()
        {
            List<Insurance> insurances = new List<Insurance>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From(tableName)
                    .OrderBy("InsuranceCode ASC");
                insurances = em.ExecuteList<Insurance>(q.ToSql(), new InsuranceMapper());
            }

            return insurances;
        }




        public List<string> GetAllCode()
        {
            List<string> list = new List<string>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From(tableName).OrderBy("InsuranceCode");

                using (var rdr = em.ExecuteReader(q.ToSql()))
                {
                    while (rdr.Read())
                    {
                        string branchCode = rdr["InsuranceCode"].ToString();
                        list.Add(branchCode);
                    }
                }
            }

            return list;
        }




        public List<Insurance> GetActiveInsurance()
        {
            List<Insurance> insurances = new List<Insurance>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From(tableName)
                    .Where("IsActive=true")
                    .OrderBy("InsuranceCode ASC");

                insurances = em.ExecuteList<Insurance>(q.ToSql(), new InsuranceMapper());
            }

            return insurances;
        }



        public Insurance GetFirst()
        {
            Insurance insurance = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().Select("TOP 1 *").From(tableName)
                    .OrderBy("InsuranceCode ASC");
                insurance = em.ExecuteObject<Insurance>(q.ToSql(), new InsuranceMapper());
            }

            return insurance;

        }



        public void Save(Insurance insurance)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    string[] columns = { "ID", "InsuranceCode", "InsuranceName", "Notes", "IsActive" };
                    object[] values = { Guid.NewGuid(), insurance.InsuranceCode,insurance.InsuranceName,
                                        insurance.Notes, insurance.IsActive==true?1:0};

                    var q = new Query().Select(columns).From(tableName).Insert(values);

                    em.ExecuteNonQuery(q.ToSql());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void Update(Insurance insurance)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    string[] columns = { "InsuranceCode", "InsuranceName", "Notes", "IsActive" };
                    object[] values = { insurance.InsuranceCode,insurance.InsuranceName,
                                        insurance.Notes,insurance.IsActive==true?1:0};

                    var q = new Query().Select(columns).From(tableName).Update(values)
                        .Where("ID").Equal("{" + insurance.ID + "}");

                    em.ExecuteNonQuery(q.ToSql());

                }
            }
            catch (Exception ex)
            {
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

                    var q = new Query().From(tableName).Delete()
                        .Where("ID").Equal("{" + id + "}");

                    em.ExecuteNonQuery(q.ToSql(),tx);
                    insuranceProgramRepository.Delete(em, tx, id);

                    tx.Commit();
                }
            }
            catch (Exception ex)
            {
                tx.Rollback();
                throw ex;
            }

        }


        public bool IsInsuranceCodeExisted(string code)
        {
            bool isExisted = false;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From("Insurance")
                    .Where("InsuranceCode").Equal(code);

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

        public bool IsInsuranceNameExisted(string name)
        {
            bool isExisted = false;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From("Insurance")
                    .Where("InsuranceName").Equal(name);

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

        public string IsInsuranceUsedByEmployee(Guid insuranceId)
        {
            string employeeCode = string.Empty;
            string employeeName = string.Empty;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string sql = "SELECT e.EmployeeCode, e.EmployeeName "
                           + "FROM EmployeeInsurance ei INNER JOIN Employee e ON ei.EmployeeId = e.ID "
                           + "WHERE ei.InsuranceId='{" + insuranceId + "}' ";

                using (var rdr = em.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        employeeCode = rdr["EmployeeCode"].ToString();
                        employeeName = rdr["EmployeeName"].ToString();
                    }

                }

            }

            if (employeeCode != "" && employeeName != null)
            {
                return employeeCode + " - " + employeeName;
            }
            else
            {
                return employeeName;
            }

        }












    }
}
