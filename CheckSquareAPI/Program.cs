using CheckSquareAPI.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<SquareService, SquareService>();

builder.Services.AddSwaggerGen(options =>
{
options.SwaggerDoc("v1", new OpenApiInfo
{
    Version = "v1",
    Title = "Check Squares API",
    Description = "An ASP.NET Core Web API for checking if given set of coordinates produce squares"
});

    string? xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    string path = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
    string? xmlPath = Path.Combine(path, xmlFile);

    //string? xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddSession(options => {options.IdleTimeout = TimeSpan.FromMinutes(10);});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();
app.UseSession();

app.MapControllers();

app.Run();
