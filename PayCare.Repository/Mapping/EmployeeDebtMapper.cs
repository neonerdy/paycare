using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityMap;
using PayCare.Model;
using System.Data;

namespace PayCare.Repository.Mapping
{
    public class EmployeeDebtMapper : IDataMapper<EmployeeDebt>
    {
        public EmployeeDebt Map(IDataReader rdr)
        {
            var employeeDebt = new EmployeeDebt();

            employeeDebt.ID = rdr["ID"] is DBNull ? Guid.Empty : (Guid)rdr["ID"];
            employeeDebt.EmployeeId = rdr["EmployeeId"] is DBNull ? Guid.Empty : (Guid)rdr["EmployeeId"];

            if (employeeDebt.Employee == null) employeeDebt.Employee = new Employee();
            employeeDebt.Employee.EmployeeCode = rdr["EmployeeCode"] is DBNull ? string.Empty : (string)rdr["EmployeeCode"];
            employeeDebt.Employee.EmployeeName = rdr["EmployeeName"] is DBNull ? string.Empty : (string)rdr["EmployeeName"];

            employeeDebt.DebtDate = rdr["DebtDate"] is DBNull ? DateTime.Now : (DateTime)rdr["DebtDate"];
            employeeDebt.TotalAmount = rdr["TotalAmount"] is DBNull ? 0 : (decimal)rdr["TotalAmount"];
            employeeDebt.LongTerm = rdr["LongTerm"] is DBNull ? 0 : (int)rdr["LongTerm"];
            employeeDebt.Installment = rdr["Installment"] is DBNull ? 0 : (decimal)rdr["Installment"];
            

            employeeDebt.AmountInWords = rdr["AmountInWords"] is DBNull ? string.Empty : (string)rdr["AmountInWords"];
            employeeDebt.Notes = rdr["Notes"] is DBNull ? string.Empty : (string)rdr["Notes"];
            employeeDebt.IsStatus = rdr["IsStatus"] is DBNull ? false : (bool)rdr["IsStatus"];
            
            employeeDebt.CreatedDate = rdr["CreatedDate"] is DBNull ? DateTime.Now : (DateTime)rdr["CreatedDate"];
            employeeDebt.ModifiedDate = rdr["ModifiedDate"] is DBNull ? DateTime.Now : (DateTime)rdr["ModifiedDate"];
            employeeDebt.CreatedBy = rdr["CreatedBy"] is DBNull ? string.Empty : (string)rdr["CreatedBy"];
            employeeDebt.ModifiedBy = rdr["ModifiedBy"] is DBNull ? string.Empty : (string)rdr["ModifiedBy"];
            
            return employeeDebt;

        }

    }
}
