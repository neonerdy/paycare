using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayCare.Model;
using EntityMap;
using PayCare.Repository.Mapping;

namespace PayCare.Repository
{
    public interface IEmployeeRepository
    {
        string GenerateCode(string branchName);
        int GetCount();
        Employee GetById(Guid id);
        List<Employee> GetByIds(Guid branchId, Guid gradeId, Guid occupationId);
        List<Employee> GetAll();
        List<Employee> GetTop(int top);
        Employee GetByCode(string code);
        Employee GetLast();
        List<Employee> GetMoslemEmployee();
        List<Employee> GetNonMoslemEmployee();
        List<Employee> GetActiveEmployee();
        List<Employee> Search(string value);


        Guid SaveHeader(Employee employee);
        void UpdateHeader(Employee employee);

        Guid Save(Employee employee);
        void Update(Employee employee);
        void UpdateCurrentInfo(EmployeeCurrentInfo currentInfo);
        void Delete(Guid id);

        string IsOldCodeExisted(string oldCode);

    }
    

    public class EmployeeRepository : IEmployeeRepository
    {
        private DataSource ds;
        private IEmployeeFamilyRepository employeeFamilyRepository;
        private IEmployeeDepartmentRepository employeeDepatmentRepository;
        private IEmployeeGradeRepository employeeGradeRepository;
        private IEmployeeOccupationRepository employeeOccupationRepository;
        private IEmployeePrincipalRepository employeePrincipalRepository;
        private IEmployeeStatusRepository employeeStatusRepository;
        private IEmployeeInsuranceRepository employeeInsuranceRepository;
        private IEmployeeSalaryRepository employeeSalaryRepository;
        private IBranchRepository branchRepository;      

        private string tableName = "Employee";



        public EmployeeRepository(DataSource ds)
        {
            this.ds = ds;
        
            employeeFamilyRepository = EntityContainer.GetType<IEmployeeFamilyRepository>();
            employeeDepatmentRepository = EntityContainer.GetType<IEmployeeDepartmentRepository>();
            employeeGradeRepository = EntityContainer.GetType<IEmployeeGradeRepository>();
            employeeOccupationRepository = EntityContainer.GetType<IEmployeeOccupationRepository>();
            employeePrincipalRepository = EntityContainer.GetType<IEmployeePrincipalRepository>();
            employeeStatusRepository = EntityContainer.GetType<IEmployeeStatusRepository>();
            employeeInsuranceRepository = EntityContainer.GetType<IEmployeeInsuranceRepository>();
            employeeSalaryRepository = EntityContainer.GetType<IEmployeeSalaryRepository>();
            branchRepository = EntityContainer.GetType<IBranchRepository>();

        }
        

        #region IEmployeeRepository Members

        public string GenerateCode(string branchName)
        {

            string code = string.Empty;

            int counter = 0;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From("Branch").Where("BranchName").Equal(branchName);
                var branch = em.ExecuteObject<Branch>(q.ToSql(), new BranchMapper());

                if (branch != null) counter = branch.EmployeeCounter;

                if (counter == 0)
                {
                    code = "000001";
                }
                else
                {
                    counter = counter + 1;
                    code = counter.ToString("D6");
                }
            }

            return code;
        }


       
        public int GetCount()
        {
            int count = 0;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string sql = "SELECT COUNT(*) FROM " + tableName;
                count=(int)em.ExecuteScalar(sql);
            }

