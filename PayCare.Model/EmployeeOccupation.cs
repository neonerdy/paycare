using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayCare.Model
{
    public class EmployeeOccupation
    {
        public Guid ID { get; set; }

        public Guid EmployeeId { get; set; }

        public DateTime EffectiveDate { get; set; }

        public Guid OccupationId { get; set; }

        public string OccupationName { get; set; }

        public bool IsTaskForce { get; set; }


    }
}
