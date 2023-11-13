using EMS.Services.Contracts;
using EMS.Service.ViewModels.Staff;
using Microsoft.AspNetCore.Mvc;

namespace EMS.Presentation.Controllers
{
    public class StaffController : Controller
    {
        private readonly IServiceManager _service;
        public StaffController(IServiceManager service)
        {
            _service = service;
        }
        // GET: StaffsController
        public async Task<IActionResult> Index()
        {
            var models = await _service.StaffService.GetStaffsAsync(trackChanges: false);
            return View(models);
        }

        // GET: StaffsController/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var model = await _service.StaffService.GetStaffAsync(id, trackChanges: false);
            return View(model);
        }

        // GET: StaffsController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: StaffsController/Create
        [HttpPost]
        public async Task<ActionResult> Create(StaffForCreationViewModel StaffForCreation)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    await _service.StaffService.CreateStaffAsync(StaffForCreation);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        // GET: StaffsController/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var model = await _service.StaffService.GetStaffAsync(id, trackChanges: false);
            var modelToUpdate = new StaffForUpdateViewModel
            {
                FirstName = model.FullName.Split(' ')[0],
                LastName = model.FullName.Split(' ')[1],
                NationalID = model.NationalID,
            };
            return View(modelToUpdate);
        }

        // POST: StaffsController/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Guid id, StaffForUpdateViewModel staffForUpdate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _service.StaffService.UpdateStaffAsync(id, staffForUpdate, trackChanges: true);
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

        // GET: StaffsController/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var model = await _service.StaffService.GetStaffAsync(id, trackChanges: false);
            return View(model);
        }

        // POST: StaffsController/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id, StaffViewModel model)
        {
            try
            {
                await _service.StaffService.DeleteStaffAsync(id, trackChanges: false);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
