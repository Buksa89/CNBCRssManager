using Microsoft.EntityFrameworkCore;
using CNBCRssManager.API.Data;
using CNBCRssManager.API.Repositories;
using CNBCRssManager.API.Services;
using CNBCRssManager.API;
using Microsoft.AspNetCore.Cors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();

builder.Services.AddDbContext<FeedDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IFeedRepository, FeedRepository>();
builder.Services.AddScoped<IRssFeedService, RssFeedService>();
builder.Services.AddHostedService<RssFeedBackgroundService>();

builder.Services.Configure<RssFeedOptions>(builder.Configuration.GetSection("RssFeedOptions"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowReactApp");
app.UseAuthorization();
app.MapControllers();

app.Run();