using AluguelApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' is missing.");
}

builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(connectionString)
);

// Configuração CORS - ajuste as origens permitidas aqui
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins(
                "https://agenda-frontend-one.vercel.app",
                "https://agenda-frontend-l8bbfbsos-murilo-valentims-projects.vercel.app"
            )
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();

// Aplicar CORS configurado
app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

// Porta configurada para Render
var port = Environment.GetEnvironmentVariable("PORT") ?? "5244";
app.Urls.Add($"http://*:{port}");

app.Run();
