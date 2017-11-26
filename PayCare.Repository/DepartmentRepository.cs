
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
    public interface IDepartmentRepository
    {
        string GenerateDepartmentCode();
        Department GetById(Guid id);
        Department GetFirst();
        List<Department> GetByBranchId(Guid branchId);
        Department GetByName(string name);
        Department GetByCode(string code);
        List<Department> GetAll();
        List<string> GetAllCode();
        void Save(Department department);
        void Update(Department department);
        void Delete(Guid id);
        List<Department> GetActiveDepartment();
        bool IsDepartmentNameExisted(string departmentName);
        bool IsDepartmentCodeExisted(string departmentCode);
        string IsDepartmentUsedByEmployee(Guid departmentId);
    }

    public class DepartmentRepository : IDepartmentRepository
    {
        private string tableName = "Department";
        private DataSource ds;

        public DepartmentRepository(DataSource ds)
        {
            this.ds = ds;
        }


        public string GenerateDepartmentCode()
        {
            int counter = 0;

            string code = string.Empty;
            string lastDepartmentCode = string.Empty;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {

                var sql = "SELECT TOP 1 d.*,b.BranchCode, b.BranchName "
                         + "FROM (Department d INNER JOIN Branch b ON d.BranchId = b.ID) "
                         + "ORDER BY DepartmentCode DESC";

                var department = em.ExecuteObject<Department>(sql, new DepartmentMapper());
                if (department != null)
                {
                    lastDepartmentCode = department.DepartmentCode;
                    counter = counter + 1;

                    code = (Convert.ToInt32(lastDepartmentCode) + counter).ToString("D3");
                }
                else
                {
                    code = "001";
                }
            }

            return code;
        }



        public Department GetById(Guid id)
        {
            Department department = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql="SELECT d.*,b.BranchCode, b.BranchName "
                      + "FROM (Department d INNER JOIN Branch b ON d.BranchId = b.ID) "
                      + "WHERE d.ID='{" + id + "}'";

                department = em.ExecuteObject<Department>(sql, new DepartmentMapper());
            }

            return department;
        }


        public Department GetFirst()
        {
            Department department = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {

                var sql = "SELECT TOP 1 d.*,b.BranchCode, b.BranchName "
                        + "FROM (Department d INNER JOIN Branch b ON d.BranchId = b.ID) "
                        + "ORDER BY d.DepartmentCode ASC";

                department = em.ExecuteObject<Department>(sql, new DepartmentMapper());
            }

            return department;
        }


        public List<Department> GetByBranchId(Guid branchId)
        {
            List<Department> departments = new List<Department>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT d.*,b.BranchCode, b.BranchName "
                        + "FROM (Department d INNER JOIN Branch b ON d.BranchId = b.ID) "
                        + "WHERE d.IsActive=true AND d.BranchId='{" + branchId + "}'";
        
                departments = em.ExecuteList<Department>(sql, new DepartmentMapper());
            }

            return departments;
        }


        public Department GetByName(string name)
        {
            Department department = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                  var sql = "SELECT d.*,b.BranchCode, b.BranchName "
                          + "FROM (Department d INNER JOIN Branch b ON d.BranchId = b.ID) "
                          + "WHERE d.DepartmentName='" + name + "'";
                            

                department = em.ExecuteObject<Department>(sql, new DepartmentMapper());
            }

            return department;
        }


        public Department GetByCode(string code)
        {
            Department department = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT d.*,b.BranchCode, b.BranchName "
                        + "FROM (Department d INNER JOIN Branch b ON d.BranchId = b.ID) "
                        + "WHERE DepartmentCode='" + code + "'";
                   
                department = em.ExecuteObject<Department>(sql, new DepartmentMapper());
            }

            return department;
        }



        public List<Department> GetAll()
        {
            List<Department> departments = new List<Department>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT d.*,b.BranchCode, b.BranchName "
                        + "FROM (Department d INNER JOIN Branch b ON d.BranchId = b.ID) "
                        + "ORDER BY d.DepartmentCode ASC";

                departments = em.ExecuteList<Department>(sql, new DepartmentMapper());
            }

            return departments;
        }


        public List<string> GetAllCode()
        {
            List<string> list = new List<string>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From(tableName).OrderBy("DepartmentCode");
                
                using (var rdr = em.ExecuteReader(q.ToSql()))
                {
                    while (rdr.Read())
                    {
                        string departmentCode=rdr["DepartmentCode"].ToString();
                        list.Add(departmentCode);
                    }
                }
            }
            return list;
        }
        
              

      




        public void Save(Department department)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    string[] fields = { "ID", "BranchId", "DepartmentCode", "DepartmentName", "DepartmentHead", 
                                          "IsActive"};

                    object[] values = { Guid.NewGuid(), department.BranchId, department.DepartmentCode, department.DepartmentName, department.DepartmentHead, 
                                          department.IsActive==true?1:0};

                    
                    Query q = new Query().Select(fields).From(tableName).Insert(values);

                    em.ExecuteNonQuery(q.ToSql());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public void Update(Department department)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {

                    string[] fields = { "BranchId", "DepartmentCode", "DepartmentName", "DepartmentHead", 
                                          "IsActive"};

                    object[] values = { department.BranchId, department.DepartmentCode, department.DepartmentName, department.DepartmentHead, 
                                          department.IsActive==true?1:0};

                    Query q = new Query().Select(fields).From(tableName).Update(values)
                        .Where("ID").Equal("{" + department.ID + "}");

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
                    Query q = new Query().From(tableName).Delete().Where("ID").Equal("{" + id + "}");
                    em.ExecuteNonQuery(q.ToSql());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


       
        public bool IsDepartmentNameExisted(string departmentName)
        {
            bool isExisted = false;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From("Department").Where("DepartmentName").Equal(departmentName);

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


        public bool IsDepartmentCodeExisted(string departmentCode)
        {
            bool isExisted = false;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From("Department").Where("DepartmentCode").Equal(departmentCode);

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


        public List<Department> GetActiveDepartment()
        {
            List<Department> departments = new List<Department>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT d.*,b.BranchCode, b.BranchName "
                        + "FROM (Department d INNER JOIN Branch b ON d.BranchId = b.ID) "
                        + "WHERE d.IsActive=true";
          
                departments = em.ExecuteList<Department>(sql, new DepartmentMapper());
            }

            return departments;
        }


        public string IsDepartmentUsedByEmployee(Guid departmentId)
        {
            string employeeCode = string.Empty;
            string employeeName = string.Empty;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string sql = "SELECT e.EmployeeCode, e.EmployeeName "
                           + "FROM EmployeeDepartment ed INNER JOIN Employee e ON ed.EmployeeId = e.ID "
                           + "WHERE ed.DepartmentId='{" + departmentId + "}' ";

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
