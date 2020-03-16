using System.IO;

namespace Pacific.Web.Models.Responses
{
    public class SystemVisitorResponse: IResponse
    {
        public FileSystemInfo[] files { get; set; }
    }
}