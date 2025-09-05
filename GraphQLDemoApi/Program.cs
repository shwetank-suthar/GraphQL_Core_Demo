using Microsoft.EntityFrameworkCore; // For UseSqlServer
using Microsoft.AspNetCore.Builder;  // For WebApplication
using Microsoft.Extensions.DependencyInjection; // For DI
using Microsoft.Extensions.Hosting; // For app.Environment
using GraphQLDemoApi.GraphQL.Queries;
using HotChocolate.AspNetCore;
using GraphQLDemoApi.Data;
using GraphQLDemoApi.Models;
using GraphQLDemoApi.GraphQL.Mutations;

var builder = WebApplication.CreateBuilder(args);

// DB connection
builder.Services.AddDbContext<WebLineIndiaBackup15nov2024Context>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add GraphQL
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "GraphQL Demo API",
        Version = "v1",
        Description = "A GraphQL API with REST endpoints for user management",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "GraphQL Demo API",
            Email = "admin@example.com"
        }
    });
    
    // Include XML comments if available
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = System.IO.Path.Combine(System.AppContext.BaseDirectory, xmlFile);
    if (System.IO.File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "GraphQL Demo API v1");
        c.RoutePrefix = "swagger"; // Set Swagger UI at /swagger
        c.DocumentTitle = "GraphQL Demo API Documentation";
        c.DefaultModelsExpandDepth(-1); // Hide models section
    });
}

app.UseHttpsRedirection();

// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };

// app.MapGet("/weatherforecast", () =>
// {
//     var forecast = Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeatherForecast");
app.UseCors();

// Map controllers for REST API endpoints
app.MapControllers();

// Map GraphQL endpoint
app.MapGraphQL()
   .WithOptions(new GraphQLServerOptions
   {
       Tool = { Enable = true } // Enables Banana Cake Pop
   });

app.Run();

// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }
