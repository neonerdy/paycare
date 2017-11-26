using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayCare.Model;
using EntityMap;
using PayCare.Repository.Mapping;

namespace PayCare.Repository
{
    public interface IOccupationRepository
    {
        Occupation GetById(Guid id);
        Occupation GetByCode(string code);
        Occupation GetByName(string name);
        List<Occupation> GetAll();
        List<string> GetAllCode();
        List<Occupation> GetActiveOccupation();
        Occupation GetFirst();
        void Save(Occupation occupation);
        void Update(Occupation occupation);
        void Delete(Guid id);
        string IsOccupationUsedByEmployee(Guid occupationId);
    }

    public class OccupationRepository : IOccupationRepository
    {

        private DataSource ds;
        private string tableName = "Occupation";

        public OccupationRepository(DataSource ds)
        {
            this.ds = ds;
        }

        #region IOccupationRepository Members

        public Occupation GetById(Guid id)
        {
            Occupation occupation=null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From(tableName).Where("ID").Equal("{" + id + "}");
                occupation = em.ExecuteObject<Occupation>(q.ToSql(), new OccupationMapper());
            }

            return occupation;
        }
        

        public Occupation GetByCode(string code)
        {
            Occupation occupation = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From(tableName).Where("OccupationCode").Equal(code);
                occupation = em.ExecuteObject<Occupation>(q.ToSql(), new OccupationMapper());
            }

            return occupation;
        }



        public Occupation GetByName(string name)
        {
            Occupation occupation = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From(tableName).Where("OccupationName").Equal(name);
                occupation = em.ExecuteObject<Occupation>(q.ToSql(), new OccupationMapper());
            }

            return occupation;
        }



        
        public List<Occupation> GetAll()
        {
            List<Occupation> occupations = new List<Occupation>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From(tableName);
                occupations = em.ExecuteList<Occupation>(q.ToSql(), new OccupationMapper());
            }

            return occupations;
        }



        public List<string> GetAllCode()
        {
            List<string> list = new List<string>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From(tableName).OrderBy("OccupationCode");

                using (var rdr = em.ExecuteReader(q.ToSql()))
                {
                    while (rdr.Read())
                    {
                        string occupationCode = rdr["OccupationCode"].ToString();
                        list.Add(occupationCode);
                    }
                }
            }
            return list;
        }






        public List<Occupation> GetActiveOccupation()
        {
            List<Occupation> occupations = new List<Occupation>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From(tableName).Where("IsActive=true");
                occupations = em.ExecuteList<Occupation>(q.ToSql(), new OccupationMapper());
            }

            return occupations;
        }



        public Occupation GetFirst()
        {
            Occupation occupation = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().Select("TOP 1 *").From(tableName).OrderBy("OccupationCode ASC");
                occupation = em.ExecuteObject<Occupation>(q.ToSql(), new OccupationMapper());
            }

            return occupation;
        
        }


       


        public void Save(Occupation occupation)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    string[] columns = { "ID","OccupationCode", "OccupationName", "HealthAllowance", "VehicleAllowance", "Notes", "IsActive" };
                    object[] values = { Guid.NewGuid(),occupation.OccupationCode,occupation.OccupationName,occupation.HealthAllowance,
                                        occupation.VehicleAllowance,occupation.Notes,occupation.IsActive==true?1:0};

                    var q = new Query().Select(columns).From(tableName).Insert(values);

                    em.ExecuteNonQuery(q.ToSql());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void Update(Occupation occupation)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    string[] columns = { "OccupationCode", "OccupationName","HealthAllowance","VehicleAllowance","Notes", "IsActive" };
                    object[] values = { occupation.OccupationCode,occupation.OccupationName,occupation.HealthAllowance,
                                        occupation.VehicleAllowance,occupation.Notes,occupation.IsActive==true?1:0};

                    var q = new Query().Select(columns).From(tableName).Update(values)
                        .Where("ID").Equal("{" + occupation.ID + "}");

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
                    var q = new Query().From(tableName).Delete()
                        .Where("ID").Equal("{" + id + "}");

                    em.ExecuteNonQuery(q.ToSql());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        
        }

        public string IsOccupationUsedByEmployee(Guid occupationId)
        {
            string employeeCode = string.Empty;
            string employeeName = string.Empty;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string sql = "SELECT e.EmployeeCode, e.EmployeeName "
                           + "FROM EmployeeOccupation eo INNER JOIN Employee e ON eo.EmployeeId = e.ID "
                           + "WHERE eo.OccupationId='{" + occupationId + "}' ";

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





        #endregion
    }
}
