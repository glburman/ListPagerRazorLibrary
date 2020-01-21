using ListPagerRazorLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListPagerExamples.Models
{
    /* Add Search to the list model */
    public class ParametersModel
    {
        public ParametersModel()
        {
            Pager = new ListPagerModel();
        }
        public ListPagerModel Pager{ get; set; }
        public string SearchTerm { get; set; }
    }
}
