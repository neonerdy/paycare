using System;

namespace PayCare.Model
{
    public class EmployeeStatus
    {
        public Guid ID { get; set; }

        public Guid EmployeeId { get; set; }

        public DateTime EffectiveDate { get; set; }

        public bool IsEnd { get; set; }

        public DateTime EndDate { get; set; }

        public string Status { get; set; }

        public string PaymentType { get; set; }

    }
}
