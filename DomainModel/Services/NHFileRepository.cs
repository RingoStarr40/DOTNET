using DomainModel.Helpers;
using DomainModel.Models;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Services
{
    public class NHFileRepository : IFilesRepository
    {
        public Files Create()
        {
            return new Files() { Id = 0 };
        }

        public bool Delete(int Id)
        {
            var item = Load(Id);
            if (item == null)
                return false;
            using (var db = new StorageContext())
            {
                db.File.Remove(item);
                db.SaveChanges();
            }
            return true;
        }

        public Files Load(int Id)
        {
            using (var db = new StorageContext())
            {
                return db.File.FirstOrDefault(o => o.Id == Id);
            }
        }

       
        public IEnumerable<Files> GetAll()
        {
            var files = new List<Files>();

            using (var session = NHibernateHelper.OpenSession())
            {
                var criteria = session.CreateCriteria(typeof(Files)); //создание новой критерии. Создание запроса к базе по нашему классу
                //criteria.Add(Restrictions.Ge("Id", 3));
                files = criteria.List<Files>().ToList();
            }

            return files;
        }

        public IEnumerable<Files> SearchFiles(string name)
        {
            var files = new List<Files>();

            using (var session = NHibernateHelper.OpenSession())
            {
                var criteria = session.CreateCriteria(typeof(Files)); //создание новой критерии. Создание запроса к базе по нашему классу
                criteria.Add(Restrictions.Eq("FileName", name));
                files = criteria.List<Files>().ToList();
            }

            return files;
        }
    }
}
