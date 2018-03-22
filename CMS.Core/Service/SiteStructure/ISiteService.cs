using System.Collections;
using CMS.Core.Domain;

namespace CMS.Core.Service.SiteStructure
{
    /// <summary>
    /// Provides functionality to manage site instances.
    /// </summary>
    public interface ISiteService
    {
        /// <summary>
        /// Get a single site by site id.
        /// </summary>
        /// <param name="siteId"></param>
        /// <returns></returns>
        Site GetSiteById(int siteId);

        /// <summary>
        /// Get a single site by root url.
        /// </summary>
        /// <param name="siteUrl"></param>
        /// <returns></returns>
        Site GetSiteBySiteUrl(string siteUrl);

        /// <summary>
        /// Get a single site alias by id.
        /// </summary>
        /// <param name="siteAliasId"></param>
        /// <returns></returns>
        SiteAlias GetSiteAliasById(int siteAliasId);

        /// <summary>
        /// Get a single site alias by root url.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        SiteAlias GetSiteAliasByUrl(string url);

        /// <summary>
        /// Get all site aliases by a given site.
        /// </summary>
        /// <param name="site"></param>
        /// <returns></returns>
        IList GetSiteAliasesBySite(Site site);

        /// <summary>
        /// Get all sites.
        /// </summary>
        /// <returns></returns>
        IList GetAllSites();

        /// <summary>
        /// Save a site.
        /// </summary>
        /// <param name="site"></param>
        void SaveSite(Site site);

        /// <summary>
        /// Delete a site.
        /// </summary>
        /// <param name="site"></param>
        void DeleteSite(Site site);

        /// <summary>
        /// Save a site alias.
        /// </summary>
        /// <param name="siteAlias"></param>
        void SaveSiteAlias(SiteAlias siteAlias);

        /// <summary>
        /// Delete a site alias.
        /// </summary>
        /// <param name="siteAlias"></param>
        void DeleteSiteAlias(SiteAlias siteAlias);
    }
}