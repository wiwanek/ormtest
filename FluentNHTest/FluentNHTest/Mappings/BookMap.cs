using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentNHTest.Mappings
{
    public class BookMap : ClassMap<Book>
    {
        public BookMap()
        {
            Table("book");
            Id(x => x.BookId, "bookid").GeneratedBy.GuidComb();
            Map(x => x.Title, "title").Length(200);
            References<Author>(x => x.Author, "authorid");
        }
    }
}
