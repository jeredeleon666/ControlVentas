using ControlVentas.BaseDeDatos.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace ControlVentas.BaseDeDatos.Servicios
{
    public class ProductoVendidoDto
    {
        public int CodigoProducto { get; set; }
        public string NombreProducto { get; set; } = "";
        public string NombreCategoria { get; set; } = "";
        public int TotalCantidadVendida { get; set; }
        public decimal TotalIngresos { get; set; }
        public int NumeroVentas { get; set; }
    }

    public class ReportesDBService
    {
        private readonly VentasDbContext _context;

        public ReportesDBService(VentasDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductoVendidoDto>> GetCategoriasMasVendidas(int anio, int codigoCategoria)
        {
            Console.WriteLine($"🔄 [{DateTime.Now:HH:mm:ss}] INICIANDO: GetCategoriasMasVendidas (Año: {anio}, Categoría: {codigoCategoria})");

            try
            {
                var productos = new List<ProductoVendidoDto>();
                var connection = _context.Database.GetDbConnection();

                // SQL SIMPLE: Solo obtener TODOS los productos vendidos
                var sql = @"
                    SELECT DISTINCT
                        p.CodigoProducto,
                        p.Nombre AS NombreProducto,
                        c.Nombre AS NombreCategoria,
                        SUM(i.Cantidad) AS TotalCantidadVendida,
                        SUM(i.Cantidad * i.PrecioUnitario) AS TotalIngresos,
                        COUNT(DISTINCT v.CodigoVenta) AS NumeroVentas
                    FROM Ventas v
                    INNER JOIN ItemVenta i ON v.CodigoVenta = i.CodigoVenta
                    INNER JOIN Producto p ON i.CodigoProducto = p.CodigoProducto
                    INNER JOIN Categoria c ON p.CodigoCategoria = c.CodigoCategoria
                    WHERE YEAR(v.Fecha) = @Anio AND c.CodigoCategoria = @CodigoCategoria
                    GROUP BY p.CodigoProducto, p.Nombre, c.Nombre
                    ORDER BY p.Nombre";

                await _context.Database.OpenConnectionAsync();

                using var command = connection.CreateCommand();
                command.CommandText = sql;
                command.Parameters.Add(new SqlParameter("@Anio", anio));
                command.Parameters.Add(new SqlParameter("@CodigoCategoria", codigoCategoria));

                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    productos.Add(new ProductoVendidoDto
                    {
                        CodigoProducto = Convert.ToInt32(reader["CodigoProducto"]),
                        NombreProducto = reader["NombreProducto"].ToString() ?? "",
                        NombreCategoria = reader["NombreCategoria"].ToString() ?? "",
                        TotalCantidadVendida = Convert.ToInt32(reader["TotalCantidadVendida"]),
                        TotalIngresos = Convert.ToDecimal(reader["TotalIngresos"]),
                        NumeroVentas = Convert.ToInt32(reader["NumeroVentas"])
                    });
                }

                Console.WriteLine($"📊 [{DateTime.Now:HH:mm:ss}] DATOS OBTENIDOS: {productos.Count} productos encontrados");
                return productos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ [{DateTime.Now:HH:mm:ss}] ERROR: {ex.Message}");
                throw;
            }
        }

        public async Task<List<(int Codigo, string Nombre)>> GetCodigoYNombreCategorias()
        {
            var categorias = await _context.categorias
                .OrderBy(c => c.Nombre)
                .Select(c => new { c.CodigoCategoria, c.Nombre })
                .ToListAsync();

            return categorias.Select(c => (c.CodigoCategoria, c.Nombre)).ToList();
        }

        public async Task<List<int>> GetAniosDisponiblesVenta()
        {
            var anios = await _context.ventas
                .Select(v => v.Fecha.Year)
                .Distinct()
                .OrderByDescending(year => year)
                .ToListAsync();

            return anios;
        }
    }
}