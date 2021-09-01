using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using AutoMapper;
using Identity_JWT_API.Controllers;
using Identity_JWT_API.DTOs;
using Identity_JWT_API.Extensions.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AdminController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        public AdminController(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("users-with-roles")]
        public async Task<ActionResult> GetUsersWithRoles()
        {
            var users = await _userManager.Users
                .Include(r => r.UserRoles)
                .ThenInclude(r => r.Role)
                .OrderBy(u => u.UserName)
                .Select(u => new
                {
                    u.Id,
                    Username = u.UserName,
                    Roles = u.UserRoles.Select(r => r.Role.Name).ToList()
                })
                .ToListAsync();

            return Ok(users);
        }

        // [HttpPost("register")]
        // public async Task<ActionResult<UserDto>> EditUser(RegisterDto registerDto, IMapper mapper)
        // {
        //     if (await UserExists(registerDto.Username) == false) return BadRequest("Username not found");

        //     var user = _mapper.Map<AppUser>(registerDto);

        //     user.UserName = registerDto.Username.ToLower();

        //     var result = await _userManager.UpdateAsync(user);

        //     if (!result.Succeeded) return BadRequest(result.Errors);

        //     //var roleResult = await _userManager.AddToRoleAsync(user, "Member");

        //     //if (!roleResult.Succeeded) return BadRequest(result.Errors);

        //     return Ok();
        // }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("edit-roles/{username}")]
        public async Task<ActionResult> EditRoles(string username, [FromQuery] string roles)
        {
            var selectedRoles = roles.Split(",").ToArray();

            var user = await _userManager.FindByNameAsync(username);

            if (user == null) return NotFound("Could not find user");

            var userRoles = await _userManager.GetRolesAsync(user);

            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if (!result.Succeeded) return BadRequest("Failed to add to roles");

            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            if (!result.Succeeded) return BadRequest("Failed to remove from roles");

            return Ok(await _userManager.GetRolesAsync(user));
        }

        private async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}