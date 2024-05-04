using MentalClinic.API.Models.Domain;
using MentalClinic.API.Models.Response;
using MentalClinic.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using TestRequest = MentalClinic.API.Models.Request.Service;

namespace MentalClinic.API.Controllers;

[Route("[controller]")]
[ApiController]
public class ServiceController : ControllerBase
{
    private readonly ServiceRepository _serviceRepository;
    private readonly TestRepository _testRepository;

    public ServiceController(ServiceRepository serviceRepository, TestRepository testRepository)
    {
        _serviceRepository = serviceRepository;
        _testRepository = testRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _serviceRepository.GetAll();
        return Ok(result.Select(x => new
        {
            Id = x.id,
            Title = x.Title,
            Description = x.Description
        }));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var service = await _serviceRepository.Get(id);

        if (service == null)
        {
            return NotFound();
        }

        List<Test> tests = new List<Test>();
        if(service.TestIds != null && service.TestIds.Any()) 
        {
            var testTasks = service.TestIds.Select(async x => await _testRepository.Get(x));
            var testResults = await Task.WhenAll(testTasks);
            tests.AddRange(testResults);
        }

        return Ok(new ServiceWithTests()
        {
            Id = service.id,
            Banner = service.Banner,
            MainIllnessInformation = service.MainIllnessInformation,
            TreatmentDuration = service.TreatmentDuration,
            Testing = tests.Select(x => new TestInfo()
            {
                Id = x.id,
                BlockHeader = x.BlockHeader,
                BlockSubHeader = x.BlockSubHeader
            }).ToList()
        });
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] TestRequest request)
    {
        string id = Guid.NewGuid().ToString();

        if (request.TestIds != null && request.TestIds.Any())
        {
            var testTasks = request.TestIds.Select(async x => await _testRepository.Get(x));
            var testResults = await Task.WhenAll(testTasks);
            if (testResults != null && testResults.Any(x => x == null))
            {
                return BadRequest("Test does not exist");
            }
        }

        await _serviceRepository.Create(new Service
        {
            id = id,
            Title = request.Title,
            Description = request.Description,
            Banner = request.Banner,
            MainIllnessInformation = request.MainIllnessInformation,
            TestIds = request.TestIds,
            TreatmentDuration = request.TreatmentDuration
        });

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromQuery] string id, [FromBody] TestRequest request)
    {
        var service = await _serviceRepository.Get(id);

        if (service == null)
        {
            return BadRequest("Service does not exist");
        }

        if (request.TestIds != null && request.TestIds.Any())
        {
            var testTasks = request.TestIds.Select(async x => await _testRepository.Get(x));
            var testResults = await Task.WhenAll(testTasks);
            if (testResults != null && testResults.Any(x => x.Equals(null)))
            {
                return BadRequest("Test does not exist");
            }
        }

        await _serviceRepository.Update(new Service
        {
            id = id,
            Title = request.Title,
            Description = request.Description,
            Banner = request.Banner,
            MainIllnessInformation = request.MainIllnessInformation,
            TestIds = request.TestIds,
            TreatmentDuration = request.TreatmentDuration
        });

        return Ok();
    }


    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] string id)
    {
        var service = await _serviceRepository.Get(id);

        if (service == null)
        {
            return NotFound();
        }

        await _serviceRepository.Delete(id);

        return Ok();
    }
}
