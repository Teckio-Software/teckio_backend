using Microsoft.EntityFrameworkCore;


using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;

namespace ERP_TECKIO
{
    public class EstimacionesProceso<TContext> where TContext : DbContext
    {
        private readonly IEstimacionesService<TContext> _EstimacionService;
        private readonly IPeriodoEstimacionesService<TContext> _PeriodoEstimacionesService;
        private readonly IPrecioUnitarioService<TContext> _PrecioUnitarioService;
        private readonly IConceptoService<TContext> _ConceptoService;
        private readonly PrecioUnitarioProceso<TContext> _PrecioUnitarioProceso;

        public EstimacionesProceso(
            IEstimacionesService<TContext> estimacionesService
            , IPeriodoEstimacionesService<TContext> periodoEstimacionesService
            , IPrecioUnitarioService<TContext> precioUnitarioService
            , IConceptoService<TContext> conceptoService
            , PrecioUnitarioProceso<TContext> precioUnitarioProceso
            )
        {
            _EstimacionService = estimacionesService;
            _PeriodoEstimacionesService = periodoEstimacionesService;
            _PrecioUnitarioService = precioUnitarioService;
            _ConceptoService = conceptoService;
            _PrecioUnitarioProceso = precioUnitarioProceso;
        }

        public async Task<List<PeriodoEstimacionesDTO>> ObtenerPeriodosXIdProyecto(int IdProyecto)
        {
            var registros = await _PeriodoEstimacionesService.ObtenTodosXIdProyecto(IdProyecto);
            var registrosOrdenados = registros.OrderBy(z => z.NumeroPeriodo).ToList();
            for (int i = 0; i < registrosOrdenados.Count; i++)
            {
                registrosOrdenados[i].DescripcionPeriodo = "Periodo " + registrosOrdenados[i].NumeroPeriodo + " " + registrosOrdenados[i].FechaInicio.ToString("dd/MM/yyyy") + " - " + registrosOrdenados[i].FechaTermino.ToString("dd/MM/yyyy");
            }
            return registrosOrdenados;
        }

