using System;

namespace PayCare.Model
{
    public class Grade
    {
        public Guid ID { get; set; }

        public string GradeCode { get; set; }

        public int GradeLevel { get; set; }

        public string GradeName { get; set; }

        public string Notes { get; set; }

        public bool IsActive { get; set; }


    }
}
