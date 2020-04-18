using Pacific.SiteMirror.Helpers;
using Pacific.SiteMirror.Models;
using Pacific.SiteMirror.Services.FileManager;
using Pacific.SiteMirror.Services.HttpServices;
using Pacific.SiteMirror.Services.PageSearcher;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task CopySiteAsync(string link, string targetPath, DomainRestriction domainRestriction = DomainRestriction.CurrentDomain, int depth = 0)
        {
            this.InitializeState(link, targetPath, domainRestriction, depth);
            this.linksDepth.Add(this._siteUri.GetUrlIdentifier(), 0);

            await this.CopySiteRecursiveAsync(this._siteUri, 0);
        }

        protected virtual bool ValidateUrlState(Uri url, int depth)
        {
            if (depth >= this._depth)
            {
                return false;
            }

            if (this.linksDepth.TryGetValue(url.GetUrlIdentifier(), out var linkDepth))
            {
                return linkDepth >= depth;
            }

            return true;
        }

        private async Task CopySiteRecursiveAsync(Uri url, int currentDepth)
        {
            var buffer = await this._clientService.GetResourceDataAsync(url);
            if(buffer.Length == 0)
            {
                return;
            }

            await this._fileManager.SaveToFileAsync(buffer, url.GenerateAbsoluteFolderPath(this._folderPath), url.GetFileName());

            if (this.ValidateUrlState(url, currentDepth))
            {
                var pageLinks = await this._pageSearcher.SearchLinksAsync(Encoding.UTF8.GetString(buffer));

                var pageUrls = this.ConvertPageLinksToUrls(pageLinks, url).Where(u => !this.linksDepth.ContainsKey(u.GetUrlIdentifier())).ToArray();
                this.AddPageUrlsInfo(pageUrls, currentDepth + 1);

                foreach (var pageUrl in pageUrls)
                {
                    if(DomainChecker.CheckDomainLevel(this._siteUri, pageUrl, this._domainRestriction))
                    {
                        await this.CopySiteRecursiveAsync(pageUrl, currentDepth + 1);
                    }
                }
            }
        }

        private IEnumerable<Uri> ConvertPageLinksToUrls(IEnumerable<string> links, Uri uri)
        {
            Uri pageUrl;
            foreach(var link in links)
            {
                pageUrl = link.ToUri(uri);

                if(pageUrl != null)
                {
                    yield return pageUrl;
                }
            }
        }

        private void AddPageUrlsInfo(IEnumerable<Uri> newUrls, int depth)
        {
            foreach(var newUri in newUrls)
            {
                if (!this.linksDepth.ContainsKey(newUri.GetUrlIdentifier()))
                {
                    this.linksDepth.Add(newUri.GetUrlIdentifier(), depth);
                }
            }
        }

        private void InitializeState(string url, string targetPath, DomainRestriction domainRestriction, int depth)
        {
            this._siteUri = url.ToUri(null);
            this._domainRestriction = domainRestriction;
            this._depth = depth;
            this._folderPath = targetPath;
            this.linksDepth = new Dictionary<string, int>();
        }
    }
}
