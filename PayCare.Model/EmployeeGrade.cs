using System;

namespace PayCare.Model
{
    public class EmployeeGrade
    {
        public Guid ID { get; set; }

        public Guid EmployeeId { get; set; }

        public DateTime EffectiveDate { get; set; }

        public Guid GradeId { get; set; }

        public string GradeName { get; set; }

        public int GradeLevel { get; set; }
     
    }

}
