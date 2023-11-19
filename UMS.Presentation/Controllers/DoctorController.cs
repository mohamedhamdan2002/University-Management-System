using UMS.Services.Contracts;
using UMS.Service.ViewModels.Doctor;
using Microsoft.AspNetCore.Mvc;
using UMS.Presentation.Utilities;

namespace UMS.Presentation.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IServiceManager _service;
        public DoctorController(IServiceManager service)
        {
            _service = service;
        }
     
        public async Task<IActionResult> Index()
        {
            var models = await _service.DoctorService.GetDoctorsAsync(trackChanges: false);
            return View(models);
        }

        
        public async Task<IActionResult> Details(Guid id)
        {
            var model = await _service.DoctorService.GetDoctorAsync(id, trackChanges: false, new[] { NavigationProperties.Courses });
            return View(model);
        }

        
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(DoctorForCreationViewModel doctorForCreation)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    await _service.DoctorService.CreateDoctorAsync(doctorForCreation);
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
            var model = await _service.DoctorService.GetDoctorAsync(id, trackChanges: false);
            var modelToUpdate = new DoctorForUpdateViewModel
            {
                FirstName = model.FullName.Split(' ')[0],
                LastName = model.FullName.Split(' ')[1],
                NationalID = model.NationalID,
            };
            return View(modelToUpdate);
        }


        [HttpPost]
        public async Task<ActionResult> Edit(Guid id, DoctorForUpdateViewModel doctorForUpdate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _service.DoctorService.UpdateDoctorAsync(id, doctorForUpdate, trackChanges: true);
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
            var model = await _service.DoctorService.GetDoctorAsync(id, trackChanges: false);
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Guid id, DoctorViewModel model)
        {
            try
            {
                await _service.DoctorService.DeleteDoctorAsync(id, trackChanges: false);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
