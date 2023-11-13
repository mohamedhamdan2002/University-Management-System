using EMS.Services.Contracts;
using EMS.Service.ViewModels.Department;
using Microsoft.AspNetCore.Mvc;

namespace EMS.Presentation.Controllers
{
    [Route("Faculties/{groupId:guid}/Departments")]
    public class DepartmentController : Controller
    {
        private readonly IServiceManager _service;
        public DepartmentController(IServiceManager service)
        {
            _service = service;
        }
        // GET: DepartmentsController
        [HttpGet]
        public async Task<IActionResult> Index(Guid groupId)
        {
            var models = await _service.DepartmentService.GetDepartmentsAsync(groupId, trackChanges: false);
            return View(models);
        }

        // GET: DepartmentsController/Details/5
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(Guid groupId, Guid id)
        {
            var model = await _service.DepartmentService.GetDepartmentAsync(groupId, id, trackChanges: false);
            return View(model);
        }

        // GET: DepartmentsController/Create
        [HttpGet("[action]")]
        public ActionResult Create(Guid groupId)
        {
            return View();
        }

        // POST: DepartmentsController/Create
        [HttpPost("[action]")]
        public async Task<ActionResult> Create(Guid groupId, DepartmentForCreationViewModel departmentForCreation)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    await _service.DepartmentService.CreateDepartmentForGroupAsync(groupId, departmentForCreation, trackChanges: false);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        // GET: DepartmentsController/Edit/5
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(Guid groupId, Guid id)
        {
            var model = await _service.DepartmentService.GetDepartmentAsync(groupId, id, trackChanges: false);
            var modelToUpdate = new DepartmentForUpdateViewModel
            {
                Name = model.Name,
            };
            return View(modelToUpdate);
        }

        // POST: DepartmentsController/Edit/5
        [HttpPost("[action]/{id}")]
        public async Task<ActionResult> Edit(Guid groupId, Guid id, DepartmentForUpdateViewModel departmentForUpdate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _service.DepartmentService.UpdateDepartmentForGroupAsync(groupId, id, departmentForUpdate, groupTrackChanges: false, departmentTrackChanges: true);
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

        // GET: DepartmentsController/Delete/5
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid groupId, Guid id)
        {
            var model = await _service.DepartmentService.GetDepartmentAsync(groupId, id, trackChanges: false);
            return View(model);
        }

        // POST: DepartmentsController/Delete/5
        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> Delete(Guid groupId, Guid id, DepartmentViewModel model)
        {
            try
            {
                await _service.DepartmentService.DeleteDepartmentForGroupAsync(groupId, id, trackChanges: false);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
