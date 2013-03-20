using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using NHibernate.Tool.hbm2ddl;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace hbm2ddl.Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Configuration config = new Configuration();
                config.Configure();
                config.AddClass(typeof(Author));

                var schema = new SchemaExport(config);
                schema.Create(true, true);

                config.AddClass(typeof(Book));
                try
                {
                    new SchemaValidator(config).Validate();
                }
                catch (HibernateException he)
                {
                    Console.Error.WriteLine("Validation failed... " + he.Message);
                    Console.WriteLine("Updating schema...");
                    new SchemaUpdate(config).Execute(true, true);

                }
                new SchemaExport(config).Drop(true, true);
                
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("E!:" + e.ToString());
            }
            Console.ReadKey();
        }
    }
}