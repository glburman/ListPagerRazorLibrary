using ListPagerRazorLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace ListPagerRazorLibrary.ViewComponents
{
    public class ListPagerRecords : ViewComponent
    {
        public IViewComponentResult Invoke(ListPagerModel model)
        {
            return View("../ListPagerRecords", model);
        }
    }
}
