using System;
using System.Collections.Generic;
using System.Text;

namespace ListPagerRazorLibrary.Models
{
    public interface IPagerItem
    {
        public string Text { get; set; }
        public bool Disabled { get; set; }
    }
}
