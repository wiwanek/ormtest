using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using NHibernate.Tool.hbm2ddl;
using Npgsql;
using System;
using System.Collections.Generic;

namespace NHTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Configuration config = new Configuration();
                config.Configure();
                config.AddAssembly(typeof(Author).Assembly);
                ISessionFactory sessionFactory = config.BuildSessionFactory();

                var schema = new SchemaExport(config);
                schema.Create(true, true);
                
                using (ISession session = sessionFactory.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        Author a = new Author { AuthorId = Guid.NewGuid(), Name = "Brian Herbert" };
                        session.Save(a);

                        session.Save(new Book { BookId = Guid.NewGuid(), Author = a, Title = "Dune" });
                        
                        transaction.Commit();
                    }

                    IList<Book> books = session.CreateCriteria(typeof(Book)).List<Book>();
                    foreach (var book in books)
                    {
                        Console.WriteLine(book.Title + " - " + book.Author.Name);
                    }

                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        Author frank = new Author {AuthorId = Guid.NewGuid(), Name = "Frank Herbert"};
                        session.Save(frank);
                        Book b = session.CreateCriteria(typeof(Book)).Add(Restrictions.Eq("Title", "Dune")).UniqueResult<Book>();
                        if (b != null)
                        {
                            b.Author = frank;
                        }
                        session.Update(b);

                        transaction.Commit();
                    }

                    books = session.CreateCriteria(typeof(Book)).List<Book>();
                    foreach (var book in books)
                    {
                        Console.WriteLine(book.Title + " - " + book.Author.Name);
                    }

                    
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.ToString());
            }
            
            Console.ReadKey();
        }
    }
}