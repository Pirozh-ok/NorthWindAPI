using NorthWindAPI.Models;

namespace NorthWindAPI.Paginations
{
    public class OrderParameters : QueryParameters
    {
        public uint OrderYearMin { get; set; } = 0;
        public uint OrderYearMax { get; set; } = (uint)DateTime.Now.Year;
        public uint DeliveryYearMin { get; set; } = 0;
        public uint DeliveryYearMax { get; set; } = (uint)DateTime.Now.Year;

        public IQueryable<Order>? Collection { get; set; }
        public override void Filtering()
        {
            Collection = Collection.Where(o => o.OrderDate.Value.Year >= OrderYearMin
                                               && o.OrderDate.Value.Year <= OrderYearMax
                                               && o.DeliveryDate.Value.Year >= DeliveryYearMin
                                               && o.DeliveryDate.Value.Year <= DeliveryYearMax);
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
            {
                IsSuccess = false;
                return Format == "xml" ? Converter.ToXml(_notFoundMessage) : Converter.ToJson(_notFoundMessage);
            }

            return Formatting();
        }

        public override void Sorting()
        {
            Collection = Sort == "desc" ? Collection.OrderByDescending(c => c.OrderDate) : Collection.OrderBy(c => c.OrderDate);
        }
    }
}
