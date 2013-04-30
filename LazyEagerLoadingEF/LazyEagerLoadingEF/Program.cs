﻿using System;
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
                b = context.Authors.Add(new Author { AuthorId = Guid.NewGuid(), Name = "John Ronald Reuel Tolkien" });
                context.Books.Add(new Book { BookId = Guid.NewGuid(), AuthorId = b.AuthorId, Title = "The Hobbit" });
                context.Books.Add(new Book { BookId = Guid.NewGuid(), AuthorId = b.AuthorId, Title = "The Lord of the Rings" });
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
                    Console.WriteLine("\nBefore Load(): \n" + book.Title + " Author: " + (book.Author == null ? "<null>" : book.Author.Name));
                    context.Entry<Book>(book).Reference("Author").Load();
                    Console.WriteLine("\nAfter Load(): \n" + book.Title + " Author: " + (book.Author == null ? "<null>" : book.Author.Name));
                    //Console.ReadKey();
                }
                
            }
            using (var context = new LibraryContext())
            {
                var authors = context.Authors.ToList();

                foreach (var author in authors)
                {
                    Console.WriteLine(author.Name + " \nBooks: \nBefore Load():");

                    foreach (var book in author.Books)
                    {
                        Console.WriteLine("\t " + book.Title);
                    }
                    context.Entry<Author>(author).Collection<Book>(a => a.Books).Load();
                    Console.WriteLine(" \nAfter Load():");
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
