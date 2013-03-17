using Iesi.Collections.Generic;
using System;

namespace FluentNHTest
{
    public class Author
    {
        public Author()
        {
          //  Books = new HashedSet<Book>();
        }

        public virtual Guid AuthorId { get; set; }
        public virtual string Name { get; set; }

        //public virtual ISet<Book> Books { get; set; }
    }
}
