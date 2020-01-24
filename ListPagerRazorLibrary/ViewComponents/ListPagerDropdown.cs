using ListPagerRazorLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ListPagerRazorLibrary.ViewComponents
{
    public class ListPagerDropdown : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(ListPagerModel model)
        {
            ListPagerDropdownModel dpm = new ListPagerDropdownModel(model);
            await Task.CompletedTask;
            return View(AppConstants.VIEW_PATH_LISTPAGER_DROPDOWN, dpm);
        }
    }
}
