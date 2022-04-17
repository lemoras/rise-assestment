using Rise.Contact.API.Utils;
using Microsoft.EntityFrameworkCore;

namespace Rise.Contact.API.Data
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ContactDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ContactDbContext>>()))
            {
                if (!context.Contacts.Any())
                {
                    var contact1 = new Rise.Contact.API.Entities.Contact
                    {
                        FirstName = "Onur",
                        LastName = "Yasar",
                        CompanyName = "A firma",
                        CreatedDate = SystemDateTime.NowDate(),
                        CreatedTime = SystemDateTime.NowTime(),
                        CreateBy = Guid.NewGuid()
                    };

                    context.Contacts.AddRange(
                        contact1
                    );

                    context.SaveChanges();
                }
            }
        }
    }
}
