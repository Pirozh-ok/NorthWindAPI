using NorthWindAPI.Models;
using NorthWindAPI.Services.Interfaces;

namespace NorthWindAPI.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly NorthwindContext _context;
        public ProductService(NorthwindContext context)
        {
            _context = context;
        }
        public void CreateProduct(Product product)
        {
            if (product is null)
            {
                throw new ArgumentNullException("Передано пустое значение!");
            }

            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var product = GetProductById(id);
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public IQueryable<Product> GetAllProducts()
        {
            return _context.Products;
        }

        public Product GetProductById(int id)
        {
            var product =  _context.Products
                .SingleOrDefault(p => p.ProductId == id);

            if(product is null)
            {
                throw new Exception("Продукт не найден!");
            }

            return product;
        }

        public Product GetProductByName(string name)
        {
            var product =  _context.Products
                .SingleOrDefault(p => p.ProductName == name);

            if (product is null)
            {
                throw new Exception("Продукт не найден!");
            }

            return product;
        }

        public void UpdateProduct(Product product)
        {

            if (product is null)
            {
                throw new ArgumentNullException("Передано пустое значение");
            }

            var updateToProduct = GetProductById(product.ProductId);
            updateToProduct.ProductName = product.ProductName;
            updateToProduct.UnitPrice = product.UnitPrice;
            updateToProduct.OrderDetails = product.OrderDetails;
            _context.SaveChanges();
        }
    }
}
