using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class CuentaContableService<T> : ICuentaContableService<T> where T : DbContext
    {
        private readonly IGenericRepository<CuentaContable, T> _Repositorio;
        private readonly IMapper _Mapper;

        public CuentaContableService(
            IGenericRepository<CuentaContable, T> repositorio
            , IMapper mapper)
        {
            _Repositorio = repositorio;
            _Mapper = mapper;
        }

        public async Task<List<CuentaContableDTO>> ObtenTodos()
        {
            try
            {
                var lista = await _Repositorio.ObtenerTodos();
                return _Mapper.Map<List<CuentaContableDTO>>(lista);
            }
            catch
            {
                return new List<CuentaContableDTO>();
            }
        }

        public async Task<List<CuentaContableDTO>> ObtenHijos(List<CuentaContableDTO> cuentasContables, CuentaContableDTO cuentaContable)
        {
            try
            {
                var cuentasContablesHijos = cuentasContables.Where(z => z.IdPadre == cuentaContable.Id).ToList();
                if(cuentasContablesHijos.Count >= 0)
                {
                    return new List<CuentaContableDTO>();
                }
                for(int i = 0; i < cuentasContablesHijos.Count; i++)
                {
                    var hijosCuentasContables = await ObtenHijos(cuentasContables, cuentasContablesHijos[i]);
                    cuentasContablesHijos[i].Hijos = hijosCuentasContables;
                }
                return cuentasContablesHijos;
            }
            catch
            {
                return new List<CuentaContableDTO>();
            }
        }

        public async Task<CuentaContableDTO> ObtenXId(int Id)
        {
            try
            {
                var lista = await _Repositorio.Obtener(z => z.Id == Id);
                return _Mapper.Map<CuentaContableDTO>(lista);
            }
            catch
            {
                return new CuentaContableDTO();
            }
        }

        public async Task<RespuestaDTO> Eliminar(int Id)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await _Repositorio.Obtener(u => u.Id == Id);

                if (objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La cuenta contable no existe";
                    return respuesta;
                }
                respuesta.Estatus = await _Repositorio.Eliminar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Descripcion = "No se pudo eliminar";
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Cuenta contable eliminada";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la eliminación de la cuenta contable";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> Editar(CuentaContableDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var modelo = _Mapper.Map<CuentaContable>(parametro);
                var objetoEncontrado = await _Repositorio.Obtener(u => u.Id == modelo.Id);
                if (parametro.EsCuentaContableEmpresa == true && parametro.TipoCuentaContable != 0) {
                    var existeTipo = await _Repositorio.ObtenerTodos(z => z.EsCuentaContableEmpresa == true && z.TipoCuentaContable == parametro.TipoCuentaContable);
                    if (existeTipo.Count() > 0) {
                        respuesta.Estatus = false;
                        respuesta.Descripcion = "Ya existe una cuenta con este tipo";
                        return respuesta;
                    }
                }
                if (objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La cuenta contable no existe";
                    return respuesta;
                }
                objetoEncontrado.Descripcion = modelo.Descripcion;
                objetoEncontrado.IdRubro = modelo.IdRubro;
                objetoEncontrado.TipoMoneda = modelo.TipoMoneda;
                objetoEncontrado.IdCodigoAgrupadorSat = modelo.IdCodigoAgrupadorSat;
                objetoEncontrado.ExisteMovimiento = parametro.ExisteMovimiento;
                objetoEncontrado.ExistePoliza = parametro.ExistePoliza;
                objetoEncontrado.EsCuentaContableEmpresa = parametro.EsCuentaContableEmpresa;
                objetoEncontrado.TipoCuentaContable = parametro.TipoCuentaContable;
                respuesta.Estatus = await _Repositorio.Editar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo editar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Cuenta contable editada";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la edición de la cuenta contable";
                return respuesta;
            }
        }

        public async Task<CuentaContableDTO> Crear(CuentaContableCreacionDTO modelo)
        {
            try
            {
                if (modelo.IdPadre <= 0 && modelo.Nivel > 1)
                {
                    return new CuentaContableDTO();
                }
                if (!(int.TryParse(modelo.Codigo, out int result)) && modelo.Nivel == 1)
                {
                    return new CuentaContableDTO();
                }

                string CodigoPrimerCuenta = "";
                switch (result.ToString().Length)
                {
                    case 1:
                        CodigoPrimerCuenta = "000" + result;
                        break;
                    case 2:
                        CodigoPrimerCuenta = "00" + result;
                        break;
                    case 3:
                        CodigoPrimerCuenta = "0" + result;
                        break;
                    case 4:
                        CodigoPrimerCuenta = "" + result;
                        break;
                    default:
                        break;
                }

                CodigoPrimerCuenta = CodigoPrimerCuenta + "-0000-0000-0000-0000";
                var lista = await _Repositorio.ObtenerTodos(z => z.Codigo == CodigoPrimerCuenta);
                if (lista.Count() > 0)
                {
                    return new CuentaContableDTO();
                }
                if (modelo.Nivel != 1)
                {
                    string[] numerosCodigos = modelo.Codigo.Split('-');
                    if (numerosCodigos.Length <= 0)
                    {
                        return new CuentaContableDTO();
                    }
                    var lista1 = await _Repositorio.ObtenerTodos(z => z.IdPadre == modelo.IdPadre);
                    if (lista1.Count() > 9999)
                    {
                        return new CuentaContableDTO();
                    }
                    int digitosListaInt = lista1.Count();
                    digitosListaInt = digitosListaInt + 1;
                    string digitosListaString = digitosListaInt.ToString();
                    switch (digitosListaString.Length)
                    {
                        case 0:
                            digitosListaString = "000" + digitosListaInt;
                            break;
                        case 1:
                            digitosListaString = "000" + digitosListaInt;
                            break;
                        case 2:
                            digitosListaString = "00" + digitosListaInt;
                            break;
                        case 3:
                            digitosListaString = "0" + digitosListaInt;
                            break;
                        case 4:
                            digitosListaString = "" + digitosListaInt;
                            break;
                        default:
                            break;
                    }
                    string[] codigoSplit = modelo.Codigo.Split('-');
                    codigoSplit[modelo.Nivel - 1] = digitosListaString;
                    CodigoPrimerCuenta = codigoSplit[0].ToString() + "-" + codigoSplit[1].ToString() + "-" + codigoSplit[2].ToString() + "-" +
                    codigoSplit[3].ToString() + "-" + codigoSplit[4].ToString();
                    //+ "-" + codigoSplit[5].ToString() + "-" + codigoSplit[6].ToString();
                    //CodigoPrimerCuenta = digitosListaString;
                }
                modelo.Codigo = CodigoPrimerCuenta;
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<CuentaContable>(modelo));
                if(objetoCreado.Id == 0)
                {
                    return new CuentaContableDTO();
                }
                return _Mapper.Map<CuentaContableDTO>(objetoCreado);
            }
            catch
            {
                return new CuentaContableDTO();
            }
        }

        public async Task<List<CuentaContableDTO>> ObtenAsignables()
        {
            var lista = await _Repositorio.ObtenerTodos(z => z.ExisteMovimiento == false);
            lista = lista.OrderBy(z => z.Codigo).ToList();
            return _Mapper.Map<List<CuentaContableDTO>>(lista);
        }

        public async Task<List<CuentaContableDTO>> ObtenXEmpresa()
        {
            var lista = await _Repositorio.ObtenerTodos(z => z.EsCuentaContableEmpresa == true);
            return _Mapper.Map<List<CuentaContableDTO>>(lista);
        }
    }
}
