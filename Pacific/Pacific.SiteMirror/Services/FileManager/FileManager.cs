using System.IO;
using System.Threading.Tasks;

namespace Pacific.SiteMirror.Services.FileManager
{
	public class FileManager : IFileManager
	{
		public async Task SaveToFileAsync(byte[] data, string path, string fileName)
		{
			this.InitializePath(path);
			
			using(var writer = new FileStream(Path.Combine(path, fileName), FileMode.Create))
			{
				await writer.WriteAsync(data, 0, data.Length);
			}
		}

		protected virtual void InitializePath(string path)
		{
			Directory.CreateDirectory(path);
		}
	}
}
