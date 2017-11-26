using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayCare.Model;
using EntityMap;
using PayCare.Repository.Mapping;

namespace PayCare.Repository
{
    public interface ISalaryUpdateRepository
    {
        SalaryUpdate GetById(Guid id);
        List<SalaryUpdate> GetAll();
        void Save(SalaryUpdate salaryUpdate);
        void Update(SalaryUpdate salaryUpdate);
        void Delete(Guid id);
    }


    public class SalaryUpdateRepository : ISalaryUpdateRepository
    {
        private DataSource ds;
        private string tableName = "SalaryUpdate";

        public SalaryUpdateRepository(DataSource ds)
        {
            this.ds = ds;
        }

        public SalaryUpdate GetById(Guid id)
        {
            SalaryUpdate salaryUpdate = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string sql = "SELECT su.*, b.BranchName, g.GradeName, o.OccupationName FROM "
                          + "((SalaryUpdate su LEFT JOIN Branch b ON su.BranchId = b.ID) "
                          + "LEFT JOIN Grade g ON su.GradeId = g.ID) LEFT JOIN Occupation o "
                          + "ON su.OccupationId = o.ID "
                          + "WHERE su.ID='{" + id + "}'";
                               
                salaryUpdate=em.ExecuteObject<SalaryUpdate>(sql, new SalaryUpdateMapper());
            }

            return salaryUpdate;
        }


        public List<SalaryUpdate> GetAll()
        {
            List<SalaryUpdate> salaryUpdates = new List<SalaryUpdate>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string sql = "SELECT su.*, b.BranchName, g.GradeName, o.OccupationName FROM "
                           + "((SalaryUpdate su LEFT JOIN Branch b ON su.BranchId = b.ID) "
                           + "LEFT JOIN Grade g ON su.GradeId = g.ID) LEFT JOIN Occupation o "
                           + "ON su.OccupationId = o.ID "
                           + "ORDER BY su.EffectiveDate DESC";


                salaryUpdates = em.ExecuteList<SalaryUpdate>(sql, new SalaryUpdateMapper());
            }

            return salaryUpdates;
        }



        public void Save(SalaryUpdate salaryUpdate)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    string[] columns = { "ID", "EffectiveDate", "BranchId", "GradeId", "OccupationId",
                                       "UpdateType", "MainSalary","LunchAllowance","TransportAllowance",
                                       "FuelAllowance", "VehicleAllowance","Notes","CreatedDate","CreatedBy",
                                       "ModifiedDate","ModifiedBy" };

                    object[] values = { Guid.NewGuid(),salaryUpdate.EffectiveDate.ToShortDateString(),salaryUpdate.BranchId,
                                      salaryUpdate.GradeId,salaryUpdate.OccupationId,salaryUpdate.UpdateType,
                                      salaryUpdate.MainSalary,salaryUpdate.LunchAllowance,salaryUpdate.TransportAllowance,
                                      salaryUpdate.FuelAllowance,salaryUpdate.VehicleAllowance,salaryUpdate.Notes,
                                      DateTime.Now.ToShortDateString(),Store.ActiveUser,DateTime.Now.ToShortDateString(),
                                      Store.ActiveUser};

                    var q = new Query().Select(columns).From(tableName).Insert(values);

                    em.ExecuteNonQuery(q.ToSql());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        
        }



        public void Update(SalaryUpdate salaryUpdate)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    string[] columns = { "EffectiveDate", "BranchId", "GradeId", "OccupationId",
                                       "UpdateType", "MainSalary","LunchAllowance","TransportAllowance",
                                       "FuelAllowance", "VehicleAllowance","Notes","CreatedDate","CreatedBy",
                                       "ModifiedDate","ModifiedBy" };

                    object[] values = { salaryUpdate.EffectiveDate.ToShortDateString(),salaryUpdate.BranchId,
                                      salaryUpdate.GradeId,salaryUpdate.OccupationId,salaryUpdate.UpdateType,
                                      salaryUpdate.MainSalary,salaryUpdate.LunchAllowance,salaryUpdate.TransportAllowance,
                                      salaryUpdate.FuelAllowance,salaryUpdate.VehicleAllowance,salaryUpdate.Notes,
                                      DateTime.Now.ToShortDateString(),Store.ActiveUser,DateTime.Now.ToShortDateString(),
                                      Store.ActiveUser};

                    var q = new Query().Select(columns).From(tableName).Update(values)
                        .Where("ID").Equal(salaryUpdate.ID);

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
                    var q = new Query().From(tableName).Delete()
                        .Where("ID").Equal(id);

                    em.ExecuteNonQuery(q.ToSql());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    
    }
}
