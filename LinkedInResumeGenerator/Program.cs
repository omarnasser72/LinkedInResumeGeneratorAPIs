
namespace LinkedInResumeGenerator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(configure =>
            {
                configure.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin",
                                   policy => policy.AllowAnyOrigin()
                                                   .AllowAnyHeader()
                                                   .AllowAnyMethod());
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapControllers();

            app.UseCors("AllowAnyOrigin");

            app.Run();
        }
    }
}
