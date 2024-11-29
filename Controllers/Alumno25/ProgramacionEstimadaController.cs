using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;









namespace ERP_TECKIO
{
    [Route("api/programacionestimada/25")]
    [ApiController]
    public class ProgramacionEstimadaAlumno25Controller : ControllerBase
    {
        private readonly ProgramacionEstimadaProceso<Alumno25Context> _ProgramacionEstimadaProceso;
        private readonly IProyectoService<Alumno25Context> _ProyectoService;
        private readonly IPrecioUnitarioService<Alumno25Context> _PrecioUnitarioService;

        public ProgramacionEstimadaAlumno25Controller(
            ProgramacionEstimadaProceso<Alumno25Context> programacionEstimadaProceso,
            IProyectoService<Alumno25Context> proyectoProceso,
            IPrecioUnitarioService<Alumno25Context> precioUnitarioService,
            Alumno25Context context)
        {
            _ProgramacionEstimadaProceso = programacionEstimadaProceso;
            _ProyectoService = proyectoProceso;
            _PrecioUnitarioService = precioUnitarioService;
        }

        [HttpGet("obtenerProgramacionEstimada/{idProyecto:int}")]
        public async Task<ActionResult<List<ProgramacionEstimadaDTO>>> obtenerProgramacionEstimada(int idProyecto)
        {
            return _ProgramacionEstimadaProceso.ObtenerProgramacionEstimada(idProyecto).Result;
        }

        [HttpGet("obtenerProgramacionEstimadaEstructurada/{idProyecto:int}")]
        public async Task<ActionResult<List<ProgramacionEstimadaDTO>>> obtenerProgramacionEstimadaEstructurada(int idProyecto)
        {
            return _ProgramacionEstimadaProceso.ObtenerProgramacionEstimadaEstructurada(idProyecto).Result;
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<ProgramacionEstimadaDTO>> GetXId(int Id)
        {
            return _ProgramacionEstimadaProceso.ObtenerXId(Id).Result;
        }

        [HttpPost]
        public async Task<ActionResult<ProgramacionEstimadaDTO>> Post([FromBody] ProgramacionEstimadaDTO programacionEstimadaCreacionDTO)
        {
            return _ProgramacionEstimadaProceso.CrearProgramacionEstimada(programacionEstimadaCreacionDTO).Result;
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] ProgramacionEstimadaDTO programacionEstimadaDTO)
        {
            await _ProgramacionEstimadaProceso.Editar(programacionEstimadaDTO);
            return NoContent();
        }

        [HttpPut("editafechasdatepicker")]
        public async Task<ActionResult> PutFecha([FromBody] ProgramacionEstimadaDTO programacionEstimada)
        {
            await _ProgramacionEstimadaProceso.EditarConDatePicker(programacionEstimada);
            return NoContent();
        }

        [HttpPut("editagantt")]
        public async Task<ActionResult> Edita([FromBody] ProgramacionEstimadaDTO programacionEstimadaDTO)
        {
            await _ProgramacionEstimadaProceso.EditarGantt(programacionEstimadaDTO);
            return NoContent();
        }


        [HttpPut("editafechasdias")]
        public async Task<ActionResult> PutFechaDias([FromBody] ProgramacionEstimadaDTO programacionEstimada)
        {
            var registro = await _ProgramacionEstimadaProceso.ObtenerXId(programacionEstimada.Id);
            registro.DiasTranscurridos = programacionEstimada.DiasTranscurridos;
            if (registro.TipoPrecioUnitario == 1)
            {
                await _ProgramacionEstimadaProceso.PutFechaDias(registro);
            }
            return NoContent();
        }

        /// <summary>
        /// Enpoint que asigna predecesora y en caso de tener hijos recorre las fechas de los hijos
        /// </summary>
        /// <param name="programacionEstimada"></param>
        /// <returns></returns>
        [HttpPut("editapredecesora")]
        public async Task<ActionResult> PutEditaPredecesora([FromBody] ProgramacionEstimadaDTO programacionEstimada)
        {
            await _ProgramacionEstimadaProceso.PutEditaPredecesora(programacionEstimada);
            return NoContent();
        }

        [HttpGet("obtenerFechaFinal/{id:int}")]
        public async Task<DateTime> obtenerRegistroParaFechaFinal(int id)
        {
            return await _ProgramacionEstimadaProceso.obtenerRegistroParaFechaFinal(id);
        }
    }
}

