## LinkedIn Resume Generator API

This API service allows you to fetch LinkedIn profile data via the ScrapeDog API and generate a PDF resume based on the retrieved information. It consists of two main endpoints:

- **GET** `/GetLinkedInData?linkedInProfileLink={profileUrl}`: Scrapes LinkedIn data and returns structured JSON with user details, experiences, and education.
- **POST** `/GetResumeFile`: Accepts a JSON body containing the resume data and returns a generated PDF file.

---

## Table of Contents

1. [Prerequisites](#prerequisites)
2. [Installation](#installation)
3. [Configuration](#configuration)
4. [Running the Service](#running-the-service)
5. [API Endpoints](#api-endpoints)
   - [LinkedIn Callback](#linkedin-callback)
   - [GetLinkedInData](#getlinkeindata)
   - [GetResumeFile](#getresumefile)
6. [DTOs and Models](#dtos-and-models)
7. [Error Handling](#error-handling)
8. [Data Seeding for Local Testing](#data-seeding-for-local-testing)
9. [License](#license)

---

## Prerequisites

- .NET 6 or later
- QuestPDF (via NuGet package QuestPDF)
- An account and API token for ScrapeDog.ai
- Visual Studio or VS Code (optional)

---

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/your-org/LinkedInResumeGenerator.git
   cd LinkedInResumeGenerator
   ```
2. Restore NuGet packages:
   ```bash
   dotnet restore
   ```
3. Build the project:
   ```bash
   dotnet build
   ```

---

## Configuration

Add the following settings to your `appsettings.json` or environment variables:

```json
{
  "ScrapeDogToken": "<YOUR_SCRAPEDOG_API_TOKEN>",
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  }
}
```

- `ScrapeDogToken`: Your API key for the ScrapeDog scraping service.

---

## Running the Service

Start the API via CLI:
```bash
dotnet run --project LinkedInResumeGenerator
```
By default, the API will listen on `http://localhost:5000`.

---

## API Endpoints

### LinkedIn Callback

**Route:** `/auth/linkedin/callback?code={code}&state={state}`

- **Method:** GET
- **Description:** OAuth callback endpoint to handle LinkedIn authentication. Currently returns the `code` query parameter.
- **Response:**
  - `200 OK` with the authorization code.
  - `400 Bad Request` if `code` is missing.

### GetLinkedInData

**Route:** `/GetLinkedInData?linkedInProfileLink={profileUrl}`

- **Method:** GET
- **Description:** Scrapes a LinkedIn profile using the ScrapeDog API and returns structured resume data.
- **Parameters:**
  - `linkedInProfileLink` (string, required): Full URL of the LinkedIn profile (e.g., `https://www.linkedin.com/in/omar-nasser/`).

- **Responses:**
  - `200 OK` with `ResumeDataDTO` JSON object.
  - `400 Bad Request` with `Error` object on failure.

**Sample Request:**
```bash
curl -X GET \
  "http://localhost:5000/GetLinkedInData?linkedInProfileLink=https://www.linkedin.com/in/john-doe/"
```

### GetResumeFile

**Route:** `/GetResumeFile`

- **Method:** POST
- **Description:** Generates a PDF resume from the provided resume data.
- **Request Body:** `ResumeDataDTO` JSON object.
- **Responses:**
  - `200 OK` with PDF file (`application/pdf`).
  - `400 Bad Request` with `Error` object on failure.

**Sample Request:**
```bash
curl -X POST \
  "http://localhost:5000/GetResumeFile" \
  -H "Content-Type: application/json" \
  -d '{
    "FirstName": "Omar",
    "LastName": "Nasser",
    "Location": "Cairo, Egypt",
    "Experiences": [
      { "Position": "Software Engineer", "CompanyName": "TechCorp", "StartDate": "Jan 2020", "EndDate": "Present" }
    ],
    "Educations": [
      { "College": "ABC University", "Degree": "B.Sc. Computer Science", "StartYear": "2016", "EndYear": "2020" }
    ]
}' --output Omar_Nasser_Resume.pdf
```

---

## DTOs and Models

- **ResumeDataDTO**: Contains `FirstName`, `LastName`, `Location`, `IEnumerable<Experience>`, `IEnumerable<Education>`.
- **Experience**: `Position`, `CompanyName`, `StartDate`, `EndDate`.
- **Education**: `College`, `Degree`, `StartYear`, `EndYear`.
- **ScrapeDogModel**: Internal model mapping JSON from ScrapeDog API.

---

## Error Handling

Errors are returned as JSON in the following format:

```json
{
  "status": 400,
  "message": "Error description here"
}
```

- `400 Bad Request`: Client-side errors such as missing parameters or failed scraping.
- `500 Internal Server Error`: Server-side exceptions.

---

## Data Seeding for Local Testing

To test without calling the ScrapeDog API, place JSON files named `ScrapeDog.json` in the `DataSeed` folder. The controller will read these files when uncommenting the local data sections.

---
