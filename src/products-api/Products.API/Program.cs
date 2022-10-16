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

var summaries = new[]
{
    "Soda", "Beer", "Coffe", "Tea", "Sandwich", "Pasta"
};

app.MapGet("/products", () =>
{
    var products =  Enumerable.Range(1, 5).Select(index =>
        new Product
        (
            summaries[Random.Shared.Next(summaries.Length)],
            Random.Shared.Next(4, 10)
        ))
        .ToArray();
    return products;
})
.WithName("GetProducts");

app.Run();

record Product(string Name, decimal Price);
