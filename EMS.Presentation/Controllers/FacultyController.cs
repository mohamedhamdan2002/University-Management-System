using EMS.Services.Contracts;
using EMS.Service.ViewModels.Faculty;
using Microsoft.AspNetCore.Mvc;
using EMS.DataAccess.Entities.Models;

namespace EMS.Presentation.Controllers
{
    //[Route("Universities/{universityId:guid}/Faculties")]
    public class FacultyController : Controller
    {
        private readonly IServiceManager _service;
        public FacultyController(IServiceManager service)
        {
            _service = service;
        }
        // GET: FacultiesController
        [HttpGet]
        public async Task<IActionResult> Index(Guid universityId)
        {
            var models = await _service.FacultyService.GetFacultiesAsync(universityId, trackChanges: false);
            ViewBag.universityId = universityId;
            return View(models);
        }

        // GET: FacultiesController/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(Guid universityId, Guid id)
        {
            var model = await _service.FacultyService.GetFacultyAsync(universityId, id, trackChanges: false, new[] { "Groups" });
            ViewBag.universityId = universityId;
            return View(model);
        }

        // GET: FacultiesController/Create
        [HttpGet]
        public ActionResult Create(Guid universityId)
        {
            ViewBag.universityId = universityId;
            return View();
        }

        // POST: FacultiesController/Create
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

        // GET: FacultiesController/Edit/5
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

        // POST: FacultiesController/Edit/5
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

        // GET: FacultiesController/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(Guid universityId, Guid id)
        {
            var model = await _service.FacultyService.GetFacultyAsync(universityId, id, trackChanges: false);
            ViewBag.universityId = universityId;
            return View(model);
        }

        // POST: FacultiesController/Delete/5
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
