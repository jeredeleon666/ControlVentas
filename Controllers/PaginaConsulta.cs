using ControlVentas.BaseDeDatos.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace ControlVentas.Controllers
{
    public class PaginaConsulta : Controller
    {
        private readonly ReportesDBService _reportesService;

        public PaginaConsulta(ReportesDBService reportesService)
        {
            _reportesService = reportesService;
        }

        // GET: /examen_GuatemalaDigital/ReporteVentasCategoria
        public IActionResult ReporteVentasCategoria()
        {
            var resultado = View();
            return resultado;
        }

        // API endpoint para obtener categorías
        [HttpGet]
        public async Task<IActionResult> GetCategorias()
        {
            try
            {
              
                var categorias = await _reportesService.GetCodigoYNombreCategorias();
             
                if (categorias != null && categorias.Any())
                {
                    Console.WriteLine("📋 CATEGORÍAS OBTENIDAS:");
                    foreach (var categoria in categorias)
                    {
                        Console.WriteLine($"   - {categoria.Codigo}: {categoria.Nombre}");
                    }
                }

                var resultado = categorias.Select(c => new { codigo = c.Codigo, nombre = c.Nombre }).ToList();
                 return Json(resultado);
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, new { error = $"Error al obtener categorías: {ex.Message}" });
            }
        }

        // API endpoint para obtener reporte de productos por categoría y año
        [HttpGet]
        public async Task<IActionResult> GetReporte(int codigoCategoria, int anio)
        {
            try
            {
              
                var productos = await _reportesService.GetCategoriasMasVendidas(anio, codigoCategoria);
             
           

                var resultado = productos.Select(p => new
                {
                    codigoProducto = p.CodigoProducto,
                    nombreProducto = p.NombreProducto,
                    nombreCategoria = p.NombreCategoria,
                    totalCantidadVendida = p.TotalCantidadVendida,
                    totalIngresos = p.TotalIngresos,
                    numeroVentas = p.NumeroVentas
                }).ToList();

            
                return Json(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Error al obtener reporte: {ex.Message}" });
            }
        }

        // Método para redirigir desde Index si es necesario
        public IActionResult Index()
        {
            var resultado = RedirectToAction("ReporteVentasCategoria");
            return resultado;
        }
    }
}