        public async Task<List<PeriodosResumenDTO>> ObtenerPeridosConImporteTotal(int IdProyecto)
        {
            var listPeridos = new List<PeriodosResumenDTO>();
            var peridos = await _PeriodoEstimacionesService.ObtenTodosXIdProyecto(IdProyecto);
            var registrosOrdenados = peridos.OrderBy(z => z.NumeroPeriodo).ToList();
            for (int i = 0; i < registrosOrdenados.Count; i++)
            {
                var estimaciones = (await ObtenerEstimacionesXIdPeriodo(registrosOrdenados[i].Id)).Where(z => z.IdPadre == 0);
                var importe = estimaciones.Sum(z => z.ImporteDeAvance);
                var avance = estimaciones.Sum(z => z.PorcentajeAvance);
                listPeridos.Add(new PeriodosResumenDTO()
                {
                    Id = registrosOrdenados[i].Id,
                    FechaInicio = registrosOrdenados[i].FechaInicio,
                    FechaTermino = registrosOrdenados[i].FechaTermino,
                    DescripcionPeriodo = registrosOrdenados[i].DescripcionPeriodo = "Periodo " + registrosOrdenados[i].NumeroPeriodo + " " + registrosOrdenados[i].FechaInicio.ToString("dd/MM/yyyy") + " - " + registrosOrdenados[i].FechaTermino.ToString("dd/MM/yyyy"),
                    NumeroPeriodo = registrosOrdenados[i].NumeroPeriodo,
                    EsCerrada = registrosOrdenados[i].EsCerrada,
                    Importe = importe,
                    ImporteConFormato = String.Format("{0:#,##0.00}", importe),
                    Avance = avance,
                    AvanceConFormato = String.Format("{0:#,##0.00}", avance)
                }); 
            }
            return listPeridos;

        }
        public async Task<List<EstimacionesDTO>> ObtenerEstimacionesXIdPeriodo(int IdPeriodo)
        {
            var Periodo = await _PeriodoEstimacionesService.ObtenXId(IdPeriodo);
            var Registros = await _EstimacionService.ObtenTodosXIdPeriodo(IdPeriodo); 
            var PreciosUnitarios = await _PrecioUnitarioProceso.ObtenerPrecioUnitarioSinEstructurar(Periodo.IdProyecto);
            for(int i = 0; i < Registros.Count; i++)
            {
                var PrecioUnitario = PreciosUnitarios.Where(z => z.Id == Registros[i].IdPrecioUnitario).FirstOrDefault();
                Registros[i].Codigo = PrecioUnitario.Codigo;
                Registros[i].Descripcion = PrecioUnitario.Descripcion;
                Registros[i].Unidad = PrecioUnitario.Unidad;
                Registros[i].CostoUnitario = PrecioUnitario.CostoUnitario;
                Registros[i].Cantidad = PrecioUnitario.Cantidad;
                Registros[i].CantidadConFormato = String.Format("{0:#,##0.00}", Registros[i].Cantidad);
                Registros[i].CostoUnitarioConFormato = String.Format("{0:#,##0.00}", Registros[i].CostoUnitario);
                Registros[i].PorcentajeAvanceConFormato = String.Format("{0:#,##0.00}", Registros[i].PorcentajeAvance);
                Registros[i].CantidadAvanceConFormato = String.Format("{0:#,##0.00}", Registros[i].CantidadAvance);
                Registros[i].ImporteDeAvanceAcumuladoConFormato = String.Format("{0:#,##0.00}", Registros[i].ImporteDeAvanceAcumulado);
                Registros[i].PorcentajeAvanceAcumuladoConFormato = String.Format("{0:#,##0.00}", Registros[i].PorcentajeAvanceAcumulado);
                Registros[i].CantidadAvanceAcumuladoConFormato = String.Format("{0:#,##0.00}", Registros[i].CantidadAvanceAcumulado);
                Registros[i].PorcentajeAvanceEditando = false;
                Registros[i].CantidadAvanceEditando = false;
                Registros[i].Importe = PrecioUnitario.Importe;
                Registros[i].Expandido = true;
                Registros[i].ImporteConFormato = String.Format("{0:#,##0.00}", Registros[i].Importe);
                Registros[i].PorcentajeTotal = Registros[i].PorcentajeAvance + Registros[i].PorcentajeAvanceAcumulado;
                Registros[i].PorcentajeTotalConFormato = String.Format("{0:#,##0.00}", Registros[i].PorcentajeTotal);
                Registros[i].ImporteTotal = Registros[i].ImporteDeAvance + Registros[i].ImporteDeAvanceAcumulado;
                Registros[i].ImporteTotalConFormato = String.Format("{0:#,##0.00}", Registros[i].ImporteTotal);
                Registros[i].CantidadAvanceTotal = Registros[i].CantidadAvance + Registros[i].CantidadAvanceAcumulado;
                Registros[i].CantidadAvanceTotalConFormato = String.Format("{0:#,##0.00}", Registros[i].CantidadAvanceTotal);
                Registros[i].ImporteDeAvanceConFormato = String.Format("{0:#,##0.00}", Registros[i].ImporteDeAvance);
            }
            var listaEstructurada = await Estructurar(Registros);
            var listaResult = listaEstructurada.OrderBy(z => z.Id).ToList();
            return listaResult;
        }

