using EMS.DataAccess.Entities.Models;
using EMS.Service.ViewModels.Role;
using EMS.Service.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Service.Interfaces
{
    public interface IAuthService
    {
        Task<bool> LogInAsync(LoginViewModel model);

        Task<IdentityResult> RegisterAsync(RegisterViewModel model);
        Task LogOutAsync();
        Task<IEnumerable<UserViewModel>> GetUsersAsync();
        Task<IEnumerable<RoleViewModel>> GetRolesAsync();
        Task<RoleViewModel> GetRoleByIdAsync(Guid roleId);
        Task<RoleFroUpdateViewModel> GetRoleForUpdateAsync(Guid roleId);
        Task<IdentityResult> CreateRoleAsync(RoleForCreationViewModel model);
        Task<IdentityResult> UpdateRoleAsync(RoleFroUpdateViewModel model);
        Task<IdentityResult> DeleteRoleAsync(Guid roleId);
        Task<UserForUpdateViewModel> GetUserForUpdateAsync(Guid userId);
        Task<IdentityResult> UpdateUserAsync(UserForUpdateViewModel model);
        Task<IdentityResult> DeleteUserAsync(Guid userId);
        Task ManageRoleUsers(Guid roleId, IEnumerable<RoleUserViewModel> roleUsers);
        Task<IEnumerable<RoleUserViewModel>> GetRoleUsersAsync(Guid roleId);

        Task ManageUserRoles(Guid userId, IEnumerable<UserRoleViewModel> userRoles);
        Task<IEnumerable<UserRoleViewModel>> GetUserRolesAsync(Guid userId);
        Task<UserClaimsViewModel> GetUserClaimsAsync(Guid userId);
        Task ManageUserClaimsAsync(UserClaimsViewModel model);

    }
}
