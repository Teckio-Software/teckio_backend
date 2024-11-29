



using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;






using AutoMapper;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;


namespace ERP_TECKIO
{
    /// <summary>
    /// Controlador de los rubros que hereda de <see cref="ControllerBase"/>
    /// </summary>
    [Route("api/rubro/34")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "SeccionRubro-Empresa34")]
    public class RubroAlumno34Controller : ControllerBase
    {
        private readonly IRubroService<Alumno34Context> _Service;
        /// <summary>
        /// Se usa para mostrar errores en consola
        /// </summary>
        private readonly ILogger<RubroAlumno34Controller> Logger;
        private readonly IMapper _Mapper;
        /// <summary>
        /// Se usa para mandar en "headers" los registros totales de los registros
        /// </summary>
        private readonly Alumno34Context Context;
        //private readonly IGenericRepository2<Rubro, ErpContextTeckio> _GenericRepository2;
        /// <summary>
        /// Constructor del controlador de Almacenes
        /// </summary>
        /// <param name="logger">Para mostrar errores en consola</param>
        /// <param name="context">Para mandar inofrmación de los registros</param>
        public RubroAlumno34Controller(
            ILogger<RubroAlumno34Controller> logger,
            Alumno34Context context
            , IMapper mapper
            //, IGenericRepository2<Rubro, ErpContextTeckio> genericRepository2
            , IRubroService<Alumno34Context> service
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "CrearRubro-Empresa34")]
        public async Task<ActionResult> Post([FromBody] RubroCreacionDTO creacionDTO)
        {
            try
            {
                var resultado = await _Service.Crear(creacionDTO);
                await HttpContext.InsertarParametrosCreacionEdicionEnCabecera<Alumno34Context>(resultado.Estatus, resultado.Descripcion);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return NoContent();
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//, Policy = "EditarRubro-Empresa34")]
        public async Task<ActionResult> Put([FromBody] RubroDTO parametroDTO)
        {
            try
            {
                var resultado = await _Service.Editar(parametroDTO);
                await HttpContext.InsertarParametrosCreacionEdicionEnCabecera<Alumno34Context>(resultado.Estatus, resultado.Descripcion);
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
            }
            return NoContent();
        }
    }
}
