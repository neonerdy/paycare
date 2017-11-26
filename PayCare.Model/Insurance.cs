using System;

namespace PayCare.Model
{

    public class Insurance
    {
        public Guid ID { get; set; }

        public string InsuranceCode { get; set; }

        public string InsuranceName { get; set; }

        public string Notes { get; set; }

        public bool IsActive { get; set; }
    }
}
