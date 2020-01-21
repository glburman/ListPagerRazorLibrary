using ListPagerRazorLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace ListPagerRazorLibrary.ViewComponents
{
    public class ListPager : ViewComponent
    {
        public IViewComponentResult Invoke(ListPagerModel model)
        {
            return View("../ListPager", model);
        }
    }
}
