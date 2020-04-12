using System;
using System.Threading.Tasks;

namespace Pacific.SiteMirror.Services.HttpServices
{
	public  interface IHttpClientService
	{
		Task<byte[]> GetResourceDataAsync(Uri url);
	}
}
