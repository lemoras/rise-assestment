using System.ComponentModel.DataAnnotations;

namespace Rise.Contact.API.Models
{
    public class ContactUpdateRequest
    {
        [Required]
        public Guid Id { get; set;}

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string CompanyName { get; set; }
    }
}
