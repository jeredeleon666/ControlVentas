namespace ControlVentas.BaseDeDatos.Util
{
    public class ProductoVentaReporte
    {
        public int CodigoProducto { get; set; }
        public string NombreProducto { get; set; }
        public string NombreCategoria { get; set; }
        public int TotalCantidadVendida { get; set; }
        public decimal TotalIngresos { get; set; }
        public int NumeroVentas { get; set; }
    }
}
