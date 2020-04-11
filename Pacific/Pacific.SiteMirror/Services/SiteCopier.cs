using Pacific.SiteMirror.Helpers;
using Pacific.SiteMirror.Models;
using Pacific.SiteMirror.Services.FileManager;
using Pacific.SiteMirror.Services.HttpServices;
using Pacific.SiteMirror.Services.PageSearcher;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Pacific.SiteMirror.Services
{
    public class SiteCopier : ISiteCopier
    {
        private Uri _siteUri;
        private string _folderPath;
        private int _depth;
        private DomainRestriction _domainRestriction;
        private IDictionary<string, int> linksDepth;

        private IHttpClientService _clientService;
        private IFileManager _fileManager;
        private IPageSearcher _pageSearcher;

        public SiteCopier(IHttpClientService clientService, IFileManager fileManager, IPageSearcher pageSearcher)
        {
            this._clientService = clientService;
            this._fileManager = fileManager;
            this._pageSearcher = pageSearcher;
        }

        public async Task CopySiteAsync(string url, string targetPath, DomainRestriction domainRestriction = DomainRestriction.CurrentDomain, int depth = 0)
        {
            this.InitializeState(url, targetPath, domainRestriction, depth);

            await this.CopySiteRecursiveAsync(this._siteUri, _folderPath, 0);
        }

        protected virtual bool ValidateUrlState(Uri url, int depth)
        {
            if(depth > this._depth)
            {
                return false;
            }

            if (this.linksDepth.TryGetValue(url.AbsoluteUri, out var linkDepth))
            {
                return linkDepth > depth;
            }

            return true;
        }

        private async Task CopySiteRecursiveAsync(Uri url, string path, int currentDepth)
        {
            if(!this.ValidateUrlState(url, currentDepth))
            {
                return;
            }

            var buffer = await this._clientService.GetResourceDataAsync(url);
            await this._fileManager.SaveToFileAsync(buffer, Path.Combine(path, url.GetFileName()));

            this.linksDepth.Add(url.AbsoluteUri, currentDepth);
            var pageLinks = await this._pageSearcher.SearchLinksAsync(Encoding.UTF8.GetString(buffer));

            Uri linkUri;
            string linkFolderPath;
            foreach(var link in pageLinks)
            {
                linkUri = link.ToUri();
                linkFolderPath = this.GeneratePath(url, linkUri, path);

                await this.CopySiteRecursiveAsync(linkUri, linkFolderPath, currentDepth + 1);
            }
        }

        private void InitializeState(string url, string targetPath, DomainRestriction domainRestriction, int depth)
        {
            this._siteUri = new Uri(url);
            this._domainRestriction = domainRestriction;
            this._depth = depth;

            this._folderPath = Path.Combine(targetPath, this._siteUri.Host);
            Directory.CreateDirectory(this._folderPath);

            this.linksDepth = new Dictionary<string, int>();
        }

        private string GeneratePath(Uri sourceUri, Uri targetUri, string sourcePath)
        {
            var relativeUri = targetUri.MakeRelativeUri(sourceUri).AbsoluteUri;

            return Path.Combine(sourcePath, relativeUri);
        }
    }
}
