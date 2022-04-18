namespace Rise.Contact.API.Utils
{
    public interface IApplicationContext
    {
        Guid UserId { get; }

        int RoleId { get; }
        object InfoMessages { get; }

        void AddReturnMessage(string message);
    }

    public class ApplicationContext : IApplicationContext
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly List<string> _infoMessages;
        
        public ApplicationContext(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
            _infoMessages = new List<string>();
        }

        public Guid UserId
        {
            get { return !string.IsNullOrWhiteSpace(_httpContext.HttpContext.Items["UserId"]?.ToString()) ? Guid.Parse(_httpContext.HttpContext.Items["UserId"].ToString()) : Guid.Empty; }
        }

        public int RoleId
        {
            get { return !string.IsNullOrWhiteSpace(_httpContext.HttpContext.Items["RoleId"]?.ToString()) ? int.Parse(_httpContext.HttpContext.Items["RoleId"].ToString()) : 0; }
        }

        public object InfoMessages => _infoMessages.Count > 0 ? new { message = string.Join("<br>", _infoMessages) } : null;

        public void AddReturnMessage(string message)
        {
            _infoMessages.Add(message);
        }
    }

    public static class ApplicationContextExtension
    {
        public static bool AnyMessage(this IApplicationContext context)
        {
            return context.InfoMessages != null;
        }
    }
}
