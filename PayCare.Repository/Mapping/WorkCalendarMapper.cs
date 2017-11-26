using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayCare.Model;
using EntityMap;
using System.Data;

namespace PayCare.Repository.Mapping
{
    public class WorkCalendarMapper : IDataMapper<WorkCalendar>
    {
        public WorkCalendar Map(IDataReader rdr)
        {
            var workCalendar = new WorkCalendar();

            workCalendar.ID = rdr["ID"] is DBNull ? Guid.Empty : (Guid)rdr["ID"];
            workCalendar.MonthPeriod = rdr["MonthPeriod"] is DBNull ? 0 : (int)rdr["MonthPeriod"];
            workCalendar.YearPeriod = rdr["YearPeriod"] is DBNull ? 0 : (int)rdr["YearPeriod"];
            workCalendar.WorkDay = rdr["WorkDay"] is DBNull ? 0 : (int)rdr["WorkDay"];
            workCalendar.OffDay = rdr["OffDay"] is DBNull ? 0 : (int)rdr["OffDay"];
            workCalendar.IsClosed = rdr["IsClosed"] is DBNull ? false : (bool)rdr["IsClosed"];
            workCalendar.IsThrClosed = rdr["IsThrClosed"] is DBNull ? false : (bool)rdr["IsThrClosed"];

            
            return workCalendar;

        }

    }
}
