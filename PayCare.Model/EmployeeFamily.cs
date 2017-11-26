using System;

namespace PayCare.Model
{
    public class EmployeeFamily
    {
        public Guid ID { get; set; }

        public Guid EmployeeId { get; set; }

        public int Status { get; set; }

        public string FamilyName { get; set; }

        public string Education { get; set; }

        public string BirthPlace { get; set; }

        public DateTime BirthDate { get; set; }

        public string Job { get; set; }

        public bool IsInsurance { get; set; }
        
    }
}
