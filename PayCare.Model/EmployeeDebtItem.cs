using System;

namespace PayCare.Model
{
    public class EmployeeDebtItemValue
    {
        public decimal Amount { get; set; }

    }

    public class EmployeeDebtItem
    {
        public Guid ID { get; set; }

        public Guid EmployeeDebtId { get; set; }

        public EmployeeDebt EmployeeDebt { get; set; }

        public int InstallmentCounter { get; set; }

        public decimal AmountPerMonth { get; set; }

        public bool IsPaid { get; set; }

        public bool IsIncludePayroll { get; set; }

        public DateTime PaymentDate { get; set; }

    }
}
