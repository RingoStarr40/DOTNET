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
        T Load(int Id);
        bool Delete(int Id);
        IEnumerable<Files> GetAll();

        IEnumerable<Files> SearchFiles(string name);
    }
}
