using Rise.Contact.API.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Rise.Contact.API.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private List<Roles> _roles;

        public AuthorizeAttribute(params Roles[] roles)
        {
            this._roles = roles.ToList();
        }

        public AuthorizeAttribute()
        {
            this._roles = new List<Roles>();
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userId = (Guid)context.HttpContext.Items["UserId"];
            var role = (Roles)context.HttpContext.Items["RoleId"];
           
            if (userId == Guid.Empty || (!_roles.Contains(role) && this._roles.Count > 0))
            {
                if (!((Guid)context.HttpContext.Items["id"] != Guid.Empty &&_roles.Contains(Roles.Manager)))
                {
                    context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                }
            }
        }
    }
}
