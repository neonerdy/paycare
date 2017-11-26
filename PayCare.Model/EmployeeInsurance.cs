using System;

namespace PayCare.Model
{
    public class EmployeeInsurance
    {
        public Guid ID { get; set; }

        public Guid EmployeeId { get; set; }

        public Guid InsuranceId { get; set; }

        public string InsuranceName { get; set; }

        public Guid InsuranceProgramId { get; set; }

        public string InsuranceProgramName { get; set; }

        public DateTime EffectiveDate { get; set; }

        public DateTime EndDate { get; set; }

        public string InsuranceNumber { get; set; }
              
       

    }
}
