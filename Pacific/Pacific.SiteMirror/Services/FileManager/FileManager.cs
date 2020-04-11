using System.IO;
using System.Threading.Tasks;
using System.Security.AccessControl;

namespace Pacific.SiteMirror.Services.FileManager
{
	public class FileManager : IFileManager
	{
		public async Task SaveToFileAsync(byte[] data, string path)
		{
			if (!this.ValidatePath(path))
			{
				this.InitializePath(path);
			}

			//var a = new FileInfo(path);
			//a.SetAccessControl(new FileSecurity(Path.GetFileName(path), AccessControlSections.All));
			
			using(var writer = new FileStream(path, FileMode.Create))
			{
				
				await writer.WriteAsync(data, 0, data.Length);
			}
		}

		protected virtual bool ValidatePath(string path)
		{
			var directory = Path.GetDirectoryName(path);
			return Directory.Exists(directory);
		}

		protected virtual void InitializePath(string path)
		{
			var directoryPath = Path.GetFullPath(path);

			Directory.CreateDirectory(directoryPath);
		}
	}
}
