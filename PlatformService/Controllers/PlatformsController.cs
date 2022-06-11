using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;

namespace PlatformService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlatformsController: ControllerBase{
    private readonly IPlatformRepo _repository;
    private readonly IMapper _mapper;

    public PlatformsController(IPlatformRepo repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    } 

    [HttpGet("GetPlatforms")]
    public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms(){
        Console.WriteLine("----Getting all platforms----");
        return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(_repository.GetAllPlatforms()));
    }

    [HttpGet("GetPlatformById/{id}")]
    public ActionResult GetPlatfromById(int id)
    {
        var platformItem = _mapper.Map<PlatformReadDto>(_repository.GetPlatformById(id));
        if(platformItem != null)
        {
            return Ok(platformItem);
        } 

        return NotFound();
    }
} 