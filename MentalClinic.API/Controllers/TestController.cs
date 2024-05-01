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
}
