using System;

namespace PayCare.Model
{
    public class Company
    {
        public Guid ID { get; set; }

        public string CompanyCode { get; set; }

        public string CompanyName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        public string Email { get; set; }

        public string Notes { get; set; }

        public string BankName { get; set; }

        public int SalaryCutOffDate { get; set; }

        public int MainSalaryDivider { get; set; }

        public int OverTimeHourDivider { get; set; }

        public int OverTimeMaximumHour { get; set; }

        public double OverTimeMultiply { get; set; }

        public double OverTimeMultiplyHoliday { get; set; }

    }
}
