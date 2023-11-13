using EMS.Services.Contracts;
using EMS.Service.ViewModels.Division;
using Microsoft.AspNetCore.Mvc;

namespace EMS.Presentation.Controllers
{
    [Route("Faculties/{departmentId:guid}/Divisions")]
    public class DivisionController : Controller
    {
        private readonly IServiceManager _service;
        public DivisionController(IServiceManager service)
        {
            _service = service;
        }
        // GET: DivisionsController
        [HttpGet]
        public async Task<IActionResult> Index(Guid departmentId)
        {
            var models = await _service.DivisionService.GetDivisionsAsync(departmentId, trackChanges: false);
            return View(models);
        }

        // GET: DivisionsController/Details/5
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(Guid departmentId, Guid id)
        {
            var model = await _service.DivisionService.GetDivisionAsync(departmentId, id, trackChanges: false);
            return View(model);
        }

        // GET: DivisionsController/Create
        [HttpGet("[action]")]
        public ActionResult Create(Guid departmentId)
        {
            return View();
        }

        // POST: DivisionsController/Create
        [HttpPost("[action]")]
        public async Task<ActionResult> Create(Guid departmentId, DivisionForCreationViewModel divisionForCreation)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    await _service.DivisionService.CreateDivisionForDepartmentAsync(departmentId, divisionForCreation, trackChanges: false);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        // GET: DivisionsController/Edit/5
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(Guid departmentId, Guid id)
        {
            var model = await _service.DivisionService.GetDivisionAsync(departmentId, id, trackChanges: false);
            var modelToUpdate = new DivisionForUpdateViewModel
            {
                Name = model.Name,
            };
            return View(modelToUpdate);
        }

        // POST: DivisionsController/Edit/5
        [HttpPost("[action]/{id}")]
        public async Task<ActionResult> Edit(Guid departmentId, Guid id, DivisionForUpdateViewModel divisionForUpdate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _service.DivisionService.UpdateDivisionForDepartmentAsync(departmentId, id, divisionForUpdate, departmentTrackChanges: false, divisionTrackChanges: true);
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

        // GET: DivisionsController/Delete/5
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid departmentId, Guid id)
        {
            var model = await _service.DivisionService.GetDivisionAsync(departmentId, id, trackChanges: false);
            return View(model);
        }

        // POST: DivisionsController/Delete/5
        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> Delete(Guid departmentId, Guid id, DivisionViewModel model)
        {
            try
            {
                await _service.DivisionService.DeleteDivisionForDepartmentAsync(departmentId, id, trackChanges: false);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }

        }
    }
}
