using Microsoft.AspNetCore.Mvc;
using Rise.Contact.API.Utils;

namespace Rise.Contact.API.Controllers
{
    public class BaseController<TController, TService> : ControllerBase
    {
        private readonly ILogger<TController> _logger;
        protected TService _service;
        protected IApplicationContext _context;

        public BaseController(ILogger<TController> logger,
            TService service, IApplicationContext context)
        {
            _logger = logger;
            _service = service;
            _context = context;
        }

        public ActionResult Result()
        {
            return _context.AnyMessage() ? base.BadRequest(_context.InfoMessages) : base.Ok();
        }

        public ActionResult Result(object obj, string checkObjectMessage = null)
        {
            if (obj == null)
            {
                if (checkObjectMessage != null)
                {
                    _context.AddReturnMessage(checkObjectMessage);
                }

                return BadRequest(_context.InfoMessages);// buradaki kontrol any message olsa dahi bir lsite bos donebilir o yuzden obje null olabilir.
            }

            return _context.AnyMessage() ? base.BadRequest(_context.InfoMessages) : base.Ok(obj);
        }
    }
}
