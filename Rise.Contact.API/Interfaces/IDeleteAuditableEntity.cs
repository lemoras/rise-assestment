namespace Rise.Contact.API.Interfaces
{
    public interface IDeleteAuditableEntity
    {
        Guid? DeleteBy { get; set; }
        DateTime? DeletedDate { get; set;}
        TimeSpan? DeletedTime { get; set;}
    }
}