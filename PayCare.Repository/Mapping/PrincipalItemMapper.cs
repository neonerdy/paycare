using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityMap;
using PayCare.Model;
using System.Data;

namespace PayCare.Repository.Mapping
{
    public class PrincipalItemValueMapper : IDataMapper<PrincipalItemValue>
    {

        public PrincipalItemValue Map(IDataReader rdr)
        {
            var principalItemValue = new PrincipalItemValue();

            principalItemValue.MainSalary = rdr["MainSalary"] is DBNull ? 0 : (decimal)rdr["MainSalary"];
            principalItemValue.LunchAllowance = rdr["LunchAllowance"] is DBNull ? 0 : (decimal)rdr["LunchAllowance"];
            principalItemValue.TransportationAllowance = rdr["TransportationAllowance"] is DBNull ? 0 : (decimal)rdr["TransportationAllowance"];

            return principalItemValue;
        }

    }


    public class PrincipalItemMapper : IDataMapper<PrincipalItem>
    {
        public PrincipalItem Map(IDataReader rdr)
        {
            var principalItem = new PrincipalItem();

            principalItem.ID = rdr["ID"] is DBNull ? Guid.Empty : (Guid)rdr["ID"];
            principalItem.PrincipalId = rdr["PrincipalId"] is DBNull ? Guid.Empty : (Guid)rdr["PrincipalId"];

            if (principalItem.Principal == null) principalItem.Principal = new Principal();
            principalItem.Principal.PrincipalCode = rdr["PrincipalCode"] is DBNull ? string.Empty : (string)rdr["PrincipalCode"];
            principalItem.Principal.PrincipalName = rdr["PrincipalName"] is DBNull ? string.Empty : (string)rdr["PrincipalName"];

            principalItem.EffectiveDate = rdr["EffectiveDate"] is DBNull ? DateTime.Now : (DateTime)rdr["EffectiveDate"];
            principalItem.Reference = rdr["Reference"] is DBNull ? string.Empty : (string)rdr["Reference"];
            principalItem.MainSalary = rdr["MainSalary"] is DBNull ? 0 : (decimal)rdr["MainSalary"];
            principalItem.LunchAllowance = rdr["LunchAllowance"] is DBNull ? 0 : (decimal)rdr["LunchAllowance"];
            principalItem.TransportationAllowance = rdr["TransportationAllowance"] is DBNull ? 0 : (decimal)rdr["TransportationAllowance"];

            return principalItem;

        }

    }
}
