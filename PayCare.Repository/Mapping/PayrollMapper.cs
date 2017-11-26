using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityMap;
using PayCare.Model;
using System.Data;

namespace PayCare.Repository.Mapping
{
    public class PayrollMapper : IDataMapper<Payroll>
    {
        public Payroll Map(IDataReader rdr)
        {
            var payroll = new Payroll();

            payroll.ID = rdr["p.ID"] is DBNull ? Guid.Empty : (Guid)rdr["p.ID"];
            payroll.YearPeriod = rdr["YearPeriod"] is DBNull ? 0 : (int)rdr["YearPeriod"];
            payroll.MonthPeriod = rdr["MonthPeriod"] is DBNull ? 0 : (int)rdr["MonthPeriod"];
            payroll.PayrollDate = rdr["PayrollDate"] is DBNull ? DateTime.Now : (DateTime)rdr["PayrollDate"];

            payroll.EmployeeId = rdr["EmployeeId"] is DBNull ? Guid.Empty : (Guid)rdr["EmployeeId"];
            if (payroll.Employee == null) payroll.Employee = new Employee();
            payroll.Employee.EmployeeCode = rdr["EmployeeCode"] is DBNull ? string.Empty : (string)rdr["EmployeeCode"];
            payroll.Employee.OldEmployeeCode = rdr["OldEmployeeCode"] is DBNull ? string.Empty : (string)rdr["OldEmployeeCode"];
            payroll.Employee.EmployeeName = rdr["EmployeeName"] is DBNull ? string.Empty : (string)rdr["EmployeeName"];
            
            payroll.Branch = rdr["Branch"] is DBNull ? string.Empty : (string)rdr["Branch"];
            payroll.Department = rdr["Department"] is DBNull ? string.Empty : (string)rdr["Department"];
            payroll.Grade = rdr["Grade"] is DBNull ? string.Empty : (string)rdr["Grade"];
            payroll.GradeLevel = rdr["GradeLevel"] is DBNull ? 0 : (int)rdr["GradeLevel"];
            payroll.Occupation = rdr["Occupation"] is DBNull ? string.Empty : (string)rdr["Occupation"];
            payroll.Status = rdr["Status"] is DBNull ? string.Empty : (string)rdr["Status"];
            payroll.PaymentType = rdr["PaymentType"] is DBNull ? string.Empty : (string)rdr["PaymentType"];

            payroll.WorkDay = rdr["WorkDay"] is DBNull ? 0 : (int)rdr["WorkDay"];
            payroll.OnLeaveDay = rdr["OnLeaveDay"] is DBNull ? 0 : (int)rdr["OnLeaveDay"];
            payroll.OffDay = rdr["OffDay"] is DBNull ? 0 : (int)rdr["OffDay"];
            payroll.TotalDay = rdr["TotalDay"] is DBNull ? 0 : (int)rdr["TotalDay"];
            

            //bank
            payroll.IsTransfer = rdr["p.IsTransfer"] is DBNull ? false : (bool)rdr["p.IsTransfer"];
            payroll.BankName = rdr["p.BankName"] is DBNull ? string.Empty : (string)rdr["p.BankName"];
            payroll.AccountNumber = rdr["p.AccountNumber"] is DBNull ? string.Empty : (string)rdr["p.AccountNumber"];
            
            //principal
            payroll.IsPrincipal = rdr["p.IsPrincipal"] is DBNull ? false : (bool)rdr["p.IsPrincipal"];
            payroll.Principal = rdr["Principal"] is DBNull ? string.Empty : (string)rdr["Principal"];
            payroll.PrincipalMainSalary = rdr["PrincipalMainSalary"] is DBNull ? 0 : (decimal)rdr["PrincipalMainSalary"];
            payroll.PrincipalTransport = rdr["PrincipalTransport"] is DBNull ? 0 : (decimal)rdr["PrincipalTransport"];
            payroll.PrincipalLunch = rdr["PrincipalLunch"] is DBNull ? 0 : (decimal)rdr["PrincipalLunch"];
            
            //tetap
            payroll.MainSalary = rdr["MainSalary"] is DBNull ? 0 : (decimal)rdr["MainSalary"];
            payroll.MainSalaryValue = rdr["MainSalaryValue"] is DBNull ? 0 : (decimal)rdr["MainSalaryValue"];

            payroll.OccupationAllowancePerMonth = rdr["OccupationAllowancePerMonth"] is DBNull ? 0 : (decimal)rdr["OccupationAllowancePerMonth"];
            payroll.FixedAllowancePerMonth = rdr["FixedAllowancePerMonth"] is DBNull ? 0 : (decimal)rdr["FixedAllowancePerMonth"];
            payroll.HealthAllowancePerMonth = rdr["HealthAllowancePerMonth"] is DBNull ? 0 : (decimal)rdr["HealthAllowancePerMonth"];
            payroll.CommunicationAllowancePerMonth = rdr["CommunicationAllowancePerMonth"] is DBNull ? 0 : (decimal)rdr["CommunicationAllowancePerMonth"];
            payroll.SupervisionAllowancePerMonth = rdr["SupervisionAllowancePerMonth"] is DBNull ? 0 : (decimal)rdr["SupervisionAllowancePerMonth"];
            payroll.OtherAllowance = rdr["OtherAllowance"] is DBNull ? 0 : (decimal)rdr["OtherAllowance"];
            payroll.TotalFixedAllowance = rdr["TotalFixedAllowance"] is DBNull ? 0 : (decimal)rdr["TotalFixedAllowance"];


            //tidak tetap
            payroll.IsFuelAllowance = rdr["p.IsFuelAllowance"] is DBNull ? false : (bool)rdr["p.IsFuelAllowance"];
            
            payroll.FuelAllowance = rdr["FuelAllowance"] is DBNull ? 0 : (decimal)rdr["FuelAllowance"];
            payroll.FuelValue = rdr["FuelValue"] is DBNull ? 0 : (decimal)rdr["FuelValue"];
            payroll.FuelDay = rdr["FuelDay"] is DBNull ? 0 : (int)rdr["FuelDay"];
            payroll.TotalFuel = rdr["TotalFuel"] is DBNull ? 0 : (decimal)rdr["TotalFuel"];

            payroll.VehicleAllowance = rdr["VehicleAllowance"] is DBNull ? 0 : (decimal)rdr["VehicleAllowance"];
            payroll.VehicleValue = rdr["VehicleValue"] is DBNull ? 0 : (decimal)rdr["VehicleValue"];
            payroll.VehicleDay = rdr["VehicleDay"] is DBNull ? 0 : (int)rdr["VehicleDay"];
            payroll.TotalVehicle = rdr["TotalVehicle"] is DBNull ? 0 : (decimal)rdr["TotalVehicle"];

            payroll.LunchAllowance = rdr["LunchAllowance"] is DBNull ? 0 : (decimal)rdr["LunchAllowance"];
            payroll.LunchValue = rdr["LunchValue"] is DBNull ? 0 : (decimal)rdr["LunchValue"];
            payroll.LunchDay = rdr["LunchDay"] is DBNull ? 0 : (int)rdr["LunchDay"];
            payroll.TotalLunch = rdr["TotalLunch"] is DBNull ? 0 : (decimal)rdr["TotalLunch"];

            payroll.TransportationAllowance = rdr["TransportationAllowance"] is DBNull ? 0 : (decimal)rdr["TransportationAllowance"];
            payroll.TransportationValue = rdr["TransportationValue"] is DBNull ? 0 : (decimal)rdr["TransportationValue"];          
            payroll.TransportationDay = rdr["TransportationDay"] is DBNull ? 0 : (int)rdr["TransportationDay"];
            payroll.TotalTransportation = rdr["TotalTransportation"] is DBNull ? 0 : (decimal)rdr["TotalTransportation"];

            //insentive
            payroll.IsIncentive = rdr["IsIncentive"] is DBNull ? false : (bool)rdr["IsIncentive"];
            payroll.Incentive = rdr["Incentive"] is DBNull ? 0 : (decimal)rdr["Incentive"];

            //lembur
            payroll.IsOverTime = rdr["IsOverTime"] is DBNull ? false : (bool)rdr["IsOverTime"];
            payroll.OverTime = rdr["OverTime"] is DBNull ? 0 : (decimal)rdr["OverTime"];

            payroll.TotalNonFixedAllowance = rdr["TotalNonFixedAllowance"] is DBNull ? 0 : (decimal)rdr["TotalNonFixedAllowance"];
            
            //potongan
            payroll.IsInsurance = rdr["p.IsInsurance"] is DBNull ? false : (bool)rdr["p.IsInsurance"];            
            payroll.InsuranceEmployeePercentage = rdr["InsuranceEmployeePercentage"] is DBNull ? 0 : (double)rdr["InsuranceEmployeePercentage"];
            payroll.InsuranceEmployeeAmount = rdr["InsuranceEmployeeAmount"] is DBNull ? 0 : (decimal)rdr["InsuranceEmployeeAmount"];
            payroll.PersonalDebt = rdr["PersonalDebt"] is DBNull ? 0 : (decimal)rdr["PersonalDebt"];

            payroll.IsTax = rdr["p.IsTax"] is DBNull ? false : (bool)rdr["p.IsTax"];            
            payroll.TaxAmount = rdr["TaxAmount"] is DBNull ? 0 : (decimal)rdr["TaxAmount"];
            payroll.OtherFee = rdr["OtherFee"] is DBNull ? 0 : (decimal)rdr["OtherFee"];
            payroll.TotalFee = rdr["TotalFee"] is DBNull ? 0 : (decimal)rdr["TotalFee"];

            //total
            payroll.GrandTotal = rdr["GrandTotal"] is DBNull ? 0 : (decimal)rdr["GrandTotal"];
            payroll.AmountInWords = rdr["AmountInWords"] is DBNull ? string.Empty : (string)rdr["AmountInWords"];

            payroll.InsuranceCompanyPercentage = rdr["InsuranceCompanyPercentage"] is DBNull ? 0 : (double)rdr["InsuranceCompanyPercentage"];
            payroll.InsuranceCompanyAmount = rdr["InsuranceCompanyAmount"] is DBNull ? 0 : (decimal)rdr["InsuranceCompanyAmount"];

            payroll.IsPaid = rdr["IsPaid"] is DBNull ? false : (bool)rdr["IsPaid"];

            payroll.CreatedDate = rdr["p.CreatedDate"] is DBNull ? DateTime.Now : (DateTime)rdr["p.CreatedDate"];
            payroll.ModifiedDate = rdr["p.ModifiedDate"] is DBNull ? DateTime.Now : (DateTime)rdr["p.ModifiedDate"];
            payroll.CreatedBy = rdr["p.CreatedBy"] is DBNull ? string.Empty : (string)rdr["p.CreatedBy"];
            payroll.ModifiedBy = rdr["p.ModifiedBy"] is DBNull ? string.Empty : (string)rdr["p.ModifiedBy"];
            
            return payroll;

        }
    }
}
