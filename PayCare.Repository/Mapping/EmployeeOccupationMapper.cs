using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayCare.Model;
using EntityMap;

using System.Data;


namespace PayCare.Repository.Mapping
{
    public class EmployeeOccupationMapper : IDataMapper<EmployeeOccupation>
    {

        public EmployeeOccupation Map(IDataReader rdr)
        {
            var employeeOccupation = new EmployeeOccupation();

            employeeOccupation.ID = rdr["ID"] is DBNull ? Guid.Empty : (Guid)rdr["ID"];
            employeeOccupation.EmployeeId = rdr["EmployeeId"] is DBNull ? Guid.Empty : (Guid)rdr["EmployeeId"];
            employeeOccupation.EffectiveDate = rdr["EffectiveDate"] is DBNull ? DateTime.Now : (DateTime)rdr["EffectiveDate"];
            employeeOccupation.OccupationId = rdr["OccupationId"] is DBNull ? Guid.Empty : (Guid)rdr["OccupationId"];
            employeeOccupation.OccupationName = rdr["OccupationName"] is DBNull ? string.Empty : (string)rdr["OccupationName"];
            employeeOccupation.IsTaskForce = rdr["IsTaskForce"] is DBNull ? false : (bool)rdr["IsTaskForce"];
            
            return employeeOccupation;
        }

    }
}
