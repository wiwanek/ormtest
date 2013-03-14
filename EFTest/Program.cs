using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMTest
{
    class Program
    {
        static void Main(string[] args)
        {

            //using (var context = new LibraryContext())
            //{
            //    try
            //    {

            //        Author b = context.Authors.Add(new Author { AuthorId = Guid.NewGuid(), Name = "Brian Herbert" });
            //        context.Books.Add(new Book { BookId = Guid.NewGuid(), AuthorId = b.AuthorId, Title = "Dune" });
            //        context.SaveChanges();
            //    }
            //    catch (Exception e)
            //    {
            //        Console.Error.WriteLine(e.ToString());
            //    }
            //}

            using (var context = new LibraryContext())
            {
                try
                {
                    var books = context.Books.Include("Author").ToList();
                    
                    foreach (var book in books)
                    {
                        Console.WriteLine(book.Title + " Author: " + book.Author.Name);
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.ToString());
                }
            }

            //using (var context = new LibraryContext())
            //{
            //    try
            //    {
            //        var books = context.Books.Where(b => b.Title == "Dune").ToList();
            //        Author frank = new Author { AuthorId = Guid.NewGuid(), Name = "Frank Herbert" };
            //        foreach (var book in books)
            //        {
            //            book.Author = frank;
            //        }
            //        context.SaveChanges();
            //    }
            //    catch (Exception e)
            //    {
            //        Console.Error.WriteLine(e.ToString());
            //    }
            //}

            //using (var context = new LibraryContext())
            //{
            //    try
            //    {
            //        var books = context.Books.Include("Author").ToList();

            //        foreach (var book in books)
            //        {
            //            Console.WriteLine(book.Title + " Author: " + book.Author.Name);
            //        }
            //    }
            //    catch (Exception e)
            //    {
            //        Console.Error.WriteLine(e.ToString());
            //    }
            //}
            Console.ReadKey();
        }

    }
}
