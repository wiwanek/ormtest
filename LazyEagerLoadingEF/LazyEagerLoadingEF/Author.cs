using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyEagerLoadingEF
{
    [Table("author", Schema = "public")]
    class Author
    {
        public Author()
        {
            Books = new List<Book>();
        }

        [Key, Column("authorid"), DatabaseGenerated(DatabaseGeneratedOption.None), Required]
        public Guid AuthorId { get; set; }

        [Column("name"), MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
