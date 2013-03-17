using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
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
            Configure();

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
