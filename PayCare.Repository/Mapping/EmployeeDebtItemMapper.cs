using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityMap;
using PayCare.Model;
using System.Data;

namespace PayCare.Repository.Mapping
{
    public class EmployeeDebtItemValueMapper : IDataMapper<EmployeeDebtItemValue>
    {
   
        public EmployeeDebtItemValue Map(IDataReader rdr)
        {
            var employeeDebtItemValue = new EmployeeDebtItemValue();

            employeeDebtItemValue.Amount = rdr["Amount"] is DBNull ? 0 : (decimal)rdr["Amount"];

            return employeeDebtItemValue;
        }

    }


    public class EmployeeDebtItemMapper : IDataMapper<EmployeeDebtItem>
    {
        public EmployeeDebtItem Map(IDataReader rdr)
        {
            var employeeDebtItem = new EmployeeDebtItem();

            employeeDebtItem.ID = rdr["ID"] is DBNull ? Guid.Empty : (Guid)rdr["ID"];
            employeeDebtItem.EmployeeDebtId = rdr["EmployeeDebtId"] is DBNull ? Guid.Empty : (Guid)rdr["EmployeeDebtId"];

            if (employeeDebtItem.EmployeeDebt == null) employeeDebtItem.EmployeeDebt = new EmployeeDebt();
            employeeDebtItem.EmployeeDebt.DebtDate = rdr["DebtDate"] is DBNull ? DateTime.Now : (DateTime)rdr["DebtDate"];

            employeeDebtItem.InstallmentCounter = rdr["InstallmentCounter"] is DBNull ? 0 : (int)rdr["InstallmentCounter"];
            employeeDebtItem.AmountPerMonth = rdr["AmountPerMonth"] is DBNull ? 0 : (decimal)rdr["AmountPerMonth"];
            employeeDebtItem.IsPaid = rdr["IsPaid"] is DBNull ? false : (bool)rdr["IsPaid"];
            employeeDebtItem.IsIncludePayroll = rdr["IsIncludePayroll"] is DBNull ? false : (bool)rdr["IsIncludePayroll"];
            
            employeeDebtItem.PaymentDate = rdr["PaymentDate"] is DBNull ? DateTime.Now : (DateTime)rdr["PaymentDate"];
            
            return employeeDebtItem;

        }

    }
}
