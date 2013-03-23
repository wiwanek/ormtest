using System;
using NHibernate.Mapping.Attributes;

namespace AttrNHTest
{
    [Class]
    public class Book
    {
        [Id(Name="BookId")]
        public virtual Guid BookId { get; set; }
        [Property(Length=200)]
        public virtual string Title { get; set; }

        [ManyToOne]
        public virtual Author Author { get; set; }
    }
}
