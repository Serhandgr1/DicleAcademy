using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Services.Contracts;

namespace DicleAcademy.Controllers
{
    public class TeamsController : Controller
    {
        private readonly IInstructorsService _instructorsService;
        private readonly IStringLocalizer<TeamsController> _loc;

        public TeamsController(IStringLocalizer<TeamsController> loc,IInstructorsService instructorsService)
        {
            _loc = loc;
            _instructorsService = instructorsService;
        }

        public IActionResult Index()
        {
         
            var instruc= _instructorsService.GetAllInstructors();
            foreach (var item in instruc) 
            {
                item.InstructorDescription = _loc[item.InstructorDescription].ToString();   
            }
            return View("TeamsIndex" , instruc);
        }
    }
}
