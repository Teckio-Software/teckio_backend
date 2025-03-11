using Microsoft.EntityFrameworkCore;


using Microsoft.AspNetCore.Mvc;
using ERP_TECKIO;
using ERP_TECKIO.Servicios.Contratos;
using ERP_TECKIO.DTO;
using DocumentFormat.OpenXml.Office2010.Excel;
using ERP_TECKIO.Modelos;

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

        private readonly IFsrxinsummoMdOService<TContext> _FsrxinsummoMdOService;
        private readonly IFsrxinsummoMdOdetalleService<TContext> _FsrxinsummoMdOdetalleService;
        private readonly IFsixinsummoMdOService<TContext> _FsixinsummoMdOService;
        private readonly IFsixinsummoMdOdetalleService<TContext> _FsixinsummoMdOdetalleService;

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


            , IFsrxinsummoMdOService<TContext> FsrxinsummoMdOService
            , IFsrxinsummoMdOdetalleService<TContext> FsrxinsummoMdOdetalleService
            , IFsixinsummoMdOService<TContext> FsixinsummoMdOService
            , IFsixinsummoMdOdetalleService<TContext> FsixinsummoMdOdetalleService
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

            _FsrxinsummoMdOService = FsrxinsummoMdOService;
            _FsrxinsummoMdOdetalleService = FsrxinsummoMdOdetalleService;
            _FsixinsummoMdOService = FsixinsummoMdOService;
            _FsixinsummoMdOdetalleService = FsixinsummoMdOdetalleService;
        }

        public async Task<RespuestaDTO> CrearFsrDetalle(FsrxinsummoMdOdetalleDTO fsrdetalle) { 
            var respuesta = new RespuestaDTO();
            var Fsr = new FsrxinsummoMdODTO();
            Fsr = await _FsrxinsummoMdOService.ObtenerXIdInsumo(fsrdetalle.Id);
            if (Fsr.Id <= 0) {
                var insumo = await _InsumoService.ObtenXId(fsrdetalle.IdInsumo);
                var nuevoFsr = new FsrxinsummoMdODTO();
                nuevoFsr.Id = 0;
                nuevoFsr.CostoDirecto = insumo.CostoBase;
                nuevoFsr.CostoFinal = 0;
                nuevoFsr.Fsr = 1;
                nuevoFsr.IdInsumo = fsrdetalle.IdInsumo;
                nuevoFsr.IdProyecto = fsrdetalle.IdProyceto;
                Fsr = await _FsrxinsummoMdOService.CrearYObtener(nuevoFsr);
            }

            fsrdetalle.IdFsrxinsummoMdO = Fsr.Id;
            var nuevoFsrDetralle = await _FsrxinsummoMdOdetalleService.Crear(fsrdetalle);
            if (!nuevoFsrDetralle)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No guardó el registro";
                return respuesta;
            }

            var editar = await EditarFsrXInsumo(Fsr.IdInsumo);
            if (!editar) { 
                respuesta.Estatus = true;
                respuesta.Descripcion = "No se editó el FSR";
            }

            respuesta.Estatus = true;
            respuesta.Descripcion = "Se editó el FSR";
            return respuesta;
        }

        public async Task<RespuestaDTO> CrearFsiDetalle(FsixinsummoMdOdetalleDTO fsidetalle) {
            var respuesta = new RespuestaDTO();
            var Fsi = await _FsixinsummoMdOService.ObtenerXIdInsumo(fsidetalle.IdInsumo);
            if (Fsi.Id <= 0)
            {
                var nuevoFsi = new FsixinsummoMdODTO();
                nuevoFsi.Id = 0;
                nuevoFsi.DiasNoLaborales = 0;
                nuevoFsi.DiasPagados = 0;
                nuevoFsi.Fsi = 0;
                nuevoFsi.IdInsumo = fsidetalle.IdInsumo;
                nuevoFsi.IdProyecto = fsidetalle.IdProyecto;
                Fsi = await _FsixinsummoMdOService.CrearYObtener(nuevoFsi);
            }

            fsidetalle.IdFsixinsummoMdO = Fsi.Id;
            var nuevoFsiDetralle = await _FsixinsummoMdOdetalleService.Crear(fsidetalle);
            if (!nuevoFsiDetralle)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No guardó el registro";
                return respuesta;
            }

            var editarFSI = await EditarFsiXInsumo(Fsi);
            if (!editarFSI) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se editó el FSI";
                return respuesta;
            }

            var editarFSR = await EditarFsrXInsumo(Fsi.IdInsumo);
            if (!editarFSR)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se editó el FSR";
            }

            respuesta.Estatus = true;
            respuesta.Descripcion = "Se editó el FSR";
            return respuesta;
        }

        public async Task<bool> EditarFsrXInsumo(int IdInsumo) {
            var Fsr = await _FsrxinsummoMdOService.ObtenerXIdInsumo(IdInsumo);
            var FsrDetalles = await _FsrxinsummoMdOdetalleService.ObtenerXIdFsr(Fsr.Id);
            var PorcentajeFSR = FsrDetalles.Sum(z => z.PorcentajeFsr);
            var Fsi = await _FsixinsummoMdOService.ObtenerXIdInsumo(IdInsumo);
            if (Fsi.Id > 0) {
                if (Fsi.Fsi != 0) {
                    Fsr.Fsr = Fsi.Fsi + (PorcentajeFSR / 100);
                }
                else
                {
                    Fsr.Fsr = 1 + (PorcentajeFSR/100);
                }
            }
            else
            {
                Fsr.Fsr = 1 + (PorcentajeFSR / 100);
            }
            Fsr.CostoFinal = Fsr.CostoDirecto * Fsr.Fsr;

            var editarFsr = await _FsrxinsummoMdOService.Editar(Fsr);
            if (!editarFsr) {
                return false;
            }
            var insumo = await _InsumoService.ObtenXId(IdInsumo);
            if (insumo.id > 0)
            {
                insumo.CostoUnitario = Fsr.CostoFinal;
                insumo.EsFsrGlobal = true;
                var editaInsumo = await _InsumoService.Editar(insumo);
            }
            return true;
        }

        public async Task<bool> EditarFsiXInsumo(FsixinsummoMdODTO Fsi) {
            var listaDetalles = await _FsixinsummoMdOdetalleService.ObtenerXIdFsi(Fsi.Id);
            var diasNoLaborales = listaDetalles.Where(z => z.EsLaborableOpagado == false).Sum(z => z.Dias);
            var diasPagados = listaDetalles.Where(z => z.EsLaborableOpagado == true).Sum(z => z.Dias);

            Fsi.DiasNoLaborales = diasNoLaborales;
            Fsi.DiasPagados = diasPagados;
            if (diasPagados == 0) {
                Fsi.Fsi = 0;
            }
            if (diasNoLaborales != 0 && diasPagados != 0) {
                Fsi.Fsi = diasPagados / ((decimal)365.25 - diasNoLaborales);
            }

            var editarFsi = await _FsixinsummoMdOService.Editar(Fsi);
            return editarFsi;
        }

        public async Task<RespuestaDTO> EditarFsrDetalle(FsrxinsummoMdOdetalleDTO objeto) {
            var respuesta = new RespuestaDTO();

            var editarDetalle = await _FsrxinsummoMdOdetalleService.Editar(objeto);
            if (!editarDetalle)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se editó en registro";
                return respuesta;
            }

            var editarFSR = await EditarFsrXInsumo(objeto.IdInsumo);
            if (!editarFSR)
            {
                respuesta.Estatus = true;
                respuesta.Descripcion = "No se editó el FSR";
            }
            respuesta.Estatus = true;
            respuesta.Descripcion = "Se actualizó la información";
            return respuesta;
        }

        public async Task<RespuestaDTO> EliminarFsrDetalle(int IdFsrDetalle)
        {
            var respuesta = new RespuestaDTO();
            var detalle = await _FsrxinsummoMdOdetalleService.ObtenerXId(IdFsrDetalle);

            var eliminarDetalle = await _FsrxinsummoMdOdetalleService.Eliminar(detalle.Id);
            if (!eliminarDetalle)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se eliminó en registro";
                return respuesta;
            }

            var editarFSR = await EditarFsrXInsumo(detalle.IdInsumo);
            if (!editarFSR)
            {
                respuesta.Estatus = true;
                respuesta.Descripcion = "No se editó el FSR";
            }
            respuesta.Estatus = true;
            respuesta.Descripcion = "Se actualizó la información";
            return respuesta;
        }

        public async Task<RespuestaDTO> EditarFsiDetalle(FsixinsummoMdOdetalleDTO objeto) {
            var respuesta = new RespuestaDTO();

            var editarDetalle = await _FsixinsummoMdOdetalleService.Editar(objeto);
            if (!editarDetalle) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se editó en registro";
                return respuesta;
            }

            var Fsi = await _FsixinsummoMdOService.ObtenerXIdInsumo(objeto.IdInsumo);

            var editarFSI = await EditarFsiXInsumo(Fsi);
            if (!editarFSI)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se editó el FSI";
                return respuesta;
            }

            var editarFSR = await EditarFsrXInsumo(Fsi.IdInsumo);
            if (!editarFSR)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se editó el FSR";
            }

            respuesta.Estatus = true;
            respuesta.Descripcion = "Se actualizó la información";
            return respuesta;
        }

        public async Task<RespuestaDTO> EliminarFsiDetalle(int IdFsiDetalle)
        {
            var respuesta = new RespuestaDTO();
            var detalle = await _FsixinsummoMdOdetalleService.ObtenerXId(IdFsiDetalle);

            var eliminarDetalle = await _FsixinsummoMdOdetalleService.Eliminar(detalle.Id);
            if (!eliminarDetalle)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se eliminó en registro";
                return respuesta;
            }

            var Fsi = await _FsixinsummoMdOService.ObtenerXIdInsumo(detalle.IdInsumo);

            var editarFSI = await EditarFsiXInsumo(Fsi);
            if (!editarFSI)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se editó el FSI";
                return respuesta;
            }

            var editarFSR = await EditarFsrXInsumo(Fsi.IdInsumo);
            if (!editarFSR)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se editó el FSR";
            }

            respuesta.Estatus = true;
            respuesta.Descripcion = "Se actualizó la información";
            return respuesta;
        }

        public async Task<ObjetoFactorSalarioXInsumoDTO> ObtenerFactorSalario(int IdInsumo) { 
            var objeto = new ObjetoFactorSalarioXInsumoDTO();
            objeto.Fsr = new EstructuraFsrxinsummoMdODTO();
            objeto.Fsi = new EstructuraFsixinsummoMdODTO();
            var Fsr = await _FsrxinsummoMdOService.ObtenerXIdInsumo(IdInsumo);
            var Fsi = await _FsixinsummoMdOService.ObtenerXIdInsumo(IdInsumo);

            objeto.Fsr = new EstructuraFsrxinsummoMdODTO();
            if (Fsr.Id > 0) { 
                objeto.Fsr.Id = Fsr.Id;
                objeto.Fsr.Fsr = Fsr.Fsr;
                objeto.Fsr.CostoDirecto = Fsr.CostoDirecto;
                objeto.Fsr.CostoFinal = Fsr.CostoFinal;
                objeto.Fsr.IdInsumo = Fsr.IdInsumo;
                objeto.Fsr.IdProyecto = Fsr.IdProyecto;

                var detallesFSR = await _FsrxinsummoMdOdetalleService.ObtenerXIdFsr(Fsr.Id);
                objeto.Fsr.detalles = detallesFSR;
            }

            if (Fsi.Id > 0) { 
                objeto.Fsi.Id = Fsi.Id;
                objeto.Fsi.DiasNoLaborales = Fsi.DiasNoLaborales;
                objeto.Fsi.DiasPagados = Fsi.DiasPagados;
                objeto.Fsi.Fsi = Fsi.Fsi;
                objeto.Fsi.IdInsumo = Fsi.IdInsumo;
                objeto.Fsi.IdProyecto = Fsi.IdProyecto;

                var detallesFSI = await _FsixinsummoMdOdetalleService.ObtenerXIdFsi(Fsi.Id);
                objeto.Fsi.detalles = detallesFSI;
            }


            return objeto;
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
