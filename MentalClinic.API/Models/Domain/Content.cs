using System.Text.Json.Serialization;

namespace MentalClinic.API.Models.Domain
{

    public class Content
    {
        public string id { get; set; }

        [JsonPropertyName("main-text")]
        public MainText MainText { get; set; }

        [JsonPropertyName("list-of-disorders")]
        public Disorder Disorder { get; set; }
    }

    public class MainText
    {
        public string SubHeader { get; set; }

        public List<string> Paragraphs { get; set; }
    }


    public class Disorder
    {
        public string Title { get; set; }

        public List<string> Items { get; set; }
    }
}
