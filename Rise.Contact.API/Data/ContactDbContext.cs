using Microsoft.EntityFrameworkCore;

namespace Rise.Contact.API.Data
{
    public class ContactDbContext : DbContext
    {
        public ContactDbContext(DbContextOptions<ContactDbContext> options)
        : base(options) { }

        public DbSet<Rise.Contact.API.Entities.Contact> Contacts { get; set; }
        public DbSet<Rise.Contact.API.Entities.ContactInfo> contactInfos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConvertAllToSnakeCase();
        }
    }
}
