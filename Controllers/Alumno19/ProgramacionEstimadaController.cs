using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;









namespace ERP_TECKIO
{
    [Route("api/programacionestimada/19")]
    [ApiController]
    public class ProgramacionEstimadaAlumno19Controller : ControllerBase
    {
        private readonly ProgramacionEstimadaProceso<Alumno19Context> _ProgramacionEstimadaProceso;
        private readonly IProyectoService<Alumno19Context> _ProyectoService;
        private readonly IPrecioUnitarioService<Alumno19Context> _PrecioUnitarioService;

        public ProgramacionEstimadaAlumno19Controller(
            ProgramacionEstimadaProceso<Alumno19Context> programacionEstimadaProceso,
            IProyectoService<Alumno19Context> proyectoProceso,
            IPrecioUnitarioService<Alumno19Context> precioUnitarioService,
            Alumno19Context context)
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