using Rise.Contact.API.Data;
using Rise.Contact.API.Models;
using Rise.Contact.API.Utils;

namespace Rise.Contact.API.Services
{
    public interface IContactService
    {
        ContactGetByIdResponse GetContact(Guid contactId);
        List<ContactListResponse> GetContacts();
        List<ContactListResponse> GetContacts(ContactFilterRequest filter);
        ContactResponse CreateContact(ContactRequest req);
        void UpdateContact(ContactUpdateRequest req);
        void RemoveContact(Guid contactId);
    }

    public class ContactService : IContactService
    {
        private readonly IApplicationContext _context;
        private readonly ContactDbContext _dbContext;

        public ContactService(IApplicationContext context, ContactDbContext dbContext)
        {
            _context = context;
            _dbContext = dbContext;
        }

        public ContactResponse CreateContact(ContactRequest req)
        {
            if (string.IsNullOrEmpty(req.FirstName) || string.IsNullOrEmpty(req.LastName))
            {
                _context.AddReturnMessage("Ad ve soyad girilmelidir");
                return default;
            }

            if (string.IsNullOrEmpty(req.CompanyName))
            {
                _context.AddReturnMessage("Firma ismi girilmelidir");
                return default;
            }

            var contact = new Rise.Contact.API.Entities.Contact
            {
            };


            _dbContext.SaveChanges();

          

            return new ContactResponse
            {

            };
        }

        public ContactGetByIdResponse GetContact(Guid contactId)
        {
            var contact = _dbContext.Contacts.SoftDelCondition().FirstOrDefault(x => x.Id == contactId);

            if (contact == null)
                return default;

            return new ContactGetByIdResponse
            {
            };
        }

        public List<ContactListResponse> GetContacts()
        {
            return GetContacts(new ContactFilterRequest
            {
                BeganDate = null,
                EndDate = null,
                ContactId = Guid.Empty
            });
        }

        public List<ContactListResponse> GetContacts(ContactFilterRequest filter)
        {
            var queryEntity = _dbContext.Contacts.SoftDelCondition();

            if (filter.ContactId != Guid.Empty)
            {
                queryEntity = queryEntity.Where(x => x.Id == filter.ContactId );
            }

            if (!string.IsNullOrEmpty(filter.BeganDate) && !string.IsNullOrEmpty(filter.EndDate))
            {
                try
                {
                    var beganDate = SystemDateTime.SetDate(filter.BeganDate);
                    var endDate = SystemDateTime.SetDate(filter.EndDate);

                    queryEntity = queryEntity.Where(x => x.CreatedDate >= beganDate && x.CreatedDate <= endDate);

                    if (beganDate == endDate)
                    {
                        var beganTime = SystemDateTime.SetTime(filter.BeganDate);
                        var endTime = SystemDateTime.SetTime(filter.EndDate);

                        queryEntity = queryEntity.Where(x => x.CreatedTime >= beganTime && x.CreatedTime <= endTime);
                    }
                }
                catch { }
            }

            var query = queryEntity.Select(o => new ContactListResponse
            {
                
            });

            return query.ToList();
        }

        public void RemoveContact(Guid contactId)
        {
            var order = _dbContext.Contacts.Where(x => x.Id == contactId).FirstOrDefault();

            if (order != null)
            {
                order.DeleteBy = _context.UserId;
                order.DeletedDate = SystemDateTime.NowDate();
                order.DeletedTime = SystemDateTime.NowTime();

                _dbContext.SaveChanges();
            }
        }

        public void UpdateContact(ContactUpdateRequest req)
        {
            if (string.IsNullOrEmpty(req.FirstName) || string.IsNullOrEmpty(req.LastName))
            {
                _context.AddReturnMessage("Ad ve soyad girilmelidir");
            }

            if (string.IsNullOrEmpty(req.CompanyName))
            {
                _context.AddReturnMessage("Firma ismi girilmelidir");
            }


            var contact = _dbContext.Contacts.FirstOrDefault(x => x.Id == req.Id);

            if (contact != null)
            {
                var contactInfo = _dbContext.contactInfos.FirstOrDefault(x => x.ContactId == contact.Id);

               
            
                _dbContext.SaveChanges();
            }
        }
    }
}
