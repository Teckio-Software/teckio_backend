using ERP_TECKIO.Modelos;

namespace ERP_TECKIO
{
    /// <summary>
    /// Clase para las cuentas contables madre que hereda de la <see cref="CuentaContableAbstract"/>/>
    /// </summary>
    public class CuentaContableDTO : CuentaContableAbstract
    {
        /// <summary>
        /// Identificador único del <c>Rubro</c>
        /// </summary>
        public int IdRubro { get; set; }
        /// <summary>
        /// Descripción del rubro
        /// </summary>
        public string DescripcionRubro { get; set; }
        /// <summary>
        /// Saldo inicial
        /// </summary>
        public decimal SaldoInicial { get; set; }
        /// <summary>
        /// Saldo final
        /// </summary>
        public decimal SaldoFinal { get; set; }
        /// <summary>
        /// Presupuesto actual de la cuenta contable
        /// </summary>
        public decimal Presupuesto { get; set; }
        /// <summary>
        /// Identificador único del <c>CodigoAgrupador</c> del sat
        /// </summary>
        public int IdCodigoAgrupadorSat { get; set; }
        /// <summary>
        /// Descripción del código agrupador
        /// </summary>
        public string DescripcionCodigoAgrupadorSat { get; set; }
        /// <summary>
        /// boleana que se vuelve true cuando esta expandido el nodo
        /// </summary>
        public bool Expandido { get; set; }
        public bool Seleccionado { get; set; }
        /// <summary>
        /// Listado de las cuentas contables hijo
        /// </summary>
        public List<CuentaContableDTO> Hijos { get; set; }

