using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ListPagerExamples.Models
{
    public class BloggerPageModel : PageModel
    {
        private readonly AppDbContext _db;
        private List<Blogger> _bloggers;
        
        public BloggerPageModel(AppDbContext context)
        {
            _db = context;
            _bloggers = new List<Blogger>();
            Parameters = new ParametersModel();
        }

      
        public void GetPage()
        {
            if (Parameters != null)
            {
                var result = Parameters.SearchTerm == null
                    ? Db.Set<Blogger>()
                    : Db.FilterLastName(Parameters.SearchTerm);

                Parameters.Pager.TotalRecords = Db.Set<Blogger>().Count();
                Parameters.Pager.FilteredRecordCount = result.Count();
                Parameters.Pager.CheckBoundaries();

                int skip = Parameters.Pager.PageSize * (Parameters.Pager.PageNumber - 1);
                Bloggers = result
                        .Skip(skip)
                        .Take(Parameters.Pager.PageSize)
                        .ToList();
            }
            else
            {
                throw new ArgumentException(Properties.Resources.GETPAGE_PARAM_ERROR);
            }

        }
        public ParametersModel Parameters { get; set; }

        //one (current) page only
        public List<Blogger> Bloggers { get { return _bloggers; } set { _bloggers = value; } }

        public AppDbContext Db => _db;

    }
}
