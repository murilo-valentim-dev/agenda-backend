using AluguelApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Conexão com o banco
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                      ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is missing.");

builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(connectionString));

// 2. CORS – Libera Vercel (troque pelos domínios corretos do seu projeto)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
                "https://agenda-frontend-one.vercel.app",
                "https://agenda-frontend-l8bbfbsos-murilo-valentims-projects.vercel.app",
                "https://agenda-frontend-axemnmnez-murilo-valentims-projects.vercel.app" // esse é o novo!
            )
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

// 3. Adiciona Controllers
builder.Services.AddControllers();

var app = builder.Build();

// 4. Middleware
app.UseRouting();
app.UseCors("AllowFrontend");
app.UseAuthorization();
app.MapControllers();

// 5. Pré-resposta para requisições OPTIONS (preflight)
app.Use(async (context, next) =>
{
    if (context.Request.Method == "OPTIONS")
    {
        context.Response.StatusCode = 204;
        context.Response.Headers["Access-Control-Allow-Origin"] = context.Request.Headers["Origin"].ToString();
        context.Response.Headers["Access-Control-Allow-Methods"] = "GET,POST,PUT,DELETE,OPTIONS";
        context.Response.Headers["Access-Control-Allow-Headers"] = "Content-Type,Authorization";
        await context.Response.CompleteAsync();
        return;
    }

    await next();
});

// 6. Configura porta da Render
var port = Environment.GetEnvironmentVariable("PORT") ?? "5244";
app.Urls.Add($"http://*:{port}");

app.Run();
