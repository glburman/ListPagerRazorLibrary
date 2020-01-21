using ListPagerExamples.Models;
using ListPagerRazorLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListPagerExamples.Areas.ViewComponent.Pages
{
    public class VCAjaxClientModel : BloggerPageModel
    {
        private readonly AppDbContext _db;
        public VCAjaxClientModel(AppDbContext db):base(db)
        {
            _db = db;
            Parameters = new ParametersModel();

        }

        public IActionResult OnGet(string search = null)
        {
            Parameters.Pager.PageSize = 5;
            Parameters.SearchTerm = search;            
            return null;
        }


        public PartialViewResult OnPostPager(ParametersModel model)
        {
            if (model != null )
            {
                Parameters = model;
                GetPage();
                return new PartialViewResult
                {
                    ViewName = "_PagerPartial",
                    ViewData = new ViewDataDictionary<ListPagerModel>(ViewData, this.Parameters.Pager)
                };
            }
            throw new ArgumentException("ParametersModel null or invalid.");
        }


        public JsonResult OnPost(ParametersModel model)
        {
            if (model != null)
            {
                Parameters = model;
                GetPage();
                return new JsonResult(Bloggers);
            }
            throw new ArgumentException("ParametersModel null or invalid.");
        }
    }
}
