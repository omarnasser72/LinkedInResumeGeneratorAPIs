using System.Text.Json.Serialization;

namespace LinkedInResumeGenerator.Models.ScrapBotModels
{
    public class ScrapeBotEducationModel
    {
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("start_year")]
        public string? StartYear { get; set; }

        [JsonPropertyName("end_year")]
        public string? EndYear { get; set; }

        [JsonPropertyName("field")]
        public string? Field { get; set; }

        [JsonPropertyName("degree")]
        public string? Degree { get; set; }
    }
}
