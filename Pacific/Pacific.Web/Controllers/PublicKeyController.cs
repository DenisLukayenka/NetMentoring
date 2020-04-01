using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Pacific.Core.Models.Subscriptions;

namespace Pacific.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicKeyController : ControllerBase
    {
        private readonly PushNotificationsOptions _options;

        public PublicKeyController(IOptions<PushNotificationsOptions> options)
        {
            this._options = options?.Value;
        }

        public ContentResult Get()
        {
            return Content(this._options.PublicKey, "text/plain");
        }
    }
}