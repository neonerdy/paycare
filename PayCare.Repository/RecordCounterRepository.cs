using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityMap;
using PayCare.Model;
using PayCare.Repository.Mapping;

namespace PayCare.Repository
{
    public interface IRecordCounterRepository
    {
        RecordCounter GetRecordCounter();
        void UpdateBranchCounter(IEntityManager em, Transaction tx);
        void UpdateDepartmentCounter(IEntityManager em, Transaction tx);
        void UpdateGradeCounter(IEntityManager em, Transaction tx);
        void UpdateOccupationCounter(IEntityManager em, Transaction tx);
        void UpdateInsuranceCounter(IEntityManager em, Transaction tx);
        void UpdateEmployeeCounter(IEntityManager em, Transaction tx);
    }
    

    public class RecordCounterRepository : IRecordCounterRepository
    {
        private DataSource ds;
        private string tableName = "RecordCounter";
        private Guid ID=new Guid("00000001-0000-0000-0000-000000000000");

        public RecordCounterRepository(DataSource ds)
        {
            this.ds = ds;
        }

        public RecordCounter GetRecordCounter()
        {
            RecordCounter recordCounter = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From(tableName).Where("ID").Equal(ID);
                recordCounter = em.ExecuteObject<RecordCounter>(q.ToSql(), new RecordCounterMapper());
            }

            return recordCounter;
        }


        public void UpdateBranchCounter(IEntityManager em, Transaction tx)
        {
            try
            {
                int branchCounter = 0;
                var recordCounter = GetRecordCounter();

                if (recordCounter != null)
                {
                    branchCounter = recordCounter.BranchCounter;
                }

                string[] columns = { "BranchCounter" };
                object[] values = { branchCounter + 1 };

                var q = new Query().Select(columns).From(tableName).Update(values)
                    .Where("ID").Equal("{" + ID + "}");

                em.ExecuteNonQuery(q.ToSql(), tx);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void UpdateDepartmentCounter(IEntityManager em, Transaction tx)
        {
            try
            {
                int departmentCounter = 0;

                var recordCounter = GetRecordCounter();
                if (recordCounter != null)
                {
                    departmentCounter = recordCounter.DepartmentCounter;
                }

                string[] columns = { "DepartmentCounter" };
                object[] values = { departmentCounter + 1 };

                var q = new Query().Select(columns).From(tableName).Update(values)
                    .Where("ID").Equal(ID);

                em.ExecuteNonQuery(q.ToSql(), tx);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void UpdateGradeCounter(IEntityManager em, Transaction tx)
        {
            try
            {
                int gradeCounter = 0;

                var recordCounter = GetRecordCounter();
                if (recordCounter != null)
                {
                    gradeCounter = recordCounter.GradeCounter;
                }
                
                string[] columns = { "GradeCounter" };
                object[] values = { gradeCounter + 1 };

                var q = new Query().Select(columns).From(tableName).Update(values)
                    .Where("ID").Equal(ID);

                em.ExecuteNonQuery(q.ToSql(), tx);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void UpdateOccupationCounter(IEntityManager em, Transaction tx)
        {
            try
            {
                int occupationCounter = 0;

                var recordCounter = GetRecordCounter();
                if (recordCounter != null)
                {
                    occupationCounter = recordCounter.OccupationCounter;
                }

                string[] columns = { "OccupationCounter" };
                object[] values = { occupationCounter + 1 };

                var q = new Query().Select(columns).From(tableName).Update(values)
                    .Where("ID").Equal(ID);

                em.ExecuteNonQuery(q.ToSql(), tx);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void UpdateInsuranceCounter(IEntityManager em, Transaction tx)
        {
            try
            {
                int insuranceCounter = 0;
                var recordCounter = GetRecordCounter();

                if (recordCounter != null)
                {
                    insuranceCounter = recordCounter.InsuranceCounter;
                }

                string[] columns = { "InsuranceCounter" };
                object[] values = { insuranceCounter + 1 };

                var q = new Query().Select(columns).From(tableName).Update(values)
                    .Where("ID").Equal(ID);

                em.ExecuteNonQuery(q.ToSql(), tx);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void UpdateEmployeeCounter(IEntityManager em, Transaction tx)
        {
            try
            {
                int employeeCounter = 0;
                var recordCounter = GetRecordCounter();

                if (recordCounter != null)
                {
                    employeeCounter = recordCounter.EmployeeCounter;
                }

                string[] columns = { "EmployeeCounter" };
                object[] values = { employeeCounter + 1 };

                var q = new Query().Select(columns).From(tableName).Update(values)
                    .Where("ID").Equal("{" + ID + "}");

                em.ExecuteNonQuery(q.ToSql(), tx);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




    }
}
