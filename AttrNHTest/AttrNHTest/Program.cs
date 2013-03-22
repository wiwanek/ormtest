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
                Stream stream = HbmSerializer.Default.Serialize(typeof(Author));
                cfg.AddInputStream(stream);
                ISessionFactory sf = cfg.BuildSessionFactory();
                new SchemaExport(cfg).Create(true, true);
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
