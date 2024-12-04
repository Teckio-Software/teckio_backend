using Microsoft.EntityFrameworkCore;


using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;

namespace ERP_TECKIO
{
    public class ActualizaEstatusSubProceso<T> where T : DbContext
    {
        private readonly IRequisicionService<T> _requisicionService;
        private readonly ICotizacionService<T> _cotizacionService;
        private readonly IOrdenCompraService<T> _ordenCompraService;
        private readonly IInsumoXCotizacionService<T> _insumoXCotizacionService;
        private readonly IInsumoXOrdenCompraService<T> _insumoXOrdenCompraService;
        private readonly IInsumoXRequisicionService<T> _insumoXRequisicionService;
        public ActualizaEstatusSubProceso(
            IOrdenCompraService<T> ordenCompraService,
            ICotizacionService<T> cotizacionService,
            IInsumoXCotizacionService<T> insumoXCotizacionService,
            IInsumoXOrdenCompraService<T> insumoXOrdenCompraService,
            IRequisicionService<T> requisicionService,
            IInsumoXRequisicionService<T> insumoXRequisicionService
            ) {
            _ordenCompraService = ordenCompraService;
            _cotizacionService = cotizacionService;
            _insumoXOrdenCompraService = insumoXOrdenCompraService;
            _insumoXCotizacionService = insumoXCotizacionService;
            _requisicionService = requisicionService;
            _insumoXRequisicionService = insumoXRequisicionService;
        }

        public async Task ActualizaEstatusRequisicionInsumos(int idRequisicion) {
            await ActualizaRequisicionInsumosComprados(idRequisicion);
            await ActualizaRequisicionInsumosSurtidos(idRequisicion);
            await ActualizaInsumosRequisicionInsumosSurtidos(idRequisicion);
            await ActualizaInsumosRequisicionInsumosComprados(idRequisicion);
        }
        
        public async Task ActualizaRequisicionInsumosSurtidos(int idRequisicion)
        {
            var ordenesCompra = await _ordenCompraService.ObtenXIdRequisicion(idRequisicion);
            int totalinsumos = 0;
            int insumosPendientes = 0;
            int insumosSurtidosTotal = 0;
            int insumosSurtidosParcial = 0;
            foreach (var oc in ordenesCompra) {
                var insumosXordencompra = await _insumoXOrdenCompraService.ObtenXIdOrdenCompra(oc.Id);
                totalinsumos += insumosXordencompra.Count();
                insumosPendientes += insumosXordencompra.Where(z => z.EstatusInsumoOrdenCompra == 1).Count();
                insumosSurtidosParcial += insumosXordencompra.Where(z => z.EstatusInsumoOrdenCompra == 2).Count();
                insumosSurtidosTotal += insumosXordencompra.Where(z => z.EstatusInsumoOrdenCompra == 3).Count();
            }

            if(insumosSurtidosTotal == totalinsumos && totalinsumos > 0)
            {
                var actualizaRequisicionInusmosSurtidos = await _requisicionService.ActualizarRequisicionInsumosSurtidos(idRequisicion, 3);
            }
            if (insumosSurtidosParcial > 0 && totalinsumos > 0)
            {
                var actualizaRequisicionInusmosSurtidos = await _requisicionService.ActualizarRequisicionInsumosSurtidos(idRequisicion, 2);
            }
            if (insumosPendientes > 0 && insumosSurtidosParcial == 0 && totalinsumos > 0)
            {
                var actualizaRequisicionInusmosSurtidos = await _requisicionService.ActualizarRequisicionInsumosSurtidos(idRequisicion, 1);
            }
        }

        public async Task ActualizaRequisicionInsumosComprados(int idRequisicion)
        {
            var cotizaciones = await _cotizacionService.ObtenXIdRequision(idRequisicion);
            var totalinsumos = 0;
            var insumosCapturados = 0;
            var insumosAutorizados = 0;
            var insumosComprados = 0;
            var insumosCancelados = 0;

            if (cotizaciones.Count() > 0) {
                foreach (var c in cotizaciones)
                {
                    var insumosXcotizacion = await _insumoXCotizacionService.ObtenXIdCotizacion(c.Id);
                    totalinsumos += insumosXcotizacion.Count();
                    insumosCapturados += insumosXcotizacion.Where(z => z.EstatusInsumoCotizacion == 1).Count();
                    insumosAutorizados += insumosXcotizacion.Where(z => z.EstatusInsumoCotizacion == 2).Count();
                    insumosComprados += insumosXcotizacion.Where(z => z.EstatusInsumoCotizacion == 3).Count();
                    insumosCancelados += insumosXcotizacion.Where(z => z.EstatusInsumoCotizacion == 4).Count();
                }
            }

            if (insumosCancelados == totalinsumos && totalinsumos > 0)
            {
                var actualizaEstatusCotizacion = await _requisicionService.ActualizarRequisicionInsumosComprados(idRequisicion, 4);
            }
            if ((totalinsumos-insumosCancelados) == insumosComprados && totalinsumos > 0)
            {
                var actualizaEstatusCotizacion = await _requisicionService.ActualizarRequisicionInsumosComprados(idRequisicion, 3);
            }
            if (insumosAutorizados > 0 && totalinsumos > 0)
            {
                var actualizaEstatusCotizacion = await _requisicionService.ActualizarRequisicionInsumosComprados(idRequisicion, 2);
            }
            if (insumosCapturados > 0 && insumosAutorizados == 0 && totalinsumos > 0)
            {
                var actualizaEstatusCotizacion = await _requisicionService.ActualizarRequisicionInsumosComprados(idRequisicion, 1);
            }
        }

