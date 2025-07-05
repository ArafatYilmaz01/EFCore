using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using EFCoreTutorial.Data.Context;
using System.Text.Json.Serialization; // bunu eklemeyi unutma

var builder = WebApplication.CreateBuilder(args);

// Connection string ile DbContext'i ekle
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "EFCore Tutorial API", 
        Version = "v1" 
    });
});

// Logging (opsiyonel)
builder.Services.AddLogging();

// Controller desteklerini ekle ve JSON döngü önleme ayarını yap
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.MaxDepth = 64; // istersen artırabilirsin
    });

var app = builder.Build();

// Swagger UI'ı aktif et
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "EFCore Tutorial API V1");
    });
}

// HTTPS yönlendirme
app.UseHttpsRedirection();

// Controller endpointlerini kullan
app.MapControllers();

app.Run();
