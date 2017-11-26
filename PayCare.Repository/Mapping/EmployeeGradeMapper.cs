using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayCare.Model;
using EntityMap;
using System.Data;

namespace PayCare.Repository.Mapping
{
    public class EmployeeGradeMapper : IDataMapper<EmployeeGrade>
    {

        public EmployeeGrade Map(IDataReader rdr)
        {
            var employeeGrade = new EmployeeGrade();

            employeeGrade.ID = rdr["ID"] is DBNull ? Guid.Empty : (Guid)rdr["ID"];
            employeeGrade.EmployeeId = rdr["EmployeeId"] is DBNull ? Guid.Empty : (Guid)rdr["EmployeeId"];
            employeeGrade.EffectiveDate = rdr["EffectiveDate"] is DBNull ? DateTime.Now : (DateTime)rdr["EffectiveDate"];
            employeeGrade.GradeId = rdr["GradeId"] is DBNull ? Guid.Empty : (Guid)rdr["GradeId"];
            employeeGrade.GradeName = rdr["GradeName"] is DBNull ? string.Empty : (string)rdr["GradeName"];
            employeeGrade.GradeLevel = rdr["GradeLevel"] is DBNull ? 0 : (int)rdr["GradeLevel"];

            return employeeGrade;
        }

    }
}
