using AutoMapper;
using ERP_TECKIO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_TECKIO
{
    public class ProgramacionEstimadaGanttProceso<TContext> where TContext : DbContext
    {
        private readonly IProgramacionEstimadaGanttService<TContext> _ProgramacionEstimadaGanttService;
        private readonly IDependenciaProgramacionEstimadaService<TContext> _DependenciaProgramacionEstimadaService;
        private readonly IMapper _Mapper;

        public ProgramacionEstimadaGanttProceso(
            IProgramacionEstimadaGanttService<TContext> programacionEstimadaGanttService,
            IDependenciaProgramacionEstimadaService<TContext> dependenciaProgramacionEstimadaService,
            IMapper mapper
            )
        {
            _ProgramacionEstimadaGanttService = programacionEstimadaGanttService;
            _DependenciaProgramacionEstimadaService = dependenciaProgramacionEstimadaService;
            _Mapper = mapper;
        }

        public async Task<List<ProgramacionEstimadaGanttDTO>> ObtenerProgramacionEstimadaXIdProyecto(int IdProyecto, DbContext db)
        {
            var programacionesEstimadas = await _ProgramacionEstimadaGanttService.ObtenerXIdProyecto(IdProyecto, db);
            return programacionesEstimadas;
        }
    }
}
