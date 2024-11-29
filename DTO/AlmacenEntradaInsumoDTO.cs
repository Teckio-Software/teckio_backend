namespace ERP_TECKIO
{
    public class AlmacenEntradaInsumoDTO
    {
        public int Id { get; set; }
        public int IdProyecto { get; set; }
        public string NombreProyecto { get; set; }
        public int? IdRequisicion { get; set; }
        public string NoRequisicion { get; set; }
        public int? IdOrdenCompra { get; set; }
        public int IdAlmacenEntrada { get; set; }
        public int IdInsumo { get; set; }
        public string CodigoInsumo { get; set; }
        public string DescripcionInsumo { get; set; }
        public string UnidadInsumo { get; set; }
        public decimal CantidadPorRecibir { get; set; }
        public decimal CantidadRecibida { get; set; }
        public int Estatus { get; set; }
    }

    public class AlmacenEntradaInsumoCreacionDTO
    {
        public int IdInsumo { get; set; }
        public int IdAlmacenEntrada { get; set; }
        public string Descripcion { get; set; }
        public string Unidad { get; set; }
        public int IdTipoInsumo { get; set; }
        public decimal CantidadPorRecibir { get; set; }
        public decimal CantidadRecibIda { get; set; }
        public int IdOrdenCompra { get; set; }
        public int IdInsumoXOrdenCompra { get; set; }
    }
}
