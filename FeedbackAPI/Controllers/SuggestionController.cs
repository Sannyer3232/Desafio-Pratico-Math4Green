using FeedbackAPI.DTOs;
using FeedbackAPI.Models;
using FeedbackAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace FeedbackAPI.Controllers;  

[ApiController]
[Route("api/[controller]")]
public class SuggestionController : ControllerBase
{
    private readonly SuggestionService _service;

    public  SuggestionController(SuggestionService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> getAll()
    {
        return Ok(await _service.getAllSuggestionsAsync());
    }

    [HttpPost]
    public async Task<IActionResult> create([FromBody] CreateSuggetionsDto dto)
    {
        try
        {
            var suggestion = await _service.createSuggestionAsync(dto);
            return Ok(suggestion);
        }
        catch(Exception ex)
        {
            return BadRequest(new {message = ex.Message });
        }
    }

    [HttpPost("vote")]
    [Authorize]
    public async Task<IActionResult> vote([FromBody] CreateVoteDto dto)
    {
        try
        {
            await _service.addVoteAsync(dto);
            return Ok( new {message = "Voto computado com sucesso!"});

        }
        catch(Exception ex)
        {
            return BadRequest(new { message = ex.Message});
        }
    }
}