            return count;

        }


        public Employee GetById(Guid id)
        {
            Employee employee = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string sql = "SELECT e.*,p.PtkpCode FROM Employee e INNER JOIN PTKP p ON e.PTKPId = p.ID "
                    + "WHERE e.ID='{" + id + "}'";
                
                employee = em.ExecuteObject<Employee>(sql, new EmployeeMapper());
            }

            return employee;
        }


        public List<Employee> GetByIds(Guid branchId, Guid gradeId, Guid occupationId)
        {
            List<Employee> employees = new List<Employee>();

            StringBuilder condition = new StringBuilder();

            if (branchId != Guid.Empty)
                condition.Append(" AND e.CurrentBranchId='{" + branchId + "}'");
          
            if (gradeId != Guid.Empty)
                condition.Append(" AND e.CurrentGradeId='{" + gradeId + "}'");

            if (occupationId != Guid.Empty)
                condition.Append(" AND e.CurrentOccupationId='{" + occupationId + "}'");
         

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string sql = "SELECT e.*,p.PtkpCode "
                    + "FROM Employee e INNER JOIN PTKP p ON e.PTKPId = p.ID "
                    + "WHERE " + condition.ToString().Substring(5) + " "
                    + "AND e.IsActive=true";
                   
                employees = em.ExecuteList<Employee>(sql, new EmployeeMapper());

                foreach (var e in employees)
                {
                    e.LastSalary = employeeSalaryRepository.GetLastSalary(e.ID);
                }
            
            }

            return employees;
        }




        public List<Employee> GetAll()
        {
            List<Employee> employees = new List<Employee>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string sql = "SELECT e.*,p.PtkpCode "
                    + "FROM Employee e INNER JOIN PTKP p ON e.PTKPId = p.ID "
                    + "Order by e.EmployeeCode desc";

                employees = em.ExecuteList<Employee>(sql, new EmployeeMapper());
            }

            return employees;
        }


        public List<Employee> GetActiveEmployee()
        {
            List<Employee> employees = new List<Employee>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string sql = "SELECT e.*,p.PtkpCode FROM Employee e INNER JOIN PTKP p ON e.PTKPId = p.ID "
                   + "WHERE e.IsActive=true ORDER BY EmployeeCode DESC";

                employees = em.ExecuteList<Employee>(sql, new EmployeeMapper());
            }

            return employees;
        }


        public List<Employee> GetTop(int top)
        {
            List<Employee> employees = new List<Employee>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string sql = "SELECT TOP " + top.ToString() + " e.*,p.PtkpCode FROM Employee e INNER JOIN PTKP p ON e.PTKPId = p.ID "
                        + "ORDER BY e.EmployeeCode DESC";

                employees = em.ExecuteList<Employee>(sql, new EmployeeMapper());
            }

            return employees;
        }

        public Employee GetByCode(string code)
        {
            Employee employee = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string sql = "SELECT TOP 20 e.*,p.PtkpCode FROM Employee e INNER JOIN PTKP p ON e.PTKPId = p.ID "
                        + "WHERE e.EmployeeCode='" + code + "'";

                employee = em.ExecuteObject<Employee>(sql, new EmployeeMapper());
            }

            return employee;
        }
        

        public Employee GetLast()
        {
            Employee employee = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {

                string sql = "SELECT TOP 1 e.*,p.PtkpCode FROM Employee e INNER JOIN PTKP p ON e.PTKPId = p.ID "
                           + "ORDER BY e.EmployeeCode DESC";

                employee = em.ExecuteObject<Employee>(sql, new EmployeeMapper());
            }

            return employee;

        }


        public List<Employee> GetMoslemEmployee()
        {
            List<Employee> employees = new List<Employee>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string sql = "SELECT e.*,p.PtkpCode "
                   + "FROM Employee e INNER JOIN PTKP p ON e.PTKPId = p.ID "
                   + "WHERE e.IsActive=true "
                   + "AND e.Religion = 'Islam' "
                   + "ORDER BY EmployeeCode DESC";

                employees = em.ExecuteList<Employee>(sql, new EmployeeMapper());
            }

            return employees;
        }

        public List<Employee> GetNonMoslemEmployee()
        {
            List<Employee> employees = new List<Employee>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string sql = "SELECT e.*,p.PtkpCode "
                   + "FROM Employee e INNER JOIN PTKP p ON e.PTKPId = p.ID "
                   + "WHERE e.IsActive=true "
                   + "AND e.Religion Not Like 'Islam' "
                   + "ORDER BY EmployeeCode DESC";

                employees = em.ExecuteList<Employee>(sql, new EmployeeMapper());
            }

            return employees;
        }


        public Guid SaveHeader(Employee employee)
        {
            Transaction tx = null;

            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    tx = em.BeginTransaction();

                    Guid ID = Guid.NewGuid();

                    string[] columns = { "ID", "EmployeeCode", "OldEmployeeCode", "EmployeeName", "BirthPlace", "BirthDate", "Gender", "Religion", "IsTransfer",
                                         "BankName","AccountNumber","MaritalStatus","NumberOfChilds","IsInsurance","IsTax","NPWP","PTKPId",
                                         "IsPrincipal","IsFuelAllowance","StartDate","EndDate", "IsActive",
                                         "CreatedDate","CreatedBy","ModifiedDate", "ModifiedBy" };

                    object[] values = { ID, employee.EmployeeCode, employee.OldEmployeeCode, employee.EmployeeName,employee.BirthPlace,employee.BirthDate.ToShortDateString(),
                                        employee.Gender==true?1:0,employee.Religion,employee.IsTransfer==true?1:0,employee.BankName,employee.AccountNumber,
                                        employee.MaritalStatus==true?1:0,employee.NumberOfChilds,employee.IsInsurance==true?1:0,employee.IsTax==true?1:0,employee.NPWP,
                                        employee.PTKPId,employee.IsPrincipal==true?1:0,employee.IsFuelAllowance==true?1:0,employee.StartDate.ToShortDateString(),
                                        employee.EndDate.ToShortDateString(), employee.IsActive==true?1:0,
                                        DateTime.Now.ToShortDateString(),Store.ActiveUser,DateTime.Now.ToShortDateString(), Store.ActiveUser};

                    var q = new Query().Select(columns).From(tableName).Insert(values);

                    em.ExecuteNonQuery(q.ToSql(),tx);

                    branchRepository.UpdateEmployeeCounter(employee.CurrentInfo.BranchName, em, tx);

                    tx.Commit();

                    return ID;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        public void UpdateHeader(Employee employee)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    string[] columns = { "OldEmployeeCode", "EmployeeName", "BirthPlace", "BirthDate", "Gender", "Religion", "IsTransfer",
                                         "BankName","AccountNumber","MaritalStatus","NumberOfChilds","IsInsurance","IsTax","NPWP","PTKPId",
                                         "IsPrincipal","IsFuelAllowance","StartDate","EndDate", "IsActive",
                                         "CreatedDate","CreatedBy","ModifiedDate", "ModifiedBy" };

                    object[] values = { employee.OldEmployeeCode, employee.EmployeeName,employee.BirthPlace,employee.BirthDate.ToShortDateString(),
                                        employee.Gender==true?1:0,employee.Religion,employee.IsTransfer==true?1:0,employee.BankName,employee.AccountNumber,
                                        employee.MaritalStatus==true?1:0,employee.NumberOfChilds,employee.IsInsurance==true?1:0,employee.IsTax==true?1:0,employee.NPWP,
                                        employee.PTKPId,employee.IsPrincipal==true?1:0,employee.IsFuelAllowance==true?1:0,employee.StartDate.ToShortDateString(),
                                        employee.EndDate.ToShortDateString(), employee.IsActive==true?1:0,
                                        DateTime.Now.ToShortDateString(),Store.ActiveUser,DateTime.Now.ToShortDateString(), Store.ActiveUser};

                    var q = new Query().Select(columns).From(tableName).Update(values)
                        .Where("EmployeeCode").Equal(employee.EmployeeCode);

                    em.ExecuteNonQuery(q.ToSql());
                                    
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }




        public Guid Save(Employee employee)
        {
            Transaction tx = null;

            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    tx = em.BeginTransaction();

                    Guid ID=Guid.NewGuid();

                    string[] columns = { "ID", "EmployeeCode", "OldEmployeeCode", "EmployeeName", "BirthPlace", "BirthDate", "Gender", "Religion", "IsTransfer",
                                         "BankName","AccountNumber","MaritalStatus","NumberOfChilds","IsInsurance","IsTax","NPWP","PTKPId",
                                         "IsPrincipal","IsFuelAllowance","StartDate","EndDate", "IsActive",
                                         "CreatedDate","CreatedBy","ModifiedDate", "ModifiedBy" };

                    object[] values = { ID, employee.EmployeeCode, employee.OldEmployeeCode, employee.EmployeeName,employee.BirthPlace,employee.BirthDate.ToShortDateString(),
                                        employee.Gender==true?1:0,employee.Religion,employee.IsTransfer==true?1:0,employee.BankName,employee.AccountNumber,
                                        employee.MaritalStatus==true?1:0,employee.NumberOfChilds,employee.IsInsurance==true?1:0,employee.IsTax==true?1:0,employee.NPWP,
                                        employee.PTKPId,employee.IsPrincipal==true?1:0,employee.IsFuelAllowance==true?1:0,employee.StartDate.ToShortDateString(),
                                        employee.EndDate.ToShortDateString(), employee.IsActive==true?1:0,
                                        DateTime.Now.ToShortDateString(),Store.ActiveUser,DateTime.Now.ToShortDateString(), Store.ActiveUser};

                    var q = new Query().Select(columns).From(tableName).Insert(values);

                    em.ExecuteNonQuery(q.ToSql(),tx);

                    foreach (var family in employee.Families)
                    {
                        family.EmployeeId = ID;
                        employeeFamilyRepository.Save(em, tx, family);
                    }

                    foreach (var department in employee.Departments)
                    {
                        department.EmployeeId = ID;
                        employeeDepatmentRepository.Save(em, tx, department);
                    }

                    foreach (var grade in employee.Grades)
                    {
                        grade.EmployeeId = ID;
                        employeeGradeRepository.Save(em, tx, grade);
                    }

                    foreach (var occupation in employee.Occupations)
                    {
                        occupation.EmployeeId = ID;
                        employeeOccupationRepository.Save(em, tx, occupation);
                    }

                    foreach (var principal in employee.Principals)
                    {
                        principal.EmployeeId = ID;
                        employeePrincipalRepository.Save(em, tx, principal);
                    }

                    foreach (var employeeStatus in employee.Status)
                    {
                        employeeStatus.EmployeeId = ID;
                        employeeStatusRepository.Save(em, tx, employeeStatus);
                    }

                    foreach (var employeeInsurance in employee.Insurances)
                    {
                        employeeInsurance.EmployeeId = ID;
                        employeeInsuranceRepository.Save(em, tx, employeeInsurance);
                    }


                    foreach (var employeeSalary in employee.Salaries)
                    {
                        employeeSalary.EmployeeId = ID;
                        employeeSalaryRepository.Save(em, tx, employeeSalary);
                    }


                    branchRepository.UpdateEmployeeCounter(employee.CurrentInfo.BranchName, em, tx);
                    
                    tx.Commit();
                    
                                       

                    return ID;
                   
                }
            }
            catch (Exception ex)
            {
                tx.Rollback();
                throw ex;
            }
        }



        public void Update(Employee employee)
        {
            Transaction tx = null;

            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    tx = em.BeginTransaction();


                    string[] columns = { "EmployeeCode", "OldEmployeeCode", "EmployeeName", "BirthPlace", "BirthDate", "Gender", "Religion", "IsTransfer",
                                         "BankName","AccountNumber","MaritalStatus","NumberOfChilds","IsInsurance","IsTax","NPWP","PTKPId",
                                         "IsPrincipal","IsFuelAllowance","StartDate","EndDate", "IsActive", 
                                         "ModifiedDate","ModifiedBy"};

                    object[] values = { employee.EmployeeCode, employee.OldEmployeeCode, employee.EmployeeName,employee.BirthPlace,employee.BirthDate.ToShortDateString(),
                                        employee.Gender==true?1:0,employee.Religion,employee.IsTransfer==true?1:0,employee.BankName,employee.AccountNumber,
                                        employee.MaritalStatus==true?1:0,employee.NumberOfChilds,employee.IsInsurance==true?1:0,employee.IsTax==true?1:0,employee.NPWP,
                                        employee.PTKPId,employee.IsPrincipal==true?1:0,employee.IsFuelAllowance==true?1:0,employee.StartDate.ToShortDateString(),
                                        employee.EndDate.ToShortDateString(), employee.IsActive==true?1:0,
                                        DateTime.Now.ToShortDateString(),Store.ActiveUser};


                    var q = new Query().Select(columns).From(tableName).Update(values)
                        .Where("ID").Equal("{" + employee.ID + "}");

                    em.ExecuteNonQuery(q.ToSql(),tx);
                    
                    employeeFamilyRepository.Delete(em, tx, employee.ID);
                    foreach (var family in employee.Families)
                    {
                        family.EmployeeId = employee.ID;
                        employeeFamilyRepository.Save(em, tx, family);
                    }


                    employeeDepatmentRepository.Delete(em, tx, employee.ID);
                    foreach (var dept in employee.Departments)
                    {
                        dept.EmployeeId = employee.ID;
                        employeeDepatmentRepository.Save(em, tx, dept);
                    }


                    employeeGradeRepository.Delete(em, tx, employee.ID);
                    foreach (var grade in employee.Grades)
                    {
                        grade.EmployeeId = employee.ID;
                        employeeGradeRepository.Save(em, tx, grade);
                    }


                    employeeOccupationRepository.Delete(em, tx, employee.ID);
                    foreach (var occupation in employee.Occupations)
                    {
                        occupation.EmployeeId = employee.ID;
                        employeeOccupationRepository.Save(em, tx, occupation);
                    }


                    employeePrincipalRepository.Delete(em, tx, employee.ID);
                    foreach (var principal in employee.Principals)
                    {
                        principal.EmployeeId = employee.ID;
                        employeePrincipalRepository.Save(em, tx, principal);
                    }

                    employeeStatusRepository.Delete(em, tx, employee.ID);
                    foreach (var status in employee.Status)
                    {
                        status.EmployeeId = employee.ID;
                        employeeStatusRepository.Save(em, tx, status);
                    }


                    employeeInsuranceRepository.Delete(em, tx, employee.ID);
                    foreach (var insurance in employee.Insurances)
                    {
                        insurance.EmployeeId = employee.ID;
                        employeeInsuranceRepository.Save(em, tx, insurance);
                    }

                    employeeSalaryRepository.Delete(em, tx, employee.ID);
                    foreach (var salary in employee.Salaries)
                    {
                        salary.EmployeeId = employee.ID;
                        employeeSalaryRepository.Save(em, tx, salary);
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



        public void UpdateCurrentInfo(EmployeeCurrentInfo currentInfo)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    string[] columns = { "CurrentBranchId", "CurrentBranch", "CurrentDepartmentId", "CurrentDepartment", "CurrentGradeId",
                                         "CurrentGrade", "CurrentGradeLevel", "CurrentOccupationId", "CurrentOccupation", "CurrentStatus", "CurrentPaymentType"};

                    object[] values = { currentInfo.BranchId, currentInfo.BranchName, currentInfo.DepartmentId, currentInfo.DepartmentName, 
                                        currentInfo.GradeId, currentInfo.GradeName, currentInfo.GradeLevel, currentInfo.OccupationId, 
                                        currentInfo.OccupationName, currentInfo.EmployeeStatus, currentInfo.PaymentType };
                    
                    var q = new Query().Select(columns).From(tableName).Update(values)
                        .Where("ID").Equal("{" + currentInfo.EmployeeId + "}");

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


                    employeeFamilyRepository.Delete(em, tx, id);
                    employeeDepatmentRepository.Delete(em, tx, id);
                    employeeGradeRepository.Delete(em, tx, id);
                    employeeOccupationRepository.Delete(em, tx, id);
                    employeePrincipalRepository.Delete(em, tx, id);
                    employeeStatusRepository.Delete(em, tx, id);
                    employeeInsuranceRepository.Delete(em, tx, id);
                    employeeSalaryRepository.Delete(em, tx, id);


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


        public List<Employee> Search(string value)
        {
            List<Employee> employees = new List<Employee>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string sql = "SELECT e.*,p.PtkpCode FROM Employee e INNER JOIN PTKP p ON e.PTKPId = p.ID "
                         + "WHERE e.IsActive=true AND "
                         + "e.EmployeeCode LIKE '%" + value + "%' " 
                         + "OR e.EmployeeName LIKE '%" + value + "%' "
                         + "OR e.CurrentBranch LIKE '%" + value + "%' "
                         + "ORDER BY EmployeeCode DESC";

                employees = em.ExecuteList<Employee>(sql, new EmployeeMapper());
            }

            return employees;
        }


        public string IsOldCodeExisted(string oldCode)
        {
            string employeeName = string.Empty;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From("Employee").Where("OldEmployeeCode").Equal(oldCode);

                using (var rdr = em.ExecuteReader(q.ToSql()))
                {
                    if (rdr.Read())
                    {
                        employeeName = rdr["EmployeeCode"].ToString() + " - " + rdr["EmployeeName"].ToString();
                    }
                }

            }

            return employeeName;

        }


        #endregion
    }

   

}
