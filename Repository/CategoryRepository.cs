using MASHROEE.IRepository;
using MASHROEE.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace MASHROEE.Repository
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly MASHROEEDbContext context;

        public CategoryRepository(MASHROEEDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Category> GetAllCategorys()
        {
            return context.Category.Include(c=>c.Products) .ToList();
        }

        public Category GetCategoryById(int id)
        {
            return context.Category.Include(c=>c.Products).FirstOrDefault(p => p.Id == id);
        }
        public Category GetCategoryByName(string name)
        {
            return context.Category.Include(c => c.Products).FirstOrDefault(p => p.Name == name);
        }
        public List<Product> GetProducts(int CategoryId)
        {
            var list = context.Products.Where(p => p.categoryid == CategoryId).ToList();
            return list;
        }
        public List<Product> Search(int CategoryId, string SearchTerm)
        {
            var list = context
                .Products
                .Where(p => p.categoryid == CategoryId &&
                    (p.Name.Contains(SearchTerm) ||
                     p.Description.Contains(SearchTerm) ||
                     p.Price.ToString().Contains(SearchTerm)
                )).ToList();
            return list;
        }
        public void AddCategory(Category category)
        {
            context.Category.Add(category);
            context.SaveChanges();
            Console.WriteLine($"Category {category.Name} added successfully.");
        }
        public void updatecategory(Category category)
        {
            context.Entry(category).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void RemoveCategory(int id)
        {
            Category category = context.Category.SingleOrDefault(p => p.Id == id);
            if (category != null)
            {
                context.Category.Remove(category);
                context.SaveChanges();
            }
            else
                throw new ArgumentException("category not found!");
        }
    }
}
