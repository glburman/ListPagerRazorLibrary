using ListPagerExamples.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.IO.Pipelines;
using Newtonsoft.Json;

namespace ListPagerExamples.Areas.ViewComponent.Pages
{
    public class AjaxSingleModel : BloggerPageModel
    {
        
        public AjaxSingleModel(AppDbContext context) : base(context)
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

        public PartialViewResult OnPostPartial(ParametersModel model = null)
        {
            
            if (model != null && ModelState.IsValid)
            {
                Parameters = model;
                GetPage();
                return new PartialViewResult
                {
                    ViewName = "_CombinedPartial",
                    ViewData = new ViewDataDictionary<AjaxSingleModel>(ViewData, this)
                };
            }
            throw new ArgumentException(Properties.Resources.PARAM_MODEL_ERROR);
        }
    }
}
