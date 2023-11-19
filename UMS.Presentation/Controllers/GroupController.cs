using UMS.Services.Contracts;
using UMS.Service.ViewModels.Group;
using Microsoft.AspNetCore.Mvc;
using UMS.DataAccess.Entities.Models;
using UMS.Presentation.Utilities;

namespace UMS.Presentation.Controllers
{
    public class GroupController : Controller
    {
        private readonly IServiceManager _service;
        public GroupController(IServiceManager service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index(Guid facultyId)
        {
            var models = await _service.GroupService.GetGroupsAsync(facultyId, trackChanges: false);
            ViewBag.facultyId = facultyId;
            return View(models);
        }

      
        [HttpGet]
        public async Task<IActionResult> Details(Guid facultyId, Guid id)
        {
            var model = await _service.GroupService.GetGroupAsync(facultyId, id, trackChanges: false, new[] { NavigationProperties.Departments, NavigationProperties.Students });
            ViewBag.facultyId = facultyId;
            return View(model);
        }

      
        [HttpGet]
        public ActionResult Create(Guid facultyId)
        {
            ViewBag.facultyId = facultyId;
            return View();
        }

     
        [HttpPost]
        public async Task<ActionResult> Create(Guid facultyId, GroupForCreationViewModel GroupForCreation)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    await _service.GroupService.CreateGroupForFacultyAsync(facultyId, GroupForCreation, trackChanges: false);
                    return RedirectToAction(nameof(Index), new { facultyId });
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid facultyId, Guid id)
        {
            var model = await _service.GroupService.GetGroupAsync(facultyId, id, trackChanges: false);
            ViewBag.facultyId = facultyId;
            var modelToUpdate = new GroupForUpdateViewModel
            {
                Name = model.Name,
                Scientific = model.Scientific,
            };
            return View(modelToUpdate);
        }


        [HttpPost]
        public async Task<ActionResult> Edit(Guid facultyId, Guid id, GroupForUpdateViewModel groupForUpdate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _service.GroupService.UpdateGroupForFacultyAsync(facultyId, id, groupForUpdate, facultyTrackChanges: false, groupTrackChanges: true);
                    return RedirectToAction(nameof(Index), new { facultyId });
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
        public async Task<IActionResult> Delete(Guid facultyId, Guid id)
        {
            var model = await _service.GroupService.GetGroupAsync(facultyId, id, trackChanges: false);
            ViewBag.facultyId = facultyId;
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Guid facultyId, Guid id, GroupViewModel model)
        {
            try
            {
                await _service.GroupService.DeleteGroupForFacultyAsync(facultyId, id, trackChanges: false);
                return RedirectToAction(nameof(Index), new { facultyId });
            }
            catch
            {
                return View();
            }
        }
    }
}
