namespace ERP_TECKIO.DTO.Factura
{
    public class ProductoYServicioConjuntoDTO
    {
        //Valores de productoservicio
        public int Id { get; set; }

        public string? CodigoPS { get; set; }

        public string? DescripcionPS { get; set; }

        //Valores de unidad
        public int IdUnidad { get; set; }

        public string? DescripcionUnidad { get; set; }

        //Valores de productoserviciosat
        public int IdPSS { get; set; }

        public string? ClavePSS { get; set; }

        public string? DescripcionPSS { get; set; }

        //Valores de unidadsat
        public int IdUnidSSat {  get; set; }
        public string? TipoUS { get; set; }
        public string? ClaveUS { get; set; }
        public string? NombreUs { get; set; }

        //Valores de categoria productoservicio
        public int IdCPS { get; set; }
        public string? DescripcionCPS { get; set; }

        //Valores de subcategoria productoservicio
        public int IdSPS { get; set; }
        public string? DescripcionSPS { get; set; }

    }
}
