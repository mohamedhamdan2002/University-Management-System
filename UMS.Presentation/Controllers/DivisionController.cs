using UMS.Services.Contracts;
using UMS.Service.ViewModels.Division;
using Microsoft.AspNetCore.Mvc;
using UMS.Presentation.Utilities;

namespace UMS.Presentation.Controllers
{
    public class DivisionController : Controller
    {
        private readonly IServiceManager _service;
        public DivisionController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index(Guid departmentId)
        {
            var models = await _service.DivisionService.GetDivisionsAsync(departmentId, trackChanges: false);
            ViewBag.departmentId = departmentId;
            return View(models);
        }


        [HttpGet]
        public async Task<IActionResult> Details(Guid departmentId, Guid id)
        {
            var model = await _service.DivisionService.GetDivisionAsync(departmentId, id, trackChanges: false, new[] { NavigationProperties.Courses, NavigationProperties.Students });
            ViewBag.departmentId = departmentId;
            return View(model);
        }


        [HttpGet]
        public ActionResult Create(Guid departmentId)
        {
            ViewBag.departmentId = departmentId;
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(Guid departmentId, DivisionForCreationViewModel divisionForCreation)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    await _service.DivisionService.CreateDivisionForDepartmentAsync(departmentId, divisionForCreation, trackChanges: false);
                    return RedirectToAction(nameof(Index), new { departmentId });
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid departmentId, Guid id)
        {
            var model = await _service.DivisionService.GetDivisionAsync(departmentId, id, trackChanges: false);
            ViewBag.departmentId = departmentId;
            var modelToUpdate = new DivisionForUpdateViewModel
            {
                Name = model.Name,
            };
            return View(modelToUpdate);
        }


        [HttpPost]
        public async Task<ActionResult> Edit(Guid departmentId, Guid id, DivisionForUpdateViewModel divisionForUpdate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _service.DivisionService.UpdateDivisionForDepartmentAsync(departmentId, id, divisionForUpdate, departmentTrackChanges: false, divisionTrackChanges: true);
                    return RedirectToAction(nameof(Index), new { departmentId });
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
        public async Task<IActionResult> Delete(Guid departmentId, Guid id)
        {
            var model = await _service.DivisionService.GetDivisionAsync(departmentId, id, trackChanges: false);
            ViewBag.departmentId = departmentId;
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Guid departmentId, Guid id, DivisionViewModel model)
        {
            try
            {
                await _service.DivisionService.DeleteDivisionForDepartmentAsync(departmentId, id, trackChanges: false);
                return RedirectToAction(nameof(Index), new { departmentId });
            }
            catch
            {
                return View();
            }

        }
    }
}
