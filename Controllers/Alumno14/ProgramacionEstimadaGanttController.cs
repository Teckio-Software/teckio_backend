using ERP_TECKIO;
using ERP_TECKIO.Procesos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SistemaERP.API.Controllers.Alumno14
{
    [Route("api/programacionestimadaGantt/14")]
    [ApiController]
    public class ProgramacionEstimadaGanttAlumno14Controller : ControllerBase
    {
        private readonly ProgramacionEstimadaGanttProceso<Alumno14Context> _ProgramacionEstimadaGanttProceso;
        private readonly DbContextOptionsBuilder<Alumno14Context> _Options;
        private readonly ImporteSemanalGanttProceso<Alumno14Context> _importeSemanalGanttProceso;

        public ProgramacionEstimadaGanttAlumno14Controller(
            ProgramacionEstimadaGanttProceso<Alumno14Context> programacionEstimadaGanttProceso,
            ImporteSemanalGanttProceso<Alumno14Context> importeSemanalGanttProceso,
            DbContextOptionsBuilder<Alumno14Context> options
            )
        {
            _ProgramacionEstimadaGanttProceso = programacionEstimadaGanttProceso;
            _Options = options;
            _importeSemanalGanttProceso = importeSemanalGanttProceso;
        }

        [HttpGet("obtenerGanttXIdProyecto/{IdProyecto:int}")]
        public async Task<List<ProgramacionEstimadaGanttDTO>> ObtenerProgramacionEstimadaGantt(int IdProyecto)
        {
            using (var db = new Alumno14Context(_Options.Options))
            {
                var programacionesEstimadas = _ProgramacionEstimadaGanttProceso.ObtenerProgramacionEstimadaXIdProyecto(IdProyecto, db).Result;
                return programacionesEstimadas;
            }
        }

        [HttpGet("obtenerImporteSemanalGantt/{IdProyecto:int}")]
        public async Task<ImportesSemanalesPorTipoDTO> obtenerImporteSemanalGantt(int IdProyecto)
        {
            var semanasRetornar = await _importeSemanalGanttProceso.ObtenerTotalesPorTipo(IdProyecto);
            return semanasRetornar;
        }

        [HttpPost("EditarFechaGantt")]
        public async Task<List<ProgramacionEstimadaGanttDTO>> EditarProgramacionEstimadaGantt(ProgramacionEstimadaGanttDTO registro)
        {
            using (var db = new Alumno14Context(_Options.Options))
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

        [HttpPut("asignarProgreso")]
        public async Task AsignarProgreso(ProgramacionEstimadaGanttDTO registro)
        {
            using (var db = new Alumno14Context(_Options.Options))
            {
                await _ProgramacionEstimadaGanttProceso.AsignarProgreso(registro, db);
            }
        }
    }
}
