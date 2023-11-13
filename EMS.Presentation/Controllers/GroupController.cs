using EMS.Services.Contracts;
using EMS.Service.ViewModels.Group;
using Microsoft.AspNetCore.Mvc;

namespace EMS.Presentation.Controllers
{
    [Route("Faculties/{facultyId:guid}/Groups")]
    public class GroupController : Controller
    {
        private readonly IServiceManager _service;
        public GroupController(IServiceManager service)
        {
            _service = service;
        }
        // GET: GroupsController
        [HttpGet]
        public async Task<IActionResult> Index(Guid facultyId)
        {
            var models = await _service.GroupService.GetGroupsAsync(facultyId, trackChanges: false);
            return View(models);
        }

        // GET: GroupsController/Details/5
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(Guid facultyId, Guid id)
        {
            var model = await _service.GroupService.GetGroupAsync(facultyId, id, trackChanges: false);
            return View(model);
        }

        // GET: GroupsController/Create
        [HttpGet("[action]")]
        public ActionResult Create(Guid FacultyId)
        {
            return View();
        }

        // POST: GroupsController/Create
        [HttpPost("[action]")]
        public async Task<ActionResult> Create(Guid facultyId, GroupForCreationViewModel GroupForCreation)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    await _service.GroupService.CreateGroupForFacultyAsync(facultyId, GroupForCreation, trackChanges: false);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        // GET: GroupsController/Edit/5
        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(Guid facultyId, Guid id)
        {
            var model = await _service.GroupService.GetGroupAsync(facultyId, id, trackChanges: false);
            var modelToUpdate = new GroupForUpdateViewModel
            {
                Name = model.Name,
            };
            return View(modelToUpdate);
        }

        // POST: GroupsController/Edit/5
        [HttpPost("[action]/{id}")]
        public async Task<ActionResult> Edit(Guid facultyId, Guid id, GroupForUpdateViewModel groupForUpdate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _service.GroupService.UpdateGroupForFacultyAsync(facultyId, id, groupForUpdate, facultyTrackChanges: false, groupTrackChanges: true);
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

        // GET: GroupsController/Delete/5
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid facultyId, Guid id)
        {
            var model = await _service.GroupService.GetGroupAsync(facultyId, id, trackChanges: false);
            return View(model);
        }

        // POST: GroupsController/Delete/5
        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> Delete(Guid facultyId, Guid id, GroupViewModel model)
        {
            try
            {
                await _service.GroupService.DeleteGroupForFacultyAsync(facultyId, id, trackChanges: false);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
