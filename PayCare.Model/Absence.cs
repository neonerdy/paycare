using System;

namespace PayCare.Model
{
    public class Absence
    {
        public Guid ID { get; set; }

        public int MonthPeriod { get; set; }

        public int YearPeriod { get; set; }

        public DateTime AbsenceStartDate { get; set; }

        public DateTime AbsenceEndDate { get; set; }

        public Guid EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public string Branch { get; set; }

        public string Department { get; set; }

        public int WorkDay { get; set; }

        public int OnLeaveDay { get; set; }

        public int OffDay { get; set; }

        public int Total { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }

    }
}
