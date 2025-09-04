using ERP_TECKIO;
using ERP_TECKIO.Procesos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SistemaERP.API.Controllers.DemoTeckioAL24
{
    [Route("api/programacionestimadaGantt/24")]
    [ApiController]
    public class ProgramacionEstimadaGanttDemoTeckioAL24Controller : ControllerBase
    {
        private readonly ProgramacionEstimadaGanttProceso<DemoTeckioAL24Context> _ProgramacionEstimadaGanttProceso;
        private readonly DbContextOptionsBuilder<DemoTeckioAL24Context> _Options;
        private readonly ImporteSemanalGanttProceso<DemoTeckioAL24Context> _importeSemanalGanttProceso;

        public ProgramacionEstimadaGanttDemoTeckioAL24Controller(
            ProgramacionEstimadaGanttProceso<DemoTeckioAL24Context> programacionEstimadaGanttProceso,
            ImporteSemanalGanttProceso<DemoTeckioAL24Context> importeSemanalGanttProceso,
            DbContextOptionsBuilder<DemoTeckioAL24Context> options
            )
        {
            _ProgramacionEstimadaGanttProceso = programacionEstimadaGanttProceso;
            _Options = options;
            _importeSemanalGanttProceso = importeSemanalGanttProceso;
        }

        [HttpGet("obtenerGanttXIdProyecto/{IdProyecto:int}")]
        public async Task<List<ProgramacionEstimadaGanttDTO>> ObtenerProgramacionEstimadaGantt(int IdProyecto)
        {
            using (var db = new DemoTeckioAL24Context(_Options.Options))
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
            using (var db = new DemoTeckioAL24Context(_Options.Options))
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
            using (var db = new DemoTeckioAL24Context(_Options.Options))
            {
                await _ProgramacionEstimadaGanttProceso.AsignarProgreso(registro, db);
            }
        }

        [HttpPut("asignarDuracion")]
        public async Task<List<ProgramacionEstimadaGanttDTO>> AsignarDuracion(ProgramacionEstimadaGanttDTO registro)
        {
            using (var db = new DemoTeckioAL24Context(_Options.Options))
            {
                registro.End = registro.Start.AddDays(registro.Duracion);
                var programaciones = await _ProgramacionEstimadaGanttProceso.EditarFechaProgramacionEstimada(registro, db);
                return programaciones;
            }
        }

        [HttpPut("generarDependenciaXNumerador")]
        public async Task AsignarDependenciaXNumerador(ProgramacionEstimadaGanttDTO registro)
        {
            using (var db = new DemoTeckioAL24Context(_Options.Options))
            {
                await _ProgramacionEstimadaGanttProceso.GenerarDependenciaXNumerador(registro, db);
            }
        }

        [HttpPut("asignarComando")]
        public async Task AsignarComando(ProgramacionEstimadaGanttDTO registro)
        {
            using (var db = new DemoTeckioAL24Context(_Options.Options))
            {
                await _ProgramacionEstimadaGanttProceso.AsignarComando(registro, db);
            }
        }

        [HttpPut("asignarDesfase")]
        public async Task AsignarDesfase(ProgramacionEstimadaGanttDTO registro)
        {
            using (var db = new DemoTeckioAL24Context(_Options.Options))
            {
                await _ProgramacionEstimadaGanttProceso.AsignarDesfase(registro, db);
            }
        }
    }
}
