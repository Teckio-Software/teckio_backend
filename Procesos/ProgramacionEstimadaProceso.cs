using Microsoft.EntityFrameworkCore;


using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;
using System.Globalization;

namespace ERP_TECKIO
{
    public class ProgramacionEstimadaProceso<TContext> where TContext : DbContext
    {
        private readonly IProgramacionEstimadaService<TContext> _ProgramacionEstimadaService;
        private readonly IPrecioUnitarioService<TContext> _PrecioUnitarioService;
        private readonly IConceptoService<TContext> _ConceptoService;
        private readonly IProyectoService<TContext> _ProyectoService;
        private readonly PrecioUnitarioProceso<TContext> _PrecioUnitarioProceso;

        public ProgramacionEstimadaProceso(
            IProgramacionEstimadaService<TContext> programacionEstimadaService
            , IPrecioUnitarioService<TContext> precioUnitarioService
            , IConceptoService<TContext> conceptoService
            , IProyectoService<TContext> proyectoService
            , PrecioUnitarioProceso<TContext> precioUnitarioProceso)
        {
            _ProgramacionEstimadaService = programacionEstimadaService;
            _PrecioUnitarioService = precioUnitarioService;
            _ConceptoService = conceptoService;
            _ProyectoService = proyectoService;
            _PrecioUnitarioProceso = precioUnitarioProceso;
        }

        public async Task<List<ProgramacionEstimadaDTO>> ObtenerProgramacionEstimada(int IdProyecto)
        {
            var registros = await Enumerar(IdProyecto);
            var conceptos = await _ConceptoService.ObtenerTodos(IdProyecto);
            var proyecto = await _ProyectoService.ObtenXId(IdProyecto);
            var preciosUnitarios = await _PrecioUnitarioService.ObtenerTodos(IdProyecto);
            for(int i = 0; i < registros.Count; i++)
            {
                var concepto = conceptos.Where(z => z.Id == registros[i].IdConcepto).FirstOrDefault();
                var PU = preciosUnitarios.Where(z => z.Id == registros[i].IdPrecioUnitario).FirstOrDefault();
                registros[i].Codigo = concepto.Codigo;
                registros[i].Descripcion = concepto.Descripcion;
                registros[i].Unidad = concepto.Unidad;
                registros[i].CostoUnitario = concepto.CostoUnitario;
                registros[i].InicioParaGantt = ((DateTimeOffset)registros[i].Inicio).ToUnixTimeSeconds();
                registros[i].TerminoParaGantt = ((DateTimeOffset)registros[i].Termino).ToUnixTimeSeconds();
                registros[i].Cantidad = PU.Cantidad;
                registros[i].EsDomingo = proyecto.EsDomingo;
                registros[i].EsSabado = proyecto.EsSabado;
                registros[i].TipoPrecioUnitario = PU.TipoPrecioUnitario;
                if (registros[i].Cantidad != null)
                {
                    registros[i].Importe = registros[i].CostoUnitario * registros[i].Cantidad;
                }
            }
            registros = await EstructurarProgramacionEstimadaParaDiagrama(registros);
            return registros;
        }