        public async Task<List<EstimacionesDTO>> ObtenerEstimacionesXIdPeriodoXReporte(int IdPeriodo)
        {
            var Periodo = await _PeriodoEstimacionesService.ObtenXId(IdPeriodo);
            var Registros = await _EstimacionService.ObtenTodosXIdPeriodo(IdPeriodo);
            Registros = Registros.Where(z => z.PorcentajeAvance != 0).ToList();
            var PreciosUnitarios = await _PrecioUnitarioProceso.ObtenerPrecioUnitarioSinEstructurar(Periodo.IdProyecto);
            for (int i = 0; i < Registros.Count; i++)
            {
                var PrecioUnitario = PreciosUnitarios.Where(z => z.Id == Registros[i].IdPrecioUnitario).FirstOrDefault();
                Registros[i].Codigo = PrecioUnitario.Codigo;
                Registros[i].Descripcion = PrecioUnitario.Descripcion;
                Registros[i].Unidad = PrecioUnitario.Unidad;
                Registros[i].CostoUnitario = PrecioUnitario.CostoUnitario;
                Registros[i].Cantidad = PrecioUnitario.Cantidad;
                Registros[i].CantidadConFormato = String.Format("{0:#,##0.00}", Registros[i].Cantidad);
                Registros[i].CostoUnitarioConFormato = String.Format("{0:#,##0.00}", Registros[i].CostoUnitario);
                Registros[i].PorcentajeAvanceConFormato = String.Format("{0:#,##0.00}", Registros[i].PorcentajeAvance);
                Registros[i].CantidadAvanceConFormato = String.Format("{0:#,##0.00}", Registros[i].CantidadAvance);
                Registros[i].ImporteDeAvanceAcumuladoConFormato = String.Format("{0:#,##0.00}", Registros[i].ImporteDeAvanceAcumulado);
                Registros[i].PorcentajeAvanceAcumuladoConFormato = String.Format("{0:#,##0.00}", Registros[i].PorcentajeAvanceAcumulado);
                Registros[i].CantidadAvanceAcumuladoConFormato = String.Format("{0:#,##0.00}", Registros[i].CantidadAvanceAcumulado);
                Registros[i].PorcentajeAvanceEditando = false;
                Registros[i].CantidadAvanceEditando = false;
                Registros[i].Importe = PrecioUnitario.Importe;
                Registros[i].Expandido = true;
                Registros[i].ImporteConFormato = String.Format("{0:#,##0.00}", Registros[i].Importe);
                Registros[i].PorcentajeTotal = Registros[i].PorcentajeAvance + Registros[i].PorcentajeAvanceAcumulado;
                Registros[i].PorcentajeTotalConFormato = String.Format("{0:#,##0.00}", Registros[i].PorcentajeTotal);
                Registros[i].ImporteTotal = Registros[i].ImporteDeAvance + Registros[i].ImporteDeAvanceAcumulado;
                Registros[i].ImporteTotalConFormato = String.Format("{0:#,##0.00}", Registros[i].ImporteTotal);
                Registros[i].CantidadAvanceTotal = Registros[i].CantidadAvance + Registros[i].CantidadAvanceAcumulado;
                Registros[i].CantidadAvanceTotalConFormato = String.Format("{0:#,##0.00}", Registros[i].CantidadAvanceTotal);
                Registros[i].ImporteDeAvanceConFormato = String.Format("{0:#,##0.00}", Registros[i].ImporteDeAvance);
            }
            var listaEstructurada = await Estructurar(Registros);
            var listaResult = listaEstructurada.OrderBy(z => z.Id).ToList();
            return listaResult;
        }

        public async Task<List<EstimacionesDTO>> ObtenerEstimacionesXIdPeriodoSinEstructurar(int IdPeriodo)
        {
            var Periodo = await _PeriodoEstimacionesService.ObtenXId(IdPeriodo);
            var Registros = await _EstimacionService.ObtenTodosXIdPeriodo(IdPeriodo);
            var PreciosUnitarios = await _PrecioUnitarioProceso.ObtenerPrecioUnitarioSinEstructurar(Periodo.IdProyecto);
            for (int i = 0; i < Registros.Count; i++)
            {
                var PrecioUnitario = PreciosUnitarios.Where(z => z.Id == Registros[i].IdPrecioUnitario).FirstOrDefault();
                Registros[i].Codigo = PrecioUnitario.Codigo;
                Registros[i].Descripcion = PrecioUnitario.Descripcion;
                Registros[i].Unidad = PrecioUnitario.Unidad;
                Registros[i].CostoUnitario = PrecioUnitario.CostoUnitario;
                Registros[i].Cantidad = PrecioUnitario.Cantidad;
                Registros[i].Importe = PrecioUnitario.Importe;
            }
            return Registros;
        }

        public async Task<List<EstimacionesDTO>> Estructurar(List<EstimacionesDTO> registros)
        {
            var padres = registros.Where(z => z.IdPadre == 0).ToList();
            for (int i = 0; i < padres.Count; i++)
            {
                padres[i].Hijos = await BuscaHijos(registros, padres[i]);
            }
            return padres;
        }

        private async Task<List<EstimacionesDTO>> BuscaHijos(List<EstimacionesDTO> registros, EstimacionesDTO padre)
        {
            var padres = registros.Where(z => z.IdPadre == padre.Id).ToList();
            for (int i = 0; i < padres.Count; i++)
            {
                padres[i].Hijos = await BuscaHijos(registros, padres[i]);
            }
            return padres;
        }

