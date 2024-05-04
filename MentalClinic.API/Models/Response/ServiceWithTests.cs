using MentalClinic.API.Models.Domain;

namespace MentalClinic.API.Models.Response;

public class ServiceWithTests
{
    public string Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public List<Banner> Banner { get; set; }

    public List<MainIllnessInformation> MainIllnessInformation { get; set; }

    public List<TestInfo> Testing { get; set; }

    public string TreatmentDuration { get; set; }
}

public class TestInfo
{
    public string Id { set; get; }

    public string BlockHeader { get; set; }

    public string BlockSubHeader { get; set; }
}
