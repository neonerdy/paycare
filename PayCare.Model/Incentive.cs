using System;

namespace PayCare.Model
{

    public class IncentiveValue
    {
        public decimal amount { get; set; }

    }

    public class Incentive
    {
        public Guid ID { get; set; }

        public Guid EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public string Branch { get; set; }

        public string Department { get; set; }

        public bool IsTransfer { get; set; }

        public string BankName { get; set; }

        public string AccountNumber { get; set; }

        public int MonthPeriod { get; set; }

        public int YearPeriod { get; set; }

        public decimal Amount { get; set; }

        public string AmountInWords { get; set; }

        public string Notes { get; set; }

        public bool IsIncludePayroll { get; set; }

        public bool IsPaid { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }

             
    }
}
