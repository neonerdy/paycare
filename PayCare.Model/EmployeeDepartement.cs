using System;

namespace PayCare.Model
{
    public class EmployeeDepartement
    {
        public Guid ID { get; set; }

        public Guid EmployeeId { get; set; }

        public DateTime EffectiveDate { get; set; }

        public Guid DepartementId { get; set; }

        public string Reference { get; set; }

    }
}
