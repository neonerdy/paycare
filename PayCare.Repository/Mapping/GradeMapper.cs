using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityMap;
using PayCare.Model;
using System.Data;

namespace PayCare.Repository.Mapping
{
    public class GradeMapper : IDataMapper<Grade>
    {
        public Grade Map(IDataReader rdr)
        {
            var grade = new Grade();

            grade.ID = rdr["ID"] is DBNull ? Guid.Empty : (Guid)rdr["ID"];
            grade.GradeCode = rdr["GradeCode"] is DBNull ? string.Empty : (string)rdr["GradeCode"];
            grade.GradeName = rdr["GradeName"] is DBNull ? string.Empty : (string)rdr["GradeName"];
            grade.GradeLevel = rdr["GradeLevel"] is DBNull ? 0 : (int)rdr["GradeLevel"];
            grade.Notes = rdr["Notes"] is DBNull ? string.Empty : (string)rdr["Notes"];
            grade.IsActive = rdr["IsActive"] is DBNull ? false : (bool)rdr["IsActive"];
         
            return grade;

        }

    }
}
