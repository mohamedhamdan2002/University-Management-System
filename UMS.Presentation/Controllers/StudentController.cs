using UMS.Services.Contracts;
using UMS.Service.ViewModels.Student;
using Microsoft.AspNetCore.Mvc;

namespace UMS.Presentation.Controllers
{
    public class StudentController : Controller
    {
        private readonly IServiceManager _service;
        public StudentController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index(Guid groupId)
        {
            var models = await _service.StudentService.GetStudentsAsync(groupId, trackChanges: false);
            return View(models);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid groupId, Guid id)
        {
            var model = await _service.StudentService.GetStudentAsync(groupId, id, trackChanges: false);
            return View(model);
        }

        [HttpGet]
        public ActionResult Create(Guid groupId)
        {
            return View();
        }

        [HttpPost]
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

        [HttpGet]
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

        [HttpPost]
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

        [HttpGet]
        public async Task<IActionResult> Delete(Guid groupId, Guid id)
        {
            var model = await _service.StudentService.GetStudentAsync(groupId, id, trackChanges: false);
            return View(model);
        }

        [HttpPost]
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
