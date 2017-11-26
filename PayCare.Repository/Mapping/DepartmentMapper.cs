using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityMap;
using System.Data;
using PayCare.Model;


namespace PayCare.Repository.Mapping
{
    public class DepartmentMapper : IDataMapper<Department>
    {
        public Department Map(IDataReader rdr)
        {
            var department = new Department();

            department.ID = rdr["ID"] is DBNull ? Guid.Empty : (Guid)rdr["ID"];
            department.DepartmentCode = rdr["DepartmentCode"] is DBNull ? string.Empty : (string)rdr["DepartmentCode"];
            department.DepartmentName = rdr["DepartmentName"] is DBNull ? string.Empty : (string)rdr["DepartmentName"];
            department.DepartmentHead = rdr["DepartmentHead"] is DBNull ? string.Empty : (string)rdr["DepartmentHead"];
            department.IsActive = rdr["IsActive"] is DBNull ? false : (bool)rdr["IsActive"];

            department.BranchId = rdr["BranchId"] is DBNull ? Guid.Empty : (Guid)rdr["BranchId"];

            if (department.Branch == null) department.Branch = new Branch();
            department.Branch.BranchCode = rdr["BranchCode"] is DBNull ? string.Empty : (string)rdr["BranchCode"];
            department.Branch.BranchName = rdr["BranchName"] is DBNull ? string.Empty : (string)rdr["BranchName"];

            

            return department;
        }

    }
}
