using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;


using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;

namespace ERP_TECKIO
{
    public class FactorSalarioRealProceso<TContext> where TContext : DbContext
    {
        private readonly IFactorSalarioRealService<TContext> _FSRService;
        private readonly IFactorSalarioRealDetalleService<TContext> _FSRDetalleService;
        private readonly IFactorSalarioIntegradoService<TContext> _FSIService;
        private readonly IRelacionFSRInsumoService<TContext> _RelacionFSRInsumoService;
        private readonly IDiasConsideradosService<TContext> _DiasConsideradosService;
        private readonly IInsumoService<TContext> _InsumoService;
        private readonly IPrecioUnitarioDetalleService<TContext> _PUDetalle;
        private readonly IPrecioUnitarioService<TContext> _PrecioUnitarioService;
        private readonly IConceptoService<TContext> _ConceptoService;
        private readonly PrecioUnitarioProceso<TContext> _PrecioUnitarioProceso;

        public FactorSalarioRealProceso(
            IFactorSalarioRealService<TContext> fsrService
            , IFactorSalarioRealDetalleService<TContext> fsrDetalleService
            , IFactorSalarioIntegradoService<TContext> fsiService
            , IRelacionFSRInsumoService<TContext> relacionFSRInsumoService
            , IDiasConsideradosService<TContext> diasConsideradosService
            , IInsumoService<TContext> insumoService
            , IPrecioUnitarioDetalleService<TContext> puDetalle
            , IPrecioUnitarioService<TContext> precioUnitarioService
            , PrecioUnitarioProceso<TContext>  precioUnitarioProceso
            , IConceptoService<TContext> conceptoService
            )
        {
            _FSRService = fsrService;
            _FSRDetalleService = fsrDetalleService;
            _FSIService = fsiService;
            _RelacionFSRInsumoService = relacionFSRInsumoService;
            _DiasConsideradosService = diasConsideradosService;
            _InsumoService = insumoService;
            _PrecioUnitarioProceso = precioUnitarioProceso;
            _PUDetalle = puDetalle;
            _PrecioUnitarioService = precioUnitarioService;
            _ConceptoService = conceptoService;
        }

        public async Task<FactorSalarioRealDTO> ObtenerFSR(int IdProyecto)
        {
            try
            {
                FactorSalarioRealDTO FSR = new FactorSalarioRealDTO();
                var ExisteFSR = await _FSRService.ObtenerTodosXProyecto(IdProyecto);
                if (ExisteFSR.Count > 0)
                {
                    FSR = ExisteFSR.FirstOrDefault();
                }
                return FSR;
            }
            catch
            {
                return new FactorSalarioRealDTO();
            }
        }

        public async Task<List<FactorSalarioRealDetalleDTO>> ObtenerDetalles(int IdFSR)
        {
            try
            {
                var registros = await _FSRDetalleService.ObtenerTodosXFSR(IdFSR);
                return registros;
            }
            catch
            {
                return new List<FactorSalarioRealDetalleDTO>();
            }
        }

        public async Task<FactorSalarioIntegradoDTO> ObtenerFSI(int IdProyecto)
        {
            try
            {
                FactorSalarioIntegradoDTO FSI = new FactorSalarioIntegradoDTO();
                var ExisteFSI = await _FSIService.ObtenerTodosXProyecto(IdProyecto);
                if (ExisteFSI.Count > 0)
                {
                    FSI = ExisteFSI.FirstOrDefault();
                }
                return FSI;
            }
            catch
            {
                return new FactorSalarioIntegradoDTO();
            }
        }

        public async Task<List<DiasConsideradosDTO>> ObtenerDiasNoLaborables(int IdFSI)
        {
            try
            {
                var diasConsiderados = await _DiasConsideradosService.ObtenerTodosXFSI(IdFSI);
                var diasNoLaborados = diasConsiderados.Where(z => z.EsLaborableOPagado == false).ToList();
                return diasNoLaborados;
            }
            catch
            {
                return new List<DiasConsideradosDTO>();
            }
        }

        public async Task<List<DiasConsideradosDTO>> ObtenerDiasPagados(int IdFSI)
        {
            try
            {
                var diasConsiderados = await _DiasConsideradosService.ObtenerTodosXFSI(IdFSI);
                var diasPagados = diasConsiderados.Where(z => z.EsLaborableOPagado == true).ToList();
                return diasPagados;
            }
            catch
            {
                return new List<DiasConsideradosDTO>();
            }
        }

        public async Task RecalcularFSI(int IdFSI)
        {
            decimal diasPagados = 0;
            decimal diasNoLaborados = 0;
            var registrosDiasNoLaborables = await ObtenerDiasNoLaborables(IdFSI);
            var registrosDiasPagados = await ObtenerDiasPagados(IdFSI);
            for (int i = 0; i < registrosDiasNoLaborables.Count; i++)
            {
                diasNoLaborados = diasNoLaborados + registrosDiasNoLaborables[i].Valor;
            }
            for (int i = 0; i < registrosDiasPagados.Count; i++)
            {
                diasPagados = diasPagados + registrosDiasPagados[i].Valor;
            }
            decimal diasCalendario = Convert.ToDecimal(365.25);
            decimal TI = diasCalendario - diasNoLaborados;
            decimal FSI = (diasPagados / TI);
            var registroFSI = await _FSIService.ObtenXId(IdFSI);
            registroFSI.Fsi = FSI;
            await _FSIService.Editar(registroFSI);
            return;
        }

