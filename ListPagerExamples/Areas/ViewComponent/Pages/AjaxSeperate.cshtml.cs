using ListPagerExamples.Models;
using ListPagerRazorLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListPagerExamples.Areas.ViewComponent.Pages
{
    public class VCAjaxTwoModel : BloggerPageModel
    {
        private readonly AppDbContext _db;
        public VCAjaxTwoModel(AppDbContext db) : base(db)
        {
            _db = db;
            Parameters = new ParametersModel();
        }

        public PartialViewResult OnPostPager(ParametersModel model)
        {
            if (model != null)
            {
                Parameters = model;
                GetPage();
                Parameters.Pager.ShowRecordsOf = Parameters.Pager.IsFiltered;
                Parameters.Pager.ShowPageOf = !Parameters.Pager.IsFiltered;
                return new PartialViewResult
                {
                    ViewName = "_ListPager",
                    ViewData = new ViewDataDictionary<ListPagerModel>(ViewData, this.Parameters.Pager)
                };
            }
            throw new System.ArgumentException(Properties.Resources.PARAM_MODEL_ERROR);
        }

        public PartialViewResult OnPost(ParametersModel model)
        {
            if (model != null)
            {
                Parameters = model;
                GetPage();
                Parameters.Pager.ShowRecordsOf = Parameters.Pager.IsFiltered;
                Parameters.Pager.ShowPageOf = !Parameters.Pager.IsFiltered;
                return new PartialViewResult
                {
                    ViewName = "_ListTable",
                    ViewData = new ViewDataDictionary<List<Blogger>>(ViewData, Bloggers)
                };
            }
            throw new System.ArgumentException(Properties.Resources.PARAM_MODEL_ERROR);
        }
    }
}
