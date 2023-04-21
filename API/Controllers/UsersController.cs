using API.Dtos;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UsersController : BaseApiController
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public UsersController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> AddUser(AddUserDto addUserDto)
    {
        var newUser = new User
        {
            Email = addUserDto.Email,
            LastName = addUserDto.LastName,
            MotherLastName = addUserDto.MotherLastName,
            Names = addUserDto.Names,
            PhoneNumber = addUserDto.PhoneNumber,
            UserName = addUserDto.UserName
        };
        var result = await _userManager.CreateAsync(newUser, addUserDto.Password);

        return Ok(result);
    }
}
