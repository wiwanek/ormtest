using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAccessTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (OpenAccessTestContext dbContext = new OpenAccessTestContext())
            {
                dbContext.UpdateSchema();
                Author a = new Author
                {
                    AuthorId = Guid.NewGuid(),
                    Name = "Brian Herbert"
                };

                Book b = new Book
                {
                    BookId = Guid.NewGuid(),
                    Title = "Dune",
                    Author = a
                };

                dbContext.Add(a);
                dbContext.Add(b);

                dbContext.SaveChanges();

                foreach (Book item in dbContext.Books)
                {
                    Console.WriteLine(item.Title + ", " + item.Author.Name);
                }
            }
            using (OpenAccessTestContext dbContext = new OpenAccessTestContext())
            {
                Author frank = new Author()
                {
                    AuthorId = Guid.NewGuid(),
                    Name = "Frank Herbert"
                };
                dbContext.Add(frank);
                var books = dbContext.Books.Where(b => b.Title == "Dune").ToList();
                foreach (var book in books)
                {
                    book.Author = frank;
                }
                dbContext.SaveChanges();

                foreach (Book item in dbContext.Books)
                {
                    Console.WriteLine(item.Title + ", " + item.Author.Name);
                }
            }
            Console.ReadKey();
        }
    }
}
