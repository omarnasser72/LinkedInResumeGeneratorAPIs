namespace LinkedInResumeGenerator.DTOs
{
    public class ResumeDataDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Location { get; set; }

        public IEnumerable<Experience>? Experiences { get; set; }
        public IEnumerable<Education>? Educations { get; set; }

    }

    public class Experience
    {
        public string? Position { get; set; }
        public string? CompanyName { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
    }

    public class Education
    {
        public string? College { get; set; }
        public string? Degree { get; set; }
        public string? StartYear { get; set; }
        public string? EndYear { get; set; }
    }
}
