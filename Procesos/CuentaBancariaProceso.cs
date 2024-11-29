using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;


using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;

namespace ERP_TECKIO
{
    public class CuentaBancariaProceso<T> where T : DbContext
    {
        private readonly ICuentaBancariaService<T> _cuentabancariaservice;
        private readonly ICuentaBancariaClienteService<T> _cuentabancariaclienteservice;
        private readonly ICuentaBancariaEmpresaService<T> _cuentaBancariaEmpresaService;

        private readonly IBancoService<T> _bancoservice;
        public CuentaBancariaProceso(ICuentaBancariaService<T> _cuentaBancariaService,
            ICuentaBancariaEmpresaService<T> cuentaBancariaEmpresaService,
            ICuentaBancariaClienteService<T> cuentabancariaclienteservice,
            IBancoService<T> _bancoservice
            ) {
            this._cuentabancariaservice = _cuentaBancariaService;
            _cuentaBancariaEmpresaService = cuentaBancariaEmpresaService;
            _cuentabancariaclienteservice = cuentabancariaclienteservice;
            this._bancoservice = _bancoservice;
        }

        public async Task<List<CuentaBancariaDTO>> ObtenerXIdContratista(int IdContratista) {
            var bancos = await _bancoservice.ObtenTodos();
            var cuentasbancarias = await _cuentabancariaservice.ObtenXIdContratista(IdContratista);
            foreach (var cuentabancaria in cuentasbancarias) {
                var banco = bancos.Where(z => z.Id == cuentabancaria.IdBanco).ToList();
                cuentabancaria.NombreBanco = banco[0].Nombre;
            }
            return cuentasbancarias;
        }

        public async Task<List<CuentaBancariaClienteDTO>> ObtenerXidCliente(int IdCliente)
        {
            var bancos = await _bancoservice.ObtenTodos();
            var cuentasbancarias = await _cuentabancariaclienteservice.ObtenXIdCliente(IdCliente);
            foreach (var cuentabancaria in cuentasbancarias)
            {
                var banco = bancos.Where(z => z.Id == cuentabancaria.IdBanco).ToList();
                cuentabancaria.NombreBanco = banco[0].Nombre;
            }
            return cuentasbancarias;
        }

        public async Task<List<CuentaBancariaEmpresasDTO>> ObtenerXEmpresa()
        {
            var bancos = await _bancoservice.ObtenTodos();
            var cuentasbancarias = await _cuentaBancariaEmpresaService.ObtenTodos();
            foreach (var cuentabancaria in cuentasbancarias)    
            {
                var banco = bancos.Where(z => z.Id == cuentabancaria.IdBanco).ToList();
                cuentabancaria.NombreBanco = banco[0].Nombre;
            }
            return cuentasbancarias;
        }   

        public async Task<RespuestaDTO> CrearCuentaBancaria(CuentaBancariaCreacionDTO cuentaBancariaCreacion) {
            RespuestaDTO respuesta = new RespuestaDTO();
            respuesta.Estatus = false;
            if (string.IsNullOrEmpty(cuentaBancariaCreacion.NumeroCuenta) || string.IsNullOrEmpty(cuentaBancariaCreacion.Clabe) ||
                string.IsNullOrEmpty(cuentaBancariaCreacion.NumeroSucursal) || cuentaBancariaCreacion.IdContratista <= 0 ||
                cuentaBancariaCreacion.NumeroCuenta.Length > 20 || cuentaBancariaCreacion.Clabe.Length > 18) {
                respuesta.Descripcion = "Llene todos los campos correctamente";
            }
            string clave = cuentaBancariaCreacion.Clabe.Substring(0, 3);
            var bancos = await _bancoservice.ObtenTodos();
            var banco = bancos.Where(z => z.Clave == clave).ToList();
            if (banco.Count <= 0) {
                respuesta.Descripcion = "El banco al que pertenece esta cuenta clabe no esta registrado";
            }
            CuentaBancariaDTO cuentaBancariaDTO = new CuentaBancariaDTO();
            cuentaBancariaDTO.IdBanco = banco[0].Id;
            cuentaBancariaDTO.IdContratista = cuentaBancariaCreacion.IdContratista;
            cuentaBancariaDTO.TipoMoneda = cuentaBancariaCreacion.TiopoMoneda;
            cuentaBancariaDTO.Clabe = cuentaBancariaCreacion.Clabe;
            cuentaBancariaDTO.NumeroSucursal = cuentaBancariaCreacion.NumeroSucursal;
            cuentaBancariaDTO.NumeroCuenta = cuentaBancariaCreacion.NumeroCuenta;
            cuentaBancariaDTO.FechaAlta = DateTime.Now;
            
            var crearCuentaBancaria = await _cuentabancariaservice.CrearYObtener(cuentaBancariaDTO);
            if (crearCuentaBancaria.Id <= 0) {
                respuesta.Descripcion = "No se creo la cuenta bancaria";
            }
            respuesta.Estatus = true;
            respuesta.Descripcion = "Se creó la cuenta bancaria";
            return respuesta;
        }

        public async Task<List<CuentaBancariaDTO>> ObtenerNombresCunetas() {
            List<CuentaBancariaDTO> cuentaBancarias = new List<CuentaBancariaDTO>();
            var query = await _cuentabancariaservice.ObtenTodos();
            var bancos = await _bancoservice.ObtenTodos();
            var lista = query.OrderBy(z => z.Id).ToList();
            if(lista.Count() > 0) {
                for (int i = 0; i < lista.Count; i++) {
                    var cuentaClabe = lista[i].Clabe;
                    var clabe = cuentaClabe.Substring(0,3);
                    var banco =  bancos.Where(z => z.Clave == clabe).ToList();
                    if(banco.Count == 0)
                    {
                        return cuentaBancarias;
                    }
                    else {
                        var nombreBanco = banco[0].Nombre;
                        cuentaBancarias.Add(new CuentaBancariaDTO()
                        {
                            Id = lista[i].Id,
                            NumeroCuenta = lista[i].NumeroCuenta,
                            Clabe = lista[i].Clabe,
                            NombreBanco = nombreBanco
                        });
                    }
                }
                return cuentaBancarias;
            }
            return cuentaBancarias;
        }
    }
}
