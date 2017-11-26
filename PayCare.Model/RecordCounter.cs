using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayCare.Model
{
    public class RecordCounter
    {
        public Guid ID { get; set; }

        public int BranchCounter { get; set; }

        public int DepartmentCounter { get; set; }
        
        public int GradeCounter { get; set; }

        public int OccupationCounter { get; set; }

        public int InsuranceCounter { get; set; }

        public int EmployeeCounter { get; set; }
    }
}
