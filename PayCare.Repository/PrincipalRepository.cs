
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
    public interface IPrincipalRepository
    {
        Principal GetById(Guid id);
        Principal GetByCode(string code);
        Principal GetFirst();
        Principal GetByName(string name);
        List<Principal> GetAll();
        List<string> GetAllCode();
        List<Principal> Search(string value);
        void Save(Principal principal);
        void Update(Principal principal);
        void Delete(Guid id);
        List<Principal> GetActivePrincipal();
        bool IsPrincipalNameExisted(string principalName);
        bool IsPrincipalCodeExisted(string principalCode);
        string IsPrincipalUsedByEmployee(Guid principalId);
    }

    public class PrincipalRepository : IPrincipalRepository
    {
        private string tableName = "Principal";
        private IPrincipalItemRepository principalItemRepository;
        private DataSource ds;

        public PrincipalRepository(DataSource ds)
        {
            this.ds = ds;
            principalItemRepository = EntityContainer.GetType<IPrincipalItemRepository>();
        }


        public Principal GetById(Guid id)
        {
            Principal principal = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From(tableName).Where("ID").Equal("{" + id + "}");
                principal = em.ExecuteObject<Principal>(q.ToSql(), new PrincipalMapper());
            }

            return principal;
        }


        public Principal GetByCode(string code)
        {
            Principal principal = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From(tableName).Where("PrincipalCode").Equal(code);
                principal = em.ExecuteObject<Principal>(q.ToSql(), new PrincipalMapper());
            }

            return principal;
        }



        public Principal GetFirst()
        {
            Principal principal = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().Select("TOP 1 *").From(tableName).OrderBy("PrincipalCode ASC");

                principal = em.ExecuteObject<Principal>(q.ToSql(), new PrincipalMapper());
            }

            return principal;
        }


        public Principal GetByName(string name)
        {
            Principal principal = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From(tableName).Where("PrincipalName").Equal(name).OrderBy("PrincipalCode ASC");
                principal = em.ExecuteObject<Principal>(q.ToSql(), new PrincipalMapper());
            }

            return principal;
        }


        public List<Principal> GetAll()
        {
            List<Principal> principals = new List<Principal>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From(tableName).OrderBy("PrincipalCode ASC");
                principals = em.ExecuteList<Principal>(q.ToSql(), new PrincipalMapper());
            }

            return principals;
        }


        public List<string> GetAllCode()
        {
            List<string> list = new List<string>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From(tableName).OrderBy("PrincipalCode");

                using (var rdr = em.ExecuteReader(q.ToSql()))
                {
                    while (rdr.Read())
                    {
                        string principalCode = rdr["PrincipalCode"].ToString();
                        list.Add(principalCode);
                    }
                }
            }

            return list;
        }


        public List<Principal> Search(string value)
        {
            List<Principal> principals = new List<Principal>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                Query q = new Query().From(tableName)
                    .Where("PrincipalName").Like("%" + value + "%")
                    .Or("PrincipalCode").Like("%" + value + "%")
                    .Or("Address").Like("%" + value + "%")
                    .Or("Phone").Like("%" + value + "%")
                    .OrderBy("PrincipalCode ASC");

                principals = em.ExecuteList<Principal>(q.ToSql(), new PrincipalMapper());
            }

            return principals;
        }




        public void Save(Principal principal)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    string[] fields = { "ID", "PrincipalCode", "PrincipalName", "Address", "Phone", "Fax", "Email", 
                                        "ContactPerson","Notes","IsActive","CutOffDate"};

                    object[] values = { Guid.NewGuid(), principal.PrincipalCode, principal.PrincipalName,principal.Address,principal.Phone,principal.Fax,principal.Email,
                                        principal.ContactPerson,principal.Notes,principal.IsActive==true?1:0,principal.CutOffDate.ToShortDateString()};

                    
                    Query q = new Query().Select(fields).From(tableName).Insert(values);

                    em.ExecuteNonQuery(q.ToSql());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public void Update(Principal principal)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {

                    string[] fields = { "PrincipalCode", "PrincipalName", "Address", "Phone", "Fax", "Email", 
                                        "ContactPerson","Notes","IsActive","CutOffDate"};

                    object[] values = { principal.PrincipalCode, principal.PrincipalName,principal.Address,principal.Phone,principal.Fax,principal.Email,
                                        principal.ContactPerson,principal.Notes,principal.IsActive==true?1:0,principal.CutOffDate.ToShortDateString()};

                    Query q = new Query().Select(fields).From(tableName).Update(values)
                        .Where("ID").Equal("{" + principal.ID + "}");

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

                    Query q = new Query().From(tableName).Delete().Where("ID").Equal("{" + id + "}");
                    em.ExecuteNonQuery(q.ToSql(),tx);

                    principalItemRepository.Delete(em, tx, id);

                    tx.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


       
        public bool IsPrincipalNameExisted(string principalName)
        {
            bool isExisted = false;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From("Principal").Where("PrincipalName").Equal(principalName);

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


        public bool IsPrincipalCodeExisted(string principalCode)
        {
            bool isExisted = false;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From("Principal").Where("PrincipalCode").Equal(principalCode);

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


        public List<Principal> GetActivePrincipal()
        {
            List<Principal> principals = new List<Principal>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From(tableName).Where("IsActive=true").OrderBy("PrincipalCode Asc");
                principals = em.ExecuteList<Principal>(q.ToSql(), new PrincipalMapper());
            }

            return principals;
        }


        public string IsPrincipalUsedByEmployee(Guid principalId)
        {
            string employeeCode = string.Empty;
            string employeeName = string.Empty;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string sql = "SELECT e.EmployeeCode, e.EmployeeName "
                           + "FROM EmployeePrincipal ep INNER JOIN Employee e ON ep.EmployeeId = e.ID "
                           + "WHERE ep.PrincipalId='{" + principalId + "}' ";

                using (var rdr = em.ExecuteReader(sql))
                {
                    if (rdr.Read())
                    {
                        employeeCode=rdr["EmployeeCode"].ToString();
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
