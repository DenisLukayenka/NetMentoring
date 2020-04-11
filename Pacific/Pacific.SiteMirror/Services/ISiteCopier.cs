using Pacific.SiteMirror.Models;
using System.Threading.Tasks;

namespace Pacific.SiteMirror.Services
{
    public interface ISiteCopier
    {
        Task CopySiteAsync(string url, string targetPath, DomainRestriction domainRestriction = DomainRestriction.CurrentDomain, int depth = 0);
    }
}
