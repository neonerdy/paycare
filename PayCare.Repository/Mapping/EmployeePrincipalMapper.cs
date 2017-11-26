using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayCare.Model;
using EntityMap;

using System.Data;

namespace PayCare.Repository.Mapping
{
    public class EmployeePrincipalMapper : IDataMapper<EmployeePrincipal>
    {
     
        public EmployeePrincipal Map(IDataReader rdr)
        {
            var employeePrincipal = new EmployeePrincipal();

            employeePrincipal.ID = rdr["ID"] is DBNull ? Guid.Empty : (Guid)rdr["ID"];
            employeePrincipal.EmployeeId = rdr["EmployeeId"] is DBNull ? Guid.Empty : (Guid)rdr["EmployeeId"];
            employeePrincipal.EffectiveDate = rdr["EffectiveDate"] is DBNull ? DateTime.Now : (DateTime)rdr["EffectiveDate"];
            employeePrincipal.PrincipalId = rdr["PrincipalId"] is DBNull ? Guid.Empty : (Guid)rdr["PrincipalId"];
            employeePrincipal.PrincipalName = rdr["PrincipalName"] is DBNull ? string.Empty : (string)rdr["PrincipalName"];
            employeePrincipal.IsActive = rdr["IsActive"] is DBNull ? false : (bool)rdr["IsActive"];
            
            return employeePrincipal;

        }

     
    }
}