        public async Task<PeriodoEstimacionesDTO> CrearPeriodo(PeriodoEstimacionesDTO registro)
        {
            var Periodos = await _PeriodoEstimacionesService.ObtenTodosXIdProyecto(registro.IdProyecto);
            if(Periodos.Count <= 0)
            {
                registro.NumeroPeriodo = 1;
            }
            else
            {
                registro.NumeroPeriodo = (Periodos.Count + 1);
                Periodos[Periodos.Count - 1].EsCerrada = true;
                await _PeriodoEstimacionesService.Editar(Periodos[Periodos.Count - 1]);
            }
            var registroCreado = await _PeriodoEstimacionesService.CrearYObtener(registro);
            if(registroCreado.NumeroPeriodo > 1)
            {
                var periodos = _PeriodoEstimacionesService.ObtenTodosXIdProyecto(registro.IdProyecto).Result.ToList();
                var periodoAnterior = periodos.Where(z => z.NumeroPeriodo == (registro.NumeroPeriodo - 1)).FirstOrDefault();
                periodoAnterior.EsCerrada = true;
                var registros = ObtenerEstimacionesXIdPeriodoSinEstructurar(periodoAnterior.Id).Result.ToList();
                var nuevosRegistros = new List<EstimacionesDTO>();
                for(int i = 0; i < registros.Count; i++)
                {
                    var registroNuevo = new EstimacionesDTO();
                    registroNuevo.Id = 0;
                    registroNuevo.PorcentajeAvanceAcumulado = registros[i].PorcentajeAvanceAcumulado + registros[i].PorcentajeAvance;
                    registroNuevo.CantidadAvanceAcumulado = registros[i].CantidadAvanceAcumulado + registros[i].CantidadAvance;
                    registroNuevo.ImporteDeAvanceAcumulado = registros[i].ImporteDeAvanceAcumulado + registros[i].ImporteDeAvance;
                    registroNuevo.PorcentajeAvance = 0;
                    registroNuevo.CantidadAvance = 0;
                    registroNuevo.ImporteDeAvance = 0;
                    registroNuevo.IdPeriodo = registroCreado.Id;
                    registroNuevo.IdProyecto = registros[i].IdProyecto;
                    registroNuevo.IdConcepto = registros[i].IdConcepto;
                    registroNuevo.IdPrecioUnitario = registros[i].IdPrecioUnitario;
                    registroNuevo.TipoPrecioUnitario = registros[i].TipoPrecioUnitario;
                    registroNuevo.IdPadre = registros[i].IdPadre;
                    if (registroNuevo.IdPadre > 0)
                    {
                        var registroPadre = registros.Where(z => z.Id == registroNuevo.IdPadre).FirstOrDefault();
                        var registroPadreNuevo = nuevosRegistros.Where(z => z.IdPrecioUnitario == registroPadre.IdPrecioUnitario).FirstOrDefault();
                        registroNuevo.IdPadre = registroPadreNuevo.Id;
                    }
                    var CrearRegistro = await _EstimacionService.CrearYObtener(registroNuevo);
                    nuevosRegistros.Add(CrearRegistro);
                }
            }
            else
            {
                var NuevasEstimaciones = new List<EstimacionesDTO>();
                var PreciosUnitarios = await _PrecioUnitarioProceso.ObtenerSinEstructura(registroCreado.IdProyecto);
                for(int i = 0; i < PreciosUnitarios.Count; i++)
                {
                    var nuevoRegistro = new EstimacionesDTO();
                    nuevoRegistro.IdPrecioUnitario = PreciosUnitarios[i].Id;
                    nuevoRegistro.IdConcepto = PreciosUnitarios[i].IdConcepto;
                    nuevoRegistro.IdProyecto = PreciosUnitarios[i].IdProyecto;
                    nuevoRegistro.IdPeriodo = registroCreado.Id;
                    nuevoRegistro.TipoPrecioUnitario = PreciosUnitarios[i].TipoPrecioUnitario;
                    if (PreciosUnitarios[i].IdPrecioUnitarioBase > 0)
                    {
                        var EstimacionPadre = NuevasEstimaciones.Where(z => z.IdPrecioUnitario == PreciosUnitarios[i].IdPrecioUnitarioBase).FirstOrDefault();
                        if(EstimacionPadre != null)
                        {
                            nuevoRegistro.IdPadre = EstimacionPadre.Id;
                        }
                    }
                    var CrearRegistro = await _EstimacionService.CrearYObtener(nuevoRegistro);
                    NuevasEstimaciones.Add(CrearRegistro);
                }
            }
            return registro;
        }

