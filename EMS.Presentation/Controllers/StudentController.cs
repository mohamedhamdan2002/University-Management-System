using EMS.Services.Contracts;
using EMS.Service.ViewModels.Student;
using Microsoft.AspNetCore.Mvc;

namespace EMS.Presentation.Controllers
{
    [Route("Faculties/{groupId:guid}/Students")]
    public class StudentController : Controller
    {
        private readonly IServiceManager _service;
        public StudentController(IServiceManager service)
        {
            _service = service;
        }
        // GET: StudentsController
        [HttpGet]
        public async Task<IActionResult> Index(Guid groupId)
        {
            var models = await _service.StudentService.GetStudentsAsync(groupId, trackChanges: false);
            return View(models);
        }

        // GET: StudentsController/Details/5
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(Guid groupId, Guid id)
        {
            var model = await _service.StudentService.GetStudentAsync(groupId, id, trackChanges: false);
            return View(model);
        }

        // GET: StudentsController/Create
        [HttpGet("[action]")]
        public ActionResult Create(Guid groupId)
        {
            return View();
        }

        // POST: StudentsController/Create
        [HttpPost("[action]")]
        public async Task<ActionResult> Create(Guid groupId, StudentForCreationViewModel studentForCreation)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    await _service.StudentService.CreateStudentForGroupAsync(groupId, studentForCreation, trackChanges: false);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        // GET: StudentsController/Edit/5
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(Guid groupId, Guid id)
        {
            var model = await _service.StudentService.GetStudentAsync(groupId, id, trackChanges: false);
            var modelToUpdate = new StudentForUpdateViewModel
            {
                FirstName = model.FullName.Split(' ')[0],
                LastName = model.FullName.Split(' ')[1],
                GPA = model.GPA,
                Level = (uint) model.Level,
                NationalID = model.NationalID,
            };
            return View(modelToUpdate);
        }

        // POST: StudentsController/Edit/5
        [HttpPost("[action]/{id}")]
        public async Task<ActionResult> Edit(Guid groupId, Guid id, StudentForUpdateViewModel studentForUpdate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _service.StudentService.UpdateStudentForGroupAsync(groupId, id, studentForUpdate, groupTrackChanges: false, studentTrackChanges: true);
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

        // GET: StudentsController/Delete/5
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid groupId, Guid id)
        {
            var model = await _service.StudentService.GetStudentAsync(groupId, id, trackChanges: false);
            return View(model);
        }

        // POST: StudentsController/Delete/5
        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> Delete(Guid groupId, Guid id, StudentViewModel model)
        {
            try
            {
                await _service.StudentService.DeleteStudentForGroupAsync(groupId, id, trackChanges: false);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }

        }
    }
}
