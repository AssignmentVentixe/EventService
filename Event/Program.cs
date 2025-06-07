using Azure.Identity;
using API.Data.Contexts;
using API.Data.Repositories;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddAzureKeyVault(new Uri(builder.Configuration["KeyVault:Uri"]!), new DefaultAzureCredential());

builder.Services.AddControllers();

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IEventService, EventService>();


builder.Services.AddCors(o =>
    o.AddPolicy("CorsPolicy", p => p
        .WithOrigins(
            "https://booking-api-ventixe-e5hydeevg6htf7br.swedencentral-01.azurewebsites.net",
            "https://lively-hill-0b76ba003.6.azurestaticapps.net",
            "https://emailservice-ventixe-ggaghhduh6dyhte8.swedencentral-01.azurewebsites.net"
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()
    )
);

builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();