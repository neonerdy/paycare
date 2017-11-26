using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityMap;
using PayCare.Model;
using System.Data;

namespace PayCare.Repository.Mapping
{
    

    public class WorkCalendarItemMapper : IDataMapper<WorkCalendarItem>
    {

        public WorkCalendarItem Map(IDataReader rdr)
        {
            var workCalendarItem = new WorkCalendarItem();

            workCalendarItem.ID = rdr["ID"] is DBNull ? Guid.Empty : (Guid)rdr["ID"];
            workCalendarItem.WorkCalendarId = rdr["WorkCalendarId"] is DBNull ? Guid.Empty : (Guid)rdr["WorkCalendarId"];

            if (workCalendarItem.WorkCalendar == null) workCalendarItem.WorkCalendar = new WorkCalendar();
            workCalendarItem.WorkCalendar.MonthPeriod = rdr["MonthPeriod"] is DBNull ? 0 : (int)rdr["MonthPeriod"];
            workCalendarItem.WorkCalendar.YearPeriod = rdr["YearPeriod"] is DBNull ? 0 : (int)rdr["YearPeriod"];
            workCalendarItem.WorkCalendar.OffDay = rdr["OffDay"] is DBNull ? 0 : (int)rdr["OffDay"];

            workCalendarItem.OffDate = rdr["OffDate"] is DBNull ? DateTime.Now : (DateTime)rdr["OffDate"];
            workCalendarItem.Notes = rdr["Notes"] is DBNull ? string.Empty : (string)rdr["Notes"];

            return workCalendarItem;

        }

    }
}
