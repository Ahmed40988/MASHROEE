using MASHROEE.IRepository;
using MASHROEE.Models;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;

namespace MASHROEE.Repository
{
    public class ProductRepository:IProductRepository
    {
        private readonly MASHROEEDbContext context;

        public ProductRepository(MASHROEEDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return context.Products.Include(p=>p.category). ToList();
        }

        public Product GetProductById(int id)
        {
            return context.Products.SingleOrDefault(p => p.Id == id);
        }
        public Product GetProductByName(string name)
        {
            return context.Products.SingleOrDefault(p => p.Name == name);
        }
        public IEnumerable<Product>Search(string SearchTerm)
        {
            return context.Products.Where(p => 
                    (p.Name.Contains(SearchTerm) ||
                     p.Description.Contains(SearchTerm) ||
                     p.Price.ToString().Contains(SearchTerm)
                )).ToList();
        }
        public void AddProduct(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
            Console.WriteLine($"Product {product.Name} added successfully.");
        }
        public void updateproduct(Product product)
        {
            context.Entry(product).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void RemoveProduct(int id)
        {
            Product product = context.Products.SingleOrDefault(p => p.Id == id);
            if (product != null)
            {
                context.Products.Remove(product);
                context.SaveChanges();
            }
            else
                throw new ArgumentException("product not found!");
        }
    }
}
