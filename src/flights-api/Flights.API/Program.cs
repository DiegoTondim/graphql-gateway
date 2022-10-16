using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c =>
    {
        c.PreSerializeFilters.Add((swagger, httpReq) =>
        {
            swagger.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}" } };
        });
    });
    app.UseSwaggerUI();
}

app.MapGet("/flights", () =>
{
    var flights =  Enumerable.Range(1, 10).Select(index =>
        new Flight
        (
            DateTime.Now.AddDays(index),
            $"FLIGHT{Random.Shared.Next(100, 200)}",
            "Dublin",
            "London",
            Random.Shared.Next(50, 400)
        ))
        .ToArray();
    return flights;
})
.WithName("GetFlights");

app.Run();

record Flight(DateTime Date, string Number, string From, string To, decimal Price);
