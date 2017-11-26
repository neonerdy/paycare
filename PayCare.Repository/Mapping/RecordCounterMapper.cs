using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityMap;
using PayCare.Model;
using System.Data;

namespace PayCare.Repository.Mapping
{
    public class RecordCounterMapper : IDataMapper<RecordCounter>
    {
        public RecordCounter Map(IDataReader rdr)
        {
            var recordCounter = new RecordCounter();

            recordCounter.ID = rdr["ID"] is DBNull ? Guid.Empty : (Guid)rdr["ID"];
            recordCounter.BranchCounter = rdr["BranchCounter"] is DBNull ? 0 : (int)rdr["BranchCounter"];
            recordCounter.DepartmentCounter = rdr["DepartmentCounter"] is DBNull ? 0 : (int)rdr["DepartmentCounter"];
            recordCounter.GradeCounter = rdr["GradeCounter"] is DBNull ? 0 : (int)rdr["GradeCounter"];
            recordCounter.OccupationCounter = rdr["OccupationCounter"] is DBNull ? 0 : (int)rdr["OccupationCounter"];
            recordCounter.InsuranceCounter = rdr["InsuranceCounter"] is DBNull ? 0 : (int)rdr["InsuranceCounter"];
            recordCounter.EmployeeCounter = rdr["EmployeeCounter"] is DBNull ? 0 : (int)rdr["EmployeeCounter"];
              
            return recordCounter;
        }
    }
}
