using DicleAcademy.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace DicleAcademyV2.Controllers
{
    public class HeaderController : Controller
    {
        private readonly IStringLocalizer<HeaderController> _loc;
        public HeaderController(IStringLocalizer<HeaderController> loc)
        {
            _loc = loc;
            
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult HeaderLink()
        {
            return PartialView();
        }
    }
}
