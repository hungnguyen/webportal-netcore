using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebPortal.Data.Entities;
using WebPortal.ViewModels;
using WebPortal.ViewModels.Common;

namespace WebPortal.Services
{
    public interface IAppUserService
    {
        Task<List<AppUser>> GetAll();
        Task<PagedResult<AppUserView>> GetPaging(AppUserSearchRequest request);
        
        Task<ApiResult<string>> GenerateAccessToken(UserLoginRequest request);

        Task<AppSignInResult<AppUser>> SignIn(UserLoginRequest request);

        Task<AppIdentityResult<AppUser>> Create(AppUserRequest request);

        Task<AppIdentityResult<AppUser>> UpdateById(object id, AppUserRequest request);
        Task<AppIdentityResult<AppUser>> UpdateByName(string name, AppUserRequest request);

        Task UpsertClaim(AppUser user, List<Claim> claims);

        Task<AppUser> GetById(object id);
        Task<AppUser> GetByName(string name);
        Task<AppUser> GetByEmail(string email);

        Task<AppIdentityResult<AppUser>> Delete(object id);

        Task<IdentityResult> RoleAssign(AppUser user, List<string> roles);
        Task<IdentityResult> ChangePassword(AppUser user, string password);
        Task<bool> CheckPassword(AppUser user, string password);
        Task<string> GenerateResetPasswordToken(AppUser user);
        Task<IdentityResult> ResetPassword(AppUser user, string token, string password);
        Task SignOut();
        Task<IList<string>> GetRoles(AppUser user);
        Task<IdentityResult> RemoveFromRoles(AppUser user, IEnumerable<string> roles);
        Task<IdentityResult> Lock(AppUser user);
        Task<IdentityResult> Unlock(AppUser user);
    }
}
