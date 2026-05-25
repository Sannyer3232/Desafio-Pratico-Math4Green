using System.Net.Http.Headers;
using FeedbackAPI.DTOs;
using FeedbackAPI.Models;
using FeedbackAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> register([FromBody]  RegisterUserDto dto)
    {
        try
        {
            var user = await _userService.registerAsync(dto);
            return Ok(user);
        }
        catch(Exception ex)
        {
            return BadRequest(new {message = ex.Message});
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> login([FromBody] LoginUserDto dto)
    {
        var token = await _userService.loginAsync(dto);

        if(token == null)
        {
            return Unauthorized(new{message = "Usuário ou senha inválidos"});

        }

        return Ok(new {token = token});
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> getAll()
    {
        return Ok(await _userService.getAllUsersAsync());
    }
}