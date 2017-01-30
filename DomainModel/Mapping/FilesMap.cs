using DomainModel.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Mapping
{
    public class FilesMap : ClassMap<Files>
    {
        public FilesMap()
        {
            Id(x => x.Id);
            Map(x => x.FileName);
            Map(x => x.Date);
            References(x => x.Author, "UserId").Cascade.SaveUpdate().Not.LazyLoad();
            Map(x => x.FileData);
        }
    }
}
