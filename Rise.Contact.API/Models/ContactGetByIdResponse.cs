namespace Rise.Contact.API.Models
{
    public class ContactGetByIdResponse
    {
       public Guid Id { get; set;}
       public string FirstName { get; set; }
       public string LastName { get; set; }
       public string CompanyName { get; set; }
       public IEnumerable<ContactInfoResponse> ContactInfoResponses { get; set; }
    }
}
