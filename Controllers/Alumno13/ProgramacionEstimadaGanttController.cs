using ERP_TECKIO;
using ERP_TECKIO.Procesos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SistemaERP.API.Controllers.Alumno13
{
    [Route("api/programacionestimadaGantt/13")]
    [ApiController]
    public class ProgramacionEstimadaGanttAlumno13Controller : ControllerBase
    {
        private readonly ProgramacionEstimadaGanttProceso<Alumno13Context> _ProgramacionEstimadaGanttProceso;
        private readonly DbContextOptionsBuilder<Alumno13Context> _Options;
        private readonly ImporteSemanalGanttProceso<Alumno13Context> _importeSemanalGanttProceso;

        public ProgramacionEstimadaGanttAlumno13Controller(
            ProgramacionEstimadaGanttProceso<Alumno13Context> programacionEstimadaGanttProceso,
            ImporteSemanalGanttProceso<Alumno13Context> importeSemanalGanttProceso,
            DbContextOptionsBuilder<Alumno13Context> options
            )
        {
            _ProgramacionEstimadaGanttProceso = programacionEstimadaGanttProceso;
            _Options = options;
            _importeSemanalGanttProceso = importeSemanalGanttProceso;
        }

        [HttpGet("obtenerGanttXIdProyecto/{IdProyecto:int}")]
        public async Task<List<ProgramacionEstimadaGanttDTO>> ObtenerProgramacionEstimadaGantt(int IdProyecto)
        {
            using (var db = new Alumno13Context(_Options.Options))
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
            using (var db = new Alumno13Context(_Options.Options))
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
            using (var db = new Alumno13Context(_Options.Options))
            {
                await _ProgramacionEstimadaGanttProceso.AsignarProgreso(registro, db);
            }
        }
    }
}
