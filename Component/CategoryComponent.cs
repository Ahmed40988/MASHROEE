using MASHROEE.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MASHROEE.Component
{
    public class CategoryComponent: ViewComponent
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryComponent(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        public IViewComponentResult Invoke()
        {
            var Categoroies=categoryRepository.GetAllCategorys();
            return View(Categoroies);
        }


    }
}
