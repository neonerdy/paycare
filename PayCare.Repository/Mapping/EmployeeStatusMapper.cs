using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayCare.Model;
using EntityMap;

using System.Data;

namespace PayCare.Repository.Mapping
{
    public class EmployeeStatusMapper : IDataMapper<EmployeeStatus>
    {

        public EmployeeStatus Map(IDataReader rdr)
        {
            var employeeStatus = new EmployeeStatus();

            employeeStatus.ID = rdr["ID"] is DBNull ? Guid.Empty : (Guid)rdr["ID"];
            employeeStatus.EmployeeId = rdr["EmployeeId"] is DBNull ? Guid.Empty : (Guid)rdr["EmployeeId"];
            employeeStatus.EffectiveDate = rdr["EffectiveDate"] is DBNull ? DateTime.Now : (DateTime)rdr["EffectiveDate"];
            employeeStatus.IsEnd = rdr["IsEnd"] is DBNull ? false : (bool)rdr["IsEnd"];
            employeeStatus.EndDate = rdr["EndDate"] is DBNull ? DateTime.Now : (DateTime)rdr["EndDate"];
            employeeStatus.Status = rdr["Status"] is DBNull ? string.Empty : (string)rdr["Status"];
            employeeStatus.PaymentType = rdr["PaymentType"] is DBNull ? string.Empty : (string)rdr["PaymentType"];
                        
            return employeeStatus;
        
        }

    }
}
