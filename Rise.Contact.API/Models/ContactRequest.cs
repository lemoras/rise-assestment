using System.ComponentModel.DataAnnotations;

namespace Rise.Contact.API.Models
{
    public class ContactRequest
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string CompanyName { get; set; }

        // [Required]
        // public Guid CompanyId { get; set; }
    }
}
