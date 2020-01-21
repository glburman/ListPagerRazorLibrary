namespace ListPagerRazorLibrary
{
    public static class AppConstants
    {
#if DEBUG
        public const bool IS_DEBUG = true;
#else
        public const bool IS_DEBUG = false;
#endif
        public const int PAGE_NUMBER = 1;
        public const int PAGE_SIZE = 20;
        public const int MAX_PAGER_PAGES = 5;
        public const int PAGE_COUNT = 0;
        public const int DROP_DOWN_INC = 5;
        public const int MAX_PAGE_SIZE = 300;
        public const int MIN_PAGE_SIZE = 1;
        public const string PAGE_SIZE_INPUT = "pageSizeIn";
    }
}
