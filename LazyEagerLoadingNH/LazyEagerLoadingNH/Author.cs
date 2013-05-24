//using Iesi.Collections.Generic;
using System;
using System.Collections.Generic;

namespace FluentNHTest
{
    public class Author
    {
        public virtual Guid AuthorId { get; set; }
        public virtual string Name { get; set; }

        public virtual IList<Book> Books { get; set; }

        public Author()
        {
            Books = new List<Book>();
        }
        
    }
}
