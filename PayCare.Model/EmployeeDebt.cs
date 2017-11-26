using System;

namespace PayCare.Model
{
    public class EmployeeDebt
    {
        public Guid ID { get; set; }

        public Guid EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public DateTime DebtDate { get; set; }

        public decimal TotalAmount { get; set; }

        public int LongTerm { get; set; }

        public decimal Installment { get; set; }

        public string AmountInWords { get; set; }

        public string Notes { get; set; }

        public bool IsStatus { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }

             
    }
}
