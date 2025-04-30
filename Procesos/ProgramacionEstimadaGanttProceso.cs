using AutoMapper;
using ERP_TECKIO;
using ERP_TECKIO.Procesos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
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
        private readonly TContext _DbContext;


        public ProgramacionEstimadaGanttProceso(
            IProgramacionEstimadaGanttService<TContext> programacionEstimadaGanttService,
            IDependenciaProgramacionEstimadaService<TContext> dependenciaProgramacionEstimadaService,
            IMapper mapper,
            TContext context
            )
        {
            _ProgramacionEstimadaGanttService = programacionEstimadaGanttService;
            _DependenciaProgramacionEstimadaService = dependenciaProgramacionEstimadaService;
            _Mapper = mapper;
            _DbContext = context;
        }

        public async Task<List<ProgramacionEstimadaGanttDTO>> ObtenerProgramacionEstimadaXIdProyecto(int IdProyecto, DbContext db)
        {
            var programacionesEstimadas = await _ProgramacionEstimadaGanttService.ObtenerXIdProyecto(IdProyecto, db);
            var dependencias = await _DependenciaProgramacionEstimadaService.ObtenerXIdProyecto(IdProyecto, db);
            foreach(ProgramacionEstimadaGanttDTO registro in programacionesEstimadas)
            {
                registro.Duracion = (registro.End - registro.Start).Days;
                if(registro.TipoPrecioUnitario == 1)
                {
                    registro.Dependencies = dependencias.Where(z => z.IdProgramacionEstimadaGantt == Convert.ToInt32(registro.Id)).ToList();
                }
            }

            var listaEnumerada = new List<ProgramacionEstimadaGanttDTO>();

            var registrosPadres = programacionesEstimadas.Where(z => z.Parent == null);
            foreach (var registro in registrosPadres)
            {
                listaEnumerada.Add(registro);
                listaEnumerada[listaEnumerada.Count - 1].Numerador = listaEnumerada.Count;
                var hijos = programacionesEstimadas.Where(z => z.Parent == registro.Id).ToList();
                if(hijos.Count() > 0)
                {
                    listaEnumerada = ObtenerNumeracionHijosProgramacionEstimada(listaEnumerada, hijos, programacionesEstimadas).Result;
                }
            }

            foreach (ProgramacionEstimadaGanttDTO registro in listaEnumerada)
            {
                registro.Duracion = (registro.End - registro.Start).Days;
                if (registro.TipoPrecioUnitario == 1)
                {
                    registro.Dependencies = dependencias.Where(z => z.IdProgramacionEstimadaGantt == Convert.ToInt32(registro.Id)).ToList();
                    var Cadena = "";
                    foreach (var dependencia in registro.Dependencies) {
                        var numero = listaEnumerada.Where(z => z.Id == dependencia.SourceId).First();
                        Cadena = Cadena + numero.Numerador +",";
                    }
                    registro.CadenaDependencias = Cadena;
                }
            }

            return listaEnumerada;
        }

        public async Task<List<ProgramacionEstimadaGanttDTO>> ObtenerNumeracionHijosProgramacionEstimada(List<ProgramacionEstimadaGanttDTO> listaEnumerada, List<ProgramacionEstimadaGanttDTO> hijos, List<ProgramacionEstimadaGanttDTO> programaciones)
        {
            foreach(var hijo in hijos)
            {
                listaEnumerada.Add(hijo);
                listaEnumerada[listaEnumerada.Count - 1].Numerador = listaEnumerada.Count;
                var subHijos = programaciones.Where(z => z.Parent == hijo.Id).ToList();
                if(subHijos.Count() > 0)
                {
                    listaEnumerada = ObtenerNumeracionHijosProgramacionEstimada(listaEnumerada, subHijos, programaciones).Result;
                }
            }
            return listaEnumerada;
        }

        public async Task<List<ProgramacionEstimadaGanttDTO>> EditarFechaProgramacionEstimada(ProgramacionEstimadaGanttDTO registro, DbContext db)
        { 
            var FI = registro.Start.ToShortDateString();
            registro.Start = Convert.ToDateTime(FI);
            var FT = registro.End.ToShortDateString();
            registro.End = Convert.ToDateTime(FT);

            var programacion = await _ProgramacionEstimadaGanttService.ObtenerXId(Convert.ToInt32(registro.Id), db);
            var dependencias = await _DependenciaProgramacionEstimadaService.ObtenerXIdPredesesora(Convert.ToInt32(registro.Id));
            if (dependencias.Count() > 0) {
                await ActualizarProgramacionDependienta(programacion, registro, dependencias, db);
            }

            var programaciones = await _ProgramacionEstimadaGanttService.ObtenerXIdProyecto(registro.IdProyecto, db);
            if (registro.Start == registro.End)
            {
                registro.End = registro.End.AddDays(1);
            }
            if (registro.End <= registro.Start)
            {
                registro.End = registro.Start;
                registro.End = registro.End.AddDays(1);
            }
            var programacionEstimada = await _ProgramacionEstimadaGanttService.Editar(registro);

            var dependenciasPadre = await _DependenciaProgramacionEstimadaService.ObtenerXIdProgramacionEstimadaGantt(Convert.ToInt32(programacion.Id));
            var listaProDep = new List<ProgramacionEstimadaGanttDTO>();
            int DiasDesfase = 0;
            if (dependenciasPadre.Count > 0)
            {
                if (registro.Comando == 1)
                {
                    foreach (var dep in dependenciasPadre)
                    {
                        var proEstimada = programaciones.Where(z => z.Id == dep.SourceId);
                        listaProDep.AddRange(proEstimada);
                    }

                    var FechaMaxima = listaProDep.OrderByDescending(z => z.End).FirstOrDefault();

                    DiasDesfase = (registro.Start - FechaMaxima.Start).Days;
                }
                if (registro.Comando == 2)
                {
                    foreach (var dep in dependenciasPadre)
                    {
                        var proEstimada = programaciones.Where(z => z.Id == dep.SourceId);
                        listaProDep.AddRange(proEstimada);
                    }

                    var FechaMaxima = listaProDep.OrderByDescending(z => z.End).FirstOrDefault();

                    DiasDesfase = (registro.Start - FechaMaxima.End).Days;
                }
                if (registro.Comando == 3)
                {
                    foreach (var dep in dependenciasPadre)
                    {
                        var proEstimada = programaciones.Where(z => z.Id == dep.SourceId);
                        listaProDep.AddRange(proEstimada);
                    }

                    var FechaMaxima = listaProDep.OrderByDescending(z => z.End).FirstOrDefault();

                    DiasDesfase = (registro.End - FechaMaxima.Start).Days;
                }
                if (registro.Comando == 4)
                {
                    foreach (var dep in dependenciasPadre)
                    {
                        var proEstimada = programaciones.Where(z => z.Id == dep.SourceId);
                        listaProDep.AddRange(proEstimada);
                    }

                    var FechaMaxima = listaProDep.OrderByDescending(z => z.End).FirstOrDefault();

                    DiasDesfase = (registro.End - FechaMaxima.End).Days;
                }
            }
            registro.DesfaseComando = DiasDesfase;
            //await AsignarDesfase(registro, db);
            //registro = await _ProgramacionEstimadaGanttService.ObtenerXId(Convert.ToInt32(registro.Id), db);

            if (registro.Start == registro.End)
            {
                registro.End = registro.End.AddDays(1);
            }
            if(registro.End <=  registro.Start)
            {
                registro.End = registro.Start;
                registro.End = registro.End.AddDays(1);
            }
            //registro.End = registro.End.AddSeconds(-1);
            programacionEstimada = await _ProgramacionEstimadaGanttService.Editar(registro);
            await EditarProgramacionEstimadaPadre(registro, db);
            return ObtenerProgramacionEstimadaXIdProyecto(registro.IdProyecto, db).Result;
        }

        public async Task EditarProgramacionEstimadaPadre(ProgramacionEstimadaGanttDTO registro, DbContext db) {
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
        }

        public async Task ActualizarProgramacionDependienta(ProgramacionEstimadaGanttDTO programacion, ProgramacionEstimadaGanttDTO registro, List<DependenciaProgramacionEstimadaDTO> dependencias, DbContext db) {

            var programaciones = await _ProgramacionEstimadaGanttService.ObtenerXIdProyecto(programacion.IdProyecto, db);

            foreach (var dpendencia in dependencias) {
                var programacionDependiente = programaciones.Where(z => z.Id == dpendencia.IdProgramacionEstimadaGantt.ToString()).First();
                
                //var FI = programacionDependiente.Start;
                //var FF = programacionDependiente.End;
                var programacionVieja = new ProgramacionEstimadaGanttDTO();
                programacionVieja.Id = programacionDependiente.Id;
                programacionVieja.Start = programacionDependiente.Start;
                programacionVieja.End = programacionDependiente.End;
                programacionVieja.Parent = programacionDependiente.Parent;
                programacionVieja.IdProyecto = programacionDependiente.IdProyecto;
                var diferencia = (programacionDependiente.End - programacionDependiente.Start).Days;
                var diferenciaActual = (programacionDependiente.Start - programacion.End).Days;

                //if (registro.End > programacion.End) {
                    programacionDependiente.Start = registro.End.AddDays(diferenciaActual);
                    programacionDependiente.End = programacionDependiente.Start.AddDays(diferencia);
                //}
                //if (registro.End < programacion.End) {
                //    programacionDependiente.Start = registro.End.AddDays(diferenciaActual);
                //    programacionDependiente.End = programacionDependiente.Start.AddDays(diferencia);
                //}

                var dependenciasPadre = await _DependenciaProgramacionEstimadaService.ObtenerXIdProgramacionEstimadaGantt(Convert.ToInt32(programacionDependiente.Id));
                var listaProDep = new List<ProgramacionEstimadaGanttDTO>();
                int DiasDesfase = 0;
                if (dependenciasPadre.Count > 0)
                {
                    foreach (var dep in dependenciasPadre)
                    {
                        var proEstimada = programaciones.Where(z => z.Id == dep.SourceId);
                        listaProDep.AddRange(proEstimada);
                    }
                    foreach (var listaPro in listaProDep) {
                        if (listaPro.Id == registro.Id) {
                            var d = (registro.End - listaPro.End).Days;
                            listaPro.End = registro.End;
                        }
                    }

                    var FechaMaxima = listaProDep.OrderByDescending(z => z.End).FirstOrDefault();

                    DiasDesfase = (programacionDependiente.Start - FechaMaxima.End).Days;
                }
                programacionDependiente.DesfaseComando = DiasDesfase;

                var editarProDependiente = await _ProgramacionEstimadaGanttService.Editar(programacionDependiente);
                
                    await EditarProgramacionEstimadaPadre(programacionDependiente, db);
                var dependenciasNuevas = await _DependenciaProgramacionEstimadaService.ObtenerXIdPredesesora(Convert.ToInt32(programacionDependiente.Id));
                if (dependenciasNuevas.Count() > 0) {
                    await ActualizarProgramacionDependienta(programacionVieja, programacionDependiente, dependenciasNuevas, db);
                }
            }
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

        public async Task GenerarDependencia(DependenciaProgramacionEstimadaDTO registro)
        {
            await _DependenciaProgramacionEstimadaService.CrearYObtener(registro);
            var programaciones = await ObtenerProgramacionEstimadaXIdProyecto(registro.IdProyecto, _DbContext);
            var programacionBase = programaciones.Where(z => z.Id == Convert.ToString(registro.IdProgramacionEstimadaGantt)).FirstOrDefault();
            var programacionDependiente = programaciones.Where(z => z.Id == registro.SourceId).FirstOrDefault();
            var diferenciaDias = (programacionBase.End - programacionBase.Start).Days;
            programacionBase.Comando = 2;
            programacionBase.Start = programacionDependiente.End;
            programacionBase.End = programacionDependiente.End.AddDays(diferenciaDias);
            await EditarFechaProgramacionEstimada(programacionBase, _DbContext);
        }

        public async Task AsignarProgreso(ProgramacionEstimadaGanttDTO registro, DbContext db)
        {
            var programaciones = await ObtenerProgramacionEstimadaXIdProyecto(registro.IdProyecto, db);
            var registroEditado = programaciones.Where(z => z.Id == registro.Id).FirstOrDefault();
            registroEditado.Progress = registro.Progress;
            await _ProgramacionEstimadaGanttService.Editar(registroEditado);
            if(registro.Parent != null)
            {
                var programacionPadre = programaciones.Where(z => z.Id == registro.Parent).FirstOrDefault();
                var hijos = programaciones.Where(z => z.Parent == programacionPadre.Id).ToList();
                if (hijos.Count > 0)
                {
                    int SumaProgreso = 0;
                    foreach (ProgramacionEstimadaGanttDTO registroHijo in hijos)
                    {
                        SumaProgreso = SumaProgreso + registroHijo.Progress;
                    }
                    SumaProgreso = SumaProgreso / hijos.Count();
                    programacionPadre.Progress = SumaProgreso;
                    await _ProgramacionEstimadaGanttService.Editar(programacionPadre);
                    if (programacionPadre.Parent != "0")
                    {
                        await RecalcularProgresoPadre(programacionPadre, db);
                    }
                }
            }
        }


        public async Task RecalcularProgresoPadre(ProgramacionEstimadaGanttDTO registro, DbContext db)
        {
            var programaciones = await ObtenerProgramacionEstimadaXIdProyecto(registro.IdProyecto, db);
            if(registro.Parent != null)
            {
                var programacionPadre = programaciones.Where(z => z.Id == registro.Parent).FirstOrDefault();
                var hijos = programaciones.Where(z => z.Parent == programacionPadre.Id).ToList();
                if (hijos.Count > 0)
                {
                    int SumaProgreso = 0;
                    foreach (ProgramacionEstimadaGanttDTO registroHijo in hijos)
                    {
                        SumaProgreso = SumaProgreso + registroHijo.Progress;
                    }
                    SumaProgreso = SumaProgreso / hijos.Count();
                    programacionPadre.Progress = SumaProgreso;
                    await _ProgramacionEstimadaGanttService.Editar(programacionPadre);
                    if (programacionPadre.Parent != "0")
                    {
                        await RecalcularProgresoPadre(programacionPadre, db);
                    }
                }
            }
        }

        public async Task GenerarDependenciaXNumerador(ProgramacionEstimadaGanttDTO registro, DbContext db)
        {
            var programaciones = await ObtenerProgramacionEstimadaXIdProyecto(registro.IdProyecto, db);
            var programacionBase = programaciones.Where(z => z.Numerador == registro.Numerador).FirstOrDefault();
            var programacionDependiente = programaciones.Where(z => z.Numerador == registro.Predecesor).FirstOrDefault();
            var nuevaDependencia = new DependenciaProgramacionEstimadaDTO();
            nuevaDependencia.IdProgramacionEstimadaGantt = Convert.ToInt32(programacionBase.Id);
            nuevaDependencia.SourceId = programacionDependiente.Id;
            nuevaDependencia.IdProyecto = registro.IdProyecto;
            nuevaDependencia.SourceTarget = programacionDependiente.Id;
            var nuevaDCreada = await _DependenciaProgramacionEstimadaService.CrearYObtener(nuevaDependencia);
            registro.Comando = 2;
            var diferenciaDias = (registro.End - registro.Start).Days;
            var programacionDependiente1 = programaciones.Where(z => z.Id == programacionDependiente.Id).FirstOrDefault();
            registro.Start = programacionDependiente1.End;
            registro.End = registro.Start.AddDays(diferenciaDias);
            await EditarFechaProgramacionEstimada(registro, db);
        }

        public async Task AsignarComando(ProgramacionEstimadaGanttDTO registro, DbContext db)
        {
            var diferenciaDias = (registro.End - registro.Start).Days;
            var programaciones = await ObtenerProgramacionEstimadaXIdProyecto(registro.IdProyecto, db);
            if(registro.Dependencies.Count > 0)
            {
                switch (registro.Comando)
                {
                    case 0://solo cuando no haya relación
                        if(registro.Dependencies.Count > 0)
                        {
                            foreach(var dependencia in registro.Dependencies)
                            {
                                await _DependenciaProgramacionEstimadaService.Eliminar(dependencia.Id);
                                registro.DesfaseComando = 0;
                                await EditarFechaProgramacionEstimada(registro, db);
                            }
                        }
                        else
                        {
                            return;
                        }
                        break;

                    case 1://Comienzo Comienzo, por lo que inician al mismo tiempo
                        var programacionDependiente = programaciones.Where(z => z.Id == registro.Dependencies[0].SourceId).FirstOrDefault();
                        registro.Start = programacionDependiente.Start.AddDays(registro.DesfaseComando);
                        registro.End = registro.Start.AddDays(diferenciaDias);
                        await EditarFechaProgramacionEstimada(registro, db);
                        break;

                    case 2://Comienzo Fin, por lo que la actividad comienza al termino de la anterior
                        programacionDependiente = programaciones.Where(z => z.Id == registro.Dependencies[0].SourceId).FirstOrDefault();
                        registro.Start = programacionDependiente.End.AddDays(registro.DesfaseComando);
                        registro.End = registro.Start.AddDays(diferenciaDias);
                        await EditarFechaProgramacionEstimada(registro, db);
                        break;

                    case 3://Fin Comienzo, por lo que la actividad termina cuando comienza la anterior
                        programacionDependiente = programaciones.Where(z => z.Id == registro.Dependencies[0].SourceId).FirstOrDefault();
                        registro.End = programacionDependiente.Start.AddDays(registro.DesfaseComando);
                        registro.Start = registro.End.AddDays(-diferenciaDias).AddDays(registro.DesfaseComando);
                        await EditarFechaProgramacionEstimada(registro, db);
                        break;

                    case 4://Fin Fin, por lo que la actividad termina cuando termina la anterior
                        programacionDependiente = programaciones.Where(z => z.Id == registro.Dependencies[0].SourceId).FirstOrDefault();
                        registro.End = programacionDependiente.End.AddDays(registro.DesfaseComando);
                        registro.Start = registro.End.AddDays(-diferenciaDias);
                        await EditarFechaProgramacionEstimada(registro, db);
                        break;

                    default:
                        return;
                        break;
                }
            }
        }

        public async Task AsignarDesfase(ProgramacionEstimadaGanttDTO registro, DbContext db)
        {
            if(registro.Dependencies.Count > 0)
            {
                var diferenciaDias = (registro.End - registro.Start).Days;
                var programaciones = await ObtenerProgramacionEstimadaXIdProyecto(registro.IdProyecto, db);

                switch (registro.Comando)
                {
                    case 0://solo cuando no haya relación
                        if (registro.Dependencies.Count > 0)
                        {
                            foreach (var dependencia in registro.Dependencies)
                            {
                                await _DependenciaProgramacionEstimadaService.Eliminar(dependencia.Id);
                                registro.DesfaseComando = 0;
                                await EditarFechaProgramacionEstimada(registro, db);
                            }
                        }
                        else
                        {
                            return;
                        }
                        break;

                    case 1://Comienzo Comienzo, por lo que inician al mismo tiempo
                        var programacionDependiente = programaciones.Where(z => z.Id == registro.Dependencies[0].SourceId).FirstOrDefault();
                        registro.Start = programacionDependiente.Start.AddDays(registro.DesfaseComando);
                        registro.End = registro.Start.AddDays(diferenciaDias);
                        await EditarFechaProgramacionEstimada(registro, db);
                        break;

                    case 2://Comienzo Fin, por lo que la actividad comienza al termino de la anterior
                        programacionDependiente = programaciones.Where(z => z.Id == registro.Dependencies[0].SourceId).FirstOrDefault();
                        registro.Start = programacionDependiente.End.AddDays(registro.DesfaseComando);
                        registro.End = registro.Start.AddDays(diferenciaDias);
                        await EditarFechaProgramacionEstimada(registro, db);
                        break;

                    case 3://Fin Comienzo, por lo que la actividad termina cuando comienza la anterior
                        programacionDependiente = programaciones.Where(z => z.Id == registro.Dependencies[0].SourceId).FirstOrDefault();
                        registro.End = programacionDependiente.Start.AddDays(registro.DesfaseComando);
                        registro.Start = registro.End.AddDays(-diferenciaDias);
                        await EditarFechaProgramacionEstimada(registro, db);
                        break;

                    case 4://Fin Fin, por lo que la actividad termina cuando termina la anterior
                        programacionDependiente = programaciones.Where(z => z.Id == registro.Dependencies[0].SourceId).FirstOrDefault();
                        registro.End = programacionDependiente.End.AddDays(registro.DesfaseComando);
                        registro.Start = registro.End.AddDays(-diferenciaDias);
                        await EditarFechaProgramacionEstimada(registro, db);
                        break;

                    default:
                        return;
                        break;
                }
            }
        }

        public async Task editarFechaProyectoGantt(ProgramacionEstimadaGanttDTO registro)
        {
            await _ProgramacionEstimadaGanttService.Editar(registro);
        }
    }
}

