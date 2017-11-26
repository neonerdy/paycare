using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayCare.Model;
using EntityMap;

namespace PayCare.Repository
{
    public interface ITransferRepository
    {
        void Save(Transfer transfer);
    }

    public class TransferRepository : ITransferRepository
    {
        private DataSource ds;
        private string tableName = "Transfer";

        public TransferRepository(DataSource ds)
        {
            this.ds = ds;
        }
        
        public void Save(Transfer transfer)
        {
            try
            {
                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    string[] columns = { "ID", "ActiveMonth", "ActiveYear", "TransferType", "TransferDate", "TotalEmployee", "TotalTransfer"};
                    object[] values = { Guid.NewGuid(), Store.ActiveMonth, Store.ActiveYear,transfer.TransferType, transfer.TransferDate.ToShortDateString(),
                                        transfer.TotalEmployee, transfer.TotalTransfer};

                    var q = new Query().Select(columns).From(tableName).Insert(values);

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
