using System.Collections.Generic;

namespace Alloy.Models.ViewModels
{
    public class SearchModuleViewModel
    {
        public string Title { get; set; }
        public int NumberOfHits { get; set; }
        public int Pages { get; set; }
        public int Page { get; set; }
        public IEnumerable<string> Hits { get; set; }

        public class SearchHitItem
        {
            public string Title { get; set; }
            public string Url { get; set; }
            public string Excerpt { get; set; }
        }
    }
}