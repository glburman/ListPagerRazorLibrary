using ListPagerRazorLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace ListPagerRazorLibrary.ViewComponents
{
    public class ListPagerRecords : ViewComponent
    {
        public IViewComponentResult Invoke(ListPagerModel model)
        {
            return View(AppConstants.VIEW_PATH_LISTPAGER_RECORDS, model);
        }
    }
}
