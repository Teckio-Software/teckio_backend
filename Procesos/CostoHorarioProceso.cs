using AutoMapper;
using ERP_TECKIO.DTO;
using ERP_TECKIO.Modelos.Presupuesto;
using ERP_TECKIO.Servicios.Contratos;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ERP_TECKIO.Procesos
{
    public class CostoHorarioProceso<TContext> where TContext : DbContext
    {
        private readonly IProyectoService<TContext> _ProyectoService;
        private readonly IPrecioUnitarioService<TContext> _PrecioUnitarioService;
        private readonly IPrecioUnitarioDetalleService<TContext> _PrecioUnitarioDetalleService;
        private readonly IConceptoService<TContext> _ConceptoService;
        private readonly IInsumoService<TContext> _InsumoService;
        private readonly IGeneradoresService<TContext> _GeneradoresService;
        private readonly IProgramacionEstimadaService<TContext> _ProgramacionEstimadaService;
        private readonly IProgramacionEstimadaGanttService<TContext> _ProgramacionEstimadaGanttService;
        private readonly IFactorSalarioRealService<TContext> _FSRService;
        private readonly IConjuntoIndirectosService<TContext> _conjuntoIndirectosService;
        private readonly IEstimacionesService<TContext> _estimacionesService;
        private readonly IPrecioUnitarioXEmpleadoService<TContext> _precioUnitarioXEmpleadoService;
        private readonly IDetalleXContratoService<TContext> _detalleXContratoService;
        private readonly IOperacionesXPrecioUnitarioDetalleService<TContext> _OperacionXPUService;
        private readonly IFsrxinsummoMdOService<TContext> _FsrxinsummoMdOService;
        private readonly ICostoHorarioFijoXPrecioUnitarioDetalleService<TContext> _CostoFijoService;
        private readonly ICostoHorarioVariableXPrecioUnitarioDetalleService<TContext> _CostoVariableService;
        private readonly IMapper _Mapper;
        private readonly TContext _dbContext;

        public CostoHorarioProceso(
            IProyectoService<TContext> proyectoService
            , IPrecioUnitarioService<TContext> precioUnitarioService
            , IPrecioUnitarioDetalleService<TContext> precioUnitarioDetalleService
            , IConceptoService<TContext> conceptoService
            , IInsumoService<TContext> insumoService
            , IGeneradoresService<TContext> generadoresService
            , IProgramacionEstimadaService<TContext> programacionEstimadaService
            , IProgramacionEstimadaGanttService<TContext> programacionEstimadaGanttService
            , IFactorSalarioRealService<TContext> FSRService
            , IConjuntoIndirectosService<TContext> conjuntoIndirectosService
            , IEstimacionesService<TContext> estimacionesService
            , IPrecioUnitarioXEmpleadoService<TContext> precioUnitarioXEmpleadoService
            , IDetalleXContratoService<TContext> detalleXContratoService
            , IOperacionesXPrecioUnitarioDetalleService<TContext> operacionXPUService
            , IFsrxinsummoMdOService<TContext> fsrxinsummoMdOService
            , ICostoHorarioFijoXPrecioUnitarioDetalleService<TContext> costoFijoService
            , ICostoHorarioVariableXPrecioUnitarioDetalleService<TContext> costoVariableService
            , IMapper mapper
            , TContext dbContext

            )
        {
            _ProyectoService = proyectoService;
            _PrecioUnitarioService = precioUnitarioService;
            _PrecioUnitarioDetalleService = precioUnitarioDetalleService;
            _ConceptoService = conceptoService;
            _InsumoService = insumoService;
            _GeneradoresService = generadoresService;
            _ProgramacionEstimadaService = programacionEstimadaService;
            _ProgramacionEstimadaGanttService = programacionEstimadaGanttService;
            _FSRService = FSRService;
            _conjuntoIndirectosService = conjuntoIndirectosService;
            _estimacionesService = estimacionesService;
            _precioUnitarioXEmpleadoService = precioUnitarioXEmpleadoService;
            _detalleXContratoService = detalleXContratoService;
            _Mapper = mapper;
            _dbContext = dbContext;
            _OperacionXPUService = operacionXPUService;
            _FsrxinsummoMdOService = fsrxinsummoMdOService;
            _CostoVariableService = costoVariableService;
            _CostoFijoService = costoFijoService;
        }

        public async Task<CostoHorarioFijoXPrecioUnitarioDetalleDTO> ObtenerCostoFijoXIdDetalle(int IdPrecioUnitarioDetalle){
            var detalle = await _CostoFijoService.ObtenTodosXIdPrecioUnitarioDetalle(IdPrecioUnitarioDetalle);
            return detalle;
        }

        public async Task<List<CostoHorarioVariableXPrecioUnitarioDetalleDTO>> obtenerRegistrosXIdDetalle(int IdPrecioUnitarioDetalle)
        {
            var items = _dbContext.Database.SqlQueryRaw<string>("""
                select
                ch.Id
                , ch.IdPrecioUnitarioDetalle
                , ch.TipoCostoVariable
                , ch.Rendimiento
                , PUD.IdPrecioUnitario
                , PUD.IdInsumo
                , PUD.EsCompuesto
                , I.CostoUnitario
                , FORMAT(I.CostoUnitario, 'N', 'en-us') as CostoUnitarioConFormato
                --Costo Unitario Con Formato
                , I.CostoBase
                , FORMAT(I.CostoBase, 'N', 'en-us') as CostoBaseConFormato
                --Costo Base Con Formato
                , PUD.Cantidad
                , FORMAT(PUD.Cantidad, 'N', 'en-us') as CantidadConFormato
                --Cantidad Con Formato
                , PUD.CantidadExcedente
                , PUD.IdPrecioUnitarioDetallePerteneciente
                , I.Codigo
                , I.Descripcion
                , I.Unidad
                , I.IdTipoInsumo
                , I.IdFamiliaInsumo
                , I.IdProyecto
                , I.EsFsrGlobal
                from CostoHorarioVariableXPrecioUnitarioDetalle ch
                join PrecioUnitarioDetalle PUD
                on PUD.Id = ch.IdPrecioUnitarioDetalle
                join Insumo I
                on PUD.IdInsumo = I.Id
                where IdPrecioUnitarioDetallePerteneciente = 
                """ + IdPrecioUnitarioDetalle +
                """ for json path""").ToList();
            if (items.Count <= 0)
            {
                return new List<CostoHorarioVariableXPrecioUnitarioDetalleDTO>();
            }
            string json = string.Join("", items);
            var datos = JsonSerializer.Deserialize<List<CostoHorarioVariableXPrecioUnitarioDetalleDTO>>(json);
            if (datos == null)
            {
                return new List<CostoHorarioVariableXPrecioUnitarioDetalleDTO>();
            }
            return datos;
        }

        public async Task<List<CostoHorarioVariableXPrecioUnitarioDetalleDTO>> ObtenerCostosVariables(PrecioUnitarioDetalleDTO registro)
        {
            var registros = new List<CostoHorarioVariableXPrecioUnitarioDetalleDTO>();
            if (registro.IdTipoInsumo == 10008) //validar que 10008 sea costo horario
            {
                registros = await obtenerRegistrosXIdDetalle(registro.Id);
            }
            return registros;
        }

        public async Task EditarCostoVariable(CostoHorarioVariableXPrecioUnitarioDetalleDTO registro)
        {
            var insumo = await _InsumoService.ObtenXId(registro.IdInsumo);
        }
    }
}
