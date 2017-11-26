using System;

namespace PayCare.Model
{
    public class Occupation
    {
        public Guid ID { get; set; }

        public string OccupationCode { get; set; }

        public string OccupationName { get; set; }

        public decimal HealthAllowance { get; set; }

        public decimal VehicleAllowance { get; set; }

        public string Notes { get; set; }

        public bool IsActive { get; set; }

    }
}
