using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Mapping.Attributes;

namespace AttrNHTest
{
    [Class]
    public class Author
    {
        [Id(Name = "AuthorId")]
        public virtual Guid AuthorId { get; set; }

        [Property]
        public virtual string Name { get; set; }
    }
}
