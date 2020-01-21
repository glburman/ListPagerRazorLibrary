using ListPagerRazorLibrary.Models;
using Microsoft.AspNetCore.Mvc;
//https://github.com/ianbusko/reusable-components-library
namespace ListPagerRazorLibrary.ViewComponents
{
    public class ListPagerArrows : ViewComponent
    {
        public IViewComponentResult Invoke(ListPagerModel state, int direction)
        {
            ListPagerArrowModel pam = new ListPagerArrowModel()
            {
                IsDisabled = direction < 0 && state.PageNumber <= 1 || direction > 0 && state.PageNumber >= state.PageCount
            };
            if (direction < 0)
            {
                pam.ClickFirst = "pager.goToPage(1)";
                pam.ClickSecond = string.Format("pager.goToPage({0})", state.PageNumber - 1);
                pam.FirstArrow = "<<";
                pam.SecondArrow = "<";
                pam.FirstWord = "First";
                pam.SecondWord = "Previous";
            }
            else
            {
                pam.ClickFirst = string.Format("pager.goToPage({0})", state.PageNumber + 1);
                pam.ClickSecond = string.Format("pager.goToPage({0})", state.PageCount);
                pam.FirstArrow = ">";
                pam.SecondArrow = ">>";
                pam.FirstWord = "Next";
                pam.SecondWord = "Last";
            }
            return View("../ListPagerArrows", pam);
        }
    }
}
