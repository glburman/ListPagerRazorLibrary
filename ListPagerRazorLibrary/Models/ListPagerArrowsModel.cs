namespace ListPagerRazorLibrary.Models
{
    public class ListPagerArrowsModel
    {
        public ListPagerArrowsModel(ListPagerModel state, int direction, bool single = false)
        {
            Direction = direction;
            Single = single;

            IsDisabled = direction < 0 && state.PageNumber <= 1 || direction > 0 && state.PageNumber >= state.PageCount;
            if (direction < 0)
            {
                LeftPageNumber = 1;
                RightPageNumber = state.PageNumber - 1;


                ClickLeft = "pager.goToPage(1)";
                ClickRight = string.Format("pager.goToPage({0})", state.PageNumber - 1);

                LeftArrow = "<<";
                RightArrow = "<";
                LeftWord = "Left";
                RightWord = "Previous";
            }
            else
            {
                LeftPageNumber = state.PageNumber + 1;
                RightPageNumber = state.PageCount;

                ClickLeft = string.Format("pager.goToPage({0})", state.PageNumber + 1);
                ClickRight = string.Format("pager.goToPage({0})", state.PageCount);
                LeftArrow = ">";
                RightArrow = ">>";
                LeftWord = "Next";
                RightWord = "Last";
            }
        }
        public bool IsDisabled { get; set; }
        public string ClickLeft { get; set; }
        public string ClickRight { get; set; }
        public string LeftArrow { get; set; }
        public string RightArrow { get; set; }
        public string LeftWord { get; set; }
        public string RightWord { get; set; }
        public int Direction { get; set; }
        public int LeftPageNumber { get; set; }
        public int RightPageNumber { get; set; }
        public bool Single { get; set; }

    }
}

