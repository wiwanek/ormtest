using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyEagerLoadingEF
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer<LibraryContext>(null);
            using (var context = new LibraryContext())
            {
                Author b = context.Authors.Add(new Author { AuthorId = Guid.NewGuid(), Name = "Frank Herbert" });
                context.Books.Add(new Book { BookId = Guid.NewGuid(), AuthorId = b.AuthorId, Title = "Dune" });
                context.Books.Add(new Book { BookId = Guid.NewGuid(), AuthorId = b.AuthorId, Title = "Dune Messiah" });
                context.Books.Add(new Book { BookId = Guid.NewGuid(), AuthorId = b.AuthorId, Title = "Children of Dune" });
                context.SaveChanges();

            }
            using (var context = new LibraryContext())
            {
                var books = context.Books.Include("Author").ToList();

                foreach (var book in books)
                {
                    Console.WriteLine(book.Title + " Author: " + book.Author.Name);
                }
            }
            using (var context = new LibraryContext())
            {
                var authors = context.Authors.Include("Books").ToList();

                foreach (var author in authors)
                {
                    Console.WriteLine(author.Name + " \nBooks: ");
                    foreach (var book in author.Books)
                    {
                        Console.WriteLine("\t " + book.Title);
                    }
                }
            }
            using (var context = new LibraryContext())
            {
                var books = context.Books.ToList();
                foreach (var book in books)
                {
                    context.Books.Remove(book);
                }
                foreach (var author in context.Authors.ToList())
                {
                    context.Authors.Remove(author);
                }
                context.SaveChanges();
            }
            Console.ReadKey();
        }
    }
}
