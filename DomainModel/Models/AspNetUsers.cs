using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Models
{
    public class AspNetUsers
    {
        [Key]
        [MaxLength(128)]
        public virtual string Id { get; set; }

        [MaxLength(256)]
        public virtual string Email { get; set; }

        public virtual bool EmailConfirmed { get; set; }

        public virtual string PasswordHash { get; set; }


        public virtual string SecurityStamp { get; set; }

        public virtual string PhoneNumber { get; set; }

        public virtual bool PhoneNumberConfirmed { get; set; }

        public virtual bool TwoFactorEnabled { get; set; }

        public virtual DateTime LockoutEndDateUtc { get; set; }

        public virtual bool LockoutEnabled { get; set; }

        public virtual int AccessFailedCount { get; set; }

        [MaxLength(256)]
        public virtual string UserName { get; set; }


        public virtual string Author
        {
            get { return $"{Email}"; }
        }

        public virtual ISet<Files> Files { get; set; }
    }
}
