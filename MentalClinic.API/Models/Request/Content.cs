using System.Text.Json.Serialization;
using MentalClinic.API.Models.Domain;

namespace MentalClinic.API.Models.Request;

public class Content
{
    [JsonPropertyName("main-text")]
    public MainText MainText { get; set; }

    [JsonPropertyName("list-of-disorders")]
    public Disorder Disorder { get; set; }
}
