using System.Collections.Generic;
using System.Web.Routing;
using EPiServer.Security;
using EPiServer.Shell.Navigation;

namespace Alloy.Business.ContentProviders
{
    [MenuProvider]
    public class MenuProvider : IMenuProvider
    {
        public IEnumerable<MenuItem> GetMenuItems()
        {
            var mainAdminMenu = new SectionMenuItem("Search Inspector", "/global/searchmodule");
            mainAdminMenu.IsAvailable = (request) => PrincipalInfo.HasAdminAccess;

            var firstMenuItem = new UrlMenuItem("Index", "/global/searchmodule/index", "/SearchModuleMethod/");
            firstMenuItem.IsAvailable = ((RequestContext request) => PrincipalInfo.HasEditAccess);
            firstMenuItem.SortIndex = 100;

            var secondMenuItem = new UrlMenuItem("Reindex", "/global/searchmodule/reindex", "/SearchModuleReindex/");
            firstMenuItem.IsAvailable = ((RequestContext request) => PrincipalInfo.HasEditAccess);
            firstMenuItem.SortIndex = 200;

            return new MenuItem[]
            {
                mainAdminMenu,
                secondMenuItem,
                firstMenuItem                
            };
        }
    }
}