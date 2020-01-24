using ListPagerRazorLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ListPagerRazorLibrary.ViewComponents
{
    public class ListPagerArrows : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(ListPagerModel state, int direction)
        {
            var pam = await Task.Run(() => GetModel(state, direction));
            return View(AppConstants.VIEW_PATH_LISTPAGER_ARROWS, pam);
        }

        private ListPagerArrowModel GetModel(ListPagerModel state, int direction)
        {
            return new ListPagerArrowModel(state, direction);
        }
    }
}
