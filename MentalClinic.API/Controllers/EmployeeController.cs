using MentalClinic.API.Helpers;
using MentalClinic.API.Models.Domain;
using MentalClinic.API.Models.Response;
using MentalClinic.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using EmployeeRequest = MentalClinic.API.Models.Request.Employee;

namespace MentalClinic.API.Controllers;

[Route("[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{

    private readonly EmployeeRepository _employeeRepository;
    private readonly AppointmentRepository _appointmentRepository;

    public EmployeeController(EmployeeRepository employeeRepository, AppointmentRepository appointmentRepository)
    {
        _employeeRepository = employeeRepository;
        _appointmentRepository = appointmentRepository;
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

    [HttpGet("{id}/availability")]
    public async Task<IActionResult> GetAvailability(string id)
    {
        var employee = await _employeeRepository.Get(id);

        if (employee == null)
        {
            return BadRequest("Employee does not exist");
        }

        var specialistAppointments = await _appointmentRepository.GetBySpecialistId(id);
        
        DateTime currentDateTime = DateTime.Now;

        List<TimeOnly> availableAppointmentHours = AppointmentHelper.GenerateHours();
        List<DateOnly> fiveWorkDays = AppointmentHelper.GenerateWorkdays(currentDateTime, 5);
        List<Availability> availability = new List<Availability>();

        foreach(var day in fiveWorkDays)
        {
            if(day == DateOnly.FromDateTime(currentDateTime))
            {
                availability.Add(new Availability()
                {
                    Date = day,
                    Timeslots = availableAppointmentHours.Except(specialistAppointments.Where(x => x.SelectedDate == day).Select(x => x.SelectedTimeSlot))
                    .Where(x => x > TimeOnly.FromDateTime(currentDateTime))
                    .ToList()
                });
            }

            availability.Add(new Availability()
            {
                Date = day,
                Timeslots = availableAppointmentHours.Except(specialistAppointments.Where(x => x.SelectedDate == day).Select(x => x.SelectedTimeSlot)).ToList()
            });
        }

        return Ok(new EmployeeAvailability()
        {
            Id = id,
            Availability = availability
        });
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] EmployeeRequest request)
    {
        string id = Guid.NewGuid().ToString();
        await _employeeRepository.Create(new Employee
        {
            id = id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Specialty = request.Specialty
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

        await _employeeRepository.Update(new Employee()
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
