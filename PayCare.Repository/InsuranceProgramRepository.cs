
using System;
using System.Collections.Generic;
using PayCare.Model;

using System.Data;
using PayCare.Repository.Mapping;
using EntityMap;
using System.Data.OleDb;
using System.Configuration;


namespace PayCare.Repository
{
    public interface IInsuranceProgramRepository
    {
        InsuranceProgram GetById(Guid id);
        InsuranceProgram GetByName(string name);
        InsuranceProgram GetLast(Guid insuranceId);
        List<InsuranceProgram> GetByInsuranceId(Guid insuranceId);
        List<InsuranceProgram> Search(string value);
        void Save(InsuranceProgram insuranceProgram);
        void Update(InsuranceProgram insuranceProgram);
        void Delete(Guid id);
        void Delete(IEntityManager em, Transaction tx, Guid insuranceId);
        bool IsInsuranceProgramExisted(string program, Guid insuranceId);
        string IsUsedByEmployee(Guid insuranceProgramId);
    }

    public class InsuranceProgramRepository : IInsuranceProgramRepository
    {
        private string tableName = "InsuranceProgram";
        private DataSource ds;

        public InsuranceProgramRepository(DataSource ds)
        {
            this.ds = ds;
        }


       
        public InsuranceProgram GetById(Guid id)
        {
            InsuranceProgram insuranceProgram = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT InsuranceProgram.ID, InsuranceProgram.Program, "
                    + "InsuranceProgram.PercentageByCompany, InsuranceProgram.PercentageByEmployee, InsuranceProgram.PercentageByEmployeeFemale,"
                    + "InsuranceProgram.InsuranceId, Insurance.InsuranceCode, Insurance.InsuranceName "
                    + "FROM InsuranceProgram INNER JOIN Insurance ON InsuranceProgram.InsuranceId = Insurance.ID "
                    + "WHERE " + "InsuranceProgram.ID ='{" + id + "}'";

                insuranceProgram = em.ExecuteObject<InsuranceProgram>(sql, new InsuranceProgramMapper());
            }

            return insuranceProgram;
        }

        public InsuranceProgram GetByName(string name)
        {
            InsuranceProgram insuranceProgram = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT InsuranceProgram.ID, InsuranceProgram.Program, "
                    + "InsuranceProgram.PercentageByCompany, InsuranceProgram.PercentageByEmployee, InsuranceProgram.PercentageByEmployeeFemale,"
                    + "InsuranceProgram.InsuranceId, Insurance.InsuranceCode, Insurance.InsuranceName "
                    + "FROM InsuranceProgram INNER JOIN Insurance ON InsuranceProgram.InsuranceId = Insurance.ID "
                    + "WHERE InsuranceProgram.Program = '" + name + "'";

                insuranceProgram = em.ExecuteObject<InsuranceProgram>(sql, new InsuranceProgramMapper());
            }

