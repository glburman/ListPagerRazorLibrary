using ListPagerRazorLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace ListPagerRazorLibrary.ViewComponents
{
    public class ListPagerLinks : ViewComponent
    {
        public IViewComponentResult Invoke(ListPagerModel model)
        {
            return View(AppConstants.VIEW_PATH_LISTPAGER_LINKS, model);
        }
    }
}
