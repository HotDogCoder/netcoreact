using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GeoApi.Application.ServicesInterfaces;
using GeoApi.Domain.Entities;
using System.Security.Claims;
using GeoApi.Application.Services;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class UserSurveyResponseController : ControllerBase
{
    private readonly ISurveyService _surveyService;

    public UserSurveyResponseController(ISurveyService surveyService)
    {
        _surveyService = surveyService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            return Ok(_surveyService.GetAllSurveys());
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        try
        {
            var survey = _surveyService.GetSurveyById(id);
            if (survey == null) return NotFound();
            return Ok(survey);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost]
    public IActionResult Create(SurveyDto surveyDto)
    {
        try
        {
            surveyDto.CreatedByUserId = GetCurrentUserId();
            var result = _surveyService.AddSurveyResponse(surveyDto);
            if (result == null)
            {
                return BadRequest("Unable to create survey response");
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    private int GetCurrentUserId()
    {
        return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
    }
}
