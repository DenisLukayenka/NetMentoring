using Lib.Net.Http.WebPush;
using Lib.Net.Http.WebPush.Authentication;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Pacific.Core.Models.Subscriptions;
using Pacific.Core.Services;
using Pacific.Core.Services.Subscriptions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pacific.Core.Producers
{
	public class FileSystemVisitorNotificationProducer : BackgroundService
	{
        private readonly IPushSubscriptionsService _pushSubscriptionsService;
        private readonly PushServiceClient _pushClient;
        private bool isFileFound = false;

        public FileSystemVisitorNotificationProducer(IOptions<PushNotificationsOptions> options, IPushSubscriptionsService pushSubscriptionsService, PushServiceClient pushClient)
        {
            this._pushSubscriptionsService = pushSubscriptionsService;

            _pushClient = pushClient;
            _pushClient.DefaultAuthentication = new VapidAuthentication(options.Value.PublicKey, options.Value.PrivateKey)
            {
                Subject = "https://locahost:5001"
            };
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(2000);
                SendNotifications(0, stoppingToken);
            }
		}

        private void SendNotifications(int temperatureC, CancellationToken stoppingToken)
        {
            PushMessage notification = new AngularPushNotification
            {
                Title = "New Weather Forecast",
                Body = $"Temp. (C): {temperatureC} | Temp. (F): {32 + (int)(temperatureC / 0.5556)}",
                Icon = "assets/icons/icon-96x96.png"
            }.ToPushMessage();

            foreach (PushSubscription subscription in _pushSubscriptionsService.GetAll())
            {
                // Fire-and-forget 
                _pushClient.RequestPushMessageDeliveryAsync(subscription, notification, stoppingToken);
            }
        }

        public static void OnFileFound(object sender, EventArgs args)
        {

        }
    }
}
