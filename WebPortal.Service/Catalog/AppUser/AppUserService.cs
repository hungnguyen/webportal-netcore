using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebPortal.Data.EF;
using WebPortal.Data.Entities;
using WebPortal.Services.Common;
using WebPortal.Services.Converters;
using WebPortal.ViewModels;
using WebPortal.ViewModels.Common;

namespace WebPortal.Services
{
    public class AppUserService : IAppUserService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;        
        private readonly IConfiguration config;
        private readonly IMapper mapper;
        private readonly IToolService toolService;
        public AppUserService(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,            
            IConfiguration config,
            IToolService toolService,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;            
            this.config = config;
            this.mapper = mapper;
            this.toolService = toolService;
        }
        public async Task<List<AppUser>> GetAll()
        {
            return await userManager.Users.ToListAsync();
        }
        public async Task<ApiResult<string>> GenerateAccessToken(UserLoginRequest request)
        {
            var user = await GetByName(request.UserName);
            if (user == null) return new ApiErrorResult<string>("User is not existing");

            var result = await signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                {
                    return new ApiErrorResult<string>("User account locked out.");
                }
                else if (result.IsNotAllowed)
                {
                    return new ApiErrorResult<string>("User account is not allowed.");
                }
                else
                {
                    return new ApiErrorResult<string>("Username or password is incorrect.");
                }
            }
            var roles = await GetRoles(user);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.FullName),
                new Claim(ClaimTypes.Role, string.Join(";",roles)),
                new Claim(ClaimTypes.Name, request.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(config["Tokens:Issuer"],
                config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            return new ApiSuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
        }

        public async Task<IdentityResult> ChangePassword(AppUser user, string password)
        {
            var token = await GenerateResetPasswordToken(user);
            return await ResetPassword(user, token, password);
        }

        public async Task<AppIdentityResult<AppUser>> Create(AppUserRequest request)
        {
            var user = mapper.Map<AppUser>(request);
            var result = await userManager.CreateAsync(user, request.NewPassword);
            return new AppIdentityResult<AppUser>()
            {
                Result = result,
                ReturnObj = user
            };
        }

        public async Task<AppIdentityResult<AppUser>> Delete(object id)
        {
            var user = await GetById(id);

            var roles = await GetRoles(user);

            await RemoveFromRoles(user, roles);

            var result = await userManager.DeleteAsync(user);

            return new AppIdentityResult<AppUser>()
            {
                Result = result,
                ReturnObj = user
            };
        }

        public async Task<AppUser> GetById(object id)
        {
            return await userManager.FindByIdAsync(TConverter.ChangeType<string>(id));
        }

        public async Task<PagedResult<AppUserView>> GetPaging(AppUserSearchRequest request)
        {
            IQueryable<AppUser> query = userManager.Users;
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(b => b.FullName.Contains(request.Keyword) || b.UserName.Contains(request.Keyword) || b.Email.Contains(request.Keyword));
            }

            query = query.OrderByDescending(b => b.UserName);

            var total = await query.CountAsync();

            if (total > request.PageSize && request.PageSize > 0)
            {
                query = query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize);
            }

            var data = await mapper.ProjectTo<AppUserView>(query)
                    .ToListAsync();

            var result = new PagedResult<AppUserView>()
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                TotalRow = total,
                Items = data
            };
            return result;
        }

        public async Task<IdentityResult> RoleAssign(AppUser user, List<string> roles)
        {
            var oldRoles = await GetRoles(user);
            var result = IdentityResult.Success;
            if (oldRoles.Count > 0)
            {
                //remove role
                foreach (var r in oldRoles)
                {
                    if (!roles.Contains(r))
                    {
                        result = await userManager.RemoveFromRoleAsync(user, r);
                        if (!result.Succeeded) return result;
                    }
                }
            }

            //add new role
            foreach (var r in roles)
            {
                if (!oldRoles.Contains(r))
                {
                    result = await userManager.AddToRoleAsync(user, r);
                    if (!result.Succeeded) return result;
                }
            }
            return result;
        }

        public async Task<AppIdentityResult<AppUser>> UpdateById(object id, AppUserRequest request)
        {
            var user = await GetById(id);
            mapper.Map(request, user);
            var result = await userManager.UpdateAsync(user);
            return new AppIdentityResult<AppUser>()
            {
                Result = result,
                ReturnObj = user
            };
        }
        public async Task<AppIdentityResult<AppUser>> UpdateByName(string name, AppUserRequest request)
        {
            var user = await GetByName(name);
            mapper.Map(request, user);
            var result = await userManager.UpdateAsync(user);
            return new AppIdentityResult<AppUser>()
            {
                Result = result,
                ReturnObj = user
            };
        }

        public async Task<AppSignInResult<AppUser>> SignIn(UserLoginRequest request)
        {
            var user = await GetByName(request.UserName);

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim("Image", user.Image ?? "noavatar.png"),
                    new Claim("FullName", user.FullName ?? user.UserName)
                };
                await UpsertClaim(user, claims);
            }

            var result = await signInManager.PasswordSignInAsync(request.UserName, request.Password, request.RememberMe, true);

            return new AppSignInResult<AppUser>()
            {
                Result=result,
                ReturnObj=user
            };
        }

        public async Task UpsertClaim(AppUser user, List<Claim> claims)
        {
            foreach (var c in claims)
            {
                var userClaims = await userManager.GetClaimsAsync(user);
                var claim = userClaims.FirstOrDefault(uc => uc.Type == c.Type);
                if (claim != null)
                    await userManager.ReplaceClaimAsync(user, claim, c);
                else
                    await userManager.AddClaimAsync(user, c);
            }
        }

        public async Task<AppUser> GetByName(string name)
        {
            return await userManager.FindByNameAsync(name);
        }

        public async Task<AppUser> GetByEmail(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        public async Task<bool> CheckPassword(AppUser user, string password)
        {
            return await userManager.CheckPasswordAsync(user, password);
        }

        public async Task<string> GenerateResetPasswordToken(AppUser user)
        {
            return await userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> ResetPassword(AppUser user, string token, string password)
        {
            return await userManager.ResetPasswordAsync(user, token, password);
        }

        public async Task SignOut()
        {
            await signInManager.SignOutAsync(); 
        }

        public async Task<IList<string>> GetRoles(AppUser user)
        {
            return await userManager.GetRolesAsync(user);
        }

        public async Task<IdentityResult> RemoveFromRoles(AppUser user, IEnumerable<string> roles)
        {
            return await userManager.RemoveFromRolesAsync(user, roles);
        }

        public async Task<IdentityResult> Lock(AppUser user)
        {
            var result = await userManager.SetLockoutEnabledAsync(user, true);
            if (!result.Succeeded) return result;

            result = await userManager.SetLockoutEndDateAsync(user, DateTimeOffset.Now.AddMinutes(5));
            return result;
        }

        public async Task<IdentityResult> Unlock(AppUser user)
        {
            return await userManager.SetLockoutEndDateAsync(user, user.LockoutEnd.Value.AddMinutes(-5));
        }
    }
}
