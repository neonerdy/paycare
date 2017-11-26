
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
    public interface IBranchRepository
    {
        int GetEmployeeCounter(string branchName);
        void UpdateEmployeeCounter(string branchName, IEntityManager em, Transaction tx);
        string GenerateBranchCode();
        Branch GetById(Guid id);
        Branch GetByCode(string code);
        Branch GetFirst();
        Branch GetByName(string name);
        List<Branch> GetAll();
        List<string> GetAllCode();
        List<Branch> Search(string value);
        void Save(Branch branch);
        void Update(Branch branch);
        void Delete(Guid id);
        List<Branch> GetActiveBranch();
        bool IsBranchNameExisted(string branchName);
        bool IsBranchCodeExisted(string branchCode);
        string IsBranchUsedByDepartment(Guid branchId);
    }


    public class BranchRepository : IBranchRepository
    {
        private string tableName = "Branch";
        private DataSource ds;

        public BranchRepository(DataSource ds)
        {
            this.ds = ds;
        }



        public int GetEmployeeCounter(string branchName)
        {
            int employeeCounter=0;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From(tableName).Where("BranchName").Equal(branchName);
                using(var rdr=em.ExecuteReader(q.ToSql()))
                {
                    if (rdr.Read())
                    {
                        employeeCounter = rdr["EmployeeCounter"] is DBNull ? 0 : (int)rdr["EmployeeCounter"];
                    }
                }
            }

            return employeeCounter;
        }



        public void UpdateEmployeeCounter(string branchName, IEntityManager em, Transaction tx)
        {
            try
            {
                int employeeCounter = GetEmployeeCounter(branchName);
                
                string[] columns = { "EmployeeCounter" };
                object[] values = { employeeCounter + 1 };

                var q = new Query().Select(columns).From(tableName).Update(values)
                    .Where("BranchName").Equal(branchName);

                em.ExecuteNonQuery(q.ToSql(), tx);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public string GenerateBranchCode()
        {
            int counter = 0;

            string code = string.Empty;
            string lastBranchCode = string.Empty;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().Select("TOP 1 *").From(tableName).OrderBy("BranchCode DESC");

                var branch = em.ExecuteObject<Branch>(q.ToSql(), new BranchMapper());
                if (branch != null)
                {
                    lastBranchCode = branch.BranchCode;
                    counter = counter + 1;

                    code = (Convert.ToInt32(lastBranchCode) + counter).ToString("D3");
                }
                else
                {
                    code = "001";
                }
            }

            return code;
        }


        public Branch GetById(Guid id)
        {
            Branch branch = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From(tableName).Where("ID").Equal("{" + id + "}");
                branch = em.ExecuteObject<Branch>(q.ToSql(), new BranchMapper());
            }

            return branch;
        }


        public Branch GetByCode(string code)
        {
            Branch branch = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From(tableName).Where("BranchCode").Equal(code);
                branch = em.ExecuteObject<Branch>(q.ToSql(), new BranchMapper());
            }

            return branch;
        }


        public Branch GetFirst()
        {
            Branch branch = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().Select("TOP 1 *").From(tableName).OrderBy("BranchCode");
                branch = em.ExecuteObject<Branch>(q.ToSql(), new BranchMapper());
            }

            return branch;
        }


        public Branch GetByName(string name)
        {
            Branch branch = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From(tableName).Where("BranchName").Equal(name);
                branch = em.ExecuteObject<Branch>(q.ToSql(), new BranchMapper());
            }

            return branch;
        }


        public List<Branch> GetAll()
        {
            List<Branch> branchs = new List<Branch>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From(tableName);
                branchs = em.ExecuteList<Branch>(q.ToSql(), new BranchMapper());
            }

            return branchs;
        }


        public List<string> GetAllCode()
        {
            List<string> list = new List<string>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From(tableName).OrderBy("BranchCode");

                using (var rdr = em.ExecuteReader(q.ToSql()))
                {
                    while (rdr.Read())
                    {
                        string branchCode = rdr["BranchCode"].ToString();
                        list.Add(branchCode);
                    }
                }
            }

            return list;
        }




        public List<Branch> Search(string value)
        {
            List<Branch> branchs = new List<Branch>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                Query q = new Query().From(tableName)
                    .Where("BranchCode").Like("%" + value + "%")
                    .Or("BranchName").Like("%" + value + "%")
                    .Or("Region").Like("%" + value + "%")
                    .Or("Address").Like("%" + value + "%")
                    .Or("Phone").Like("%" + value + "%")
                    .OrderBy("BranchCode ASC");

                branchs = em.ExecuteList<Branch>(q.ToSql(), new BranchMapper());
            }

            return branchs;
        }






        public void Save(Branch branch)
        {
            Transaction tx = null;

            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    tx = em.BeginTransaction();

                    string[] fields = { "ID", "BranchCode", "BranchName", "Region", "BranchHead", "ViceHead", "Address", "Phone", "Fax", "Email", 
                                        "UMR","FuelAllowance","LunchAllowance","TransportAllowance","IsActive"};

                    object[] values = { Guid.NewGuid(), branch.BranchCode, branch.BranchName, branch.Region, branch.BranchHead, branch.ViceHead,
                                        branch.Address,branch.Phone,branch.Fax,branch.Email,branch.UMR,branch.FuelAllowance,branch.LunchAllowance,
                                        branch.TransportAllowance,branch.IsActive==true?1:0};
                                        
                    Query q = new Query().Select(fields).From(tableName).Insert(values);
                    

                    em.ExecuteNonQuery(q.ToSql(),tx);

                  
                    tx.Commit();
                
                }
            }
            catch (Exception ex)
            {
                tx.Rollback();
                throw ex;
            }

        }


        public void Update(Branch branch)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {

                    string[] fields = { "BranchCode", "BranchName", "Region", "BranchHead", "ViceHead", "Address", "Phone", "Fax", "Email", 
                                        "UMR","FuelAllowance","LunchAllowance","TransportAllowance","IsActive"};

                    object[] values = { branch.BranchCode, branch.BranchName, branch.Region, branch.BranchHead, branch.ViceHead,
                                        branch.Address,branch.Phone,branch.Fax,branch.Email,branch.UMR,branch.FuelAllowance,branch.LunchAllowance,
                                        branch.TransportAllowance,branch.IsActive==true?1:0};
                

                    Query q = new Query().Select(fields).From(tableName).Update(values)
                        .Where("ID").Equal("{" + branch.ID + "}");

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


       
        public bool IsBranchNameExisted(string branchName)
        {
            bool isExisted = false;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From("Branch").Where("BranchName").Equal(branchName);

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


        public bool IsBranchCodeExisted(string branchCode)
        {
            bool isExisted = false;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From("Branch").Where("BranchCode").Equal(branchCode);

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


        public List<Branch> GetActiveBranch()
        {
            List<Branch> branchs = new List<Branch>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From(tableName).Where("IsActive=true").OrderBy("BranchCode Asc");
                branchs = em.ExecuteList<Branch>(q.ToSql(), new BranchMapper());
            }

            return branchs;
        }

        public string IsBranchUsedByDepartment(Guid branchId)
        {
           string department = string.Empty;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From("Department").Where("BranchId").Equal("{" + branchId + "}");

                using (var rdr = em.ExecuteReader(q.ToSql()))
                {
                    if (rdr.Read())
                    {
                        department = rdr["DepartmentCode"].ToString() + " - " + rdr["DepartmentName"].ToString();
                    }
                }

            }

            return department;

        }









    }
}
