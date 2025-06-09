using ERP_TECKIO.Modelos;

namespace ERP_TECKIO
{
    /// <summary>
    /// Clase CuentaBancariaEmpresaDTO que implementa la interfaz <seealso cref="ICuentaBancaria"/>
    /// </summary>
    public class CuentaBancariaDTO : CuentaBancariaAbstract
    {
        public int IdContratista { get; set; }
        public string NombreBanco { get; set; }
    }
    public class CuentaBancariaClienteDTO : CuentaBancariaAbstract
    {
        public int IdCliente { get; set; }
        public string NombreBanco { get; set; }
    }

    public class CuentaBancariaEmpresasDTO : CuentaBancariaAbstract
    {
        public int IdCuentaContable { get; set; }
        public string NombreBanco { get; set; }
        public bool ExisteCuentaContable { get; set; }
    }

    public class CuentaBancariaCreacionDTO {
        public string NumeroCuenta { get; set; }
        public string NumeroSucursal { get; set; }
        public string Clabe { get; set; }
        public int TiopoMoneda { get; set; }
        public int IdContratista { get; set; }
    }
    public class CuentaBancariaEmpresaDTO
    {
        /// <summary>
        /// Identificador único de la cuenta bancaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador único del banco
        /// </summary>
        public int IdBanco { get; set; }
        /// <summary>
        /// Nombre de la cuenta bancara
        /// </summary>
        public string? Nombre { get; set; }
        /// <summary>
        /// RFC del banco
        /// </summary>
        public string BancoRFC { get; set; }
        /// <summary>
        /// Número de sucursal del banco
        /// </summary>
        public string NumeroSucursal { get; set; }
        /// <summary>
        /// CLABE de la cuenta bancaria de la empresa
        /// </summary>
        public string Clabe { get; set; }

        /// <summary>
        /// Identificador de la cuenta contable de la cuenta bancaria
        /// </summary>
        public int IdCuentaContable { get; set; }
        /// <summary>
        /// Código de la cuenta bancaria
        /// </summary>
        public string? Codigo { get; set; }
        /// <summary>
        /// Aplica topen minimo
        /// </summary>
        public bool TopeMinimo { get; set; }
        /// <summary>
        /// Ultimo número de cheque
        /// </summary>
        public int UltimoNoCheque { get; set; }
        /// <summary>
        /// Fecha de apertura de la cuenta bancaria
        /// </summary>
        public DateTime FechaApertura { get; set; }
        /// <summary>
        /// Tipo de moneda (MXN, USD)
        /// </summary>
        public string TipoMoneda { get; set; }
        /// <summary>
        /// Saldo inicial de la cuenta bancaria
        /// </summary>
        public decimal SaldoInicial { get; set; }
    }

    /// <summary>
    /// Clase que contiene los parametros para filtrar el registro y paginarlo
    /// </summary>
    public class CuentaBancariaEmpresaCreacionDTO
    {
        /// <summary>
        /// RFC del banco
        /// </summary>
        public string BancoRFC { get; set; }
        /// <summary>
        /// Número de sucursal a la que pertenece la cuenta bancaria
        /// </summary>
        public int NumeroSucursal { get; set; }
        /// <summary>
        /// CLABE de la cuenta bancaria de la empresa
        /// </summary>
        public string CLABE { get; set; }
        /// <summary>
        /// Identificador de la cuenta contable de la cuenta bancaria
        /// </summary>
        public int IdCuentaContable { get; set; }
        /// <summary>
        /// Aplica topen minimo
        /// </summary>
        public bool? TopeMinimo { get; set; }
        /// <summary>
        /// Ultimo número de cheque
        /// </summary>
        public int UltimoNoCheque { get; set; }
        /// <summary>
        /// Fecha de apertura de la cuenta bancaria
        /// </summary>
        public DateTime FechaApertura { get; set; }
        /// <summary>
        /// Tipo de moneda (MXN, USD)
        /// </summary>
        public string TipoMoneda { get; set; }
    }
    /// <summary>
    /// Clase utilizada para el almacenamiento de la CLABE de la cuenta bancaria
    /// </summary>
    public class CuentaBancariaObtenCodigoDTO
    {
        /// <summary>
        /// CLABE de la cuenta bancaria de la empresa
        /// </summary>
        public string CLABE { get; set; }
    }
}
