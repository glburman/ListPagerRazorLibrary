using ListPagerRazorLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace ListPagerRazorLibrary.ViewComponents
{
    public class ListPagerPageOf : ViewComponent
    {
        public IViewComponentResult Invoke(ListPagerModel model)
        {
            return View("../ListPagerPageOf", model);
        }
    }
}
