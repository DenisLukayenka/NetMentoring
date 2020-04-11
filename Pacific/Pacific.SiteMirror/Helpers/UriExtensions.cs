using System;
using System.IO;

namespace Pacific.SiteMirror.Helpers
{
	public static class UriExtensions
	{
		public static string GetFileName(this Uri uri)
		{
			var absolutePath = uri.AbsolutePath;

			string fileName = Path.GetFileName(absolutePath);
			if (string.IsNullOrWhiteSpace(fileName))
			{
				return "index.html";
			}

			return fileName;
		}
	}
}
