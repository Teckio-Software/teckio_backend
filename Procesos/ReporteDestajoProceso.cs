using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;


using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;

namespace ERP_TECKIO
{
    public class ReporteDestajoProceso<T> where T :  DbContext
    {
        private readonly IContratoService<T> _ContratoService;
        private readonly IDetalleXContratoService<T> _DetalleXContrato;
        private readonly PrecioUnitarioProceso<T> _PrecioUnitarioProceso;
        private readonly EstimacionesProceso<T> _EstmacionesProceso;
        private readonly IPeriodoEstimacionesService<T> _periodoEstimacionesService;
        private readonly ContratosProceso<T> _contratosProceso;
        private readonly IContratistaService<T> _contratistaService;
        public ReporteDestajoProceso(
              IContratoService<T> contratoService
            , IDetalleXContratoService<T> detalleXContrato,
              PrecioUnitarioProceso<T> precioUnitarioProceso,
              EstimacionesProceso<T> EstmacionesProceso,
              IPeriodoEstimacionesService<T> periodoEstimacionesService,
              ContratosProceso<T> contratosProceso,
              IContratistaService<T> contratistaService
            )
        {
            _ContratoService = contratoService;
            _DetalleXContrato = detalleXContrato;
            _PrecioUnitarioProceso = precioUnitarioProceso;
            _EstmacionesProceso = EstmacionesProceso;
            _periodoEstimacionesService = periodoEstimacionesService;
            _contratosProceso = contratosProceso;
            _contratistaService = contratistaService;
        }

