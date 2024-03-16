using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Services.Contracts;

namespace DicleAcademy.Controllers
{
    public class AbouteController : Controller
    {
        private readonly IAboutUsService _abouteService;
        private readonly IStringLocalizer<AbouteController> _loc;
        public AbouteController(IStringLocalizer<AbouteController> loc,IAboutUsService abouteService)
        {
            _loc = loc;
            _abouteService = abouteService;
        }

        public IActionResult Index()
        {
         
            var aboute=  _abouteService.GetAllAboutUs();
            foreach (var about in aboute)
            {
                about.AboutUsTitle = _loc[about.AboutUsTitle].ToString();
                about.AboutUsDescription = _loc[about.AboutUsDescription].ToString();

            }
            return View("AbouteIndex" , aboute);
        }
    }
}
