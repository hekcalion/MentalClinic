using MentalClinic.API.Models.Domain;

namespace MentalClinic.API.Models.Request;

public class Service
{
    public string Title { get; set; }

    public string Description { get; set; }

    public List<Banner> Banner { get; set; }

    public List<MainIllnessInformation> MainIllnessInformation { get; set; }

    public List<string> TestIds { get; set; }

    public string TreatmentDuration { get; set; }
}
