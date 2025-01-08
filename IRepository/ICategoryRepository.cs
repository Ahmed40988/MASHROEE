using MASHROEE.Models;

namespace MASHROEE.IRepository
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAllCategorys();
        Category GetCategoryById(int id);
        Category GetCategoryByName(string name);
        List<Product> GetProducts(int CategoryId);
        List<Product> Search(int CategoryId, string SearchTerm);
        void AddCategory(Category category);
        void updatecategory(Category category);
        void RemoveCategory(int id);
    }
}
