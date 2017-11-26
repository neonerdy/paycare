using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using EntityMap;
using PayCare.Model;

namespace PayCare.Repository.Mapping
{
    public class UserLoginMapper : IDataMapper<UserLogin>
    {
        public UserLogin Map(IDataReader rdr)
        {
            var user = new UserLogin();

            user.ID = rdr["ID"] is DBNull ? Guid.Empty : (Guid)rdr["ID"];
            user.UserName = rdr["UserName"] is DBNull ? string.Empty : (string)rdr["UserName"];
            user.UserPassword = rdr["UserPassword"] is DBNull ? string.Empty : (string)rdr["UserPassword"];
            user.FullName = rdr["FullName"] is DBNull ? string.Empty : (string)rdr["FullName"];
            user.IsAdministrator = rdr["IsAdministrator"] is DBNull ? false : (bool)rdr["IsAdministrator"];
            
            return user;
        }
    }
}
