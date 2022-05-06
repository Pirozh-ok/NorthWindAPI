using NorthWindAPI.DTOs;
using NorthWindAPI.Models;

namespace NorthWindAPI.Services.Interfaces
{
    public interface IProductService
    {
        IQueryable<Product> GetAllProducts();
        Product GetProductById(int id);
        Product GetProductByName(string name);
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
        public IEnumerable<PriceList> GetPriceList();
        public List<StaticSalesDTO> GetSalesStatistics(int maxYear, int minYear);
    }
}
