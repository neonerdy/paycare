using System;

namespace PayCare.Model
{
    public class Department
    {
        public Guid ID { get; set; }

        public Guid BranchId { get; set; }

        public Branch Branch { get; set; }

        public string DepartmentCode { get; set; }

        public string DepartmentName { get; set; }

        public string DepartmentHead { get; set; }

        public bool IsActive { get; set; }
    }
}
