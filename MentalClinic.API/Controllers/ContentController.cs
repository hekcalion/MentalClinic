using MentalClinic.API.Models.Domain;
using MentalClinic.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using ContentRequest = MentalClinic.API.Models.Request.Content;

namespace MentalClinic.API.Controllers;

[Route("[controller]")]
[ApiController]
public class ContentController : ControllerBase
{
    private readonly ContentRepository _contentRepository;

    public ContentController(ContentRepository contentRepository)
    {  
        _contentRepository = contentRepository; 
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _contentRepository.GetAll();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var result = await _contentRepository.Get(id);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ContentRequest request)
    {
        string id = Guid.NewGuid().ToString();
        await _contentRepository.Create(new Content
        {
            id = id,
            MainText = request.MainText,
            Disorder = request.Disorder
        });

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromQuery] string id, [FromBody] ContentRequest request)
    {
        var content = await _contentRepository.Get(id);

        if (content == null)
        {
            return BadRequest("Employee does not exist");
        }

        await _contentRepository.Update(new Content()
        {
            id = id,
            MainText = request.MainText,
            Disorder = request.Disorder
        });

        return Ok();
    }


    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] string id)
    {
        var content = await _contentRepository.Get(id);

        if (content == null)
        {
            return NotFound();
        }

        await _contentRepository.Delete(id);

        return Ok();
    }
}
