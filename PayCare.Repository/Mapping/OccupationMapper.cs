using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayCare.Model;
using EntityMap;
using System.Data;

namespace PayCare.Repository.Mapping
{
    public class OccupationMapper : IDataMapper<Occupation>
    {

        public Occupation Map(IDataReader rdr)
        {
            var occupation = new Occupation();

            occupation.ID = rdr["ID"] is DBNull ? Guid.Empty : (Guid)rdr["ID"];
            occupation.OccupationCode = rdr["OccupationCode"] is DBNull ? string.Empty : (string)rdr["OccupationCode"];
            occupation.OccupationName = rdr["OccupationName"] is DBNull ? string.Empty : (string)rdr["OccupationName"];
            occupation.HealthAllowance = rdr["HealthAllowance"] is DBNull ? 0 : (decimal)rdr["HealthAllowance"];
            occupation.VehicleAllowance = rdr["VehicleAllowance"] is DBNull ? 0 : (decimal)rdr["VehicleAllowance"];
            occupation.Notes = rdr["Notes"] is DBNull ? string.Empty : (string)rdr["Notes"];
            occupation.IsActive = rdr["IsActive"] is DBNull ? false : (bool)rdr["IsActive"];
           
            return occupation;
        
        }

    }
}
