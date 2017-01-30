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
        Files IRepository<Files>.Create()
        {
            return new Files() { Id = 0 };
        }

        void IRepository<Files>.Update(Files File)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Save(File);

                    }
                    catch (Exception e)
                    {
                        //вывод е в лог
                        transaction.Rollback();
                        throw;
                    }
                    transaction.Commit();
                }

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
