namespace LinkedInResumeGenerator.Models
{
    public class Error
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = null!;

        public Error(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }

    }
}
