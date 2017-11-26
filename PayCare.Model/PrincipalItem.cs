using System;

namespace PayCare.Model
{
    public class PrincipalItemValue
    {
        public decimal MainSalary { get; set; }

        public decimal LunchAllowance { get; set; }

        public decimal TransportationAllowance { get; set; }
    }

    public class PrincipalItem
    {
        public Guid ID { get; set; }

        public Guid PrincipalId { get; set; }

        public Principal Principal { get; set; }

        public DateTime EffectiveDate { get; set; }

        public string Reference { get; set; }

        public decimal MainSalary { get; set; }

        public decimal LunchAllowance { get; set; }

        public decimal TransportationAllowance { get; set; }
    }
}
