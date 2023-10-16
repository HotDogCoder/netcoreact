using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GeoApi.Application.ServicesInterfaces;
using GeoApi.Domain.Entities;
using System.Security.Claims;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class RankingController : ControllerBase
{
    private readonly ISurveyService _surveyService;

    public RankingController(ISurveyService surveyService)
    {
        _surveyService = surveyService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_surveyService.GetRankingModels());
    }
}
