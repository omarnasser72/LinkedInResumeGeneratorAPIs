using LinkedInResumeGenerator.DTOs;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

public static class PDFService
{
    public static byte[] GeneratePdf(ResumeDataDTO data)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        data.Experiences = data.Experiences?
                                .Where(e => !string.IsNullOrEmpty(e.Position) && !string.IsNullOrEmpty(e.CompanyName))
                                .ToList();

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(11));

                page.Header()
                    .AlignCenter()
                    .Column(col =>
                    {
                        col.Item().Text($"{data.FirstName} {data.LastName}")
                            .FontSize(20).Bold();

                        col.Item().Text(data.Location)
                            .FontSize(12)
                            .Italic();
                    });

                page.Content()
                    .PaddingVertical(15)
                    .Column(column =>
                    {
                        column.Spacing(15);

                        // Experience Section
                        column.Item().Element((container) =>
                        {
                            container.Column(col =>
                            {
                                col.Item().Text("EXPERIENCE").FontSize(14).Bold();

                                foreach (var exp in data.Experiences!)
                                {
                                    col.Item().PaddingVertical(5).Column(expCol =>
                                    {
                                        expCol.Item().Text($"{exp.Position} at {exp.CompanyName}");
                                        expCol.Item().Text($"{exp.StartDate} - {exp.EndDate}")
                                            .FontColor(Colors.Grey.Darken1);
                                    });
                                }
                            });

                        });

                        // Education Section
                        column.Item().Element(container =>
                        {
                            container.Column(col =>
                            {
                                col.Item().Text("EDUCATION").FontSize(14).Bold();

                                foreach (var edu in data.Educations!)
                                {
                                    col.Item().PaddingVertical(5).Column(eduCol =>
                                    {
                                        eduCol.Item().Text(edu.College);
                                        eduCol.Item().Text(edu.Degree);
                                        eduCol.Item().Text($"{edu.StartYear} - {edu.EndYear}")
                                            .FontColor(Colors.Grey.Darken1);
                                    });
                                }
                            });
                        });
                    });


            });
        });

        return document.GeneratePdf();
    }
}