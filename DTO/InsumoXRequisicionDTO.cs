

namespace ERP_TECKIO
{
    /// <summary>
    /// Clase <c>InsumoXRequisicionDTO</c> que implementa la interfaz <seealso cref="IInsumoXRequisicion"/>
    /// </summary>
    public class InsumoXRequisicionDTO
    {
        /// <summary>
        /// Identificador de los Insumos Por Requisición
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador de <c>Requisición</c> <seealso cref="RequisicionDTO"/>
        /// </summary>
        public int IdRequisicion { get; set; }
        /// <summary>
        /// Identificador del <c>Insumo</c> <seealso cref="InsumoDTO"/>
        /// </summary>
        public int IdInsumo { get; set; }
        /// <summary>
        /// Codigo del <c>Insumo</c> <seealso cref="InsumoDTO"/>
        /// </summary>
        //public string Codigo { get; set; }
        /// <summary>
        /// Descripción del <c>Insumo</c> <seealso cref="InsumoDTO"/>
        /// </summary>
        public string Descripcion { get; set; }
        /// <summary>
        /// Unidad del <c>Insumo</c> <seealso cref="InsumoDTO"/>
        /// </summary>
        public string Unidad { get; set; }
        /// <summary>
        /// 1= denominación ordinaria, 2= urgente
        /// </summary>
        public int Denominacion { get; set; }
        /// <summary>
        /// Iniciales de la persona que solicita el insumo
        /// </summary>
        public string? PersonaIniciales { get; set; }
        /// <summary>
        /// Número de la requisción más el número del insumo
        /// </summary>
        public string Folio { get; set; }
        /// <summary>
        /// Cantidad del insumo
        /// </summary>
        public decimal Cantidad { get; set; }
        /// <summary>
        /// Cantidad del insumo comprado
        /// </summary>
        public decimal CantidadComprada { get; set; }
        /// <summary>
        /// Cantidad del Insumo en almacen
        /// </summary>
        public decimal CantidadEnAlmacen { get; set; }
        /// <summary>
        /// Observaciones
        /// </summary>
        public string Observaciones { get; set; }
        /// <summary>
        /// 1 = capturada, 2 = autorizada, 3 = En cotización, 4 = Cancelado, 5 = Comprado
        /// </summary>
        public int EstatusInsumoRequisicion { get; set; }
        /// <summary>
        /// 1 = compra parcial, 2 = compra total, 3 = compra con excedentes
        /// </summary>
        public int EstatusInsumoComprado { get; set; }
        /// <summary>
        /// 1 = surtida parcialmente, 2 = surtIda en su totalidad, 3 = surtida con excedentes
        /// </summary>
        public int EstatusInsumoSurtido { get; set; }

        public string EstatusInsumoCompradoDescripcion { get; set; }
        /// <summary>
        /// 1 = surtida parcialmente, 2 = surtIda en su totalidad, 3 = surtida con excedentes
        /// </summary>
        public string EstatusInsumoSurtidoDescripcion { get; set; }

        public DateTime FechaEntrega { get; set; }
    }

    /// <summary>
    /// Clase <c>InsumoXRequisicionCreacion</c>
    /// </summary>
    public class InsumoXRequisicionCreacionDTO
    {
        public int Id { get; set; }
        /// <summary>
        /// Descripción del <c>Insumo</c> <seealso cref="InsumoDTO"/>
        /// </summary>
        public int IdRequisicion { get; set; }
        /// 
        public string Descripcion { get; set; }
        /// <summary>
        /// Unidad del <c>Insumo</c> <seealso cref="InsumoDTO"/>
        /// </summary>
        public string Unidad { get; set; }
        /// <summary>
        /// 1= denominación ordinaria, 2= urgente
        /// </summary>
        public int Denominacion { get; set; }
        /// <summary>
        /// Iniciales de la persona que solicita el insumo
        /// </summary>
        public string? PersonaIniciales { get; set; }
        public string Folio { get; set; }
        /// <summary>
        /// Cantidad del insumo
        /// </summary>
        public decimal Cantidad { get; set; }
        /// <summary>
        /// Observaciones
        /// </summary>
        public string Observaciones { get; set; }

        public DateTime FechaEntrega { get; set; }
        public int idTipoInsumo { get; set; }
        public int idInsumo { get; set; }
        public decimal cUnitario { get; set; }
        public decimal cPresupuestada { get; set; }


    }

}
