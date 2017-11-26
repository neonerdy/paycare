using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityMap;
using PayCare.Model;
using PayCare.Repository.Mapping;

namespace PayCare.Repository
{

    public interface IGradeRepository
    {
        string GenerateGradeCode();
        Grade GetById(Guid id);
        Grade GetByCode(string code);
        Grade GetByName(string name);
        List<Grade> GetAll();
        List<string> GetAllCode();
        List<Grade> GetActiveGrade();
        Grade GetFirst();
        void Save(Grade grade);
        void Update(Grade grade);
        void Delete(Guid id);
        string IsGradeUsedByEmployee(Guid gradelId);
    }

    public class GradeRepository : IGradeRepository
    {

        private DataSource ds;
        private string tableName = "Grade";

        public GradeRepository(DataSource ds)
        {
            this.ds = ds;
        }
        
        public string GenerateGradeCode()
        {
            int counter = 0;

            string code = string.Empty;
            string lastGradeCode = string.Empty;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().Select("TOP 1 *").From(tableName).OrderBy("GradeCode DESC");

                var grade = em.ExecuteObject<Grade>(q.ToSql(), new GradeMapper());
                if (grade != null)
                {
                    lastGradeCode = grade.GradeCode;
                    counter = counter + 1;

                    code = (Convert.ToInt32(lastGradeCode) + counter).ToString("D3");
                }
                else
                {
                    code = "001";
                }
            }

            return code;
        }


        public Grade GetById(Guid id)
        {
            Grade grade = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From(tableName).Where("ID").Equal("{" + id + "}");
                grade = em.ExecuteObject<Grade>(q.ToSql(), new GradeMapper());
            }

            return grade;
        }



        public Grade GetByCode(string code)
        {
            Grade grade = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From(tableName).Where("GradeCode").Equal(code);
                grade = em.ExecuteObject<Grade>(q.ToSql(), new GradeMapper());
            }

            return grade;
        }



        public Grade GetByName(string name)
        {
            Grade grade = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From(tableName).Where("GradeName").Equal(name);
                grade = em.ExecuteObject<Grade>(q.ToSql(), new GradeMapper());
            }

            return grade;
        }




        public List<Grade> GetAll()
        {
            List<Grade> grades = new List<Grade>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From(tableName);
                grades = em.ExecuteList<Grade>(q.ToSql(), new GradeMapper());
            }

            return grades;
        }


        public List<string> GetAllCode()
        {
            List<string> list = new List<string>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From(tableName).OrderBy("GradeCode");

                using (var rdr = em.ExecuteReader(q.ToSql()))
                {
                    while (rdr.Read())
                    {
                        string gradeCode = rdr["GradeCode"].ToString();
                        list.Add(gradeCode);
                    }
                }
            }
            return list;
        }




        public List<Grade> GetActiveGrade()
        {
            List<Grade> grades = new List<Grade>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From(tableName).Where("IsActive=true");
                grades = em.ExecuteList<Grade>(q.ToSql(), new GradeMapper());
            }

            return grades;
        }
        


        public Grade GetFirst()
        {
            Grade grade = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().Select("TOP 1 *").From(tableName).OrderBy("GradeCode ASC");
                grade = em.ExecuteObject<Grade>(q.ToSql(), new GradeMapper());
            }

            return grade;

        }



        public void Save(Grade grade)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    string[] columns = { "ID", "GradeLevel", "GradeCode", "GradeName", "Notes", "IsActive" };
                    object[] values = { Guid.NewGuid(), grade.GradeLevel, grade.GradeCode,grade.GradeName,
                                         grade.Notes,grade.IsActive==true?1:0};

                    var q = new Query().Select(columns).From(tableName).Insert(values);

                    em.ExecuteNonQuery(q.ToSql());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void Update(Grade grade)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    string[] columns = { "GradeCode", "GradeLevel", "GradeName", "Notes", "IsActive" };
                    object[] values = { grade.GradeCode,grade.GradeLevel, grade.GradeName,
                                        grade.Notes,grade.IsActive==true?1:0};

                    var q = new Query().Select(columns).From(tableName).Update(values)
                        .Where("ID").Equal("{" + grade.ID + "}");

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

        public string IsGradeUsedByEmployee(Guid gradeId)
        {
            string employeeCode = string.Empty;
            string employeeName = string.Empty;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string sql = "SELECT e.EmployeeCode, e.EmployeeName "
                           + "FROM EmployeeGrade eg INNER JOIN Employee e ON eg.EmployeeId = e.ID "
                           + "WHERE eg.GradeId='{" + gradeId + "}' ";

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
