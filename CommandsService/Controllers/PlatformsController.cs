using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers;

[Route("api/c/[controller]")]
[ApiController]
public class PlatformsController : ControllerBase
{

    [HttpPost("Test")]
    public ActionResult Index()
    {
        Console.WriteLine("Inbound Post call to Command Service Index Method"); 
        return Ok("Response from Command Service Index method");
    }
}