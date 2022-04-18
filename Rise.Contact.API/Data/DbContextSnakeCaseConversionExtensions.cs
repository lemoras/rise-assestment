using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Rise.Contact.API.Data
{
    public static class DbContextSnakeCaseConversionExtensions
    {
        public static void ConvertAllToSnakeCase(this ModelBuilder modelBuilder)
        { 
            foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.GetTableName().ToSnakeCase());

                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.GetColumnBaseName().ToSnakeCase());
                }

                foreach (var key in entity.GetKeys())
                {
                    var keyName = key.GetName().ToSnakeCase().Substring(3) + "_pkey";
                    key.SetName(keyName);
                }

                foreach (var key in entity.GetForeignKeys())
                {
                    key.SetConstraintName(key.GetConstraintName().ToSnakeCase());
                }

                foreach (var index in entity.GetIndexes())
                {
                    index.SetDatabaseName(index.Name.ToSnakeCase());
                }
            }
        }
    }
}