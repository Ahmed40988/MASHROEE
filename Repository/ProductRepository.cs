using MASHROEE.IRepository;
using MASHROEE.Models;
using MASHROEE.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MASHROEE.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MASHROEEDbContext context;

        public ProductRepository(MASHROEEDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return context.Products.Include(p => p.category).Include(p => p.user).ToList();
        }

        public Product GetProductById(int id)
        {
            return context.Products.Include(p => p.category).Include(p => p.user).SingleOrDefault(p => p.Id == id);
        }

        public IEnumerable<Product> GetallProductsforuserid(string id)
        {
            return context.Products.Include(p=> p.category)
                .Include(p=>p.user).Where(p => p.userid == id).ToList();
        }
        public Product GetProductByName(string name)
        {
            return context.Products.Include(p => p.category).SingleOrDefault(p => p.Name == name);
        }
        public IEnumerable<Product> GetallProductsbycategory(int  id) 
        {
            return context.Products.Include(p => p.category).Where(p=>p.categoryid == id).ToList();
        }
        public IEnumerable<Product> Searchbyname(string SearchTerm)
        {
            return context.Products.Where(p =>
                    (p.Name.Contains(SearchTerm) ||
                     p.Description.Contains(SearchTerm)
                )).ToList();
        }
        public IEnumerable<Product> Searchbyprice(decimal SearchTerm)
        {
            return context.Products.Where(p =>
                    p.Price >= SearchTerm && p.Price <= SearchTerm + 50
                ).OrderBy(c=>c.Price).ToList();
        }
        public IEnumerable<ProductViewModel> Maping(IEnumerable<Product> products)
        {
            var listmodel = new List<ProductViewModel>();
            foreach (var item in products)
            {
                var model = new ProductViewModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    CategoryName = item.category?.Name ?? "No Category", 
                    Price = item.Price,
                    CreatedDate = item.CreatedDate,
                    image = item.image,
                    username = item.user?.Name ?? "No User",
                    Quantity = item.Quantity,
                };
                listmodel.Add(model);
            }
            return listmodel;
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
				var product = GetProductById(id);
            if (product != null)
            {
                context.Products.Remove(product);
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine($"{product.Name} is Not Found");
            }


        }
	}
}
