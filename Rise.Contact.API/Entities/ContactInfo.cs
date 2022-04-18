using Rise.Contact.API.Interfaces;
using Rise.Contact.API.Utils;

namespace Rise.Contact.API.Entities
{
    public class ContactInfo : Entity, ICreateAuditableEntity
    {
        public Guid ContactId { get; set; }
        public InfoType InfoType { get; set; }
        public string InfoContent { get; set; }

        public Guid CreateBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public TimeSpan CreatedTime  { get; set; }

        public Guid? ModifyBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public TimeSpan? ModifiedTime  { get; set; }
    }
}
