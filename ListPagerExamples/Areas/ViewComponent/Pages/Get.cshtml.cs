using ListPagerExamples.Models;
using System.Threading.Tasks;

//also see - https://marketplace.visualstudio.com/items?itemName=DavidEbbo.RazorGenerator


namespace ListPagerExamples.Areas.ViewComponent.Pages
{
    public class VCGetModel : BloggerPageModel
    {
        public VCGetModel(AppDbContext db) : base(db)
        {
        }

        public void OnGet(int PageNumber = 1, int PageSize = 5, string search = null)
        {
            Parameters.Pager.PageNumber = PageNumber;
            Parameters.Pager.PageSize = PageSize;
            Parameters.SearchTerm = search;
            Parameters.Pager.MaxPageLinks = 5;
            GetPage();   //see BloggerPageModel
        }
    }
}
