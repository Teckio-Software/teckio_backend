using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using Microsoft.AspNetCore.Authorization;



using Microsoft.AspNetCore.Authentication.JwtBearer;

using AutoMapper;

namespace ERP_TECKIO.Controllers
{
    /// <summary>
    /// Controlador de los rubros que hereda de <see cref="ControllerBase"/>
    /// </summary>
    [Route("api/rubro/3")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionRubro-Empresa1")]
    public class RubroAlumno03Controller : ControllerBase
    {
        private readonly IRubroService<Alumno03Context> _Service;
        /// <summary>
        /// Se usa para mostrar errores en consola
        /// </summary>
        private readonly ILogger<RubroAlumno03Controller> Logger;
        private readonly IMapper _Mapper;
        /// <summary>
        /// Se usa para mandar en "headers" los registros totales de los registros
        /// </summary>
        private readonly Alumno03Context Context;
        //private readonly IGenericRepository2<Rubro, ErpContextTeckio> _GenericRepository2;
        /// <summary>
        /// Constructor del controlador de Almacenes
        /// </summary>
        /// <param name="logger">Para mostrar errores en consola</param>
        /// <param name="context">Para mandar inofrmación de los registros</param>
        public RubroAlumno03Controller(
            ILogger<RubroAlumno03Controller> logger,
            Alumno03Context context
            , IMapper mapper
            //, IGenericRepository2<Rubro, ErpContextTeckio> genericRepository2
            , IRubroService<Alumno03Context> service
            )
        {
            //_Service = service;
            Logger = logger;
            Context = context;
            _Mapper = mapper;
            _Service = service;
            //_GenericRepository2 = genericRepository2;
        }

        [HttpGet("todos")]
        public async Task<ActionResult<List<RubroDTO>>> Get()
        {
            var query = _Service.ObtenTodos().Result.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnCabecera(query);
            var lista = query.OrderBy(x => x.Id).ToList();
            if (lista.Count <= 0) { return new List<RubroDTO>(); }
            return lista;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "CrearRubro-Empresa1")]
        public async Task<ActionResult> Post([FromBody] RubroCreacionDTO creacionDTO)
        {
            try
            {
                var resultado = await _Service.Crear(creacionDTO);
                await HttpContext.InsertarParametrosCreacionEdicionEnCabecera<Alumno03Context>(resultado.Estatus, resultado.Descripcion);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return NoContent();
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EditarRubro-Empresa1")]
        public async Task<ActionResult> Put([FromBody] RubroDTO parametroDTO)
        {
            try
            {
                var resultado = await _Service.Editar(parametroDTO);
                await HttpContext.InsertarParametrosCreacionEdicionEnCabecera<Alumno03Context>(resultado.Estatus, resultado.Descripcion);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return NoContent();
        }
    }
}
