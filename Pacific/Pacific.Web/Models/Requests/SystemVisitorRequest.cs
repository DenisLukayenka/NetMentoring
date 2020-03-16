namespace Pacific.Web.Models.Requests
{
    public class SystemVisitorRequest: IRequest
    {
        public string FolderPath { get; set; }
        public bool ShowFilteredFiles { get; set; }
        public bool ShowFilteredDirectories { get; set; }
    }
}