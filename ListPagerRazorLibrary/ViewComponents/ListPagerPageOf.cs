using ListPagerRazorLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace ListPagerRazorLibrary.ViewComponents
{
    public class ListPagerPageOf : ViewComponent
    {
        public IViewComponentResult Invoke(ListPagerModel model)
        {
            return View(AppConstants.VIEW_PATH_LISTPAGER_PAGEOF, model);
        }
    }
}
