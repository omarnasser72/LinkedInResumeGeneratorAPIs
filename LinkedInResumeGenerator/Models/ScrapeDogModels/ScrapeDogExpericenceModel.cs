using System.Text.Json.Serialization;

namespace LinkedInResumeGenerator.Models.ScrapeDogModels
{
    public class ScrapeDogExpericenceModel
    {
        [JsonPropertyName("position")]
        public string? Position { get; set; }

        [JsonPropertyName("company_name")]
        public string? CompanyName { get; set; }

        [JsonPropertyName("starts_at")]
        public string? StartsAt { get; set; }

        [JsonPropertyName("ends_at")]
        public string? EndsAt { get; set; }
    }
}
