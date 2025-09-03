using AutoMapper;
using DocumentFormat.OpenXml.Drawing;
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
        private readonly FactorSalarioRealProceso<TContext> _fsrProceso;
        private readonly PrecioUnitarioProceso<TContext> _precioUnitarioProceso;

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
            , FactorSalarioRealProceso<TContext> fsrProceso
            , PrecioUnitarioProceso<TContext> precioUnitarioProceso
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
            _fsrProceso = fsrProceso;
            _precioUnitarioProceso = precioUnitarioProceso;
        }

        public async Task<CostoHorarioFijoXPrecioUnitarioDetalleDTO> ObtenerCostoFijoXIdDetalle(int IdPrecioUnitarioDetalle){
            var detalle = await _CostoFijoService.ObtenTodosXIdPrecioUnitarioDetalle(IdPrecioUnitarioDetalle);
            detalle.InteresSobreCapital = detalle.Inversion * detalle.InteresAnual / detalle.HorasUso;
            detalle.Depreciacion = detalle.Inversion / detalle.VidaUtil;
            detalle.Reparaciones = detalle.PorcentajeReparacion * detalle.Depreciacion;
            detalle.Seguro = detalle.PorcentajeSeguroAnual * detalle.Inversion / detalle.HorasUso;
            detalle.GastosAnuales = detalle.GastoAnual / detalle.HorasUso;
            detalle.Suma = detalle.InteresSobreCapital + detalle.Depreciacion + detalle.Reparaciones + detalle.Seguro + detalle.GastosAnuales;
            detalle.SubtotalGastosFijos = detalle.Suma * (12 / detalle.MesTrabajoReal);
            //Horas Uso = horas de trabajo al año
            //Vida Util = horas totales de uso
            return detalle;
        }

        public async Task<List<CostoHorarioVariableXPrecioUnitarioDetalleDTO>> obtenerRegistrosXIdDetalle(int IdPrecioUnitarioDetalle)
        {
            var items = _dbContext.Database.SqlQueryRaw<string>("""
                select
                ch.Id
                , ch.IdPrecioUnitarioDetalle
                , ch.IdCostoVariablePerteneciente
                , ch.TipoCostoVariable
                , ch.Rendimiento
                , ch.IdInsumo
                , ch.EsCompuesto
                , I.CostoUnitario
                , FORMAT(I.CostoUnitario, 'N', 'en-us') as CostoUnitarioConFormato
                , I.CostoBase
                , FORMAT(I.CostoBase, 'N', 'en-us') as CostoBaseConFormato
                , ch.Cantidad
                , FORMAT(ch.Cantidad, 'N', 'en-us') as CantidadConFormato
                , ch.CantidadExcedente
                , case 
                when ch.TipoCostoVariable = 1 --Combustible 
                	then ch.Cantidad * ch.Rendimiento * I.CostoUnitario 
                when ch.TipoCostoVariable = 2 --Lubricantes
                	then ch.Cantidad * ch.Rendimiento * I.CostoUnitario
                when ch.TipoCostoVariable = 3 --Llantas
                	then ch.Cantidad * I.CostoUnitario / ch.Rendimiento 
                when ch.TipoCostoVariable = 4 --Operación
                	then ch.Cantidad * I.CostoUnitario 
                when ch.TipoCostoVariable = 5 --Fletes
                	then ch.Cantidad * I.CostoUnitario / ch.Rendimiento
                else
                	0
                end 
                as Importe
                , FORMAT(
                Case
                	when ch.TipoCostoVariable = 1 
                		then ch.Cantidad * ch.Rendimiento * I.CostoUnitario 
                	when ch.TipoCostoVariable = 2 
                		then ch.Cantidad * ch.Rendimiento * I.CostoUnitario
                	when ch.TipoCostoVariable = 3 
                		then ch.Cantidad * I.CostoUnitario / ch.Rendimiento 
                	when ch.TipoCostoVariable = 4 
                		then ch.Cantidad * I.CostoUnitario 
                	when ch.TipoCostoVariable = 5 
                		then ch.Cantidad * I.CostoUnitario / ch.Rendimiento
                	else 0
                end
                , 'N', 'en-us') as ImporteConFormato
                , I.Codigo
                , I.Descripcion
                , I.Unidad
                , I.IdTipoInsumo
                , I.IdFamiliaInsumo
                , I.IdProyecto
                , I.EsFsrGlobal
                from CostoHorarioVariableXPrecioUnitarioDetalle ch
                join Insumo I
                on ch.IdInsumo = I.Id
                where IdPrecioUnitarioDetalle =
                """ + IdPrecioUnitarioDetalle +
                """ and ch.IdCostoVariablePerteneciente = 0 for json path""").ToList();
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

        public async Task<List<CostoHorarioVariableXPrecioUnitarioDetalleDTO>> obtenerRegistrosXIdDetallePerteneciente(int IdPrecioUnitarioDetalle, int IdCostoVariablePerteneciente)
        {
            var items = _dbContext.Database.SqlQueryRaw<string>("""
                select
                ch.Id
                , ch.IdPrecioUnitarioDetalle
                , ch.IdCostoVariablePerteneciente
                , ch.TipoCostoVariable
                , ch.Rendimiento
                , ch.IdInsumo
                , ch.EsCompuesto
                , I.CostoUnitario
                , FORMAT(I.CostoUnitario, 'N', 'en-us') as CostoUnitarioConFormato
                , I.CostoBase
                , FORMAT(I.CostoBase, 'N', 'en-us') as CostoBaseConFormato
                , ch.Cantidad
                , FORMAT(ch.Cantidad, 'N', 'en-us') as CantidadConFormato
                , ch.CantidadExcedente
                , case 
                when ch.TipoCostoVariable = 1 --Combustible 
                	then ch.Cantidad * ch.Rendimiento * I.CostoUnitario 
                when ch.TipoCostoVariable = 2 --Lubricantes
                	then ch.Cantidad * ch.Rendimiento * I.CostoUnitario
                when ch.TipoCostoVariable = 3 --Llantas
                	then ch.Cantidad * I.CostoUnitario / ch.Rendimiento 
                when ch.TipoCostoVariable = 4 --Operación
                	then ch.Cantidad * I.CostoUnitario 
                when ch.TipoCostoVariable = 5 --Fletes
                	then ch.Cantidad * I.CostoUnitario / ch.Rendimiento
                else
                	0
                end 
                as Importe
                , FORMAT(
                Case
                	when ch.TipoCostoVariable = 1 
                		then ch.Cantidad * ch.Rendimiento * I.CostoUnitario 
                	when ch.TipoCostoVariable = 2 
                		then ch.Cantidad * ch.Rendimiento * I.CostoUnitario
                	when ch.TipoCostoVariable = 3 
                		then ch.Cantidad * I.CostoUnitario / ch.Rendimiento 
                	when ch.TipoCostoVariable = 4 
                		then ch.Cantidad * I.CostoUnitario
                	when ch.TipoCostoVariable = 5 
                		then ch.Cantidad * I.CostoUnitario / ch.Rendimiento
                	else 0
                end
                , 'N', 'en-us') as ImporteConFormato
                , I.Codigo
                , I.Descripcion
                , I.Unidad
                , I.IdTipoInsumo
                , I.IdFamiliaInsumo
                , I.IdProyecto
                , I.EsFsrGlobal
                from CostoHorarioVariableXPrecioUnitarioDetalle ch
                join Insumo I
                on ch.IdInsumo = I.Id
                where IdPrecioUnitarioDetalle =
                """ + IdPrecioUnitarioDetalle +
                """ and ch.IdCostoVariablePerteneciente = """
                + IdCostoVariablePerteneciente + 
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

        public async Task CrearEditarCostoVariable(CostoHorarioVariableXPrecioUnitarioDetalleDTO registro)
        {
            var insumo = await _InsumoService.ObtenXId(registro.IdInsumo);
            if(insumo.idTipoInsumo == 10001)
            {
                var registros = await obtenerRegistrosXIdDetallePerteneciente(registro.IdPrecioUnitarioDetalle, registro.IdCostoVariablePerteneciente);
                var registrosFiltrados = registros.Where(z => z.IdTipoInsumo == 10000).ToList();
                decimal costoTotal = 0;
                if(registrosFiltrados.Count > 0)
                {
                    foreach(var costo in registrosFiltrados)
                    {
                        costoTotal =+ costo.Importe;
                    }
                }
                registro.CostoBase = costoTotal;
                registro.CostoUnitario = costoTotal;
            }
            if (insumo.id != 0)
            {
                insumo.Codigo = registro.Codigo;
                insumo.Descripcion = registro.Descripcion;
                insumo.Unidad = registro.Unidad;
                insumo.idTipoInsumo = registro.IdTipoInsumo;
                insumo.CostoBase = registro.CostoBase;
                if(insumo.idTipoInsumo != 10000)
                {
                    insumo.CostoUnitario = registro.CostoBase;
                }
                var insumoEditado = await _InsumoService.Editar(insumo);
            }
            else
            {
                var insumoCreacion = new InsumoCreacionDTO();
                insumoCreacion.Codigo = registro.Codigo;
                insumoCreacion.Descripcion = registro.Descripcion;
                insumoCreacion.Unidad = registro.Unidad;
                insumoCreacion.idTipoInsumo = registro.IdTipoInsumo;
                insumoCreacion.CostoBase = registro.CostoBase;
                if (insumoCreacion.idTipoInsumo != 10000)
                {
                    insumoCreacion.CostoUnitario = registro.CostoBase;
                }
                insumoCreacion.CostoUnitario = registro.CostoBase;
                insumo = await _InsumoService.CrearYObtener(insumoCreacion);
            }
            registro.IdInsumo = insumo.id;

            var registroCreado = await _CostoVariableService.CrearYObtener(registro);
            var obtenerRegistros = await obtenerRegistrosXIdDetallePerteneciente(registro.IdPrecioUnitarioDetalle, registro.IdCostoVariablePerteneciente);
            var costoFijo = await ObtenerCostoFijoXIdDetalle(registro.IdPrecioUnitarioDetalle);

            if (registro.IdCostoVariablePerteneciente == 0)
            {
                var detalles = await _precioUnitarioProceso.ObtenerDetallesPorIdInsumo(registro.IdInsumo, _dbContext);
                foreach(var detalle in detalles)
                {
                    registro.Id = 0;

                }

            }
            
        }
    }
}
