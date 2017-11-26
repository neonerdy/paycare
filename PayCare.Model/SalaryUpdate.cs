using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayCare.Model
{
    public class SalaryUpdate
    {
        public Guid ID { get; set; }

        public DateTime EffectiveDate { get; set; }

        public Guid BranchId { get; set; }

        public string BranchName { get; set; }

        public Guid GradeId { get; set; }

        public string GradeName { get; set; }

        public Guid OccupationId { get; set; }

        public string OccupationName { get; set; }

        public int UpdateType { get; set; }

        public decimal Percentage { get; set; }

        public decimal MainSalary { get; set; }

        public decimal LunchAllowance { get; set; }

        public decimal TransportAllowance { get; set; }

        public decimal FuelAllowance { get; set; }

        public decimal VehicleAllowance { get; set; }

        public string Notes { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }
             


    }
}
