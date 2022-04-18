namespace Rise.Contact.API.Models
{
    public class ContactFilterRequest
    {
        public Guid ContactId { get; set; }

        public string BeganDate { get; set; }
        public string EndDate { get; set; }
    }
}
