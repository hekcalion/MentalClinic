using MentalClinic.API.Models.Domain;

namespace MentalClinic.API.Models.Request;

public class Test
{
    public string BlockHeader { get; set; }

    public string BlockSubHeader { get; set; }

    public string Name { get; set; }

    public string ShortDescription { get; set; }

    public string long_test_description { get; set; }

    public List<Question> Questions { get; set; }

    public List<Result> Result { get; set; }
}