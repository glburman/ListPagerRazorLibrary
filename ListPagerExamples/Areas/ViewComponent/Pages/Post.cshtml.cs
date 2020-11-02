using ListPagerExamples.Models;
using ListPagerRazorLibrary.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListPagerExamples.Areas.ViewComponent.Pages
{
    public class VCPostModel : BloggerPageModel
    {
       
        public VCPostModel(AppDbContext db):base(db)
        {
            //Parameters = new ParametersModel();
        }

        public void OnGet()
        {
            Parameters = new ParametersModel();
            Parameters.Pager = new ListPagerModel();
            Parameters.Pager.PageSize = 5;
            Parameters.Pager.ShowPageOf = true;
            GetPage();
        }

        public void OnPost(ListPagerModel model)
        {
            if (model != null )
            {
                Parameters = new ParametersModel();
                Parameters.Pager = model;
                GetPage();
            }
            else
            {
                throw new ArgumentException(Properties.Resources.PARAM_MODEL_ERROR);
            }
        }
       
    }
}
