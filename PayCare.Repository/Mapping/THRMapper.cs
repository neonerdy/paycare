using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityMap;
using System.Data;
using PayCare.Model;


namespace PayCare.Repository.Mapping
{
    public class THRMapper : IDataMapper<THR>
    {
        public THR Map(IDataReader rdr)
        {
            var thr = new THR();

            thr.ID = rdr["t.ID"] is DBNull ? Guid.Empty : (Guid)rdr["t.ID"];

            thr.EmployeeId = rdr["EmployeeId"] is DBNull ? Guid.Empty : (Guid)rdr["EmployeeId"];

            if (thr.Employee == null) thr.Employee = new Employee();
            thr.Employee.EmployeeCode = rdr["EmployeeCode"] is DBNull ? string.Empty : (string)rdr["EmployeeCode"];
            thr.Employee.EmployeeName = rdr["EmployeeName"] is DBNull ? string.Empty : (string)rdr["EmployeeName"];
            thr.Employee.BankName = rdr["e.BankName"] is DBNull ? string.Empty : (string)rdr["e.BankName"];
            thr.Employee.AccountNumber = rdr["e.AccountNumber"] is DBNull ? string.Empty : (string)rdr["e.AccountNumber"];
            thr.Employee.IsTransfer = rdr["e.IsTransfer"] is DBNull ? false : (bool)rdr["e.IsTransfer"];

            thr.Branch = rdr["Branch"] is DBNull ? string.Empty : (string)rdr["Branch"];
            thr.Department = rdr["Department"] is DBNull ? string.Empty : (string)rdr["Department"];
            thr.Grade = rdr["Grade"] is DBNull ? string.Empty : (string)rdr["Grade"];
            thr.GradeLevel = rdr["GradeLevel"] is DBNull ? 0 : (int)rdr["GradeLevel"];
            thr.Occupation = rdr["Occupation"] is DBNull ? string.Empty : (string)rdr["Occupation"];
            thr.Status = rdr["Status"] is DBNull ? string.Empty : (string)rdr["Status"];
            thr.PaymentType = rdr["PaymentType"] is DBNull ? string.Empty : (string)rdr["PaymentType"];

            thr.IsTransfer = rdr["t.IsTransfer"] is DBNull ? false : (bool)rdr["t.IsTransfer"];
            thr.BankName = rdr["t.BankName"] is DBNull ? string.Empty : (string)rdr["t.BankName"];
            thr.AccountNumber = rdr["t.AccountNumber"] is DBNull ? string.Empty : (string)rdr["t.AccountNumber"];
            

            thr.HolidayType = rdr["HolidayType"] is DBNull ? string.Empty : (string)rdr["HolidayType"];
            thr.StartDate = rdr["t.StartDate"] is DBNull ? DateTime.Now : (DateTime)rdr["t.StartDate"];
            thr.EffectiveDate = rdr["EffectiveDate"] is DBNull ? DateTime.Now : (DateTime)rdr["EffectiveDate"];
            thr.YearOfWork = rdr["YearOfWork"] is DBNull ? 0 : (int)rdr["YearOfWork"];
            thr.MonthOfWork = rdr["MonthOfWork"] is DBNull ? 0 : (int)rdr["MonthOfWork"];
            thr.DayOfWork = rdr["DayOfWork"] is DBNull ? 0 : (int)rdr["DayOfWork"];

            thr.MainSalary = rdr["MainSalary"] is DBNull ? 0 : (decimal)rdr["MainSalary"];
            thr.Amount = rdr["Amount"] is DBNull ? 0 : (decimal)rdr["Amount"];
            thr.OtherAmount = rdr["OtherAmount"] is DBNull ? 0 : (decimal)rdr["OtherAmount"];
            thr.TotalAmount = rdr["TotalAmount"] is DBNull ? 0 : (decimal)rdr["TotalAmount"];
            
            thr.AmountInWords = rdr["AmountInWords"] is DBNull ? string.Empty : (string)rdr["AmountInWords"];
            thr.IsPaid = rdr["IsPaid"] is DBNull ? false : (bool)rdr["IsPaid"];

            thr.CreatedDate = rdr["t.CreatedDate"] is DBNull ? DateTime.Now : (DateTime)rdr["t.CreatedDate"];
            thr.ModifiedDate = rdr["t.ModifiedDate"] is DBNull ? DateTime.Now : (DateTime)rdr["t.ModifiedDate"];
            thr.CreatedBy = rdr["t.CreatedBy"] is DBNull ? string.Empty : (string)rdr["t.CreatedBy"];
            thr.ModifiedBy = rdr["t.ModifiedBy"] is DBNull ? string.Empty : (string)rdr["t.ModifiedBy"];
            
            return thr;
        }

    }
}
