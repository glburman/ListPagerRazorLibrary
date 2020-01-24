using System;
using System.Collections.Generic;
using System.Text;

namespace ListPagerRazorLibrary.Models
{
    public class ListPagerDropdownModel
    {
        public ListPagerDropdownModel(ListPagerModel model)
        {
            Increments = new List<int>();
            PageCount = model.PageCount;
            for (int step = 1; step <= model.PageCount; step++)
            {
                if ((step % model.DropDownIncrement == 0 
                        || step == 1 
                        || step == PageCount
                    ) 
                    && (step < model.Start 
                        || step > model.End))
                {
                    Increments.Add(step);
                }
            }
        }
        public List<int> Increments { get; set; }
        public int PageCount { get; set; }
    }
}
