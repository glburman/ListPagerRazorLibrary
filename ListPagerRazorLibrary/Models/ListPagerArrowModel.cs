namespace ListPagerRazorLibrary.Models
{
    public class ListPagerArrowModel
    {
        public ListPagerArrowModel(ListPagerModel state, int direction)
        {
            IsDisabled = direction < 0 && state.PageNumber <= 1 || direction > 0 && state.PageNumber >= state.PageCount;
            if (direction < 0)
            {
                ClickFirst = "pager.goToPage(1)";
                ClickSecond = string.Format("pager.goToPage({0})", state.PageNumber - 1);
                FirstArrow = "<<";
                SecondArrow = "<";
                FirstWord = "First";
                SecondWord = "Previous";
            }
            else
            {
                ClickFirst = string.Format("pager.goToPage({0})", state.PageNumber + 1);
                ClickSecond = string.Format("pager.goToPage({0})", state.PageCount);
                FirstArrow = ">";
                SecondArrow = ">>";
                FirstWord = "Next";
                SecondWord = "Last";
            }
        }
        public bool IsDisabled { get; set; }
        public string ClickFirst { get; set; }
        public string ClickSecond { get; set; }
        public string FirstArrow { get; set; }
        public string SecondArrow { get; set; }
        public string FirstWord { get; set; }
        public string SecondWord { get; set; }
    }

}

