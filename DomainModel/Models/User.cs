using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Models
{
    public class User
    {
        [Key]
        public virtual int Id { get; set; }

        [MaxLength(50)]
        public virtual string Email { get; set; }

        [MaxLength(50)]
        public virtual string PasswordHash { get; set; }

        public virtual string Author
        {
            get { return $"{Email}"; }
        }

        public virtual ISet<Files> Files { get; set; }
    }
}
