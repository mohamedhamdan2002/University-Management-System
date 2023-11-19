using UMS.Services.Contracts;
using UMS.Service.ViewModels.Department;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using UMS.Presentation.Utilities;

namespace UMS.Presentation.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IServiceManager _service;
        public DepartmentController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index(Guid groupId)
        {
            var models = await _service.DepartmentService.GetDepartmentsAsync(groupId, trackChanges: false);
            ViewBag.groupId = groupId;
            return View(models);
        }


        [HttpGet]
        public async Task<IActionResult> Details(Guid groupId, Guid id)
        {
            var model = await _service.DepartmentService.GetDepartmentAsync(groupId, id, trackChanges: false, new[] { NavigationProperties.DepartmentDivisions_Division });
            ViewBag.groupId = groupId;
            return View(model);
        }

        
        [HttpGet]
        public ActionResult Create(Guid groupId)
        {
            ViewBag.groupId = groupId;
            return View();
        }

        
        [HttpPost]
        public async Task<ActionResult> Create(Guid groupId, DepartmentForCreationViewModel departmentForCreation)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    await _service.DepartmentService.CreateDepartmentForGroupAsync(groupId, departmentForCreation, trackChanges: false);
                    ViewBag.groupId = groupId;
                    return RedirectToAction(nameof(Index), new { groupId });
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
            var model = await _service.DepartmentService.GetDepartmentAsync(groupId, id, trackChanges: false);
            ViewBag.groupId = groupId;
            var modelToUpdate = new DepartmentForUpdateViewModel
            {
                Name = model.Name,
            };
            return View(modelToUpdate);
        }


        [HttpPost]
        public async Task<ActionResult> Edit(Guid groupId, Guid id, DepartmentForUpdateViewModel departmentForUpdate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _service.DepartmentService.UpdateDepartmentForGroupAsync(groupId, id, departmentForUpdate, groupTrackChanges: false, departmentTrackChanges: true);
                    return RedirectToAction(nameof(Index), new { groupId });
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
            var model = await _service.DepartmentService.GetDepartmentAsync(groupId, id, trackChanges: false);
            ViewBag.groupId = groupId;
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Guid groupId, Guid id, DepartmentViewModel model)
        {
            try
            {
                await _service.DepartmentService.DeleteDepartmentForGroupAsync(groupId, id, trackChanges: false);
                return RedirectToAction(nameof(Index), new { groupId });
            }
            catch
            {
                return View();
            }
        }
    }
}
