using System;

namespace PayCare.Model
{
    public class THR
    {
        public Guid ID { get; set; }

        public Guid EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public string Branch { get; set; }

        public string Department { get; set; }

        public string Grade { get; set; }

        public int GradeLevel { get; set; }

        public string Occupation { get; set; }

        public string Status { get; set; }

        public bool IsTransfer { get; set; }

        public string BankName { get; set; }

        public string AccountNumber { get; set; }

        public string PaymentType { get; set; }

        public int YearPeriod { get; set; }

        public string HolidayType { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EffectiveDate { get; set; }

        public int YearOfWork { get; set; }

        public int MonthOfWork { get; set; }

        public int DayOfWork { get; set; }

        public decimal MainSalary { get; set; }

        public decimal Amount { get; set; }

        public decimal OtherAmount { get; set; }

        public decimal TotalAmount { get; set; }

        public string AmountInWords { get; set; }

        public bool IsPaid { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }

    }
}
