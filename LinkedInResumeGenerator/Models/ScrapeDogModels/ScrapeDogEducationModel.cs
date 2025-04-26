using System.Text.Json.Serialization;

namespace LinkedInResumeGenerator.Models.ScrapeDogModels
{
    public class ScrapeDogEducationModel
    {
        [JsonPropertyName("college_name")]
        public string? College { get; set; }

        [JsonPropertyName("college_degree_field")]
        public string? Degree { get; set; }

        [JsonPropertyName("college_duration")]
        public string? Duration { get; set; }
    }
}
