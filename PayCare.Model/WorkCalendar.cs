using System;

namespace PayCare.Model
{
    public class WorkCalendar
    {
        public Guid ID { get; set; }

        public int MonthPeriod { get; set; }

        public int YearPeriod { get; set; }

        public int WorkDay { get; set; }

        public int OffDay { get; set; }

        public bool IsClosed { get; set; }

        public bool IsThrClosed { get; set; }
    }
}
