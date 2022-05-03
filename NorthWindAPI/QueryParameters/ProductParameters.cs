using NorthWindAPI.Models;

namespace NorthWindAPI.Paginations
{
    public class ProductParameters : QueryParameters
    {
        public uint PriceMin { get; set; } = 0;
        public uint PriceMax { get; set; } = uint.MaxValue;
        public IQueryable<Product>? Collection { get; set; }
        public override void Filtering()
        {
           Collection = Collection.Where(p => p.UnitPrice >= PriceMin && p.UnitPrice <= PriceMax);
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
            Collection = Sort == "desc" ? Collection.OrderByDescending(p => p.ProductName) : Collection.OrderBy(p => p.ProductName);
        }
    }
}
