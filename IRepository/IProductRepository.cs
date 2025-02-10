using MASHROEE.Models;
using MASHROEE.ViewModel;

namespace MASHROEE.IRepository
{
    public interface IProductRepository
    {
      IEnumerable<Product> GetAllProducts();

      Product GetProductById(int id);

      Product GetProductByName(string name);
     IEnumerable<Product> GetallProductsbycategory(int id);

    IEnumerable<Product> GetallProductsforuserid(string id);
        IEnumerable<Product> Searchbyname(string SearchTerm); 
        IEnumerable<Product> Searchbyprice(decimal SearchTerm); 
        IEnumerable<ProductViewModel> Maping(IEnumerable<Product> products);
      void AddProduct(Product product);
      void updateproduct(Product product);
        void RemoveProduct(int id);

	}
}
