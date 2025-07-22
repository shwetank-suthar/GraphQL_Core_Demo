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
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // Swagger
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
