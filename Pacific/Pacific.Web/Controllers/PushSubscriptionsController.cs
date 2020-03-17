using Lib.Net.Http.WebPush;
using Microsoft.AspNetCore.Mvc;
using Pacific.Core.Services.Subscriptions;

namespace Pacific.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PushSubscriptionsController : ControllerBase
    {
        private readonly IPushSubscriptionsService _pushSubscriptionsService;

        public PushSubscriptionsController(IPushSubscriptionsService pushSubscriptionsService)
        {
            this._pushSubscriptionsService = pushSubscriptionsService;
        }

        [HttpPost]
        public void Post([FromBody] PushSubscription subscription)
        {
            this._pushSubscriptionsService.Insert(subscription);
        }

        [HttpDelete]
        public void Delete(string endpoint)
        {
            this._pushSubscriptionsService.Delete(endpoint);
        }
    }
}