using ListPagerRazorLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ListPagerRazorLibrary.ViewComponents
{
    public class ListPager : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(ListPagerModel model)
        {
            await Task.CompletedTask;
            return View(AppConstants.VIEW_PATH_LISTPAGER, model);
        }
    }
}
