using EMS.Services.Contracts;
using EMS.Service.ViewModels.Course;
using Microsoft.AspNetCore.Mvc;
using EMS.DataAccess.Entities.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using EMS.Presentation.Utilities;

namespace EMS.Presentation.Controllers
{
    public class CourseController : Controller
    {
        private readonly IServiceManager _service;
        public CourseController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index(Guid divisionId)
        {
            var models = await _service.CourseService.GetCoursesAsync(divisionId, trackChanges: false);
            ViewBag.divisionId = divisionId;
            return View(models);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid divisionId, Guid id)
        {
            var model = await _service.CourseService.GetCourseAsync(divisionId, id, trackChanges: false, new[] { NavigationProperties.Enrollments_Student });
            ViewBag.divisionId = divisionId;
            return View(model);
        }

        [HttpGet]
        public ActionResult Create(Guid divisionId)
        {
            ViewBag.divisionId = divisionId;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Guid divisionId, CourseForCreationViewModel courseForCreation)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    await _service.CourseService.CreateCourseForDivisionAsync(divisionId, courseForCreation, trackChanges: false);
                    return RedirectToAction(nameof(Index), new { divisionId });
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid divisionId, Guid id)
        {
            var model = await _service.CourseService.GetCourseAsync(divisionId, id, trackChanges: false);
            ViewBag.divisionId = divisionId;
            var modelToUpdate = new CourseForUpdateViewModel
            {
                Name = model.Name,
                Code = model.Code,
                Credits = model.Credits,
                Semester = model.Semester,
                Description = model.Description,
            };
            return View(modelToUpdate);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Guid divisionId, Guid id, CourseForUpdateViewModel courseForUpdate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _service.CourseService.UpdateCourseForDivisionAsync(divisionId, id, courseForUpdate, divisionTrackChanges: false, courseTrackChanges: true);
                    return RedirectToAction(nameof(Index), new { divisionId });
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
        public async Task<IActionResult> Delete(Guid divisionId, Guid id)
        {
            var model = await _service.CourseService.GetCourseAsync(divisionId, id, trackChanges: false);
            ViewBag.divisionId = divisionId;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid divisionId, Guid id, CourseViewModel model)
        {
            try
            {
                await _service.CourseService.DeleteCourseForDivisionAsync(divisionId, id, trackChanges: false);
                return RedirectToAction(nameof(Index), new { divisionId });
            }
            catch
            {
                return View();
            }

        }
    }
}
