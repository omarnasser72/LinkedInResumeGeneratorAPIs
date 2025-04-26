//using System.Text.Json.Serialization;

//namespace LinkedInResumeGenerator.Models
//{
//    class ProxyCurlModel
//    {
//        [JsonPropertyName("first_name")]
//        public string FirstName { get; set; } = null!;

//        [JsonPropertyName("last_name")]
//        public string LastName { get; set; } = null!;

//        [JsonPropertyName("country_full_name")]
//        public string Country { get; set; } = string.Empty;

//        [JsonPropertyName("city")]
//        public string City { get; set; } = string.Empty;

//        [JsonPropertyName("experiences")]
//        public IEnumerable<Experience>? Experiences { get; set; }

//        [JsonPropertyName("education")]
//        public IEnumerable<Education>? Educations { get; set; }

//        [JsonPropertyName("skills")]
//        public IEnumerable<string>? Skills { get; set; }

//    }

//    class Experience
//    {
//        [JsonPropertyName("starts_at")]
//        public Date? StartDate { get; set; }

//        [JsonPropertyName("ends_at")]
//        public Date? EndDate { get; set; }

//        [JsonPropertyName("company")]
//        public string Company { get; set; } = string.Empty;

//        [JsonPropertyName("title")]
//        public string Title { get; set; } = string.Empty;

//        [JsonPropertyName("description")]
//        public string Description { get; set; } = string.Empty;
//    }

//    class Education
//    {
//        [JsonPropertyName("starts_at")]
//        public Date? StartDate { get; set; }

//        [JsonPropertyName("ends_at")]
//        public Date? EndDate { get; set; }

//        [JsonPropertyName("field_of_study")]
//        public string FieldOfStudy { get; set; } = string.Empty;

//        [JsonPropertyName("degree_name")]
//        public string Degree { get; set; } = string.Empty;

//        [JsonPropertyName("school")]
//        public string School { get; set; } = string.Empty;
//    }

//    class Date
//    {
//        [JsonPropertyName("day")]
//        public int Day { get; set; }

//        [JsonPropertyName("month")]
//        public int Month { get; set; }

//        [JsonPropertyName("year")]
//        public int Year { get; set; }
//    }

//}
