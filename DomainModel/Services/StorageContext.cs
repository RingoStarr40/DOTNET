using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Services
{
    public class StorageContext : DbContext
    {
        public StorageContext()
            :base("DefaultConnection")
        {

        }

        public DbSet<Files> File { get; set; }

        public DbSet<AspNetUsers> AspNetUsers { get; set; }
    }
}
