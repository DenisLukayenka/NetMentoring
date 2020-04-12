using System;
using System.Text;
using System.Web;

namespace Pacific.SiteMirror.Helpers
{
	public static class StringExtensions
	{
        public const string w3wHostSegment = "www.";

		public static Uri ToUri(this string link, Uri uri)
		{
            var decodedLink = HttpUtility.UrlDecode(link, Encoding.UTF8);

            if (Uri.TryCreate(decodedLink, UriKind.Absolute, out var absoluteUri))
            {
                return absoluteUri;
            }
            else if (Uri.TryCreate(decodedLink, UriKind.Relative, out var relativeUri))
            {
                var resultUri = new Uri(uri, relativeUri);

                return resultUri;
            }

            return null;
        }

        public static string RemoveW3WFromHost(this string host)
        {
            if (host.StartsWith(w3wHostSegment))
            {
                return host.Substring(w3wHostSegment.Length);
            }

            return host;
        }
	}
}
