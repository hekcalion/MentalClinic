namespace MentalClinic.API.Helpers;

public class AppointmentHelper
{
    private readonly static TimeSpan startWorkTime = new TimeSpan(9, 0, 0);

    private readonly static TimeSpan endWorkTime = new TimeSpan(18, 0, 0);

    private readonly static TimeSpan appointmentDuration = TimeSpan.FromMinutes(30);

    public static List<TimeOnly> GenerateHours()
    {
        List<TimeSpan> appointmentHours = new List<TimeSpan>();

        TimeSpan currentAppointmentTime = startWorkTime;
        while (currentAppointmentTime < endWorkTime)
        {
            appointmentHours.Add(currentAppointmentTime);
            currentAppointmentTime = currentAppointmentTime.Add(appointmentDuration);
        }

        return appointmentHours.Select(x => TimeOnly.FromTimeSpan(x)).ToList();
    }

    public static List<DateOnly> GenerateWorkdays(DateTime startDate, int numberOfDays)
    {
        List<DateTime> workdays = new List<DateTime>();

        DateTime currentDate =  (TimeOnly.FromDateTime(startDate) < GenerateHours().Last()) ? startDate : startDate.AddDays(1);
        int daysAdded = 0;

        while (daysAdded < numberOfDays)
        {
            if (IsWorkday(currentDate))
            {
                workdays.Add(currentDate);
                daysAdded++;
            }
            currentDate = currentDate.AddDays(1);
        }

        return workdays.Select(x => DateOnly.FromDateTime(x)).ToList();
    }

    static bool IsWorkday(DateTime date)
    {
        return date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;
    }
}
