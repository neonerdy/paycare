using System;

namespace PayCare.Model
{
    public class InsuranceValue
    {
        public double ByCompany { get; set; }

        public double ByEmployee { get; set; }

        public double ByEmployeeFemale { get; set; }
    }

    public class InsuranceProgram
    {
        public Guid ID { get; set; }

        public Guid InsuranceId { get; set; }

        public Insurance Insurance { get; set; }

        public string Program { get; set; }

        public double PercentageByCompany { get; set; }

        public double PercentageByEmployee { get; set; }

        public double PercentageByEmployeeFemale { get; set; }
    }
}
