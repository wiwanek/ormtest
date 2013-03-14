using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System;

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
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.ToString());
            }
            Console.ReadKey();
        }
    }
}