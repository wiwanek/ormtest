using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAccessTest
{
    public class Book
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public Author Author { get; set; }
    }
}
