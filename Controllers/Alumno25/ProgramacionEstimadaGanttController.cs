using ERP_TECKIO;
using ERP_TECKIO.Procesos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SistemaERP.API.Controllers.Alumno25
{
    [Route("api/programacionestimadaGantt/25")]
    [ApiController]
    public class ProgramacionEstimadaGanttAlumno25Controller : ControllerBase
    {
        private readonly ProgramacionEstimadaGanttProceso<Alumno25Context> _ProgramacionEstimadaGanttProceso;
        private readonly DbContextOptionsBuilder<Alumno25Context> _Options;
        private readonly ImporteSemanalGanttProceso<Alumno25Context> _importeSemanalGanttProceso;

        public ProgramacionEstimadaGanttAlumno25Controller(
            ProgramacionEstimadaGanttProceso<Alumno25Context> programacionEstimadaGanttProceso,
            ImporteSemanalGanttProceso<Alumno25Context> importeSemanalGanttProceso,
            DbContextOptionsBuilder<Alumno25Context> options
            )
        {
            _ProgramacionEstimadaGanttProceso = programacionEstimadaGanttProceso;
            _Options = options;
            _importeSemanalGanttProceso = importeSemanalGanttProceso;
        }

        [HttpGet("obtenerGanttXIdProyecto/{IdProyecto:int}")]
        public async Task<List<ProgramacionEstimadaGanttDTO>> ObtenerProgramacionEstimadaGantt(int IdProyecto)
        {
            using (var db = new Alumno25Context(_Options.Options))
            {
                var programacionesEstimadas = await _ProgramacionEstimadaGanttProceso.ObtenerProgramacionEstimadaXIdProyecto(IdProyecto, db);
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
            using (var db = new Alumno25Context(_Options.Options))
            {
                var programaciones = await _ProgramacionEstimadaGanttProceso.EditarFechaProgramacionEstimada(registro, db);
                return programaciones;
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
            using (var db = new Alumno25Context(_Options.Options))
            {
                await _ProgramacionEstimadaGanttProceso.AsignarProgreso(registro, db);
            }
        }

        [HttpPut("generarDependenciaXNumerador")]
        public async Task AsignarDependenciaXNumerador(ProgramacionEstimadaGanttDTO registro)
        {
            using (var db = new Alumno25Context(_Options.Options))
            {
                await _ProgramacionEstimadaGanttProceso.GenerarDependenciaXNumerador(registro, db);
            }
        }

        [HttpPut("asignarComando")]
        public async Task AsignarComando(ProgramacionEstimadaGanttDTO registro)
        {
            using (var db = new Alumno25Context(_Options.Options))
            {
                await _ProgramacionEstimadaGanttProceso.AsignarComando(registro, db);
            }
        }

        [HttpPut("asignarDesfase")]
        public async Task AsignarDesfase(ProgramacionEstimadaGanttDTO registro)
        {
            using (var db = new Alumno25Context(_Options.Options))
            {
                await _ProgramacionEstimadaGanttProceso.AsignarDesfase(registro, db);
            }
        }
    }
}
