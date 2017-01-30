using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Services
{
    public interface IRepository<T> where T : class
    {
        T Create();

        IEnumerable<Files> GetAll();
        void Update(T File);
        IEnumerable<Files> SearchFiles(string name);
    }
}
