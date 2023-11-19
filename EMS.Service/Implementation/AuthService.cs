using EMS.DataAccess.Entities.Exceptions;
using EMS.DataAccess.Entities.Models;
using EMS.Service.Interfaces;
using EMS.Service.ViewModels.Role;
using EMS.Service.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Service.Implementation
{
    internal sealed class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManger;
        private readonly SignInManager<User> _signInManger;
        private readonly RoleManager<IdentityRole<Guid>> _roleManger;
        public AuthService(UserManager<User> userManger, SignInManager<User> signInManger, RoleManager<IdentityRole<Guid>> roleManger)
        {
            _userManger = userManger;
            _signInManger = signInManger;
            _roleManger = roleManger;
        }
        public async Task<IdentityResult> CreateRoleAsync(RoleForCreationViewModel model)
        {
            var roleDb = new IdentityRole<Guid>()
            {
                Name = model.RoleName
            };
            var result = await _roleManger.CreateAsync(roleDb);
            return result;
        }
        public async Task<IdentityResult> DeleteRoleAsync(Guid roleId)
        {
            var roleFromDb = await GetRoleAndCheckIfItExist(roleId);
            var result = await _roleManger.DeleteAsync(roleFromDb);
            return result;
        }
        public async Task<RoleViewModel> GetRoleByIdAsync(Guid roleId)
        {
            var roleDb = await GetRoleAndCheckIfItExist(roleId);
            var roleToReturn = new RoleViewModel(roleDb.Id, roleDb.Name!);
            return roleToReturn;
        }
        public async Task<RoleFroUpdateViewModel> GetRoleForUpdateAsync(Guid roleId)
        {
            var roleFromDb = await GetRoleAndCheckIfItExist(roleId);
            var roleUsers = await _userManger.GetUsersInRoleAsync(roleFromDb.Name!);
            var roleToReturn = new RoleFroUpdateViewModel
            {
                id = roleFromDb.Id,
                RoleName = roleFromDb.Name,
                Users = roleUsers.Select(u => u.UserName).ToList(),
            };
            return roleToReturn;

        }
        public async Task<IEnumerable<RoleViewModel>> GetRolesAsync()
        {
            var rolesFromDb = await _roleManger.Roles.ToListAsync();
            var rolesToReturn = rolesFromDb.Select(r => new RoleViewModel(r.Id, r.Name!)).ToList();
            return rolesToReturn;

        }
        public async Task<IEnumerable<UserViewModel>> GetUsersAsync()
        {
            var usersFromDb = await _userManger.Users.ToListAsync();
            var usersToReturn = usersFromDb.Select(u => new UserViewModel(u.Id, u.UserName!, u.Email!)).ToList();
            return usersToReturn;
        }
        public async Task<bool> LogInAsync(LoginViewModel userForAuth)
        {
            var user = await _userManger.FindByEmailAsync(userForAuth.Email!);
            if(user == null)
            {
                throw new InvalidOperationException();
            }
            var result = await _signInManger.PasswordSignInAsync(user, userForAuth.Password, userForAuth.RememberMe, false);
            if(result.Succeeded)
                return true;
            return false;
        }
        public async Task LogOutAsync() => await _signInManger.SignOutAsync();
        public async Task<IdentityResult> RegisterAsync(RegisterViewModel model)
        {
            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
            };
            var result = await _userManger.CreateAsync(user, model.Password!);
            return result;
        }
        public async Task<IdentityResult> UpdateRoleAsync(RoleFroUpdateViewModel model)
        {
            var roleFromDb = await GetRoleAndCheckIfItExist(model.id);
            roleFromDb.Name = model.RoleName;
            var result = await _roleManger.UpdateAsync(roleFromDb);
            return result;
        }

        public async Task<UserForUpdateViewModel> GetUserForUpdateAsync(Guid userId)
        {
            var userFromDb = await GetUserAndCheckIfItExist(userId);
            var userRoles = await _userManger.GetRolesAsync(userFromDb);
            var userClaims = await _userManger.GetClaimsAsync(userFromDb);
            var userToReturn = new UserForUpdateViewModel
            {
                UserId = userFromDb.Id,
                UserName = userFromDb.UserName,
                Email = userFromDb.Email,
                Roles = userRoles.ToList(),
                Claims = userClaims.Select(c => c.Value).ToList(),
            };
            return userToReturn;
        }
        public async Task<IdentityResult> UpdateUserAsync(UserForUpdateViewModel model)
        {
            var userFromDb = await GetUserAndCheckIfItExist(model.UserId);
            userFromDb.UserName = model.UserName;
            userFromDb.Email = model.Email;
            var result = await _userManger.UpdateAsync(userFromDb);
            return result;
        }

        private async Task<IdentityRole<Guid>> GetRoleAndCheckIfItExist(Guid roleId)
        {
            var roleFromDb = await _roleManger.FindByIdAsync(roleId.ToString());
            if (roleFromDb is null)
                throw new NotFoundException($"the Role With Id : {roleId} doesn't exist");
            return roleFromDb;
        }
        private async Task<User> GetUserAndCheckIfItExist(Guid userId)
        {
            var userFromDb = await _userManger.FindByIdAsync(userId.ToString());
            if (userFromDb is null)
                throw new NotFoundException($"the User With Id : {userId} doesn't exist");
            return userFromDb;
        }

        public async Task<IdentityResult> DeleteUserAsync(Guid userId)
        {
            var userFromDb = await GetUserAndCheckIfItExist(userId);
            var result = await _userManger.DeleteAsync(userFromDb);
            return result;
        }
        
        public async Task ManageRoleUsers(Guid roleId, IEnumerable<RoleUserViewModel> roleUsers)
        {
            var roleFromDb = await GetRoleAndCheckIfItExist(roleId);
            foreach(var roleUser in roleUsers)
            {
                var userFromDb = await GetUserAndCheckIfItExist(roleUser.UserId);
                var isUserInRole = (await _userManger.IsInRoleAsync(userFromDb, roleFromDb.Name!));
                
                if (roleUser.IsSelected && !(isUserInRole))
                     await _userManger.AddToRoleAsync(userFromDb, roleFromDb.Name!);
                else if (!roleUser.IsSelected && (isUserInRole))
                     await _userManger.RemoveFromRoleAsync(userFromDb, roleFromDb.Name!);
            }
        }

        public async Task<IEnumerable<RoleUserViewModel>> GetRoleUsersAsync(Guid roleId)
        {
            var roleFromDb = await GetRoleAndCheckIfItExist(roleId);
            List<RoleUserViewModel> roleUsers = new();
            foreach (var user in await _userManger.Users.ToListAsync())
            {
                var roleUser = new RoleUserViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                };
                  
                if(await _userManger.IsInRoleAsync(user, roleFromDb.Name!))
                    roleUser.IsSelected = true;
                roleUsers.Add(roleUser);
            }
            return roleUsers;
        }

        public async Task ManageUserRoles(Guid userId, IEnumerable<UserRoleViewModel> userRoles)
        {
            var userFromDb = await GetUserAndCheckIfItExist(userId);
            var roles = await _userManger.GetRolesAsync(userFromDb);
            await _userManger.RemoveFromRolesAsync(userFromDb, roles);
            var newRoles = userRoles.Where(x => x.IsSelected).Select(x => x.RoleName);
            await _userManger.AddToRolesAsync(userFromDb, newRoles!);
        }

        public async Task<IEnumerable<UserRoleViewModel>> GetUserRolesAsync(Guid userId)
        {
            var userFromDb = await GetUserAndCheckIfItExist(userId);
            List<UserRoleViewModel> userRoles = new();
            foreach (var role in await _roleManger.Roles.ToListAsync())
            {
                var userRole = new UserRoleViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                };

                if (await _userManger.IsInRoleAsync(userFromDb, role.Name!))
                    userRole.IsSelected = true;

                userRoles.Add(userRole);
            }
            return userRoles;
        }

        public async Task<UserClaimsViewModel> GetUserClaimsAsync(Guid userId)
        {
            var userFromDb = await GetUserAndCheckIfItExist(userId);
            var existingUserClaims = await _userManger.GetClaimsAsync(userFromDb);
            var userClaimsToReturn = new UserClaimsViewModel
            {
                UserId = userFromDb.Id,
            };
            foreach (var claim in ClaimStore.AllClaims)
            {
                var userclaim = new UserClaim
                {
                    ClaimType = claim.Type,
                };
                if(existingUserClaims.Any(c => c.Type == claim.Type))
                    userclaim.IsSelected = true;
                userClaimsToReturn.Claims.Add(userclaim);
            }
            return userClaimsToReturn;

        }

        public async Task ManageUserClaimsAsync(UserClaimsViewModel model)
        {
            var userFromDb = await GetUserAndCheckIfItExist(model.UserId);
            var userClaims = await _userManger.GetClaimsAsync(userFromDb);
            await _userManger.RemoveClaimsAsync(userFromDb, userClaims);
            var newClaims = model.Claims.Where(c => c.IsSelected).Select(c => new Claim(c.ClaimType!, c.ClaimType!));
            await _userManger.AddClaimsAsync(userFromDb, newClaims);
        }
    }
}
