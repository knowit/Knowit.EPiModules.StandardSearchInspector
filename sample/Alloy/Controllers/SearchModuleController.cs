using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Alloy.Business;
using Alloy.Models.ViewModels;
using EPiServer.Core;
using EPiServer.Framework.Web;
using EPiServer.Globalization;
using EPiServer.Search;
using EPiServer.Web;
using EPiServer.Web.Routing;
using Newtonsoft.Json;

namespace Alloy.Controllers
{
    public class SearchModuleController : Controller
    {
        private const int HitsPerPage = 10;
        private readonly SearchServiceModule _searchService;
        private readonly ContentSearchHandler _contentSearchHandler;
        private readonly UrlResolver _urlResolver;
        private readonly TemplateResolver _templateResolver;

        public SearchModuleController(
            SearchServiceModule searchService,
            ContentSearchHandler contentSearchHandler,
            TemplateResolver templateResolver,
            UrlResolver urlResolver)
        {
            _searchService = searchService;
            _contentSearchHandler = contentSearchHandler;
            _templateResolver = templateResolver;
            _urlResolver = urlResolver;
        }

        // GET: SearchModule
        public ActionResult Index()
        {
            var model = new SearchModuleViewModel();

            model.Title = "Search Inspector";

            return View("/Views/SearchModule/Index.cshtml", model);
        }

        public ActionResult Search(string query = null, int page = 1)
        {
            var model = new SearchModuleViewModel();

            if (!string.IsNullOrEmpty(query) && _searchService.IsActive)
            {

                var hits = _searchService.Search(query.Trim(),
                    new[] { SiteDefinition.Current.StartPage, SiteDefinition.Current.GlobalAssetsRoot, SiteDefinition.Current.SiteAssetsRoot },
                    ControllerContext.HttpContext, 
                    ContentLanguage.PreferredCulture.Name, 
                    HitsPerPage,
                    page);

                model.Hits = hits.IndexResponseItems.SelectMany(CreateHitModel);
                model.NumberOfHits = hits.TotalHits;
                model.Page = page;
                model.Pages = (int)Math.Ceiling((double)model.NumberOfHits / HitsPerPage);
            }

            return PartialView("/Views/SearchModule/_Result.cshtml", model);
        }


        public ActionResult SearchReindex() {


            return View("/Views/SearchModule/_Reindex.cshtml");
        }

        private IEnumerable<string> CreateHitModel(IndexResponseItem responseItem)
        {
            var content = _contentSearchHandler.GetContent<IContent>(responseItem);
            if (content != null && HasTemplate(content) && IsPublished(content as IVersionable))
            {
                yield return JsonConvert.SerializeObject(responseItem, Formatting.Indented); //JavaScriptSerializer().Serialize(responseItem); //CreatePageHit(content);
            }
        }

        private bool HasTemplate(IContent content)
        {
            return _templateResolver.HasTemplate(content, TemplateTypeCategories.Page);
        }

        private bool IsPublished(IVersionable content)
        {
            if (content == null)
                return true;
            return content.Status.HasFlag(VersionStatus.Published);
        }
    }
}