            return insuranceProgram;
        }



        public InsuranceProgram GetLast(Guid insuranceId)
        {
            InsuranceProgram insuranceProgram = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT TOP 1 InsuranceProgram.ID, InsuranceProgram.Program, "
                    + "InsuranceProgram.PercentageByCompany, InsuranceProgram.PercentageByEmployee, InsuranceProgram.PercentageByEmployeeFemale,"
                    + "InsuranceProgram.InsuranceId, Insurance.InsuranceCode, Insurance.InsuranceName "
                    + "FROM InsuranceProgram INNER JOIN Insurance ON InsuranceProgram.InsuranceId = Insurance.ID "
                    + "WHERE InsuranceProgram.InsuranceId = '{" + insuranceId + "}'";
            
                insuranceProgram = em.ExecuteObject<InsuranceProgram>(sql, new InsuranceProgramMapper());
            }

            return insuranceProgram;
        }


        public List<InsuranceProgram> GetByInsuranceId(Guid insuranceId)
        {
            List<InsuranceProgram> insuranceProgram = new List<InsuranceProgram>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT InsuranceProgram.ID, InsuranceProgram.Program, "
                    + "InsuranceProgram.PercentageByCompany, InsuranceProgram.PercentageByEmployee, InsuranceProgram.PercentageByEmployeeFemale,"
                    + "InsuranceProgram.InsuranceId, Insurance.InsuranceCode, Insurance.InsuranceName "
                    + "FROM InsuranceProgram INNER JOIN Insurance ON InsuranceProgram.InsuranceId = Insurance.ID "
                    + "WHERE InsuranceProgram.InsuranceId = '{" + insuranceId + "}' "
                    + "ORDER BY InsuranceProgram.Program ASC";

                insuranceProgram = em.ExecuteList<InsuranceProgram>(sql, new InsuranceProgramMapper());
            }

            return insuranceProgram;

        }




        public List<InsuranceProgram> Search(string value)
        {
            List<InsuranceProgram> insuranceProgram = new List<InsuranceProgram>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT InsuranceProgram.ID, InsuranceProgram.Program, "
                    + "InsuranceProgram.PercentageByCompany, InsuranceProgram.PercentageByEmployee, InsuranceProgram.PercentageByEmployeeFemale,"
                    + "InsuranceProgram.InsuranceId, Insurance.InsuranceCode, Insurance.InsuranceName "
                    + "FROM InsuranceProgram INNER JOIN Insurance ON InsuranceProgram.InsuranceId = Insurance.ID "
                    + "WHERE "
                    + "InsuranceProgram.Program like '%" + value + "%' "
                    + "OR Insurance.InsuranceCode like '%" + value + "%' "
                    + "OR Insurance.InsuranceName like '%" + value + "%' "
                    + "ORDER BY InsuranceProgram.Program ASC";
                
                insuranceProgram = em.ExecuteList<InsuranceProgram>(sql, new InsuranceProgramMapper());


            }

            return insuranceProgram;
        }


        public void Save(InsuranceProgram insuranceProgram)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {

                    string[] columns = {"ID", "Program", "PercentageByCompany", 
                                           "PercentageByEmployee", "PercentageByEmployeeFemale",
                                           "InsuranceId"};

                    object[] values = {Guid.NewGuid(),insuranceProgram.Program, 
                                          insuranceProgram.PercentageByCompany, 
                                          insuranceProgram.PercentageByEmployee, insuranceProgram.PercentageByEmployeeFemale,
                                      insuranceProgram.InsuranceId};

                    var q = new Query().Select(columns).From(tableName).Insert(values);

                    em.ExecuteNonQuery(q.ToSql());

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Update(InsuranceProgram insuranceProgram)
        {
            try
            {

                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    string[] columns = {"Program", "PercentageByCompany", 
                                           "PercentageByEmployee", "PercentageByEmployeeFemale",
                                           "InsuranceId"};

                    object[] values = {insuranceProgram.Program, insuranceProgram.PercentageByCompany, 
                                          insuranceProgram.PercentageByEmployee, insuranceProgram.PercentageByEmployeeFemale,
                                      insuranceProgram.InsuranceId};

                    var q = new Query().Select(columns).From(tableName).Update(values).Where("ID").Equal("{" + insuranceProgram.ID + "}");

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
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    var q = new Query().From(tableName).Delete().Where("ID").Equal("{" + id + "}");

                    em.ExecuteNonQuery(q.ToSql());
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void Delete(IEntityManager em,Transaction tx,Guid insuranceId)
        {
            var q = new Query().From(tableName).Delete()
                .Where("InsuranceId").Equal("{" + insuranceId + "}");

            em.ExecuteNonQuery(q.ToSql(),tx);
 
        }




        

        public bool IsInsuranceProgramExisted(string program, Guid insuranceId)
        {
            bool isExisted = false;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From("InsuranceProgram").Where("Program").Equal(program)
                   .And("InsuranceId").Equal(insuranceId);

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

        public string IsUsedByEmployee(Guid insuranceProgramId)
        {
            string employeeCode = string.Empty;
            string employeeName = string.Empty;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string sql = "SELECT e.EmployeeCode, e.EmployeeName "
                           + "FROM EmployeeInsurance ei INNER JOIN Employee e ON ei.EmployeeId = e.ID "
                           + "WHERE ei.InsuranceProgramId='{" + insuranceProgramId + "}' ";

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