        public async Task ActualizaInsumosRequisicionInsumosSurtidos(int idRequisicion)
        {
            var insumosXRequisicion = await _insumoXRequisicionService.ObtenXIdRequisicion(idRequisicion);
            var ordenesCompra = await _ordenCompraService.ObtenXIdRequisicion(idRequisicion);
            var insumosXordenCompra = await _insumoXOrdenCompraService.ObtenTodos();
            List<InsumoXOrdenCompraDTO> listInsumos = new List<InsumoXOrdenCompraDTO>();
            foreach(var oc in ordenesCompra)
            {
                var insumosXordencompraXIOC = insumosXordenCompra.Where(z => z.IdOrdenCompra == oc.Id);
                foreach(var ioc in insumosXordencompraXIOC)
                {
                    listInsumos.Add(ioc);
                }
            }
            var insumosXOrdenCompraAgrupados = listInsumos.GroupBy(z => z.IdInsumo);
            foreach (var ixoca in insumosXOrdenCompraAgrupados) {
                decimal Cantidad = ixoca.Sum(z => z.Cantidad);
                foreach(var ixr in insumosXRequisicion)
                {
                    if(ixoca.Key == ixr.IdInsumo)
                    {
                        if (Cantidad >= ixr.Cantidad)
                        {
                            var actualizaInsumoRequisicionInusmosSurtidos = await _insumoXRequisicionService.ActualizarEstatusInsumosSurtidos(ixr.Id, 3);
                            Cantidad -= ixr.Cantidad;
                        }
                        if (Cantidad < ixr.Cantidad && Cantidad > 0)
                        {
                            var actualizaInsumoRequisicionInusmosSurtidos = await _insumoXRequisicionService.ActualizarEstatusInsumosSurtidos(ixr.Id, 2);
                            Cantidad -= ixr.Cantidad;
                        }
                    }
                }
            }
        }

        public async Task ActualizaInsumosRequisicionInsumosComprados(int idRequisicion)
        {
            var insumosXRequisicion = await _insumoXRequisicionService.ObtenXIdRequisicion(idRequisicion);
            var cotizaciones = await _cotizacionService.ObtenXIdRequision(idRequisicion);
            var insumosXcotizacion = await _insumoXCotizacionService.ObtenTodos();
            List<InsumoXCotizacionDTO> listInsumos = new List<InsumoXCotizacionDTO>();
            foreach (var c in cotizaciones)
            {
                var insumosXcotizacionXIC = insumosXcotizacion.Where(z => z.IdCotizacion == c.Id);
                foreach (var ic in insumosXcotizacionXIC)
                {
                    listInsumos.Add(ic);
                }
            }
            var insumosXCotizacionAgrupados = listInsumos.GroupBy(z => z.IdInsumo);
            foreach (var ixca in insumosXCotizacionAgrupados)
            {
                decimal Cantidad = ixca.Sum(z => z.Cantidad);
                int Estatus = ixca.Sum(z => z.EstatusInsumoCotizacion);
                foreach (var ixr in insumosXRequisicion)
                {
                    if (ixca.Key == ixr.IdInsumo)
                    {
                        if (Cantidad >= ixr.Cantidad && Estatus == ixca.Count() * 4)
                        {
                            var actualizaInsumoRequisicionInusmosComprados = await _insumoXRequisicionService.ActualizarEstatusInsumosComprados(ixr.Id, 4);
                        }
                        if (Cantidad >= ixr.Cantidad && Estatus == ixca.Count()*3)
                        {
                            var actualizaInsumoRequisicionInusmosComprados = await _insumoXRequisicionService.ActualizarEstatusInsumosComprados(ixr.Id, 3);
                        }
                        if (Cantidad >= ixr.Cantidad && Estatus == ixca.Count()*2)
                        {
                            var actualizaInsumoRequisicionInusmosComprados = await _insumoXRequisicionService.ActualizarEstatusInsumosComprados(ixr.Id, 2);
                        }
                        if (Cantidad >= ixr.Cantidad && Estatus == ixca.Count())
                        {
                            var actualizaInsumoRequisicionInusmosComprados = await _insumoXRequisicionService.ActualizarEstatusInsumosComprados(ixr.Id, 1);
                        }
                    }
                }
            }
        }
    }
}
