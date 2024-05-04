namespace MentalClinic.API.Models.Domain;

public class Service
{
    public string id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public List<Banner> Banner { get; set; }

    public List<MainIllnessInformation> MainIllnessInformation { get; set; }

    public List<string> TestIds { get; set; }

    public string TreatmentDuration { get; set; }
}

public class MainIllnessInformation
{
    public string Header { get; set; }

    public List<Symptom> Symptoms { get; set; }

    public string SymptomsDuration { get; set; }
}

public class Symptom
{
    public string Description { get; set; }
}

public class Banner
{
    public string Title { get; set; }

    public string Subtitle { get; set; }
}
