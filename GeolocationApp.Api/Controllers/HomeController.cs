using Microsoft.AspNetCore.Mvc;

namespace GeolocationApp.Api.Controllers;

[ApiController]
[Route("/")]
public class HomeController : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        var info = new
        {
            name = "Geolocation App", 
            version = "1.0.0"
        };
        return Ok(info);
    }
}