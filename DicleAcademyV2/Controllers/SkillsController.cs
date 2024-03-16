using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Services.Contracts;

namespace DicleAcademy.Controllers
{
    public class SkillsController : Controller
    {
        private readonly ISkillsService _skillsService;
        private readonly IStringLocalizer<SkillsController> _loc;

        public SkillsController(IStringLocalizer<SkillsController> loc,ISkillsService skillsService)
        {
            _loc = loc;
            _skillsService = skillsService;
        }

        public IActionResult Index()
        {
            var skills=  _skillsService.GetAllSkills();
            foreach (var item in skills)
            {
                item.SkillTitle = _loc[item.SkillTitle].ToString();
                item.SkillDescription = _loc[item.SkillDescription].ToString();

            }
            return View("SkillsIndex" , skills);
        }
    }
}
