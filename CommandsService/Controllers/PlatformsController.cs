using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers;

[Route("api/c/[controller]")]
[ApiController]
public class PlatformsController : ControllerBase
{

    [HttpPost("Test")]
    public ActionResult Index()
    {
        Console.WriteLine("PlatformsController.Index()");
        return Ok("PlatformsController.Index()");
    }
}