using EMS.DataAccess.Entities.Models;
using EMS.Service.ViewModels.User;
using EMS.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace EMS.Presentation.Controllers
{
    public class UserController : Controller
    {
        private readonly IServiceManager _service;
        public UserController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Users()
        {
            var users = await _service.AuthService.GetUsersAsync();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var user = await _service.AuthService.GetRoleForUpdateAsync(id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserForUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.AuthService.UpdateUserAsync(model);

                if (result.Succeeded)
                    return RedirectToAction(nameof(Users));

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
            
        }
        
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.AuthService.DeleteUserAsync(id);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(nameof(Users));
        }

        [HttpGet]
        public async Task<IActionResult> ManageUserRoles(Guid id)
        {
            ViewBag.userId = id;
            var model = await _service.AuthService.GetUserRolesAsync(id);
            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> ManageUserRoles(Guid userId, List<UserRoleViewModel> model)
        {
            await _service.AuthService.ManageUserRoles(userId, model);  
            return RedirectToAction(nameof(Edit), new { id = userId });
        }

        [HttpGet]
        public async Task<IActionResult> ManageUserClaims(Guid userId)
        {
            var model = await _service.AuthService.GetUserClaimsAsync(userId);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManageUserClaims(UserClaimsViewModel model)
        {
            await _service.AuthService.ManageUserClaimsAsync(model);
            return RedirectToAction(nameof(Edit), new { id = model.UserId });
        }          

    }
}
