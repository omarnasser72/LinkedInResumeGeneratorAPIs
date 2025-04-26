using System.Text.Json.Serialization;

namespace LinkedInResumeGenerator.Models.ScrapBotModels
{
    public class ScrapeBotModel
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("experience")]
        public IEnumerable<ScrapeBotExperienceModel>? Experiences { get; set; }

        [JsonPropertyName("education")]
        public IEnumerable<ScrapeBotEducationModel>? Educations { get; set; }
    }
}
