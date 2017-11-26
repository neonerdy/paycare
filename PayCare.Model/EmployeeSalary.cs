using System;

namespace PayCare.Model
{
    public class EmployeeSalary
    {
        public Guid ID { get; set; }

        public Guid EmployeeId { get; set; }

        public DateTime EffectiveDate { get; set; }

        public decimal MainSalary { get; set; }

        public decimal OccupationAllowancePerMonth { get; set; }
        
        public decimal FixedAllowancePerMonth { get; set; }

        public decimal HealthAllowancePerMonth { get; set; }

        public decimal CommunicationAllowancePerMonth { get; set; }

        public decimal SupervisionAllowancePerMonth { get; set; }

        public decimal OtherAllowance { get; set; }

        public decimal FuelAllowancePerDays { get; set; }

        public decimal VehicleAllowancePerDays { get; set; }

        public decimal LunchAllowancePerDays { get; set; }

        public decimal TransportationAllowancePerDays { get; set; }

        public decimal JamsostekAmount { get; set; }

        public decimal PersonalDebt { get; set; }

        public decimal OtherFee { get; set; }


    }
}
