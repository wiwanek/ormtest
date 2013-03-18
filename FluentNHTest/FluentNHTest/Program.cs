using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Tool.hbm2ddl;
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
            ISessionFactory sessionFactory = Configure();

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
                    Author frank = new Author { AuthorId = Guid.NewGuid(), Name = "Frank Herbert" };
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

            Console.ReadKey();
        }

        public static ISessionFactory Configure()
        {
            return Fluently.Configure()
                //which database
                .Diagnostics(x => x.OutputToConsole())
                .Database(
                    PostgreSQLConfiguration.PostgreSQL82
                //connection string from app.config
                        .ConnectionString(
                            cs => cs.FromConnectionStringWithKey("ORMTest"))
                        .ShowSql())
                .Mappings(x => x.FluentMappings.AddFromAssemblyOf<Book>())
                .ExposeConfiguration(x => new SchemaExport(x).Create(true, true))
                .BuildSessionFactory();
        }
    }
}