        public async Task<List<EstimacionesDTO>> EditarEstimacion(EstimacionesDTO registro)
        {
            registro.ImporteDeAvance = (registro.PorcentajeAvance/100) * registro.Importe;
            await _EstimacionService.Editar(registro);
            await RecalcularEstimacion(registro);
            var registros = await ObtenerEstimacionesXIdPeriodo(registro.IdPeriodo);
            return registros;
        }

        public async Task RecalcularEstimacion(EstimacionesDTO registroEditado)
        {
            if(registroEditado.IdPadre > 0)
            {
                var registros = await ObtenerEstimacionesXIdPeriodoSinEstructurar(registroEditado.IdPeriodo);
                var registroPadre = registros.Where(z => z.Id == registroEditado.IdPadre).FirstOrDefault();
                var registrosHijos = registros.Where(z => z.IdPadre == registroPadre.Id).ToList();
                decimal importe = 0;
                decimal sumaPorcentaje = 0;
                for(int i = 0; i < registrosHijos.Count; i++)
                {
                    importe = importe + registrosHijos[i].ImporteDeAvance;
                    sumaPorcentaje = sumaPorcentaje + registrosHijos[i].PorcentajeAvance;
                }
                registroPadre.ImporteDeAvance = importe;
                registroPadre.PorcentajeAvance = (importe / registroPadre.Importe) * 100;
                var resultado = await _EstimacionService.Editar(registroPadre);
                await RecalcularEstimacion(registroPadre);
            }
        }

        public async Task EliminarPeriodo(int IdPeriodo)
        {
            var Periodo = await _PeriodoEstimacionesService.ObtenXId(IdPeriodo);
            if(Periodo.Id > 0)
            {
                if(Periodo.EsCerrada == false)
                {
                    await _EstimacionService.EliminarMultiple(IdPeriodo);
                    await _PeriodoEstimacionesService.Eliminar(IdPeriodo);
                }
                var PeriodosRestantes = await _PeriodoEstimacionesService.ObtenTodosXIdProyecto(Periodo.IdProyecto);
                if(PeriodosRestantes.Count > 0)
                {
                    var PeriodoAnterior = PeriodosRestantes.Where(z => z.NumeroPeriodo == (Periodo.NumeroPeriodo - 1)).FirstOrDefault();
                    PeriodoAnterior.EsCerrada = false;
                    await _PeriodoEstimacionesService.Editar(PeriodoAnterior);
                }
            }
        }

        public async Task<List<PeriodosXEstimacionDTO>> ObtenerPeridosXEstimacion(int IdEstimacion) {
            var periodosXEstimacion = new List<PeriodosXEstimacionDTO>();
            var estimacion = await _EstimacionService.ObtenXId(IdEstimacion);
            var estimaciones = await _EstimacionService.ObtenXIdPrecioUnitario(estimacion.IdPrecioUnitario);
            var periodos = await _PeriodoEstimacionesService.ObtenTodosXIdProyecto(estimacion.IdProyecto);

            foreach (var periodo in periodos) {
                var EstimacionAsignar = estimaciones.Where(z => z.IdPeriodo == periodo.Id).First();
                if (EstimacionAsignar.Id > 0) {
                    periodosXEstimacion.Add(new PeriodosXEstimacionDTO()
                    {
                        IdEstimacion = EstimacionAsignar.Id,
                        IdPeriodo = periodo.Id,
                        Avance = EstimacionAsignar.CantidadAvance,
                        AvanceConFormato = String.Format("{0:#,##0.00}", EstimacionAsignar.CantidadAvance),
                        Importe = EstimacionAsignar.ImporteDeAvance,
                        ImporteConFormato = String.Format("{0:#,##0.00}", EstimacionAsignar.ImporteDeAvance),
                        DescripcionPeriodo = "Periodo " + periodo.NumeroPeriodo + " " + periodo.FechaInicio.ToString("dd/MM/yyyy") +
                        " - " + periodo.FechaTermino.ToString("dd/MM/yyyy"),
                        IdProyecto = periodo.IdProyecto
                    });
                }
            }
            return periodosXEstimacion;
        }
    }
}