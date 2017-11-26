using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityMap;
using PayCare.Model;
using PayCare.Repository.Mapping;

namespace PayCare.Repository
{

    public interface IPrincipalItemRepository
    {
        PrincipalItem GetById(Guid id);
        
        PrincipalItem GetLast(Guid principalId);
        
        List<PrincipalItem> GetByPrincipalId(Guid principalId);
        void Save(PrincipalItem principalItem);
        void Update(PrincipalItem principalItem);
        void Delete(Guid id);
        void Delete(IEntityManager em, Transaction tx, Guid id);
        PrincipalItem GetCurrentPrincipal(Guid principalId, int month, int year);
        PrincipalItem GetPreviousPrincipal(Guid principalId, int month, int year);
        bool IsItemExisted(string reference, Guid principalId);
        
    }


    public class PrincipalItemRepository : IPrincipalItemRepository
    {
        private DataSource ds;
        private string tableName = "PrincipalItem";

        public PrincipalItemRepository(DataSource ds)
        {
            this.ds = ds;
        }


        public PrincipalItem GetById(Guid id)
        {
            PrincipalItem principalItem = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT pi.*, p.PrincipalCode, p.PrincipalName "
                    + "FROM PrincipalItem pi INNER JOIN Principal p ON pi.PrincipalId = p.ID "
                    + "WHERE pi.ID ='{" + id + "}'";

                principalItem = em.ExecuteObject<PrincipalItem>(sql, new PrincipalItemMapper());
            }

            return principalItem;
        }


        public PrincipalItem GetLast(Guid principalId)
        {
            PrincipalItem principalItem = null;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var sql = "SELECT TOP 1 pi.*, p.PrincipalCode, p.PrincipalName "
                    + "FROM PrincipalItem pi INNER JOIN Principal p ON pi.PrincipalId = p.ID "
                    + "WHERE pi.PrincipalId = '{" + principalId + "}' "
                    + "ORDER BY pi.EffectiveDate DESC";

                principalItem = em.ExecuteObject<PrincipalItem>(sql, new PrincipalItemMapper());
            }

            return principalItem;
        }

        public List<PrincipalItem> GetByPrincipalId(Guid principalId)
        {
            List<PrincipalItem> principalItems = new List<PrincipalItem>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string sql = "SELECT pi.*,p.PrincipalCode, p.PrincipalName FROM Principal p "
                           + "INNER JOIN PrincipalItem pi ON p.ID = pi.PrincipalId "
                           + "WHERE pi.PrincipalId='{" + principalId + "}' "
                           + "ORDER BY pi.EffectiveDate DESC";
                            

                principalItems = em.ExecuteList<PrincipalItem>(sql, new PrincipalItemMapper());
            }

            return principalItems;
        }



        public void Save(PrincipalItem principalItem)
        {
           try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {

                    string[] columns = { "ID", "PrincipalId", "EffectiveDate",
                                   "Reference", "MainSalary",
                               "LunchAllowance", "TransportationAllowance"};

                    object[] values = { Guid.NewGuid(),principalItem.PrincipalId,principalItem.EffectiveDate.ToShortDateString(),
                                principalItem.Reference, principalItem.MainSalary,
                              principalItem.LunchAllowance, principalItem.TransportationAllowance};

                    var q = new Query().Select(columns).From(tableName).Insert(values);

                    em.ExecuteNonQuery(q.ToSql());

                }

            }
           catch (Exception ex)
           {
               throw ex;
           }

        }



        public void Update(PrincipalItem principalItem)
        {
            try
            {

                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    string[] columns = { "PrincipalId", "EffectiveDate",
                                   "Reference", "MainSalary",
                               "LunchAllowance", "TransportationAllowance"};

                    object[] values = { principalItem.PrincipalId,principalItem.EffectiveDate.ToShortDateString(),
                                principalItem.Reference, principalItem.MainSalary,
                              principalItem.LunchAllowance, principalItem.TransportationAllowance};

                    var q = new Query().Select(columns).From(tableName).Update(values).Where("ID").Equal("{" + principalItem.ID + "}");

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


        public void Delete(IEntityManager em,Transaction tx, Guid id)
        {
            var q = new Query().From(tableName).Delete().Where("PrincipalId").Equal("{" + id + "}");
            em.ExecuteNonQuery(q.ToSql(),tx);
        }



        public PrincipalItem GetCurrentPrincipal(Guid principalId, int month, int year)
        {
            PrincipalItem principalItem = new PrincipalItem();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string sql = "SELECT pi.*,p.PrincipalCode,p.PrincipalName FROM Principal p "
                          + "INNER JOIN PrincipalItem pi ON p.ID = pi.PrincipalId "
                          + "WHERE pi.PrincipalId='{" + principalId + "}' "
                          + "AND month(pi.EffectiveDate)=" + month + " "
                          + "AND year(pi.EffectiveDate)=" + year;

                principalItem = em.ExecuteObject<PrincipalItem>(sql, new PrincipalItemMapper());
            }

            return principalItem;
        }

        public PrincipalItem GetPreviousPrincipal(Guid principalId, int month, int year)
        {
            PrincipalItem principalItem = new PrincipalItem();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                string sql = "SELECT pi.*,p.PrincipalCode,p.PrincipalName FROM Principal p "
                          + "INNER JOIN PrincipalItem pi ON p.ID = pi.PrincipalId "
                          + "WHERE pi.PrincipalId='{" + principalId + "}' "
                          + "AND month(pi.EffectiveDate) <=" + month + " "
                          + "AND year(pi.EffectiveDate) <=" + year;
                
                principalItem = em.ExecuteObject<PrincipalItem>(sql, new PrincipalItemMapper());
            }

            return principalItem;
        }



        public bool IsItemExisted(string reference, Guid principalId)
        {
            bool isExisted = false;

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From("PrincipalItem")
                    .Where("Reference").Equal(reference)
                   .And("PrincipalId").Equal(principalId);

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
