using API.Dtos;
using API.Service;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UsersController : BaseApiController
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> AddUser(AddUserDto addUserDto)
    {
        //var existingEmail = await _userManager.FindByEmailAsync(addUserDto.Email);
        //if (existingEmail != null)
        //{
        //    return BadRequest(new ApiResponse(400, new string[] { "The email already exist." }));
        //}

        //var newUser = new User
        //{
        //    Email = addUserDto.Email,
        //    PaternLastName = addUserDto.PaternLastName,
        //    MotherLastName = addUserDto.MotherLastName,
        //    Names = addUserDto.Names,
        //    PhoneNumber = addUserDto.PhoneNumber,
        //    UserName = addUserDto.UserName
        //};
        //var result = await _userManager.CreateAsync(newUser, addUserDto.Password);

        //if(result.Errors.Any())
        //{
        //    string[] errorMessages = result.Errors.Select(e => e.Description).ToArray();

        //    return BadRequest(new ApiResponse(400, errorMessages));
        //}

        //return CreatedAtAction(nameof(AddUser), new
        //{
        //    id = newUser.Id
        //});

        var result = await _userService.AddUserAsync(addUserDto);
        return Ok(result);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> LoginUserAsync(LoginUserDto loginUserDto)
    {
        var result = await _userService.LoginUserAsync(loginUserDto);
        SetRefreshTokenInCookie(result.RefreshToken);
        return Ok(result);
    }

    [HttpPost("Rol")]
    public async Task<IActionResult> AddRoleAsync(AddRolDto model)
    {
        var result = await _userService.AddRolAsync(model);
        return Ok(result);
    }


    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        var response = await _userService.RefreshTokenAsync(refreshToken);
        if (!string.IsNullOrEmpty(response.RefreshToken))
            SetRefreshTokenInCookie(response.RefreshToken);
        return Ok(response);
    }


    private void SetRefreshTokenInCookie(string refreshToken)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(10),
        };
        Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
    }
}
