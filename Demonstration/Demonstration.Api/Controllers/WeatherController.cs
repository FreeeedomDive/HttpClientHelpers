using Microsoft.AspNetCore.Mvc;
using Xdd.HttpHelpers.Models.Attributes;

namespace Demonstration.Api.Controllers;

[Route("api/weather")]
public class WeatherController : Controller
{
    [HttpGet("temperature")]
    public int GetTemperature()
    {
        return Random.Shared.Next(-20, 20);
    }

    [HttpGet("temperature2")]
    [DontGenerate]
    /* Method in client will not be generated */
    public int GetRandomTemperature()
    {
        return Random.Shared.Next(-20, 20);
    }
}