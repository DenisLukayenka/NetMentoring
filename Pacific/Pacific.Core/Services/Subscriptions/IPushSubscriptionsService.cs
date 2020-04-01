using Lib.Net.Http.WebPush;
using System.Collections.Generic;

namespace Pacific.Core.Services.Subscriptions
{
	public interface IPushSubscriptionsService
	{
		IEnumerable<PushSubscription> GetAll();

		void Insert(PushSubscription subscription);

		void Delete(string endpoint);
	}
}
