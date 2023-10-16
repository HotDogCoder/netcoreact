using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GeoApi.Application.ServicesInterfaces;
using GeoApi.Domain.Entities;
using System.Security.Claims;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class SurveyController : ControllerBase
{
    private readonly ISurveyService _surveyService;

    public SurveyController(ISurveyService surveyService)
    {
        _surveyService = surveyService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_surveyService.GetAllSurveys());
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var survey = _surveyService.GetSurveyById(id);
        if (survey == null) return NotFound();
        return Ok(survey);
    }

    [HttpPost]
    public IActionResult Create(Survey survey)
    {
        _surveyService.AddSurvey(survey);
        return CreatedAtAction(nameof(GetById), new { id = survey.Id }, survey);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, SurveyDto surveyDto)
    {
        if (id != surveyDto.Id) return BadRequest();

        surveyDto.CreatedByUserId = GetCurrentUserId();
        _surveyService.UpdateSurvey(surveyDto);
        return Ok(surveyDto);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _surveyService.DeleteSurvey(id);
        return NoContent();
    }

    private int GetCurrentUserId()
    {
        return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
    }
}
