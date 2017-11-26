using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityMap;
using PayCare.Model;
using PayCare.Repository.Mapping;

namespace PayCare.Repository
{

    public interface IWorkCalendarItemRepository
    {
        WorkCalendarItem GetById(Guid id);
        
        WorkCalendarItem GetLast(Guid workCalendarId);
        
        List<WorkCalendarItem> GetByWorkCalendarId(Guid workCalendarId);
        void Save(WorkCalendarItem workCalendarItem);
        void Update(WorkCalendarItem workCalendarItem);
        void Delete(Guid id);
        void Delete(IEntityManager em, Transaction tx, Guid workCalendarId);
        bool IsItemExisted(DateTime offDate);
        
    }


    public class WorkCalendarItemRepository : IWorkCalendarItemRepository
    {
        private DataSource ds;
        private string tableName = "WorkCalendarItem";

        public WorkCalendarItemRepository(DataSource ds)
        {
            this.ds = ds;
        }


        public WorkCalendarItem GetById(Guid id)
        {
            WorkCalendarItem workCalendarItem = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT wci.*, wc.MonthPeriod, wc.YearPeriod, wc.OffDay "
                    + "FROM WorkCalendarItem wci INNER JOIN WorkCalendar wc ON wci.WorkCalendarId = wc.ID "
                    + "WHERE wci.ID ='{" + id + "}'";

                workCalendarItem = em.ExecuteObject<WorkCalendarItem>(sql, new WorkCalendarItemMapper());
            }

            return workCalendarItem;
        }


        public WorkCalendarItem GetLast(Guid workCalendarId)
        {
            WorkCalendarItem workCalendarItem = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT TOP 1 wci.*, wc.MonthPeriod, wc.YearPeriod, wc.OffDay "
                    + "FROM WorkCalendar wc INNER JOIN WorkCalendarItem wci ON wci.WorkCalendarId = wc.ID "
                    + "WHERE wci.WorkCalendarId = '{" + workCalendarId + "}' "
                    + "ORDER BY wci.OffDate DESC";

                workCalendarItem = em.ExecuteObject<WorkCalendarItem>(sql, new WorkCalendarItemMapper());
            }

            return workCalendarItem;
        }

        public List<WorkCalendarItem> GetByWorkCalendarId(Guid workCalendarId)
        {
            List<WorkCalendarItem> workCalendarItems = new List<WorkCalendarItem>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string sql = "SELECT wci.*, wc.MonthPeriod, wc.YearPeriod, wc.OffDay "
                           + "FROM WorkCalendar wc "
                           + "INNER JOIN WorkCalendarItem wci ON wc.ID = wci.WorkCalendarId "
                           + "WHERE wci.WorkCalendarId='{" + workCalendarId + "}' "
                           + "ORDER BY wci.OffDate DESC";
                            

                workCalendarItems = em.ExecuteList<WorkCalendarItem>(sql, new WorkCalendarItemMapper());
            }

            return workCalendarItems;
        }



        public void Save(WorkCalendarItem workCalendarItem)
        {
           try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {

                    string[] columns = { "ID", "WorkCalendarId", "OffDate",
                                   "Notes"};

                    object[] values = { Guid.NewGuid(),workCalendarItem.WorkCalendarId,
                                          workCalendarItem.OffDate.ToShortDateString(),
                                workCalendarItem.Notes};

                    var q = new Query().Select(columns).From(tableName).Insert(values);

                    em.ExecuteNonQuery(q.ToSql());

                }

            }
           catch (Exception ex)
           {
               throw ex;
           }

        }



        public void Update(WorkCalendarItem workCalendarItem)
        {
            try
            {

                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    string[] columns = { "WorkCalendarId", "OffDate",
                                   "Notes"};

                    object[] values = { workCalendarItem.WorkCalendarId,
                                          workCalendarItem.OffDate.ToShortDateString(),
                                workCalendarItem.Notes};

                    var q = new Query().Select(columns).From(tableName).Update(values).Where("ID").Equal("{" + workCalendarItem.ID + "}");

                    em.ExecuteNonQuery(q.ToSql());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void Delete(Guid id)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    var q = new Query().From(tableName).Delete().Where("ID").Equal("{" + id + "}");

                    em.ExecuteNonQuery(q.ToSql());
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void Delete(IEntityManager em, Transaction tx, Guid workCalendarId)
        {
            var q = new Query().From(tableName).Delete().Where("WorkCalendarId")
                .Equal("{" + workCalendarId + "}");

            em.ExecuteNonQuery(q.ToSql(), tx);
        }


        
        public bool IsItemExisted(DateTime offDate)
        {
            bool isExisted = false;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From("WorkCalendarItem")
                    .Where("OffDate = #" + offDate.ToShortDateString() + "#");

                using (var rdr = em.ExecuteReader(q.ToSql()))
                {
                    if (rdr.Read())
                    {
                        isExisted = true;
                    }
                }

            }

            return isExisted;

        }


      











    }

   
}
