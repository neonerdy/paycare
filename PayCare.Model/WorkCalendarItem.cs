using System;

namespace PayCare.Model
{
    
    public class WorkCalendarItem
    {
        public Guid ID { get; set; }

        public Guid WorkCalendarId { get; set; }

        public WorkCalendar WorkCalendar { get; set; }

        public DateTime OffDate { get; set; }

        public string Notes { get; set; }
        
    }
}
