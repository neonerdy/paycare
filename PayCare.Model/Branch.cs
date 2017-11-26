using System;

namespace PayCare.Model
{
    public class Branch
    {
        public Guid ID { get; set; }

        public string BranchCode { get; set; }

        public string BranchName { get; set; }

        public string Region { get; set; }

        public string BranchHead { get; set; }

        public string ViceHead { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        public string Email { get; set; }

        public decimal UMR { get; set; }

        public decimal FuelAllowance { get; set; }

        public decimal LunchAllowance { get; set; }

        public decimal TransportAllowance { get; set; }

        public bool IsActive { get; set; }
        
        public int EmployeeCounter { get; set; }

    }
}