        public async Task RecalcularFSR(int IdFSR)
        {
            FactorSalarioIntegradoDTO FSI = new FactorSalarioIntegradoDTO();
            var FSR = await _FSRService.ObtenXId(IdFSR);
            var FSRAnterior = FSR.PorcentajeFsr;
            decimal PS = 0;
            var detallesPS = await _FSRDetalleService.ObtenerTodosXFSR(IdFSR);
            for (int i = 0; i < detallesPS.Count; i++)
            {
                PS = PS + (detallesPS[i].PorcentajeFsrdetalle / 100);
            }
            var ExisteFSI = await _FSIService.ObtenerTodosXProyecto(FSR.IdProyecto);
            if(ExisteFSI.Count > 0)
            {
                FSI = ExisteFSI.FirstOrDefault();
            }
            FSR.PorcentajeFsr = (PS + FSI.Fsi);
            await _FSRService.Editar(FSR);
            var insumos = await _InsumoService.ObtenXIdProyecto(FSR.IdProyecto);
            var insumoFiltrados = insumos.Where(z => z.idTipoInsumo == 10000).ToList();
            var detalles = await _PUDetalle.ObtenerTodos();
            for(int i = 0; i < detalles.Count; i++)
            {
                var insumo = insumos.Where(z => z.id == detalles[i].IdInsumo).FirstOrDefault();
                if(insumo != null)
                {
                    detalles[i].Codigo = insumo.Codigo;
                    detalles[i].Descripcion = insumo.Descripcion;
                    detalles[i].Unidad = insumo.Unidad;
                    detalles[i].CostoUnitario = insumo.CostoUnitario;
                    detalles[i].IdTipoInsumo = insumo.idTipoInsumo;
                    detalles[i].IdFamiliaInsumo = insumo.idFamiliaInsumo;
                }
            }
            for(int y = 0; y < insumoFiltrados.Count(); y++)
            {
                insumoFiltrados[y].CostoUnitario = (insumoFiltrados[y].CostoUnitario / FSRAnterior);
                insumoFiltrados[y].CostoUnitario = (insumoFiltrados[y].CostoUnitario * FSR.PorcentajeFsr);
                await _InsumoService.Editar(insumoFiltrados[y]);
                var insumosRefrescados = await _InsumoService.ObtenXIdProyecto(FSR.IdProyecto);
                var precioUnitariosDetalles = await _PUDetalle.ObtenerTodos();
                var precioUnitariosFiltradosXInsumo = precioUnitariosDetalles.Where(z => z.IdInsumo == insumoFiltrados[y].id && z.EsCompuesto == false).ToList();
                var detallesF = detalles.Where(z => z.IdTipoInsumo != 0).ToList();
                for(int j = 0; j < precioUnitariosFiltradosXInsumo.Count; j++)
                {
                    var resultados = await _PrecioUnitarioProceso.RecalcularDetalles(precioUnitariosFiltradosXInsumo[j].IdPrecioUnitario, detallesF, insumosRefrescados);
                    var PrecioUnitario = await _PrecioUnitarioService.ObtenXId(precioUnitariosFiltradosXInsumo[j].IdPrecioUnitario);
                    var concepto = await _ConceptoService.ObtenXId(PrecioUnitario.IdConcepto);
                    concepto.CostoUnitario = resultados.Total;
                    await _ConceptoService.Editar(concepto);
                    await _PrecioUnitarioProceso.RecalcularPrecioUnitario(PrecioUnitario);
                }
            }
        }

        public async Task CrearDetalleFSR(FactorSalarioRealDetalleDTO nuevoDetalle)
        {
            var DetalleCreado = await _FSRDetalleService.CrearYObtener(nuevoDetalle);
            await RecalcularFSR(DetalleCreado.IdFactorSalarioReal);
        }

        public async Task EditarDetalleFSR(FactorSalarioRealDetalleDTO detalleEditado)
        {
            await _FSRDetalleService.Editar(detalleEditado);
            await RecalcularFSR(detalleEditado.IdFactorSalarioReal);
        }

        public async Task AgregarDiasFSI(DiasConsideradosDTO nuevoDia)
        {
            var nuevoDiaCreado = await _DiasConsideradosService.CrearYObtener(nuevoDia);
            await RecalcularFSI(nuevoDiaCreado.IdFactorSalarioIntegrado);
            var FSI = await _FSIService.ObtenXId(nuevoDiaCreado.IdFactorSalarioIntegrado);
            var ExisteFSR = await _FSRService.ObtenerTodosXProyecto(FSI.IdProyecto);
            if(ExisteFSR.Count > 0)
            {
                var FSR = ExisteFSR.FirstOrDefault();
                await RecalcularFSR(FSR.Id);
            }
        }

        public async Task EditarDiasFSI(DiasConsideradosDTO diaEditado)
        {
            await _DiasConsideradosService.Editar(diaEditado);
            await RecalcularFSI(diaEditado.IdFactorSalarioIntegrado);
            var FSI = await _FSIService.ObtenXId(diaEditado.IdFactorSalarioIntegrado);
            var ExisteFSR = await _FSRService.ObtenerTodosXProyecto(FSI.IdProyecto);
            if (ExisteFSR.Count > 0)
            {
                var FSR = ExisteFSR.FirstOrDefault();
                await RecalcularFSR(FSR.Id);
            }
        }
    }
}
