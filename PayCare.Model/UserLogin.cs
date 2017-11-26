using System;

namespace PayCare.Model
{
    public class UserLogin
    {
        public Guid ID { get; set; }

        public string UserName { get; set; }

        public string UserPassword { get; set; }

        public string FullName { get; set; }

        public bool IsAdministrator { get; set; }


    }
}
