using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityMap;
using System.Data;
using PayCare.Model;


namespace PayCare.Repository.Mapping
{
    public class AbsenceMapper : IDataMapper<Absence>
    {
        public Absence Map(IDataReader rdr)
        {
            var absence = new Absence();

            absence.ID = rdr["ID"] is DBNull ? Guid.Empty : (Guid)rdr["ID"];

            absence.YearPeriod = rdr["YearPeriod"] is DBNull ? 0 : (int)rdr["YearPeriod"];
            absence.MonthPeriod = rdr["MonthPeriod"] is DBNull ? 0 : (int)rdr["MonthPeriod"];

            absence.AbsenceStartDate = rdr["AbsenceStartDate"] is DBNull ? DateTime.Now : (DateTime)rdr["AbsenceStartDate"];
            absence.AbsenceEndDate = rdr["AbsenceEndDate"] is DBNull ? DateTime.Now : (DateTime)rdr["AbsenceEndDate"];

            absence.EmployeeId = rdr["EmployeeId"] is DBNull ? Guid.Empty : (Guid)rdr["EmployeeId"];

            if (absence.Employee == null) absence.Employee = new Employee();
            absence.Employee.EmployeeCode = rdr["EmployeeCode"] is DBNull ? string.Empty : (string)rdr["EmployeeCode"];
            absence.Employee.EmployeeName = rdr["EmployeeName"] is DBNull ? string.Empty : (string)rdr["EmployeeName"];

            absence.Branch = rdr["Branch"] is DBNull ? string.Empty : (string)rdr["Branch"];
            absence.Department = rdr["Department"] is DBNull ? string.Empty : (string)rdr["Department"];
            

            absence.WorkDay = rdr["WorkDay"] is DBNull ? 0 : (int)rdr["WorkDay"];
            absence.OnLeaveDay = rdr["OnLeaveDay"] is DBNull ? 0 : (int)rdr["OnLeaveDay"];
            absence.OffDay = rdr["OffDay"] is DBNull ? 0 : (int)rdr["OffDay"];
            absence.Total = rdr["Total"] is DBNull ? 0 : (int)rdr["Total"];

            absence.CreatedDate = rdr["CreatedDate"] is DBNull ? DateTime.Now : (DateTime)rdr["CreatedDate"];
            absence.ModifiedDate = rdr["ModifiedDate"] is DBNull ? DateTime.Now : (DateTime)rdr["ModifiedDate"];
            absence.CreatedBy = rdr["CreatedBy"] is DBNull ? string.Empty : (string)rdr["CreatedBy"];
            absence.ModifiedBy = rdr["ModifiedBy"] is DBNull ? string.Empty : (string)rdr["ModifiedBy"];
            
            

            return absence;
        }

    }
}