        public async Task<List<ReporteDestajoDTO>> reporteDestajo(ReporteDestajoDTO parametrosBusqueda)
        {
            List<ReporteDestajoDTO> reporteDestajo = new List<ReporteDestajoDTO>();
            var PreciosUnitarios = _PrecioUnitarioProceso.ObtenerPrecioUnitarioSinEstructurar(parametrosBusqueda.IdProyecto).Result;
            var Periodos = await _periodoEstimacionesService.ObtenTodosXIdProyecto(parametrosBusqueda.IdProyecto);
            if (parametrosBusqueda.IdContrato == 0) {
                var contratos = await _ContratoService.ObtenerRegistrosXIdProyecto(parametrosBusqueda.IdProyecto);
                var contratosXContratista = contratos.Where(z => z.IdContratista == parametrosBusqueda.IdContratista).ToList();
                if (contratosXContratista.Count <= 0) {
                    return reporteDestajo;
                }
                else
                {
                    foreach (var C in contratosXContratista) {
                        var DetallesContrato = await _DetalleXContrato.ObtenerRegistrosXIdContrato(C.Id);
                        if (DetallesContrato.Count() <= 0)
                        {
                            continue;
                        }
                        foreach (var DC in DetallesContrato)
                        {
                            decimal PEAcumulado = 0;
                            var PrecioUnitario = PreciosUnitarios.Where(z => z.Id == DC.IdPrecioUnitario).ToList();
                            foreach (var P in Periodos)
                            {
                                var Estimaciones = _EstmacionesProceso.ObtenerEstimacionesXIdPeriodoSinEstructurar(P.Id).Result;
                                var Estimacion = Estimaciones.Where(z => z.IdPrecioUnitario == PrecioUnitario[0].Id).ToList();
                                decimal porcentajeE = Estimacion.Count() > 0 ? Estimacion[0].PorcentajeAvance : 0; 
                                decimal importe = DC.ImporteDestajo * (porcentajeE / 100);
                                PEAcumulado += porcentajeE;
                                var existeConcepto = reporteDestajo.FindIndex(z => z.IdPrecioUnitario == DC.IdPrecioUnitario);
                                if (existeConcepto >= 0)
                                { 
                                    if (reporteDestajo[existeConcepto].IdContrato != DC.IdContrato) {
                                        reporteDestajo[existeConcepto].IdContrato = DC.IdContrato;
                                        reporteDestajo[existeConcepto].PorcentajeDestajo += DC.PorcentajeDestajo; 
                                        reporteDestajo[existeConcepto].ImporteDestajo += DC.ImporteDestajo; 
                                        reporteDestajo[existeConcepto].ImporteDestajoConFormato = String.Format("{0:#,##0.00}", reporteDestajo[existeConcepto].ImporteDestajo);
                                        reporteDestajo[existeConcepto].Importe += importe;
                                        reporteDestajo[existeConcepto].ImporteConFormato = String.Format("{0:#,##0.00}", reporteDestajo[existeConcepto].Importe);
                                    }
                                    if (parametrosBusqueda.IdPeriodoEstimacion == P.Id) {
                                        reporteDestajo[existeConcepto].PorcentajePago = (porcentajeE * reporteDestajo[existeConcepto].PorcentajeDestajo / 100);
                                        reporteDestajo[existeConcepto].PorcentajePagoConFormato = String.Format("{0:#,##0.00}", reporteDestajo[existeConcepto].PorcentajePago);
                                        reporteDestajo[existeConcepto].Acumulado = (PEAcumulado * reporteDestajo[existeConcepto].PorcentajeDestajo / 100);
                                        reporteDestajo[existeConcepto].AcumuladoConFormato = String.Format("{0:#,##0.00}", reporteDestajo[existeConcepto].Acumulado);
                                    }
                                }
                                else
                                {
                                    if (parametrosBusqueda.IdPeriodoEstimacion == P.Id)
                                    {
                                        if (DC.ImporteDestajo > 0 && porcentajeE > 0)
                                        {
                                            reporteDestajo.Add(new ReporteDestajoDTO()
                                            {
                                                IdPeriodoEstimacion = parametrosBusqueda.IdPeriodoEstimacion,
                                                IdContratista = parametrosBusqueda.IdContratista,
                                                IdContrato = C.Id,
                                                IdPrecioUnitario = PrecioUnitario[0].Id,
                                                IdProyecto = parametrosBusqueda.IdProyecto,
                                                Descripcion = PrecioUnitario[0].Descripcion,
                                                PorcentajeDestajo = DC.PorcentajeDestajo,
                                                PorcentajeEstimacion = porcentajeE,
                                                porcentajeEstimacionConFormato = String.Format("{0:#,##0.00}", porcentajeE),
                                                PorcentajePago = (porcentajeE * DC.PorcentajeDestajo / 100),
                                                PorcentajePagoConFormato = String.Format("{0:#,##0.00}", (porcentajeE * DC.PorcentajeDestajo / 100)),
                                                Acumulado = porcentajeE * DC.PorcentajeDestajo / 100,
                                                AcumuladoConFormato = String.Format("{0:#,##0.00}", porcentajeE * DC.PorcentajeDestajo / 100),
                                                Importe = importe,
                                                ImporteConFormato = String.Format("{0:#,##0.00}", importe),
                                                ImporteDestajo = DC.ImporteDestajo,
                                                ImporteDestajoConFormato = String.Format("{0:#,##0.00}", DC.ImporteDestajo)
                                            });
                                            if (PEAcumulado != porcentajeE)
                                            {
                                                var existeConceptoReciente = reporteDestajo.FindIndex(z => z.IdPrecioUnitario == DC.IdPrecioUnitario);
                                                if (existeConceptoReciente >= 0)
                                                {
                                                    reporteDestajo[existeConceptoReciente].Acumulado = (PEAcumulado * reporteDestajo[existeConceptoReciente].PorcentajeDestajo / 100);
                                                    reporteDestajo[existeConceptoReciente].AcumuladoConFormato = String.Format("{0:#,##0.00}", reporteDestajo[existeConceptoReciente].Acumulado);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                var DetallesContrato = await _DetalleXContrato.ObtenerRegistrosXIdContrato(parametrosBusqueda.IdContrato);
                if (DetallesContrato.Count() <= 0) {
                    return reporteDestajo;
                }
                foreach (var DC in DetallesContrato) {
                    var PrecioUnitario = PreciosUnitarios.Where(z => z.Id == DC.IdPrecioUnitario).ToList();
                    decimal PEAcumulado = 0;
                    foreach (var P in Periodos) {
                        var Estimaciones = _EstmacionesProceso.ObtenerEstimacionesXIdPeriodoSinEstructurar(P.Id).Result;
                        var Estimacion = Estimaciones.Where(z => z.IdPrecioUnitario == PrecioUnitario[0].Id).ToList();
                        decimal porcentajeE = Estimacion.Count() > 0 ? Estimacion[0].PorcentajeAvance : 0;
                        PEAcumulado += porcentajeE > 0 ? (porcentajeE * DC.PorcentajeDestajo / 100) : 0;
                        decimal importe = porcentajeE == 0 ? DC.ImporteDestajo : DC.ImporteDestajo * (porcentajeE / 100);
                        if (parametrosBusqueda.IdPeriodoEstimacion == P.Id) {
                            if (DC.ImporteDestajo > 0 && porcentajeE > 0)
                            { 
                                reporteDestajo.Add(new ReporteDestajoDTO()
                                {
                                    IdPeriodoEstimacion = parametrosBusqueda.IdPeriodoEstimacion,
                                    IdContratista = parametrosBusqueda.IdContratista,
                                    IdContrato = parametrosBusqueda.IdContrato,
                                    IdPrecioUnitario = PrecioUnitario[0].Id,
                                    IdProyecto = parametrosBusqueda.IdProyecto,
                                    Descripcion = PrecioUnitario[0].Descripcion,
                                    PorcentajeEstimacion = porcentajeE,
                                    porcentajeEstimacionConFormato = String.Format("{0:#,##0.00}", porcentajeE),
                                    Importe = importe,
                                    ImporteConFormato = String.Format("{0:#,##0.00}", importe),
                                    PorcentajePago = (porcentajeE * DC.PorcentajeDestajo / 100),
                                    PorcentajePagoConFormato = String.Format("{0:#,##0.00}", (porcentajeE * DC.PorcentajeDestajo / 100)),
                                    Acumulado = PEAcumulado,
                                    AcumuladoConFormato = String.Format("{0:#,##0.00}", PEAcumulado),
                                    ImporteDestajo = DC.ImporteDestajo,
                                    ImporteDestajoConFormato = String.Format("{0:#,##0.00}", DC.ImporteDestajo)
                                });
                            }
                        }
                        else
                        {
                            if (parametrosBusqueda.IdPeriodoEstimacion == P.Id) {
                                var existeConcepto = reporteDestajo.FindIndex(z => z.IdPrecioUnitario == DC.IdPrecioUnitario);
                                if (existeConcepto >= 0)
                                {
                                    reporteDestajo[existeConcepto].Acumulado = PEAcumulado;
                                    reporteDestajo[existeConcepto].AcumuladoConFormato = String.Format("{0:#,##0.00}", PEAcumulado);
                                }
                            }
                        }
                    }
                }
            }
            return reporteDestajo;
        }

        public async Task<ObjetoDestajoacumuladoDTO> DestajoAcumulado(ParametrosParaBuscarContratos parametros) {
            var objetoAcumulado = new ObjetoDestajoacumuladoDTO();
            objetoAcumulado.destajos = new List<DetalleXContratoParaTablaDTO>();
            objetoAcumulado.periodos = new List<PeridoAcumuladosDTO>();
            
            var peridosEstimaciones = await _periodoEstimacionesService.ObtenTodosXIdProyecto(parametros.IdProyecto);
            if (parametros.FechaInicio != null && parametros.FechaFin != null)
            {
                var fechaI = DateTime.Parse(parametros.FechaInicio).Date;
                var fechaF = DateTime.Parse(parametros.FechaFin).Date;
                peridosEstimaciones = peridosEstimaciones.Where(z => (z.FechaInicio.Year >= fechaI.Year && z.FechaInicio.Month >= fechaI.Month && z.FechaInicio.Day >= fechaI.Day) &&
                (z.FechaTermino.Day <= fechaF.Day && z.FechaTermino.Month <= fechaF.Month && z.FechaTermino.Year <= fechaF.Year)).ToList();
            }
            if (peridosEstimaciones.Count() <= 0)
            {
                return objetoAcumulado;
            }
            var registrosOrdenados = peridosEstimaciones.OrderBy(z => z.NumeroPeriodo).ToList();
            var PreciosUnitarios = _PrecioUnitarioProceso.ObtenerPrecioUnitarioSinEstructurar(parametros.IdProyecto).Result;

            if (parametros.IdContrato == 0) {
                var contratos = await _ContratoService.ObtenerRegistrosXIdProyecto(parametros.IdProyecto);
                var contratosXContratista = contratos.Where(z => z.IdContratista == parametros.IdContratista).ToList();
                if (contratosXContratista.Count() <= 0) {
                    return objetoAcumulado;
                }
                foreach (var con in contratosXContratista)
                {
                    var detalleContrato = _DetalleXContrato.ObtenerRegistrosXIdContrato(con.Id).Result;
                    foreach (var det in detalleContrato)
                    {
                        var precioUnitario = PreciosUnitarios.Where(z => z.Id == det.IdPrecioUnitario).ToList();
                        var existeDestajo = objetoAcumulado.destajos.FindIndex(z => z.IdPrecioUnitario == det.IdPrecioUnitario);
                        if (existeDestajo >= 0) {
                            objetoAcumulado.destajos[existeDestajo].ImporteDestajo += det.ImporteDestajo;
                            objetoAcumulado.destajos[existeDestajo].PorcentajeDestajo += det.PorcentajeDestajo;
                            continue;
                        }
                        objetoAcumulado.destajos.Add(new DetalleXContratoParaTablaDTO()
                        {
                            IdPrecioUnitario = precioUnitario[0].Id,
                            Descripcion = precioUnitario[0].Descripcion,
                            ImporteDestajo = det.ImporteDestajo,
                            PorcentajeDestajo = det.PorcentajeDestajo
                        });
                    }
                }
            }
            else
            {
                var detalleContrato = _DetalleXContrato.ObtenerRegistrosXIdContrato(parametros.IdContrato).Result;
                foreach (var det in detalleContrato) {
                    var precioUnitario = PreciosUnitarios.Where(z => z.Id == det.IdPrecioUnitario).ToList();
                    objetoAcumulado.destajos.Add(new DetalleXContratoParaTablaDTO()
                    {
                        IdPrecioUnitario = precioUnitario[0].Id,
                        Descripcion = precioUnitario[0].Descripcion,
                        ImporteDestajo = det.ImporteDestajo,
                        PorcentajeDestajo = det.PorcentajeDestajo
                    });
                }

            }
            foreach (var per in registrosOrdenados)
            {
                var estimaciones = await _EstmacionesProceso.ObtenerEstimacionesXIdPeriodoSinEstructurar(per.Id);
                var detalles = new List<AcumuladosDTO>();
                for (int i = 0; i < objetoAcumulado.destajos.Count(); i++)
                {
                    var estimacion = estimaciones.Where(z => z.IdPrecioUnitario == objetoAcumulado.destajos[i].IdPrecioUnitario).ToList();

                    var porcentajeEstimacion = estimacion.Count() <= 0 ? 0 : estimacion[0].PorcentajeAvance;
                    detalles.Add(new AcumuladosDTO()
                    {
                        Importe = porcentajeEstimacion == 0 ? 0 : porcentajeEstimacion / 100 * objetoAcumulado.destajos[i].ImporteDestajo,
                        ImporteConFormato = String.Format("{0:#,##0.00}", porcentajeEstimacion == 0 ? 0 : porcentajeEstimacion / 100 * objetoAcumulado.destajos[i].ImporteDestajo),
                        Avance = porcentajeEstimacion == 0 ? 0 : porcentajeEstimacion * objetoAcumulado.destajos[i].PorcentajeDestajo / 100,
                        AvanceConFormato = String.Format("{0:#,##0.00}", porcentajeEstimacion == 0 ? 0 : porcentajeEstimacion * objetoAcumulado.destajos[i].PorcentajeDestajo / 100),
                    });
                    objetoAcumulado.destajos[i].Importe += porcentajeEstimacion == 0 ? 0 : porcentajeEstimacion / 100 * objetoAcumulado.destajos[i].ImporteDestajo;
                    objetoAcumulado.destajos[i].ImporteConFormato = String.Format("{0:#,##0.00}", objetoAcumulado.destajos[i].Importe);
                }
                var peridoTotal = detalles.Sum(z => z.Importe);
                if (peridoTotal <= 0) {
                    continue;
                }
                objetoAcumulado.periodos.Add(new PeridoAcumuladosDTO()
                {
                    Id = per.Id,
                    DescripcionPeriodo = "Periodo " + per.NumeroPeriodo + " " + per.FechaInicio.ToString("dd/MM/yyyy") + " - " + per.FechaTermino.ToString("dd/MM/yyyy"),
                    detalles = detalles
                });
            }
            return objetoAcumulado;
;       }

        public async Task<ObjetoDestajoTotalDTO> DestajoTotal(ParametrosParaBuscarContratos parametros) {
            var objetoDestajoTotal = new ObjetoDestajoTotalDTO();

            var contratos = await _ContratoService.ObtenerRegistrosXIdProyecto(parametros.IdProyecto);
            var peridosEstimaciones = await _periodoEstimacionesService.ObtenTodosXIdProyecto(parametros.IdProyecto);

            if (parametros.FechaInicio != null && parametros.FechaFin != null) {
                var fechaI = DateTime.Parse(parametros.FechaInicio).Date;
                var fechaF = DateTime.Parse(parametros.FechaFin).Date;
                peridosEstimaciones = peridosEstimaciones.Where(z => (z.FechaInicio.Year >= fechaI.Year && z.FechaInicio.Month >= fechaI.Month && z.FechaInicio.Day >= fechaI.Day) && 
                (z.FechaTermino.Day <= fechaF.Day && z.FechaTermino.Month <= fechaF.Month && z.FechaTermino.Year <= fechaF.Year)).ToList();
            }
            if (peridosEstimaciones.Count() <= 0) {
                return objetoDestajoTotal;
            }

            if (contratos.Count() <= 0) {
                return objetoDestajoTotal;
            }
            else
            {
                foreach (var peridos in peridosEstimaciones) {
                    objetoDestajoTotal.periodos.Add(new PeriodosTotalesDTO()
                    {
                        Id = peridos.Id,
                        DescripcionPeriodo = "Periodo " + peridos.NumeroPeriodo + " " + peridos.FechaInicio.ToString("dd/MM/yyyy") + " - " + peridos.FechaTermino.ToString("dd/MM/yyyy"),
                    });
                }
            }
            var contratistas = await _contratistaService.ObtenTodos();
            var contratistaLista = new List<ContratistaDTO>();

            foreach (var contrato in contratos) {
                var existe = objetoDestajoTotal.contratistas.Count(z => z.Id == contrato.IdContratista);
                if (existe <= 0) {
                    var contartista = contratistas.Where(z => z.Id == contrato.IdContratista).FirstOrDefault();
                    contratistaLista.Add(contartista);
                    objetoDestajoTotal.contratistas.Add(new ContaratistaImporteDTO()
                    {
                        Id = contartista.Id,
                        RazonSocial = contartista.RazonSocial,
                    });
                }
            }
            foreach (var contratista in contratistaLista) {
                var indice = objetoDestajoTotal.contratistas.FindIndex(z => z.Id == contratista.Id);

                var contratosXContratista = contratos.Where(z => z.IdContratista == contratista.Id);
                int existePeridos = 0;
                foreach (var c in contratosXContratista) {
                    parametros.IdContrato = c.Id;
                    var objetoAcumulado = await DestajoAcumulado(parametros);
                    if(objetoAcumulado.periodos.Count() > 0)
                    {
                        existePeridos++;
                        foreach (var per in objetoDestajoTotal.periodos)
                        {
                            var peridosexistente = per.totales.FindIndex(z => z.IdContratista == contratista.Id);
                            if (peridosexistente < 0) {
                                per.totales.Add(new TotalesDTO()
                                {
                                    IdContratista = contratista.Id,
                                    Importe = 0,
                                    ImporteConFormato = String.Format("{0:#,##0.00}", 0),
                                    Avance = 0,
                                    AvanceConFormato = String.Format("{0:#,##0.00}", 0)
                                });
                            }
                            
                            foreach (var perAcu in objetoAcumulado.periodos)
                            {
                                if (perAcu.Id == per.Id)
                                {
                                    var existeTotal = per.totales.FindIndex(z => z.IdContratista == contratista.Id);
                                    if (existeTotal >= 0)
                                    {
                                        per.totales[existeTotal].Importe += perAcu.detalles.Sum(z => z.Importe);
                                        per.totales[existeTotal].ImporteConFormato = String.Format("{0:#,##0.00}", per.totales[existeTotal].Importe);
                                        per.totales[existeTotal].Avance += perAcu.detalles.Sum(z => z.Avance);
                                        per.totales[existeTotal].AvanceConFormato = String.Format("{0:#,##0.00}", per.totales[existeTotal].Avance);
                                        objetoDestajoTotal.contratistas[indice].Importe += perAcu.detalles.Sum(z => z.Importe);
                                        objetoDestajoTotal.contratistas[indice].ImporteConFormato = String.Format("{0:#,##0.00}", objetoDestajoTotal.contratistas[indice].Importe);
                                    }
                                    else
                                    {
                                        objetoDestajoTotal.contratistas[indice].Importe += perAcu.detalles.Sum(z => z.Importe);
                                        objetoDestajoTotal.contratistas[indice].ImporteConFormato = String.Format("{0:#,##0.00}", objetoDestajoTotal.contratistas[indice].Importe);
                                        per.totales.Add(new TotalesDTO()
                                        {
                                            IdContratista = contratista.Id,
                                            Importe = perAcu.detalles.Sum(z => z.Importe),
                                            ImporteConFormato = String.Format("{0:#,##0.00}", perAcu.detalles.Sum(z => z.Importe)),
                                            Avance = perAcu.detalles.Sum(z => z.Avance),
                                            AvanceConFormato = String.Format("{0:#,##0.00}", perAcu.detalles.Sum(z => z.Avance))
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
                if (existePeridos <= 0) {
                    objetoDestajoTotal.contratistas.RemoveAt(indice);
                }
            }
            for (int i = 0; i < peridosEstimaciones.Count(); i++)
            {
                var totalPeridos = objetoDestajoTotal.periodos[i].totales.Sum(z => z.Importe);
                if (totalPeridos <= 0) {
                    objetoDestajoTotal.periodos.RemoveAt(i);
                }
            }
            return objetoDestajoTotal;
        }

    }
}
