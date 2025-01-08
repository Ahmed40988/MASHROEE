using MASHROEE.IRepository;
using MASHROEE.Models;
using MASHROEE.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace MASHROEE.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        public CategoryController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
            var list = categoryRepository.GetAllCategorys().Select(c => new CategoryViewModel()
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                ProductsCount = c.Products.Count()
            });
            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                var category = new Category()
                {
                    Name = categoryViewModel.Name,
                    Description = categoryViewModel.Description,
                };
                categoryRepository.AddCategory(category);
                return RedirectToAction("Index", "Category");
            }
            return View(categoryViewModel);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var oldcategory=categoryRepository.GetCategoryById(id);
            var model = new CategoryViewModel()
            {
                Id=oldcategory.Id,
                Name=oldcategory.Name,
                Description=oldcategory.Description,
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(int id,CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid) 
            {
                var category = categoryRepository.GetCategoryById(id);
                category.Id = categoryViewModel.Id;
                category.Name = categoryViewModel.Name;
                category.Description = categoryViewModel.Description;
                categoryRepository.updatecategory(category);
                return RedirectToAction("index");
             }
            return View(categoryViewModel);
        }

        public IActionResult Delete(int id,Category category)
        {
            categoryRepository.RemoveCategory(id);
            return RedirectToAction("Index");
        }


    }
}
