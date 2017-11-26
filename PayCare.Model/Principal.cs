using System;

namespace PayCare.Model
{
    public class Principal
    {
        public Guid ID { get; set; }

        public string PrincipalCode { get; set; }

        public string PrincipalName { get; set; }

        public string ContactPerson { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        public string Email { get; set; }

        public string Notes { get; set; }

        public DateTime CutOffDate { get; set; }

        public bool IsActive { get; set; }

    }
}
