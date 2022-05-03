/*namespace NorthWindAPI.Paginations
{
    public class OrderPagination:QueryParameters
    {
        private string _sort = "asc";
        private string _format = "json";
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
    }
}
*/