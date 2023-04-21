using API.Dtos;
using API.Helpers.Errors;
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
        var existingEmail = await _userManager.FindByEmailAsync(addUserDto.Email);
        if (existingEmail != null)
        {
            return BadRequest(new ApiResponse(400, new string[] { "The email already exist." }));
        }

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

        if(result.Errors.Any())
        {
            string[] errorMessages = result.Errors.Select(e => e.Description).ToArray();

            return BadRequest(new ApiResponse(400, errorMessages));
        }

        return CreatedAtAction(nameof(AddUser), new
        {
            id = newUser.Id
        });
    }
}
