using System.ComponentModel.DataAnnotations;

namespace MentalClinic.API.Models.Request;

public class Appointment
{
    [Required]
    public string SpecialistSelect { get; set; }

    [Required]
    public DateOnly SelectedDate { get; set; }

    [Required]
    public TimeOnly SelectedTimeSlot { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    public string MiddleName { get; set; }

    public string PhoneNumber { get; set; }

    public bool AgreeToTerms { get; set; }

    public string IssueMsg { get; set; }

    public CheckboxGroup CheckboxGroup { get; set; }
}


public class CheckboxGroup
{
    public bool TelegramCheckbox { get; set; }

    public bool ViberCheckbox { get; set; }

    public bool WhatsappCheckbox { get; set; }
}
