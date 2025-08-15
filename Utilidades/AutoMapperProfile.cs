using AutoMapper;
using ERP_TECKIO.DTO;
using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.DTO.Usuario;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Modelos.Contabilidad;
using ERP_TECKIO.Modelos.Facturacion;
using ERP_TECKIO.Modelos.Facturaion;
using ERP_TECKIO.Modelos.Presupuesto;
using Microsoft.AspNetCore.Mvc.RazorPages;




namespace ERP_TECKIO
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Teckio

            #region Proyecto
            CreateMap<Proyecto, ProyectoDTO>();
            CreateMap<ProyectoDTO, Proyecto>();
            //////////////////////////////////////
            CreateMap<PrecioUnitario, PrecioUnitarioDTO>()
            .ForMember(destino => destino.EsCatalogoGeneral,
                opt => opt.MapFrom(origien => origien.EsCatalogoGeneral == null ? false : origien.EsCatalogoGeneral))
            .ForMember(destino => destino.EsAvanceObra,
                opt => opt.MapFrom(origien => origien.EsAvanceObra == null ? false : origien.EsAvanceObra))
            .ForMember(destino => destino.EsAdicional,
                opt => opt.MapFrom(origien => origien.EsAdicional == null ? false : origien.EsAdicional));
            CreateMap<PrecioUnitario, PrecioUnitarioCopiaDTO>();
            CreateMap<PrecioUnitarioDTO, PrecioUnitario>()
            .ForMember(z => z.IdProyectoNavigation,
                opt => opt.Ignore())
                .ForMember(z => z.IdConceptoNavigation,
                opt => opt.Ignore());
            CreateMap<PrecioUnitarioCopiaDTO, PrecioUnitario>()
                .ForMember(z => z.IdProyectoNavigation,
                opt => opt.Ignore())
                .ForMember(z => z.IdConceptoNavigation,
                opt => opt.Ignore());
            //////////////////////////////////////
            CreateMap<PrecioUnitarioDetalle, PrecioUnitarioDetalleDTO>();
            CreateMap<PrecioUnitarioDetalleDTO, PrecioUnitarioDetalle>()
                .ForMember(z => z.IdInsumoNavigation,
                opt => opt.Ignore())
                .ForMember(z => z.IdPrecioUnitarioNavigation,
                opt => opt.Ignore());
            //////////////////////////////////////
            CreateMap<Generadores, GeneradoresDTO>();
            CreateMap<GeneradoresDTO, Generadores>()
                .ForMember(z => z.IdPrecioUnitarioNavigation,
                opt => opt.Ignore());
            //////////////////////////////////////
            CreateMap<FactorSalarioIntegrado, FactorSalarioIntegradoDTO>();
            CreateMap<FactorSalarioIntegradoDTO, FactorSalarioIntegrado>()
                .ForMember(z => z.IdProyectoNavigation,
                opt => opt.Ignore());
            //////////////////////////////////////
            CreateMap<FactorSalarioReal, FactorSalarioRealDTO>()
                .ForMember(destino => destino.EsCompuesto,
                opt => opt.MapFrom(origien => origien.EsCompuesto == null ? false : origien.EsCompuesto));
            CreateMap<FactorSalarioRealDTO, FactorSalarioReal>()
                .ForMember(z => z.IdProyectoNavigation,
                opt => opt.Ignore());
            //////////////////////////////////////
            CreateMap<FactorSalarioRealDetalle, FactorSalarioRealDetalleDTO>();
            CreateMap<FactorSalarioRealDetalleDTO, FactorSalarioRealDetalle>()
                .ForMember(z => z.IdFactorSalarioRealNavigation,
                opt => opt.Ignore());
            //////////////////////////////////////
            CreateMap<DiasConsiderados, DiasConsideradosDTO>();
            CreateMap<DiasConsideradosDTO, DiasConsiderados>()
                .ForMember(z => z.IdFactorSalarioIntegradoNavigation,
                opt => opt.Ignore());
            CreateMap<ParametrosFsr, ParametrosFsrDTO>();
            CreateMap<ParametrosFsrDTO, ParametrosFsr>();

            CreateMap<PorcentajeCesantiaEdad, PorcentajeCesantiaEdadDTO>();
            CreateMap<PorcentajeCesantiaEdadDTO, PorcentajeCesantiaEdad>();
            //////////////////////////////////////
            CreateMap<FsrxinsummoMdO, FsrxinsummoMdODTO>();
            CreateMap<FsrxinsummoMdODTO, FsrxinsummoMdO>();
            CreateMap<FsrxinsummoMdOdetalle, FsrxinsummoMdOdetalleDTO>();
            CreateMap<FsrxinsummoMdOdetalleDTO, FsrxinsummoMdOdetalle>();
            //////////////////////////////////////
            CreateMap<FsixinsummoMdO, FsixinsummoMdODTO>();
            CreateMap<FsixinsummoMdODTO, FsixinsummoMdO>();
            CreateMap<FsixinsummoMdOdetalle, FsixinsummoMdOdetalleDTO>();
            CreateMap<FsixinsummoMdOdetalleDTO, FsixinsummoMdOdetalle>();
            //////////////////////////////////////
            CreateMap<RelacionFSRInsumo, RelacionFSRInsumoDTO>();
            CreateMap<RelacionFSRInsumoDTO, RelacionFSRInsumo>()
                .ForMember(z => z.IdProyectoNavigation,
                opt => opt.Ignore())
                .ForMember(z => z.IdInsumoNavigation,
                opt => opt.Ignore());
            //////////////////////////////////////
            CreateMap<ProgramacionEstimada, ProgramacionEstimadaDTO>();
            CreateMap<ProgramacionEstimadaDTO, ProgramacionEstimada>()
                .ForMember(z => z.IdProyectoNavigation,
                opt => opt.Ignore())
                .ForMember(z => z.IdConceptoNavigation,
                opt => opt.Ignore())
                .ForMember(z => z.IdPrecioUnitarioNavigation,
                opt => opt.Ignore());
            CreateMap<Estimaciones, EstimacionesDTO>();
            CreateMap<EstimacionesDTO, Estimaciones>()
                .ForMember(z => z.IdConceptoNavigation,
                opt => opt.Ignore())
                .ForMember(z => z.IdPeriodoNavigation,
                opt => opt.Ignore())
                .ForMember(z => z.IdPrecioUnitarioNavigation,
                opt => opt.Ignore())
                .ForMember(z => z.IdProyectoNavigation,
                opt => opt.Ignore());
            CreateMap<GeneradoresXEstimacion, GeneradoresXEstimacionDTO>();
            CreateMap<GeneradoresXEstimacionDTO, GeneradoresXEstimacion>()
                .ForMember(destino => destino.IdGenerador,
                opt => opt.MapFrom(origen => origen.IdGenerador <= 0 ? null : origen.IdGenerador));

            CreateMap<PeriodoEstimaciones, PeriodoEstimacionesDTO>();
            CreateMap<PeriodoEstimacionesDTO, PeriodoEstimaciones>()
                .ForMember(z => z.IdProyectoNavigation,
                opt => opt.Ignore());
            //////////////////////////////////////
            CreateMap<DetalleXContrato, DetalleXContratoDTO>()
                .ForMember(destino => destino.FactorDestajo,
                opt => opt.MapFrom(origen => origen.FactorDestajo == null ? 0 : origen.FactorDestajo));
            CreateMap<DetalleXContratoDTO, DetalleXContrato>()
                .ForMember(z => z.IdContratoNavigation,
                opt => opt.Ignore())
                .ForMember(z => z.IdPrecioUnitarioNavigation,
                opt => opt.Ignore());
            //////////////////////////////////////
            CreateMap<PorcentajeAcumuladoContrato, PorcentajeAcumuladoContratoDTO>();
            CreateMap<PorcentajeAcumuladoContratoDTO, PorcentajeAcumuladoContrato>()
                .ForMember(z => z.IdPrecioUnitarioNavigation,
                opt => opt.Ignore());
            //////////////////////////////////////
            CreateMap<Contrato, ContratoDTO>();
            CreateMap<ContratoDTO, Contrato>()
                .ForMember(z => z.IdContratistaNavigation,
                opt => opt.Ignore())
                .ForMember(z => z.IdProyectoNavigation,
                opt => opt.Ignore());

            CreateMap<ConjuntoIndirectos, ConjuntoIndirectosDTO>();
            CreateMap<ConjuntoIndirectosDTO, ConjuntoIndirectos>()
                .ForMember(z => z.IdProyectoNavigation,
                opt => opt.Ignore());

            CreateMap<Indirectos, IndirectosDTO>();
            CreateMap<IndirectosDTO, Indirectos>()
                .ForMember(z => z.IdConjuntoIndirectosNavigation,
                opt => opt.Ignore());
            CreateMap<IndirectosXConcepto, IndirectosXConceptoDTO>();
            CreateMap<IndirectosXConceptoDTO, IndirectosXConcepto>()
                .ForMember(z => z.IdConceptoNavigation,
                opt => opt.Ignore());
            //////////////////////////////////////
            CreateMap<OperacionesXPrecioUnitarioDetalle, OperacionesXPrecioUnitarioDetalleDTO>();
            CreateMap<OperacionesXPrecioUnitarioDetalleDTO, OperacionesXPrecioUnitarioDetalle>()
                .ForMember(z => z.IdPrecioUnitarioDetalleNavigation,
                opt => opt.Ignore());
            #endregion

            #region ModuloInsumos

            #region FamiliaInsumo
            CreateMap<FamiliaInsumo, FamiliaInsumoDTO>();
            CreateMap<FamiliaInsumoCreacionDTO, FamiliaInsumo>();
            CreateMap<FamiliaInsumoDTO, FamiliaInsumo>();
            #endregion

            #region TipoInsumo
            CreateMap<TipoInsumo, TipoInsumoDTO>();
            CreateMap<TipoInsumoCreacionDTO, TipoInsumo>();
            CreateMap<TipoInsumoDTO, TipoInsumo>();
            #endregion

            #region Insumo
            CreateMap<Insumo, InsumoDTO>()
                .ForMember(z => z.DescripcionTipoInsumo,
                opt => opt.MapFrom(origen => origen.IdTipoInsumoNavigation == null ? "" : origen.IdTipoInsumoNavigation.Descripcion))
                .ForMember(z => z.DescripcionFamiliaInsumo,
                opt => opt.MapFrom(origen => origen.IdFamiliaInsumoNavigation == null ? "" : origen.IdFamiliaInsumoNavigation.Descripcion))
                .ForMember(destino => destino.EsAutorizado,
                opt => opt.MapFrom(origien => origien.EsAutorizado == null ? false : origien.EsAutorizado));
            CreateMap<InsumoCreacionDTO, Insumo>()
                .ForMember(z => z.IdFamiliaInsumoNavigation,
                opt => opt.Ignore())
                .ForMember(z => z.IdTipoInsumoNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.EsAutorizado,
                opt => opt.MapFrom(origien => origien.EsAutorizado == null ? false : origien.EsAutorizado));
            CreateMap<InsumoDTO, Insumo>()
                .ForMember(z => z.IdFamiliaInsumoNavigation,
                opt => opt.Ignore())
                .ForMember(z => z.IdTipoInsumoNavigation,
                opt => opt.Ignore());
            CreateMap<InsumoParaExplosionDTO, Insumo>();
            #endregion

            #region Concepto
            //////////////////////////
            CreateMap<Especialidad, EspecialidadDTO>();
            CreateMap<EspecialidadDTO, Especialidad>();
            //////////////////////////
            CreateMap<Concepto, ConceptoDTO>();
            CreateMap<ConceptoDTO, Concepto>();
            #endregion
            #endregion

            #region ModuloCompras
            #region Requisicion
            CreateMap<Requisicion, RequisicionDTO>();
            CreateMap<RequisicionDTO, Requisicion>()
                .ForMember(destino => destino.IdProyectoNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.InsumoXrequisicions,
                opt => opt.Ignore());
            CreateMap<RequisicionCreacionDTO, Requisicion>()
                .ForMember(destino => destino.IdProyectoNavigation,
                opt => opt.Ignore());
            #endregion

            #region InsumosXRequsicion
            CreateMap<InsumoXRequisicion, InsumoXRequisicionDTO>();
            CreateMap<InsumoXRequisicionDTO, InsumoXRequisicion>()
                .ForMember(destino => destino.IdInsumoNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdRequisicionNavigation,
                opt => opt.Ignore());
            CreateMap<InsumoXRequisicionCreacionDTO, InsumoXRequisicion>()
                .ForMember(destino => destino.IdInsumoNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdRequisicionNavigation,
                opt => opt.Ignore());
            #endregion

            #region Cotizacion
            CreateMap<Cotizacion, CotizacionDTO>()
                .ForMember(destino => destino.IdContratista,
                opt => opt.MapFrom(origen => origen.IdContratista == null? 0 : origen.IdContratista));
            CreateMap<CotizacionDTO, Cotizacion>()
                .ForMember(destino => destino.IdProyectoNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdRequisicionNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdContratistaNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.InsumoXcotizacions,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdContratista,
                opt => opt.MapFrom(origen => origen.IdContratista <= 0 ? null : origen.IdContratista));
            CreateMap<CotizacionCreacionDTO, Cotizacion>()
                .ForMember(destino => destino.IdProyectoNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdRequisicionNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdContratistaNavigation,
                opt => opt.Ignore());
            #endregion

            #region InsumosXCotizacion
            CreateMap<InsumoXCotizacion, InsumoXCotizacionDTO>()
                .ForMember(destino => destino.Codigo,
                opt => opt.MapFrom(origen => origen.IdInsumoNavigation!.Codigo))
                .ForMember(destino => destino.Descripcion,
                opt => opt.MapFrom(origen => origen.IdInsumoNavigation!.Descripcion))
                .ForMember(destino => destino.Unidad,
                opt => opt.MapFrom(origen => origen.IdInsumoNavigation!.Unidad));
            CreateMap<InsumoXCotizacionDTO, InsumoXCotizacion>()
                .ForMember(destino => destino.IdCotizacionNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdInsumoNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdInsumoRequisicionNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.InsumoXordenCompras,
                opt => opt.Ignore());
            CreateMap<InsumoXCotizacionCreacionDTO, InsumoXCotizacion>()
                .ForMember(destino => destino.IdCotizacionNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdInsumoNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdInsumoRequisicionNavigation,
                opt => opt.Ignore());
            #endregion

            #region OrdenCompra
            CreateMap<OrdenCompra, OrdenCompraDTO>();
            CreateMap<OrdenCompraDTO, OrdenCompra>()
                .ForMember(destino => destino.IdProyectoNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdRequisicionNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdContratistaNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdCotizacionNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.InsumoXordenCompras,
                opt => opt.Ignore());
            CreateMap<OrdenCompraCreacionDTO, OrdenCompra>()
                .ForMember(destino => destino.IdProyectoNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdRequisicionNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdContratistaNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdCotizacionNavigation,
                opt => opt.Ignore());
            #endregion

            #region InsumosXOrdenCompra
            CreateMap<InsumoXOrdenCompra, InsumoXOrdenCompraDTO>()
                .ForMember(destino => destino.Codigo,
                opt => opt.MapFrom(origen => origen.IdInsumoNavigation!.Codigo))
                .ForMember(destino => destino.Descripcion,
                opt => opt.MapFrom(origen => origen.IdInsumoNavigation!.Descripcion))
                .ForMember(destino => destino.Unidad,
                opt => opt.MapFrom(origen => origen.IdInsumoNavigation!.Unidad));
            CreateMap<InsumoXOrdenCompraDTO, InsumoXOrdenCompra>()
                .ForMember(destino => destino.IdInsumoXcotizacionNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdInsumoNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdOrdenCompraNavigation,
                opt => opt.Ignore());
            CreateMap<InsumoXOrdenCompraCreacionDTO, InsumoXOrdenCompra>()
                .ForMember(destino => destino.IdInsumoXcotizacionNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdInsumoNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdOrdenCompraNavigation,
                opt => opt.Ignore());
            #endregion

            #region CompraDirecta
            CreateMap<CompraDirecta, CompraDirectaDTO>()
                .ForMember(destino => destino.RazonSocialContratista,
                opt => opt.MapFrom(origen => origen.IdContratistaNavigation != null ? origen.IdContratistaNavigation.RazonSocial : ""));
            CreateMap<CompraDirectaDTO, CompraDirecta>()
                .ForMember(destino => destino.IdProyectoNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdRequisicionNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdContratistaNavigation,
                opt => opt.Ignore());
            CreateMap<CompraDirectaCreacionDTO, CompraDirecta>()
                .ForMember(destino => destino.IdProyectoNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdRequisicionNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdContratistaNavigation,
                opt => opt.Ignore());
            #endregion

            #region InsumosXCompraDirecta
            CreateMap<InsumoXCompraDirecta, InsumoXCompraDirectaDTO>()
                .ForMember(destino => destino.CodigoInsumo,
                opt => opt.MapFrom(origen => origen.IdInsumoNavigation!.Codigo))
                .ForMember(destino => destino.DescripcionInsumo,
                opt => opt.MapFrom(origen => origen.IdInsumoNavigation!.Descripcion))
                .ForMember(destino => destino.UnidadInsumo,
                opt => opt.MapFrom(origen => origen.IdInsumoNavigation!.Unidad));
            CreateMap<InsumoXCompraDirectaDTO, InsumoXCompraDirecta>()
                .ForMember(destino => destino.IdInsumoNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdCompraDirectaNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdInsumoRequisicionNavigation,
                opt => opt.Ignore());
            CreateMap<InsumoXCompraDirectaCreacionDTO, InsumoXCompraDirecta>()
                .ForMember(destino => destino.IdInsumoNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdCompraDirectaNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdInsumoRequisicionNavigation,
                opt => opt.Ignore());
            #endregion

            #region ImpuestosInsumoCotizado
            CreateMap<ImpuestoInsumoCotizado, ImpuestoInsumoCotizadoDTO>();
            CreateMap<ImpuestoInsumoCotizadoDTO, ImpuestoInsumoCotizado>()
                .ForMember(destino => destino.IdImpuestoNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdInsumoCotizadoNavigation,
                opt => opt.Ignore());
            CreateMap<ImpuestoInsumoCotizadoCreacionDTO, ImpuestoInsumoCotizado>()
                .ForMember(destino => destino.IdImpuestoNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdInsumoCotizadoNavigation,
                opt => opt.Ignore());

            #endregion

            #region ImpuestosInsumoOrdenCompra
            CreateMap<ImpuestoInsumoOrdenCompra, ImpuestoInsumoOrdenCompraDTO>();
            CreateMap<ImpuestoInsumoOrdenCompraDTO, ImpuestoInsumoOrdenCompra>()
                .ForMember(destino => destino.IdImpuestoNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdInsumoOrdenCompraNavigation,
                opt => opt.Ignore());
            CreateMap<ImpuestoInsumoOrdenCompraCreacionDTO, ImpuestoInsumoOrdenCompra>()
                .ForMember(destino => destino.IdImpuestoNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdInsumoOrdenCompraNavigation,
                opt => opt.Ignore());

            #endregion

            #endregion


            #region Almacen
            CreateMap<Almacen, AlmacenDTO>();
            CreateMap<AlmacenDTO, Almacen>()
                .ForMember(destino => destino.AlmacenEntrada,
                opt => opt.Ignore())
                .ForMember(destino => destino.AlmacenSalida,
                opt => opt.Ignore())
                .ForMember(destino => destino.InsumoExistencia,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdProyectoNavigation,
                opt => opt.Ignore());
            CreateMap<AlmacenCreacionDTO, Almacen>()
                .ForMember(destino => destino.IdProyectoNavigation,
                opt => opt.Ignore());
            #endregion

            #region AlmacenEntrada
            CreateMap<AlmacenEntrada, AlmacenEntradaDTO>();
            CreateMap<AlmacenEntradaDTO, AlmacenEntrada>()
                .ForMember(destino => destino.IdContratistaNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdAlmacenNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.AlmacenEntradaInsumos,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdContratista,
                opt => opt.MapFrom(origen => origen.IdContratista <= 0 ? null : origen.IdContratista));
            CreateMap<AlmacenEntradaCreacionDTO, AlmacenEntrada>()
                .ForMember(destino => destino.IdAlmacenNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdContratistaNavigation,
                opt => opt.Ignore());
            #endregion

            #region AlmacenEntradaInsumo
            CreateMap<AlmacenEntradaInsumo, AlmacenEntradaInsumoDTO>()
                .ForMember(destino => destino.NombreProyecto,
                opt => opt.MapFrom(origen => origen.IdProyectoNavigation != null ? origen.IdProyectoNavigation.Nombre : ""))
                .ForMember(destino => destino.CodigoInsumo,
                opt => opt.MapFrom(origen => origen.IdInsumoNavigation.Codigo))
                .ForMember(destino => destino.DescripcionInsumo,
                opt => opt.MapFrom(origen => origen.IdInsumoNavigation.Descripcion))
                .ForMember(destino => destino.UnidadInsumo,
                opt => opt.MapFrom(origen => origen.IdInsumoNavigation.Unidad))
                .ForMember(destino => destino.NoRequisicion,
                opt => opt.MapFrom(origen => origen.IdRequisicionNavigation != null ? origen.IdRequisicionNavigation.NoRequisicion : ""));
            CreateMap<AlmacenEntradaInsumoDTO, AlmacenEntradaInsumo>()
                .ForMember(destino => destino.IdAlmacenEntradaNavigation,
                opt => opt.Ignore())
                //.ForMember(destino => destino.IdCompraDirectaNavigation,
                //opt => opt.Ignore())
                .ForMember(destino => destino.IdInsumoNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdOrdenCompraNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdProyectoNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdRequisicionNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdRequisicion,
                opt => opt.MapFrom(origen => origen.IdRequisicion <= 0 ? null : origen.IdRequisicion))
                .ForMember(destino => destino.IdOrdenCompra,
                opt => opt.MapFrom(origen => origen.IdOrdenCompra <= 0 ? null : origen.IdOrdenCompra));
            CreateMap<AlmacenEntradaInsumoCreacionDTO, AlmacenEntradaInsumo>()
                .ForMember(destino => destino.IdAlmacenEntradaNavigation,
                opt => opt.Ignore())
                //.ForMember(destino => destino.IdCompraDirectaNavigation,
                //opt => opt.Ignore())
                .ForMember(destino => destino.IdInsumoNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdOrdenCompraNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdProyectoNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdRequisicionNavigation,
                opt => opt.Ignore());
            #endregion

            #region AlmacenSalida
            CreateMap<AlmacenSalida, AlmacenSalidaDTO>();
            CreateMap<AlmacenSalidaDTO, AlmacenSalida>()
                .ForMember(destino => destino.IdAlmacenNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.AlmacenSalidaInsumos,
                opt => opt.Ignore());
            CreateMap<AlmacenSalidaCreacionDTO, AlmacenSalida>()
                .ForMember(destino => destino.IdAlmacenNavigation,
                opt => opt.Ignore());
            #endregion

            #region AlmacenSalidaInsumo
            CreateMap<AlmacenSalidaInsumo, AlmacenSalidaInsumoDTO>()
                .ForMember(destino => destino.PrestamoFinalizado,
                opt => opt.MapFrom(origen => origen.PrestamoFinalizado == null ? false : origen.PrestamoFinalizado));
            CreateMap<AlmacenSalidaInsumoDTO, AlmacenSalidaInsumo>()
                .ForMember(destino => destino.IdAlmacenSalidaNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdInsumoNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdProyectoNavigation,
                opt => opt.Ignore());
            CreateMap<AlmacenSalidaInsumoCreacionDTO, AlmacenSalidaInsumo>()
                .ForMember(destino => destino.IdAlmacenSalidaNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdInsumoNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdProyectoNavigation,
                opt => opt.Ignore()); ;
            #endregion

            #region AlmacenExistenciaInsumo
            CreateMap<InsumoExistencia, AlmacenExistenciaInsumoDTO>()
                .ForMember(destino => destino.Codigo,
                opt => opt.MapFrom(origen => origen.IdInsumoNavigation.Codigo))
                .ForMember(destino => destino.Descripcion,
                opt => opt.MapFrom(origen => origen.IdInsumoNavigation.Descripcion))
                .ForMember(destino => destino.Unidad,
                opt => opt.MapFrom(origen => origen.IdInsumoNavigation.Unidad))
                .ForMember(destino => destino.AlmacenNombre,
                opt => opt.MapFrom(origen => origen.IdAlmacenNavigation.AlmacenNombre))
                .ForMember(destino => destino.EsCentral,
                opt => opt.MapFrom(origen => origen.IdAlmacenNavigation.Central));
            CreateMap<AlmacenExistenciaInsumoDTO, InsumoExistencia>()
                .ForMember(destino => destino.IdAlmacenNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdInsumoNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdProyectoNavigation,
                opt => opt.Ignore());
            CreateMap<AlmacenExistenciaInsumoCreacionDTO, InsumoExistencia>()
                .ForMember(destino => destino.IdAlmacenNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdInsumoNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdProyectoNavigation,
                opt => opt.Ignore());
            #endregion

            #region Banco
            CreateMap<Banco, BancoDTO>();
            CreateMap<BancoDTO, Banco>();
            #endregion

            #region CuentaBancaria
            CreateMap<CuentaBancaria, CuentaBancariaDTO>();
            CreateMap<CuentaBancariaCliente, CuentaBancariaClienteDTO>();
            CreateMap<CuentaBancariaEmpresa, CuentaBancariaEmpresasDTO>();
            CreateMap<CuentaBancariaDTO, CuentaBancaria>();
            CreateMap<CuentaBancariaClienteDTO, CuentaBancariaCliente>();
            CreateMap<CuentaBancariaEmpresasDTO, CuentaBancariaEmpresa>()
                .ForMember(destino => destino.IdBancoNavigation,
                opt => opt.Ignore());
            CreateMap<CuentaBancariaCreacionDTO, CuentaBancaria>()
                .ForMember(destino => destino.IdBancoNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdContratistaNavigation,
                opt => opt.Ignore());
            #endregion

            #region MovimientoBancario 
            CreateMap<MovimientoBancario, MovimientoBancarioTeckioDTO>();
            CreateMap<MovimientoBancarioTeckioDTO, MovimientoBancario>()
                .ForMember(destino => destino.IdPoliza,
                opt => opt.MapFrom(origen => origen.IdPoliza <= 0 ? null : origen.IdPoliza))
                .ForMember(destino => destino.IdFactura,
                opt => opt.MapFrom(origen => origen.IdFactura <= 0 ? null : origen.IdFactura));
            #endregion
            #region MovimientoBancarioBeneficiario
            CreateMap<MovimientoBancarioContratista, MBancarioBeneficiarioDTO>()
                .ForMember(destino => destino.IdBeneficiario,
                opt => opt.MapFrom(origen => origen.IdContratista));
            CreateMap<MBancarioBeneficiarioDTO, MovimientoBancarioContratista>()
                .ForMember(destino => destino.IdContratista,
                opt => opt.MapFrom(origen => origen.IdBeneficiario));
            CreateMap<MovimientoBancarioCliente, MBancarioBeneficiarioDTO>()
                .ForMember(destino => destino.IdBeneficiario,
                opt => opt.MapFrom(origen => origen.IdCliente));
            CreateMap<MBancarioBeneficiarioDTO, MovimientoBancarioCliente>()
                .ForMember(destino => destino.IdCliente,
                opt => opt.MapFrom(origen => origen.IdBeneficiario));
            CreateMap<MovimientoBancarioEmpresa, MBancarioBeneficiarioDTO>();
            CreateMap<MBancarioBeneficiarioDTO, MovimientoBancarioEmpresa>();
            #endregion
            #region OrdenCompraXMovimientoBancario
            CreateMap<OrdenCompraXMovimientoBancario, OrdenCompraXMovimientoBancarioDTO>();
            CreateMap<OrdenCompraXMovimientoBancarioDTO, OrdenCompraXMovimientoBancario>();
            #endregion
            #region FacturaXOrdenCompraXMovimientoBancario
            CreateMap<FacturaXOrdenCompraXMovimientoBancario, FacturaXOrdenCompraXMovimientoBancarioDTO>();
            CreateMap<FacturaXOrdenCompraXMovimientoBancarioDTO, FacturaXOrdenCompraXMovimientoBancario>();
            #endregion

            #region MovimientoBancarioSaldo
            CreateMap<MovimientoBancarioSaldoDTO, MovimientoBancarioSaldo>();
            CreateMap<MovimientoBancarioSaldo, MovimientoBancarioSaldoDTO>();
            #endregion

            #region CodigoAgrupadorSat
            CreateMap<CodigoAgrupadorSat, CodigoAgrupadorSatDTO>();
            CreateMap<CodigoAgrupadorSatDTO, CodigoAgrupadorSat>();
            CreateMap<CodigoAgrupadorSatCreacionDTO, CodigoAgrupadorSat>();
            #endregion

            #region Contabilidad
            CreateMap<Rubro, RubroDTO>();
            CreateMap<RubroDTO, Rubro>();
            CreateMap<RubroCreacionDTO, Rubro>();
            ///////////////////////////////////////////////////////////////
            CreateMap<TipoPoliza, TipoPolizaDTO>();
            CreateMap<TipoPolizaDTO, TipoPoliza>();
            CreateMap<TipoPolizaCreacionDTO, TipoPoliza>();
            ///////////////////////////////////////////////////////////////
            CreateMap<Rubro, RubroDTO>();
            CreateMap<RubroDTO, Rubro>();
            CreateMap<RubroCreacionDTO, Rubro>();
            ///////////////////////////////////////////////////////////////
            CreateMap<TipoPoliza, TipoPolizaDTO>();
            CreateMap<TipoPolizaDTO, TipoPoliza>();
            CreateMap<TipoPolizaCreacionDTO, TipoPoliza>();
            ///////////////////////////////////////////////////////////////
            CreateMap<CuentaContable, CuentaContableDTO>()
                .ForMember(detino => detino.TipoCuentaContable, 
                opt => opt.MapFrom(origen => origen.TipoCuentaContable == null ? 0 : origen.TipoCuentaContable));
            CreateMap<CuentaContableDTO, CuentaContable>()
                .ForMember(destino => destino.IdRubroNavigation,
                opt => opt.Ignore());
            CreateMap<CuentaContableCreacionDTO, CuentaContable>()
                .ForMember(destino => destino.IdRubroNavigation,
                opt => opt.Ignore());
            ///////////////////////////////////////////////////////////////
            CreateMap<Clientes, ClienteDTO>();
            CreateMap<ClienteDTO, Clientes>()
                .ForMember(destino => destino.IdCuentaContableNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdIvaTrasladadoNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdIvaPorTrasladarNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdCuentaAnticiposNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdIvaExentoNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdIvaGravableNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdRetensionIsrNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdIepsNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdIvaRetenidoNavigation,
                opt => opt.Ignore());
            CreateMap<ClienteDTO, ClienteDTO>()
                .ForMember(destino => destino.IdCuentaContable,
                opt => opt.MapFrom(origen => origen.IdCuentaContable <= 0 ? null : origen.IdCuentaContable))
                .ForMember(destino => destino.IdIvaTrasladado,
                opt => opt.MapFrom(origen => origen.IdIvaTrasladado <= 0 ? null : origen.IdIvaTrasladado))
                .ForMember(destino => destino.IdIvaPorTrasladar,
                opt => opt.MapFrom(origen => origen.IdIvaPorTrasladar <= 0 ? null : origen.IdIvaPorTrasladar))
                .ForMember(destino => destino.IdCuentaAnticipos,
                opt => opt.MapFrom(origen => origen.IdCuentaAnticipos <= 0 ? null : origen.IdCuentaAnticipos))
                .ForMember(destino => destino.IdIvaExento,
                opt => opt.MapFrom(origen => origen.IdIvaExento <= 0 ? null : origen.IdIvaExento))
                .ForMember(destino => destino.IdIvaGravable,
                opt => opt.MapFrom(origen => origen.IdIvaGravable <= 0 ? null : origen.IdIvaGravable))
                .ForMember(destino => destino.IdRetensionIsr,
                opt => opt.MapFrom(origen => origen.IdRetensionIsr <= 0 ? null : origen.IdRetensionIsr))
                .ForMember(destino => destino.IdIeps,
                opt => opt.MapFrom(origen => origen.IdIeps <= 0 ? null : origen.IdIeps))
                .ForMember(destino => destino.IdIvaRetenido,
                opt => opt.MapFrom(origen => origen.IdIvaRetenido <= 0 ? null : origen.IdIvaRetenido));
            ///////////////////////////////////////////////////////////////
            CreateMap<Poliza, PolizaDTO>();
            CreateMap<PolizaDTO, Poliza>()
                .ForMember(destino => destino.IdTipoPolizaNavigation,
                opt => opt.Ignore());
            ///////////////////////////////////////////////////////////////
            CreateMap<PolizaDetalle, PolizaDetalleDTO>();
            CreateMap<PolizaDetalleDTO, PolizaDetalle>()
                .ForMember(destino => destino.IdPolizaNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.IdCuentaContableNavigation,
                opt => opt.Ignore());
            ///////////////////////////////////////////////////////////////
            CreateMap<SaldosBalanzaComprobacion, SaldosBalanzaComprobacionDTO>();
            CreateMap<SaldosBalanzaComprobacionDTO, SaldosBalanzaComprobacion>()
                .ForMember(destino => destino.IdCuentaContableNavigation,
                opt => opt.Ignore());
            ///////////////////////////////////////////////////////////////
            CreateMap<Contratista, ContratistaDTO>();
            CreateMap<ContratistaDTO, Contratista>();
            //CreateMap<ContratistaDTO, Contratista>()
            //    .ForMember(destino => destino.Tipo,
            //    opt => opt.Ignore())
            //    .ForMember(destino => destino.RepresentanteLegal,
            //    opt => opt.Ignore())
            //    .ForMember(destino => destino.TelefonoRepresentanteLegal,
            //    opt => opt.Ignore())
            //    .ForMember(destino => destino.EmailRepresentanteLegal,
            //    opt => opt.Ignore())
            //    .ForMember(destino => destino.Domicilio,
            //    opt => opt.Ignore())
            //    .ForMember(destino => destino.Nexterior,
            //    opt => opt.Ignore())
            //    .ForMember(destino => destino.Colonia,
            //    opt => opt.Ignore())
            //    .ForMember(destino => destino.Municipio,
            //    opt => opt.Ignore())
            //    .ForMember(destino => destino.CodigoPostal,
            //    opt => opt.Ignore())
            //    .ForMember(destino => destino.IdCuentaAnticiposNavigation,
            //    opt => opt.Ignore())
            //    .ForMember(destino => destino.IdCuentaContableNavigation,
            //    opt => opt.Ignore())
            //    .ForMember(destino => destino.IdCuentaContableProveedorNavigation,
            //    opt => opt.Ignore())
            //    .ForMember(destino => destino.IdIvaTrasladadoNavigation,
            //    opt => opt.Ignore())
            //    .ForMember(destino => destino.IdIvaPorTrasladarNavigation,
            //    opt => opt.Ignore())
            //    .ForMember(destino => destino.IdIvaExentoNavigation,
            //    opt => opt.Ignore())
            //    .ForMember(destino => destino.IdIvaGravableNavigation,
            //    opt => opt.Ignore())
            //    .ForMember(destino => destino.IdRetensionIsrNavigation,
            //    opt => opt.Ignore())
            //    .ForMember(destino => destino.IdIepsNavigation,
            //    opt => opt.Ignore())
            //    .ForMember(destino => destino.IdIvaRetenidoNavigation,
            //    opt => opt.Ignore());
            #endregion

            #region TipoImpuesto
            CreateMap<Impuesto, ImpuestoDTO>();
            CreateMap<ImpuestoDTO, Impuesto>();
            #endregion
            CreateMap<ProgramacionEstimadaGantt, ProgramacionEstimadaGanttDTO>()
                .ForMember(destino => destino.Id,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Id)))
                .ForMember(destino => destino.Start,
                opt => opt.MapFrom(origen => origen.FechaInicio))
                .ForMember(destino => destino.End,
                opt => opt.MapFrom(origen => origen.FechaTermino))
                .ForMember(destino => destino.Parent,
                opt => opt.MapFrom(origen => origen.IdPadre));
            CreateMap<ProgramacionEstimadaGanttDTO, ProgramacionEstimadaGantt>()
                .ForMember(z => z.IdConceptoNavigation,
                opt => opt.Ignore())
                .ForMember(z => z.IdPrecioUnitarioNavigation,
                opt => opt.Ignore())
                .ForMember(z => z.IdProyectoNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.Id,
                opt => opt.MapFrom(origen => Convert.ToInt32(origen.Id)))
                .ForMember(destino => destino.FechaInicio,
                opt => opt.MapFrom(origen => origen.Start))
                .ForMember(destino => destino.FechaTermino,
                opt => opt.MapFrom(origen => origen.End))
                .ForMember(destino => destino.IdPadre,
                opt => opt.MapFrom(origen => origen.Parent))
                .ForMember(destino => destino.Progreso,
                opt => opt.MapFrom(origen => origen.Progress));
            CreateMap<ProgramacionEstimadaGanttDeserealizadaDTO, ProgramacionEstimadaGanttDTO>()
                .ForMember(destino => destino.Id,
                opt => opt.MapFrom(origen => Convert.ToString(origen.Id)))
                .ForMember(destino => destino.Start,
                opt => opt.MapFrom(origen => origen.FechaInicio))
                .ForMember(destino => destino.End,
                opt => opt.MapFrom(origen => origen.FechaTermino))
                .ForMember(destino => destino.Parent,
                opt => opt.MapFrom(origen => origen.IdPadre));

            CreateMap<Empleado, EmpleadoDTO>();
            CreateMap<EmpleadoDTO, Empleado>();

            CreateMap<CostoHorarioFijoXPrecioUnitarioDetalle, CostoHorarioFijoXPrecioUnitarioDetalleDTO>();
            CreateMap<CostoHorarioFijoXPrecioUnitarioDetalleDTO, CostoHorarioFijoXPrecioUnitarioDetalle>()
                .ForMember(z => z.IdPrecioUnitarioDetalleNavigation,
                opt => opt.Ignore());

            CreateMap<CostoHorarioVariableXPrecioUnitarioDetalle, CostoHorarioVariableXPrecioUnitarioDetalleDTO>();
            CreateMap<CostoHorarioVariableXPrecioUnitarioDetalleDTO, CostoHorarioVariableXPrecioUnitarioDetalle>()
                .ForMember(z => z.IdInsumoNavigation,
                opt => opt.Ignore())
                .ForMember(z => z.IdPrecioUnitarioDetalleNavigation,
                opt => opt.Ignore());


            CreateMap<PrecioUnitarioXEmpleado, PrecioUnitarioXEmpleadoDTO>();
            CreateMap<PrecioUnitarioXEmpleadoDTO, PrecioUnitarioXEmpleado>();
            CreateMap<DependenciaProgramacionEstimadaDTO, DependenciaProgramacionEstimada>()
                .ForMember(z => z.IdProyectoNavigation,
                opt => opt.Ignore())
                .ForMember(z => z.IdProgramacionEstimadaGanttNavigation,
                opt => opt.Ignore())
                .ForMember(z => z.IdProgramacionEstimadaGanttPredecesoraNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.Id,
                opt => opt.MapFrom(origen => Convert.ToInt32(origen.Id)))
                .ForMember(destino => destino.IdProgramacionEstimadaGantt,
                opt => opt.MapFrom(origen => Convert.ToInt32(origen.IdProgramacionEstimadaGantt)))
                .ForMember(destino => destino.IdProyecto,
                opt => opt.MapFrom(origen => Convert.ToInt32(origen.IdProyecto)))
                .ForMember(destino => destino.IdProgramacionEstimadaGanttPredecesora,
                opt => opt.MapFrom(origen => Convert.ToInt32(origen.SourceId)))
                ;
            CreateMap<DependenciaProgramacionEstimada, DependenciaProgramacionEstimadaDTO>()
                .ForMember(destino => destino.SourceId,
                opt => opt.MapFrom(origen => Convert.ToInt32(origen.IdProgramacionEstimadaGanttPredecesora)));
            CreateMap<DependenciaProgramacionEstimadaDeserealizadaDTO, DependenciaProgramacionEstimadaDTO>()
                .ForMember(destino => destino.SourceId,
                opt => opt.MapFrom(origen => Convert.ToString(origen.IdProgramacionEstimadaGanttPredecesora)));

            #region OrdenVenta
            CreateMap<OrdenVentaDTO, OrdenVentum>();
            CreateMap<OrdenVentum, OrdenVentaDTO>();
            #endregion
            #region DetalleOrdenVenta
            CreateMap<DetalleOrdenVentaDTO, DetalleOrdenVentum>();
            CreateMap<DetalleOrdenVentum, DetalleOrdenVentaDTO>();
            #endregion
            #region ImpuestoDetalleOrdenVenta
            CreateMap<ImpuestoDetalleOrdenVentaDTO, ImpuestoDetalleOrdenVentum>();
            CreateMap<ImpuestoDetalleOrdenVentum, ImpuestoDetalleOrdenVentaDTO>();
            #endregion

            #endregion
            #region Facturas

            #region Facturas
            CreateMap<Factura, FacturaDTO>()
                .ForMember(destino => destino.IdFormaPago,
                opt => opt.MapFrom(origien => origien.IdFormaPago == null ? 0 : origien.IdFormaPago))
                .ForMember(destino => destino.IdArchivo,
                opt => opt.MapFrom(origien => origien.IdArchivo == null ? 0 : origien.IdArchivo))
                .ForMember(destino => destino.EstatusEnviadoCentroCostos,
                opt => opt.MapFrom(origien => origien.EstatusEnviadoCentroCostos == null ? false : origien.EstatusEnviadoCentroCostos));
            CreateMap<FacturaDTO, Factura>();

           // CreateMap<Factura, FacturaBaseDTO>()
           //     .ForMember(destino => destino.MetodoPago,
           //     opt => opt.MapFrom(origen => origen.MetodoPago == null ? "" : origen.MetodoPago))
           //     .ForMember(destino => destino.FormaPago,
           //     opt => opt.MapFrom(origen => origen.FormaPago == null ? "" : origen.FormaPago));
           // CreateMap<FacturaBaseDTO, Factura>()
           //     .ForMember(destino => destino.MetodoPago,
           //     opt => opt.MapFrom(origen => string.IsNullOrEmpty(origen.MetodoPago) ? "" : origen.MetodoPago))
           //     .ForMember(destino => destino.FormaPago,
           //     opt => opt.MapFrom(origen => string.IsNullOrEmpty(origen.FormaPago) ? "" : origen.FormaPago))
           //.ForMember(destino => destino.IdArchivoNavigation,
           //     opt => opt.Ignore())
           //.ForMember(destino => destino.IdArchivoPdfNavigation,
           //     opt => opt.Ignore());
            #endregion
            #region FacturaDetalles
            CreateMap<FacturaDetalle, FacturaDetalleDTO>();
            CreateMap<FacturaDetalleDTO, FacturaDetalle>()
           .ForMember(destino => destino.IdFacturaNavigation,
                opt => opt.Ignore());
            #endregion
            #region MonedaSat
            CreateMap<MonedaSat, MonedaSatDTO>();
            CreateMap<MonedaSatDTO, MonedaSat>();
            #endregion
            #region FormaPagoSat
            CreateMap<FormaPagoSat, FormaPagoSatDTO>();
            CreateMap<FormaPagoSatDTO, FormaPagoSat>();
            #endregion
            #region RegimenFiscalSat
            CreateMap<RegimenFiscalSat, RegimenFiscalSatDTO>();
            CreateMap<RegimenFiscalSatDTO, RegimenFiscalSat>();
            #endregion
            #region UnidadSat
            CreateMap<UnidadSat, UnidadSatDTO>();
            CreateMap<UnidadSatDTO, UnidadSat>();
            #endregion
            #region Unidad
            CreateMap<Unidad, UnidadDTO>();
            CreateMap<UnidadDTO, Unidad>();
            #endregion
            #region ProductoYServicioSat
            CreateMap<ProductoYservicioSat, ProductoYServicioSatDTO>();
            CreateMap<ProductoYServicioSatDTO, ProductoYservicioSat>();
            #endregion
            #region ProductoYServicio
            CreateMap<ProductoYservicio, ProductoYservicioDTO>();
            CreateMap<ProductoYservicioDTO, ProductoYservicio>();
            #endregion
            #region CategoriaProductoYServicio
            CreateMap<CategoriaProductoYServicio, CategoriaProductoYServicioDTO>();
            CreateMap<CategoriaProductoYServicioDTO, CategoriaProductoYServicio>();
            #endregion
            #region SubcategoriaProductoYServicio
            CreateMap<SubcategoriaProductoYServicio, SubcategoriaProductoYServicioDTO>();
            CreateMap<SubcategoriaProductoYServicioDTO, SubcategoriaProductoYServicio>();
            #endregion
            #region UsoCfdiSat
            CreateMap<UsoCfdiSat, UsoCfdiSatDTO>();
            CreateMap<UsoCfdiSatDTO, UsoCfdiSat>();
            #endregion
            #region AcuseValidacion
            CreateMap<AcuseValidacion, AcuseValidacionDTO>();
            CreateMap<AcuseValidacionDTO, AcuseValidacion>()
           .ForMember(destino => destino.IdFacturaNavigation,
                opt => opt.Ignore());
            #endregion
            #region CatalogoValidacion
            //CreateMap<CatalogoValidacion, CatalogoValidacionDTO>();
            //CreateMap<CatalogoValidacionDTO, CatalogoValidacion>();
            #endregion
            #region AcuseDetalleValidacion
            CreateMap<DetalleValidacion, DetalleValidacionDTO>();
            CreateMap<DetalleValidacionDTO, DetalleValidacion>()
           .ForMember(destino => destino.IdAcuseValidacionNavigation,
                opt => opt.Ignore());
            #endregion
            #region DocumentoContable
            // CreateMap<DocumentoContable, DocumentoContableDTO>();
            // CreateMap<DocumentoContable, DocumentoContable>()
            //.ForMember(destino => destino.IdFacturaNavigation,
            //     opt => opt.Ignore());
            #endregion
            #region ClasificacionImpuesto
            CreateMap<ClasificacionImpuesto, ClasificacionImpuestoDTO>();
            CreateMap<ClasificacionImpuestoDTO, ClasificacionImpuesto>();
            #endregion
            #region FacturaImpuestos
            CreateMap<FacturaImpuesto, FacturaImpuestosDTO>();
            CreateMap<FacturaImpuestosDTO, FacturaImpuesto>()
           .ForMember(destino => destino.IdCategoriaImpuestoNavigation,
                opt => opt.Ignore())
           .ForMember(destino => destino.IdClasificacionImpuestoNavigation,
                opt => opt.Ignore())
           .ForMember(destino => destino.IdFacturaNavigation,
                opt => opt.Ignore());
            #endregion
            #region FacturaImpuestosLocales
            CreateMap<FacturaImpuestosLocal, FacturaImpuestosLocalesDTO>();
            CreateMap<FacturaImpuestosLocalesDTO, FacturaImpuestosLocal>()
           .ForMember(destino => destino.IdCategoriaImpuestoNavigation,
                opt => opt.Ignore())
           .ForMember(destino => destino.IdClasificacionImpuestoNavigation,
                opt => opt.Ignore())
           .ForMember(destino => destino.IdFacturaNavigation,
                opt => opt.Ignore());
            #endregion
            #region FacturaDetalleImpuestos
            CreateMap<FacturaDetalleImpuesto, FacturaDetalleImpuestoDTO>();
            CreateMap<FacturaDetalleImpuestoDTO, FacturaDetalleImpuesto>()
           .ForMember(destino => destino.IdClasificacionImpuestoNavigation,
                opt => opt.Ignore())
           .ForMember(destino => destino.IdFacturaDetalleNavigation,
                opt => opt.Ignore())
           .ForMember(destino => destino.IdTipoFactorNavigation,
                opt => opt.Ignore())
           .ForMember(destino => destino.IdTipoImpuestoNavigation,
                opt => opt.Ignore());
            #endregion
            #region FacturaComplementoPago
            CreateMap<FacturaComplementoPago, FacturaComplementoPagoDTO>();
            CreateMap<FacturaComplementoPagoDTO, FacturaComplementoPago>()
           .ForMember(destino => destino.IdFacturaNavigation,
                opt => opt.Ignore());
            #endregion
            #region CategoriaImpuesto
            CreateMap<CategoriaImpuesto, CategoriaImpuestoDTO>();
            CreateMap<CategoriaImpuestoDTO, CategoriaImpuesto>();
            #endregion
            #region Archivo
            CreateMap<Archivo, ArchivoDTO>();
            CreateMap<ArchivoDTO, Archivo>();
            #endregion
            #region TipoImpuesto
            CreateMap<TipoImpuesto, TipoImpuestoDTO>();
            CreateMap<TipoImpuestoDTO, TipoImpuesto>();
            #endregion
            #region TipoFactor
            CreateMap<TipoFactor, TipoFactorDTO>();
            CreateMap<TipoFactorDTO, TipoFactor>();
            #endregion
            #region ArchivoDocumento
            // CreateMap<ArchivoDocumento, ArchivoDocumentoDTO>();
            // CreateMap<ArchivoDocumentoDTO, ArchivoDocumento>()
            //.ForMember(destino => destino.Documentos,
            //     opt => opt.Ignore());
            #endregion
            #region TipoDocumento
            //CreateMap<TipoDocumento, TipoDocumentoDTO>();
            //CreateMap<TipoDocumentoDTO, TipoDocumento>();
            #endregion
            #region Documento
            // CreateMap<Documento, DocumentoDTO>();
            // CreateMap<DocumentoDTO, Documento>()
            //.ForMember(destino => destino.IdArchivoNavigation,
            //     opt => opt.Ignore())
            //.ForMember(destino => destino.IdTipoDocumentoNavigation,
            //     opt => opt.Ignore());
            #endregion
            #region FacturaXOrdenCompra
            CreateMap<FacturaXOrdenCompraDTO, FacturaXOrdenCompra>();
            CreateMap<FacturaXOrdenCompra, FacturaXOrdenCompraDTO>()
                .ForMember(destino => destino.TotalSaldado, 
                opt => opt.MapFrom(origen => origen.TotalSaldado == null ? 0 : origen.TotalSaldado ));
            #endregion
            #region FacturaEntradaMaterial
            // CreateMap<FacturaEntradaMaterial, FacturaEntradaMaterialDTO>();
            // CreateMap<FacturaEntradaMaterialDTO, FacturaEntradaMaterial>()
            //.ForMember(destino => destino.IdEntradaMaterialNavigation,
            //     opt => opt.Ignore())
            //.ForMember(destino => destino.IdFacturaNavigation,
            //     opt => opt.Ignore());
            #endregion

            #region AcuseCfdi
            // CreateMap<AcuseCfdi, AcuseCfdiDTO>();
            // CreateMap<AcuseCfdiDTO, AcuseCfdi>()
            //.ForMember(destino => destino.IdArchivoNavigation,
            //     opt => opt.Ignore())
            //.ForMember(destino => destino.IdArchivoPdfNavigation,
            //     opt => opt.Ignore());
            #endregion
            #region EntradasAutorizacionDTO
            //CreateMap<EntradasAutorizacion, EntradasAutorizacionDTO>();
            //CreateMap<EntradasAutorizacionDTO, EntradasAutorizacion>();
            #endregion
            #region CatalogoEstatusEntradasAutorizacionDTO
            //CreateMap<CatalogoEstatusEntradasAutorizacion, CatalogoEstatusEntradasAutorizacionDTO>();
            //CreateMap<CatalogoEstatusEntradasAutorizacionDTO, CatalogoEstatusEntradasAutorizacion>();
            #endregion
            #region CuentaContableProveedores
            //CreateMap<CuentaContableSAP, CuentaContableProveedoresDTO>();
            //CreateMap<CuentaContableProveedoresDTO, CuentaContableSAP>();

            #region movimiento
            //CreateMap<MovimientosCuentaContable, MovimientosCuentaContableDTO>();
            //CreateMap<MovimientosCuentaContableDTO, MovimientosCuentaContable>()
            //    .ForMember(archivo => archivo.idCuentaContableNavigation,
            //    opt => opt.Ignore());
            #endregion

            #endregion
            #region PagoProveedores
            //CreateMap<Pago, PagoDTO>();
            //CreateMap<PagoDTO, Pago>();
            #endregion
            #region PagoDetalleProveedores
            // CreateMap<PagoDetalle, PagoDetalleDTO>();
            // CreateMap<PagoDetalleDTO, PagoDetalle>()
            //.ForMember(destino => destino.IdPagoNavigation,
            //     opt => opt.Ignore());
            #endregion
            #region FacturaCentroCostos
            // CreateMap<FacturaDetalleDTO, FacturaDetalleCentroCostosDTO>()
            //.ForMember(destino => destino.Tipo,
            //     opt => opt.MapFrom(origen => origen.IdFactura > 0 ? 1 : 0))
            //.ForMember(destino => destino.CuentaContable,
            //     opt => opt.MapFrom(origen => origen.IdFactura > 0 ? "" : ""));
            #endregion
            #region PolizaProveedores
            CreateMap<PolizaProveedores, PolizaProveedoresDTO>();
            CreateMap<PolizaProveedoresDTO, PolizaProveedores>()
           .ForMember(destino => destino.IdTipoPolizaNavigation,
                opt => opt.Ignore());
            #endregion
            #region PolizaLayoutSinPedido
            //CreateMap<PolizaLayoutSinPedido, PolizaLayoutSinPedidoDTO>();
            //CreateMap<PolizaLayoutSinPedidoDTO, PolizaLayoutSinPedido>();
            #endregion
            #region PolizaLayoutOrdenCompra
            //CreateMap<PolizaSp, PolizaSpDTO>();
            //CreateMap<PolizaSpDTO, PolizaSp>()
            //.ForMember(destino => destino.IdPolizaNavigation,
            //    opt => opt.Ignore());

            #endregion
            #region PolizaOc
            // CreateMap<PolizaOc, PolizaOcDTO>();
            // CreateMap<PolizaOcDTO, PolizaOc>()
            //.ForMember(destino => destino.IdPolizaNavigation,
            //     opt => opt.Ignore());
            #endregion
            #region PolizaOcDetalle
            // CreateMap<PolizaOcdetalle, PolizaOcdetalleDTO>();
            // CreateMap<PolizaOcdetalleDTO, PolizaOcdetalle>()
            //.ForMember(destino => destino.IdPolizaOcNavigation,
            //     opt => opt.Ignore());
            #endregion
            #region PolizaSp
            // CreateMap<PolizaSp, PolizaSpDTO>();
            // CreateMap<PolizaSpDTO, PolizaSp>()
            //.ForMember(destino => destino.IdPolizaNavigation,
            //     opt => opt.Ignore()); ;

            // CreateMap<PolizaSpDetalle, PolizaSpDetalleDTO>();
            // CreateMap<PolizaSpDetalleDTO, PolizaSpDetalle>()
            //.ForMember(destino => destino.IdCentroCostosNavigation,
            //     opt => opt.Ignore())
            //.ForMember(destino => destino.IdConceptoNavigation,
            //     opt => opt.Ignore())
            //.ForMember(destino => destino.IdCuentaContableNavigation,
            //     opt => opt.Ignore())
            //.ForMember(destino => destino.IdPolizaSpNavigation,
            //     opt => opt.Ignore())
            //.ForMember(destino => destino.IdOrdenInternaNavigation,
            //     opt => opt.Ignore());
            #endregion
            #region InformacionAdicional
            // CreateMap<InformacionAdicional, InformacionAdicionalDTO>()
            //.ForMember(destino => destino.TipoOcString,
            //     opt => opt.Ignore());
            // CreateMap<InformacionAdicionalDTO, InformacionAdicional>()
            //.ForMember(destino => destino.IdOrdenCompraNavigation,
            //     opt => opt.Ignore());

            #endregion
            #region FacturaEmisor
            CreateMap<FacturaEmisor, FacturaEmisorDTO>();
            CreateMap<FacturaEmisorDTO, FacturaEmisor>()
           .ForMember(destino => destino.IdFacturaNavigation,
                opt => opt.Ignore());

            #endregion
            #region FacturaReceptor
            // CreateMap<FacturaReceptor, FacturaReceptorDTO>();
            // CreateMap<FacturaReceptorDTO, FacturaReceptor>()
            //.ForMember(destino => destino.IdFacturaNavigation,
            //     opt => opt.Ignore()); ;

            #endregion
            #region InsumoXProductoYServicio

            CreateMap<InsumoxProductoYservicio, InsumoXProductoYServicioDTO>();
            CreateMap<InsumoXProductoYServicioDTO, InsumoxProductoYservicio>()
                .ForMember(destino => destino.IdProductoYservicioNavigation, opt => opt.Ignore())
                .ForMember(destino => destino.IdInsumoNavigation, opt => opt.Ignore());

            #endregion
            #region Produccion

            CreateMap<Produccion, ProduccionDTO>();
            CreateMap<ProduccionDTO, Produccion>()
                .ForMember(destino => destino.IdProductoYservicioNavigation, opt => opt.Ignore())
                .ForMember(destino => destino.InsumoxProduccion, opt => opt.Ignore());
            CreateMap<ProduccionDTO, ProduccionConAlmacenDTO>();
            CreateMap<ProduccionConAlmacenDTO, ProduccionDTO>();



            #endregion

            #endregion

        }
    }
}