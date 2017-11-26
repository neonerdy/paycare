using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayCare.Model
{
    public class Transfer
    {
        public Guid ID { get; set; }

        public int ActiveMonth { get; set; }

        public int ActiveYear { get; set; }

        public string TransferType { get; set; }

        public DateTime TransferDate { get; set; }

        public int TotalEmployee { get; set; }

        public decimal TotalTransfer { get; set; }

              
    }
}
