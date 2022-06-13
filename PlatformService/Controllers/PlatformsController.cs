using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

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
    public async Task<ActionResult<IEnumerable<PlatformReadDto>>> GetPlatforms(){
        Console.WriteLine("----Getting all platforms----");
        return Ok(await Task.FromResult(_mapper.Map<IEnumerable<PlatformReadDto>>(_repository.GetAllPlatforms())));
    }

    [HttpGet("{id}",Name="GetPlatformById")]
    public async Task<ActionResult> GetPlatformById(int id)
    {
        var platformItem = await Task.FromResult(_mapper.Map<PlatformReadDto>(_repository.GetPlatformById(id)));
        if(platformItem != null)
        {
            return Ok(platformItem);
        } 

        return NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto platformCreateDto)
    {
        var platformModel = await Task.FromResult(_mapper.Map<Platform>(platformCreateDto));
        _repository.CreatePlatform(platformModel);
        _repository.SaveChanges();
        var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);
        return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDto.Id }, platformReadDto);
    }
} 