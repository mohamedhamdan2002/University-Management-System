using EMS.Service.ViewModels.Role;
using EMS.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EMS.Presentation.Controllers
{
    public class RoleController : Controller
    {
        private readonly IServiceManager _service;
        public RoleController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Roles() 
        {
            var roles = await _service.AuthService.GetRolesAsync();
            return View(roles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleForCreationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.AuthService.CreateRoleAsync(model);
                if (result.Succeeded)                
                    return RedirectToAction(nameof(Roles));

                foreach (var erro in result.Errors)
                    ModelState.AddModelError(string.Empty, erro.Description);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var model = await _service.AuthService.GetRoleForUpdateAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleFroUpdateViewModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await _service.AuthService.UpdateRoleAsync(model);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Roles));
     
                foreach (var erro in result.Errors)
                    ModelState.AddModelError(string.Empty, erro.Description);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.AuthService.DeleteRoleAsync(id);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(nameof(Roles));
        }

        [HttpGet]
        public async Task<IActionResult> ManageRoleUsers(Guid roleId)
        {
            var model = await _service.AuthService.GetRoleUsersAsync(roleId);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManageRoleUsers(Guid roleId, List<RoleUserViewModel> model)
        {
            await _service.AuthService.ManageRoleUsers(roleId, model);
            return RedirectToAction(nameof(Edit), new { id = roleId });
        }
    }
}
