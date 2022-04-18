using Rise.Contact.API.Models;

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
}
