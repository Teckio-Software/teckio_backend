using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace ERP_TECKIO.Controllers
{
    /// <summary>
    /// Controlador de las cuentas contables que hereda de <see cref="ControllerBase"/>
    /// </summary>
    [Route("api/cuentacontable/31")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionCuentaContable-Empresa1")]
    public class CuentaContableAlumno31Controller : ControllerBase
    {
        private readonly ICuentaContableService<Alumno31Context> _Service;
        private readonly ICodigoAgrupadorService<Alumno31Context> _CodigoAgrupadorService;
        private readonly IRubroService<Alumno31Context> _RubroService;
        /// <summary>
        /// Se usa para mostrar errores en consola
        /// </summary>
        private readonly ILogger<CuentaContableAlumno31Controller> Logger;
        /// <summary>
        /// Se usa para mandar en "headers" los registros totales de los registros
        /// </summary>
        private readonly Alumno31Context Context;
        /// <summary>
        /// Constructor del controlador de las cuentas contables
        /// </summary>
        /// <param name="logger">Para mostrar errores en consola</param>
        /// <param name="context">Para mandar información de los registros</param>
        public CuentaContableAlumno31Controller(
            ILogger<CuentaContableAlumno31Controller> logger,
            Alumno31Context context
            , ICuentaContableService<Alumno31Context> Service
            , ICodigoAgrupadorService<Alumno31Context> CodigoAgrupadorService
            , IRubroService<Alumno31Context> RubroService
            )
        {
            Logger = logger;
            Context = context;
            _Service = Service;
            _CodigoAgrupadorService = CodigoAgrupadorService;
            _RubroService = RubroService;
        }

        [HttpGet("todos")]
        public async Task<ActionResult<List<CuentaContableDTO>>> Get()
        {
            var lista = await _Service.ObtenTodos();
            if (lista.Count <= 0)
            {
                return new List<CuentaContableDTO>();
            }
            var listaRubros = await _RubroService.ObtenTodos();
            if (listaRubros.Count <= 0)
            {
                return new List<CuentaContableDTO>();
            }
            var listaCodigosAgrupadores = await _CodigoAgrupadorService.ObtenTodos();
            if (listaCodigosAgrupadores.Count <= 0)
            {
                return new List<CuentaContableDTO>();
            }
            for (int i = 0; i < lista.Count; i++)
            {
                lista[i].DescripcionRubro = listaRubros.Where(z => z.Id == lista[i].IdRubro).FirstOrDefault()!.Descripcion;
                lista[i].DescripcionCodigoAgrupadorSat = listaCodigosAgrupadores.Where(z => z.Id == lista[i].IdCodigoAgrupadorSat).FirstOrDefault()!.Descripcion;
            }
            var cuentasContablesPadres = lista.Where(z => z.IdPadre == 0).ToList();
            for (int i = 0; i < cuentasContablesPadres.Count; i++)
            {
                var cuentasContablesHijos = await ObtenHijos(lista, cuentasContablesPadres[i]);
                cuentasContablesPadres[i].Hijos = cuentasContablesHijos;
            }
            return cuentasContablesPadres.OrderBy(z => z.Codigo).ToList();
        }

        [HttpGet("Asignables")]
        public async Task<List<CuentaContableDTO>> ObtenerAsignables()
        {
            var lista = await _Service.ObtenTodos();
            var asignables = lista.Where(z => z.ExisteMovimiento == false).OrderBy(z => z.Codigo).ToList();
            return asignables;
        }

        [HttpGet("todossinestructura")]
        public async Task<ActionResult<List<CuentaContableDTO>>> GetSinEstructura()
        {
            var lista = await _Service.ObtenTodos();
            return lista;
        }

        private async Task<List<CuentaContableDTO>> ObtenHijos(List<CuentaContableDTO> cuentasContables, CuentaContableDTO cuentaContable)
        {
            try
            {
                var cuentasContablesHijos = cuentasContables.Where(z => z.IdPadre == cuentaContable.Id).ToList();
                if (cuentasContablesHijos.Count <= 0)
                {
                    return new List<CuentaContableDTO>();
                }
                for (int i = 0; i < cuentasContablesHijos.Count; i++)
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

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "CrearCuentaContable-Empresa1")]
        public async Task<CuentaContableDTO> Post([FromBody] CuentaContableCreacionDTO creacionDTO)
        {
            try
            {
                var padre = await _Service.ObtenXId(creacionDTO.IdPadre);
                if (padre.Id > 0)
                {
                    if (padre.ExisteMovimiento == false)
                    {
                        padre.ExisteMovimiento = true;
                        await _Service.Editar(padre);
                    }
                }
                var resultado = await _Service.Crear(creacionDTO);
                return resultado;
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
                return new CuentaContableDTO();
            }
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "CrearCuentaContable-Empresa1")]
        public async Task<CuentaContableDTO> Put([FromBody] CuentaContableDTO registro)
        {
            try
            {
                var resultado = await _Service.Editar(registro);
                return registro;
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
                return new CuentaContableDTO();
            }
        }

        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "CrearCuentaContable-Empresa1")]
        public async Task<bool> Delete(int Id)
        {
            try
            {
                var resultado = await _Service.Eliminar(Id);
                return resultado.Estatus;
            }
            catch(Exception ex)
            {
                string error = ex.Message.ToString();
                return false;
            }
        }
    }
}
