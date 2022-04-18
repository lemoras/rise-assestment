using System;

namespace Rise.Contact.API.Interfaces
{
    public interface ICreateAuditableEntity : IEntity
    {
        Guid CreateBy { get; set; }
        DateTime CreatedDate { get; set;}
        TimeSpan CreatedTime { get; set;}
    }
}