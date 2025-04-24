using API;
using API.Hubs;
using Application;
using Application.CQRS.ProductCommandQuery.Command;
using Application.Interfaces;
using Application.Services;
using Core.IRepositories;
using Infrastructure;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using Serilog;

//var builder = WebApplication.CreateBuilder(args);
var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    ApplicationName=typeof(Program).Assembly.FullName,
    ContentRootPath=Path.GetFullPath(Directory.GetCurrentDirectory()),
    WebRootPath=Path.GetFullPath(Directory.GetCurrentDirectory()),
    Args=args
});

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

//fill AppConfigurations from appsetting.json
builder.Services.AddSignalR();
builder.Services.AddOptions();
builder.Services.Configure<AppConfigurations>(builder.Configuration.GetSection("AppConfigurations"));




// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddDbContext<OnlineShopDbContext>(optins =>
{
    optins
    .UseSqlServer(builder.Configuration.GetConnectionString("sqlconnection"))
    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
builder.Services.ApplicationServices();
builder.Services.AddRepositories();
builder.Services.AddUnitOfWork();
builder.Services.AddInfraUtility();
builder.Services.AddJWT();

///dotnet add package Microsoft.Extensions.Caching.Memory --version 9.0.3
builder.Services.AddMemoryCache();
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SaveProductCommandHandler).Assembly));
builder.Services.AddMemoryCache();
builder.Services.AddMiniProfiler(option => option.RouteBasePath = "/profiler").AddEntityFramework();
////results-index 


// Register Swagger
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
//    c.EnableAnnotations();
//});

builder.Services.AddSwagger();
builder.Services.AddHttpContextAccessor();

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAPI",
        builder =>
        {
            builder.WithOrigins("*");// api.domain.com
            builder.WithHeaders("*");
            builder.WithMethods("*");
        });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   // app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
    });

}
app.UseMiniProfiler();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.WebRootPath, "Media")),
    RequestPath = "/Media"
});
//app.UseCors("MyAPI");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();


//var summaries = new[]
//{
//    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//};

//app.MapGet("/weatherforecast", () =>
//{
//    var forecast =  Enumerable.Range(1, 5).Select(index =>
//        new WeatherForecast
//        (
//            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//            Random.Shared.Next(-20, 55),
//            summaries[Random.Shared.Next(summaries.Length)]
//        ))
//        .ToArray();
//    return forecast;
//})
//.WithName("GetWeatherForecast");


app.MapControllers();
app.MapHub<ChatHub>("/chatHub");

app.Run();

//record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
//{
//    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
//}
