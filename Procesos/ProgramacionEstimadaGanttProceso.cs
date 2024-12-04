using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ERP_TECKIO
{
    public class ProgramacionEstimadaGanttProceso<TContext> where TContext : DbContext
    {
        private readonly IProgramacionEstimadaGanttService<TContext> _ProgramacionEstimadaGanttService;
        private readonly IDependenciaProgramacionEstimadaService<TContext> _DependenciaProgramacionEstimadaService;
        private readonly DbContextOptionsBuilder<Alumno01Context> _Options;
        private readonly IMapper _Mapper;

        public ProgramacionEstimadaGanttProceso(
            IProgramacionEstimadaGanttService<TContext> programacionEstimadaGanttService,
            IDependenciaProgramacionEstimadaService<TContext> dependenciaProgramacionEstimadaService,
            DbContextOptionsBuilder<Alumno01Context> options,
            IMapper mapper
            )
        {
            _ProgramacionEstimadaGanttService = programacionEstimadaGanttService;
            _DependenciaProgramacionEstimadaService = dependenciaProgramacionEstimadaService;
            _Mapper = mapper;
        }

        [HttpPost]
        public async Task<ProgramacionEstimadaGanttDTO> CrearProgramacionEstimadaGantt()
        {
            var nuevaProgramacionEstimada = new ProgramacionEstimadaGanttDTO();
            return nuevaProgramacionEstimada;
        }

        [HttpGet("obtenerGanttXIdProyecto/{IdProyecto:int}")]
        public async Task<List<ProgramacionEstimadaGanttDTO>> ObtenerProgramacionEstimadaGantt(int IdProyecto)
        {
            using (var db = new Alumno01Context(_Options.Options))
            {
                var programacionesEstimadas = _ProgramacionEstimadaGanttService.ObtenerXIdProyecto(IdProyecto, db).Result;
                return programacionesEstimadas;
            }
        }
    }
}
