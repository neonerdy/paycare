using System;

namespace PayCare.Model
{
    public class Payroll
    {
        public Guid ID { get; set; }

        public int MonthPeriod { get; set; }

        public int YearPeriod { get; set; }

        public DateTime PayrollDate { get; set; }

        public Guid EmployeeId { get; set; }

        public Employee Employee { get; set; }
                
        public string Branch { get; set; }

        public string Department { get; set; }

        public string Grade { get; set; }

        public int GradeLevel { get; set; }

        public string Occupation { get; set; }

        public string Status { get; set; }

        public string PaymentType { get; set; }

        public int WorkDay { get; set; }

        public int OnLeaveDay { get; set; }

        public int OffDay { get; set; }

        public int TotalDay { get; set; }

        public bool IsTransfer { get; set; }

        public string BankName { get; set; }

        public string AccountNumber { get; set; }

        public bool IsPrincipal { get; set; }

        public string Principal { get; set; }

        public decimal PrincipalMainSalary { get; set; }

        public decimal PrincipalTransport { get; set; }

        public decimal PrincipalLunch { get; set; }

        public decimal MainSalary { get; set; }

        public decimal MainSalaryValue { get; set; }

        public decimal OccupationAllowancePerMonth { get; set; }

        public decimal FixedAllowancePerMonth { get; set; }

        public decimal HealthAllowancePerMonth { get; set; }

        public decimal CommunicationAllowancePerMonth { get; set; }

        public decimal SupervisionAllowancePerMonth { get; set; }

        public decimal OtherAllowance { get; set; }

        public decimal TotalFixedAllowance { get; set; }

        public bool IsFuelAllowance { get; set; }

        public decimal FuelAllowance { get; set; }

        public decimal FuelValue { get; set; }

        public int FuelDay { get; set; }

        public decimal TotalFuel { get; set; }

        public decimal VehicleAllowance { get; set; }

        public decimal VehicleValue { get; set; }

        public int VehicleDay { get; set; }

        public decimal TotalVehicle { get; set; }

        public decimal LunchAllowance { get; set; }

        public decimal LunchValue { get; set; }

        public int LunchDay { get; set; }

        public decimal TotalLunch { get; set; }

        public decimal TransportationAllowance { get; set; }

        public decimal TransportationValue { get; set; }

        public decimal TransportationDay { get; set; }

        public decimal TotalTransportation { get; set; }

        public bool IsIncentive { get; set; }

        public decimal Incentive { get; set; }

        public bool IsOverTime { get; set; }

        public decimal OverTime { get; set; }

        public decimal TotalNonFixedAllowance { get; set; }

        public bool IsInsurance { get; set; }

        public double InsuranceEmployeePercentage { get; set; }

        public decimal InsuranceEmployeeAmount { get; set; }

        public decimal PersonalDebt { get; set; }

        public bool IsTax { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal OtherFee { get; set; }

        public decimal TotalFee { get; set; }

        public decimal GrandTotal { get; set; }

        public string AmountInWords { get; set; }

        public double InsuranceCompanyPercentage { get; set; }

        public decimal InsuranceCompanyAmount { get; set; }

        public bool IsPaid { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }
         

    }
}
