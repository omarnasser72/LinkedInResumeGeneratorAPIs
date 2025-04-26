using System.Text.Json.Serialization;

namespace LinkedInResumeGenerator.Models.ScrapBotModels
{
    public class ScrapeBotExperienceModel
    {
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("subtitle")]
        public string? Company { get; set; }

        [JsonPropertyName("start_date")]
        public string? StartDate { get; set; }

        [JsonPropertyName("end_date")]
        public string? EndDate { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }
}
