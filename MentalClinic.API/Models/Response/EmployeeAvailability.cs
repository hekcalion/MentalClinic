namespace MentalClinic.API.Models.Response;

public class EmployeeAvailability
{
    public string Id { get; set; }

    public List<Availability> Availability { get; set; }
}

public class Availability
{
    public DateOnly Date { get; set; }

    public List<TimeOnly> Timeslots { get; set; }
}
