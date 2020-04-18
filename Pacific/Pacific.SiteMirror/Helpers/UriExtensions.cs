using System;
using System.IO;
using System.Text;
using System.Web;

namespace Pacific.SiteMirror.Helpers
{
	public static class UriExtensions
	{
		public static string GetFileName(this Uri uri)
		{
			if (uri.TryGetFileNameExist(out string fileName))
			{
				return fileName;
			}

			return "index" + uri.Fragment + ".html";
		}

		public static bool TryGetFileNameExist(this Uri uri, out string fileName)
		{
			fileName = Path.GetFileName(HttpUtility.UrlDecode(uri.AbsolutePath, Encoding.UTF8));
			return !string.IsNullOrWhiteSpace(fileName) && Path.HasExtension(fileName);
		}

		public static string GenerateAbsoluteFolderPath(this Uri sourceUri, string sourcePath)
		{
			var host = sourceUri.Host;
			var path = HttpUtility.UrlDecode(sourceUri.AbsolutePath.TrimStart('/'), Encoding.UTF8);
			var directoryPath = Path.Combine(sourcePath, host, path);

			if (sourceUri.TryGetFileNameExist(out string fileName))
			{
				return directoryPath.Substring(0, directoryPath.Length - fileName.Length);
			}

			return directoryPath;
		}

		public static string GetUrlIdentifier(this Uri uri)
		{
			return uri.Host + uri.AbsolutePath + uri.Fragment;
		}
	}
}
