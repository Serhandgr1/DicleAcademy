﻿using Entities.ModelsDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Services.Contracts;

namespace DicleAcademy.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICoursesCategoriesService _coursesCategoriesService;
        private readonly ICourseDetailsService _courseDetailsService;
        private readonly IInstructorsService _instructorsService;
        private readonly ICoursesService _coursesService;
        private readonly IStringLocalizer<CoursesController> _loc;

        public CoursesController(IStringLocalizer<CoursesController> loc,ICoursesCategoriesService coursesCategoriesService, ICourseDetailsService courseDetailsService, ICoursesService coursesService, IInstructorsService instructorsService)
        {
            _loc = loc;
            _coursesCategoriesService = coursesCategoriesService;
            _courseDetailsService = courseDetailsService;
            _coursesService = coursesService;
            _instructorsService = instructorsService;
        }

        public IActionResult Index()
        {
           
            List<GetCategoryWithCoursesDto> getCategoryWithCoursesDto = new List<GetCategoryWithCoursesDto>();
            List<InstructorsDto> instructorsList = _instructorsService.GetAllInstructors().ToList();

            var data =  _coursesCategoriesService.GetAllCoursesCategories();

           foreach (var item in data) 
            {
                GetCategoryWithCoursesDto getCategoryWithCourses = new GetCategoryWithCoursesDto();

                var dto =  _coursesService.GetCoursesByCategoryId(item.CategoryId);
                getCategoryWithCourses.CategoryName = _loc[item.CategoryName].ToString();
                getCategoryWithCourses.Courses = dto;
                getCategoryWithCourses.CoursesCategory = item;
                getCategoryWithCoursesDto.Add(getCategoryWithCourses);
            }
           
            return View("CoursesIndex" , Tuple.Create((List<GetCategoryWithCoursesDto>)getCategoryWithCoursesDto , (List<CoursesCategoriesDto>)data, instructorsList));
        }
        public IActionResult CoursesDetails(int id)
        {
          var courses= _courseDetailsService.GetByIdCourseDetails(id);
            if (courses is not null)
            {
                courses.CourseName = _loc[courses.CourseName].ToString();
                courses.CourseDescription = _loc[courses.CourseDescription].ToString();
                return View("CoursesDetails", courses);
            }
            else 
            {
                CourseDetailsDto course = new CourseDetailsDto();
                return View("CoursesDetails", course);
            }
        
        }
    }
}
