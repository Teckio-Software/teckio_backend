using ERP_TECKIO.Modelos.Facturacion;
using ERP_TECKIO.Procesos;
using ERP_TECKIO.Procesos.Facturacion;
using ERP_TECKIO.Servicios;
using ERP_TECKIO.Servicios.Contratos;
using ERP_TECKIO.Servicios.Contratos.Facturacion;
using ERP_TECKIO.Servicios.Facturacion;
using Microsoft.EntityFrameworkCore;
namespace ERP_TECKIO
{
    public static class Dependencia
    {
        public static void InyectarDependencias(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            services.AddAutoMapper(typeof(AutoMapperProfile));

            //Sistema Teckio
            services.AddScoped(typeof(IEmpleadoService<>), typeof(EmpleadoService<>));
            services.AddScoped(typeof(IPrecioUnitarioXEmpleadoService<>), typeof(PrecioUnitarioXEmpleadoService<>));
            //Proyecto
            services.AddScoped(typeof(IProyectoService<>), typeof(ProyectoService<>));
            services.AddScoped(typeof(IPrecioUnitarioService<>), typeof(PrecioUnitarioService<>));
            services.AddScoped(typeof(IPrecioUnitarioDetalleService<>), typeof(PrecioUnitarioDetalleService<>));
            services.AddScoped(typeof(IGeneradoresService<>), typeof(GeneradoresService<>));
            services.AddScoped(typeof(IProgramacionEstimadaService<>), typeof(ProgramacionEstimadaService<>));
            services.AddScoped(typeof(IConjuntoIndirectosService<>), typeof(ConjuntoIndirectosService<>));
            services.AddScoped(typeof(IIndirectosService<>), typeof(IndirectosService<>));
            services.AddScoped(typeof(IIndirectosXConceptoService<>), typeof(IndirectosXConceptoService<>));
            services.AddScoped(typeof(IProgramacionEstimadaGanttService<>), typeof(ProgramacionEstimadaGanttService<>));
            services.AddScoped(typeof(IDependenciaProgramacionEstimadaService<>), typeof(DependenciaProgramacionEstimadaService<>));
            services.AddScoped(typeof(IOperacionesXPrecioUnitarioDetalleService<>), typeof(OperacionesXPrecioUnitarioDetalleService<>));
            //Factor de salario real
            services.AddScoped(typeof(IFactorSalarioRealService<>), typeof(FactorSalarioRealService<>));
            services.AddScoped(typeof(IFactorSalarioRealDetalleService<>), typeof(FactorSalarioRealDetalleService<>));
            services.AddScoped(typeof(IFactorSalarioIntegradoService<>), typeof(FactorSalarioIntegradoService<>));
            services.AddScoped(typeof(IRelacionFSRInsumoService<>), typeof(RelacionFSRInsumoService<>));
            services.AddScoped(typeof(IDiasConsideradosService<>), typeof(DiasConsideradosService<>));
            services.AddScoped(typeof(IFsrxinsummoMdOService<>), typeof(FsrxinsummoMdOService<>));
            services.AddScoped(typeof(IFsrxinsummoMdOdetalleService<>), typeof(FsrxinsummoMdOdetalleService<>));
            services.AddScoped(typeof(IFsixinsummoMdOService<>), typeof(FsixinsummoMdOService<>));
            services.AddScoped(typeof(IFsixinsummoMdOdetalleService<>), typeof(FsixinsummoMdOdetalleService<>));
            //Insumos
            services.AddScoped(typeof(ITipoInsumoService<>), typeof(TipoInsumoService<>));
            services.AddScoped(typeof(IFamiliaInsumoService<>), typeof(FamiliaInsumoService<>));
            services.AddScoped(typeof(IInsumoService<>), typeof(InsumoService<>));
            services.AddScoped(typeof(IEspecialidadService<>), typeof(EspecialidadService<>));
            services.AddScoped(typeof(IConceptoService<>), typeof(ConceptoService<>));
            services.AddScoped(typeof(IContratistaService<>), typeof(ContratistaService<>));
            //Compras
            services.AddScoped(typeof(IRequisicionService<>), typeof(RequisicionService<>));
            services.AddScoped(typeof(IInsumoXRequisicionService<>), typeof(InsumoXRequisicionService<>));
            services.AddScoped(typeof(ICotizacionService<>), typeof(CotizacionService<>));
            services.AddScoped(typeof(IInsumoXCotizacionService<>), typeof(InsumoXCotizacionService<>));
            services.AddScoped(typeof(IOrdenCompraService<>), typeof(OrdenCompraService<>));
            services.AddScoped(typeof(IInsumoXOrdenCompraService<>), typeof(InsumoXOrdenCompraService<>));
            services.AddScoped(typeof(ICompraDirectaService<>), typeof(CompraDirectaService<>));
            services.AddScoped(typeof(IInsumoXCompraDirectaService<>), typeof(InsumoXCompraDirectaService<>));
            services.AddScoped(typeof(IImpuestoInsumoCotizadoService<>), typeof(ImpuestoInsumoCotizadoService<>));
            services.AddScoped(typeof(IImpuestoInsumoOrdenCompraService<>), typeof(ImpuestoInsumoOrdenCompraService<>));
            services.AddScoped(typeof(IImpuestoService<>), typeof(ImpuestoService<>));
            //Almacenes
            services.AddScoped(typeof(IAlmacenService<>), typeof(AlmacenService<>));
            services.AddScoped(typeof(IAlmacenEntradaService<>), typeof(AlmacenEntradaService<>));
            services.AddScoped(typeof(IInsumoXAlmacenEntradaService<>), typeof(AlmacenEntradaInsumoService<>));
            services.AddScoped(typeof(IAlmacenSalidaService<>), typeof(AlmacenSalidaService<>));
            services.AddScoped(typeof(IInsumoXAlmacenSalidaService<>), typeof(AlmacenSalidaInsumoService<>));
            services.AddScoped(typeof(IAlmacenExistenciaInsumoService<>), typeof(AlmacenExistenciaInsumoService<>));
            //Contabilidad
            services.AddScoped(typeof(IClientesService<>), typeof(ClientesService<>));
            services.AddScoped(typeof(IRubroService<>), typeof(RubroService<>));
            services.AddScoped(typeof(ITipoPolizaService<>), typeof(TipoPolizaService<>));
            services.AddScoped(typeof(ICuentaContableService<>), typeof(CuentaContableService<>));
            services.AddScoped(typeof(ICodigoAgrupadorService<>), typeof(CodigoAgrupadorSatService<>));
            services.AddScoped(typeof(IPolizaService<>), typeof(PolizaService<>));
            services.AddScoped(typeof(IPolizaDetalleService<>), typeof(PolizaDetalleService<>));
            services.AddScoped(typeof(ISaldosBalanzaComprobacionService<>), typeof(SaldosBalanzaComrpobacionService<>));
            services.AddScoped(typeof(ICuentaBancariaService<>), typeof(CuentaBancariaService<>));
            services.AddScoped(typeof(ICuentaBancariaEmpresaService<>), typeof(CuentaBancariaEmpresaService<>));
            services.AddScoped(typeof(ICuentaBancariaClienteService<>), typeof(CuentaBancariaClienteService<>));
            services.AddScoped(typeof(IMovimientoBancarioService<>), typeof(MovimientoBancarioService<>));
            services.AddScoped(typeof(IMBancarioContratistaService<>), typeof(MBancarioContratistaService<>));
            services.AddScoped(typeof(IMovimientoBancarioClienteService<>), typeof(MovimientoBancarioClienteService<>));
            services.AddScoped(typeof(IMovimientoBancarioEmpresaService<>), typeof(MovimientoBancarioEmpresaService<>));
            services.AddScoped(typeof(IMovimientoBancarioSaldoService<>), typeof(MovimientoBancarioSaldoService<>));

            //Estimaciones
            services.AddScoped(typeof(IEstimacionesService<>), typeof(EstimacionesService<>));
            services.AddScoped(typeof(IPeriodoEstimacionesService<>), typeof(PeriodoEstimacionesService<>));
            //Contratos
            services.AddScoped(typeof(IContratoService<>), typeof(ContratoService<>));
            services.AddScoped(typeof(IDetalleXContratoService<>), typeof(DetalleXContratoService<>));
            services.AddScoped(typeof(IPorcentajeAcumuladoContratoService<>), typeof(PorcentajeAcumuladoContratoService<>));
            services.AddScoped(typeof(IBancoService<>), typeof(BancoService<>));
            //Procesos
            services.AddScoped(typeof(PrecioUnitarioProceso<>));
            services.AddScoped(typeof(GeneradoresProceso<>));
            services.AddScoped(typeof(TipoInsumoProceso<>));
            services.AddScoped(typeof(FamiliaInsumoProceso<>));
            services.AddScoped(typeof(InsumoProceso<>));
            services.AddScoped(typeof(ConceptoProceso<>));
            services.AddScoped(typeof(EspecialidadProceso<>));
            services.AddScoped(typeof(ProyectoProceso<>));
            services.AddScoped(typeof(FactorSalarioRealProceso<>));
            services.AddScoped(typeof(ProgramacionEstimadaProceso<>));
            services.AddScoped(typeof(CuentaBancariaProceso<>));
            services.AddScoped(typeof(EstimacionesProceso<>));
            services.AddScoped(typeof(ClienteProceso<>));
            services.AddScoped(typeof(RequisicionProceso<>));
            services.AddScoped(typeof(CotizacionProceso<>));
            services.AddScoped(typeof(ContratosProceso<>));
            services.AddScoped(typeof(OrdenCompraProceso<>));
            services.AddScoped(typeof(AlmacenEntradaProceso<>));
            services.AddScoped(typeof(ActualizaEstatusSubProceso<>));
            services.AddScoped(typeof(AlmacenSalidaProceso<>));
            services.AddScoped(typeof(ExistenciasProceso<>));
            services.AddScoped(typeof(ObjetoRequisicionProceso<>));
            services.AddScoped(typeof(PrecioUnitarioXEmpleadoProceso<>));
            services.AddScoped(typeof(ExplocionInsumosProceso<>));
            services.AddScoped(typeof(ObtenFacturaProceso<>));
            //services.AddScoped(typeof(RegistraFacturaProceso<>));
            //services.AddScoped(typeof(ObtenFacturasProceso<>));
            services.AddScoped(typeof(MovimientoBancarioProceso<>));
            services.AddScoped(typeof(MovimientoBancarioSaldoProeceso<>));
            services.AddScoped(typeof(ReporteDestajoProceso<>));
            services.AddScoped(typeof(IndirectosProceso<>));
            services.AddScoped(typeof(IndirectosXConceptoProceso<>));
            services.AddScoped(typeof(ProgramacionEstimadaGanttProceso<>));
            services.AddScoped(typeof(ImporteSemanalGanttProceso<>));
            services.AddScoped(typeof(GeneradoresProceso<>));
            services.AddScoped(typeof(DbContextOptionsBuilder<>));
            services.AddScoped(typeof(ContratistaCuentasContablesProceso<>));
            services.AddScoped(typeof(ClienteCuentasContablesProceso<>));
            services.AddScoped(typeof(PolizaProceso<>));

            //Facturas
            services.AddScoped(typeof(ITipoImpuestoService<>), typeof(TipoImpuestoService<>));
            services.AddScoped(typeof(IUsoCfdiSatService<>), typeof(UsoCfdiSatService<>));
            services.AddScoped(typeof(IUnidadSatService<>), typeof(UnidadSatService<>));
            services.AddScoped(typeof(IUnidadService<>), typeof(UnidadService<>));
            services.AddScoped(typeof(ITipoFactorService<>), typeof(TipoFactorService<>));
            services.AddScoped(typeof(ISubcategoriaProdutoYServicio<>), typeof(SubcategoriaProductoYServicioService<>));
            services.AddScoped(typeof(ICategoriaProductoYServicioService<>), typeof(CategoriaProductoYServicioService<>));
            services.AddScoped(typeof(IRegimenFiscalSatService<>), typeof(RegimenFiscalSatService<>));
            services.AddScoped(typeof(IProductoYServicioSatService<>), typeof(ProductoYServicioSatService<>));
            services.AddScoped(typeof(IProductoYservicioService<>), typeof(ProductoYservicio<>));
            services.AddScoped(typeof(IMonedaSatService<>), typeof(MonedaSatService<>));
            services.AddScoped(typeof(IFormaPagoSatService<>), typeof(FormaPagoSatService<>));
            services.AddScoped(typeof(IFacturaEmisorService<>), typeof(FacturaEmisorService<>));
            services.AddScoped(typeof(IFacturaImpuestoLocalService<>), typeof(FacturaImpuestoLocalService<>));
            services.AddScoped(typeof(IFacturaImpuestosService<>), typeof(FacturaImpuestosService<>));
            services.AddScoped(typeof(IFacturaDetalleService<>), typeof(FacturaDetalleService<>));
            services.AddScoped(typeof(IFacturaDetalleImpuestoService<>), typeof(FacturaDetalleImpuestoService<>));
            services.AddScoped(typeof(IFacturaComplementoPagoService<>), typeof(FacturaComplementoPagoService<>));
            services.AddScoped(typeof(IFacturaService<>), typeof(FacturaService<>));
            services.AddScoped(typeof(IDetalleValidacionService<>), typeof(DetalleValidacionService<>));
            services.AddScoped(typeof(IClasificacionImpuestoService<>), typeof(ClasificacionImpuestoService<>));
            services.AddScoped(typeof(ICategoriaImpuestoService<>), typeof(CategoriaImpuestoService<>));
            services.AddScoped(typeof(IAcuseValidacionService<>), typeof(AcuseValidacionService<>));
            services.AddScoped(typeof(IArchivoService<>), typeof(ArchivoService<>));
            services.AddScoped(typeof(IFacturaXOrdenCompraService<>), typeof(FacturaXOrdenCompraService<>));
        }
    }
}