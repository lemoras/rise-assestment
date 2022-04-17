using Rise.Contact.API.Data;
using Rise.Contact.API.Models;
using Rise.Contact.API.Utils;

namespace Rise.Contact.API.Services
{
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
            if (string.IsNullOrWhiteSpace(req.FirstName) || string.IsNullOrWhiteSpace(req.LastName))
            {
                _context.AddReturnMessage("Ad ve soyad girilmelidir");
                return default;
            }

            if (string.IsNullOrWhiteSpace(req.CompanyName))
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
            return _dbContext.Contacts
                .SoftDelCondition()
                .GroupJoin(_dbContext.ContactInfos, x=> x.Id, y=> y.ContactId, (x,y)=> new { x, y})
                .Where(x=> x.x.Id == contactId)
                .Select(x=> new ContactGetByIdResponse 
                {
                    Id = x.x.Id,
                    FirstName = x.x.FirstName,
                    LastName = x.x.LastName,
                    CompanyName = x.x.CompanyName,
                    ContactInfoResponses = x.y.Select(z=> new ContactInfoResponse
                    {
                        Id = z.Id,
                        ContactId =  z.ContactId,
                        InfoType = z.InfoType,
                        InfoContent = z.InfoContent,
                        CreateBy = z.CreateBy,
                        CreatedDate = z.CreatedDate,
                        CreatedTime = z.CreatedTime,
                        ModifiedDate = z.ModifiedDate,
                        ModifiedTime = z.ModifiedTime,
                        ModifyBy = z.ModifyBy
                    })
                }).FirstOrDefault();
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
            var contact = _dbContext.Contacts.Where(x => x.Id == contactId).FirstOrDefault();

            if (contact != null)
            {
                contact.DeleteBy = _context.UserId;
                contact.DeletedDate = SystemDateTime.NowDate();
                contact.DeletedTime = SystemDateTime.NowTime();

                _dbContext.SaveChanges();
            }
        }

        public void UpdateContact(ContactUpdateRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.FirstName) || string.IsNullOrWhiteSpace(req.LastName))
            {
                _context.AddReturnMessage("Ad ve soyad girilmelidir");
            }

            if (string.IsNullOrWhiteSpace(req.CompanyName))
            {
                _context.AddReturnMessage("Firma ismi girilmelidir");
            }


            var contact = _dbContext.Contacts.FirstOrDefault(x => x.Id == req.Id);

            if (contact != null)
            {
                var contactInfo = _dbContext.ContactInfos.FirstOrDefault(x => x.ContactId == contact.Id);

               
            
                _dbContext.SaveChanges();
            }
        }
    }
}
