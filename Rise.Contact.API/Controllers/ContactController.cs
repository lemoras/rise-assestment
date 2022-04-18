using Rise.Contact.API.Helpers;
using Rise.Contact.API.Utils;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Rise.Contact.API.Services;
using Rise.Contact.API.Models;

namespace Rise.Contact.API.Controllers
{
    [EnableCors]
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : BaseController<ContactController, IContactService>
    {
        public ContactController(ILogger<ContactController> logger,
            IContactService service, IApplicationContext context)
            : base (logger, service, context)
        {
        }

        [Authorize]
        [HttpGet("getbyid/{contactId}")]
        public ActionResult Get(Guid contactId)
        {
            var response = _service.GetContact(contactId);
            return Result(response, "Rehber bulunamadı");
        }

        [Authorize]
        [HttpPost("getall")]
        public ActionResult GetAll(ContactFilterRequest filter)
        {
            var response = _service.GetContacts(filter);
            return Result(response);
        }

        [Authorize]
        [HttpGet("getall")]
        public ActionResult GetAll()
        {
            var response = _service.GetContacts();
            return Result(response);
        }

        [Authorize]
        [HttpPost("create")]
        public ActionResult Create(ContactRequest req)
        {
            var response = _service.CreateContact(req);
            return Result(response);
        }

        [Authorize]
        [HttpPut("update")]
        public ActionResult Update(ContactUpdateRequest req)
        {
            _service.UpdateContact(req);
            return Result();
        }

        [Authorize(Roles.Admin, Roles.Manager)]
        [HttpDelete("remove/{contactId}")]
        public ActionResult Removecontacts(Guid contactId)
        {
            _service.RemoveContact(contactId);
            return Result();
        }
    }
}
