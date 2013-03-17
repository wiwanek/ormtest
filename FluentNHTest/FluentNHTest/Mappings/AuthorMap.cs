using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentNHTest.Mappings
{
    public class AuthorMap : ClassMap<Author>
    {
        public AuthorMap()
        {
            Table("author");
            Id(x => x.AuthorId, "authorid").GeneratedBy.GuidComb();
            Map(x => x.Name, "name").Length(50);

        }
    }
}
