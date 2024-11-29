


using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;



using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace ERP_TECKIO
{
    /// <summary>
    /// Controlador de las cuentas contables que hereda de <see cref="ControllerBase"/>
    /// </summary>
    [Route("api/cuentacontable/21")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionCuentaContable-Empresa21")]
    public class CuentaContableAlumno21Controller : ControllerBase
    {
        private readonly ICuentaContableService<Alumno21Context> _Service;
        private readonly ICodigoAgrupadorService _CodigoAgrupadorService;
        private readonly IRubroService<Alumno21Context> _RubroService;
        /// <summary>
        /// Se usa para mostrar errores en consola
        /// </summary>
        private readonly ILogger<CuentaContableAlumno21Controller> Logger;
        /// <summary>
        /// Se usa para mandar en "headers" los registros totales de los registros
        /// </summary>
        private readonly Alumno21Context Context;
        /// <summary>
        /// Constructor del controlador de las cuentas contables
        /// </summary>
        /// <param name="logger">Para mostrar errores en consola</param>
        /// <param name="context">Para mandar información de los registros</param>
        public CuentaContableAlumno21Controller(
            ILogger<CuentaContableAlumno21Controller> logger,
            Alumno21Context context
            , ICuentaContableService<Alumno21Context> Service
            , ICodigoAgrupadorService CodigoAgrupadorService
            , IRubroService<Alumno21Context> RubroService
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
            return cuentasContablesPadres;
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
    }
}
