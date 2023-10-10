using Microsoft.AspNetCore.Mvc;
using GeoApi.Application;
using GeoApi.Domain;
using MaxMind.GeoIP2;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Authorization;

namespace GeoApi.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class GeoApiController : ControllerBase
{
    private readonly ILogger<GeoApiController> logger;
    private readonly IGeoApiService geoApiService;
    private readonly WebServiceClient maxMindClient;

    public GeoApiController
    (
        ILogger<GeoApiController> logger,
        IGeoApiService geoApiService,
        WebServiceClient maxMindClient
    )
    {
        this.logger = logger;
        this.geoApiService = geoApiService;
        this.maxMindClient = maxMindClient;
    }

    [HttpGet]
    [Route("GetGeoApiClasses")]
    public async Task<GeoApiClass> GetGeoApiClasses()
    {
        string ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
        
        if(ip == "::1")
        {
            ip = "181.67.138.98";
        }

        GeoApiFilterClass geoApiFilterClass = new GeoApiFilterClass(
            ip: ip,
            address: "",
            country: ""
        );

        GeoApiClass data = await this.geoApiService.GetGeoApiClasses(geoApiFilterClass);
                
        return data;
    }

    [HttpPost]
    [Route("PostGeoApiClasses")]
    public async Task<List<GeoApiClass>> PostGeoApiClasses([FromBody] List<GeoApiFilterClass> geoApiFilterClasses)
    {
        List<GeoApiClass> data = await this.geoApiService.PostGeoApiClasses(geoApiFilterClasses);
                
        return data;
    }
}
