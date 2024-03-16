using DicleAcademy.Controllers;
using Entities.Models;
using Entities.ModelsDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Services.Contracts;
using Services.EFCore;

namespace DicleAcademyV2.Controllers
{
    public class FooterController : Controller
    {
        private readonly IGalleryService _galleryService;
        private readonly IContactService _contactService;
        private readonly IStringLocalizer<FooterController> _loc;
        public FooterController(IStringLocalizer<FooterController> loc , IGalleryService galleryService, IContactService contactService)
        {
            _loc = loc;
            _galleryService = galleryService;
            _contactService = contactService;
        }
        public IActionResult Index()
        { 
            var contact =  _contactService.GetAllContact();
            var gallery=  _galleryService.GetAllGallery();
            return PartialView(Tuple.Create((List<GalleryDto>)gallery, (List<ContactDto>)contact));
        }
    }
}
