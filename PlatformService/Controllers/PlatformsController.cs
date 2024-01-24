using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.DTOs;
using PlatformService.Models;

namespace PlatformService.Controllers;


[Route("api/[controller]")]
[ApiController]
public class PlatformsController: ControllerBase
{
    private readonly IPlatformRepo _repository;
    private readonly IMapper _mapper;

    public PlatformsController(IPlatformRepo repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlatformReadDto>>> GetPlatforms()
    {
        var platforms = await _repository.GetAllPlatforms();

        return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
    }

    [HttpGet("{id}", Name = "GetPlatformById")]
    public async Task<ActionResult<PlatformReadDto>> GetPlatformById(int id)
    {
        var platform = await _repository.GetPlatformById(id);

        if(platform == null) return NotFound();

        return Ok(_mapper.Map<PlatformReadDto>(platform));
    }

    [HttpPost]
    public async Task<ActionResult<PlatformReadDto>> AddPlatofrm(PlatformCreateDto dto)
    {
        if(!ModelState.IsValid) return BadRequest();

        var model = _mapper.Map<Platform>(dto);
        await _repository.CreatePlatform(model);

        if(! await _repository.SaveChangesAsync()) { return NotFound(); }

        var platform = _mapper.Map<PlatformReadDto>(model);

        return CreatedAtRoute(nameof(GetPlatformById), new { Id = platform.Id}, platform);
    }

     [HttpPut("{id}")]
    public async Task<ActionResult> UpdatePlatform(int id, PlatformUpdateDto dto)
    {
         if(!ModelState.IsValid) return BadRequest();
         if(dto.Id != id) return BadRequest("Mistach Ids");

        var model = _mapper.Map<Platform>(dto);
        
        await _repository.UpdatePlatform(id, model);

         if(! await _repository.SaveChangesAsync()) { return NotFound(); }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePlatform(int id)
    {
        await _repository.DeletePlatform(id);

         if(! await _repository.SaveChangesAsync()) { return NotFound(); }

        return NoContent();
    }
}
