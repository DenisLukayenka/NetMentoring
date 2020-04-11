using System;

namespace Pacific.SiteMirror.Helpers
{
	public static class StringExtensions
	{
		public static Uri ToUri(this string source)
		{
			return new Uri(source);
		}
	}
}
