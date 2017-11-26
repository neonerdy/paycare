
using System;
using System.Collections.Generic;
using PayCare.Model;

using System.Data;
using PayCare.Repository.Mapping;
using EntityMap;
using System.Data.OleDb;
using System.Configuration;


namespace PayCare.Repository
{
    public interface IPTKPRepository
    {
        PTKP GetById(Guid id);
        PTKP GetLast();
        PTKP GetByName(string name);
        PTKP GetByCode(string code);
        List<PTKP> GetAll();
        List<PTKP> Search(string value);
        void Save(PTKP ptkp);
        void Update(PTKP ptkp);
        void Delete(Guid id);
        List<PTKP> GetActivePTKP();
        bool IsPTKPNameExisted(string ptkpName);
        bool IsPTKPCodeExisted(string ptkpCode);
        //bool IsPTKPUsedByDepartment(Guid ptkpId);
    }

    public class PTKPRepository : IPTKPRepository
    {
        private string tableName = "PTKP";
        private DataSource ds;

        public PTKPRepository(DataSource ds)
        {
            this.ds = ds;
        }


        public PTKP GetById(Guid id)
        {
            PTKP ptkp = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT PTKP.ID, PTKP.EffectiveDate, PTKP.PtkpCode, PTKP.PtkpName, "
                        + "PTKP.TaxValue, PTKP.MaritalValue, PTKP.ChildValue, PTKP.Total, PTKP.NumberOfChild "
                        + "FROM PTKP "
                        + "WHERE "
                        + "PTKP.ID='{" + id + "}'";

                ptkp = em.ExecuteObject<PTKP>(sql, new PTKPMapper());
            }

            return ptkp;
        }


        public PTKP GetLast()
        {
            PTKP ptkp = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT TOP 1 PTKP.ID, PTKP.EffectiveDate, PTKP.PtkpCode, PTKP.PtkpName, "
                        + "PTKP.TaxValue, PTKP.MaritalValue, PTKP.ChildValue, PTKP.Total, PTKP.NumberOfChild "
                        + "FROM PTKP "
                        + "ORDER BY PTKP.PTKPCode ASC";

                ptkp = em.ExecuteObject<PTKP>(sql, new PTKPMapper());
            }

            return ptkp;
        }


        public PTKP GetByCode(string code)
        {
            PTKP ptkp = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From(tableName).Where("PtkpCode").Equal(code);

                ptkp = em.ExecuteObject<PTKP>(q.ToSql(), new PTKPMapper());
            }

            return ptkp;
        }

        

        public PTKP GetByName(string name)
        {
            PTKP ptkp = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT PTKP.ID, PTKP.EffectiveDate, PTKP.PtkpCode, PTKP.PtkpName, "
                        + "PTKP.TaxValue, PTKP.MaritalValue, PTKP.ChildValue, PTKP.Total, PTKP.NumberOfChild "
                        + "FROM PTKP "
                        + "WHERE "
                        + "PTKP.PTKPName='" + name + "'";

                ptkp = em.ExecuteObject<PTKP>(sql, new PTKPMapper());
            }

            return ptkp;
        }


        public List<PTKP> GetAll()
        {
            List<PTKP> ptkps = new List<PTKP>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT PTKP.ID, PTKP.EffectiveDate, PTKP.PtkpCode, PTKP.PtkpName, "
                        + "PTKP.TaxValue, PTKP.MaritalValue, PTKP.ChildValue, PTKP.Total, PTKP.NumberOfChild "
                        + "FROM PTKP "
                        + "ORDER BY PTKP.PTKPCode ASC";

                ptkps = em.ExecuteList<PTKP>(sql, new PTKPMapper());
            }

            return ptkps;
        }




        public List<PTKP> Search(string value)
        {
            List<PTKP> ptkps = new List<PTKP>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT PTKP.ID, PTKP.EffectiveDate, PTKP.PtkpCode, PTKP.PtkpName, "
                        + "PTKP.TaxValue, PTKP.MaritalValue, PTKP.ChildValue, PTKP.Total, PTKP.NumberOfChild "
                        + "FROM PTKP "
                        + "WHERE "
                        + "(PTKP.PTKPCode like '%" + value + "%' "
                        + "OR PTKP.PTKPName like '%" + value + "%')";

                ptkps = em.ExecuteList<PTKP>(sql, new PTKPMapper());
            }

            return ptkps;
        }




        public void Save(PTKP ptkp)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    string[] fields = { "ID", "EffectiveDate", "PTKPCode", "PTKPName", 
                                          "TaxValue", "MaritalValue", "ChildValue",
                                          "Total", "NumberOfChild"};

                    object[] values = { Guid.NewGuid(), ptkp.EffectiveDate.ToShortDateString(), ptkp.PTKPCode, ptkp.PTKPName, 
                                          ptkp.TaxValue, ptkp.MaritalValue, ptkp.ChildValue,
                                          ptkp.Total, ptkp.NumberOfChild};

                    
                    Query q = new Query().Select(fields).From(tableName).Insert(values);

                    em.ExecuteNonQuery(q.ToSql());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public void Update(PTKP ptkp)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {

                    string[] fields = { "EffectiveDate", "PTKPCode", "PTKPName", 
                                          "TaxValue", "MaritalValue", "ChildValue",
                                          "Total", "NumberOfChild"};

                    object[] values = { ptkp.EffectiveDate.ToShortDateString(), ptkp.PTKPCode, ptkp.PTKPName, 
                                          ptkp.TaxValue, ptkp.MaritalValue, ptkp.ChildValue,
                                          ptkp.Total, ptkp.NumberOfChild};

                    Query q = new Query().Select(fields).From(tableName).Update(values)
                        .Where("ID").Equal("{" + ptkp.ID + "}");

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
                    Query q = new Query().From(tableName).Delete().Where("ID").Equal("{" + id + "}");
                    em.ExecuteNonQuery(q.ToSql());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


       
        public bool IsPTKPNameExisted(string ptkpName)
        {
            bool isExisted = false;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From("PTKP").Where("PTKPName").Equal(ptkpName);

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


        public bool IsPTKPCodeExisted(string ptkpCode)
        {
            bool isExisted = false;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From("PTKP").Where("PTKPCode").Equal(ptkpCode);

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


        public List<PTKP> GetActivePTKP()
        {
            List<PTKP> ptkps = new List<PTKP>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From(tableName).Where("IsActive=true").OrderBy("PTKPCode Asc");
                ptkps = em.ExecuteList<PTKP>(q.ToSql(), new PTKPMapper());
            }

            return ptkps;
        }

        //public bool IsPTKPUsedByDepartment(Guid ptkpId)
        //{
        //    bool isUsed = false;

        //    using (var em = EntityManagerFactory.CreateInstance(ds))
        //    {
        //        var q = new Query().From("Department").Where("PTKPId").Equal("{" + ptkpId + "}");

        //        using (var rdr = em.ExecuteReader(q.ToSql()))
        //        {
        //            if (rdr.Read())
        //            {
        //                isUsed = true;
        //            }
        //        }

        //    }

        //    return isUsed;

        //}
       










    }
}
