using BookingAPI.Application.Interfaces;
using BookingAPI.Application.UseCases;
using BookingAPI.Infrastructure.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

//  Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//  Swagger configuration
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Booking API",
        Version = "v1",
        Description = "API to list available homes by date range (Clean Architecture)",
        Contact = new OpenApiContact
        {
            Name = "Fərid Rajabli",
            Email = "farid.racabli232gmail.com",
            Url = new Uri("https://github.com/faridrajabli23")
        }
    });

    // Optional: if you later add XML comments
    // var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    // c.IncludeXmlComments(xmlPath);
});

//  Dependency Injection
builder.Services.AddSingleton<IHomeService, InMemoryHomeService>();
builder.Services.AddScoped<HomeUseCase>();

var app = builder.Build();

//  Configure HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Booking API v1");
        c.RoutePrefix = ""; // Swagger as root
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();

//  Required for integration tests (WebApplicationFactory)
public partial class Program { }
