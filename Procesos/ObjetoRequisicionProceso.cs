using Microsoft.EntityFrameworkCore;


using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;

namespace ERP_TECKIO
{
    public class ObjetoRequisicionProceso<T> where T : DbContext
    {
        private readonly IRequisicionService<T> _requisicionService;
        private readonly IInsumoXRequisicionService<T> _insumoXRequisicionService;
        private readonly IInsumoXCotizacionService<T> _insumoXCotizacionService;
        private readonly ICotizacionService<T> _cotizacionService;
        private readonly IInsumoService<T> _inuimoService;
        private readonly IContratistaService<T> _contratistaService;
        private readonly IImpuestoInsumoCotizadoService<T> _impuestoInsumoCotizadoService;
        public ObjetoRequisicionProceso(
            IRequisicionService<T> requisicionService,
            IInsumoXRequisicionService<T> insumoXRequisicionService,
            ICotizacionService<T> cotizacionService,
            IInsumoService<T> insumoService,
            IContratistaService<T> contratistaService,
            IInsumoXCotizacionService<T> insumoXCotizacionService,
            IImpuestoInsumoCotizadoService<T> impuestoInsumoCotizadoService
            ) {
            _requisicionService = requisicionService;
            _insumoXRequisicionService = insumoXRequisicionService;
            _cotizacionService = cotizacionService;
            _inuimoService = insumoService;
            _contratistaService = contratistaService;
            _insumoXCotizacionService = insumoXCotizacionService;
            _impuestoInsumoCotizadoService = impuestoInsumoCotizadoService;
        }

        public async Task<ObjetoRequisicionDTO> CrearObjetoRequisicion(int IdRequisicion)
        {
            ObjetoRequisicionDTO objeto = new ObjetoRequisicionDTO();
            objeto.InsumosXRequisicion = new List<InsumosXRequisicionObjetoRequisicionDTO>();
            objeto.Cotizacion = new List<CotizacionObjetoRequisicionDTO>();
            

            var requisicion = await _requisicionService.ObtenXId(IdRequisicion);

            objeto.IdRequisicion = IdRequisicion;

            var insumosXRequisicion = await _insumoXRequisicionService.ObtenXIdRequisicion(IdRequisicion);
            if (insumosXRequisicion.Count() <= 0) {
                return objeto;
            }

            var cotizaciones = await _cotizacionService.ObtenXIdRequision(IdRequisicion);
            if (cotizaciones.Count() <= 0)
            {
                return objeto;
            }

            var insumos = await _inuimoService.ObtenXIdProyecto(requisicion.IdProyecto);

            foreach (var IXR in insumosXRequisicion) {
                var insumo = insumos.Where(z => z.id == IXR.IdInsumo).ToList();
                objeto.InsumosXRequisicion.Add(new InsumosXRequisicionObjetoRequisicionDTO
                {
                    IdInsumoXRequisicion = IXR.Id,
                    IdInsumo = IXR.IdInsumo,
                    Cantidad = IXR.Cantidad,
                    Descripcion = insumo[0].Descripcion,
                    Unidad = insumo[0].Unidad,
                    PrecioUnitario = insumo[0].CostoUnitario,
                    Importe = insumo[0].CostoUnitario * IXR.Cantidad
                });
            }
            var contratistas = await _contratistaService.ObtenTodos();
            foreach(var c in cotizaciones) {
                var insumosXCotizacion = await _insumoXCotizacionService.ObtenXIdCotizacion(c.Id);
                if (insumosXCotizacion.Count() <= 0) {
                    continue;
                }
                decimal importeTotal = 0;
                List<InsumosXCotizacionObjetoRequisicionDTO> insumosXC = new List<InsumosXCotizacionObjetoRequisicionDTO>();
                foreach (var IXR in objeto.InsumosXRequisicion)
                {
                    var insumo = insumos.Where(z => z.id == IXR.IdInsumo).ToList();
                    var existeIC = insumosXCotizacion.Where(z => z.IdInsumoRequisicion == IXR.IdInsumoXRequisicion).ToList();
                    
                    if (existeIC.Count() > 0) {
                        importeTotal += existeIC[0].ImporteTotal;
                        insumosXC.Add(new InsumosXCotizacionObjetoRequisicionDTO()
                        {
                            IdInsumoXCotizacion = existeIC[0].Id,
                            Cantidad = existeIC[0].Cantidad,
                            PrecioUnitario = existeIC[0].PrecioUnitario,
                            Importe = existeIC[0].ImporteTotal,
                            IdInsumo = insumo[0].id,
                            Descripcion = insumo[0].Descripcion,
                            Unidad = insumo[0].Unidad,
                            Estatus = existeIC[0].EstatusInsumoCotizacion,
                            ImpuestoInsumoCotizado = await _impuestoInsumoCotizadoService.ObtenerXIdInsumoCotizado(existeIC[0].Id)
                        });
                    }
                    else
                    {
                        insumosXC.Add(new InsumosXCotizacionObjetoRequisicionDTO()
                        {
                            IdInsumoXCotizacion = 0,
                            Cantidad = 0,
                            PrecioUnitario = 0,
                            Importe = 0,
                            IdInsumo = insumo[0].id,
                            Descripcion = insumo[0].Descripcion,
                            Unidad = insumo[0].Unidad,
                            Estatus = 0,
                            ImpuestoInsumoCotizado = new List<ImpuestoInsumoCotizadoDTO>()
                        });
                    }
                    
                }
                var contratisa = contratistas.Where(z => z.Id == c.IdContratista).ToList();
                if (contratisa.Count() <= 0) { 
                    continue;
                }
                objeto.Cotizacion.Add(new CotizacionObjetoRequisicionDTO()
                {
                    IdCotizacion = c.Id,
                    NoCotizacion = c.NoCotizacion,
                    IdContratista = c.IdContratista,
                    RazonSocial = contratisa[0].RazonSocial,
                    ImporteTotal = importeTotal,
                    InsumoXCotizacion = insumosXC
                });
            }

            return objeto;
        }
    }
}
