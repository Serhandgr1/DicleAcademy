using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Services.Contracts;

namespace DicleAcademy.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICoursesCategoriesService _coursesCategoriesService;
        private readonly IStringLocalizer<CategoryController> _loc;

        public CategoryController(IStringLocalizer<CategoryController> loc,ICoursesCategoriesService coursesCategoriesService)
        {
            _loc = loc; 
            _coursesCategoriesService = coursesCategoriesService;
        }

        public IActionResult Index()
        {
          
            var category=  _coursesCategoriesService.GetAllCoursesCategories();
            foreach (var item in category) 
            {
                item.CategoryDescription = _loc[item.CategoryDescription].ToString();
                item.CategoryName = _loc[item.CategoryName].ToString();
            }
            return View("CategoryIndex" , category);
        }
        public IActionResult CategoryDetails(int id)
        { //category detail
            return View("CategoryDetailsIndex");
        }
    }
}
