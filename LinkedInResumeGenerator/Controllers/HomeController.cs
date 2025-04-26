using LinkedInResumeGenerator.DTOs;
using LinkedInResumeGenerator.Models;
using LinkedInResumeGenerator.Models.ScrapBotModels;
using LinkedInResumeGenerator.Models.ScrapeDogModels;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using static LinkedInResumeGenerator.Constants.LinkedInConstants;

namespace LinkedInResumeGenerator.Controllers
{
    [Route("{controller}")]
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(IConfiguration configuration, ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("/auth/linkedin/callback")]
        public IActionResult LinkedInCallback([FromQuery] string code, [FromQuery] string state)
        {
            if (code is { })
                return Ok(code);
            return BadRequest();
        }

        private string? GetLinkedInId(string linkedInProfileLink)
            => new string(linkedInProfileLink.Skip(28).ToArray());

        private string? GetJsonData(string fileName)
        {
            var path = Path.Combine(_webHostEnvironment.ContentRootPath, "DataSeed", fileName);

            if (!System.IO.File.Exists(path))
                return null!;

            return System.IO.File.ReadAllText(path);
        }

        [HttpGet("/GetLinkedInData")]
        public async Task<IActionResult> GetLinkedInData([FromQuery] string linkedInProfileLink)
        {
            var linkedInId = GetLinkedInId(linkedInProfileLink);
            try
            {
                #region ScrapeBot Actual Call

                //var scrapeBotReq = new HttpClient();

                //var username = _configuration["ScrapeBotUsername"];
                //var apiKey = _configuration["ScrapeBotPassword"];

                //var credentials = Convert.ToBase64String(
                //    Encoding.ASCII.GetBytes($"{username}:{apiKey}")
                //);

                //scrapeBotReq.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                //var scrapeBotUrl = new string(ScrapeBotUrl.Concat($"&api_key={_configuration["ScrapeDogToken"]}&linkId={linkedInId}").ToArray());

                //var requestBody = new
                //{
                //    scraper = "linkedinProfile",
                //    url = linkedInId,
                //};

                //var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody));

                //var response = await scrapeBotReq.PostAsync(scrapeBotUrl, jsonContent);

                //response.EnsureSuccessStatusCode();
                //return await response.Content.ReadAsStringAsync();

                //var scrapeBotRes = await scrapeBotReq.PostAsync(scrapeBotUrl);

                //if (!scrapeBotRes.IsSuccessStatusCode)
                //    return BadRequest(new Error(400, "Failed to retrieve data from given url."));

                //var scrapeBotModel = scrapeBotRes.Content.ReadFromJsonAsync<ScrapeDogModel>().Result;

                #endregion

                #region ScrapeDog Actual Call

                var scrapeDogFeq = new HttpClient();

                var scrapeDogUrl = new string(ScrapeDogUrl.Concat($"&api_key={_configuration["ScrapeDogToken"]}&linkId={linkedInId}").ToArray());

                var scrapeDogRes = await scrapeDogFeq.GetAsync(scrapeDogUrl);

                if (!scrapeDogRes.IsSuccessStatusCode)
                    return BadRequest(new Error(400, "Failed to retrieve data from given url."));

                var scrapeDogModel = scrapeDogRes.Content.ReadFromJsonAsync<ScrapeDogModel>().Result;

                #endregion

                #region ScrapeDog

                //var jsonScrapeDogData = GetJsonData("ScrapeDog.json");

                //if (jsonScrapeDogData is not { })
                //    return BadRequest(new Error(404, "Couldn't find json file."));

                //var scrapeDogModel = JsonSerializer.Deserialize<List<ScrapeDogModel>>(jsonScrapeDogData)?[0];

                #endregion

                #region ScrapeBot

                //var jsonScrapeBotData = GetJsonData("ScrapeBot2.json");

                //if (jsonScrapeBotData is not { })
                //    return BadRequest(new Error(404, "Couldn't find json file."));

                //var scrapeBotModel = JsonSerializer.Deserialize<List<ScrapeBotModel>>(jsonScrapeBotData)?[0];

                #endregion

                if (scrapeDogModel is null)
                    return BadRequest(new Error(500, "Failed to scrape your profile."));

                List<Experience> experiences = [];
                if (scrapeDogModel.Experiences is { })
                {
                    foreach (var experience in scrapeDogModel.Experiences)
                        experiences.Add(new Experience()
                        {
                            CompanyName = experience.CompanyName,
                            Position = experience.Position,
                            StartDate = experience.StartsAt,
                            EndDate = experience.EndsAt
                        });

                }


                List<Education> educations = [];
                if (scrapeDogModel.Educations is { })
                {
                    foreach (var education in scrapeDogModel.Educations)
                        educations.Add(new Education()
                        {
                            College = education.College,
                            Degree = education.Degree,
                            StartYear = education.Duration!.Split('-')[0],
                            EndYear = education.Duration.Split('-')[1]
                        });
                }

                var resumeData = new ResumeDataDTO()
                {
                    FirstName = scrapeDogModel.FirstName,
                    LastName = scrapeDogModel.LastName,
                    Location = scrapeDogModel.Location,
                    Educations = educations,
                    Experiences = experiences,
                };

                return Ok(resumeData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new Error(500, "Sorry our server is busy."));
            }
        }

        [HttpPost("/GetResumeFile")]
        public async Task<IActionResult> GetResumeFile([FromBody] ResumeDataDTO resumeData)
        {
            var generatedBytes = PDFService.GeneratePdf(resumeData);

            return File(generatedBytes, "application/pdf", $"{resumeData.FirstName}_{resumeData.LastName}_Resume.pdf");
        }

    }
}
