using ListPagerRazorLibrary.Models;
using Microsoft.AspNetCore.Mvc;
//https://github.com/ianbusko/reusable-components-library
namespace ListPagerRazorLibrary.ViewComponents
{
    public class ListPagerPageSize : ViewComponent
    {
        public IViewComponentResult Invoke(ListPagerModel model)
        {
            return View("../ListPagerPageSize", model);
        }
    }
}
