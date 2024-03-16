using Entities.ModelsDto;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Services.Contracts;
using Services.EFCore;
using System.Diagnostics;

namespace DicleAcademy.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBestCoursesService _bestcoursesService;
        private readonly ICoursesService _coursesService;
        private readonly IWelcomeInformationsService _welcomeInformationsService;
        private readonly ISkillsService _skillsService;
        private readonly IInstructorsService _instructorsService;
        private readonly IStudentsSayService _studentsSayService;
        private readonly IHeaderService _headerService;
        private readonly IStringLocalizer<HomeController> _loc;

        public HomeController(IStringLocalizer<HomeController> loc,IHeaderService headerService, IBestCoursesService bestcoursesService, ICoursesService coursesService, IWelcomeInformationsService welcomeInformationsService, ISkillsService skillsService, IInstructorsService instructorsService, IStudentsSayService studentsSayService)
        {
            _loc = loc;
            _bestcoursesService = bestcoursesService;
            _coursesService = coursesService;
            _welcomeInformationsService = welcomeInformationsService;
            _skillsService = skillsService;
            _instructorsService = instructorsService;
            _studentsSayService = studentsSayService;
            _headerService = headerService;
        }

        public IActionResult Index()
        {
            
            var header = _headerService.GetAllHeader();
            var bestCourses = _bestcoursesService.GetAllBestCourses();
            var instructors = _instructorsService.GetAllInstructors();
            foreach (var item in instructors)
            {
                item.InstructorDescription = _loc[item.InstructorDescription].ToString();
            }
            var courses = new List<CoursesDto>();
            foreach (var item in header) 
            {
                item.HeaderTitle= _loc[item.HeaderTitle].ToString();
                item.HeaderDescription = _loc[item.HeaderDescription].ToString();
            }
            foreach (var cours in courses)
            {
                cours.CourseName = _loc[cours.CourseName].ToString();
                
            }
            //IQueryable<List<CoursesDto>> courses;
            foreach (var course in bestCourses)
            {
                var courseDto = _coursesService.GetByIdCourses(course.CourseId);
                courses.Add((CoursesDto)courseDto);
            }
            var welcome = _welcomeInformationsService.GetAllWelcomeInformations();
            foreach (var item in welcome)
            {
                item.WelcomeInformationTitle = _loc[item.WelcomeInformationTitle].ToString();
                item.WelcomeInformationDescription = _loc[item.WelcomeInformationDescription].ToString();
            }
            var skills = _skillsService.GetAllSkills();
            foreach (var skill in skills) 
            {
                skill.SkillTitle = _loc[skill.SkillTitle].ToString();
                skill.SkillDescription= _loc[skill.SkillDescription].ToString();
            }
            var instruct = _instructorsService.GetAllInstructors();
            var studentSay = _studentsSayService.GetAllStudentsSay();
            foreach (var item in studentSay)
            {
                item.StudentsSayDescription = _loc[item.StudentsSayDescription].ToString();

            }
            //welcome abaout skills courses  InstructorsDto Testimonial
            return View(Tuple.Create((List<CoursesDto>)courses, (List<WelcomeInformationsDto>)welcome, (List<SkillsDto>)skills, (List<InstructorsDto>)instruct, (List<StudentsSayDto>)studentSay, (List<HeaderDto>)header, (List<InstructorsDto>)instructors));
        }
        public IActionResult SetLanguage(string culture, string returnUrl)
        {

            Response.Cookies.Append(
              CookieRequestCultureProvider.DefaultCookieName,
              CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
              new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
              );
            //return RedirectToAction("Index");
            return LocalRedirect(returnUrl);
        }

    }
}
