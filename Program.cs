using AluguelApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adiciona o contexto do banco de dados
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configura o CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        builder => builder
            .WithOrigins("https://agenda-frontend-one.vercel.app") // Corrigido: sem /login
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddControllers();

var app = builder.Build();

// Usa o CORS
app.UseCors("AllowFrontend");

app.MapControllers();

// Adiciona suporte à porta da Render
var port = Environment.GetEnvironmentVariable("PORT") ?? "5244";
app.Urls.Add($"http://*:{port}");

app.Run();
