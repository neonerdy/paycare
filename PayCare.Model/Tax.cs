using System;

namespace PayCare.Model
{
    public class Tax
    {
        public Guid ID { get; set; }

        public int MonthPeriod { get; set; }

        public int YearPeriod { get; set; }

        public Guid EmployeeId { get; set; }

        public Guid PTKPId { get; set; }

        public DateTime StartDate { get; set; }

        public bool MartialStatus { get; set; }

        public int NumberOfChild { get; set; }

        public decimal TotalSalary { get; set; }

        public decimal TotalTax { get; set; }

        public string AmountInWords { get; set; }

    }
}
