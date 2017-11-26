using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityMap;
using System.Data;
using PayCare.Model;


namespace PayCare.Repository.Mapping
{
    public class PTKPMapper : IDataMapper<PTKP>
    {
        public PTKP Map(IDataReader rdr)
        {
            var ptkp = new PTKP();

            ptkp.ID = rdr["ID"] is DBNull ? Guid.Empty : (Guid)rdr["ID"];
            ptkp.PTKPCode = rdr["PTKPCode"] is DBNull ? string.Empty : (string)rdr["PTKPCode"];
            ptkp.PTKPName = rdr["PTKPName"] is DBNull ? string.Empty : (string)rdr["PTKPName"];

            ptkp.EffectiveDate = rdr["EffectiveDate"] is DBNull ? DateTime.Now : (DateTime)rdr["EffectiveDate"];


            ptkp.TaxValue = rdr["TaxValue"] is DBNull ? 0 : (decimal)rdr["TaxValue"];
            ptkp.MaritalValue = rdr["MaritalValue"] is DBNull ? 0 : (decimal)rdr["MaritalValue"];
            ptkp.ChildValue = rdr["ChildValue"] is DBNull ? 0 : (decimal)rdr["ChildValue"];
            ptkp.Total = rdr["Total"] is DBNull ? 0 : (decimal)rdr["Total"];

            ptkp.NumberOfChild = rdr["NumberOfChild"] is DBNull ? 0 : (int)rdr["NumberOfChild"];
            
            return ptkp;
        }

    }
}
