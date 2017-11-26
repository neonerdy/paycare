using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayCare.Model;
using EntityMap;
using System.Data;

namespace PayCare.Repository.Mapping
{
       
    public class EmployeeMapper : IDataMapper<Employee>
    {
        public Employee Map(IDataReader rdr)
        {
            var employee = new Employee();

            employee.ID = rdr["ID"] is DBNull ? Guid.Empty : (Guid)rdr["ID"];
            employee.EmployeeCode = rdr["EmployeeCode"] is DBNull ? string.Empty : (string)rdr["EmployeeCode"];
            employee.OldEmployeeCode = rdr["OldEmployeeCode"] is DBNull ? string.Empty : (string)rdr["OldEmployeeCode"];
            employee.EmployeeName = rdr["EmployeeName"] is DBNull ? string.Empty : (string)rdr["EmployeeName"];
            employee.BirthPlace = rdr["BirthPlace"] is DBNull ? string.Empty : (string)rdr["BirthPlace"];
            employee.BirthDate = rdr["BirthDate"] is DBNull ? DateTime.Now : (DateTime)rdr["BirthDate"];
            employee.Gender = rdr["Gender"] is DBNull ?false : (bool)rdr["Gender"];
            employee.Religion = rdr["Religion"] is DBNull ? string.Empty : (string)rdr["Religion"];
            employee.IsTransfer = rdr["IsTransfer"] is DBNull ? false: (bool)rdr["IsTransfer"];
            employee.BankName = rdr["BankName"] is DBNull ? string.Empty : (string)rdr["BankName"];
            employee.AccountNumber = rdr["AccountNumber"] is DBNull ? string.Empty : (string)rdr["AccountNumber"];
            employee.MaritalStatus = rdr["MaritalStatus"] is DBNull ? false : (bool)rdr["MaritalStatus"];
            employee.NumberOfChilds = rdr["NumberOfChilds"] is DBNull ? 0 : (int)rdr["NumberOfChilds"];
            employee.IsInsurance = rdr["IsInsurance"] is DBNull ? false : (bool)rdr["IsInsurance"];
            employee.IsTax = rdr["IsTax"] is DBNull ? false : (bool)rdr["IsTax"];
            employee.NPWP = rdr["NPWP"] is DBNull ? string.Empty : (string)rdr["NPWP"];
            employee.PTKPId = rdr["PTKPId"] is DBNull ? Guid.Empty : (Guid)rdr["PTKPId"];
            employee.PTKPCode = rdr["PTKPCode"] is DBNull ? string.Empty : (string)rdr["PTKPCode"];
            employee.IsPrincipal = rdr["IsPrincipal"] is DBNull ? false : (bool)rdr["IsPrincipal"];
            employee.IsFuelAllowance = rdr["IsFuelAllowance"] is DBNull ? false : (bool)rdr["IsFuelAllowance"];
            employee.StartDate = rdr["StartDate"] is DBNull ? DateTime.Now : (DateTime)rdr["StartDate"];
            employee.EndDate = rdr["EndDate"] is DBNull ? DateTime.Now : (DateTime)rdr["EndDate"];
            employee.IsActive = rdr["IsActive"] is DBNull ? false : (bool)rdr["IsActive"];

            if (employee.CurrentInfo == null) employee.CurrentInfo = new EmployeeCurrentInfo();

            employee.CurrentInfo.BranchId = rdr["CurrentBranchId"] is DBNull ? Guid.Empty : (Guid)rdr["CurrentBranchId"];
            employee.CurrentInfo.BranchName = rdr["CurrentBranch"] is DBNull ? string.Empty : (string)rdr["CurrentBranch"];
            employee.CurrentInfo.DepartmentId = rdr["CurrentDepartmentId"] is DBNull ? Guid.Empty : (Guid)rdr["CurrentDepartmentId"];
            employee.CurrentInfo.DepartmentName = rdr["CurrentDepartment"] is DBNull ? string.Empty : (string)rdr["CurrentDepartment"];
            employee.CurrentInfo.GradeId = rdr["CurrentGradeId"] is DBNull ? Guid.Empty : (Guid)rdr["CurrentGradeId"];
            employee.CurrentInfo.GradeName = rdr["CurrentGrade"] is DBNull ? string.Empty : (string)rdr["CurrentGrade"];
            employee.CurrentInfo.GradeLevel = rdr["CurrentGradeLevel"] is DBNull ? 0 : (int)rdr["CurrentGradeLevel"];
            employee.CurrentInfo.OccupationId = rdr["CurrentOccupationId"] is DBNull ? Guid.Empty : (Guid)rdr["CurrentOccupationId"];
            employee.CurrentInfo.OccupationName = rdr["CurrentOccupation"] is DBNull ? string.Empty : (string)rdr["CurrentOccupation"];
            employee.CurrentInfo.EmployeeStatus = rdr["CurrentStatus"] is DBNull ? string.Empty : (string)rdr["CurrentStatus"];
            employee.CurrentInfo.PaymentType = rdr["CurrentPaymentType"] is DBNull ? string.Empty : (string)rdr["CurrentPaymentType"];
            employee.CurrentInfo.BankName = rdr["BankName"] is DBNull ? string.Empty : (string)rdr["BankName"];
            employee.CurrentInfo.AccountNumber = rdr["AccountNumber"] is DBNull ? string.Empty : (string)rdr["AccountNumber"];
                       


            return employee;
        
        
        }
    }
}
