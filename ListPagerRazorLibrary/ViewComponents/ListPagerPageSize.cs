using ListPagerRazorLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace ListPagerRazorLibrary.ViewComponents
{
    public class ListPagerPageSize : ViewComponent
    {
        public IViewComponentResult Invoke(ListPagerModel model)
        {
            return View(AppConstants.VIEW_PATH_LISTPAGER_PAGESIZE, model);
        }
    }
}
