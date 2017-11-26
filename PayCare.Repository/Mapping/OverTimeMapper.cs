using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityMap;
using PayCare.Model;
using System.Data;

namespace PayCare.Repository.Mapping
{

    public class OverTimeValueMapper : IDataMapper<OverTimeValue>
    {

        public OverTimeValue Map(IDataReader rdr)
        {
            var overTimeValue = new OverTimeValue();

            overTimeValue.amount = rdr["Amount"] is DBNull ? 0 : (decimal)rdr["Amount"];

            return overTimeValue;
        }

    }

    public class OverTimeMapper : IDataMapper<OverTime>
    {
        public OverTime Map(IDataReader rdr)
        {
            var overTime = new OverTime();

            overTime.ID = rdr["ID"] is DBNull ? Guid.Empty : (Guid)rdr["ID"];
            overTime.EmployeeId = rdr["EmployeeId"] is DBNull ? Guid.Empty : (Guid)rdr["EmployeeId"];

            if (overTime.Employee == null) overTime.Employee = new Employee();
            overTime.Employee.EmployeeCode = rdr["EmployeeCode"] is DBNull ? string.Empty : (string)rdr["EmployeeCode"];
            overTime.Employee.EmployeeName = rdr["EmployeeName"] is DBNull ? string.Empty : (string)rdr["EmployeeName"];

            overTime.Branch = rdr["Branch"] is DBNull ? string.Empty : (string)rdr["Branch"];
            overTime.Department = rdr["Department"] is DBNull ? string.Empty : (string)rdr["Department"];
            overTime.YearPeriod = rdr["YearPeriod"] is DBNull ? 0 : (int)rdr["YearPeriod"];
            overTime.MonthPeriod = rdr["MonthPeriod"] is DBNull ? 0 : (int)rdr["MonthPeriod"];

            overTime.DayType = rdr["DayType"] is DBNull ? 0 : (int)rdr["DayType"];
            overTime.OverTimeDate = rdr["OverTimeDate"] is DBNull ? DateTime.Now : (DateTime)rdr["OverTimeDate"];

            overTime.StartHour = rdr["StartHour"] is DBNull ? string.Empty : (string)rdr["StartHour"];
            overTime.EndHour = rdr["EndHour"] is DBNull ? string.Empty : (string)rdr["EndHour"];
            overTime.TotalInMinute = rdr["TotalInMinute"] is DBNull ? 0 : (int)rdr["TotalInMinute"];
            overTime.TotalInHour = rdr["TotalInHour"] is DBNull ? string.Empty : (string)rdr["TotalInHour"];

            overTime.Amount = rdr["Amount"] is DBNull ? 0 : (decimal)rdr["Amount"];
            overTime.AmountInWords = rdr["AmountInWords"] is DBNull ? string.Empty : (string)rdr["AmountInWords"];
            overTime.Notes = rdr["Notes"] is DBNull ? string.Empty : (string)rdr["Notes"];

            overTime.IsIncludePayroll = rdr["IsIncludePayroll"] is DBNull ? false : (bool)rdr["IsIncludePayroll"];
            overTime.IsPaid = rdr["IsPaid"] is DBNull ? false : (bool)rdr["IsPaid"];            


            overTime.CreatedDate = rdr["CreatedDate"] is DBNull ? DateTime.Now : (DateTime)rdr["CreatedDate"];
            overTime.ModifiedDate = rdr["ModifiedDate"] is DBNull ? DateTime.Now : (DateTime)rdr["ModifiedDate"];
            overTime.CreatedBy = rdr["CreatedBy"] is DBNull ? string.Empty : (string)rdr["CreatedBy"];
            overTime.ModifiedBy = rdr["ModifiedBy"] is DBNull ? string.Empty : (string)rdr["ModifiedBy"];
            
            return overTime;

        }
    }
}
