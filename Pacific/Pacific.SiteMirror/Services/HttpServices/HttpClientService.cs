using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pacific.SiteMirror.Services.HttpServices
{
	public class HttpClientService : IHttpClientService
	{
		public async Task<byte[]> GetResourceDataAsync(Uri url)
		{
			if(url is null)
			{
				throw new ArgumentNullException($"The unexpected null parameter: {nameof(url)} had been passed into method.");
			}

			if (!IsRequestUrlValid(url))
			{
				return new byte[] { };
			}

			var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
			requestMessage.Headers.Add("Host", url.Host ?? "");

			return await this.FetchData(requestMessage);
		}

		protected virtual bool IsRequestUrlValid(Uri url)
		{
			return url.Scheme == "http" || url.Scheme == "https";
		}

		protected async virtual Task<byte[]> FetchData(HttpRequestMessage requestMessage)
		{
			var httpClient = new HttpClient();

			try
			{
				var responceMessage = await httpClient.SendAsync(requestMessage);

				if (responceMessage.IsSuccessStatusCode)
				{
					return await responceMessage.Content.ReadAsByteArrayAsync();
				}
			}
			catch
			{

			}
			finally
			{
				httpClient.Dispose();
			}
			

			return new byte[] { };
		}
	}
}
