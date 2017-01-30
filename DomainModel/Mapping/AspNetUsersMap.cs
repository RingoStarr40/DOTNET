using DomainModel.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Mapping
{
    public class AspNetUsersMap : ClassMap<AspNetUsers>
    {
        public AspNetUsersMap()
        {
            Id(x => x.Id);
            Map(x => x.Email);
            Map(x => x.EmailConfirmed);
            Map(x => x.PasswordHash);
            Map(x => x.SecurityStamp);
            Map(x => x.PhoneNumber);
            Map(x => x.PhoneNumberConfirmed);
            Map(x => x.TwoFactorEnabled);
            Map(x => x.LockoutEndDateUtc);
            Map(x => x.LockoutEnabled);
            Map(x => x.AccessFailedCount);
            Map(x => x.UserName);

        }
    }
}
