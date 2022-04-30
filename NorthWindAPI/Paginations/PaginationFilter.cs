namespace NorthWindAPI
{
    public class PaginationFilter
    {
        private const int _maxPageSize = 30;
        private int _pageSize = 15;
        private int _pageNumber = 1;
        public int PageNumber
        {
            get
            {
                return _pageNumber;
            }
            set 
            {
                _pageNumber = (value > 0) ? value : 1;
            }
        }
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > _maxPageSize) ? _maxPageSize : value;
            }
        }        
    }
}
