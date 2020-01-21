using ListPagerRazorLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace ListPagerRazorLibrary.ViewComponents
{
    public class ListPagerLinks : ViewComponent
    {
        public IViewComponentResult Invoke(ListPagerModel model)
        {
            return View("../ListPagerLinks", model);
        }
    }
}
