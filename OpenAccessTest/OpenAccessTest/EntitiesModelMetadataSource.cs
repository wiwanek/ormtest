using System.Collections.Generic;
using Telerik.OpenAccess.Metadata.Fluent;
namespace OpenAccessTest
{
    public class EntitiesModelMetadataSource : FluentMetadataSource
    {
        protected override IList<MappingConfiguration> PrepareMapping()
        {
            List<MappingConfiguration> configurations = new List<MappingConfiguration>();

            MappingConfiguration<Author> authorConfiguration = new MappingConfiguration<Author>();
            authorConfiguration.MapType().ToTable("Authors");
            authorConfiguration.HasProperty(a => a.AuthorId).IsIdentity();
            configurations.Add(authorConfiguration);

            MappingConfiguration<Book> bookConfiguration = new MappingConfiguration<Book>();
            bookConfiguration.MapType().ToTable("Books");
            bookConfiguration.HasProperty(p => p.BookId).IsIdentity();
            bookConfiguration.HasAssociation(p => p.Author);
            configurations.Add(bookConfiguration);

            return configurations;
        }
    }
}