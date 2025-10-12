using ERP_TECKIO.DTO;
using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Modelos;

namespace ERP_TECKIO
{
    /// <summary>
    /// Este DTO es para afectar el monto presupuestado de los proyectos
    /// </summary>
    public class MovimientoBancarioTeckioDTO : MovimientoBancarioAbstract
    {
        public int IdBeneficiario { get; set; }
        public int IdCuentaBancaria { get; set; }
        public int IdMovimientoBancario { get; set; }
        public string beneficiario { get; set; }
        public string descripcionModalidad { get; set; }
        public string descripcionMoneda { get; set; }
        public string descripcionEstatus { get; set; }
        public decimal saldo { get; set; }
        public bool EsFactura { get; set; }
        public bool EsOrdenCompra { get; set; }
        public bool EsFacturaOrdenVenta { get; set; }
        public bool EsOrdenVenta { get; set; }

        public List<FacturaXOrdenCompraDTO> FacturasXOrdenCompra { get; set; } = new List<FacturaXOrdenCompraDTO>();
        public List<OrdenCompraDTO> OrdenCompras { get; set; } = new List<OrdenCompraDTO>();
        public List<FacturaXOrdenVentaDTO> FacturasXOrdenVenta { get; set; } = new List<FacturaXOrdenVentaDTO>();
        public List<OrdenVentaDTO> OrdenVentas { get; set; } = new List<OrdenVentaDTO>();
    } 
    public class MovimientoBancarioProyectoDTO
    {
        /// <summary>
        /// Identificador único del movimiento bancario
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Número de movimiento bancario
        /// </summary>
        public int NoMovBancario { get; set; }
        /// <summary>
        /// Fecha de alta del movimiento bancario
        /// </summary>
        public DateTime FechaAlta { get; set; }
        /// <summary>
        /// Fecha de aplicación del movimiento bancario
        /// </summary>
        public DateTime FechaAplicacion { get; set; }
        /// <summary>
        /// Fecha de cobro del movimiento bancario (normalmente la misma que la fecha de aplicación
        /// </summary>
        public DateTime FechaCobro { get; set; }
        /// <summary>
        /// Código del tipo de movimiento bancario (PAP, PAC, PAT, PRP, PRC y PRI)
        /// </summary>
        public string ModalIdad { get; set; }
        /// <summary>
        /// Número de cheque
        /// </summary>
        public string? NoCheque { get; set; }
        /// <summary>
        /// Número de referencia
        /// </summary>
        public string? Referencia { get; set; }
        /// <summary>
        /// Identificador del <c>Contratista</c>
        /// </summary>
        public int IdContratista { get; set; }
        /// <summary>
        /// Identificador de la cuenta bancaria del contratista
        /// </summary>
        public int IdContratistaCuentaBancaria { get; set; }
        /// <summary>
        /// CLABE de la cuenta bancaria del contratista
        /// </summary>
        public string ContratistaCuentaBancariaClabe { get; set; }
        /// <summary>
        /// Número de cuenta de la cuenta bancaria del contratista
        /// </summary>
        public string ContratistaCuentaBancariaNumeroCuenta { get; set; }
        /// <summary>
        /// Nombre del beneficiario
        /// </summary>
        public string Beneficiario { get; set; }
        /// <summary>
        /// Monto del movimiento bancario si corresponde a un deposito
        /// </summary>
        public decimal Deposito { get; set; }
        /// <summary>
        /// Monto del movimiento bancario si corresponde a un retiro
        /// </summary>
        public decimal Retiro { get; set; }
        /// <summary>
        /// Monto final del movimiento bancario.
        /// </summary>
        public decimal Saldo { get; set; }
        /// <summary>
        /// Corresponde al código de la poliza creada por movimiento bancario
        /// Si no hay es sin aplicar
        /// </summary>
        public string NoPoliza { get; set; }
        /// <summary>
        /// Corresponde al estatus Autorizado, capturada, liberada, cancelada
        /// </summary>
        public string Estatus { get; set; }
        /// <summary>
        /// Descripción del concepto del movimiento bancario
        /// </summary>
        public string Concepto { get; set; }
        /// <summary>
        /// Identificador de la cuenta bancaria de la empresa
        /// </summary>
        public int IdCuentaBancariaEmpresa { get; set; }
        /// <summary>
        /// CLABE de la cuenta bancaria de la empresa
        /// </summary>
        public string ClabeCuentaBancariaEmpresa { get; set; }
        /// <summary>
        /// Si la modalIdad es un pago/prestamo a tercera persona se captura el nombre de la tercera persona
        /// </summary>
        public string? NombreTercerPersona { get; set; }
        /// <summary>
        /// Si la modalIdad es un pago/prestamo a tercera persona se captura el RFC de la tercera persona
        /// </summary>
        public string? RfcTerceraPersona { get; set; }
        /// <summary>
        /// Para si es pago, prestamo, deposito
        /// </summary>
        public string Naturaleza { get; set; }
        /// <summary>
        /// Identificador del proyecto al que pertence un movimiento bancario
        /// </summary>
        public int IdProyecto { get; set; }
        /// <summary>
        /// Descripción del proyecto
        /// </summary>
        public string DescripcionProyecto { get; set; }
    }
    /// <summary>
    /// Este DTO es para obtener los montos de los pedIdos
    /// </summary>
    public class MovimientoBancarioPedIdoDTO
    {
        /// <summary>
        /// Identificador único del movimiento bancario
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Número de movimiento bancario
        /// </summary>
        public int NoMovBancario { get; set; }
        /// <summary>
        /// Fecha de alta del movimiento bancario
        /// </summary>
        public DateTime FechaAlta { get; set; }
        /// <summary>
        /// Fecha de aplicación del movimiento bancario
        /// </summary>
        public DateTime FechaAplicacion { get; set; }
        /// <summary>
        /// Fecha de cobro del movimiento bancario (normalmente la misma que la fecha de aplicación
        /// </summary>
        public DateTime FechaCobro { get; set; }
        /// <summary>
        /// Código del tipo de movimiento bancario (PAP, PAC, PAT, PRP, PRC y PRI)
        /// </summary>
        public string ModalIdad { get; set; }
        /// <summary>
        /// Número de cheque
        /// </summary>
        public string? NoCheque { get; set; }
        /// <summary>
        /// Número de referencia
        /// </summary>
        public string? Referencia { get; set; }
        /// <summary>
        /// Identificador del <c>Contratista</c>
        /// </summary>
        public int IdContratista { get; set; }
        /// <summary>
        /// Identificador de la cuenta bancaria del contratista
        /// </summary>
        public int IdContratistaCuentaBancaria { get; set; }
        /// <summary>
        /// CLABE de la cuenta bancaria del contratista
        /// </summary>
        public string ContratistaCuentaBancariaClabe { get; set; }
        /// <summary>
        /// Número de cuenta de la cuenta bancaria del contratista
        /// </summary>
        public string ContratistaCuentaBancariaNumeroCuenta { get; set; }
        /// <summary>
        /// Nombre del beneficiario
        /// </summary>
        public string Beneficiario { get; set; }
        /// <summary>
        /// Monto del movimiento bancario si corresponde a un deposito
        /// </summary>
        public decimal Deposito { get; set; }
        /// <summary>
        /// Monto del movimiento bancario si corresponde a un retiro
        /// </summary>
        public decimal Retiro { get; set; }
        /// <summary>
        /// Monto final del movimiento bancario.
        /// </summary>
        public decimal Saldo { get; set; }
        /// <summary>
        /// Corresponde al código de la poliza creada por movimiento bancario
        /// Si no hay es sin aplicar
        /// </summary>
        public string NoPoliza { get; set; }
        /// <summary>
        /// Corresponde al estatus Autorizado, capturada, liberada, cancelada
        /// </summary>
        public string Estatus { get; set; }
        /// <summary>
        /// Descripción del concepto del movimiento bancario
        /// </summary>
        public string Concepto { get; set; }
        /// <summary>
        /// Identificador de la cuenta bancaria de la empresa
        /// </summary>
        public int IdCuentaBancariaEmpresa { get; set; }
        /// <summary>
        /// CLABE de la cuenta bancaria de la empresa
        /// </summary>
        public string ClabeCuentaBancariaEmpresa { get; set; }
        /// <summary>
        /// Si la modalIdad es un pago/prestamo a tercera persona se captura el nombre de la tercera persona
        /// </summary>
        public string? NombreTercerPersona { get; set; }
        /// <summary>
        /// Si la modalIdad es un pago/prestamo a tercera persona se captura el RFC de la tercera persona
        /// </summary>
        public string? RfcTerceraPersona { get; set; }
        /// <summary>
        /// Para si es pago, prestamo, deposito
        /// </summary>
        public string Naturaleza { get; set; }
        /// <summary>
        /// Identificador del proyecto al que pertence un movimiento bancario
        /// </summary>
        public int IdProyecto { get; set; }
        /// <summary>
        /// Descripción del proyecto
        /// </summary>
        public string DescripcionProyecto { get; set; }
    }
    ///Nota: Puedes obtener una factura varias veces hasta que quede liquIdad en su totalIdad
    /// <summary>
    /// Este DTO es para obtener las los montos de las facturas
    /// </summary>
    public class MovimientoBancarioFacturaDTO
    {
        /// <summary>
        /// Identificador único del movimiento bancario
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Número de movimiento bancario
        /// </summary>
        public int NoMovBancario { get; set; }
        /// <summary>
        /// Fecha de alta del movimiento bancario
        /// </summary>
        public DateTime FechaAlta { get; set; }
        /// <summary>
        /// Fecha de aplicación del movimiento bancario
        /// </summary>
        public DateTime FechaAplicacion { get; set; }
        /// <summary>
        /// Fecha de cobro del movimiento bancario (normalmente la misma que la fecha de aplicación
        /// </summary>
        public DateTime FechaCobro { get; set; }
        /// <summary>
        /// Código del tipo de movimiento bancario (PAP, PAC, PAT, PRP, PRC y PRI)
        /// </summary>
        public string ModalIdad { get; set; }
        /// <summary>
        /// Número de cheque
        /// </summary>
        public string? NoCheque { get; set; }
        /// <summary>
        /// Número de referencia
        /// </summary>
        public string? Referencia { get; set; }
        /// <summary>
        /// Identificador del <c>Contratista</c>
        /// </summary>
        public int IdContratista { get; set; }
        /// <summary>
        /// Identificador de la cuenta bancaria del contratista
        /// </summary>
        public int IdContratistaCuentaBancaria { get; set; }
        /// <summary>
        /// CLABE de la cuenta bancaria del contratista
        /// </summary>
        public string ContratistaCuentaBancariaClabe { get; set; }
        /// <summary>
        /// Número de cuenta de la cuenta bancaria del contratista
        /// </summary>
        public string ContratistaCuentaBancariaNumeroCuenta { get; set; }
        /// <summary>
        /// Nombre del beneficiario
        /// </summary>
        public string Beneficiario { get; set; }
        /// <summary>
        /// Monto del movimiento bancario si corresponde a un deposito
        /// </summary>
        public decimal Deposito { get; set; }
        /// <summary>
        /// Monto del movimiento bancario si corresponde a un retiro
        /// </summary>
        public decimal Retiro { get; set; }
        /// <summary>
        /// Monto final del movimiento bancario.
        /// </summary>
        public decimal Saldo { get; set; }
        /// <summary>
        /// Corresponde al código de la poliza creada por movimiento bancario
        /// Si no hay es sin aplicar
        /// </summary>
        public string NoPoliza { get; set; }
        /// <summary>
        /// Corresponde al estatus Autorizado, capturada, liberada, cancelada
        /// </summary>
        public string Estatus { get; set; }
        /// <summary>
        /// Descripción del concepto del movimiento bancario
        /// </summary>
        public string Concepto { get; set; }
        /// <summary>
        /// Identificador de la cuenta bancaria de la empresa
        /// </summary>
        public int IdCuentaBancariaEmpresa { get; set; }
        /// <summary>
        /// CLABE de la cuenta bancaria de la empresa
        /// </summary>
        public string ClabeCuentaBancariaEmpresa { get; set; }
        /// <summary>
        /// Si la modalIdad es un pago/prestamo a tercera persona se captura el nombre de la tercera persona
        /// </summary>
        public string? NombreTercerPersona { get; set; }
        /// <summary>
        /// Si la modalIdad es un pago/prestamo a tercera persona se captura el RFC de la tercera persona
        /// </summary>
        public string? RfcTerceraPersona { get; set; }
        /// <summary>
        /// Para si es pago, prestamo, deposito
        /// </summary>
        public string Naturaleza { get; set; }
        /// <summary>
        /// Identificador del proyecto al que pertence un movimiento bancario
        /// </summary>
        public int IdProyecto { get; set; }
        /// <summary>
        /// Descripción del proyecto
        /// </summary>
        public string DescripcionProyecto { get; set; }
    }
    /// <summary>
    /// Este DTO es para mostrar los movimientos bancarios
    /// </summary>
    public class MovimientoBancarioDTO
    {
        /// <summary>
        /// Identificador único del movimiento bancario
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Número de movimiento bancario
        /// </summary>
        public int NoMovBancario { get; set; }
        /// <summary>
        /// Fecha de alta del movimiento bancario
        /// </summary>
        public DateTime FechaAlta { get; set; }
        /// <summary>
        /// Fecha de aplicación del movimiento bancario
        /// </summary>
        public DateTime FechaAplicacion { get; set; }
        /// <summary>
        /// Fecha de cobro del movimiento bancario (normalmente la misma que la fecha de aplicación
        /// </summary>
        public DateTime FechaCobro { get; set; }
        /// <summary>
        /// Código del tipo de movimiento bancario (PAP, PAC, PAT, PRP, PRC y PRI)
        /// </summary>
        public string ModalIdad { get; set; }
        /// <summary>
        /// Número de cheque
        /// </summary>
        public string? NoCheque { get; set; }
        /// <summary>
        /// Número de referencia
        /// </summary>
        public string? Referencia { get; set; }
        /// <summary>
        /// Identificador del <c>Contratista</c>
        /// </summary>
        public int IdContratista { get; set; }
        /// <summary>
        /// Identificador de la cuenta bancaria del contratista
        /// </summary>
        public int IdContratistaCuentaBancaria { get; set; }
        /// <summary>
        /// CLABE de la cuenta bancaria del contratista
        /// </summary>
        public string ContratistaCuentaBancariaClabe { get; set; }
        /// <summary>
        /// Número de cuenta de la cuenta bancaria del contratista
        /// </summary>
        public string ContratistaCuentaBancariaNumeroCuenta { get; set; }
        /// <summary>
        /// Nombre del beneficiario
        /// </summary>
        public string Beneficiario { get; set; }
        /// <summary>
        /// Monto del movimiento bancario si corresponde a un deposito
        /// </summary>
        public decimal Deposito { get; set; }
        /// <summary>
        /// Monto del movimiento bancario si corresponde a un retiro
        /// </summary>
        public decimal Retiro { get; set; }
        /// <summary>
        /// Monto final del movimiento bancario.
        /// </summary>
        public decimal Saldo { get; set; }
        /// <summary>
        /// Corresponde al código de la poliza creada por movimiento bancario
        /// Si no hay es sin aplicar
        /// </summary>
        public string NoPoliza { get; set; }
        /// <summary>
        /// Corresponde al estatus Autorizado, capturada, liberada, cancelada
        /// </summary>
        public string Estatus { get; set; }
        /// <summary>
        /// Descripción del concepto del movimiento bancario
        /// </summary>
        public string Concepto { get; set; }
        /// <summary>
        /// Identificador de la cuenta bancaria de la empresa
        /// </summary>
        public int IdCuentaBancariaEmpresa { get; set; }
        /// <summary>
        /// CLABE de la cuenta bancaria de la empresa
        /// </summary>
        public string ClabeCuentaBancariaEmpresa { get; set; }
        /// <summary>
        /// Si la modalIdad es un pago/prestamo a tercera persona se captura el nombre de la tercera persona
        /// </summary>
        public string? NombreTercerPersona { get; set; }
        /// <summary>
        /// Si la modalIdad es un pago/prestamo a tercera persona se captura el RFC de la tercera persona
        /// </summary>
        public string? RfcTerceraPersona { get; set; }
        /// <summary>
        /// Para si es pago, prestamo, deposito
        /// </summary>
        public string Naturaleza { get; set; } //depositos (cargos) y Retiros (abonos)
        /// <summary>
        /// Identificador del proyecto al que pertence un movimiento bancario
        /// </summary>
        public int IdProyecto { get; set; }
        /// <summary>
        /// Descripción del proyecto
        /// </summary>
        public string DescripcionProyecto { get; set; }
        /// <summary>
        /// Si aplica DIOT
        /// </summary>
        public bool AplicaDiot { get; set; } // si aplica es el total del monto 
        /// <summary>
        /// En caso de no ser pesos mexicanos se captura el tipo de cmabio diferente de 1
        /// </summary>
        public decimal TipoCambio { get; set; }
        /// <summary>
        /// Tipo de movimiento
        /// </summary>
        public int TipoMovimiento { get; set; }
        /// <summary>
        /// Resultado del monto por el tipo de cambio
        /// </summary>
        public decimal MontoTotal { get; set; }
    }
    /// <summary>
    /// Este DTO es para la creación de un nuevo movimiento bancario
    /// </summary>
    public class MovimientoBancarioCreacionDTO
    {
        /// <summary>
        /// Número de movimiento bancario
        /// </summary>
        public int NoMovBancario { get; set; }
        /// <summary>
        /// Fecha de alta del movimiento bancario
        /// </summary>
        public DateTime FechaAlta { get; set; }
        /// <summary>
        /// Fecha de aplicación del movimiento bancario
        /// </summary>
        public DateTime FechaAplicacion { get; set; }
        /// <summary>
        /// Fecha de cobro del movimiento bancario (normalmente la misma que la fecha de aplicación
        /// </summary>
        public DateTime FechaCobro { get; set; }
        /// <summary>
        /// Código del tipo de movimiento bancario (PAP, PAC, PAT, PRP, PRC y PRI)
        /// </summary>
        public string ModalIdad { get; set; }
        /// <summary>
        /// Número de cheque
        /// </summary>
        public string? NoCheque { get; set; }
        /// <summary>
        /// Número de referencia
        /// </summary>
        public string? Referencia { get; set; }
        /// <summary>
        /// Identificador del <c>Contratista</c>
        /// </summary>
        public int IdContratista { get; set; }
        /// <summary>
        /// Identificador de la <c>ContratistaCuentaBancariaDTO</c>
        /// </summary>
        public int IdContratistaCuentaBancaria { get; set; }
        //public string Beneficiario { get; set; }
        /// <summary>
        /// Monto del movimiento bancario si corresponde a un deposito
        /// </summary>
        public decimal Deposito { get; set; }
        /// <summary>
        /// Monto del movimiento bancario si corresponde a un retiro
        /// </summary>
        public decimal Retiro { get; set; }
        /// <summary>
        /// Monto final del movimiento bancario.
        /// </summary>
        public decimal Saldo { get; set; }
        /// <summary>
        /// Aplica si es abono a cuenta, no es abono es retiro
        /// </summary>
        public bool AbonoACuenta { get; set; }
        /// <summary>
        /// Aplica si se quiere generar una nueva poliza
        /// </summary>
        public bool GeneraPoliza { get; set; }
        /// <summary>
        /// Corresponde al código de la poliza creada por movimiento bancario
        /// Si no hay es sin aplicar
        /// </summary>
        public string? NoPoliza { get; set; }//Si no hay es sin aplicar
        /// <summary>
        /// Corresponde al estatus Autorizado, capturada, liberada, cancelada
        /// </summary>
        public string Estatus { get; set; }
        /// <summary>
        /// Descripción del concepto del movimiento bancario
        /// </summary>
        public string Concepto { get; set; }
        /// <summary>
        /// Identificador de la cuenta bancaria de la empresa
        /// </summary>
        public int IdCuentaBancariaEmpresa { get; set; }
        /// <summary>
        /// Si la modalIdad es un pago/prestamo a tercera persona se captura el nombre de la tercera persona
        /// </summary>
        public string? NombreTercerPersona { get; set; }
        /// <summary>
        /// Si la modalIdad es un pago/prestamo a tercera persona se captura el RFC de la tercera persona
        /// </summary>
        public string? RfcTerceraPersona { get; set; }
        /// <summary>
        /// Para si es pago, prestamo, deposito
        /// </summary>
        public string Naturaleza { get; set; }
        /// <summary>
        /// Identificador del proyecto al que pertence un movimiento bancario
        /// </summary>
        public int IdProyecto { get; set; }
        /// <summary>
        /// Si aplica DIOT
        /// </summary>
        public bool AplicaDiot { get; set; } // si aplica es el total del monto 
        /// <summary>
        /// Si es MXN o USD
        /// </summary>
        public string TipoMoneda { get; set; }
        /// <summary>
        /// En caso de no ser pesos mexicanos se captura el tipo de cmabio diferente de 1
        /// </summary>
        public decimal TipoCambio { get; set; }
        /// <summary>
        /// Tipo de movimiento
        /// </summary>
        public string TipoMovimiento { get; set; }
        /// <summary>
        /// Resultado del monto por el tipo de cambio
        /// </summary>
        public decimal MontoTotal { get; set; }
        /// <summary>
        /// Se calcula el monto conforme al porcentaje de la DIOT
        /// </summary>
        public decimal MontoBaseDiot { get; set; }
        /// <summary>
        /// Porcentaje del IVA en DIOT
        /// </summary>
        public decimal TasaIvaDiot { get; set; }
        /// <summary>
        /// Tipo de gastos directos
        /// </summary>
        public string? TipoGastoDirecto { get; set; }
        /// <summary>
        /// Tipo de movimiento del proyecto
        /// </summary>
        public string? TipoMovimientoProyecto { get; set; }
    }
}
