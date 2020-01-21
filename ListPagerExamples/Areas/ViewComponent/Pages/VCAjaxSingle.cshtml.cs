using ListPagerExamples.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;

namespace ListPagerExamples.Areas.ViewComponent.Pages
{
    public class VCAjaxSingleModel : BloggerPageModel
    {
        
        public VCAjaxSingleModel(AppDbContext context) : base(context)
        {
            Parameters = new ParametersModel();
        }

        public IActionResult OnGet()
        {
            var pager = Parameters.Pager;
            pager.ShowRecordsOf = pager.IsFiltered;
            pager.ShowPageOf = !pager.IsFiltered;
            pager.ShowDropDown = true;
            Parameters.Pager = pager;
            Parameters.Pager.PageSize = 5;
            return null;
        }

        public PartialViewResult OnPostPartial(ParametersModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                Parameters = model;
                GetPage();
                return new PartialViewResult
                {
                    ViewName = "_CombinedPartial",
                    ViewData = new ViewDataDictionary<VCAjaxSingleModel>(ViewData, this)
                };
            }
            throw new ArgumentException("ParametersModel invalid or null");
        }
    }
}
