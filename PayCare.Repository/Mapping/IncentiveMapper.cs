using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityMap;
using PayCare.Model;
using System.Data;

namespace PayCare.Repository.Mapping
{

    public class IncentiveValueMapper : IDataMapper<IncentiveValue>
    {

        public IncentiveValue Map(IDataReader rdr)
        {
            var incentiveValue = new IncentiveValue();

            incentiveValue.amount = rdr["Amount"] is DBNull ? 0 : (decimal)rdr["Amount"];

            return incentiveValue;
        }

    }


    public class IncentiveMapper : IDataMapper<Incentive>
    {
        public Incentive Map(IDataReader rdr)
        {
            var incentive = new Incentive();

            incentive.ID = rdr["ID"] is DBNull ? Guid.Empty : (Guid)rdr["ID"];
            incentive.EmployeeId = rdr["EmployeeId"] is DBNull ? Guid.Empty : (Guid)rdr["EmployeeId"];

            if (incentive.Employee == null) incentive.Employee = new Employee();
            incentive.Employee.EmployeeCode = rdr["EmployeeCode"] is DBNull ? string.Empty : (string)rdr["EmployeeCode"];
            incentive.Employee.EmployeeName = rdr["EmployeeName"] is DBNull ? string.Empty : (string)rdr["EmployeeName"];

            incentive.Branch = rdr["Branch"] is DBNull ? string.Empty : (string)rdr["Branch"];
            incentive.Department = rdr["Department"] is DBNull ? string.Empty : (string)rdr["Department"];

            incentive.IsTransfer = rdr["IsTransfer"] is DBNull ? false : (bool)rdr["IsTransfer"];
            incentive.BankName = rdr["BankName"] is DBNull ? string.Empty : (string)rdr["BankName"];
            incentive.AccountNumber = rdr["AccountNumber"] is DBNull ? string.Empty : (string)rdr["AccountNumber"];
            
            incentive.YearPeriod = rdr["YearPeriod"] is DBNull ? 0 : (int)rdr["YearPeriod"];
            incentive.MonthPeriod = rdr["MonthPeriod"] is DBNull ? 0 : (int)rdr["MonthPeriod"];

            incentive.Amount = rdr["Amount"] is DBNull ? 0 : (decimal)rdr["Amount"];

            incentive.AmountInWords = rdr["AmountInWords"] is DBNull ? string.Empty : (string)rdr["AmountInWords"];
            incentive.Notes = rdr["Notes"] is DBNull ? string.Empty : (string)rdr["Notes"];

            incentive.IsIncludePayroll = rdr["IsIncludePayroll"] is DBNull ? false : (bool)rdr["IsIncludePayroll"];
            incentive.IsPaid = rdr["IsPaid"] is DBNull ? false : (bool)rdr["IsPaid"];            

            incentive.CreatedDate = rdr["CreatedDate"] is DBNull ? DateTime.Now : (DateTime)rdr["CreatedDate"];
            incentive.ModifiedDate = rdr["ModifiedDate"] is DBNull ? DateTime.Now : (DateTime)rdr["ModifiedDate"];
            incentive.CreatedBy = rdr["CreatedBy"] is DBNull ? string.Empty : (string)rdr["CreatedBy"];
            incentive.ModifiedBy = rdr["ModifiedBy"] is DBNull ? string.Empty : (string)rdr["ModifiedBy"];
            
            return incentive;

        }

    }
}
