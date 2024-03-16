using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Services.Contracts;

namespace DicleAcademy.Controllers
{
    public class TestimonialController : Controller
    {
        private readonly IStudentsSayService _studentsSayService;
        private readonly IStringLocalizer<TestimonialController> _loc;
        public TestimonialController(IStringLocalizer<TestimonialController> loc,IStudentsSayService studentsSayService)
        {
            _loc = loc; 
            _studentsSayService = studentsSayService;
        }

        public IActionResult Index()
        {
            var studentSay= _studentsSayService.GetAllStudentsSay();
            foreach (var item in studentSay)
            {
                item.StudentsSayDescription = _loc[item.StudentsSayDescription].ToString();
            }
            return View("TestimonialIndex" , studentSay);
        }
    }
}
