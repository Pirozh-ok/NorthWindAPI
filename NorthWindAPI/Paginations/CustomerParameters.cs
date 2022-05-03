using NorthWindAPI.Models;

namespace NorthWindAPI.Paginations
{
    public class CustomerParameters : QueryParameters
    { 
        public string? Country { get; set; }
        public string? City { get; set; }
        public IQueryable<Customer>? Collection { get; set; }
        public override void Filtering()
        {
            if (Country is not null)
                Collection = Collection.Where(c => c.Country == Country);

            if(City is not null)
                Collection = Collection.Where(c => c.City == City);
        }

        public override string Formatting()
        {
            return Format == "xml" ? Converter.ToXml(Collection.ToList()) : Converter.ToJson(Collection.ToList());
        }

        public override void Pagination()
        {
            Collection = Collection
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize);
        }

        public override string ResultProcessing()
        {
            Filtering();
            Pagination();
            Sorting();

            if (Collection.Count() <= 0)
                return Format == "xml" ? Converter.ToXml(_notFoundMessage) : Converter.ToJson(_notFoundMessage);

            return Formatting();
        }

        public override void Sorting()
        {
            Collection = Sort == "desc" ? Collection.OrderByDescending(c => c.CompanyName) : Collection.OrderBy(c => c.CompanyName);
        }
    }
}
