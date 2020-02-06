using System;
using System.Collections.Generic;
using System.Text;

namespace ListPagerRazorLibrary.Models
{
    public class ListPagerItemModel
    {
        public ListPagerItemModel()
        {

        }
        public ListPagerItemModel(int pager, string text, string srword, bool active= false, bool disabled = false, string cssclass = null)
        {
            PageNumber = pager;
            Text = text;
            Disabled = disabled;
            SrWord = srword ?? text ?? string.Empty;
            CssClass = cssclass ?? String.Empty;
            Active = active;
        }

        public int PageNumber { get; set; }
        public string Text { get; set; }
        public bool Disabled { get; set; }
        public string SrWord { get; set; }
        public string CssClass { get; set; }
        public bool Active { get; set; }
    }
}
