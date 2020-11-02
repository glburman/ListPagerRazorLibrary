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
using ListPagerRazorLibrary.Models;

namespace ListPagerExamples.Areas.ViewComponent.Pages
{
    public class AjaxSheetsModel : BloggerPageModel
    {

        public AjaxSheetsModel(AppDbContext context) : base(context)
        {
            Parameters = new ParametersModel();
        }

        public IActionResult OnGet()
        {
            Parameters.Pager.ShowRecordsOf = false;
            Parameters.Pager.ShowPageOf = false;
            Parameters.Pager.ShowDropDown = false;
            Parameters.Pager.PageSize = 1;
            Parameters.Pager.MaxPageLinks = 4;
            Parameters.Pager.PageCount = 11;
            return null;
        }

        public PartialViewResult OnPostPager(ParametersModel model = null)
        {
            //just set page number for this demo
            if (model != null && ModelState.IsValid)
            {
                Parameters = model;
                return new PartialViewResult
                {
                    ViewName = "_ListPagerSheets",
                    ViewData = new ViewDataDictionary<ListPagerModel>(ViewData, this.Parameters.Pager)
                };
            }
            throw new ArgumentException(Properties.Resources.PARAM_MODEL_ERROR);
        }

    }
}
