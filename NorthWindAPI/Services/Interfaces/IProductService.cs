using NorthWindAPI.Models;

namespace NorthWindAPI.Services.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts(QueryParameters filter);
        Product GetProductById(int id);
        Product GetProductByName(string name);
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
    }
}
