using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyEagerLoadingEF
{
    [Table("book", Schema = "public")]
    class Book
    {
        [Key]
        [Column("bookid"), Required]
        public Guid BookId { get; set; }

        [ForeignKey("Author")]
        [Column("authorid")]
        public Guid AuthorId { get; set; }

        [Column("title"), MaxLength(100)]
        public string Title { get; set; }

        public virtual Author Author { get; set; }
    }
}
