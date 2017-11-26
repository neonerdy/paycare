using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayCare.Model;
using EntityMap;
using System.Data;

namespace PayCare.Repository.Mapping
{
    public class EmployeeFamilyMapper : IDataMapper<EmployeeFamily> 
    {
        public EmployeeFamily Map(IDataReader rdr)
        {
            var employeeFamily = new EmployeeFamily();

            employeeFamily.ID = rdr["ID"] is DBNull ? Guid.Empty : (Guid)rdr["ID"];
            employeeFamily.EmployeeId = rdr["EmployeeId"] is DBNull ? Guid.Empty : (Guid)rdr["EmployeeId"];
            employeeFamily.FamilyName = rdr["FamilyName"] is DBNull ? string.Empty : (string)rdr["FamilyName"];
            employeeFamily.Status = rdr["Status"] is DBNull ? 0 : (int)rdr["Status"];
            employeeFamily.Education = rdr["Education"] is DBNull ? string.Empty : (string)rdr["Education"];
            employeeFamily.BirthPlace = rdr["BirthPlace"] is DBNull ? string.Empty : (string)rdr["BirthPlace"];
            employeeFamily.BirthDate = rdr["BirthDate"] is DBNull ? DateTime.Now : (DateTime)rdr["BirthDate"];
            employeeFamily.Job = rdr["Job"] is DBNull ? string.Empty : (string)rdr["Job"];
            employeeFamily.IsInsurance = rdr["IsInsurance"] is DBNull ? false : (bool)rdr["IsInsurance"];

        
            return employeeFamily;

        }

    }
}
