using System;
using System.Collections;

using System.Collections.Generic;

namespace PayCare.Model
{
    public class EmployeeCurrentInfo
    {
        public Guid EmployeeId { get; set; }

        public Guid BranchId { get; set; } 
        
        public string BranchName { get; set; }

        public Guid DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public Guid GradeId { get; set; }
        
        public string GradeName { get; set; }

        public int GradeLevel { get; set; }

        public Guid OccupationId { get; set; }

        public string OccupationName { get; set; }

        public string EmployeeStatus { get; set; }

        public string PaymentType { get; set; }

        public string BankName { get; set; }

        public string AccountNumber { get; set; }

    }



    public class Employee
    {
        public Guid ID { get; set; }

        public string EmployeeCode { get; set; }

        public string OldEmployeeCode { get; set; }

        public string EmployeeName { get; set; }

        public string BirthPlace { get; set; }

        public DateTime BirthDate { get; set; }

        public bool Gender { get; set; }

        public string Religion { get; set; }

        public bool IsTransfer { get; set; }

        public string BankName { get; set; }

        public string AccountNumber { get; set; }

        public bool MaritalStatus { get; set; }

        public int NumberOfChilds { get; set; }

        public bool IsInsurance { get; set; }

        public bool IsTax { get; set; }

        public string NPWP { get; set; }

        public Guid PTKPId { get; set; }

        public string PTKPCode { get; set; }

        public bool IsPrincipal { get; set; }

        public bool IsFuelAllowance { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }

        public bool IsActive { get; set; }

        public EmployeeCurrentInfo CurrentInfo { get; set; }

        public EmployeeSalary LastSalary { get; set; }

        public List<EmployeeFamily> Families { get; set; }

        public List<EmployeeDepartment> Departments { get; set; }

        public List<EmployeeGrade> Grades { get; set; }

        public List<EmployeeOccupation> Occupations { get; set; }

        public List<EmployeePrincipal> Principals { get; set; }

        public List<EmployeeStatus> Status { get; set; }

        public List<EmployeeInsurance> Insurances { get; set; }

        public List<EmployeeSalary> Salaries { get; set; }
        

    }
}
