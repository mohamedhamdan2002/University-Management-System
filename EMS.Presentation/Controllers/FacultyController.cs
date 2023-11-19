using EMS.Services.Contracts;
using EMS.Service.ViewModels.Faculty;
using Microsoft.AspNetCore.Mvc;
using EMS.DataAccess.Entities.Models;
using EMS.Presentation.Utilities;

namespace EMS.Presentation.Controllers
{
    public class FacultyController : Controller
    {
        private readonly IServiceManager _service;
        public FacultyController(IServiceManager service)
        {
            _service = service;
        }
     
        [HttpGet]
        public async Task<IActionResult> Index(Guid universityId)
        {
            var models = await _service.FacultyService.GetFacultiesAsync(universityId, trackChanges: false);
            ViewBag.universityId = universityId;
            return View(models);
        }


        [HttpGet]
        public async Task<IActionResult> Details(Guid universityId, Guid id)
        {
            var model = await _service.FacultyService.GetFacultyAsync(universityId, id, trackChanges: false, new[] { NavigationProperties.Groups });
            ViewBag.universityId = universityId;
            return View(model);
        }

     
        [HttpGet]
        public ActionResult Create(Guid universityId)
        {
            ViewBag.universityId = universityId;
            return View();
        }

        
        [HttpPost]
        public async Task<ActionResult> Create(Guid universityId, FacultyForCreationViewModel FacultyForCreation)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    await _service.FacultyService.CreateFacultyForUniversityAsync(universityId, FacultyForCreation, trackChanges: false);
                    return RedirectToAction(nameof(Index), new { universityId });
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid universityId, Guid id)
        {
            var model = await _service.FacultyService.GetFacultyAsync(universityId, id, trackChanges: false);
            ViewBag.universityId = universityId;
            var modelToUpdate = new FacultyForUpdateViewModel
            {
                Name = model.Name,
                Description = model.Description,
            };
            return View(modelToUpdate);
        }

        
        [HttpPost]
        public async Task<ActionResult> Edit(Guid universityId, Guid id, FacultyForUpdateViewModel FacultyForUpdate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _service.FacultyService.UpdateFacultyForUniversityAsync(universityId, id, FacultyForUpdate, universityTrackChanges: false, facultyTrackChanges: true);
                    return RedirectToAction(nameof(Index), new { universityId });
                }
                return View();
            }
            catch (Exception ex)
            {
                ModelState.TryAddModelError(string.Empty, ex.Message);
                return View(ModelState);
            }
        }

       
        [HttpGet]
        public async Task<IActionResult> Delete(Guid universityId, Guid id)
        {
            var model = await _service.FacultyService.GetFacultyAsync(universityId, id, trackChanges: false);
            ViewBag.universityId = universityId;
            return View(model);
        }

   
        [HttpPost]
        public async Task<IActionResult> Delete(Guid universityId, Guid id, FacultyViewModel model)
        {
            try
            {
                await _service.FacultyService.DeleteFacultyForUniverstiyAsync(universityId, id, trackChanges: false);
                return RedirectToAction(nameof(Index), new { universityId });
            }
            catch
            {
                return View();
            }
        }
    }
}
