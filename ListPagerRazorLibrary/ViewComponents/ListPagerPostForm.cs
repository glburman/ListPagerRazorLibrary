using ListPagerRazorLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace ListPagerRazorLibrary.ViewComponents
{
    public class ListPagerPostForm : ViewComponent
    {
        public IViewComponentResult Invoke(ListPagerModel model)
        {
            return View(AppConstants.VIEW_PATH_LISTPAGER_POSTFORM, model);
        }
    }
}
