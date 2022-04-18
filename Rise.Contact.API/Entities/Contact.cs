using Rise.Contact.API.Interfaces;

namespace Rise.Contact.API.Entities
{
    public class Contact : Entity, IDeleteAuditableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        // public Guid CompandyId { get; set; }
        public string CompanyName { get; set; }

        public Guid CreateBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public TimeSpan CreatedTime  { get; set; }

        public Guid? ModifyBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public TimeSpan? ModifiedTime  { get; set; }

        public Guid? DeleteBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public TimeSpan? DeletedTime  { get; set; }
    }
}
