using AluguelApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// conexão com banco
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrWhiteSpace(connectionString))
    throw new InvalidOperationException("Connection string 'DefaultConnection' is missing.");

builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(connectionString)
);

// Configuração de CORS
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
            .AllowAnyHeader()
            .WithExposedHeaders("Access-Control-Allow-Origin");
    });
});

builder.Services.AddControllers();

var app = builder.Build();

// HTTPS redirection
app.UseHttpsRedirection();

app.UseRouting();

// Se quiser testar temporariamente aceitando qualquer origem:
// policy => policy.SetIsOriginAllowed(_ => true)...

app.UseCors("AllowFrontend");

app.UseAuthorization();

// **Adicionar endpoint explícito para lidar com preflight OPTIONS**
app.MapMethods("{*path}", new[] { "OPTIONS" }, (HttpContext context) =>
{
    context.Response.StatusCode = 204;
    context.Response.Headers["Access-Control-Allow-Origin"] = context.Request.Headers["Origin"].ToString();
    context.Response.Headers["Access-Control-Allow-Methods"] = "GET,POST,PUT,DELETE,OPTIONS";
    context.Response.Headers["Access-Control-Allow-Headers"] = "Content-Type,Authorization";

    return Task.CompletedTask;
});

// Rotear controllers
app.MapControllers();

// Porta dinâmica para Render
var port = Environment.GetEnvironmentVariable("PORT") ?? "5244";
app.Urls.Add($"http://*:{port}");

app.Run();
