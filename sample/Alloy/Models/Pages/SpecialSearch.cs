using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;

namespace Alloy.Models.Pages
{
    [ContentType(DisplayName = "SpecialSearch", GUID = "9dab72e9-2ecd-4ba0-9bd5-c0f34c5cb0cc", Description = "")]
    public class SpecialSearch : SitePageData
    {
        /*
                [CultureSpecific]
                [Display(
                    Name = "Main body",
                    Description = "The main body will be shown in the main content area of the page, using the XHTML-editor you can insert for example text, images and tables.",
                    GroupName = SystemTabNames.Content,
                    Order = 1)]
                public virtual XhtmlString MainBody { get; set; }
         */
    }
}