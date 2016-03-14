using Alloy.Models.Pages;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alloy.Models.ViewModels
{
    public class SpecialSearchViewModel : PageViewModel<SpecialSearch>
    {
        public SpecialSearchViewModel(SpecialSearch currentPage) : base(currentPage)
        {
        }

        public PageDataCollection Hits { get; set; }
        public int NumberOfHits { get; set; }
    }
}