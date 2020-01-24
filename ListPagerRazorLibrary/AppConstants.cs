namespace ListPagerRazorLibrary
{
    public static class AppConstants
    {
#if DEBUG
        public const bool IS_DEBUG = true;
#else
        public const bool IS_DEBUG = false;
#endif
        //Model constants
        public const int PAGE_NUMBER = 1;
        public const int PAGE_SIZE = 20;
        public const int MAX_PAGE_LINKS = 5;
        public const int PAGE_COUNT = 0;
        public const int DROP_DOWN_INC = 5;
        public const int MAX_PAGE_SIZE = 300;
        public const int MIN_PAGE_SIZE = 1;
        public const string PAGE_SIZE_INPUT = "pageSizeIn";
        //View Constants
        public const string VIEWNAME_ARROWS = "ListPagerArrows";
        public const string VIEWNAME_DROPDOWN = "ListPagerDropdown";
        public const string VIEWNAME_LINKS = "ListPagerLinks";
        public const string VIEWNAME_PAGEOF = "ListPagerPageOf";
        public const string VIEWNAME_PAGESIZE = "ListPagerPageSize";
        public const string VIEWNAME_POSTFORM = "ListPagerPostForm";
        public const string VIEWNAME_RECORDS = "ListPagerRecords";
        public const string PAGE_LINK_ACTIVE_CLASS = "active";
        public const string DROPDOWN_BOUNDARY_CLASS = "last";
        public const string PAGE_LINK_FORMAT = "/?page={0}";
        //Component Constants
        public const string VIEW_PATH_LISTPAGER = "../ListPager";
        public const string VIEW_PATH_LISTPAGER_ARROWS = "../ListPagerArrows";
        public const string VIEW_PATH_LISTPAGER_DROPDOWN = "../ListPagerDropdown";
        public const string VIEW_PATH_LISTPAGER_LINKS = "../ListPagerLinks";
        public const string VIEW_PATH_LISTPAGER_PAGEOF = "../ListPagerPageOf";
        public const string VIEW_PATH_LISTPAGER_PAGESIZE = "../ListPagerPageSize";
        public const string VIEW_PATH_LISTPAGER_POSTFORM = "../ListPagerPostForm";
        public const string VIEW_PATH_LISTPAGER_RECORDS = "../ListPagerRecords";
        //Assembly Constants
        public const string ASSEMBLY_TYPE_SCRIPT = "ListPagerRazorLibrary.js";
        public const string ASSEMBLY_TYPE_STYLES = "ListPagerRazorLibrary.css";
    }
}
