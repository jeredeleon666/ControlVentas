using ControlVentas.BaseDeDatos.Context;
using ControlVentas.BaseDeDatos.Servicios;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Mantener nombres originales
        options.JsonSerializerOptions.WriteIndented = true; // JSON formateado
    });

// Configurar Entity Framework con SQL Server
builder.Services.AddDbContext<VentasDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

    // Solo en desarrollo - mostrar queries en consola
    if (builder.Environment.IsDevelopment())
    {
        options.EnableSensitiveDataLogging();
        options.EnableDetailedErrors();
    }
});

// Registrar servicios de la base de datos
builder.Services.AddScoped<ReportesDBService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "Control Ventas API",
        Version = "v1",
        Description = "API para sistema de control de ventas"
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Control Ventas API V1");
        c.RoutePrefix = "swagger"; // Swagger en /swagger
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("AllowLocalhost");
app.UseRouting();
app.UseAuthorization();

// IMPORTANTE: El orden de las rutas importa - más específicas primero

// Ruta principal del examen - esta es la página de inicio
app.MapControllerRoute(
    name: "examen_principal",
    pattern: "examen_GuatemalaDigital/{action=ReporteVentasCategoria}",
    defaults: new { controller = "PaginaConsulta" });

// Ruta alternativa para APIs del controlador
app.MapControllerRoute(
    name: "examen_api",
    pattern: "PaginaConsulta/{action}",
    defaults: new { controller = "PaginaConsulta" });

// Ruta por defecto (menor prioridad)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=PaginaConsulta}/{action=ReporteVentasCategoria}/{id?}");

// Redirecciones de URLs comunes a la página principal
app.MapGet("/", () => Results.Redirect("/examen_GuatemalaDigital/ReporteVentasCategoria"));
app.MapGet("/index", () => Results.Redirect("/examen_GuatemalaDigital/ReporteVentasCategoria"));
app.MapGet("/home", () => Results.Redirect("/examen_GuatemalaDigital/ReporteVentasCategoria"));



app.Run();