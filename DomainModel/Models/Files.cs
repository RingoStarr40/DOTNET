﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Models
{
    public class Files
    {
        [Key]
        public virtual int Id { get; set; }

        [MaxLength(50)]
        public virtual string FileName { get; set; }

        public virtual DateTime Date { get; set; }

        [MaxLength(128)]
        public virtual string UserId { get; set; }

        public virtual AspNetUsers Author { get; set; }

        public virtual byte[] FileData{ get; set; }
    }
}
