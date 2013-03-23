using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Mapping.Attributes;
using System.Reflection;
using System.IO;

namespace AttrNHTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Configuration cfg = new Configuration();
                cfg.Configure();
                HbmSerializer.Default.Serialize(Console.OpenStandardOutput(), Assembly.GetExecutingAssembly());
                Stream stream = HbmSerializer.Default.Serialize(Assembly.GetExecutingAssembly()); ;
                cfg.AddInputStream(stream);
                ISessionFactory sf = cfg.BuildSessionFactory();
                new SchemaExport(cfg).Create(true, true);

                using (ISession session = sf.OpenSession())
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

            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.ToString());
                Console.Error.WriteLine(HbmSerializer.Default.Error.ToString());
            }
            Console.ReadKey();
        }
    }
}
