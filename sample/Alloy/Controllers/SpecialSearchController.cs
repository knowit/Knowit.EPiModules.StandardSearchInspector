using Alloy.Models.Pages;
using System.Web.Mvc;
using EPiServer.Core;
using Alloy.Business;
using EPiServer.Web;
using EPiServer.Web.Routing;
using Alloy.Models.ViewModels;
using EPiServer.DataAbstraction;
using EPiServer;
using EPiServer.ServiceLocation;
using EPiServer.Filters;

namespace Alloy.Controllers
{
    public class SpecialSearchController : PageControllerBase<SpecialSearch>
    {
        private const int MaxResults = 40;
        private readonly SearchService _searchService;
        private readonly ContentSearchHandler _contentSearchHandler;
        private readonly UrlResolver _urlResolver;
        private readonly TemplateResolver _templateResolver;

        public SpecialSearchController(
            SearchService searchService,
            ContentSearchHandler contentSearchHandler,
            TemplateResolver templateResolver,
            UrlResolver urlResolver)
        {
            _searchService = searchService;
            _contentSearchHandler = contentSearchHandler;
            _templateResolver = templateResolver;
            _urlResolver = urlResolver;
        }

        // GET: SpecialSearch
        public ViewResult Index(SpecialSearch currentPage, string q)
        {
            if (string.IsNullOrEmpty(q)) {
                return View(new SpecialSearchViewModel(currentPage));
            }

            var model = new SpecialSearchViewModel(currentPage)
            {
                Hits = SearchPages(q)
            };

            return View(model);
        }

        public ActionResult Search(SpecialSearch currentPage, string q)
        {
            var model = new SpecialSearchViewModel(currentPage)
            {
                Hits = Autocomplete(q)
            };

            return PartialView("Autocomplete", model);
        }

        private PageDataCollection SearchPages(string query) {

            PropertyCriteriaCollection criterias = new PropertyCriteriaCollection();

            var Locate = ServiceLocator.Current.GetInstance<ServiceLocationHelper>();

            Locate.ContentLoader();

            PropertyCriteria criteria = new PropertyCriteria();
            criteria.Condition = CompareCondition.Equal;
            criteria.Name = "PageTypeID";
            criteria.Type = PropertyDataType.PageType;
            criteria.Value = Locate.ContentTypeRepository().Load("StandardPage").ID.ToString();
            criteria.Required = true;

            PropertyCriteria criteria2 = new PropertyCriteria();
            criteria2.Name = "PageName";
            criteria2.Condition = CompareCondition.Contained;
            criteria2.Type = PropertyDataType.String;
            criteria2.Value = query;

            criterias.Add(criteria);

            if (!string.IsNullOrEmpty(query)) { 
                criterias.Add(criteria2);
            }

            PageDataCollection _newsPageItems = Locate.PageCriteriaQueryService().FindPagesWithCriteria(PageReference.StartPage, criterias);

            FilterForVisitor.Filter(_newsPageItems);
            new FilterSort(FilterSortOrder.PublishedDescending).Filter(_newsPageItems);
            
            //new FilterCompareTo("MainBody", "alloy").Filter(_newsPageItems);

            return _newsPageItems;
        }

        private PageDataCollection Autocomplete(string query)
        {

            PropertyCriteriaCollection criterias = new PropertyCriteriaCollection();

            var Locate = ServiceLocator.Current.GetInstance<ServiceLocationHelper>();

            Locate.ContentLoader();

            PropertyCriteria criteria2 = new PropertyCriteria();
            criteria2.Name = "PageName";
            criteria2.Condition = CompareCondition.StartsWith;
            criteria2.Type = PropertyDataType.String;
            criteria2.Value = query;

            if (!string.IsNullOrEmpty(query))
            {
                criterias.Add(criteria2);
            }

            PageDataCollection _newsPageItems = Locate.PageCriteriaQueryService().FindPagesWithCriteria(PageReference.StartPage, criterias);

            FilterForVisitor.Filter(_newsPageItems);
            new FilterSort(FilterSortOrder.PublishedDescending).Filter(_newsPageItems);

            //new FilterCompareTo("MainBody", "alloy").Filter(_newsPageItems);

            return _newsPageItems;
        }
        /*
        public void SearchFilter(object sender, FilterEventArgs e)
        {
            PageDataCollection pages = e.Pages;
            for (int pageIndex = pages.Count - 1; pageIndex >= 0; pageIndex--)
            {
                PageData pageData = pages[pageIndex];

                if (pageData.PageTypeName.Equals("discussion") &&
                    pageData.Property.Contains("MainBody") &&
                    pageData.Property["MainBody"].ToString().IndexOf("agree") >= 0)
                {
                    // keep page
                }
                else
                {
                    pages.RemoveAt(pageIndex);
                }
            }
        }*/
    }
}