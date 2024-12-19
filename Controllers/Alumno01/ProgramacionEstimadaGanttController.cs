using ERP_TECKIO;
using ERP_TECKIO.Procesos;
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
        private readonly ImporteSemanalGanttProceso<Alumno01Context> _importeSemanalGanttProceso;

        public ProgramacionEstimadaGanttAlumno01Controller(
            ProgramacionEstimadaGanttProceso<Alumno01Context> programacionEstimadaGanttProceso,
            ImporteSemanalGanttProceso<Alumno01Context> importeSemanalGanttProceso,
            DbContextOptionsBuilder<Alumno01Context> options
            )
        {
            _ProgramacionEstimadaGanttProceso = programacionEstimadaGanttProceso;
            _Options = options;
            _importeSemanalGanttProceso = importeSemanalGanttProceso;
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

        [HttpGet("obtenerImporteSemanalGantt/{IdProyecto:int}")]
        public async Task<List<ImporteSemanalDTO>> obtenerImporteSemanalGantt(int IdProyecto)
        {
            return await _importeSemanalGanttProceso.ImporteSemanal(IdProyecto);

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

        [HttpDelete("EliminarDependencia/{IdDependencia:int}")]
        public async Task EliminarDependencia(int IdDependencia)
        {
            await _ProgramacionEstimadaGanttProceso.EliminarDependencia(IdDependencia);
        }

        [HttpPut("generarDependencia")]
        public async Task GenerarDependencia(DependenciaProgramacionEstimadaDTO registro)
        {
            await _ProgramacionEstimadaGanttProceso.GenerarDependencia(registro);
        }
    }
}
