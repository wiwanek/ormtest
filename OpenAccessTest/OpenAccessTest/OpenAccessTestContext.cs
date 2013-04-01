using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;

namespace OpenAccessTest
{
    public class OpenAccessTestContext : OpenAccessContext
    {
        static MetadataContainer metadataContainer = new EntitiesModelMetadataSource().GetModel();
        static BackendConfiguration backendConfiguration = new BackendConfiguration();
        private const string DbConnectionString = "ORMTest";
        public OpenAccessTestContext()
            : base(DbConnectionString, backendConfiguration, metadataContainer)
        {
            backendConfiguration.Logging.LogEvents = LoggingLevel.Normal;
            backendConfiguration.Logging.LogEventsToSysOut = true;
        }

        public IQueryable<Author> Authors
        {
            get
            {
                return this.GetAll<Author>();
            }
        }

        public IQueryable<Book> Books
        {
            get
            {
                return this.GetAll<Book>();
            }
        }

        public void UpdateSchema()
        {
            var handler = this.GetSchemaHandler();
            string script = null;
            try
            {
                script = handler.CreateUpdateDDLScript(null);
            }
            catch
            {
                bool throwException = false;
                try
                {
                    handler.CreateDatabase();
                    script = handler.CreateDDLScript();
                }
                catch
                {
                    throwException = true;
                }
                if (throwException)
                    throw;
            }
            if (string.IsNullOrEmpty(script) == false)
            {
                handler.ExecuteDDLScript(script);
            }
        }

        public void DropSchema()
        {
            var handler = this.GetSchemaHandler();
            string script = null;
            try
            {
                script = handler.CreateDDLScript();
                
            }
            catch
            {
                bool throwException = false;
                try
                {
                    handler.CreateDatabase();
                    script = handler.CreateDDLScript();
                }
                catch
                {
                    throwException = true;
                }
                if (throwException)
                    throw;
            }
            if (string.IsNullOrEmpty(script) == false)
            {
                handler.ExecuteDDLScript(script);
            }
        }
    }
}
