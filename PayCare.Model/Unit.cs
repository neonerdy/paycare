using System;

namespace PayCare.Model
{
    public class Unit
    {
        public Guid ID { get; set; }

        public string UnitCode { get; set; }

        public string UnitName { get; set; }

        public string UnitHead { get; set; }

        public string ViceHead { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        public string Email { get; set; }

        public string Notes { get; set; }

        public bool IsNonActive { get; set; }

    }
}
