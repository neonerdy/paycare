using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityMap;
using System.Data;
using PayCare.Model;


namespace PayCare.Repository.Mapping
{
    public class PrincipalMapper : IDataMapper<Principal>
    {
        public Principal Map(IDataReader rdr)
        {
            var principal = new Principal();

            principal.ID = rdr["ID"] is DBNull ? Guid.Empty : (Guid)rdr["ID"];
            principal.PrincipalCode = rdr["PrincipalCode"] is DBNull ? string.Empty : (string)rdr["PrincipalCode"];
            principal.PrincipalName = rdr["PrincipalName"] is DBNull ? string.Empty : (string)rdr["PrincipalName"];
            
            principal.ContactPerson = rdr["ContactPerson"] is DBNull ? string.Empty : (string)rdr["ContactPerson"];
            principal.Address = rdr["Address"] is DBNull ? string.Empty : (string)rdr["Address"];           
            principal.Phone = rdr["Phone"] is DBNull ? string.Empty : (string)rdr["Phone"];
            principal.Fax = rdr["Fax"] is DBNull ? string.Empty : (string)rdr["Fax"];
            principal.Email = rdr["Email"] is DBNull ? string.Empty : (string)rdr["Email"];
            principal.Notes = rdr["Notes"] is DBNull ? string.Empty : (string)rdr["Notes"];
            principal.CutOffDate = rdr["CutOffDate"] is DBNull ? DateTime.Now : (DateTime)rdr["CutOffDate"];
            principal.IsActive = rdr["IsActive"] is DBNull ? false : (bool)rdr["IsActive"];
            
            return principal;
        }

    }
}
