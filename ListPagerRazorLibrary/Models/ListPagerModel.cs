using System;
namespace ListPagerRazorLibrary.Models
{
    [Serializable]
    public class ListPagerModel
    {
        private int _minPageSize = AppConstants.MIN_PAGE_SIZE;
        private int _maxPageSize = AppConstants.MAX_PAGE_SIZE;
        private int _pageSize = AppConstants.PAGE_SIZE;
        private int _pageNumber = AppConstants.PAGE_NUMBER;
        private int _pageCount = AppConstants.PAGE_COUNT;
        private int? _totalRecords;
        private int? _filteredRecordCount;

        public ListPagerModel()
        {
        }
        
        public void CheckBoundaries()
        {
            _totalRecords = _totalRecords ?? 0;
            _filteredRecordCount = _filteredRecordCount ?? _totalRecords;
            _pageSize = (_pageSize == 0 && _filteredRecordCount > 0) ? (int)_filteredRecordCount : _pageSize;
            _pageCount = _filteredRecordCount > 0 ? (int)Math.Ceiling((int)_filteredRecordCount / (double)_pageSize) : 1;
            _pageNumber = Math.Max(1, _pageNumber > _pageCount ? _pageCount : _pageNumber);
        }
        public int Start    //UI page enumeration
        {
            get
            {
                int pnm = PageNumber - MaxPageLinks + 1;
                return Math.Max(1, Math.Max(pnm % MaxPageLinks, pnm));
            }
        }
        public int End
        {
            get => Math.Min(Start + MaxPageLinks - 1, PageCount);
        }
       
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value < _minPageSize
                    ? _minPageSize : value > _maxPageSize
                        ? _maxPageSize : value;
        }
        public int PageNumber
        {
            get => _pageNumber;
            set => _pageNumber = value;
        }
        public int PageCount
        {
            get => _pageCount;
            set => _pageCount = value < 0 ? 0 : value;
        }
        public int MaxPageLinks { get; set; } = AppConstants.MAX_PAGE_LINKS;
        public int MaxPageSize
        {
            get => _maxPageSize;
            set => _maxPageSize = Math.Min(AppConstants.MAX_PAGE_SIZE, value);
        }
        public int MinPageSize
        {
            get => _minPageSize;
            set => _minPageSize = Math.Max(AppConstants.MIN_PAGE_SIZE, value);
        }
        public int? TotalRecords
        {
            get => _totalRecords ?? 0;
            set => _totalRecords = value;
        }
        public int? FilteredRecordCount
        {
            get => _filteredRecordCount;
            set => _filteredRecordCount = value;
        }
        public bool IsFiltered
        {
            get => FilteredRecordCount < TotalRecords;
        }
        public bool ShowPageSize { get; set; } = true;
        public bool ShowPageOf { get; set; } = false;
        public bool ShowDropDown { get; set; } = false;
        public bool ShowRecordsOf { get; set; } = false;
        public int DropDownIncrement { get; set; } = AppConstants.DROP_DOWN_INC;
        public string PageSizeInputId { get; set; } = AppConstants.PAGE_SIZE_INPUT;
        public string PostTarget { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
    }
}
