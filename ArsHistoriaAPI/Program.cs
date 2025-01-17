using ArsHistoriaAPI.Data;
using ArsHistoriaAPI.Services;
using ArsHistoriaAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ArsHistoriaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ArsHistoriaDbContext") ?? throw new InvalidOperationException("Connection string 'ArsHistoriaDbContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IStyleService, StyleService>();
builder.Services.AddScoped<IArticleService, ArticleService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<ArsHistoriaDbContext>();
        context.Database.EnsureCreated();
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
