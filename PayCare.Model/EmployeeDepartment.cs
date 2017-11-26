using System;

namespace PayCare.Model
{
    public class EmployeeDepartment
    {
        public Guid ID { get; set; }

        public Guid EmployeeId { get; set; }

        public DateTime EffectiveDate { get; set; }

        public Guid BranchId { get; set; }
        
        public string BranchName { get; set; }

        public Guid DepartmentId { get; set; }

        public string DepartmentName { get; set; }
     
    }
}
