using System.Text.Json.Serialization;

namespace LinkedInResumeGenerator.Models.ScrapeDogModels
{
    public class ScrapeDogModel
    {
        [JsonPropertyName("first_name")]
        public string? FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string? LastName { get; set; }

        [JsonPropertyName("location")]
        public string? Location { get; set; }

        [JsonPropertyName("experience")]
        public IEnumerable<ScrapeDogExpericenceModel>? Experiences { get; set; }

        [JsonPropertyName("education")]
        public IEnumerable<ScrapeDogEducationModel>? Educations { get; set; }

    }
}
