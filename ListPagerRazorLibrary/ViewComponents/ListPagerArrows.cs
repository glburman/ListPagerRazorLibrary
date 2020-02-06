using ListPagerRazorLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ListPagerRazorLibrary.ViewComponents
{
    public class ListPagerArrows : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(ListPagerModel state, int direction, bool single)
        {
            var pam = await Task.Run(() => new ListPagerArrowsModel(state, direction, single));
            return View(AppConstants.VIEW_PATH_LISTPAGER_ARROWS, pam);
        }
    }
}
