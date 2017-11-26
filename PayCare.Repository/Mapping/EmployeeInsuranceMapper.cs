using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayCare.Model;
using EntityMap;

using System.Data;

namespace PayCare.Repository.Mapping
{
    public class EmployeeInsuranceMapper : IDataMapper<EmployeeInsurance>
    {
    
        public EmployeeInsurance Map(IDataReader rdr)        {
            var employeeInsurance = new EmployeeInsurance();

            employeeInsurance.ID = rdr["ID"] is DBNull ? Guid.Empty : (Guid)rdr["ID"];
            employeeInsurance.EmployeeId = rdr["EmployeeId"] is DBNull ? Guid.Empty : (Guid)rdr["EmployeeId"];
            employeeInsurance.InsuranceId = rdr["InsuranceId"] is DBNull ? Guid.Empty : (Guid)rdr["InsuranceId"];
            employeeInsurance.InsuranceName = rdr["InsuranceName"] is DBNull ? string.Empty : (string)rdr["InsuranceName"];
            employeeInsurance.InsuranceProgramId = rdr["InsuranceProgramId"] is DBNull ? Guid.Empty : (Guid)rdr["InsuranceProgramId"];
            employeeInsurance.InsuranceProgramName = rdr["Program"] is DBNull ? string.Empty : (string)rdr["Program"];
            employeeInsurance.EffectiveDate = rdr["EffectiveDate"] is DBNull ? DateTime.Now : (DateTime)rdr["EffectiveDate"];
            employeeInsurance.EndDate = rdr["EndDate"] is DBNull ? DateTime.Now : (DateTime)rdr["EndDate"];
            employeeInsurance.InsuranceNumber = rdr["InsuranceNumber"] is DBNull ? string.Empty : (string)rdr["InsuranceNumber"];
                      

            return employeeInsurance;       
        
        }

    }
}
