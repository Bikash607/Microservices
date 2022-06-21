using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlatformsController: ControllerBase{
    private readonly IPlatformRepo _repository;
    private readonly IMapper _mapper;
    private readonly ICommandDataClient _commandDataClient;

    public PlatformsController(
        IPlatformRepo repository,
        IMapper mapper, 
        ICommandDataClient commandDataClient)
    {
        _repository = repository;
        _mapper = mapper;
        _commandDataClient = commandDataClient;
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

    [HttpPost("CreatePlatform")]
    public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto platformCreateDto)
    {
        var platformModel = await Task.FromResult(_mapper.Map<Platform>(platformCreateDto));
        _repository.CreatePlatform(platformModel);
        _repository.SaveChanges();
        var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);
        try
        {
            await _commandDataClient.SendPlatformToCommand(platformReadDto);
        }
        catch (System.Exception ex)
        {
            Console.WriteLine("Cound not send synchronously :: " + ex.Message);
        }

        return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDto.Id }, platformReadDto);
    }


} 