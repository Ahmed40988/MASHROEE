using MASHROEE.IRepository;
using MASHROEE.Models;
using MASHROEE.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

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
        [HttpGet]
        [Authorize(Roles ="admin")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                var listc = categoryRepository.GetAllCategorys();
                if (listc.Any(c => c.Name == categoryViewModel.Name))
                {
                    TempData["CategoryExistsMessage"] = "Category is already added!";
                    return View(categoryViewModel); 
                }

                var category = new Category()
                {
                    Name = categoryViewModel.Name,
                    Description = categoryViewModel.Description,
                };
                categoryRepository.AddCategory(category);
                return RedirectToAction("Index", "DashBoard");
            }
            return View(categoryViewModel);
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
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
                return RedirectToAction("index","DashBoard");
             }
            return View(categoryViewModel);
        }
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int id,Category category)
        {
            categoryRepository.RemoveCategory(id);
            return RedirectToAction("Index","DashBoard");
        }

        public IActionResult Filterbycategory(int id) 
        {
            var list =productRepository.GetallProductsbycategory(id);
            var listmodel =productRepository.Maping(list);
            return View(listmodel);
        }



    }
}
