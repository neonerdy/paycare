using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityMap;
using PayCare.Model;
using PayCare.Repository.Mapping;

namespace PayCare.Repository
{
    public interface ICompanyRepository
    {
        Company GetById(Guid id);
        Company GetAll();
        List<Company> Search(string column, string value);
        void Update(Company company);
    }

    public class CompanyRepository : ICompanyRepository
    {
        private string tableName = "Company";
        private DataSource ds;

        public CompanyRepository(DataSource ds)
        {
            this.ds = ds;
        }

        public Company GetById(Guid id)
        {
            Company company = null;
            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From(tableName).Where("ID").Equal("{" + id + "}");
                company = em.ExecuteObject<Company>(q.ToSql(), new CompanyMapper());
            }
            return company;
        }


        public Company GetAll()
        {
            Company companies = new Company();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From(tableName);
                companies = em.ExecuteObject<Company>(q.ToSql(), new CompanyMapper());
            }

            return companies;
        }

        public List<Company> Search(string column, string value)
        {
            List<Company> companies = new List<Company>();

            using (var em = EntityManagerFactory.CreateInstance(ds))
            {
                var q = new Query().From(tableName).Where(column).Equal(value);
                companies = em.ExecuteList<Company>(q.ToSql(), new CompanyMapper());
            }

            return companies;
        }



        public void Update(Company company)
        {
            try
            {

                using (var em = EntityManagerFactory.CreateInstance(ds))
                {
                    string[] columns = {"CompanyCode", "CompanyName","Address", "Phone", "Fax", "Email", "Notes", "BankName",
                                        "SalaryCutOffDate", "MainSalaryDivider", 
                                        "OverTimeHourDivider", "OverTimeMaximumHour",
                                        "OverTimeMultiply", "OverTimeMultiplyHoliday"};

                    object[] values = {company.CompanyCode, company.CompanyName, company.Address, company.Phone, company.Fax, 
                                       company.Email, company.Notes, company.BankName, 
                                       company.SalaryCutOffDate, company.MainSalaryDivider, 
                                       company.OverTimeHourDivider, company.OverTimeMaximumHour,
                                       company.OverTimeMultiply, company.OverTimeMultiplyHoliday};

                    var q = new Query().Select(columns).From(tableName).Update(values).Where("ID").Equal("{" + company.ID + "}");

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
