using ERP_TECKIO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SistemaERP.API.Controllers.Alumno01
{
    [Route("api/programacionestimadaGantt/1")]
    [ApiController]
    public class ProgramacionEstimadaGanttAlumno01Controller : ControllerBase
    {
        private readonly ProgramacionEstimadaGanttProceso<Alumno01Context> _ProgramacionEstimadaGanttProceso;
        private readonly DbContextOptionsBuilder<Alumno01Context> _Options;

        public ProgramacionEstimadaGanttAlumno01Controller(
            ProgramacionEstimadaGanttProceso<Alumno01Context> programacionEstimadaGanttProceso,
            DbContextOptionsBuilder<Alumno01Context> options
            )
        {
            _ProgramacionEstimadaGanttProceso = programacionEstimadaGanttProceso;
            _Options = options;
        }

        [HttpGet("obtenerGanttXIdProyecto/{IdProyecto:int}")]
        public async Task<List<ProgramacionEstimadaGanttDTO>> ObtenerProgramacionEstimadaGantt(int IdProyecto)
        {
            using (var db = new Alumno01Context(_Options.Options))
            {
                var programacionesEstimadas = _ProgramacionEstimadaGanttProceso.ObtenerProgramacionEstimadaXIdProyecto(IdProyecto, db).Result;
                return programacionesEstimadas;
            }
        }

        [HttpPost("EditarFechaGantt")]
        public async Task<List<ProgramacionEstimadaGanttDTO>> EditarProgramacionEstimadaGantt(ProgramacionEstimadaGanttDTO registro)
        {
            using (var db = new Alumno01Context(_Options.Options))
            {
                var programaciones = _ProgramacionEstimadaGanttProceso.EditarFechaProgramacionEstimada(registro, db);
                return programaciones.Result;
            }
        }
    }
}
