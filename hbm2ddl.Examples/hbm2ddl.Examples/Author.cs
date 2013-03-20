using Iesi.Collections.Generic;
using System;

namespace hbm2ddl.Examples
{
    public class Author
    {
        public virtual Guid AuthorId { get; set; }
        public virtual string Name { get; set; }
    }
}
