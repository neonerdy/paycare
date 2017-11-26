using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityMap;
using PayCare.Model;
using System.Data;

namespace PayCare.Repository.Mapping
{
    public class InsuranceValueMapper : IDataMapper<InsuranceValue>
    {

        public InsuranceValue Map(IDataReader rdr)
        {
            var insuranceValue = new InsuranceValue();

            insuranceValue.ByCompany = rdr["ByCompany"] is DBNull ? 0 : (double)rdr["ByCompany"];
            insuranceValue.ByEmployee = rdr["ByEmployee"] is DBNull ? 0 : (double)rdr["ByEmployee"];
            insuranceValue.ByEmployeeFemale = rdr["ByEmployeeFemale"] is DBNull ? 0 : (double)rdr["ByEmployeeFemale"];

            return insuranceValue;
        }

    }


    public class InsuranceProgramMapper : IDataMapper<InsuranceProgram>
    {

        public InsuranceProgram Map(IDataReader rdr)
        {
            var insuranceProgram = new InsuranceProgram();

            insuranceProgram.ID = rdr["ID"] is DBNull ? Guid.Empty : (Guid)rdr["ID"];
            insuranceProgram.InsuranceId = rdr["InsuranceId"] is DBNull ? Guid.Empty : (Guid)rdr["InsuranceId"];

            if (insuranceProgram.Insurance == null) insuranceProgram.Insurance = new Insurance();
            insuranceProgram.Insurance.InsuranceCode = rdr["InsuranceCode"] is DBNull ? string.Empty : (string)rdr["InsuranceCode"];
            insuranceProgram.Insurance.InsuranceName = rdr["InsuranceName"] is DBNull ? string.Empty : (string)rdr["InsuranceName"];

            insuranceProgram.Program = rdr["Program"] is DBNull ? string.Empty : (string)rdr["Program"];
            insuranceProgram.PercentageByCompany = rdr["PercentageByCompany"] is DBNull ? 0 : (double)rdr["PercentageByCompany"];
            insuranceProgram.PercentageByEmployee = rdr["PercentageByEmployee"] is DBNull ? 0 : (double)rdr["PercentageByEmployee"];
            insuranceProgram.PercentageByEmployeeFemale = rdr["PercentageByEmployeeFemale"] is DBNull ? 0 : (double)rdr["PercentageByEmployeeFemale"];
            
            return insuranceProgram;

        }

    }
}
