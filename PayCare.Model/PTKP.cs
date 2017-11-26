using System;

namespace PayCare.Model
{
    public class PTKP
    {
        public Guid ID { get; set; }

        public DateTime EffectiveDate { get; set; }

        public string PTKPCode { get; set; }

        public string PTKPName { get; set; }

        public decimal TaxValue { get; set; }

        public decimal MaritalValue { get; set; }

        public decimal ChildValue { get; set; }

        public decimal Total { get; set; }

        public int NumberOfChild { get; set; }

    }
}
