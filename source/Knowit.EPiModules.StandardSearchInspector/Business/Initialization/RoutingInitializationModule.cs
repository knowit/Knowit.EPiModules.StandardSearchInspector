using System.Web.Mvc;
using System.Web.Routing;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;

namespace Knowit.EPiModules.StandardSearchInspector.Business.Initialization
{
    public class RoutingInitializationModule
    {
        [InitializableModule]
        [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
        public class RoutesInitializationModule : IInitializableModule
        {
            public void Initialize(InitializationEngine context)
            {
                RouteTable.Routes.MapRoute("SearchModuleRoute", "SearchModuleMethod/{action}", new { controller = "SearchModule", action = "Index" });
                RouteTable.Routes.MapRoute("SearchModuleSearchResultRoute", "SearchModuleSearchHits/{action}", new { controller = "SearchModule", action = "Search" });
                RouteTable.Routes.MapRoute("SearchModuleSearchReindexRoute", "SearchModuleReindex/{action}", new { controller = "SearchModule", action = "SearchReindex" });
            }

            public void Uninitialize(InitializationEngine context)
            {

            }
        }
    }
}