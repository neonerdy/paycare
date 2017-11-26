using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityMap;
using System.Data;
using PayCare.Model;


namespace PayCare.Repository.Mapping
{
    public class BranchMapper : IDataMapper<Branch>
    {
        public Branch Map(IDataReader rdr)
        {
            var branch = new Branch();

            branch.ID = rdr["ID"] is DBNull ? Guid.Empty : (Guid)rdr["ID"];
            branch.BranchCode = rdr["BranchCode"] is DBNull ? string.Empty : (string)rdr["BranchCode"];
            branch.BranchName = rdr["BranchName"] is DBNull ? string.Empty : (string)rdr["BranchName"];
            branch.Region = rdr["Region"] is DBNull ? string.Empty : (string)rdr["Region"];
            branch.BranchHead = rdr["BranchHead"] is DBNull ? string.Empty : (string)rdr["BranchHead"];
            branch.ViceHead = rdr["ViceHead"] is DBNull ? string.Empty : (string)rdr["ViceHead"];            
            branch.Address = rdr["Address"] is DBNull ? string.Empty : (string)rdr["Address"];
            branch.Phone = rdr["Phone"] is DBNull ? string.Empty : (string)rdr["Phone"];
            branch.Fax = rdr["Fax"] is DBNull ? string.Empty : (string)rdr["Fax"];
            branch.Email = rdr["Email"] is DBNull ? string.Empty : (string)rdr["Email"];
            branch.UMR = rdr["UMR"] is DBNull ? 0 : (decimal)rdr["UMR"];
            branch.FuelAllowance = rdr["FuelAllowance"] is DBNull ? 0 : (decimal)rdr["FuelAllowance"];
            branch.LunchAllowance = rdr["LunchAllowance"] is DBNull ? 0 : (decimal)rdr["LunchAllowance"];
            branch.TransportAllowance = rdr["TransportAllowance"] is DBNull ? 0 : (decimal)rdr["TransportAllowance"];
            branch.IsActive = rdr["IsActive"] is DBNull ? false : (bool)rdr["IsActive"];
            branch.EmployeeCounter = rdr["EmployeeCounter"] is DBNull ? 0 : (int)rdr["EmployeeCounter"];
            

            return branch;
        }

    }
}
