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


// ✅ Configuração do CORS para Vercel
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .SetIsOriginAllowed(origin =>
                origin.Contains("vercel.app") &&
                (origin.StartsWith("https://agenda-frontend-") || origin == "https://agenda-frontend-one.vercel.app")
            )
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddControllers();

var app = builder.Build();

// ✅ Pipeline de middlewares
app.UseRouting();
app.UseCors("AllowFrontend"); // Precisa vir após UseRouting
app.UseAuthorization();
app.MapControllers();

// ✅ Porta configurada para Render
var port = Environment.GetEnvironmentVariable("PORT") ?? "5244";
app.Urls.Add($"http://*:{port}");

app.Run();
