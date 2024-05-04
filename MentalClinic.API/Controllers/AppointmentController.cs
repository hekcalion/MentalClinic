using MentalClinic.API.Models.Domain;
using MentalClinic.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using AppointmentRequest = MentalClinic.API.Models.Request.Appointment;

namespace MentalClinic.API.Controllers;

[Route("[controller]")]
[ApiController]
public class AppointmentController : ControllerBase
{
    private readonly AppointmentRepository _appointmentRepository;
    private readonly EmployeeRepository _employeeRepository;

    public AppointmentController(AppointmentRepository appointmentRepository, EmployeeRepository employeeRepository)
    {
        _appointmentRepository = appointmentRepository;
        _employeeRepository = employeeRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _appointmentRepository.GetAll();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var appointment = await _appointmentRepository.Get(id);

        if (appointment == null)
        {
            return NotFound();
        }

        return Ok(appointment);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AppointmentRequest request)
    {
        string id = Guid.NewGuid().ToString();

        var employee = await _employeeRepository.Get(request.SpecialistSelect);
        if (employee == null)
        {
            return BadRequest("Specialist does not exist");
        }

        await _appointmentRepository.Create(new Appointment
        {
            id = id,
            SpecialistSelect = request.SpecialistSelect,
            SelectedDate = request.SelectedDate,
            SelectedTimeSlot = request.SelectedTimeSlot,
            FirstName = request.FirstName,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            PhoneNumber = request.PhoneNumber,
            AgreeToTerms = request.AgreeToTerms,
            IssueMsg = request.IssueMsg,
            CheckboxGroup = new Models.Domain.CheckboxGroup()
            {
                TelegramCheckbox = request.CheckboxGroup.TelegramCheckbox,
                ViberCheckbox = request.CheckboxGroup.ViberCheckbox,
                WhatsappCheckbox = request.CheckboxGroup.WhatsappCheckbox
            }
        });

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromQuery] string id, [FromBody] AppointmentRequest request)
    {
        var appointment = await _appointmentRepository.Get(id);
        if (appointment == null)
        {
            return BadRequest("Appointment does not exist");
        }

        var employee = await _employeeRepository.Get(request.SpecialistSelect);
        if (employee == null)
        {
            return BadRequest("Specialist does not exist");
        }

        await _appointmentRepository.Update(new Appointment()
        {
            id = id,
            SpecialistSelect = request.SpecialistSelect,
            SelectedDate = request.SelectedDate,
            SelectedTimeSlot = request.SelectedTimeSlot,
            FirstName = request.FirstName,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            PhoneNumber = request.PhoneNumber,
            AgreeToTerms = request.AgreeToTerms,
            IssueMsg = request.IssueMsg,
            CheckboxGroup = new Models.Domain.CheckboxGroup()
            {
                TelegramCheckbox = request.CheckboxGroup.TelegramCheckbox,
                ViberCheckbox = request.CheckboxGroup.ViberCheckbox,
                WhatsappCheckbox = request.CheckboxGroup.WhatsappCheckbox
            }
        });

        return Ok();
    }


    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] string id)
    {
        var appointment = await _appointmentRepository.Get(id);

        if (appointment == null)
        {
            return NotFound();
        }

        await _appointmentRepository.Delete(id);

        return Ok();
    }
}
