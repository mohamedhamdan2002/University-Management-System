using UMS.Services.Contracts;
using UMS.Service.ViewModels.Staff;
using Microsoft.AspNetCore.Mvc;

namespace UMS.Presentation.Controllers
{
    public class StaffController : Controller
    {
        private readonly IServiceManager _service;
        public StaffController(IServiceManager service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var models = await _service.StaffService.GetStaffsAsync(trackChanges: false);
            return View(models);
        }


        public async Task<IActionResult> Details(Guid id)
        {
            var model = await _service.StaffService.GetStaffAsync(id, trackChanges: false);
            return View(model);
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


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

        
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var model = await _service.StaffService.GetStaffAsync(id, trackChanges: false);
            return View(model);
        }

        
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
