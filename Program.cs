using AluguelApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adiciona o contexto do banco de dados
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Configuração do CORS
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
app.UseCors("AllowFrontend"); // A ordem importa: precisa vir após UseRouting
app.UseAuthorization();
app.MapControllers();

// ✅ Porta para compatibilidade com Render
var port = Environment.GetEnvironmentVariable("PORT") ?? "5244";
app.Urls.Add($"http://*:{port}");

app.Run();
