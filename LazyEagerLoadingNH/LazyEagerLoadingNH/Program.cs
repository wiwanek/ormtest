using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentNHTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ISessionFactory sessionFactory = Configure();

                // Fill DB with data
                using (ISession session = sessionFactory.OpenSession())
                {
                    using (ITransaction transaction = session.BeginTransaction())
                    {
                        Author a = new Author { AuthorId = Guid.NewGuid(), Name = "Frank Herbert" };
                        session.Save(a);

                        session.Save(new Book { BookId = Guid.NewGuid(), Author = a, Title = "Dune" });
                        session.Save(new Book { BookId = Guid.NewGuid(), Author = a, Title = "Dune Messiah" });
                        session.Save(new Book { BookId = Guid.NewGuid(), Author = a, Title = "Children of Dune" });

                        Author b = new Author { AuthorId = Guid.NewGuid(), Name = "John Ronald Reuel Tolkien" };
                        session.Save(b);

                        session.Save(new Book { BookId = Guid.NewGuid(), Author = b, Title = "The Hobbit" });
                        session.Save(new Book { BookId = Guid.NewGuid(), Author = b, Title = "The Lord of the Rings" });

                        transaction.Commit();
                    }
                }

                // Lazy loading examples
                using (ISession session = sessionFactory.OpenSession())
                {
                    IList<Book> books = session.CreateCriteria<Book>().List<Book>();
                    foreach (Book b in books)
                    {
                        Console.Out.WriteLine(b.Title + " Author: " + b.Author.Name);
                    }
                }
                using (ISession session = sessionFactory.OpenSession())
                {
                    IList<Author> authors = session.CreateCriteria<Author>().List<Author>();
                    foreach (Author a in authors)
                    {
                        Console.Out.WriteLine(a.Name + " books:");
                        foreach(Book b in a.Books) {
                            Console.Out.WriteLine("\t" + b.Title);
                        }
                    }
                }

                // Eager loading examples
                using (ISession session = sessionFactory.OpenSession())
                {
                    IList<Book> books = session.CreateCriteria<Book>().SetFetchMode("Author",FetchMode.Eager).List<Book>();
                    
                    foreach (Book b in books)
                    {
                        Console.Out.WriteLine(b.Title + " Author: " + b.Author.Name);
                    }
                }
                using (ISession session = sessionFactory.OpenSession())
                {
                    IList<Author> authors = session.CreateCriteria<Author>().SetFetchMode("Books", FetchMode.Eager).List<Author>();
                    foreach (Author a in authors)
                    {
                        Console.Out.WriteLine(a.Name + " books:");
                        foreach (Book b in a.Books)
                        {
                            Console.Out.WriteLine("\t" + b.Title);
                        }
                    }
                }
                // Fixed multiple author occurences
                using (ISession session = sessionFactory.OpenSession())
                {
                    IList<Author> authors = session.CreateCriteria<Author>().SetFetchMode("Books", FetchMode.Eager).SetResultTransformer(new DistinctRootEntityResultTransformer()).List<Author>();
                    foreach (Author a in authors)
                    {
                        Console.Out.WriteLine(a.Name + " books:");
                        foreach (Book b in a.Books)
                        {
                            Console.Out.WriteLine("\t" + b.Title);
                        }
                    }
                }
                // using QueryOver
                using (ISession session = sessionFactory.OpenSession())
                {
                    IList<Author> authors = session.QueryOver<Author>().Fetch(a => a.Books).Eager.TransformUsing(Transformers.DistinctRootEntity).List();
                    foreach (Author a in authors)
                    {
                        Console.Out.WriteLine(a.Name + " books:");
                        foreach (Book b in a.Books)
                        {
                            Console.Out.WriteLine("\t" + b.Title);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);

            }
            Console.ReadKey();

        }

        public static ISessionFactory Configure()
        {
            return Fluently.Configure()
                .Database(
                    PostgreSQLConfiguration.PostgreSQL82
                        .ConnectionString(
                            cs => cs.FromConnectionStringWithKey("ORMTest"))
                        .ShowSql())
                .Mappings(x => x.FluentMappings.AddFromAssemblyOf<Book>())
                .ExposeConfiguration(x => new SchemaExport(x).Create(true, true))
                .BuildSessionFactory();
        }
    }
}
