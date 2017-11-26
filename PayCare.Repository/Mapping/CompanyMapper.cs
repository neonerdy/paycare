using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityMap;
using System.Data;
using PayCare.Model;


namespace PayCare.Repository.Mapping
{
    public class CompanyMapper : IDataMapper<Company>
    {
        public Company Map(IDataReader rdr)
        {
            var company = new Company();

            company.ID = rdr["ID"] is DBNull ? Guid.Empty : (Guid)rdr["ID"];
            company.CompanyCode = rdr["CompanyCode"] is DBNull ? string.Empty : (string)rdr["CompanyCode"];
            company.CompanyName = rdr["CompanyName"] is DBNull ? string.Empty : (string)rdr["CompanyName"];
            company.Address = rdr["Address"] is DBNull ? string.Empty : (string)rdr["Address"];
            company.Phone = rdr["Phone"] is DBNull ? string.Empty : (string)rdr["Phone"];
            company.Fax = rdr["Fax"] is DBNull ? string.Empty : (string)rdr["Fax"];
            company.Email = rdr["Email"] is DBNull ? string.Empty : (string)rdr["Email"];
            company.Notes = rdr["Notes"] is DBNull ? string.Empty : (string)rdr["Notes"];
            company.BankName = rdr["BankName"] is DBNull ? string.Empty : (string)rdr["BankName"];
            company.SalaryCutOffDate = rdr["SalaryCutOffDate"] is DBNull ? 0 : (int)rdr["SalaryCutOffDate"];
            company.MainSalaryDivider = rdr["MainSalaryDivider"] is DBNull ? 0 : (int)rdr["MainSalaryDivider"];
            company.OverTimeHourDivider = rdr["OverTimeHourDivider"] is DBNull ? 0 : (int)rdr["OverTimeHourDivider"];
            company.OverTimeMaximumHour = rdr["OverTimeMaximumHour"] is DBNull ? 0 : (int)rdr["OverTimeMaximumHour"];
            company.OverTimeMultiply = rdr["OverTimeMultiply"] is DBNull ? 0 : (double)rdr["OverTimeMultiply"];
            company.OverTimeMultiplyHoliday = rdr["OverTimeMultiplyHoliday"] is DBNull ? 0 : (double)rdr["OverTimeMultiplyHoliday"];

            return company;
        }

    }
}
