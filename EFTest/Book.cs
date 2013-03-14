using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ORMTest
{
    [Table("book",Schema="public")]
    class Book
    {
        [Key]
        [Column("bookid"),Required]
        public Guid BookId { get; set; }
        [Column("authorid")]
        public Guid AuthorId { get; set; }
        [Column("title"),MaxLength(100)]
        public string Title { get; set; }

        [ForeignKey("AuthorId")]
        public virtual Author Author { get; set; }
    }
}
