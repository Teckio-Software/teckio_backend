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
            var dependencias = await _DependenciaProgramacionEstimadaService.ObtenerXIdProyecto(IdProyecto, db);
            foreach(ProgramacionEstimadaGanttDTO registro in programacionesEstimadas)
            {
                if(registro.TipoPrecioUnitario == 1)
                {
                    registro.Dependencies = dependencias.Where(z => z.IdProgramacionEstimadaGantt == Convert.ToInt32(registro.Id)).ToList();
                }
            }
            return programacionesEstimadas;
        }

        public async Task<List<ProgramacionEstimadaGanttDTO>> EditarFechaProgramacionEstimada(ProgramacionEstimadaGanttDTO registro, DbContext db)
        {
            var programacionEstimada = await _ProgramacionEstimadaGanttService.Editar(registro);
            var programaciones = await _ProgramacionEstimadaGanttService.ObtenerXIdProyecto(registro.IdProyecto, db);
            if (Convert.ToInt32(registro.Parent) != 0)
            {
                var programacionPadre = programaciones.Where(z => z.Id == registro.Parent).FirstOrDefault();
                var hijos = programaciones.Where(z => z.Parent == programacionPadre.Id).ToList();
                var registroMenorInicio = hijos.OrderBy(z => z.Start).FirstOrDefault();
                var registroMayorTermino = hijos.OrderByDescending(z => z.End).FirstOrDefault();
                programacionPadre.Start = registroMenorInicio.Start;
                programacionPadre.End = registroMayorTermino.End;
                await _ProgramacionEstimadaGanttService.Editar(programacionPadre);
                await RecalcularPadresProgramacionEstimada(programacionPadre, db);
            }
            return ObtenerProgramacionEstimadaXIdProyecto(registro.IdProyecto, db).Result;
        }

        public async Task RecalcularPadresProgramacionEstimada(ProgramacionEstimadaGanttDTO registro, DbContext db)
        {
            var programaciones = await _ProgramacionEstimadaGanttService.ObtenerXIdProyecto(registro.IdProyecto, db);
            if(Convert.ToInt32(registro.Parent) != 0)
            {
                var programacionPadre = programaciones.Where(z => z.Id == registro.Parent).FirstOrDefault();
                var hijos = programaciones.Where(z => z.Parent == programacionPadre.Id).ToList();
                var registroMenorInicio = hijos.OrderBy(z => z.Start).FirstOrDefault();
                var registroMayorTermino = hijos.OrderByDescending(z => z.End).FirstOrDefault();
                programacionPadre.Start = registroMenorInicio.Start;
                programacionPadre.End = registroMayorTermino.End;
                await _ProgramacionEstimadaGanttService.Editar(programacionPadre);
                await RecalcularPadresProgramacionEstimada(programacionPadre, db);
            }
        }

        public async Task EliminarDependencia(int IdDependencia)
        {
            await _DependenciaProgramacionEstimadaService.Eliminar(IdDependencia);
        }
    }
}
