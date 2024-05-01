using MentalClinic.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using EmployeeRequest = MentalClinic.API.Models.Request.Employee; 

namespace MentalClinic.API.Controllers;

[Route("[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{

    private readonly EmployeeRepository _employeeRepository;

    public EmployeeController(EmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _employeeRepository.GetAll();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var result = await _employeeRepository.Get(id);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] EmployeeRequest request)
    {
        string id = Guid.NewGuid().ToString();
        var statusCode = await _employeeRepository.Create(new Models.Domain.Employee
        {
            id = id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Specialty = request.Specialty,
        });

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromQuery] string id, [FromBody] EmployeeRequest request)
    {
        var employee = await _employeeRepository.Get(id);

        if (employee == null)
        {
            return BadRequest("Employee does not exist");
        }

        await _employeeRepository.Update(new Models.Domain.Employee()
        {
            id = id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Specialty = request.Specialty
        });

        return Ok();
    }


    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] string id)
    {
        var employee = await _employeeRepository.Get(id);

        if (employee == null)
        {
            return NotFound();
        }

        await _employeeRepository.Delete(id);
        return Ok();
    }
}
