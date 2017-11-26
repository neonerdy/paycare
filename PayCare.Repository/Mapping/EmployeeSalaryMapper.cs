using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayCare.Model;
using EntityMap;

using System.Data;

namespace PayCare.Repository.Mapping
{
    public class EmployeeSalaryMapper : IDataMapper<EmployeeSalary>
    {
        public EmployeeSalary Map(IDataReader rdr)
        {
            var employeeSalary = new EmployeeSalary();

            employeeSalary.ID = rdr["ID"] is DBNull ? Guid.Empty : (Guid)rdr["ID"];
            employeeSalary.EmployeeId = rdr["EmployeeId"] is DBNull ? Guid.Empty : (Guid)rdr["EmployeeId"];
            employeeSalary.EffectiveDate = rdr["EffectiveDate"] is DBNull ? DateTime.Now : (DateTime)rdr["EffectiveDate"];
            employeeSalary.MainSalary = rdr["MainSalary"] is DBNull ? 0 : (decimal)rdr["MainSalary"];
            employeeSalary.OccupationAllowancePerMonth = rdr["OccupationAllowancePerMonth"] is DBNull ? 0 : (decimal)rdr["OccupationAllowancePerMonth"];
            employeeSalary.FixedAllowancePerMonth = rdr["FixedAllowancePerMonth"] is DBNull ? 0 : (decimal)rdr["FixedAllowancePerMonth"];
            employeeSalary.HealthAllowancePerMonth = rdr["HealthAllowancePerMonth"] is DBNull ? 0 : (decimal)rdr["HealthAllowancePerMonth"];
            employeeSalary.CommunicationAllowancePerMonth = rdr["CommunicationAllowancePerMonth"] is DBNull ? 0 : (decimal)rdr["CommunicationAllowancePerMonth"];
            employeeSalary.SupervisionAllowancePerMonth = rdr["SupervisionAllowancePerMonth"] is DBNull ? 0 : (decimal)rdr["SupervisionAllowancePerMonth"];
            employeeSalary.OtherAllowance = rdr["OtherAllowance"] is DBNull ? 0 : (decimal)rdr["OtherAllowance"];
            employeeSalary.FuelAllowancePerDays = rdr["FuelAllowancePerDays"] is DBNull ? 0 : (decimal)rdr["FuelAllowancePerDays"];
            employeeSalary.VehicleAllowancePerDays = rdr["VehicleAllowancePerDays"] is DBNull ? 0 : (decimal)rdr["VehicleAllowancePerDays"];
            employeeSalary.LunchAllowancePerDays = rdr["LunchAllowancePerDays"] is DBNull ? 0 : (decimal)rdr["LunchAllowancePerDays"];
            employeeSalary.TransportationAllowancePerDays = rdr["TransportationAllowancePerDays"] is DBNull ? 0 : (decimal)rdr["TransportationAllowancePerDays"];
            employeeSalary.JamsostekAmount = rdr["JamsostekAmount"] is DBNull ? 0 : (decimal)rdr["JamsostekAmount"];
            employeeSalary.PersonalDebt = rdr["PersonalDebt"] is DBNull ? 0 : (decimal)rdr["PersonalDebt"];
            employeeSalary.OtherFee = rdr["OtherFee"] is DBNull ? 0 : (decimal)rdr["OtherFee"];
                              

            return employeeSalary;
        }

    }
}
