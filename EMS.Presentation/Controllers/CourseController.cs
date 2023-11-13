using EMS.Services.Contracts;
using EMS.Service.ViewModels.Course;
using Microsoft.AspNetCore.Mvc;

namespace EMS.Presentation.Controllers
{
    [Route("Faculties/{divisionId:guid}/Courses")]
    public class CourseController : Controller
    {
        private readonly IServiceManager _service;
        public CourseController(IServiceManager service)
        {
            _service = service;
        }
        // GET: CoursesController
        [HttpGet]
        public async Task<IActionResult> Index(Guid divisionId)
        {
            var models = await _service.CourseService.GetCoursesAsync(divisionId, trackChanges: false);
            return View(models);
        }

        // GET: CoursesController/Details/5
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(Guid divisionId, Guid id)
        {
            var model = await _service.CourseService.GetCourseAsync(divisionId, id, trackChanges: false);
            return View(model);
        }

        // GET: CoursesController/Create
        [HttpGet("[action]")]
        public ActionResult Create(Guid divisionId)
        {
            return View();
        }

        // POST: CoursesController/Create
        [HttpPost("[action]")]
        public async Task<ActionResult> Create(Guid divisionId, CourseForCreationViewModel courseForCreation)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    await _service.CourseService.CreateCourseForDivisionAsync(divisionId, courseForCreation, trackChanges: false);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        // GET: CoursesController/Edit/5
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(Guid divisionId, Guid id)
        {
            var model = await _service.CourseService.GetCourseAsync(divisionId, id, trackChanges: false);
            var modelToUpdate = new CourseForUpdateViewModel
            {
                Name = model.Name,
            };
            return View(modelToUpdate);
        }

        // POST: CoursesController/Edit/5
        [HttpPost("[action]/{id}")]
        public async Task<ActionResult> Edit(Guid divisionId, Guid id, CourseForUpdateViewModel courseForUpdate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _service.CourseService.UpdateCourseForDivisionAsync(divisionId, id, courseForUpdate, divisionTrackChanges: false, courseTrackChanges: true);
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch (Exception ex)
            {
                ModelState.TryAddModelError(string.Empty, ex.Message);
                return View(ModelState);
            }
        }

        // GET: CoursesController/Delete/5
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid divisionId, Guid id)
        {
            var model = await _service.CourseService.GetCourseAsync(divisionId, id, trackChanges: false);
            return View(model);
        }

        // POST: CoursesController/Delete/5
        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> Delete(Guid divisionId, Guid id, CourseViewModel model)
        {
            try
            {
                await _service.CourseService.DeleteCourseForDivisionAsync(divisionId, id, trackChanges: false);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }

        }
    }
}