        public string TipoCuentaContableDescripcion { get; set; }
    }
    /// <summary>
    /// Clase para las cuentas contables hijos de las cuentas contables madre
    /// </summary>
    public class CuentaContableHijoDTO
    {
        /// <summary>
        /// Identificador único de la cuenta contable
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Código de la cuenta contable
        /// </summary>
        public string Codigo { get; set; }
        /// <summary>
        /// Descripción de la cuenta contable
        /// </summary>
        public string Descripcion { get; set; }
        /// <summary>
        /// Identificador único del <c>Rubro</c>
        /// </summary>
        public int IdRubro { get; set; }
        /// <summary>
        /// Descripción del rubro
        /// </summary>
        public string DescripcionRubro { get; set; }
        /// <summary>
        /// Tipo de moneda
        /// </summary>
        public int TipoMoneda { get; set; }
        /// <summary>
        /// Saldo inicial
        /// </summary>
        public decimal SaldoInicial { get; set; }
        /// <summary>
        /// Saldo final
        /// </summary>
        public decimal SaldoFinal { get; set; }
        /// <summary>
        /// Presupuesto actual de la cuenta contable
        /// </summary>
        public decimal Presupuesto { get; set; }
        /// <summary>
        /// Identificador único del <c>CodigoAgrupador</c> del sat
        /// </summary>
        public int IdCodigoAgrupadoSat { get; set; }
        /// <summary>
        /// Descripción del código agrupador del sat
        /// </summary>
        public string DescripcionCodigoAgrupadorSat { get; set; }
        /// <summary>
        /// Identificador de la cuenta madre a la que pertence
        /// </summary>
        public int IdCuentaBase { get; set; }
        /// <summary>
        /// Nivel que va del 1 al 7 para las cuentas contables
        /// </summary>
        public int Nivel { get; set; }
        public bool ExisteMovimiento { get; set; }
        public bool ExistePoliza { get; set; }
        /// <summary>
        /// boleana que se vuelve true cuando esta expandido el nodo
        /// </summary>
        public bool Expandido { get; set; }
        /// <summary>
        /// Listado de las cuentas contables hijo
        /// </summary>
        public List<CuentaContableHijoDTO> Hijos { get; set; }
    }
    /// <summary>
    /// Clase para la creación de una nueva cuenta contable 
    /// </summary>
    public class CuentaContableCreacionDTO
    {
        /// <summary>
        /// Código de la empresa
        /// </summary>
        public string Codigo { get; set; }
        /// <summary>
        /// Descripción de la cuenta contable
        /// </summary>
        public string Descripcion { get; set; }
        /// <summary>
        /// Identificador único del <c>Rubro</c>
        /// </summary>
        public int IdRubro { get; set; }
        /// <summary>
        /// Tipo moneda
        /// </summary>
        public int TipoMoneda { get; set; }
        /// <summary>
        /// Identificador único del <c>CodigoAgrupador</c> del sat
        /// </summary>
        public int IdCodigoAgrupadorSat { get; set; }
        /// <summary>
        /// Identificador de la cuenta madre a la que pertence
        /// </summary>
        public int IdPadre { get; set; }
        /// <summary>
        /// Nivel que va del 1 al 7 para las cuentas contables
        /// </summary>
        public int Nivel { get; set; }
        public bool ExisteMovimiento { get; set; }
        public bool ExistePoliza { get; set; }
        /// <summary>
        /// boleana que se vuelve true cuando esta expandido el nodo
        /// </summary>
        public bool Expandido { get; set; }
        /// <summary>
        /// Listado de las cuentas contables hijo
        /// </summary>
        public List<CuentaContableCreacionHijoDTO> Hijos { get; set; }
        /// <summary>
        /// Saldo inicial
        /// </summary>
        public decimal SaldoInicial { get; set; }
        /// <summary>
        /// Saldo final
        /// </summary>
        public decimal SaldoFinal { get; set; }
        /// <summary>
        /// Presupuesto actual de la cuenta contable
        /// </summary>
        public decimal Presupuesto { get; set; }
        public bool? EsCuentaContableEmpresa { get; set; }
        public int? TipoCuentaContable { get; set; }
    }
    /// <summary>
    /// Clase para la creación de una nueva cuenta 
    /// </summary>
    public class CuentaContableCreacionHijoDTO
    {
        /// <summary>
        /// Código de la cuenta contable
        /// </summary>
        public string Codigo { get; set; }
        /// <summary>
        /// Descripción de la cuenta contable
        /// </summary>
        public string Descripcion { get; set; }
        /// <summary>
        /// Identificador único del <c>Rubro</c>
        /// </summary>
        public int IdRubro { get; set; }
        /// <summary>
        /// Tipo de moneda
        /// </summary>
        public int TipoMoneda { get; set; }
        /// <summary>
        /// Identificador único del <c>CodigoAgrupador</c> del sat
        /// </summary>
        public int IdCodigoAgrupadorSat { get; set; }
        /// <summary>
        /// Identificador de la cuenta madre a la que pertence
        /// </summary>
        public int IdPadre { get; set; }
        /// <summary>
        /// Nivel que va del 1 al 7 para las cuentas contables
        /// </summary>
        public int Nivel { get; set; }
        /// <summary>
        /// Boleano que comprueba si ya existe un movimiento en esta
        /// </summary>
        public bool ExisteMovimiento { get; set; }
        public bool ExistePoliza { get; set; }
        /// <summary>
        /// boleana que se vuelve true cuando esta expandido el nodo
        /// </summary>
        public bool Expandido { get; set; }
        /// <summary>
        /// Listado de las cuentas contables hijo
        /// </summary>
        public List<CuentaContableCreacionHijoDTO> Hijos { get; set; }
        /// <summary>
        /// Saldo inicial
        /// </summary>
        public decimal SaldoInicial { get; set; }
        /// <summary>
        /// Saldo final
        /// </summary>
        public decimal SaldoFinal { get; set; }
        /// <summary>
        /// Presupuesto actual de la cuenta contable
        /// </summary>
        public decimal Presupuesto { get; set; }
    }

    public class CuentaContableProveedoresDTO : CuentaContableAbstract
    {
        public DateTime FechaAlta { get; set; }
        /// <summary>
        /// Naturaleza de la cuenta contable, 1 = Acreedor, 0 = Deudor
        /// </summary>
        public bool EsAcreedor { get; set; }

        public bool Estatus { get; set; }
        public List<CuentaContableProveedoresDTO> Hijos { get; set; }
    }
}