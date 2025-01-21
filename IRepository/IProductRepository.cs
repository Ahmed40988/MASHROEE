using MASHROEE.Models;

namespace MASHROEE.IRepository
{
    public interface IProductRepository
    {
      IEnumerable<Product> GetAllProducts();

      Product GetProductById(int id);

      Product GetProductByName(string name);

    IEnumerable<Product> GetallProductsforuserid(string id);
      IEnumerable<Product> Search(string SearchTerm);
      void AddProduct(Product product);
      void updateproduct(Product product);
      void RemoveProduct(int id);
    }
}
