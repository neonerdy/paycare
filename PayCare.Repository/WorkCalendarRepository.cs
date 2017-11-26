using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PayCare.Model;
using EntityMap;
using PayCare.Repository.Mapping;

namespace PayCare.Repository
{
    public interface IWorkCalendarRepository
    {
        WorkCalendar GetById(Guid id);
        WorkCalendar GetByMonthYear(int month, int year);
        List<WorkCalendar> GetAll();
        List<Guid> GetAllID();
        WorkCalendar GetLast();
        void Save(WorkCalendar workCalendar);
        void Update(WorkCalendar workCalendar);
        void Delete(Guid id);
        bool IsPeriodClosed(int month, int year);
        bool IsThrClosed(int year);
        void UpdateIsThr(int year, bool paid);
        void UpdateIsPeriod(int month,  int year, bool paid);
    }


    public class WorkCalendarRepository : IWorkCalendarRepository
    {

        private DataSource ds;
        private IWorkCalendarItemRepository workCalendarItemRepository;
        private string tableName = "WorkCalendar";

        public WorkCalendarRepository(DataSource ds)
        {
            this.ds = ds;
            workCalendarItemRepository = EntityContainer.GetType<IWorkCalendarItemRepository>();
        }



        public WorkCalendar GetById(Guid id)
        {
            WorkCalendar workCalendar = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From(tableName).Where("ID").Equal("{" + id + "}");

                workCalendar = em.ExecuteObject<WorkCalendar>(q.ToSql(), new WorkCalendarMapper());
            }

            return workCalendar;            
        }

        public WorkCalendar GetByMonthYear(int month, int year)
        {
            WorkCalendar workCalendar = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From(tableName)
                    .Where("MonthPeriod").Equal(month)
                    .And("YearPeriod").Equal(year);

                workCalendar = em.ExecuteObject<WorkCalendar>(q.ToSql(), new WorkCalendarMapper());
            }

            return workCalendar;
        }

        public List<WorkCalendar> GetAll()
        {
            List<WorkCalendar> workCalendars = new List<WorkCalendar>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From(tableName).OrderBy("MonthPeriod DESC,YearPeriod DESC");
              
                workCalendars = em.ExecuteList<WorkCalendar>(q.ToSql(), new WorkCalendarMapper());
            }

            return workCalendars;
        }


        public List<Guid> GetAllID()
        {
            List<Guid> list = new List<Guid>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From(tableName).OrderBy("MonthPeriod DESC,YearPeriod DESC");

                using (var rdr = em.ExecuteReader(q.ToSql()))
                {
                    while (rdr.Read())
                    {
                        Guid Id = (Guid)rdr["ID"];
                        list.Add(Id);
                    }
                }
            }
            return list;
        }




        public WorkCalendar GetLast()
        {
            WorkCalendar workCalendar = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().Select("TOP 1 *").From(tableName).OrderBy("MonthPeriod DESC,YearPeriod DESC");

                workCalendar = em.ExecuteObject<WorkCalendar>(q.ToSql(), new WorkCalendarMapper());
            }

            return workCalendar;
        }

        


        public void Save(WorkCalendar workCalendar)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    string[] columns = { "ID", "MonthPeriod", "YearPeriod", 
                                           "WorkDay", "OffDay", 
                                           "IsClosed", "IsThrClosed" };
                    object[] values = { Guid.NewGuid(), workCalendar.MonthPeriod, workCalendar .YearPeriod,
                                        workCalendar.WorkDay,workCalendar.OffDay, 
                                        workCalendar.IsClosed==true?1:0, workCalendar.IsThrClosed==true?1:0};

                    var q = new Query().Select(columns).From(tableName).Insert(values);

                    em.ExecuteNonQuery(q.ToSql());

                    UpdateIsThr(workCalendar.YearPeriod, workCalendar.IsThrClosed);
                }                              

            }
            catch (Exception ex)
            {
                throw ex;
            }
        
        }

        public void Update(WorkCalendar workCalendar)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    string[] columns = { "MonthPeriod", "YearPeriod", 
                                           "WorkDay", "OffDay", 
                                           "IsClosed", "IsThrClosed" };
                    object[] values = { workCalendar.MonthPeriod, workCalendar .YearPeriod,
                                        workCalendar.WorkDay,workCalendar.OffDay, 
                                        workCalendar.IsClosed==true?1:0, workCalendar.IsThrClosed==true?1:0};

                    var q = new Query().Select(columns).From(tableName).Update(values)
                           .Where("ID").Equal("{" + workCalendar.ID + "}");
                    
                    em.ExecuteNonQuery(q.ToSql());

                    UpdateIsThr(workCalendar.YearPeriod, workCalendar.IsThrClosed);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void Delete(Guid id)
        {
            Transaction tx = null;

            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    tx = em.BeginTransaction();

                    var q = new Query().From(tableName).Delete().Where("ID").Equal("{" + id + "}");
                
                    em.ExecuteNonQuery(q.ToSql(),tx);
                    workCalendarItemRepository.Delete(em, tx, id);

                    tx.Commit();
                
                }
            }
            catch (Exception ex)
            {
                tx.Rollback();
                throw ex;
            }


        }

        public bool IsPeriodClosed(int month, int year)
        {
            bool isClosed = false;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From("WorkCalendar")
                    .Where("YearPeriod").Equal(year)
                    .And("MonthPeriod").Equal(month)
                    .And("IsClosed = true");

                using (var rdr = em.ExecuteReader(q.ToSql()))
                {
                    if (rdr.Read())
                    {
                        isClosed = true;
                    }
                }

            }

            return isClosed;

        }


        public bool IsThrClosed(int year)
        {
            bool isThrClosed = false;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From("WorkCalendar")
                    .Where("YearPeriod").Equal(year)
                    .And("IsThrClosed = true");

                using (var rdr = em.ExecuteReader(q.ToSql()))
                {
                    if (rdr.Read())
                    {
                        isThrClosed = true;
                    }
                }

            }

            return isThrClosed;

        }


        public void UpdateIsThr(int year, bool paid)
        {
            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                int status = 0;
                if (paid) status = 1;

                string sql = "UPDATE " + tableName + " SET IsThrClosed = " + status + " WHERE YearPeriod=" + year + " ";
                em.ExecuteNonQuery(sql);
            }
        }


        public void UpdateIsPeriod(int month, int year, bool paid)
        {
            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                int status = 0;
                if (paid) status = 1;

                string sql = "UPDATE " + tableName + " SET IsClosed = " + status + " WHERE MonthPeriod = " + month + " and YearPeriod=" + year + " ";
                em.ExecuteNonQuery(sql);
            }
        }











        
    }
}
