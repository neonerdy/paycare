using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PayCare.Model;
using System.Data;
using EntityMap;

namespace PayCare.Repository.Mapping
{
    public class EmployeeDepartmentMapper : IDataMapper<EmployeeDepartment>
    {
        public EmployeeDepartment Map(IDataReader rdr)
        {
            var employeeDepartment = new EmployeeDepartment();

            employeeDepartment.ID = rdr["ID"] is DBNull ? Guid.Empty : (Guid)rdr["ID"];
            employeeDepartment.EmployeeId = rdr["EmployeeId"] is DBNull ? Guid.Empty : (Guid)rdr["EmployeeId"];
            employeeDepartment.EffectiveDate = rdr["EffectiveDate"] is DBNull ? DateTime.Now : (DateTime)rdr["EffectiveDate"];
            employeeDepartment.BranchId = rdr["BranchId"] is DBNull ? Guid.Empty : (Guid)rdr["BranchId"];
            employeeDepartment.BranchName = rdr["BranchName"] is DBNull ? string.Empty : (string)rdr["BranchName"];
            employeeDepartment.DepartmentId = rdr["DepartmentId"] is DBNull ? Guid.Empty : (Guid)rdr["DepartmentId"];
            employeeDepartment.DepartmentName = rdr["DepartmentName"] is DBNull ? string.Empty : (string)rdr["DepartmentName"];
        
            return employeeDepartment;
        
        }

    }
}
