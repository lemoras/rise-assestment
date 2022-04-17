using Rise.Contact.API.Interfaces;

namespace Rise.Contact.API.Utils
{
    public static class QueryableExtension
    {
        public static IQueryable<TModel> SoftDelCondition<TModel>(this IQueryable<TModel> obj) where TModel : IDeleteAuditableEntity
        {
            return obj.Where(x=> x.DeleteBy == null && x.DeletedDate == null && x.DeletedTime == null);
        }
    }
}
