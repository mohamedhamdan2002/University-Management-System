using EMS.Services.Contracts;
using EMS.Service.ViewModels.University;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EMS.Presentation.Controllers
{
    public class UniversityController : Controller
    {
        private readonly IServiceManager _service;
        public UniversityController(IServiceManager service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var models = await _service.UniversityService.GetUniversitiesAsync(trackChanges: false); 
            return View(models);
        }


        public async Task<IActionResult> Details(Guid id)
        {
            var model = await _service.UniversityService.GetUniversityAsync(id, trackChanges: false, new[] { "Faculties" });
            return View(model);
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Create(UniversityForCreationViewModel universityForCreation)
        {
            if(ModelState.IsValid)
            {
                try
                {

                    await _service.UniversityService.CreateUniversityAsync(universityForCreation);
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
            try
            {
                var model = await _service.UniversityService.GetUniversityAsync(id, trackChanges: false);
                var modelToUpdate = new UniversityForUpdateViewModel
                {
                    Name = model.Name,
                    Location = model.Location,
                };
                return View(modelToUpdate);
            }
            catch (Exception)
            {

            }
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Edit(Guid id, UniversityForUpdateViewModel universityForUpdate)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    await _service.UniversityService.UpdateUniversityAsync(id, universityForUpdate, trackChanges: true);
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch(Exception ex)
            {
                ModelState.TryAddModelError(string.Empty, ex.Message);
                return View(ModelState);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var model = await _service.UniversityService.GetUniversityAsync(id, trackChanges: false);
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Guid id, UniversityViewModel model)
        {
            try
            {
                await _service.UniversityService.DeleteUniversityAsync(id, trackChanges: false);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
