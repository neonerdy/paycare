using System;

namespace PayCare.Model
{
    public class EmployeePrincipal
    {
        public Guid ID { get; set; }

        public Guid EmployeeId { get; set; }
        
        public DateTime EffectiveDate { get; set; }

        public Guid PrincipalId { get; set; }

        public string PrincipalName { get; set; }

        public bool IsActive { get; set; }


    }
}