        public async Task<List<ProgramacionEstimadaDTO>> ObtenerProgramacionEstimadaEstructurada(int IdProyecto)
        {
            var registros = await Enumerar(IdProyecto);
            var conceptos = await _ConceptoService.ObtenerTodos(IdProyecto);
            var proyecto = await _ProyectoService.ObtenXId(IdProyecto);
            var preciosUnitarios = await _PrecioUnitarioService.ObtenerTodos(IdProyecto);
            for (int i = 0; i < registros.Count; i++)
            {
                var concepto = conceptos.Where(z => z.Id == registros[i].IdConcepto).FirstOrDefault();
                var PU = preciosUnitarios.Where(z => z.Id == registros[i].IdPrecioUnitario).FirstOrDefault();
                registros[i].Codigo = concepto.Codigo;
                registros[i].Descripcion = concepto.Descripcion;
                registros[i].Unidad = concepto.Unidad;
                registros[i].CostoUnitario = concepto.CostoUnitario;
                registros[i].InicioParaGantt = ((DateTimeOffset)registros[i].Inicio).ToUnixTimeSeconds();
                registros[i].TerminoParaGantt = ((DateTimeOffset)registros[i].Termino).ToUnixTimeSeconds();
                registros[i].Cantidad = PU.Cantidad;
                registros[i].EsDomingo = proyecto.EsDomingo;
                registros[i].EsSabado = proyecto.EsSabado;
                registros[i].TipoPrecioUnitario = PU.TipoPrecioUnitario;
                if (registros[i].Cantidad != null)
                {
                    registros[i].Importe = registros[i].CostoUnitario * registros[i].Cantidad;
                }
            }
            registros = await EstructurarProgramacionEstimada(registros);
            return registros;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="registros"></param>
        /// <returns></returns>
        public async Task<List<ProgramacionEstimadaDTO>> EstructurarProgramacionEstimada(List<ProgramacionEstimadaDTO> registros)
        {
            var registrosPadre = registros.Where(z => z.IdPadre == 0).ToList();
            foreach(ProgramacionEstimadaDTO registroPadre in registrosPadre)
            {
                registroPadre.Hijos = await BuscaHijos(registros, registroPadre);
            }
            return registrosPadre;
        }

        public async Task<List<ProgramacionEstimadaDTO>> BuscaHijos(List<ProgramacionEstimadaDTO> registros, ProgramacionEstimadaDTO padre)
        {
            var padres = registros.Where(z => z.IdPadre == padre.Id).ToList();
            foreach(ProgramacionEstimadaDTO registroPadre in padres)
            {
                registroPadre.Hijos = await BuscaHijos(registros, registroPadre);
            }
            return padres;
        }

        /// <summary>
        /// Metodos para estructurar el diagrama de gantt en el orden que se despliega la tabla con estructura de arbol.
        /// </summary>
        /// <param name="registros">Lista de todos los registros, no hay ningun filtrado especial</param>
        /// <returns>La misma lista pero ordenada de la misma forma que la que tiene estructura de arbol
        /// la diferencia radica en que esta no esta compuesta de nodos, sino es una lista lineal</returns>
        public async Task<List<ProgramacionEstimadaDTO>> EstructurarProgramacionEstimadaParaDiagrama(List<ProgramacionEstimadaDTO> registros)
        {
            var programaciones = new List<ProgramacionEstimadaDTO>();
            var registrosPadre = registros.Where(z => z.IdPadre == 0).ToList();
            foreach (ProgramacionEstimadaDTO registroPadre in registrosPadre)
            {
                programaciones.Add(registroPadre);
                var nuevosRegistros = await BuscaHijosParaDiagrama(registros, registroPadre);
                programaciones.AddRange(nuevosRegistros);
            }
            return programaciones;
        }

        public async Task<List<ProgramacionEstimadaDTO>> BuscaHijosParaDiagrama(List<ProgramacionEstimadaDTO> registros, ProgramacionEstimadaDTO padre)
        {
            var programaciones = new List<ProgramacionEstimadaDTO>();
            var padres = registros.Where(z => z.IdPadre == padre.Id).ToList();
            foreach (ProgramacionEstimadaDTO registroPadre in padres)
            {
                programaciones.Add(registroPadre);
                var nuevosRegistros = await BuscaHijosParaDiagrama(registros, registroPadre);
                programaciones.AddRange(nuevosRegistros);
            }
            return programaciones;
        }

        public async Task<ProgramacionEstimadaDTO> CrearProgramacionEstimada(ProgramacionEstimadaDTO registro)
        {
            var ProgramacioEstimada = await _ProgramacionEstimadaService.CrearYObtener(registro);
            return ProgramacioEstimada;
        }

        public async Task EditarProgramacionEstimada(ProgramacionEstimadaDTO registro)
        {
            var programacionEstimadaEditada = await _ProgramacionEstimadaService.ObtenXId(registro.Id);
            await _ProgramacionEstimadaService.Editar(registro);
            return;
        }

        public async Task AsignaPredecesorAsync(ProgramacionEstimadaDTO registroDTO)
        {
            registroDTO.Comando = 1;
            if (registroDTO.IdPredecesora == 0)
            {
                registroDTO.Comando = 0;
                var asd = await _ProgramacionEstimadaService.Editar(registroDTO);
                return;
            }
            var registro = _ProgramacionEstimadaService.ObtenXId(registroDTO.IdPredecesora).Result;
            var PU = await _PrecioUnitarioService.ObtenXId(registro.IdPrecioUnitario);
            if(PU.TipoPrecioUnitario == 0)
            {
                return;
            }
            registroDTO.Inicio = registro.Termino.AddDays(1);
            if (registroDTO.Inicio.DayOfWeek == DayOfWeek.Saturday && registroDTO.EsSabado)
            {
                registroDTO.Inicio = registroDTO.Inicio.AddDays(1);
            }
            if (registroDTO.Inicio.DayOfWeek == DayOfWeek.Sunday && registroDTO.EsDomingo)
            {
                registroDTO.Inicio = registroDTO.Inicio.AddDays(1);
            }
            registroDTO.Termino = registroDTO.Inicio;
            for (int i = 0; i < registroDTO.DiasTranscurridos - 1; i++)
            {
                registroDTO.Termino = registroDTO.Termino.AddDays(1);
                if (registroDTO.Termino.DayOfWeek == DayOfWeek.Saturday && registroDTO.EsSabado)
                {
                    registroDTO.Termino = registroDTO.Termino.AddDays(1);
                }
                if (registroDTO.Termino.DayOfWeek == DayOfWeek.Sunday && registroDTO.EsDomingo)
                {
                    registroDTO.Termino = registroDTO.Termino.AddDays(1);
                }
            }
            registroDTO.DiasTranscurridos = (registroDTO.Termino - registroDTO.Inicio).Days + 1;
            await _ProgramacionEstimadaService.Editar(registroDTO);
            var registroFinal = ObtenerProgramacionEstimada(registroDTO.IdProyecto).Result;
            if (registroFinal.Count <= 0)
            {
                return;
            }
            else
            {
                await RecorreFechasConceptosSucesores(registroFinal, registroDTO);
            }
        }

        public async Task RecorreFechasConceptosSucesores(List<ProgramacionEstimadaDTO> listaSucesores, ProgramacionEstimadaDTO Predecesor)
        {
            if (listaSucesores.Count > 0)
            {
                var hijos = listaSucesores.Where(z => z.IdPredecesora == Predecesor.Id).ToList();
                for (int i = 0; i < hijos.Count; i++)
                {
                    if (hijos[i].Inicio < Predecesor.Termino)
                    {
                        hijos[i].Inicio = Predecesor.Termino.AddDays(1);
                        if (hijos[i].Inicio.DayOfWeek == DayOfWeek.Saturday && hijos[i].EsSabado)
                        {
                            hijos[i].Inicio = hijos[i].Inicio.AddDays(1);
                        }
                        if (hijos[i].Inicio.DayOfWeek == DayOfWeek.Sunday && hijos[i].EsDomingo)
                        {
                            hijos[i].Inicio = hijos[i].Inicio.AddDays(1);
                        }
                        hijos[i].Termino = hijos[i].Inicio;
                        for (int j = 0; j < hijos[i].DiasTranscurridos - 1; j++)
                        {
                            hijos[i].Termino = hijos[i].Termino.AddDays(1);
                            if (hijos[i].Termino.DayOfWeek == DayOfWeek.Saturday && hijos[i].EsSabado)
                            {
                                hijos[i].Termino = hijos[i].Termino.AddDays(1);
                            }
                            if (hijos[i].Termino.DayOfWeek == DayOfWeek.Sunday && hijos[i].EsDomingo)
                            {
                                hijos[i].Termino = hijos[i].Termino.AddDays(1);
                            }
                        }
                        var registro = ObtenerProgramacionEstimada(Predecesor.IdProyecto).Result;
                        var lista1 = registro.Where(z => z.IdPredecesora == hijos[i].Id).ToList();
                        for (int j = 0; j < lista1.Count; j++)
                        {
                            await RecorreFechasConceptosSucesores(lista1, hijos[i]);
                        }
                        hijos[i].DiasTranscurridos = (hijos[i].Termino - hijos[i].Inicio).Days + 1;
                        hijos[i].DiasTranscurridos = (hijos[i].Termino - hijos[i].Inicio).Days + 1;
                        await _ProgramacionEstimadaService.Editar(hijos[i]);
                    }
                }
            }
        }

        public async Task RecorreFechasPartidasPadre(int idProyecto)
        {
            var registros = ObtenerProgramacionEstimada(idProyecto).Result;
            var lista = registros.Where(z => z.TipoPrecioUnitario != 1).OrderByDescending(z => z.Nivel).FirstOrDefault();//Obtener el nivel maximo de subpartida que existe
            for (int i = lista.Nivel; i > 0; i--)
            {
                var registros1 = ObtenerProgramacionEstimada(idProyecto).Result;
                var lista1 = registros1.Where(z => z.Nivel == i).Where(z => z.TipoPrecioUnitario != 1).ToList();//Obtener todas las subpartidas o partidas en el ultimo nivel
                for (int j = 0; j < lista1.Count; j++)//Para cada sub partida o partida
                {
                    var lista2 = registros1.Where(z => z.IdPadre == lista1[j].Id).ToList();//Obtener todos los conceptos o sub partidas hijos
                    if (lista2.Count > 0)
                    {
                        var inicio = lista2.OrderBy(z => z.Inicio).FirstOrDefault().Inicio;//se obtiene la fecha inicial
                        var termino = lista2.OrderByDescending(z => z.Termino).FirstOrDefault().Termino;//se obtiene la fecha final
                        lista1[j].Inicio = inicio;//Error de que el primer nivel no se refresca
                        lista1[j].Termino = termino;
                        var difDias = (termino - inicio).Days;//se calcula la diferencia en días
                        var fechaAux = inicio;
                        var diasTranscurridos = difDias + 1;
                        for (int aux = 0; aux < difDias; aux++)//calcula días transcurridos
                        {
                            if (fechaAux.DayOfWeek == DayOfWeek.Saturday && lista1[j].EsSabado)
                            {
                                diasTranscurridos = diasTranscurridos - 1;
                            }
                            if (fechaAux.DayOfWeek == DayOfWeek.Sunday && lista1[j].EsDomingo)
                            { 
                                diasTranscurridos = diasTranscurridos - 1;
                            }
                            fechaAux = fechaAux.AddDays(1);
                        }
                        lista1[j].DiasTranscurridos = diasTranscurridos;
                        lista1[j].DiasTranscurridos = (lista1[j].Termino - lista1[j].Inicio).Days + 1;
                        await _ProgramacionEstimadaService.Editar(lista1[j]);//edita
                    }
                }
            }
        }

        public async Task<List<ProgramacionEstimadaDTO>> ValidacionDePredecesor(int idPredecesora, int idBase)
        {
            var lista = new List<ProgramacionEstimadaDTO>();
            if (idPredecesora <= 0)
            {
                return lista;
            }
            var registro = await _ProgramacionEstimadaService.ObtenXId(idPredecesora);
            if (registro.Id == idBase)
            {
                lista.Add(registro);
                return lista;
            }
            if (registro.IdPredecesora <= 0)
            {
                return lista;
            }
            lista = BuscaPredecesores(registro.IdPredecesora, idBase).Result;
            return lista;
        }

        public async Task<List<ProgramacionEstimadaDTO>> BuscaPredecesores(int idPredecesora, int idBase)
        {
            var lista = new List<ProgramacionEstimadaDTO>();
            var registro = await _ProgramacionEstimadaService.ObtenXId(idPredecesora);
            if (registro.Id == idBase)
            {
                lista.Add(registro);
                return lista;
            }
            if (registro.IdPredecesora <= 0)
            {
                return lista;
            }
            lista = BuscaPredecesores(registro.IdPredecesora, idBase).Result;
            return lista;
        }

        public async Task RecorreSucesores(ProgramacionEstimadaDTO registro, DateTime FechaTerminoBase)
        {
            var registros = ObtenerProgramacionEstimada(registro.IdProyecto).Result;
            var lista = registros.Where(z => z.IdPredecesora == registro.Id).ToList(); //Se obtienen todos los registros cuya predecesora sea el id de la anterior
            if (lista.Count <= 0)
            {
                return;
            }
            for (int i = 0; i < lista.Count; i++)
            {
                var fechaTerminoAux = FechaTerminoBase; //se almacena la fecha de termino original de este registro para pasarla al metodo para recorrer sucesores
                int difDays = Convert.ToInt32((lista[i].Inicio - FechaTerminoBase).TotalDays); //se obtiene la diferencia de dias que ya existia entre los 2 registros
                lista[i].Inicio = registro.Termino.AddDays(1); //Se añade 1 día y en el caso de que caiga sabado o domingo (y estos sean días de descanso) se agregan esos días
                var diasTranscurridos = 0;
                if (lista[i].Inicio.DayOfWeek == DayOfWeek.Saturday && lista[i].EsSabado)
                {
                    lista[i].Inicio = lista[i].Inicio.AddDays(1);
                    diasTranscurridos = diasTranscurridos + 1;
                }
                if (lista[i].Inicio.DayOfWeek == DayOfWeek.Sunday && lista[i].EsDomingo)
                {
                    lista[i].Inicio = lista[i].Inicio.AddDays(1);
                    diasTranscurridos = diasTranscurridos + 1;
                }
                var aux = difDays;
                for (int j = 0; j < (difDays - diasTranscurridos - 1); j++) //se agrega la cantidad de días que existia de diferencia originalmente entre el registro predecesor.
                {
                    lista[i].Inicio = lista[i].Inicio.AddDays(1);
                    if (lista[i].Inicio.DayOfWeek == DayOfWeek.Saturday && lista[i].EsSabado)
                    {
                        lista[i].Inicio = lista[i].Inicio.AddDays(1);
                        j = j + 1;
                    }
                    if (lista[i].Inicio.DayOfWeek == DayOfWeek.Sunday && lista[i].EsDomingo)
                    {
                        lista[i].Inicio = lista[i].Inicio.AddDays(1);
                        j = j + 1;
                    }
                }
                lista[i].Termino = lista[i].Inicio;
                for (int j = 0; j < (lista[i].DiasTranscurridos - 1); j++) //se asigna la fecha de termino a partir de la nueva fecha de inicio y se le agregan los días transcurridos que tenía el registro original (se pone -1 en días transcurridos porque es la duración)
                {
                    lista[i].Termino = lista[i].Termino.AddDays(1);
                    if (lista[i].Termino.DayOfWeek == DayOfWeek.Saturday && lista[i].EsSabado)
                    {
                        lista[i].Termino = lista[i].Termino.AddDays(1);
                    }
                    if (lista[i].Termino.DayOfWeek == DayOfWeek.Sunday && lista[i].EsDomingo)
                    {
                        lista[i].Termino = lista[i].Termino.AddDays(1);
                    }
                }
                await _ProgramacionEstimadaService.Editar(lista[i]);//Se editan
                await RecorreSucesoresHijos(lista[i], fechaTerminoAux);
            }
        }

        public async Task RecorreSucesoresHijos(ProgramacionEstimadaDTO registro, DateTime FechaTerminoBase)
        {
            var registros = ObtenerProgramacionEstimada(registro.IdProyecto).Result;
            var lista = registros.Where(z => z.IdPredecesora == registro.Id).ToList(); //Se obtienen todos los registros cuya predecesora sea el id de la anterior
            if (lista.Count <= 0)
            {
                return;
            }
            for (int i = 0; i < lista.Count; i++)
            {
                var fechaTerminoAux = FechaTerminoBase; //se almacena la fecha de termino original de este registro para pasarla al metodo para recorrer sucesores
                int difDays = Convert.ToInt32((lista[i].Inicio - FechaTerminoBase).TotalDays); //se obtiene la diferencia de dias que ya existia entre los 2 registros
                lista[i].Inicio = registro.Termino.AddDays(1); //Se añade 1 día y en el caso de que caiga sabado o domingo (y estos sean días de descanso) se agregan esos días
                var diasTranscurridos = 0;
                if (lista[i].Inicio.DayOfWeek == DayOfWeek.Saturday && lista[i].EsSabado)
                {
                    lista[i].Inicio = lista[i].Inicio.AddDays(1);
                    diasTranscurridos = diasTranscurridos + 1;
                }
                if (lista[i].Inicio.DayOfWeek == DayOfWeek.Sunday && lista[i].EsDomingo)
                {
                    lista[i].Inicio = lista[i].Inicio.AddDays(1);
                    diasTranscurridos = diasTranscurridos + 1;
                }
                var aux = difDays;
                for (int j = 0; j < (difDays - diasTranscurridos - 1); j++) // se agrega la cantidad de días que existia de diferencia originalmente entre el registro predecesor.
                {
                    lista[i].Inicio = lista[i].Inicio.AddDays(1);
                    if (lista[i].Inicio.DayOfWeek == DayOfWeek.Saturday && lista[i].EsSabado)
                    {
                        lista[i].Inicio = lista[i].Inicio.AddDays(1);
                        j = j + 1;
                    }
                    if (lista[i].Inicio.DayOfWeek == DayOfWeek.Sunday && lista[i].EsDomingo)
                    {
                        lista[i].Inicio = lista[i].Inicio.AddDays(1);
                        j = j + 1;
                    }
                }
                lista[i].Termino = lista[i].Inicio;
                for (int j = 0; j < (lista[i].DiasTranscurridos - 1); j++) // se asigna la fecha de termino a partir de la nueva fecha de inicio y se le agregan los días transcurridos que tenía el registro original (se pone -1 en días transcurridos porque es la duración)
                {
                    lista[i].Termino = lista[i].Termino.AddDays(1);
                    if (lista[i].Termino.DayOfWeek == DayOfWeek.Saturday && lista[i].EsSabado)
                    {
                        lista[i].Termino = lista[i].Termino.AddDays(1);
                    }
                    if (lista[i].Termino.DayOfWeek == DayOfWeek.Sunday && lista[i].EsDomingo)
                    {
                        lista[i].Termino = lista[i].Termino.AddDays(1);
                    }
                }
                await _ProgramacionEstimadaService.Editar(lista[i]);//Se editan
                await RecorreSucesoresHijos(lista[i], fechaTerminoAux);
            }
        }

        public async Task Crear(ProgramacionEstimadaDTO registro)
        {
            if(registro.IdPadre > 0)
            {
                registro.Nivel++;
            }
            var registros = ObtenerProgramacionEstimada(registro.IdProyecto).Result;
            if(registros.Count < 0)
            {
                return;
            }
            await _ProgramacionEstimadaService.CrearYObtener(registro);
            return;
        }

        public async Task Editar(ProgramacionEstimadaDTO registro)
        {
            var RegistroEncontrado = await _ProgramacionEstimadaService.ObtenXId(registro.Id);
            return;
        }

        public async Task EditarConDatePicker(ProgramacionEstimadaDTO registro)
        {
            if (registro.Termino < registro.Inicio)
            {
                return;
            }
            var fechaTerminoAux = registro.Termino;
            var registroEncontrado = await _ProgramacionEstimadaService.ObtenXId(registro.Id);
            var Predecesor = new ProgramacionEstimadaDTO();
            if (registro.IdPredecesora > 0)
            {
                Predecesor = await _ProgramacionEstimadaService.ObtenXId(registro.IdPredecesora);
                if (registro.Inicio <= Predecesor.Inicio)
                {
                    return;
                }
            }
            var fechaAux = registro.Inicio;
            var difDias = Convert.ToInt32((registro.Termino - registro.Inicio).TotalDays);
            var auxDias = difDias;
            for (int i = 0; i < auxDias; i++)
            {
                fechaAux = fechaAux.AddDays(1);
                if (fechaAux.DayOfWeek == DayOfWeek.Saturday && registro.EsSabado)
                {
                    difDias = difDias - 1;
                }
                if (fechaAux.DayOfWeek == DayOfWeek.Sunday && registro.EsDomingo)
                {
                    difDias = difDias - 1;
                }
            }
            registro.DiasTranscurridos = difDias + 1;
            registro.DiasTranscurridos = (registro.Termino - registro.Inicio).Days + 1;
            await _ProgramacionEstimadaService.Editar(registro);
            await RecorreSucesores(registro, fechaTerminoAux);
            await RecorreFechasPartidasPadre(registro.IdProyecto);
            return;
        }

        public async Task EditarGantt(ProgramacionEstimadaDTO registro)
        {
            var RegistroEncontrado = await _ProgramacionEstimadaService.ObtenXId(registro.Id);
            RegistroEncontrado.Inicio = registro.Inicio.Date;
            RegistroEncontrado.Termino = registro.Termino.Date;
            RegistroEncontrado.Predecesor = registro.Predecesor;
            RegistroEncontrado.DiasTranscurridos = registro.DiasTranscurridos;

            registro.DiasTranscurridos = (registro.Termino - registro.Inicio).Days + 1;
            await _ProgramacionEstimadaService.Editar(registro);
            await RecorreFechasPartidasPadre(registro.IdProyecto);
            return;
        }

        public async Task PutFechaDias(ProgramacionEstimadaDTO registro)
        {
            var fechaTerminoAux = registro.Termino;
            var registroEncontrado = await _ProgramacionEstimadaService.ObtenXId(registro.Id);
            DateTime fechaFor = registro.Inicio;
            for (int i = 0; i < registro.DiasTranscurridos - 1; i++)
            {
                fechaFor = fechaFor.AddDays(1);
                if (fechaFor.DayOfWeek == DayOfWeek.Saturday && registro.EsSabado)
                {
                    fechaFor = fechaFor.AddDays(1);
                }
                if (fechaFor.DayOfWeek == DayOfWeek.Sunday && registro.EsDomingo)
                {
                    fechaFor = fechaFor.AddDays(1);
                }
            }
            if (fechaFor.DayOfWeek == DayOfWeek.Sunday && registro.EsDomingo)
            {
                fechaFor = fechaFor.AddDays(1);
            }
            registro.Termino = fechaFor;
            registro.DiasTranscurridos = (registro.Termino - registro.Inicio).Days + 1;
            await _ProgramacionEstimadaService.Editar(registro);
            await RecorreSucesores(registro, fechaTerminoAux);
            await RecorreFechasPartidasPadre(registro.IdProyecto);
            return;
        }

        public async Task PutEditaPredecesora(ProgramacionEstimadaDTO programacionEstimada)
        {
            if (programacionEstimada.Id == programacionEstimada.IdPredecesora)
            {
                return;
            }
            var fechaTerminoAux = programacionEstimada.Termino;
            var lista = ValidacionDePredecesor(programacionEstimada.IdPredecesora, programacionEstimada.Id).Result;
            if (lista.Count > 0)
            {
                return;
            }
            await AsignaPredecesorAsync(programacionEstimada);
            await RecorreSucesores(programacionEstimada, fechaTerminoAux);
            await RecorreFechasPartidasPadre(programacionEstimada.IdProyecto);
            return;
        }

        public async Task<ProgramacionEstimadaDTO> ObtenerXId(int Id)
        {
            var registro = _ProgramacionEstimadaService.ObtenXId(Id).Result;
            var concepto = _ConceptoService.ObtenXId(registro.IdConcepto).Result;
            var PU = _PrecioUnitarioService.ObtenXId(registro.IdPrecioUnitario).Result;
            registro.Codigo = concepto.Codigo;
            registro.Descripcion = concepto.Descripcion;
            registro.Unidad = concepto.Unidad;
            registro.CostoUnitario = concepto.CostoUnitario;
            registro.InicioParaGantt = ((DateTimeOffset)registro.Inicio).ToUnixTimeSeconds();
            registro.TerminoParaGantt = ((DateTimeOffset)registro.Termino).ToUnixTimeSeconds();
            registro.Cantidad = PU.Cantidad;
            registro.TipoPrecioUnitario = PU.TipoPrecioUnitario;
            if (registro.Cantidad != null)
            {
                registro.Importe = registro.CostoUnitario * registro.Cantidad;
            }
            return registro;
        }

        public async Task FinComienzo(ProgramacionEstimadaDTO registro)
        {
            var proyecto = await _ProyectoService.ObtenXId(registro.IdProyecto);
            var programacionPadre = await _ProgramacionEstimadaService.ObtenXId(registro.IdPredecesora);
            registro.Inicio = programacionPadre.Termino.AddDays(1);
            if(registro.Inicio.DayOfWeek == DayOfWeek.Saturday && proyecto.EsSabado == true)
            {
                registro.Inicio.AddDays(1);
            }
            if (registro.Inicio.DayOfWeek == DayOfWeek.Sunday && proyecto.EsDomingo == true)
            {
                registro.Inicio.AddDays(1);
            }
            registro.Termino = registro.Inicio.AddDays(1);
            if (registro.Termino.DayOfWeek == DayOfWeek.Saturday && proyecto.EsSabado == true)
            {
                registro.Termino.AddDays(1);
            }
            if (registro.Termino.DayOfWeek == DayOfWeek.Sunday && proyecto.EsDomingo == true)
            {
                registro.Termino.AddDays(1);
            }
            registro.DiasTranscurridos = (registro.Termino - registro.Inicio).Days + 1;
            await _ProgramacionEstimadaService.Editar(registro);
        }

        public async Task ComienzoFin(ProgramacionEstimadaDTO registro)
        {
            var proyecto = await _ProyectoService.ObtenXId(registro.IdProyecto);
            var programacionPadre = await _ProgramacionEstimadaService.ObtenXId(registro.IdPredecesora);
            registro.Termino = programacionPadre.Inicio.AddDays(-1);
            if (registro.Termino.DayOfWeek == DayOfWeek.Sunday && proyecto.EsDomingo == true)
            {
                registro.Termino.AddDays(-1);
            }
            if (registro.Termino.DayOfWeek == DayOfWeek.Saturday && proyecto.EsSabado == true)
            {
                registro.Termino.AddDays(-1);
            }
            registro.Inicio = registro.Termino.AddDays(1);
            if (registro.Termino.DayOfWeek == DayOfWeek.Saturday && proyecto.EsSabado == true)
            {
                registro.Termino.AddDays(1);
            }
            if (registro.Termino.DayOfWeek == DayOfWeek.Sunday && proyecto.EsDomingo == true)
            {
                registro.Termino.AddDays(1);
            }
            registro.DiasTranscurridos = (registro.Termino - registro.Inicio).Days + 1;
            await _ProgramacionEstimadaService.Editar(registro);
        }

        public async Task ComienzoComienzo(ProgramacionEstimadaDTO registro)
        {
            var proyecto = await _ProyectoService.ObtenXId(registro.IdProyecto);
            var programacionPadre = await _ProgramacionEstimadaService.ObtenXId(registro.IdPredecesora);
            registro.Inicio = programacionPadre.Inicio;
            registro.Termino = registro.Inicio.AddDays(1);
            if (registro.Termino.DayOfWeek == DayOfWeek.Saturday && proyecto.EsSabado == true)
            {
                registro.Termino.AddDays(1);
            }
            if (registro.Termino.DayOfWeek == DayOfWeek.Sunday && proyecto.EsDomingo == true)
            {
                registro.Termino.AddDays(1);
            }
            registro.DiasTranscurridos = (registro.Termino - registro.Inicio).Days + 1;
            await _ProgramacionEstimadaService.Editar(registro);
        }

        public async Task FinFin(ProgramacionEstimadaDTO registro)
        {
            var proyecto = await _ProyectoService.ObtenXId(registro.IdProyecto);
            var programacionPadre = await _ProgramacionEstimadaService.ObtenXId(registro.IdPredecesora);
            registro.Termino = programacionPadre.Termino;
            registro.Inicio = programacionPadre.Termino.AddDays(-1);
            if (registro.Inicio.DayOfWeek == DayOfWeek.Sunday && proyecto.EsDomingo == true)
            {
                registro.Inicio.AddDays(-1);
            }
            if (registro.Inicio.DayOfWeek == DayOfWeek.Saturday && proyecto.EsSabado == true)
            {
                registro.Inicio.AddDays(-1);
            }
            registro.DiasTranscurridos = (registro.Termino - registro.Inicio).Days + 1;
            await _ProgramacionEstimadaService.Editar(registro);
        }

        public async Task AsignarComando(ProgramacionEstimadaDTO registro)
        {
            if(registro.Comando == 0)
            {
                registro.DiasTranscurridos = (registro.Termino - registro.Inicio).Days + 1;
                await _ProgramacionEstimadaService.Editar(registro);
                return;
            }
            if(registro.Comando == 1)
            {
                await FinComienzo(registro);
                registro.Inicio = registro.Inicio.AddDays(registro.DiasComando);
                registro.Termino = registro.Inicio;
                await _ProgramacionEstimadaService.Editar(registro);
                return;
            }
            if(registro.Comando == 2)
            {
                await ComienzoFin(registro);
                registro.Inicio = registro.Inicio.AddDays(registro.DiasComando);
                registro.Termino = registro.Inicio;
                await _ProgramacionEstimadaService.Editar(registro);
                return;
            }
            if (registro.Comando == 3)
            {
                await ComienzoComienzo(registro);
                registro.Inicio = registro.Inicio.AddDays(registro.DiasComando);
                registro.Termino = registro.Inicio;
                await _ProgramacionEstimadaService.Editar(registro);
                return;
            }
            if (registro.Comando == 4)
            {
                await FinFin(registro);
                registro.Inicio = registro.Inicio.AddDays(registro.DiasComando);
                registro.Termino = registro.Inicio;
                await _ProgramacionEstimadaService.Editar(registro);
                return;
            }
        }

        public async Task<List<ProgramacionEstimadaDTO>> Enumerar (int IdProyecto)
        {
            var numerador = 1;
            var registros = await _ProgramacionEstimadaService.ObtenerTodosXProyecto(IdProyecto);
            for (int i = 0; i < registros.Where(z => z.IdPadre == 0).Count(); i++)
            {
                registros.Where(z => z.IdPadre == 0).ToList()[i].Numerador = numerador;
                numerador++;
                if(registros.Where(z => z.IdPadre == 0).ToList().Count > 0)
                {
                    var RegistrosHijosEnumerados = await EnumerarHijos(registros, registros.Where(z => z.IdPadre == 0).ToList()[i], numerador);
                    registros = RegistrosHijosEnumerados.registros;
                    numerador = RegistrosHijosEnumerados.numerador;
                }
            }
            return registros;
        }

        public async Task<registrosParaEnumerarDTO> EnumerarHijos(List<ProgramacionEstimadaDTO> registros, ProgramacionEstimadaDTO padre, int numerador)
        {
            var RegistrosParaRegresar = new registrosParaEnumerarDTO();
            for(int i = 0; i < registros.Where(z => z.IdPadre == padre.Id).Count(); i++)
            {
                registros.Where(z => z.IdPadre == padre.Id).ToList()[i].Numerador = numerador;
                numerador++;
                if (registros.Where(z => z.IdPadre == padre.Id).ToList().Count > 0)
                {
                    var RegistrosEnumerados = await EnumerarHijos(registros, registros.Where(z => z.IdPadre == padre.Id).ToList()[i], numerador);
                    registros = RegistrosEnumerados.registros;
                    numerador = RegistrosEnumerados.numerador;
                }
            }
            RegistrosParaRegresar.registros = registros;
            RegistrosParaRegresar.numerador = numerador;
            return RegistrosParaRegresar;
        }

        public async Task<List<int>> ObtenerSemanas(int IdProgramacionEstimada)
        {
            var programacionEstimada = await _ProgramacionEstimadaService.ObtenXId(IdProgramacionEstimada);
            var precioUnitario = await _PrecioUnitarioService.ObtenXId(programacionEstimada.IdPrecioUnitario);
            var diasTotales = Convert.ToInt32((programacionEstimada.Termino - programacionEstimada.Inicio).TotalDays);
            var fechaAux = programacionEstimada.Inicio;
            var semanas = new List<int>();
            for(int i = 0; i < diasTotales; i++)
            {
                Calendar calendar = CultureInfo.InvariantCulture.Calendar;
                int w = calendar.GetWeekOfYear(fechaAux, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                if(semanas.Where(z => z == w).Count() <= 0)
                {
                    semanas.Add(w);
                }
                fechaAux = fechaAux.AddDays(1);
            }
            return semanas;
        }

        public async Task<List<PrecioUnitarioDetalleDTO>> ObtenerDetallesPorSemana(int IdProgramacionEstimada, int SelectedSemana)
        {
            var programacionEstimada = await _ProgramacionEstimadaService.ObtenXId(IdProgramacionEstimada);
            var precioUnitario = await _PrecioUnitarioService.ObtenXId(programacionEstimada.IdPrecioUnitario);
            var diasTotales = Convert.ToInt32((programacionEstimada.Termino - programacionEstimada.Inicio).TotalDays);
            var fechaAux = programacionEstimada.Inicio;
            var semanas = new List<int>();
            for (int i = 0; i < diasTotales; i++)
            {
                Calendar calendar = CultureInfo.InvariantCulture.Calendar;
                int w = calendar.GetWeekOfYear(fechaAux, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                if (semanas.Where(z => z == w).Count() <= 0)
                {
                    semanas.Add(w);
                }
                fechaAux = fechaAux.AddDays(1);
            }
            var detalles = _PrecioUnitarioProceso.ObtenerDetallesParaProgramacionEstimada(precioUnitario.Id).Result;
            for(int i = 0; i < detalles.Count; i++)
            {
                detalles[i].Importe = (detalles[i].Importe / semanas.Count);
            }
            return detalles;
        }

        public async Task<DateTime> obtenerRegistroParaFechaFinal(int Id)
        {
            var registro = await _ProgramacionEstimadaService.ObtenXId(Id);
            return registro.Termino;
        }
    }
}