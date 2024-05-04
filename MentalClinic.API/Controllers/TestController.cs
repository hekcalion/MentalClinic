using MentalClinic.API.Models.Domain;
using MentalClinic.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using TestRequest = MentalClinic.API.Models.Request.Test;

namespace MentalClinic.API.Controllers;

[Route("[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    private readonly TestRepository _testRepository;

    public TestController(TestRepository testRepository)
    {
        _testRepository = testRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _testRepository.GetAll();

        return Ok(result.Select(x => new
        {
            Id = x.id,
            Name = x.Name,
            ShortDescription = x.ShortDescription
        }));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var test = await _testRepository.Get(id);

        if (test == null)
        {
            return NotFound();
        }

        return Ok(new
        {
            Id = test.id,
            Name = test.Name,
            ShortDescription = test.ShortDescription,
            long_test_description = test.long_test_description,
            Questions = test.Questions,
            Result = test.Result
        });
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] TestRequest request)
    {
        string id = Guid.NewGuid().ToString();
        await _testRepository.Create(new Test()
        {
            id = id,
            BlockHeader = request.BlockHeader,
            BlockSubHeader = request.BlockSubHeader,
            Name = request.Name,
            ShortDescription = request.ShortDescription,
            long_test_description = request.long_test_description,
            Questions = request.Questions,
            Result = request.Result
        });

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromQuery] string id, [FromBody] TestRequest request)
    {
        var test = await _testRepository.Get(id);

        if (test == null)
        {
            return BadRequest("Test does not exist");
        }

        await _testRepository.Update(new Test()
        {
            id = id,
            BlockHeader = request.BlockHeader,
            BlockSubHeader = request.BlockSubHeader,
            Name = request.Name,
            ShortDescription = request.ShortDescription,
            long_test_description = request.long_test_description,
            Questions = request.Questions,
            Result = request.Result
        });

        return Ok();
    }


    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] string id)
    {
        var test = await _testRepository.Get(id);

        if (test == null)
        {
            return NotFound();
        }

        await _testRepository.Delete(id);

        return Ok();
    }
}
