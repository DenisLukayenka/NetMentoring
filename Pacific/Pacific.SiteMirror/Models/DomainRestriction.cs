using Pacific.SiteMirror.Helpers;
using System;

namespace Pacific.SiteMirror.Models
{
    public enum DomainRestriction
    {
        NotHigherCurrentDomain = 1,
        CurrentDomain = 2,
        NoRestriction = 4,
    }

    public static class DomainChecker
    {
        public static bool CheckDomainLevel(Uri sourceUrl, Uri targetUrl, DomainRestriction sourceRestriction)
        {
            switch (sourceRestriction)
            {
                case DomainRestriction.NoRestriction:
                    return true;
                case DomainRestriction.CurrentDomain:
                    return sourceUrl.Host.RemoveW3WFromHost().Contains(targetUrl.Host.RemoveW3WFromHost());
                case DomainRestriction.NotHigherCurrentDomain:
                    return targetUrl.Host.RemoveW3WFromHost().Contains(sourceUrl.Host.RemoveW3WFromHost());
                default:
                    return false;
            }
        }
    }
}
