using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityMap;
using PayCare.Model;
using System.Data;

namespace PayCare.Repository.Mapping
{
    public class InsuranceMapper : IDataMapper<Insurance>
    {

        public Insurance Map(IDataReader rdr)
        {
            var insurance = new Insurance();

            insurance.ID = rdr["ID"] is DBNull ? Guid.Empty : (Guid)rdr["ID"];
            insurance.InsuranceCode = rdr["InsuranceCode"] is DBNull ? string.Empty : (string)rdr["InsuranceCode"];
            insurance.InsuranceName = rdr["InsuranceName"] is DBNull ? string.Empty : (string)rdr["InsuranceName"];
            insurance.Notes = rdr["Notes"] is DBNull ? string.Empty : (string)rdr["Notes"];
            insurance.IsActive = rdr["IsActive"] is DBNull ? false : (bool)rdr["IsActive"];
         
            return insurance;

        }

    }
}
