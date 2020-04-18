using System.Threading.Tasks;

namespace Pacific.SiteMirror.Services.FileManager
{
	public interface IFileManager
	{
		Task SaveToFileAsync(byte[] data, string path, string fileName);
	}
}
