namespace NorthWindAPI
{
    public abstract class QueryParameters
    {
        private const int _maxPageSize = 30;
        private int _pageSize = 15;
        private int _pageNumber = 1;
        private string _sort = "asc";
        private string _format = "json";
        protected string _notFoundMessage = "Ничего не найдено";
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
        public string Sort
        {
            get
            {
                return _sort;
            }
            set
            {
                _sort = (value.ToLower() == "asc" || value.ToLower() == "desc") ? value : "asc";
            }
        }
        public string Format
        {
            get
            {
                return _format;
            }
            set
            {
                _format = (value.ToLower() == "json" || value.ToLower() == "xml") ? value : "json";
            }
        }

        public abstract string ResultProcessing();
        public abstract void Pagination();
        public abstract void Sorting();
        public abstract void Filtering();
        public abstract string Formatting();
    }
}
