using AutoMapper;
using AutoMapper.Configuration.Annotations;
using DbfDataReader;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DocumentFormat.OpenXml.Office2013.Excel;
using ERP_TECKIO.DTO;
using ERP_TECKIO.DTO.Factura;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Procesos;
using ERP_TECKIO.Servicios;
using ERP_TECKIO.Servicios.Contratos;
using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;
using Microsoft.Win32;
using SpreadsheetLight;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;

namespace ERP_TECKIO
{
    public class PrecioUnitarioProceso<TContext> where TContext : DbContext
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
        private readonly IMapper _Mapper;
        private readonly TContext _dbContex;
        private static readonly Encoding Latin1 = Encoding.GetEncoding("windows-1252");
        private readonly FactorSalarioRealProceso<TContext> _factorSalarioRealProceso;
        public PrecioUnitarioProceso(
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
            , IMapper mapper
            , TContext dbContex
            , FactorSalarioRealProceso<TContext> factorSalarioRealProceso

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
            _dbContex = dbContex;
            _OperacionXPUService = operacionXPUService;
            _FsrxinsummoMdOService = fsrxinsummoMdOService;
            _factorSalarioRealProceso = factorSalarioRealProceso;
        }

        public async Task RecalcularPrecioUnitario(PrecioUnitarioDTO registro)
        {
            var registros = await _PrecioUnitarioService.ObtenerTodos(registro.IdProyecto);
            for (int i = 0; i < registros.Count; i++)
            {
                registros[i].Expandido = true;
            }
            var conceptos = await _ConceptoService.ObtenerTodos(registro.IdProyecto);
            for (int i = 0; i < registros.Count; i++)
            {
                var concepto = conceptos.Where(z => z.Id == registros[i].IdConcepto).FirstOrDefault();
                registros[i].Descripcion = concepto.Descripcion;
                registros[i].Codigo = concepto.Codigo;
                registros[i].Unidad = concepto.Unidad;
                registros[i].CostoUnitario = concepto.CostoUnitario;
            }
            var existePadre = registros.Where(z => z.Id == registro.IdPrecioUnitarioBase);
            if (existePadre.Count() > 0)
            {
                var padre = existePadre.FirstOrDefault();
                var hijos = registros.Where(z => z.IdPrecioUnitarioBase == padre.Id).ToList();
                decimal total = 0;
                for (int i = 0; i < hijos.Count; i++)
                {
                    total = total + hijos[i].CostoUnitario * hijos[i].Cantidad;
                }
                padre.CostoUnitario = total;
                var conceptoPadre = conceptos.Where(z => z.Id == padre.IdConcepto).FirstOrDefault();
                conceptoPadre.CostoUnitario = total;
                await _ConceptoService.Editar(conceptoPadre);
                await RecalcularPadresPrecioUnitario(padre, registros);
            }
        }

        public async Task RecalcularPorcentajeManoDeObra(PrecioUnitarioDetalleDTO registro)
        {
            var precioUnitario = await _PrecioUnitarioService.ObtenXId(registro.IdPrecioUnitario);
            var insumos = await _InsumoService.ObtenXIdProyecto(precioUnitario.IdProyecto);
            var detalles = await _PrecioUnitarioDetalleService.ObtenerTodosXIdPrecioUnitario(registro.IdPrecioUnitario);
            var detallesFiltrados = detalles.Where(z => z.IdPrecioUnitarioDetallePerteneciente == registro.IdPrecioUnitarioDetallePerteneciente).ToList();
            for (int i = 0; i < detallesFiltrados.Count; i++)
            {
                var insumo = insumos.Where(z => z.id == detallesFiltrados[i].IdInsumo).FirstOrDefault();
                detallesFiltrados[i].Codigo = insumo.Codigo;
                detallesFiltrados[i].Descripcion = insumo.Descripcion;
                detallesFiltrados[i].Unidad = insumo.Unidad;
                detallesFiltrados[i].CostoUnitario = insumo.CostoUnitario;
                detallesFiltrados[i].IdTipoInsumo = insumo.idTipoInsumo;
                detallesFiltrados[i].IdFamiliaInsumo = insumo.idFamiliaInsumo;
            }
            if (detallesFiltrados.Count == 4)
            {
                var x = 0;
            }
            var detallesPorcentajeManoObra = detallesFiltrados.Where(z => z.IdTipoInsumo == 10001).ToList();
            if (detallesPorcentajeManoObra.Count > 0)
            {
                for (int i = 0; i < detallesPorcentajeManoObra.Count; i++)
                {
                    decimal total = 0;
                    var detallesManoObra = detallesFiltrados.Where(z => z.IdTipoInsumo == 10000).ToList();
                    if (detallesManoObra.Count > 0)
                    {
                        for (int j = 0; j < detallesManoObra.Count; j++)
                        {
                            total = total + detallesManoObra[j].CostoUnitario * detallesManoObra[j].Cantidad;
                        }
                    }
                    var insumoPorcentaje = insumos.Where(z => z.id == detallesPorcentajeManoObra[i].IdInsumo).FirstOrDefault();
                    insumoPorcentaje.CostoUnitario = total;
                    await _InsumoService.Editar(insumoPorcentaje);
                    await _PrecioUnitarioDetalleService.Editar(detallesPorcentajeManoObra[i]);
                }
            }
        }

        public async Task RecalcularPadresPrecioUnitario(PrecioUnitarioDTO registro, List<PrecioUnitarioDTO> registros)
        {
            var conceptos = await _ConceptoService.ObtenerTodos(registro.IdProyecto);
            for (int i = 0; i < registros.Count; i++)
            {
                var concepto = conceptos.Where(z => z.Id == registros[i].IdConcepto).FirstOrDefault();
                registros[i].Descripcion = concepto.Descripcion;
                registros[i].Codigo = concepto.Codigo;
                registros[i].Unidad = concepto.Unidad;
                registros[i].CostoUnitario = concepto.CostoUnitario;
            }
            if (registro.IdPrecioUnitarioBase > 0)
            {
                var padre = registros.Where(z => z.Id == registro.IdPrecioUnitarioBase).FirstOrDefault();
                var hijos = registros.Where(z => z.IdPrecioUnitarioBase == padre.Id).ToList();
                decimal total = 0;
                for (int i = 0; i < hijos.Count; i++)
                {
                    total = total + hijos[i].CostoUnitario * hijos[i].Cantidad;
                }
                padre.CostoUnitario = total;
                var conceptoPadre = conceptos.Where(z => z.Id == padre.IdConcepto).FirstOrDefault();
                conceptoPadre.CostoUnitario = total;
                await _ConceptoService.Editar(conceptoPadre);
                await RecalcularPadresPrecioUnitario(padre, registros);
            }
            else
            {
                var padre = registros.Where(z => z.Id == registro.Id).FirstOrDefault();
                var hijos = registros.Where(z => z.IdPrecioUnitarioBase == padre.Id).ToList();
                decimal total = 0;
                for (int i = 0; i < hijos.Count; i++)
                {
                    total = total + hijos[i].CostoUnitario * hijos[i].Cantidad;
                }
                padre.CostoUnitario = total;
                var conceptoPadre = conceptos.Where(z => z.Id == registro.IdConcepto).FirstOrDefault();
                conceptoPadre.CostoUnitario = total;
                await _ConceptoService.Editar(conceptoPadre);
            }
        }

        public async Task<DatosAuxiliaresPrecioUnitarioDTO> RecalcularDetalles(int IdPrecioUnitario, List<PrecioUnitarioDetalleDTO> detalles, List<InsumoDTO> insumos)
        {
            var PU = await _PrecioUnitarioService.ObtenXId(IdPrecioUnitario);
            for (int i = 0; i < detalles.Count; i++)
            {
                var insumo = insumos.Where(z => z.id == detalles[i].IdInsumo).FirstOrDefault();
                detalles[i].CostoUnitario = insumo.CostoUnitario;
            }
            DatosAuxiliaresPrecioUnitarioDTO datosDetalles = new DatosAuxiliaresPrecioUnitarioDTO();
            var detallesFiltrados = detalles.Where(z => z.IdPrecioUnitarioDetallePerteneciente == 0).ToList();
            var detallesFiltradosSinPorcentaje = detallesFiltrados.Where(z => z.IdTipoInsumo != 10001).ToList();
            decimal total = 0;
            if (detallesFiltradosSinPorcentaje.Count > 0)
            {
                for (int i = 0; i < detallesFiltradosSinPorcentaje.Count; i++)
                {
                    if (detallesFiltradosSinPorcentaje[i].EsCompuesto == true)
                    {
                        var registro = insumos.Where(z => z.id == detallesFiltradosSinPorcentaje[i].IdInsumo).FirstOrDefault();
                        if (registro.idTipoInsumo == 10000)
                        {
                            await RecalcularPorcentajeManoDeObra(detallesFiltradosSinPorcentaje[i]);
                        }
                        datosDetalles = await RecalcularDetallesHijos(IdPrecioUnitario, detallesFiltradosSinPorcentaje[i], detalles, insumos);

                        var registro2 = insumos.Where(z => z.id == detallesFiltradosSinPorcentaje[i].IdInsumo).FirstOrDefault();
                        registro2.CostoUnitario = datosDetalles.Total;
                        registro2.CostoBase = datosDetalles.Total;
                        await _InsumoService.Editar(registro2);
                        detallesFiltradosSinPorcentaje[i].CostoUnitario = datosDetalles.Total;
                        total = total + detallesFiltradosSinPorcentaje[i].CostoUnitario * detallesFiltradosSinPorcentaje[i].Cantidad;
                    }
                    else
                    {
                        var registro = insumos.Where(z => z.id == detallesFiltradosSinPorcentaje[i].IdInsumo).FirstOrDefault();
                        if (registro.idTipoInsumo == 10000)
                        {
                            await RecalcularPorcentajeManoDeObra(detallesFiltradosSinPorcentaje[i]);
                        }
                        total = total + (detallesFiltradosSinPorcentaje[i].CostoUnitario * detallesFiltradosSinPorcentaje[i].Cantidad);
                    }
                }
            }
            var totalAux = total;
            //Obtener todos los registros correspondientes al porcentaje en este nivel y PU, y sumar su total a cada uno de los registros;
            var detallesFiltradosParaFiltrarPorcentaje = detalles.Where(z => z.IdPrecioUnitarioDetallePerteneciente == 0).ToList();
            var detallesFiltradosPorcentaje = detallesFiltradosParaFiltrarPorcentaje.Where(z => z.IdTipoInsumo == 10001).ToList();
            if (detallesFiltradosPorcentaje.Count > 0)
            {
                for (int i = 0; i < detallesFiltradosPorcentaje.Count; i++)
                {
                    var registro = insumos.Where(z => z.id == detallesFiltradosSinPorcentaje[i].IdInsumo).FirstOrDefault();
                    if (registro.idTipoInsumo == 10000)
                    {
                        await RecalcularPorcentajeManoDeObra(detallesFiltradosSinPorcentaje[i]);
                    }
                    await _PrecioUnitarioDetalleService.Editar(detallesFiltradosPorcentaje[i]);
                    total = total + detallesFiltradosPorcentaje[i].CostoUnitario * detallesFiltradosPorcentaje[i].Cantidad;
                }
            }
            datosDetalles.Total = total;
            datosDetalles.Insumos = insumos;
            datosDetalles.Detalles = detalles;
            return datosDetalles;
        }

        public async Task<DatosAuxiliaresPrecioUnitarioDTO> RecalcularDetallesHijos(int IdPrecioUnitario, PrecioUnitarioDetalleDTO detalle, List<PrecioUnitarioDetalleDTO> detalles, List<InsumoDTO> insumos)
        {
            DatosAuxiliaresPrecioUnitarioDTO datosDetalles = new DatosAuxiliaresPrecioUnitarioDTO();
            var detallesFiltrados = detalles.Where(z => z.IdPrecioUnitarioDetallePerteneciente == detalle.Id).ToList();
            var detallesFiltradosSinPorcentaje = detallesFiltrados.Where(z => z.IdTipoInsumo != 10001).ToList();
            decimal total = 0;
            decimal totalAux = 0;
            if (detallesFiltradosSinPorcentaje.Count > 0)
            {
                for (int i = 0; i < detallesFiltradosSinPorcentaje.Count; i++)
                {
                    if (detallesFiltradosSinPorcentaje[i].EsCompuesto == true)
                    {
                        if (insumos.Where(z => z.id == detallesFiltradosSinPorcentaje[i].IdInsumo).FirstOrDefault().idTipoInsumo == 10000)
                        {
                            await RecalcularPorcentajeManoDeObra(detallesFiltradosSinPorcentaje[i]);
                        }
                        var detallesParaPrueba = detalles.Where(z => z.IdPrecioUnitarioDetallePerteneciente == detallesFiltradosSinPorcentaje[i].Id).ToList();
                        if (detallesParaPrueba.Count > 0)
                        {
                            datosDetalles = await RecalcularDetallesHijos(IdPrecioUnitario, detallesFiltradosSinPorcentaje[i], detalles, insumos);
                            var registro2 = insumos.Where(z => z.id == detallesFiltradosSinPorcentaje[i].IdInsumo).FirstOrDefault();
                            registro2.CostoUnitario = datosDetalles.Total;
                            registro2.CostoBase = datosDetalles.Total;
                            await _InsumoService.Editar(insumos.Where(z => z.id == detallesFiltradosSinPorcentaje[i].IdInsumo).FirstOrDefault());
                            //await RecalcularAfectados(insumo.id);
                            detallesFiltradosSinPorcentaje[i].CostoUnitario = datosDetalles.Total;
                            detallesFiltradosSinPorcentaje[i].CostoBase = datosDetalles.Total;
                        }
                        total = total + detallesFiltradosSinPorcentaje[i].CostoUnitario * detallesFiltradosSinPorcentaje[i].Cantidad;
                        detalles.Where(z => z.Id == detallesFiltradosSinPorcentaje[i].Id).FirstOrDefault().CostoUnitario = datosDetalles.Total;
                    }
                    else
                    {
                        if (insumos.Where(z => z.id == detallesFiltradosSinPorcentaje[i].IdInsumo).FirstOrDefault().idTipoInsumo == 10000)
                        {
                            await RecalcularPorcentajeManoDeObra(detallesFiltradosSinPorcentaje[i]);
                            totalAux = totalAux + (detallesFiltradosSinPorcentaje[i].CostoUnitario * detallesFiltradosSinPorcentaje[i].Cantidad);
                        }
                        total = total + (detallesFiltradosSinPorcentaje[i].CostoUnitario * detallesFiltradosSinPorcentaje[i].Cantidad);
                    }
                }
                var detallesFiltradosParaFiltrarPorcentaje = detalles.Where(z => z.IdPrecioUnitarioDetallePerteneciente == detalle.Id).ToList();
                var detallesFiltradosPorcentaje = detallesFiltradosParaFiltrarPorcentaje.Where(z => z.IdTipoInsumo == 10001).ToList();
                if (detallesFiltradosPorcentaje.Count > 0)
                {
                    for (int i = 0; i < detallesFiltradosPorcentaje.Count; i++)
                    {
                        await _PrecioUnitarioDetalleService.Editar(detallesFiltradosPorcentaje[i]);
                        total = total + totalAux * detallesFiltradosPorcentaje[i].Cantidad;
                    }
                }
            }
            datosDetalles.Insumos = insumos;
            datosDetalles.Detalles = detalles;
            datosDetalles.Total = total;
            return datosDetalles;
        }

        //public async Task<decimal> RecalcularDetalles(int IdPrecioUnitario)
        //{
        //    var PU = await _PrecioUnitarioService.ObtenXId(IdPrecioUnitario);
        //    var detalles = await _PrecioUnitarioDetalleService.ObtenerTodosXIdPrecioUnitario(IdPrecioUnitario);
        //    var insumos = await _InsumoService.Lista(PU.IdProyecto);
        //    for (int i = 0; i < detalles.Count; i++)
        //    {
        //        var insumo = insumos.Where(z => z.id == detalles[i].IdInsumo).FirstOrDefault();
        //        detalles[i].Codigo = insumo.Codigo;
        //        detalles[i].Descripcion = insumo.Descripcion;
        //        detalles[i].Unidad = insumo.Unidad;
        //        detalles[i].CostoUnitario = insumo.CostoUnitario;
        //        detalles[i].IdTipoInsumo = insumo.idTipoInsumo;
        //        detalles[i].IdFamiliaInsumo = insumo.idFamiliaInsumo;
        //    }
        //    var detallesFiltrados = detalles.Where(z => z.IdPrecioUnitarioDetallePerteneciente == 0).ToList();
        //    var detallesFiltradosSinPorcentaje = detallesFiltrados.Where(z => z.IdTipoInsumo != 10001).ToList();
        //    decimal total = 0;
        //    if (detallesFiltradosSinPorcentaje.Count > 0)
        //    {
        //        for (int i = 0; i < detallesFiltradosSinPorcentaje.Count; i++)
        //        {
        //            if (detallesFiltradosSinPorcentaje[i].EsCompuesto == true)
        //            {
        //                var costo = await RecalcularDetallesHijos(IdPrecioUnitario, detallesFiltradosSinPorcentaje[i], detalles, insumos);
        //                var insumo = insumos.Where(z => z.id == detallesFiltradosSinPorcentaje[i].IdInsumo).FirstOrDefault();
        //                insumo.CostoUnitario = costo;
        //                await _InsumoService.Editar(insumo);
        //                detallesFiltradosSinPorcentaje[i].CostoUnitario = costo;
        //                if (insumo.idTipoInsumo == 10000)
        //                {
        //                    await RecalcularPorcentajeManoDeObra(detallesFiltradosSinPorcentaje[i]);
        //                }
        //                total = total + detallesFiltradosSinPorcentaje[i].CostoUnitario * detallesFiltradosSinPorcentaje[i].Cantidad;
        //            }
        //            else
        //            {
        //                total = total + (detallesFiltradosSinPorcentaje[i].CostoUnitario * detallesFiltradosSinPorcentaje[i].Cantidad);
        //            }
        //        }
        //    }
        //    //Obtener todos los registros correspondientes al porcentaje en este nivel y PU, y sumar su total a cada uno de los registros;
        //    var detallesParaFiltrarPorcentaje = await _PrecioUnitarioDetalleService.ObtenerTodosXIdPrecioUnitario(IdPrecioUnitario);
        //    var insumosParaPorcentaje = await _InsumoService.Lista(PU.IdProyecto);
        //    for (int i = 0; i < detallesParaFiltrarPorcentaje.Count; i++)
        //    {
        //        var insumo = insumosParaPorcentaje.Where(z => z.id == detallesParaFiltrarPorcentaje[i].IdInsumo).FirstOrDefault();
        //        detallesParaFiltrarPorcentaje[i].Codigo = insumo.Codigo;
        //        detallesParaFiltrarPorcentaje[i].Descripcion = insumo.Descripcion;
        //        detallesParaFiltrarPorcentaje[i].Unidad = insumo.Unidad;
        //        detallesParaFiltrarPorcentaje[i].CostoUnitario = insumo.CostoUnitario;
        //        detallesParaFiltrarPorcentaje[i].IdTipoInsumo = insumo.idTipoInsumo;
        //        detallesParaFiltrarPorcentaje[i].IdFamiliaInsumo = insumo.idFamiliaInsumo;
        //    }
        //    var detallesFiltradosParaFiltrarPorcentaje = detallesParaFiltrarPorcentaje.Where(z => z.IdPrecioUnitarioDetallePerteneciente == 0).ToList();
        //    var detallesFiltradosPorcentaje = detallesFiltradosParaFiltrarPorcentaje.Where(z => z.IdTipoInsumo == 10001).ToList();
        //    if (detallesFiltradosPorcentaje.Count > 0)
        //    {
        //        for (int i = 0; i < detallesFiltradosPorcentaje.Count; i++)
        //        {
        //            total = total + detallesFiltradosPorcentaje[i].CostoUnitario * detallesFiltradosPorcentaje[i].Cantidad;
        //        }
        //    }
        //    return total;
        //}

        //public async Task<decimal> RecalcularDetallesHijos(int IdPrecioUnitario, PrecioUnitarioDetalleDTO detalle, List<PrecioUnitarioDetalleDTO> detalles, List<InsumoDTO> insumos)
        //{
        //    var detallesFiltrados = detalles.Where(z => z.IdPrecioUnitarioDetallePerteneciente == detalle.Id).ToList();
        //    decimal total = 0;
        //    if (detallesFiltrados.Count > 0)
        //    {
        //        for (int i = 0; i < detallesFiltrados.Count; i++)
        //        {
        //            if (detallesFiltrados[i].EsCompuesto == true)
        //            {
        //                var costo = await RecalcularDetallesHijos(IdPrecioUnitario, detallesFiltrados[i], detalles, insumos);
        //                insumos.Where(z => z.id == detallesFiltrados[i].IdInsumo).FirstOrDefault().CostoUnitario = costo;
        //                var insumo = insumos.Where(z => z.id == detallesFiltrados[i].IdInsumo).FirstOrDefault();
        //                insumo.CostoUnitario = costo;
        //                await _InsumoService.Editar(insumo);
        //                //await RecalcularAfectados(insumo.id);
        //                detallesFiltrados[i].CostoUnitario = costo;
        //                total = total + detallesFiltrados[i].CostoUnitario * detallesFiltrados[i].Cantidad;
        //            }
        //            else
        //            {
        //                total = total + (detallesFiltrados[i].CostoUnitario * detallesFiltrados[i].Cantidad);
        //            }
        //        }
        //    }
        //    return total;
        //}

        public async Task<List<PrecioUnitarioDTO>> ObtenerPrecioUnitario(int IdProyecto)
        {
            var proyecto = await _ProyectoService.ObtenXId(IdProyecto);
            var conceptos = await _ConceptoService.ObtenerTodos(IdProyecto);
            var lista = await _PrecioUnitarioService.ObtenerTodos(IdProyecto);
            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].Nivel > 1)
                {
                    lista[i].Expandido = true;
                }
            }
            var conjuntoIndirectos = await _conjuntoIndirectosService.ObtenerXIdProyecto(IdProyecto);
            decimal indirectos = 0;
            if (conjuntoIndirectos.Porcentaje == 0 || conjuntoIndirectos.Id <= 0)
            {
                indirectos = 1;
            }
            else
            {
                indirectos = conjuntoIndirectos.Porcentaje;
            }
            for (int i = 0; i < lista.Count; i++)
            {
                var concepto = conceptos.Where(z => z.Id == lista[i].IdConcepto).FirstOrDefault();
                //decimal indirectoIndividual = concepto.PorcentajeIndirecto * concepto.CostoUnitario;
                lista[i].NoSerie = proyecto.NoSerie;
                lista[i].Codigo = concepto.Codigo;
                lista[i].Descripcion = concepto.Descripcion;
                lista[i].Unidad = concepto.Unidad;
                lista[i].CostoUnitario = concepto.CostoUnitario;
                lista[i].PorcentajeIndirecto = concepto.PorcentajeIndirecto;
                lista[i].PrecioUnitario = (lista[i].CostoUnitario * indirectos);
                lista[i].Importe = lista[i].PrecioUnitario * lista[i].Cantidad;
                lista[i].ImporteSeries = lista[i].Importe * lista[i].NoSerie;
                lista[i].Expandido = true;
                lista[i].CantidadConFormato = String.Format("{0:#,##0.0000}", lista[i].Cantidad);
                lista[i].CantidadExcedenteConFormato = String.Format("{0:#,##0.0000}", lista[i].CantidadExcedente);
                lista[i].CostoUnitarioConFormato = String.Format("{0:#,##0.00}", lista[i].CostoUnitario);
                lista[i].PrecioUnitarioConFormato = String.Format("{0:#,##0.00}", lista[i].PrecioUnitario);
                lista[i].ImporteConFormato = String.Format("{0:#,##0.00}", lista[i].Importe);
                lista[i].ImporteSeriesConFormato = String.Format("{0:#,##0.00}", lista[i].ImporteSeries);
                lista[i].PorcentajeIndirectoConFormato = String.Format("{0:#,##0.0000}", lista[i].PorcentajeIndirecto);
            }
            var listaEstructurada = await _PrecioUnitarioService.Estructurar(lista, indirectos);
            var listaResult = listaEstructurada.OrderBy(z => z.Posicion).ToList();
            return listaResult;
        }

        public async Task<List<PrecioUnitarioDTO>> ObtenerPrecioUnitarioSinEstructurar(int IdProyecto)
        {
            var proyecto = await _ProyectoService.ObtenXId(IdProyecto);
            var conceptos = await _ConceptoService.ObtenerTodos(IdProyecto);
            var lista = await _PrecioUnitarioService.ObtenerTodos(IdProyecto);
            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].Nivel > 1)
                {
                    lista[i].Expandido = true;
                }
            }
            var indirectos = 1;
            for (int i = 0; i < lista.Count; i++)
            {
                var concepto = conceptos.Where(z => z.Id == lista[i].IdConcepto).FirstOrDefault();
                lista[i].NoSerie = proyecto.NoSerie;
                lista[i].Codigo = concepto.Codigo;
                lista[i].Descripcion = concepto.Descripcion;
                lista[i].Unidad = concepto.Unidad;
                lista[i].CostoUnitario = concepto.CostoUnitario;
                lista[i].PrecioUnitario = lista[i].CostoUnitario * indirectos;
                lista[i].Importe = lista[i].PrecioUnitario * lista[i].Cantidad;
                lista[i].ImporteSeries = lista[i].Importe * lista[i].NoSerie;
                lista[i].Expandido = true;
                lista[i].CantidadConFormato = String.Format("{0:#,##0.0000}", lista[i].Cantidad);
                lista[i].CantidadExcedenteConFormato = String.Format("{0:#,##0.0000}", lista[i].CantidadExcedente);
                lista[i].CostoUnitarioConFormato = String.Format("{0:#,##0.00}", lista[i].CostoUnitario);
                lista[i].PrecioUnitarioConFormato = String.Format("{0:#,##0.00}", lista[i].PrecioUnitario);
                lista[i].ImporteConFormato = String.Format("{0:#,##0.00}", lista[i].Importe);
                lista[i].ImporteSeriesConFormato = String.Format("{0:#,##0.00}", lista[i].ImporteSeries);
            }
            return lista;
        }

        public async Task<ActionResult<List<PrecioUnitarioCopiaDTO>>> ObtenerPrecioUnitarioCopia(int IdProyecto)
        {
            var proyecto = await _ProyectoService.ObtenXId(IdProyecto);
            var conceptos = await _ConceptoService.ObtenerTodos(IdProyecto);
            var lista = await _PrecioUnitarioService.ObtenerTodosParaCopia(IdProyecto);
            var indirectos = 1;
            for (int i = 0; i < lista.Count; i++)
            {
                var concepto = conceptos.Where(z => z.Id == lista[i].IdConcepto).FirstOrDefault();
                lista[i].NoSerie = proyecto.NoSerie;
                lista[i].Codigo = concepto.Codigo;
                lista[i].Descripcion = concepto.Descripcion;
                lista[i].Unidad = concepto.Unidad;
                lista[i].CostoUnitario = concepto.CostoUnitario;
                lista[i].PrecioUnitario = lista[i].CostoUnitario * indirectos;
                lista[i].Importe = lista[i].PrecioUnitario * lista[i].Cantidad;
                lista[i].ImporteSeries = lista[i].Importe * lista[i].NoSerie;
                lista[i].Expandido = true;
                lista[i].CantidadConFormato = String.Format("{0:#,##0.0000}", lista[i].Cantidad);
                lista[i].CantidadExcedenteConFormato = String.Format("{0:#,##0.0000}", lista[i].CantidadExcedente);
                lista[i].CostoUnitarioConFormato = String.Format("{0:#,##0.00}", lista[i].CostoUnitario);
                lista[i].PrecioUnitarioConFormato = String.Format("{0:#,##0.00}", lista[i].PrecioUnitario);
                lista[i].ImporteConFormato = String.Format("{0:#,##0.00}", lista[i].Importe);
                lista[i].ImporteSeriesConFormato = String.Format("{0:#,##0.00}", lista[i].ImporteSeries);
            }
            var listaEstructurada = await _PrecioUnitarioService.EstructurarCopia(lista);
            var listaResult = listaEstructurada.OrderBy(z => z.Id).ToList();
            return listaResult;
        }

        public async Task<List<PrecioUnitarioDTO>> ObtenerSinEstructura(int IdProyecto)
        {
            var proyecto = await _ProyectoService.ObtenXId(IdProyecto);
            var conceptos = await _ConceptoService.ObtenerTodos(IdProyecto);
            var lista = await _PrecioUnitarioService.ObtenerTodos(IdProyecto);
            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].Nivel == 1)
                {
                    lista[i].Expandido = true;
                }
            }
            var indirectos = 1;
            for (int i = 0; i < lista.Count; i++)
            {
                var concepto = conceptos.Where(z => z.Id == lista[i].IdConcepto).FirstOrDefault();
                lista[i].NoSerie = proyecto.NoSerie;
                lista[i].Codigo = concepto.Codigo;
                lista[i].Descripcion = concepto.Descripcion;
                lista[i].Unidad = concepto.Unidad;
                lista[i].CostoUnitario = concepto.CostoUnitario;
                lista[i].PrecioUnitario = lista[i].CostoUnitario * indirectos;
                lista[i].Importe = lista[i].PrecioUnitario * lista[i].Cantidad;
                lista[i].ImporteSeries = lista[i].Importe * lista[i].NoSerie;
                lista[i].CantidadConFormato = String.Format("{0:#,##0.0000}", lista[i].Cantidad);
                lista[i].CantidadExcedenteConFormato = String.Format("{0:#,##0.0000}", lista[i].CantidadExcedente);
                lista[i].CostoUnitarioConFormato = String.Format("{0:#,##0.00}", lista[i].CostoUnitario);
                lista[i].PrecioUnitarioConFormato = String.Format("{0:#,##0.00}", lista[i].PrecioUnitario);
                lista[i].ImporteConFormato = String.Format("{0:#,##0.00}", lista[i].Importe);
                lista[i].ImporteSeriesConFormato = String.Format("{0:#,##0.00}", lista[i].ImporteSeries);
            }
            return lista;
        }

        public async Task<List<PrecioUnitarioDTO>> Estructurar(List<PrecioUnitarioDTO> registros)
        {
            var padres = registros.Where(z => z.IdPrecioUnitarioBase == 0).ToList();
            for (int i = 0; i < padres.Count; i++)
            {
                padres[i].Hijos = await BuscaHijos(registros, padres[i]);
            }
            return padres;
        }

        private async Task<List<PrecioUnitarioDTO>> BuscaHijos(List<PrecioUnitarioDTO> registros, PrecioUnitarioDTO padre)
        {
            var padres = registros.Where(z => z.IdPrecioUnitarioBase == padre.Id).ToList();
            for (int i = 0; i < padres.Count; i++)
            {
                padres[i].Hijos = await BuscaHijos(registros, padres[i]);
            }
            return padres;
        }

        public async Task<ActionResult<List<PrecioUnitarioDTO>>> CrearYObtener(PrecioUnitarioDTO registro, DbContext db)
        {
            try
            {
                if (registro.TipoPrecioUnitario == 1 && registro.Codigo != "" && registro.Descripcion == "" && registro.Unidad == "" && registro.Cantidad <= 0)
                {
                    var PUs = await ObtenerPrecioUnitarioSinEstructurar(registro.IdProyecto);
                    var PUcopiar = PUs.Where(z => z.Codigo == registro.Codigo && z.TipoPrecioUnitario == 1).FirstOrDefault();
                    //var PUCdigoSimilar = PUs.Where(z => z.Codigo.Count() >= registro.Codigo.Count() && 
                    //z.Codigo.Substring(0, registro.Codigo.Count()) == registro.Codigo).ToList();

                    if (PUcopiar != null)
                    {
                        PUcopiar.IdPrecioUnitarioBase = registro.IdPrecioUnitarioBase;

                        await CopiarPUXCodigo(PUcopiar, _dbContex);
                    }
                    return await ObtenerPrecioUnitario(registro.IdProyecto);
                }
                var registrosSinEstructurar = await ObtenerPrecioUnitarioSinEstructurar(registro.IdProyecto);
                var codigoExistente = registrosSinEstructurar.Where(z => z.Codigo == registro.Codigo).ToList();
                if (codigoExistente.Count() > 0) {
                    return await ObtenerPrecioUnitario(registro.IdProyecto);
                }
                ConceptoDTO concepto = new ConceptoDTO();
                concepto.Codigo = registro.Codigo;
                concepto.Descripcion = registro.Descripcion;
                concepto.Unidad = registro.Unidad;
                concepto.IdEspecialidad = null;
                concepto.CostoUnitario = registro.CostoUnitario;
                concepto.IdProyecto = registro.IdProyecto;
                var nuevoConcepto = await _ConceptoService.CrearYObtener(concepto);
                registro.IdConcepto = nuevoConcepto.Id;
                var registrosFiltrados = registrosSinEstructurar.Where(z => z.IdPrecioUnitarioBase == registro.IdPrecioUnitarioBase);
                registro.Posicion = registrosFiltrados.Count() + 1;
                var nuevoRegistro = await _PrecioUnitarioService.CrearYObtener(registro);
                var Proyecto = await _ProyectoService.ObtenXId(registro.IdProyecto);
                ProgramacionEstimadaGanttDTO nuevaProgramacion = new ProgramacionEstimadaGanttDTO();
                //ProgramacionEstimadaDTO nuevaProgramacion = new ProgramacionEstimadaDTO();
                nuevaProgramacion.IdProyecto = Proyecto.Id;
                nuevaProgramacion.IdPrecioUnitario = nuevoRegistro.Id;
                nuevaProgramacion.IdConcepto = nuevoRegistro.IdConcepto;
                nuevaProgramacion.Start = Proyecto.FechaInicio;
                nuevaProgramacion.End = Proyecto.FechaInicio.AddDays(1);
                nuevaProgramacion.Duracion = 1;
                nuevaProgramacion.Progress = 0;
                nuevaProgramacion.Comando = 0;
                nuevaProgramacion.DesfaseComando = 0;
                nuevaProgramacion.Parent = null;
                if (nuevoRegistro.IdPrecioUnitarioBase != 0)
                {
                    var programacionesEstimadas = await _ProgramacionEstimadaGanttService.ObtenerXIdProyecto(Proyecto.Id, db);
                    var programacionEstimadaPadre = programacionesEstimadas.Where(z => z.IdPrecioUnitario == nuevoRegistro.IdPrecioUnitarioBase).FirstOrDefault();
                    nuevaProgramacion.Parent = Convert.ToString(programacionEstimadaPadre.Id);
                }
                else
                {
                    nuevaProgramacion.Parent = "0";
                }
                await _ProgramacionEstimadaGanttService.CrearYObtener(nuevaProgramacion);
                var lista = await ObtenerPrecioUnitario(nuevoRegistro.IdProyecto);
                return lista;
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
                return new List<PrecioUnitarioDTO>();
            }
        }

        public async Task<ActionResult<List<PrecioUnitarioDTO>>> CrearYObtenerCopia(PrecioUnitarioDTO registro)
        {
            try
            {
                ConceptoDTO concepto = new ConceptoDTO();
                var conceptos = await _ConceptoService.ObtenerTodos(registro.IdProyecto);
                var existeConcepto = conceptos.Where(z => z.Descripcion.ToLower() == registro.Descripcion.ToLower()).ToList();
                if (existeConcepto.Count > 0)
                {
                    var conceptoEncontrado = existeConcepto.FirstOrDefault();
                    concepto.Codigo = conceptoEncontrado.Codigo;
                    concepto.Descripcion = conceptoEncontrado.Descripcion;
                    concepto.Unidad = conceptoEncontrado.Unidad;
                    concepto.IdEspecialidad = null;
                    concepto.CostoUnitario = conceptoEncontrado.CostoUnitario;
                    concepto.IdProyecto = conceptoEncontrado.IdProyecto;
                    registro.IdConcepto = conceptoEncontrado.Id;
                }
                else
                {
                    concepto.Codigo = registro.Codigo;
                    concepto.Descripcion = registro.Descripcion;
                    concepto.Unidad = registro.Unidad;
                    concepto.IdEspecialidad = null;
                    concepto.CostoUnitario = registro.CostoUnitario;
                    concepto.IdProyecto = registro.IdProyecto;
                    var nuevoConcepto = await _ConceptoService.CrearYObtener(concepto);
                    registro.IdConcepto = nuevoConcepto.Id;
                }
                var nuevoRegistro = await _PrecioUnitarioService.CrearYObtener(registro);
                var Proyecto = await _ProyectoService.ObtenXId(registro.IdProyecto);
                ProgramacionEstimadaDTO nuevaProgramacion = new ProgramacionEstimadaDTO();
                nuevaProgramacion.IdConcepto = nuevoRegistro.IdConcepto;
                nuevaProgramacion.Inicio = Proyecto.FechaInicio;
                nuevaProgramacion.Termino = Proyecto.FechaInicio;
                nuevaProgramacion.IdProyecto = Proyecto.Id;
                nuevaProgramacion.IdPrecioUnitario = nuevoRegistro.Id;
                nuevaProgramacion.DiasTranscurridos = 1;
                nuevaProgramacion.Nivel = nuevoRegistro.Nivel;
                nuevaProgramacion.Progreso = 0;
                nuevaProgramacion.IdPredecesora = 0;
                if (nuevoRegistro.IdPrecioUnitarioBase != 0)
                {
                    var programacionesEstimadas = await _ProgramacionEstimadaService.ObtenerTodosXProyecto(Proyecto.Id);
                    var programacionEstimadaPadre = programacionesEstimadas.Where(z => z.IdPrecioUnitario == nuevoRegistro.IdPrecioUnitarioBase).FirstOrDefault();
                    nuevaProgramacion.IdPadre = programacionEstimadaPadre.Id;
                }
                else
                {
                    nuevaProgramacion.IdPadre = 0;
                }
                await _ProgramacionEstimadaService.CrearYObtener(nuevaProgramacion);
                nuevaProgramacion.Predecesor = "";
                var lista = await ObtenerPrecioUnitario(nuevoRegistro.IdProyecto);
                return lista;
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
                return new List<PrecioUnitarioDTO>();
            }
        }

        public async Task<ActionResult<List<PrecioUnitarioDTO>>> Editar(PrecioUnitarioDTO registro)
        {
            try
            {
                ConceptoDTO concepto = new ConceptoDTO();
                var obtenconcepto = await _ConceptoService.ObtenXId(registro.IdConcepto);
                if (obtenconcepto.Unidad != registro.Unidad)
                {
                    var eliminarG = await _GeneradoresService.EliminarTodos(registro.Id);
                }
                concepto.Id = registro.IdConcepto;
                concepto.Codigo = registro.Codigo;
                concepto.Descripcion = registro.Descripcion;
                concepto.Unidad = registro.Unidad;
                concepto.CostoUnitario = registro.CostoUnitario;
                concepto.IdProyecto = registro.IdProyecto;
                concepto.PorcentajeIndirecto = registro.PorcentajeIndirecto;
                var conceptoEditado = await _ConceptoService.Editar(concepto);
                var precioUnitario = await _PrecioUnitarioService.ObtenXId(registro.Id);
                var generadores = await _GeneradoresService.ObtenerTodosXIdPrecioUnitario(registro.Id);
                decimal cantidadTotal = 0;
                for (int i = 0; i < generadores.Count; i++)
                {
                    cantidadTotal = cantidadTotal + generadores[i].X * generadores[i].Y * generadores[i].Z * generadores[i].Cantidad;
                }
                if (cantidadTotal != registro.Cantidad)
                {
                    for (int i = 0; i < generadores.Count; i++)
                    {
                        await _GeneradoresService.Eliminar(generadores[i].Id);
                    }
                }
                await _PrecioUnitarioService.Editar(registro);
                await RecalcularPrecioUnitario(registro);
                var lista = await ObtenerPrecioUnitario(registro.IdProyecto);
                return lista;
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
                return new List<PrecioUnitarioDTO>();
            }
        }

        public async Task<List<PrecioUnitarioDTO>> Eliminar(int Id)
        {
            return new List<PrecioUnitarioDTO>();
        }

        public async Task<RespuestaDTO> EliminarPU(int Id)
        {
            try
            {
                var respuesta = new RespuestaDTO();

                var precioUnitario = await _PrecioUnitarioService.ObtenXId(Id);
                var registros = await _PrecioUnitarioService.ObtenerTodos(precioUnitario.IdProyecto);
                //for (int i = 0; i < registros.Count; i++)
                //{
                //    registros[i].Expandido = true;
                //}
                var hijos = registros.Where(z => z.IdPrecioUnitarioBase == precioUnitario.Id).ToList();
                var programacionE = await _ProgramacionEstimadaGanttService.ObtenerXIdProyecto(precioUnitario.IdProyecto, _dbContex);
                var PE = new ProgramacionEstimadaGanttDTO();
                if (hijos.Count > 0)
                {
                    for (int i = 0; i < hijos.Count; i++)
                    {
                        respuesta = await PrecioUnitarioHijos(hijos[i].Id, registros);
                        if (!respuesta.Estatus)
                        {
                            return respuesta;
                        }
                    }

                    for (int i = 0; i < hijos.Count; i++)
                    {
                        await EliminarPrecioUnitarioHijos(hijos[i].Id, registros, programacionE);

                        //await EliminarDetallesPorPrecioUnitario(hijos[i].Id);
                        //PE = programacionE.Where(z => z.IdPrecioUnitario == hijos[i].Id).First();
                        //await _ProgramacionEstimadaGanttService.Eliminar(Convert.ToInt32(PE.Id));
                        //await _GeneradoresService.EliminarTodos(hijos[i].Id);
                        //await _PrecioUnitarioService.Eliminar(hijos[i].Id);
                    }
                }
                else
                {
                    var existenEstimaciones = await _estimacionesService.ObtenXIdPrecioUnitario(Id);
                    if (existenEstimaciones.Count() > 0)
                    {
                        respuesta.Estatus = false;
                        respuesta.Descripcion = "Error, ya existe una o mas estimaciones";
                        return respuesta;
                    }
                    var existeEmpleado = await _precioUnitarioXEmpleadoService.ObtenerXIdPrecioUnitario(Id);
                    if (existeEmpleado.Count() > 0)
                    {
                        respuesta.Estatus = false;
                        respuesta.Descripcion = "Error, ya hay una relación con uno o mas empleados";
                        return respuesta;
                    }
                    var existeDetalleXContrato = await _detalleXContratoService.ObtenerRegistrosXIdPrecioUnitario(Id);
                    if (existeDetalleXContrato.Count() > 0)
                    {
                        respuesta.Estatus = false;
                        respuesta.Descripcion = "Error, ya existe relacion con algún contrato";
                        return respuesta;
                    }

                }
                await EliminarDetallesPorPrecioUnitario(Id);
                if (programacionE.Count > 0)
                {
                    PE = programacionE.Where(z => z.IdPrecioUnitario == Id).First();
                    if (PE != null)
                    {
                        await _ProgramacionEstimadaGanttService.Eliminar(Convert.ToInt32(PE.Id));
                    }
                }
                await _GeneradoresService.EliminarTodos(Id);
                await _PrecioUnitarioService.Eliminar(Id);

                respuesta.Estatus = true;
                respuesta.Descripcion = "Eliminacion correacta";
                return respuesta;
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
                return new RespuestaDTO();
            }
        }

        public async Task<RespuestaDTO> PrecioUnitarioHijos(int IdPrecioUnitario, List<PrecioUnitarioDTO> registros)
        {
            var respuesta = new RespuestaDTO();
            var hijos = registros.Where(z => z.IdPrecioUnitarioBase == IdPrecioUnitario).ToList();
            if (hijos.Count() <= 0)
            {
                var existenEstimaciones = await _estimacionesService.ObtenXIdPrecioUnitario(IdPrecioUnitario);
                if (existenEstimaciones.Count() > 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Error, ya existe una o mas estimaciones";
                    return respuesta;
                }
                var existeEmpleado = await _precioUnitarioXEmpleadoService.ObtenerXIdPrecioUnitario(IdPrecioUnitario);
                if (existeEmpleado.Count() > 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Error, ya hay una relación con uno o mas empleados";
                    return respuesta;
                }
                var existeDetalleXContrato = await _detalleXContratoService.ObtenerRegistrosXIdPrecioUnitario(IdPrecioUnitario);
                if (existeDetalleXContrato.Count() > 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Error, ya existe relacion con algún contrato";
                    return respuesta;
                }
            }
            foreach (var hijo in hijos)
            {
                await PrecioUnitarioHijos(hijo.Id, registros);
            }

            respuesta.Estatus = true;
            respuesta.Descripcion = "Listo para eliminar";

            return respuesta;
        }

        public async Task<RespuestaDTO> EliminarPrecioUnitarioHijos(int IdPrecioUnitario, List<PrecioUnitarioDTO> registros, List<ProgramacionEstimadaGanttDTO> progrmaciones)
        {
            var respuesta = new RespuestaDTO();
            var hijos = registros.Where(z => z.IdPrecioUnitarioBase == IdPrecioUnitario).ToList();
            if (hijos.Count() <= 0)
            {
                await EliminarDetallesPorPrecioUnitario(IdPrecioUnitario);
                var PE = progrmaciones.Where(z => z.IdPrecioUnitario == IdPrecioUnitario).First();
                await _ProgramacionEstimadaGanttService.Eliminar(Convert.ToInt32(PE.Id));
                await _GeneradoresService.EliminarTodos(IdPrecioUnitario);
                await _PrecioUnitarioService.Eliminar(IdPrecioUnitario);
            }
            else
            {
                foreach (var hijo in hijos)
                {
                    await EliminarPrecioUnitarioHijos(hijo.Id, registros, progrmaciones);
                }
            }

            respuesta.Estatus = true;
            respuesta.Descripcion = "Listo para eliminar";

            return respuesta;
        }




        public async Task EliminarDetallesPorPrecioUnitario(int IdPrecioUnitario)
        {
            var detalles = await _PrecioUnitarioDetalleService.ObtenerTodosXIdPrecioUnitario(IdPrecioUnitario);
            if (detalles.Count > 0)
            {
                for (int i = 0; i < detalles.Count; i++)
                {
                    await _PrecioUnitarioDetalleService.Eliminar(detalles[i].Id);
                }
            }
        }



        //public async Task<List<PrecioUnitarioDetalleDTO>> ObtenerDetalles(int IdPrecioUnitario)
        //{
        //    var lista = await _PrecioUnitarioDetalleService.ObtenerTodosXIdPrecioUnitario(IdPrecioUnitario);
        //    var PU = await _PrecioUnitarioService.ObtenXId(IdPrecioUnitario);
        //    var insumos = await _InsumoService.ObtenXIdProyecto(PU.IdProyecto);
        //    var listaFiltrada = lista.Where(z => z.IdPrecioUnitarioDetallePerteneciente == 0).ToList();
        //    for(int i = 0; i < listaFiltrada.Count; i++)
        //    {
        //        var insumo = insumos.Where(z => z.id == listaFiltrada[i].IdInsumo).FirstOrDefault();
        //        listaFiltrada[i].Codigo = insumo.Codigo;
        //        listaFiltrada[i].Descripcion = insumo.Descripcion;
        //        listaFiltrada[i].Unidad = insumo.Unidad;
        //        listaFiltrada[i].CostoUnitario = insumo.CostoUnitario;
        //        listaFiltrada[i].IdTipoInsumo = insumo.idTipoInsumo;
        //        listaFiltrada[i].IdFamiliaInsumo = insumo.idFamiliaInsumo;
        //        listaFiltrada[i].Importe = listaFiltrada[i].Cantidad * listaFiltrada[i].CostoUnitario;
        //        listaFiltrada[i].ImporteConFormato = String.Format("{0:#,##0.00}", listaFiltrada[i].Importe);
        //        listaFiltrada[i].CantidadConFormato = String.Format("{0:#,##0.0000}", listaFiltrada[i].Cantidad);
        //        listaFiltrada[i].CostoUnitarioConFormato = String.Format("{0:#,##0.00}", listaFiltrada[i].CostoUnitario);
        //    }
        //    return listaFiltrada;
        //}

        public async Task<List<PrecioUnitarioDetalleDTO>> ObtenerDetalles(int IdPrecioUnitario, DbContext db)
        {
            var items = db.Database.SqlQueryRaw<string>(""""
                select 
                PUD.Id,
                PUD.IdPrecioUnitario,
                PUD.IdInsumo,
                PUD.EsCompuesto,
                I.CostoUnitario,
                FORMAT(I.CostoUnitario, 'N', 'en-us') as CostoUnitarioConFormato,
                I.CostoBase,
                FORMAT(I.CostoBase, 'N', 'en-us') as CostoBaseConFormato,
                PUD.Cantidad,
                FORMAT(PUD.Cantidad, '0.0000') as CantidadConFormato,
                PUD.IdPrecioUnitarioDetallePerteneciente,
                I.Codigo,
                I.Descripcion,
                I.Unidad,
                I.IdTipoInsumo,
                I.IdFamiliaInsumo,
                PUD.Cantidad * I.CostoUnitario as Importe,
                FORMAT(PUD.Cantidad * I.CostoUnitario, 'N', 'en-us') as ImporteConFormato
                from PrecioUnitarioDetalle PUD
                Join Insumo I
                on PUD.IdInsumo = I.Id
                where PUD.IdPrecioUnitario = 
                """" + IdPrecioUnitario +
                """" and PUD.IdPrecioUnitarioDetallePerteneciente = 0 for json path"""").ToList();
            if (items.Count <= 0)
            {
                return new List<PrecioUnitarioDetalleDTO>();
            }
            string json = string.Join("", items);
            var datos = JsonSerializer.Deserialize<List<PrecioUnitarioDetalleDTO>>(json);
            return datos;
        }

        public async Task<List<PrecioUnitarioDetalleDTO>> ObtenerDetallesParaProgramacionEstimada(int IdPrecioUnitario)
        {
            var PU = await _PrecioUnitarioService.ObtenXId(IdPrecioUnitario);
            var lista = await _PrecioUnitarioDetalleService.ObtenerTodosXIdPrecioUnitario(IdPrecioUnitario);
            var insumos = await _InsumoService.ObtenXIdProyecto(PU.IdProyecto);
            var listaFiltrada = lista;
            for (int i = 0; i < listaFiltrada.Count; i++)
            {
                var insumo = insumos.Where(z => z.id == listaFiltrada[i].IdInsumo).FirstOrDefault();
                listaFiltrada[i].Codigo = insumo.Codigo;
                listaFiltrada[i].Descripcion = insumo.Descripcion;
                listaFiltrada[i].Unidad = insumo.Unidad;
                listaFiltrada[i].CostoUnitario = insumo.CostoUnitario;
                listaFiltrada[i].IdTipoInsumo = insumo.idTipoInsumo;
                listaFiltrada[i].IdFamiliaInsumo = insumo.idFamiliaInsumo;
                listaFiltrada[i].Importe = listaFiltrada[i].Cantidad * listaFiltrada[i].CostoUnitario;
                listaFiltrada[i].ImporteConFormato = String.Format("{0:#,##0.00}", listaFiltrada[i].Importe);
                listaFiltrada[i].CantidadConFormato = String.Format("{0:#,##0.0000}", listaFiltrada[i].Cantidad);
                listaFiltrada[i].CostoUnitarioConFormato = String.Format("{0:#,##0.00}", listaFiltrada[i].CostoUnitario);
            }
            return listaFiltrada;
        }

        public async Task<List<PrecioUnitarioDetalleDTO>> ObtenerDetallesPorPU(int IdPrecioUnitario, DbContext db)
        {
            var items = db.Database.SqlQueryRaw<string>(""""
                select 
                PUD.Id,
                PUD.IdPrecioUnitario,
                PUD.IdInsumo,
                PUD.EsCompuesto,
                I.CostoUnitario,
                FORMAT(I.CostoUnitario, 'N', 'en-us') as CostoUnitarioConFormato,
                I.CostoBase,
                FORMAT(I.CostoBase, 'N', 'en-us') as CostoBaseConFormato,
                PUD.Cantidad,
                FORMAT(PUD.Cantidad, '0.0000') as CantidadConFormato,
                PUD.IdPrecioUnitarioDetallePerteneciente,
                I.Codigo,
                I.Descripcion,
                I.Unidad,
                I.IdTipoInsumo,
                I.IdFamiliaInsumo,
                PUD.Cantidad * I.CostoUnitario as Importe,
                FORMAT(PUD.Cantidad * I.CostoUnitario, 'N', 'en-us') as ImporteConFormato
                from PrecioUnitarioDetalle PUD
                Join Insumo I
                on PUD.IdInsumo = I.Id
                where PUD.IdPrecioUnitario = 
                """" + IdPrecioUnitario +
                """" for json path"""").ToList();
            if (items.Count <= 0)
            {
                return new List<PrecioUnitarioDetalleDTO>();
            }
            string json = string.Join("", items);
            var datos = JsonSerializer.Deserialize<List<PrecioUnitarioDetalleDTO>>(json);
            return datos;
        }

        public async Task<List<PrecioUnitarioDetalleDTO>> ObtenerDetallesPorIdInsumo(int IdInsumo, DbContext db)
        {
            var items = db.Database.SqlQueryRaw<string>(""""
                select 
                PUD.Id,
                PUD.IdPrecioUnitario,
                PUD.IdInsumo,
                PUD.EsCompuesto,
                I.CostoUnitario,
                FORMAT(I.CostoUnitario, 'N', 'en-us') as CostoUnitarioConFormato,
                I.CostoBase,
                FORMAT(I.CostoBase, 'N', 'en-us') as CostoBaseConFormato,
                PUD.Cantidad,
                FORMAT(PUD.Cantidad, '0.0000') as CantidadConFormato,
                PUD.IdPrecioUnitarioDetallePerteneciente,
                I.Codigo,
                I.Descripcion,
                I.Unidad,
                I.IdTipoInsumo,
                I.IdFamiliaInsumo,
                PUD.Cantidad * I.CostoUnitario as Importe,
                FORMAT(PUD.Cantidad * I.CostoUnitario, 'N', 'en-us') as ImporteConFormato
                from PrecioUnitarioDetalle PUD
                Join Insumo I
                on PUD.IdInsumo = I.Id
                where PUD.IdInsumo = 
                """" + IdInsumo +
                """" for json path"""").ToList();
            if (items.Count <= 0)
            {
                return new List<PrecioUnitarioDetalleDTO>();
            }
            string json = string.Join("", items);
            var datos = JsonSerializer.Deserialize<List<PrecioUnitarioDetalleDTO>>(json);
            return datos;
        }

        public async Task<List<PrecioUnitarioDetalleDTO>> ObtenerDetallesPorTipoInsumo(int IdPrecioUnitario, int IdTipoInsumo, DbContext db)
        {
            var registros = await ObtenerDetalles(IdPrecioUnitario, db);
            return registros;
        }

        //public async Task<ActionResult<List<PrecioUnitarioDetalleDTO>>> ObtenerDetallesHijos(PrecioUnitarioDetalleDTO registro)
        //{
        //    var PU = await _PrecioUnitarioService.ObtenXId(registro.IdPrecioUnitario);
        //    var insumos = await _InsumoService.ObtenXIdProyecto(PU.IdProyecto);
        //    var lista = await _PrecioUnitarioDetalleService.ObtenerTodosXIdPrecioUnitario(registro.IdPrecioUnitario);
        //    var listaFiltrada = lista.Where(z => z.IdPrecioUnitarioDetallePerteneciente == registro.Id).ToList();
        //    for (int i = 0; i < listaFiltrada.Count; i++)
        //    {
        //        var insumo = insumos.Where(z => z.id == listaFiltrada[i].IdInsumo).FirstOrDefault();
        //        listaFiltrada[i].Codigo = insumo.Codigo;
        //        listaFiltrada[i].Descripcion = insumo.Descripcion;
        //        listaFiltrada[i].Unidad = insumo.Unidad;
        //        listaFiltrada[i].CostoUnitario = insumo.CostoUnitario;
        //        listaFiltrada[i].IdTipoInsumo = insumo.idTipoInsumo;
        //        listaFiltrada[i].IdFamiliaInsumo = insumo.idFamiliaInsumo;
        //        listaFiltrada[i].Importe = listaFiltrada[i].Cantidad * listaFiltrada[i].CostoUnitario;
        //        listaFiltrada[i].ImporteConFormato = String.Format("{0:#,##0.00}", listaFiltrada[i].Importe);
        //        listaFiltrada[i].CantidadConFormato = String.Format("{0:#,##0.0000}", listaFiltrada[i].Cantidad);
        //        listaFiltrada[i].CostoUnitarioConFormato = String.Format("{0:#,##0.00}", listaFiltrada[i].CostoUnitario);
        //    }
        //    return listaFiltrada;
        //}

        public async Task<List<PrecioUnitarioDetalleDTO>> ObtenerDetallesHijos(PrecioUnitarioDetalleDTO registro, DbContext db)
        {
            //var items = db.Database.SqlQueryRaw<string>(""""select Id, IdPrecioUnitario, IdInsumo, EsCompuesto, Cantidad, CantidadExcedente, IdPrecioUnitarioDetallePerteneciente from PrecioUnitarioDetalle where IdPrecioUnitario = '"""" + registro.IdPrecioUnitario + """"' for json path"""").ToList();
            //if (items.Count() <= 0)
            //{
            //    return new List<PrecioUnitarioDetalleDTO>();
            //}
            //string json = string.Join("", items);
            //var datos = JsonSerializer.Deserialize<List<PrecioUnitarioDetalle>>(json);
            //var lista = _Mapper.Map<List<PrecioUnitarioDetalleDTO>>(datos);
            var lista = await ObtenerDetallesPorPU(registro.IdPrecioUnitario, _dbContex);
            //var PU = await _PrecioUnitarioService.ObtenXId(registro.IdPrecioUnitario);
            //var insumos = await _InsumoService.ObtenXIdProyecto(PU.IdProyecto);
            var listaFiltrada = lista.Where(z => z.IdPrecioUnitarioDetallePerteneciente == registro.Id).ToList();
            return listaFiltrada;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<ActionResult<List<PrecioUnitarioDetalleDTO>>> CrearYObtenerDetalle(PrecioUnitarioDetalleDTO registro)
        {
            try
            {
                PrecioUnitarioDetalleDTO nuevoRegistro = new PrecioUnitarioDetalleDTO();
                var precioUnitario = await _PrecioUnitarioService.ObtenXId(registro.IdPrecioUnitario);
                var FSR = await _FSRService.ObtenerTodosXProyecto(precioUnitario.IdProyecto);
                if (registro.IdTipoInsumo == 10006)
                {
                    registro.EsCompuesto = true;
                    registro.CostoUnitario = 0;
                }

                if (registro.IdTipoInsumo == 10000)
                {
                    registro.CostoUnitario = registro.CostoBase * FSR[0].PorcentajeFsr;
                    if (FSR.Count > 0)
                    {
                        var factorSR = FSR.FirstOrDefault();

                        if (!factorSR.EsCompuesto)
                        {
                            registro.CostoUnitario = registro.CostoBase * factorSR.PorcentajeFsr;
                        }
                        else
                        {
                            registro.CostoBase = registro.CostoBase;
                            registro.CostoUnitario = await _factorSalarioRealProceso.obtenerCostoUnitarioConFsrXCostoBase(registro.CostoBase, factorSR);
                        }
                    }
                    else
                    {
                        registro.CostoUnitario = registro.CostoBase;
                    }
                }
                if (registro.IdTipoInsumo == 10001)
                {
                    registro.IdInsumo = 0;
                    var TodosDetalles = ObtenerDetallesPorPU(precioUnitario.Id, _dbContex).Result;
                    var DetallesParaCalcularPorcentaje = TodosDetalles.Where(z => z.IdPrecioUnitarioDetallePerteneciente == registro.IdPrecioUnitarioDetallePerteneciente).ToList();
                    decimal costo = 0;
                    var insumos = _InsumoService.ObtenXIdProyecto(precioUnitario.IdProyecto).Result;
                    for (int i = 0; i < DetallesParaCalcularPorcentaje.Count; i++)
                    {
                        var insumo = insumos.Where(z => z.id == DetallesParaCalcularPorcentaje[i].IdInsumo).FirstOrDefault();
                        if (insumo.idTipoInsumo == 10000)
                        {
                            costo = costo + DetallesParaCalcularPorcentaje[i].Cantidad * insumo.CostoUnitario;
                        }
                    }
                    registro.CostoUnitario = costo;
                }
                if (registro.IdInsumo == 0)
                {
                    var PU = await _PrecioUnitarioService.ObtenXId(registro.IdPrecioUnitario);
                    InsumoCreacionDTO insumo = new InsumoCreacionDTO();
                    insumo.Codigo = registro.Codigo;
                    insumo.Descripcion = registro.Descripcion;
                    insumo.Unidad = registro.Unidad;
                    insumo.idTipoInsumo = registro.IdTipoInsumo;
                    insumo.CostoBase = registro.CostoBase;
                    if (insumo.idTipoInsumo != 10000 && insumo.idTipoInsumo != 10001 && insumo.idTipoInsumo != 10006)
                    {
                        insumo.CostoUnitario = registro.CostoBase;
                    }
                    else
                    {
                        insumo.CostoUnitario = registro.CostoUnitario;
                    }
                    insumo.IdProyecto = PU.IdProyecto;
                    if (registro.IdFamiliaInsumo == 0)
                    {
                        insumo.idFamiliaInsumo = null;
                    }
                    else
                    {
                        insumo.idFamiliaInsumo = registro.IdFamiliaInsumo;
                    } 
                    var nuevoInsumo = await _InsumoService.CrearYObtener(insumo);
                    registro.IdInsumo = nuevoInsumo.id;
                    var PrecioUnitario = await _PrecioUnitarioService.ObtenXId(registro.IdPrecioUnitario);
                    var PreciosSinFiltrar = await ObtenerPrecioUnitarioSinEstructurar(PrecioUnitario.IdProyecto);
                    var PreciosFiltrados = PreciosSinFiltrar.Where(z => z.IdConcepto == PrecioUnitario.IdConcepto).ToList();
                    foreach(var partida in PreciosFiltrados)
                    {
                        registro.IdPrecioUnitario = partida.Id;
                        nuevoRegistro = await _PrecioUnitarioDetalleService.CrearYObtener(registro);
                        if (registro.IdTipoInsumo == 10000)
                        {
                            await RecalcularPorcentajeManoDeObra(nuevoRegistro);
                        }
                        var detalles = await ObtenerDetallesPorPU(precioUnitario.Id, _dbContex);
                        var insumos = await _InsumoService.ObtenXIdProyecto(precioUnitario.IdProyecto); //Era id del proyecto pero no lo obtengo
                        var detallesFiltrados = detalles.Where(z => z.IdPrecioUnitarioDetallePerteneciente == registro.IdPrecioUnitarioDetallePerteneciente).ToList();
                        var datos = await RecalcularDetalles(nuevoRegistro.IdPrecioUnitario, detalles, insumos);
                        for (int i = 0; i < detallesFiltrados.Count; i++)
                        {
                            var dato = datos.Detalles.Where(z => z.Id == detallesFiltrados[i].Id).FirstOrDefault();
                            detallesFiltrados[i] = dato;
                        }
                        insumos = datos.Insumos;
                        var concepto = await _ConceptoService.ObtenXId(precioUnitario.IdConcepto);
                        concepto.CostoUnitario = datos.Total;
                        await _ConceptoService.Editar(concepto);
                        await RecalcularPrecioUnitario(precioUnitario);
                    }
                    var DetallesParaRetornar = await ObtenerDetallesPorPU(precioUnitario.Id, _dbContex);
                    var detallesFiltrados1 = DetallesParaRetornar.Where(z => z.IdPrecioUnitarioDetallePerteneciente == registro.IdPrecioUnitarioDetallePerteneciente).ToList();
                    return detallesFiltrados1;
                }
                else
                {
                    //var Insumos = await _InsumoService.ObtenXIdProyecto(precioUnitario.IdProyecto);
                    var DetallesFiltrados = await ObtenerDetallesPorIdInsumo(registro.IdInsumo, _dbContex);
                    if (DetallesFiltrados.Count > 0)
                    {
                        var Detalle = DetallesFiltrados.FirstOrDefault();
                        if (Detalle.EsCompuesto == true)
                        {
                            registro.EsCompuesto = true;
                        }
                        else
                        {
                            registro.EsCompuesto = false;
                        }
                        var RegistroPadre = DetallesFiltrados.Where(z => z.Id == registro.IdPrecioUnitarioDetallePerteneciente).ToList();
                        if (RegistroPadre.Count() > 0)
                        {
                            if (RegistroPadre[0].IdInsumo != registro.IdInsumo)
                            {
                                var PrecioUnitario = await _PrecioUnitarioService.ObtenXId(registro.IdPrecioUnitario);
                                var PreciosSinFiltrar = await ObtenerPrecioUnitarioSinEstructurar(PrecioUnitario.IdProyecto);
                                var PreciosFiltrados = PreciosSinFiltrar.Where(z => z.IdConcepto == PrecioUnitario.IdConcepto).ToList();
                                foreach(var partida in PreciosFiltrados)
                                {
                                    registro.IdPrecioUnitario = partida.Id;
                                    nuevoRegistro = await _PrecioUnitarioDetalleService.CrearYObtener(registro);
                                    if (Detalle.EsCompuesto == true)
                                    {
                                        var DetllesPU = await ObtenerDetallesPorPU(Detalle.IdPrecioUnitario, _dbContex);
                                        //var DetllesPU = Detalles.Where(z => z.IdPrecioUnitario == Detalle.IdPrecioUnitario).ToList();
                                        var DetallesHijos = DetllesPU.Where(z => z.IdPrecioUnitarioDetallePerteneciente == Detalle.Id).ToList();
                                        for (int i = 0; i < DetallesHijos.Count; i++)
                                        {
                                            PrecioUnitarioDetalleDTO DetalleAuxiliar = new PrecioUnitarioDetalleDTO();
                                            DetalleAuxiliar.Cantidad = DetallesHijos[i].Cantidad;
                                            DetalleAuxiliar.EsCompuesto = DetallesHijos[i].EsCompuesto;
                                            DetalleAuxiliar.Id = DetallesHijos[i].Id;
                                            DetalleAuxiliar.IdPrecioUnitario = DetallesHijos[i].IdPrecioUnitario;
                                            DetalleAuxiliar.IdInsumo = DetallesHijos[i].IdInsumo;
                                            DetalleAuxiliar.CantidadExcedente = DetallesHijos[i].CantidadExcedente;
                                            DetalleAuxiliar.IdPrecioUnitarioDetallePerteneciente = DetallesHijos[i].IdPrecioUnitarioDetallePerteneciente;
                                            DetallesHijos[i].Id = 0;
                                            DetallesHijos[i].IdPrecioUnitario = registro.IdPrecioUnitario;
                                            DetallesHijos[i].IdPrecioUnitarioDetallePerteneciente = nuevoRegistro.Id;
                                            if (DetallesHijos[i].IdTipoInsumo == 10001)
                                            {
                                                var insumoNuevo = new InsumoCreacionDTO();
                                                insumoNuevo.IdProyecto = precioUnitario.IdProyecto;
                                                insumoNuevo.Codigo = DetallesHijos[i].Codigo;
                                                insumoNuevo.Descripcion = DetallesHijos[i].Descripcion;
                                                insumoNuevo.Unidad = DetallesHijos[i].Unidad;
                                                insumoNuevo.CostoUnitario = DetallesHijos[i].CostoUnitario;
                                                insumoNuevo.idTipoInsumo = DetallesHijos[i].IdTipoInsumo;
                                                var insumoCreadoNuevo = await _InsumoService.CrearYObtener(insumoNuevo);
                                                DetallesHijos[i].IdInsumo = insumoCreadoNuevo.id;
                                            }
                                            var nuevoDetalle = await _PrecioUnitarioDetalleService.CrearYObtener(DetallesHijos[i]);
                                            if (DetallesHijos[i].IdTipoInsumo == 10000 && DetallesHijos[i].EsCompuesto == false)
                                            {
                                                await RecalcularPorcentajeManoDeObra(nuevoDetalle);
                                            }
                                            if (DetallesHijos[i].EsCompuesto == true)
                                            {
                                                await CrearDetallesHijosCompuestoExistente(DetalleAuxiliar, nuevoDetalle, precioUnitario.IdProyecto);
                                            }
                                        }
                                    }
                                    var detalles = await ObtenerDetallesPorPU(partida.Id, _dbContex);
                                    var insumos = await _InsumoService.ObtenXIdProyecto(precioUnitario.IdProyecto); //Era id del proyecto pero no lo obtengo
                                    var datos = await RecalcularDetalles(nuevoRegistro.IdPrecioUnitario, detalles, insumos);
                                    var concepto = await _ConceptoService.ObtenXId(partida.IdConcepto);
                                    concepto.CostoUnitario = datos.Total;
                                    await _ConceptoService.Editar(concepto);
                                    await RecalcularPrecioUnitario(partida);
                                }
                                
                            }
                        }
                        else
                        {
                            var PrecioUnitario = await _PrecioUnitarioService.ObtenXId(registro.IdPrecioUnitario);
                            var PreciosSinFiltrar = await ObtenerPrecioUnitarioSinEstructurar(PrecioUnitario.IdProyecto);
                            var PreciosFiltrados = PreciosSinFiltrar.Where(z => z.IdConcepto == PrecioUnitario.IdConcepto).ToList();
                            foreach(var partida in PreciosFiltrados)
                            {
                                registro.IdPrecioUnitario = partida.Id;
                                nuevoRegistro = await _PrecioUnitarioDetalleService.CrearYObtener(registro);
                                if (Detalle.EsCompuesto == true)
                                {
                                    var DetllesPU = await ObtenerDetallesPorPU(Detalle.IdPrecioUnitario, _dbContex);
                                    //var DetllesPU = Detalles.Where(z => z.IdPrecioUnitario == Detalle.IdPrecioUnitario).ToList();
                                    var DetallesHijos = DetllesPU.Where(z => z.IdPrecioUnitarioDetallePerteneciente == Detalle.Id).ToList();
                                    for (int i = 0; i < DetallesHijos.Count; i++)
                                    {
                                        PrecioUnitarioDetalleDTO DetalleAuxiliar = new PrecioUnitarioDetalleDTO();
                                        DetalleAuxiliar.Cantidad = DetallesHijos[i].Cantidad;
                                        DetalleAuxiliar.EsCompuesto = DetallesHijos[i].EsCompuesto;
                                        DetalleAuxiliar.Id = DetallesHijos[i].Id;
                                        DetalleAuxiliar.IdPrecioUnitario = DetallesHijos[i].IdPrecioUnitario;
                                        DetalleAuxiliar.IdInsumo = DetallesHijos[i].IdInsumo;
                                        DetalleAuxiliar.CantidadExcedente = DetallesHijos[i].CantidadExcedente;
                                        DetalleAuxiliar.IdPrecioUnitarioDetallePerteneciente = DetallesHijos[i].IdPrecioUnitarioDetallePerteneciente;
                                        DetallesHijos[i].Id = 0;
                                        DetallesHijos[i].IdPrecioUnitario = registro.IdPrecioUnitario;
                                        DetallesHijos[i].IdPrecioUnitarioDetallePerteneciente = nuevoRegistro.Id;
                                        if (DetallesHijos[i].IdTipoInsumo == 10001)
                                        {
                                            var insumoNuevo = new InsumoCreacionDTO();
                                            insumoNuevo.IdProyecto = precioUnitario.IdProyecto;
                                            insumoNuevo.Codigo = DetallesHijos[i].Codigo;
                                            insumoNuevo.Descripcion = DetallesHijos[i].Descripcion;
                                            insumoNuevo.Unidad = DetallesHijos[i].Unidad;
                                            insumoNuevo.CostoUnitario = DetallesHijos[i].CostoUnitario;
                                            insumoNuevo.idTipoInsumo = DetallesHijos[i].IdTipoInsumo;
                                            var insumoCreadoNuevo = await _InsumoService.CrearYObtener(insumoNuevo);
                                            DetallesHijos[i].IdInsumo = insumoCreadoNuevo.id;
                                        }
                                        var nuevoDetalle = await _PrecioUnitarioDetalleService.CrearYObtener(DetallesHijos[i]);
                                        if (DetallesHijos[i].IdTipoInsumo == 10000 && DetallesHijos[i].EsCompuesto == false)
                                        {
                                            await RecalcularPorcentajeManoDeObra(nuevoDetalle);
                                        }
                                        if (DetallesHijos[i].EsCompuesto == true)
                                        {
                                            await CrearDetallesHijosCompuestoExistente(DetalleAuxiliar, nuevoDetalle, precioUnitario.IdProyecto);
                                        }
                                    }
                                }
                                var detalles = await ObtenerDetallesPorPU(partida.Id, _dbContex);
                                var insumos = await _InsumoService.ObtenXIdProyecto(precioUnitario.IdProyecto); //Era id del proyecto pero no lo obtengo
                                var datos = await RecalcularDetalles(nuevoRegistro.IdPrecioUnitario, detalles, insumos);
                                var concepto = await _ConceptoService.ObtenXId(partida.IdConcepto);
                                concepto.CostoUnitario = datos.Total;
                                await _ConceptoService.Editar(concepto);
                                await RecalcularPrecioUnitario(partida);
                            }
                        }
                    }
                    else
                    {
                        if(registro.IdPrecioUnitarioDetallePerteneciente != 0)
                        {
                            var padre = await _PrecioUnitarioDetalleService.ObtenerXId(registro.IdPrecioUnitarioDetallePerteneciente);
                            var registrosPadresXInsumo = await ObtenerDetallesPorIdInsumo(padre.IdInsumo, _dbContex);
                            foreach (var registroPadre in registrosPadresXInsumo)
                            {
                                if (registroPadre.IdInsumo == padre.IdInsumo)
                                {
                                    registro.IdPrecioUnitarioDetallePerteneciente = registroPadre.Id;
                                    registro.IdPrecioUnitario = registroPadre.IdPrecioUnitario;
                                    nuevoRegistro = await _PrecioUnitarioDetalleService.CrearYObtener(registro);
                                    if (registro.IdTipoInsumo == 10000 && registro.EsCompuesto == false)
                                    {
                                        await RecalcularPorcentajeManoDeObra(nuevoRegistro);
                                    }
                                    var PrecioUnitarioRegistro = await _PrecioUnitarioService.ObtenXId(nuevoRegistro.IdPrecioUnitario);
                                    var detalles = await ObtenerDetallesPorPU(nuevoRegistro.IdPrecioUnitario, _dbContex);
                                    var insumos = await _InsumoService.ObtenXIdProyecto(precioUnitario.IdProyecto); //Era id del proyecto pero no lo obtengo
                                    var datos = await RecalcularDetalles(nuevoRegistro.IdPrecioUnitario, detalles, insumos);
                                    var concepto = await _ConceptoService.ObtenXId(PrecioUnitarioRegistro.IdConcepto);
                                    concepto.CostoUnitario = datos.Total;
                                    await _ConceptoService.Editar(concepto);
                                    await RecalcularPrecioUnitario(PrecioUnitarioRegistro);
                                }
                            }
                        }
                        else
                        {
                            var PrecioUnitario = await _PrecioUnitarioService.ObtenXId(registro.IdPrecioUnitario);
                            var PreciosUnitarioSinEstructurar = await ObtenerPrecioUnitarioSinEstructurar(PrecioUnitario.IdProyecto);
                            var PreciosFiltrados = PreciosUnitarioSinEstructurar.Where(z => z.IdConcepto == PrecioUnitario.IdConcepto);
                            foreach(var partida in PreciosFiltrados)
                            {
                                registro.IdPrecioUnitario = partida.Id;
                                nuevoRegistro = await _PrecioUnitarioDetalleService.CrearYObtener(registro);
                                if (registro.IdTipoInsumo == 10000)
                                {
                                    await RecalcularPorcentajeManoDeObra(nuevoRegistro);
                                }
                                var detalles = await ObtenerDetallesPorPU(partida.Id, _dbContex);
                                var insumos = await _InsumoService.ObtenXIdProyecto(precioUnitario.IdProyecto); //Era id del proyecto pero no lo obtengo
                                var datos = await RecalcularDetalles(nuevoRegistro.IdPrecioUnitario, detalles, insumos);
                                var concepto = await _ConceptoService.ObtenXId(partida.IdConcepto);
                                concepto.CostoUnitario = datos.Total;
                                await _ConceptoService.Editar(concepto);
                                await RecalcularPrecioUnitario(partida);
                            }
                        }
                        
                    }
                    var DetallesParaRetornar = await ObtenerDetallesPorPU(precioUnitario.Id, _dbContex);
                    var detallesFiltrados = DetallesParaRetornar.Where(z => z.IdPrecioUnitarioDetallePerteneciente == registro.IdPrecioUnitarioDetallePerteneciente).ToList();
                    return detallesFiltrados;
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
                return new List<PrecioUnitarioDetalleDTO>();
            }
        }
        public async Task CrearDetallesHijosCompuestoExistente(PrecioUnitarioDetalleDTO detalleOriginal, PrecioUnitarioDetalleDTO nuevoDetalle, int IdProyecto)
        {
            var DetllesPU = await ObtenerDetallesPorPU(detalleOriginal.IdPrecioUnitario, _dbContex);
            //var DetllesPU = detalles.Where(z => z.IdPrecioUnitario == detalleOriginal.IdPrecioUnitario).ToList();
            var detallesFiltrados = DetllesPU.Where(z => z.IdPrecioUnitarioDetallePerteneciente == detalleOriginal.Id).ToList();
            if (detallesFiltrados.Count > 0)
            {
                for (int i = 0; i < detallesFiltrados.Count; i++)
                {
                    PrecioUnitarioDetalleDTO DetalleAuxiliar = new PrecioUnitarioDetalleDTO();
                    DetalleAuxiliar.Cantidad = detallesFiltrados[i].Cantidad;
                    DetalleAuxiliar.EsCompuesto = detallesFiltrados[i].EsCompuesto;
                    DetalleAuxiliar.Id = detallesFiltrados[i].Id;
                    DetalleAuxiliar.IdPrecioUnitario = detallesFiltrados[i].IdPrecioUnitario;
                    DetalleAuxiliar.IdInsumo = detallesFiltrados[i].IdInsumo;
                    DetalleAuxiliar.CantidadExcedente = detallesFiltrados[i].CantidadExcedente;
                    DetalleAuxiliar.IdPrecioUnitarioDetallePerteneciente = detallesFiltrados[i].IdPrecioUnitarioDetallePerteneciente;
                    detallesFiltrados[i].Id = 0;
                    detallesFiltrados[i].IdPrecioUnitario = nuevoDetalle.IdPrecioUnitario;
                    detallesFiltrados[i].IdPrecioUnitarioDetallePerteneciente = nuevoDetalle.Id;
                    if (detallesFiltrados[i].IdTipoInsumo == 10001)
                    {
                        var insumoNuevo = new InsumoCreacionDTO();
                        insumoNuevo.IdProyecto = IdProyecto;
                        insumoNuevo.Codigo = detallesFiltrados[i].Codigo;
                        insumoNuevo.Descripcion = detallesFiltrados[i].Descripcion;
                        insumoNuevo.Unidad = detallesFiltrados[i].Unidad;
                        insumoNuevo.CostoUnitario = detallesFiltrados[i].CostoUnitario;
                        insumoNuevo.idTipoInsumo = detallesFiltrados[i].IdTipoInsumo;
                        var insumoCreadoNuevo = await _InsumoService.CrearYObtener(insumoNuevo);
                        detallesFiltrados[i].IdInsumo = insumoCreadoNuevo.id;
                    }
                    var nuevoDetalleCreado = await _PrecioUnitarioDetalleService.CrearYObtener(detallesFiltrados[i]);
                    if (detallesFiltrados[i].IdTipoInsumo == 10000 && detallesFiltrados[i].EsCompuesto == false)
                    {
                        await RecalcularPorcentajeManoDeObra(nuevoDetalleCreado);
                    }
                    if (detallesFiltrados[i].EsCompuesto == true)
                    {
                        await CrearDetallesHijosCompuestoExistente(DetalleAuxiliar, nuevoDetalleCreado, IdProyecto);
                    }
                }
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public async Task<ActionResult<List<PrecioUnitarioDetalleDTO>>> EditarDetalle(PrecioUnitarioDetalleDTO registro)
        {
            try
            {
                var insumoOriginal = await _InsumoService.ObtenXId(registro.IdInsumo);

                //if (registro.IdInsumo != 0)
                //{
                //    var fsr = await _FsrxinsummoMdOService.ObtenerXIdInsumo(registro.IdInsumo);
                //    if (fsr.Id > 0)
                //    {
                //        registro.CostoUnitario = registro.CostoBase * fsr.Fsr;
                //    }
                //    else
                //    {
                //        if (registro.CostoBase != 0)
                //        {
                //            if (registro.IdTipoInsumo == 10000)
                //            {
                //                var FSR = await _FSRService.ObtenerTodosXProyecto(insumoOriginal.IdProyecto);
                //                if (FSR.Count > 0)
                //                {
                //                    var factorSR = FSR.FirstOrDefault();
                //                    registro.CostoUnitario = registro.CostoBase * FSR[0].PorcentajeFsr;
                //                }
                //                else
                //                {
                //                    registro.CostoUnitario = registro.CostoBase;
                //                }

                //            }
                //            else
                //            {
                //                registro.CostoUnitario = registro.CostoBase;

                //            }
                //        }
                //    }
                //}
                var PU = await _PrecioUnitarioService.ObtenXId(registro.IdPrecioUnitario);
                InsumoDTO insumo = new InsumoDTO();
                insumo.id = registro.IdInsumo;
                insumo.Codigo = registro.Codigo;
                insumo.Descripcion = registro.Descripcion;
                insumo.Unidad = registro.Unidad;
                insumo.idTipoInsumo = registro.IdTipoInsumo;
                insumo.idFamiliaInsumo = registro.IdFamiliaInsumo;
                insumo.CostoUnitario = registro.CostoUnitario;
                insumo.CostoBase = registro.CostoBase;
                insumo.IdProyecto = PU.IdProyecto;

                if (registro.IdTipoInsumo == 10000)
                {
                    var FSR = await _FSRService.ObtenerTodosXProyecto(insumoOriginal.IdProyecto);
                    if (FSR.Count > 0)
                    {
                        var factorSR = FSR.FirstOrDefault();

                        if (!factorSR.EsCompuesto)
                        {
                            insumo.CostoUnitario = registro.CostoBase * factorSR.PorcentajeFsr;
                            var insumoEditado = await _InsumoService.Editar(insumo);
                        }
                        else
                        {
                            insumo.CostoBase = registro.CostoBase;
                            await _factorSalarioRealProceso.actualizarCostoUnitarioInsumoMO(insumo, factorSR);
                        }
                    }
                    else
                    {
                        insumo.CostoUnitario = registro.CostoBase;
                        var insumoEditado = await _InsumoService.Editar(insumo);
                    }

                }
                else
                {
                    insumo.CostoUnitario = registro.CostoBase;
                    var insumoEditado = await _InsumoService.Editar(insumo);
                }

                await _PrecioUnitarioDetalleService.Editar(registro);
                if (registro.IdTipoInsumo == 10000)
                {
                    await RecalcularPorcentajeManoDeObra(registro);
                }
                var precioUnitario = await _PrecioUnitarioService.ObtenXId(registro.IdPrecioUnitario);
                var insumos = await _InsumoService.ObtenXIdProyecto(precioUnitario.IdProyecto); //Era id del proyecto pero no lo obtengo
                var detalles = await ObtenerDetallesPorPU(precioUnitario.Id, _dbContex);
                var detallesFiltrados = detalles.Where(z => z.IdPrecioUnitarioDetallePerteneciente == registro.IdPrecioUnitarioDetallePerteneciente).ToList();
                var datos = await RecalcularDetalles(registro.IdPrecioUnitario, detalles, insumos);
                detalles = datos.Detalles;
                for (int i = 0; i < detallesFiltrados.Count; i++)
                {
                    var detalle = detalles.Where(z => z.Id == detallesFiltrados[i].Id).FirstOrDefault();
                    detallesFiltrados[i] = detalle;
                }
                insumos = datos.Insumos;
                //await _PrecioUnitarioService.Editar(precioUnitario);
                var concepto = await _ConceptoService.ObtenXId(precioUnitario.IdConcepto);
                concepto.CostoUnitario = datos.Total;
                await _ConceptoService.Editar(concepto);
                await RecalcularPrecioUnitario(precioUnitario);
                detalles = await ObtenerDetallesPorPU(precioUnitario.Id, _dbContex);
                detallesFiltrados = detalles.Where(z => z.IdPrecioUnitarioDetallePerteneciente == registro.IdPrecioUnitarioDetallePerteneciente).ToList();

                return detallesFiltrados;
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
                return new List<PrecioUnitarioDetalleDTO>();
            }
        }

        public async Task<ActionResult<List<PrecioUnitarioDetalleDTO>>> EditarImporteDetalle(PrecioUnitarioDetalleDTO registro) {
            if (registro.Importe > 0)
            {
                var detallesFiltrados = new List<PrecioUnitarioDetalleDTO>();
                var precioUnitario = await _PrecioUnitarioService.ObtenXId(registro.IdPrecioUnitario);
                var PUs = await ObtenerPrecioUnitarioSinEstructurar(precioUnitario.IdProyecto);
                var conceptos = await _ConceptoService.ObtenerTodos(precioUnitario.IdProyecto);
                var insumos = await _InsumoService.ObtenXIdProyecto(precioUnitario.IdProyecto); //Era id del proyecto pero no lo obtengo
                var existePadre = new PrecioUnitarioDetalleDTO();
                if (registro.IdPrecioUnitarioDetallePerteneciente != 0)
                {
                    existePadre = await _PrecioUnitarioDetalleService.ObtenXId(registro.IdPrecioUnitarioDetallePerteneciente);
                    var detallesXInsumo = await ObtenerDetallesPorIdInsumo(registro.IdInsumo, _dbContex);
                    foreach (var det in detallesXInsumo)
                    {
                        var DetallePadre = await _PrecioUnitarioDetalleService.ObtenXId(det.IdPrecioUnitarioDetallePerteneciente);
                        if (DetallePadre.IdInsumo != existePadre.IdInsumo || DetallePadre.Id == 0 && det.Id != registro.Id)
                        {
                            continue;
                        }
                        var PU = PUs.Where(z => z.Id == det.IdPrecioUnitario).First();
                        det.Cantidad = registro.Importe / registro.CostoUnitario;
                        await _PrecioUnitarioDetalleService.Editar(det);
                        if (det.IdTipoInsumo == 10000)
                        {
                            await RecalcularPorcentajeManoDeObra(det);
                        }

                        var detalles = await ObtenerDetallesPorPU(PU.Id, _dbContex);
                        var datos = await RecalcularDetalles(det.IdPrecioUnitario, detalles, insumos);
                        var concepto = conceptos.Where(z => z.Id == PU.IdConcepto).First();
                        concepto.CostoUnitario = datos.Total;
                        await _ConceptoService.Editar(concepto);
                        await RecalcularPrecioUnitario(PU);
                    }
                }
                else
                {
                    var partidas = PUs.Where(z => z.IdConcepto == precioUnitario.IdConcepto && z.TipoPrecioUnitario == 1);
                    foreach (var partida in partidas)
                    {
                        var detalles = await ObtenerDetallesPorPU(partida.Id, _dbContex);
                        var detEditar = detalles.Where(z => z.IdInsumo == registro.IdInsumo && z.IdPrecioUnitarioDetallePerteneciente == 0).ToList();
                        foreach (var det in detEditar)
                        {
                            det.Cantidad = registro.Importe / registro.CostoUnitario;
                            await _PrecioUnitarioDetalleService.Editar(det);
                            if (det.IdTipoInsumo == 10000)
                            {
                                await RecalcularPorcentajeManoDeObra(det);
                            }
                        }

                        detalles = await ObtenerDetallesPorPU(partida.Id, _dbContex);
                        var datos = await RecalcularDetalles(partida.Id, detalles, insumos);
                        var concepto = conceptos.Where(z => z.Id == partida.IdConcepto).First();
                        concepto.CostoUnitario = datos.Total;
                        await _ConceptoService.Editar(concepto);
                        await RecalcularPrecioUnitario(partida);
                    }
                }
                
            }
            var lista = await ObtenerDetallesPorPU(registro.IdPrecioUnitario, _dbContex);
            return lista.Where(z => z.IdPrecioUnitarioDetallePerteneciente == registro.IdPrecioUnitarioDetallePerteneciente).ToList();
        }


        public async Task<ActionResult<List<PrecioUnitarioDetalleDTO>>> PartirDetalle(PrecioUnitarioDetalleDTO registro)
        {
            var insumo = await _InsumoService.ObtenXId(registro.IdInsumo);
            var detallesXIdInsumo = await ObtenerDetallesPorIdInsumo(registro.IdInsumo, _dbContex);
            var registrosFiltrados = await ObtenerDetallesPorPU(registro.IdPrecioUnitario, _dbContex);
            if (detallesXIdInsumo.Count <= 1)
            {
                return registrosFiltrados.Where(z => z.IdPrecioUnitarioDetallePerteneciente == registro.IdPrecioUnitarioDetallePerteneciente).ToList();
            }
            var insumos = await _InsumoService.ObtenerInsumoXProyecto(insumo.IdProyecto);
            var mismoCodigo = insumos.Where(z => z.Codigo == insumo.Codigo).ToList();

            InsumoCreacionDTO nuevoInsumo = new InsumoCreacionDTO();
            nuevoInsumo.Codigo = insumo.Codigo + "-" + mismoCodigo.Count().ToString();
            nuevoInsumo.Unidad = insumo.Unidad;
            nuevoInsumo.idTipoInsumo = insumo.idTipoInsumo;
            nuevoInsumo.idFamiliaInsumo = insumo.idFamiliaInsumo;
            nuevoInsumo.CostoUnitario = insumo.CostoUnitario;
            nuevoInsumo.IdProyecto = insumo.IdProyecto;
            nuevoInsumo.Descripcion = insumo.Descripcion + "-" + mismoCodigo.Count().ToString();

            var detallesFiltrados = registrosFiltrados.Where(z => z.IdPrecioUnitarioDetallePerteneciente == registro.IdPrecioUnitarioDetallePerteneciente).ToList();

            //var detalles = await _PrecioUnitarioDetalleService.ObtenerTodosXIdPrecioUnitario(registro.IdPrecioUnitario);
            //var detallesFiltrados = detalles.Where(z => z.IdPrecioUnitarioDetallePerteneciente == registro.IdPrecioUnitarioDetallePerteneciente).ToList();
            //for (int i = 0; i < detallesFiltrados.Count; i++)
            //{
            //    var buscaInsumo = insumos.Where(z => z.id == detalles[i].IdInsumo).FirstOrDefault();
            //    detallesFiltrados[i].Codigo = buscaInsumo.Codigo;
            //    detallesFiltrados[i].Descripcion = buscaInsumo.Descripcion;
            //    detallesFiltrados[i].Unidad = buscaInsumo.Unidad;
            //    detallesFiltrados[i].IdTipoInsumo = buscaInsumo.idTipoInsumo;
            //    detallesFiltrados[i].IdFamiliaInsumo = buscaInsumo.idFamiliaInsumo;
            //    detallesFiltrados[i].CostoUnitario = buscaInsumo.CostoUnitario;
            //    detallesFiltrados[i].Importe = detalles[i].Cantidad * detalles[i].CostoUnitario;
            //    detallesFiltrados[i].CantidadConFormato = String.Format("{0:#,##0.0000}", detalles[i].Cantidad);
            //    detallesFiltrados[i].CostoUnitarioConFormato = String.Format("{0:#,##0.00}", detalles[i].CostoUnitario);
            //    detallesFiltrados[i].ImporteConFormato = String.Format("{0:#,##0.00}", detalles[i].Importe);
            //}
            var crearInsumo = await _InsumoService.CrearYObtener(nuevoInsumo);
            if (crearInsumo.id <= 0)
            {
                return detallesFiltrados;
            }

            registro.IdInsumo = crearInsumo.id;
            registro.Codigo = crearInsumo.Codigo;
            registro.Descripcion = crearInsumo.Descripcion;
            var editar = await _PrecioUnitarioDetalleService.Editar(registro);
            if (!editar.Estatus)
            {
                return detallesFiltrados;
            }
            for (int i = 0; i < detallesFiltrados.Count; i++)
            {
                if (detallesFiltrados[i].Id == registro.Id)
                {
                    detallesFiltrados[i] = registro;
                }
            }
            return detallesFiltrados;
        }

        //public async Task<List<PrecioUnitarioDetalleDTO>> EliminarDetalle(int Id, DbContext db)
        //{
        //    try
        //    {
        //        var registro = await _PrecioUnitarioDetalleService.ObtenXId(Id);
        //        var PU = await _PrecioUnitarioService.ObtenXId(registro.IdPrecioUnitario);
        //        var resultado = await _PrecioUnitarioDetalleService.Eliminar(Id);
        //        if (resultado.Estatus) {
        //            var eliminaInsumo = await _InsumoService.Eliminar(registro.IdInsumo);
        //        }
        //        await RecalcularPorcentajeManoDeObra(registro);
        //        //var detalles = await _PrecioUnitarioDetalleService.ObtenerTodosXIdPrecioUnitario(registro.IdPrecioUnitario);
        //        var detalles = await ObtenerDetallesPorPU(registro.IdPrecioUnitario, db);
        //        var insumos = await _InsumoService.ObtenXIdProyecto(PU.IdProyecto);
        //        //for (int i = 0; i < detalles.Count; i++)
        //        //{
        //        //    var insumo = insumos.Where(z => z.id == detalles[i].IdInsumo).FirstOrDefault();
        //        //    detalles[i].Codigo = insumo.Codigo;
        //        //    detalles[i].Descripcion = insumo.Descripcion;
        //        //    detalles[i].Unidad = insumo.Unidad;
        //        //    detalles[i].CostoUnitario = insumo.CostoUnitario;
        //        //    detalles[i].IdTipoInsumo = insumo.idTipoInsumo;
        //        //    detalles[i].IdFamiliaInsumo = insumo.idFamiliaInsumo;
        //        //}
        //        var datos = await RecalcularDetalles(registro.IdPrecioUnitario, detalles, insumos);
        //        detalles = datos.Detalles;
        //        insumos = datos.Insumos;
        //        var precioUnitario = await _PrecioUnitarioService.ObtenXId(registro.IdPrecioUnitario);
        //        var listaHijos = await ObtenerDetalles(precioUnitario.Id, db);
        //        decimal total = 0;
        //        for (int i = 0; i < listaHijos.Count; i++)
        //        {
        //            total = total + (listaHijos[i].CostoUnitario * listaHijos[i].Cantidad);
        //        }
        //        var concepto = await _ConceptoService.ObtenXId(precioUnitario.IdConcepto);
        //        concepto.CostoUnitario = total;
        //        await _ConceptoService.Editar(concepto);
        //        precioUnitario.CostoUnitario = total;
        //        await _PrecioUnitarioService.Editar(precioUnitario);
        //        await RecalcularPrecioUnitario(precioUnitario);
        //        var detallesFiltrados = detalles.Where(z => z.IdPrecioUnitarioDetallePerteneciente == registro.IdPrecioUnitarioDetallePerteneciente).ToList();
        //        for (int i = 0; i < detallesFiltrados.Count; i++)
        //        {
        //            var insumo = insumos.Where(z => z.id == detallesFiltrados[i].IdInsumo).FirstOrDefault();
        //            detallesFiltrados[i].Codigo = insumo.Codigo;
        //            detallesFiltrados[i].Descripcion = insumo.Descripcion;
        //            detallesFiltrados[i].Unidad = insumo.Unidad;
        //            detallesFiltrados[i].IdTipoInsumo = insumo.idTipoInsumo;
        //            detallesFiltrados[i].IdFamiliaInsumo = insumo.idFamiliaInsumo;
        //            detallesFiltrados[i].CostoUnitario = insumo.CostoUnitario;
        //            detallesFiltrados[i].Importe = detallesFiltrados[i].Cantidad * detallesFiltrados[i].CostoUnitario;
        //            detallesFiltrados[i].CantidadConFormato = String.Format("{0:#,##0.0000}", detallesFiltrados[i].Cantidad);
        //            detallesFiltrados[i].CostoUnitarioConFormato = String.Format("{0:#,##0.00}", detallesFiltrados[i].CostoUnitario);
        //            detallesFiltrados[i].ImporteConFormato = String.Format("{0:#,##0.00}", detallesFiltrados[i].Importe);
        //        }
        //        return detallesFiltrados;

        //    }
        //    catch (Exception ex)
        //    {
        //        string error = ex.Message.ToString();
        //        return new List<PrecioUnitarioDetalleDTO>();
        //    }
        //}

        public async Task<List<PrecioUnitarioDetalleDTO>> EliminarDetalle(int Id, DbContext db)
        {
            try
            {
                var registro = await _PrecioUnitarioDetalleService.ObtenerXId(Id);
                var registros = await ObtenerDetallesPorIdInsumo(registro.IdInsumo, db);
                var registroPadre = new PrecioUnitarioDetalleDTO();
                if (registro.IdPrecioUnitarioDetallePerteneciente != 0)
                {
                    registroPadre = await _PrecioUnitarioDetalleService.ObtenerXId(registro.IdPrecioUnitarioDetallePerteneciente);
                    foreach (var detalle in registros)
                    {
                        if (detalle.IdPrecioUnitarioDetallePerteneciente != 0)
                        {
                            var detallePadre = await _PrecioUnitarioDetalleService.ObtenerXId(detalle.IdPrecioUnitarioDetallePerteneciente);
                            if (detallePadre.IdInsumo == registroPadre.IdInsumo)
                            {
                                var PrecioUnitario = await _PrecioUnitarioService.ObtenXId(detalle.IdPrecioUnitario);
                                await _PrecioUnitarioDetalleService.Eliminar(detalle.Id);
                                var insumos = await _InsumoService.ObtenXIdProyecto(PrecioUnitario.IdProyecto);
                                var detalles = await ObtenerDetallesPorPU(PrecioUnitario.Id, db);
                                var valores = await RecalcularDetalles(PrecioUnitario.Id, detalles, insumos);
                                var concepto = await _ConceptoService.ObtenXId(PrecioUnitario.IdConcepto);
                                concepto.CostoUnitario = valores.Total;
                                await _ConceptoService.Editar(concepto);
                                await RecalcularPrecioUnitario(PrecioUnitario);
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                }
                else
                {
                    var PrecioUnitario = await _PrecioUnitarioService.ObtenXId(registro.IdPrecioUnitario);
                    var preciosUnitariosSinOrganizar = await ObtenerPrecioUnitarioSinEstructurar(PrecioUnitario.IdProyecto);
                    var registrosFiltrados = preciosUnitariosSinOrganizar.Where(z => z.IdConcepto == PrecioUnitario.IdConcepto && z.TipoPrecioUnitario == 1).ToList();
                    foreach(var partida in registrosFiltrados)
                    {
                        var detalles = await ObtenerDetallesPorPU(partida.Id, db);
                        var detEliminar = detalles.Where(z => z.IdInsumo == registro.IdInsumo && z.IdPrecioUnitarioDetallePerteneciente == 0).ToList();
                        foreach (var det in detEliminar) {
                            await _PrecioUnitarioDetalleService.Eliminar(det.Id);
                        }
                        var insumos = await _InsumoService.ObtenXIdProyecto(PrecioUnitario.IdProyecto);
                        detalles = await ObtenerDetallesPorPU(partida.Id, db);
                        var valores = await RecalcularDetalles(partida.Id, detalles, insumos);
                        var concepto = await _ConceptoService.ObtenXId(partida.IdConcepto);
                        concepto.CostoUnitario = valores.Total;
                        await _ConceptoService.Editar(concepto);
                        await RecalcularPrecioUnitario(partida);
                    }
                }
                var lista = await ObtenerDetallesPorPU(registro.IdPrecioUnitario, _dbContex);
                return lista.Where(z => z.IdPrecioUnitarioDetallePerteneciente == registro.IdPrecioUnitarioDetallePerteneciente).ToList();
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
                return new List<PrecioUnitarioDetalleDTO>();
            }
        }

        public async Task RecalcularAfectados(int IdInsumo)
        {
            var insumoBase = await _InsumoService.ObtenXId(IdInsumo);
            var todosDetalles = await _PrecioUnitarioDetalleService.ObtenerTodos();
            var detallesEditados = todosDetalles.Where(z => z.IdInsumo == IdInsumo).ToList();
            var insumos = await _InsumoService.ObtenXIdProyecto(insumoBase.IdProyecto);
            for (int i = 0; i < detallesEditados.Count; i++)
            {
                var insumo = insumos.Where(z => z.id == detallesEditados[i].IdInsumo).FirstOrDefault();
                detallesEditados[i].Codigo = insumo.Codigo;
                detallesEditados[i].Descripcion = insumo.Descripcion;
                detallesEditados[i].Unidad = insumo.Unidad;
                detallesEditados[i].CostoUnitario = insumo.CostoUnitario;
                detallesEditados[i].IdTipoInsumo = insumo.idTipoInsumo;
                detallesEditados[i].IdFamiliaInsumo = insumo.idFamiliaInsumo;
            }
            if (detallesEditados.Count > 0)
            {
                for (int i = 0; i < detallesEditados.Count; i++)
                {
                    var detalles = todosDetalles.Where(z => z.IdPrecioUnitario == detallesEditados[i].IdPrecioUnitario).ToList();
                    for (int j = 0; j < detalles.Count; j++)
                    {
                        var insumo = insumos.Where(z => z.id == detalles[i].IdInsumo).FirstOrDefault();
                        detalles[j].Codigo = insumo.Codigo;
                        detalles[j].Descripcion = insumo.Descripcion;
                        detalles[j].Unidad = insumo.Unidad;
                        detalles[j].CostoUnitario = insumo.CostoUnitario;
                        detalles[j].IdTipoInsumo = insumo.idTipoInsumo;
                        detalles[j].IdFamiliaInsumo = insumo.idFamiliaInsumo;
                    }
                    var datos = await RecalcularDetalles(detallesEditados[i].IdPrecioUnitario, detalles, insumos);
                    detalles = datos.Detalles;
                    insumos = datos.Insumos;
                    var precioUnitarioEditado = await _PrecioUnitarioService.ObtenXId(detallesEditados[i].IdPrecioUnitario);
                    var conceptoPadre = await _ConceptoService.ObtenXId(precioUnitarioEditado.IdConcepto);
                    conceptoPadre.CostoUnitario = datos.Total;
                    await _ConceptoService.Editar(conceptoPadre);
                    await RecalcularPrecioUnitario(precioUnitarioEditado);
                }
            }
        }

        public async Task<List<PrecioUnitarioDTO>> CrearRegistrosCopias(List<PrecioUnitarioCopiaDTO> precios, int IdPrecioUnitarioBase, int IdProyecto, DbContext db)
        {
            var registrosEstructuradosParaCopiar = await EstructurarPreciosParaCopiar(precios);
            await CrearRegistrosSeleccionados(registrosEstructuradosParaCopiar, IdPrecioUnitarioBase, IdProyecto, db);
            var preciosUnitarios = await ObtenerPrecioUnitario(IdProyecto);
            return preciosUnitarios;
        }

        public async Task<List<PrecioUnitarioCopiaDTO>> EstructurarPreciosParaCopiar(List<PrecioUnitarioCopiaDTO> precios)
        {
            var preciosCopiar = new List<PrecioUnitarioCopiaDTO>();
            for (int i = 0; i < precios.Count; i++)
            {
                var hijosCopiar = new List<PrecioUnitarioCopiaDTO>();
                if (precios[i].TipoPrecioUnitario == 0)
                {
                    var hijos = await EstructurarPreciosParaCopiar(precios[i].Hijos);
                    for (int j = 0; j < hijos.Count; j++)
                    {
                        if (hijos[j].Seleccionado == true)
                        {
                            hijosCopiar.Add(hijos[j]);
                        }
                    }
                    if (precios[i].Seleccionado == true)
                    {
                        precios[i].Hijos = hijosCopiar;
                        preciosCopiar.Add(precios[i]);
                    }
                    else
                    {
                        preciosCopiar.AddRange(hijosCopiar);
                    }
                }
                else
                {
                    if (precios[i].Seleccionado == true)
                    {
                        preciosCopiar.Add(precios[i]);
                    }
                }
            }
            return preciosCopiar;
        }

        public async Task CrearRegistrosSeleccionados(List<PrecioUnitarioCopiaDTO> precios, int IdPrecioUnitarioBase, int IdProyecto, DbContext db)
        {
            if (IdPrecioUnitarioBase > 0)
            {
                var precioUnitarioBase = await _PrecioUnitarioService.ObtenXId(IdPrecioUnitarioBase);
                for (int i = 0; i < precios.Count; i++)
                {
                    precios[i].Nivel = precioUnitarioBase.Nivel + 1;
                    precios[i].IdPrecioUnitarioBase = precioUnitarioBase.Id;
                    precios[i].IdProyecto = IdProyecto;
                    var Id = precios[i].Id;
                    precios[i].Id = 0;
                    var conceptos = await _ConceptoService.ObtenerTodos(IdProyecto);
                    var nuevoConcepto = new ConceptoDTO();
                    nuevoConcepto.IdProyecto = IdProyecto;
                    nuevoConcepto.Codigo = precios[i].Codigo;
                    nuevoConcepto.Descripcion = precios[i].Descripcion;
                    nuevoConcepto.Unidad = precios[i].Unidad;
                    nuevoConcepto.CostoUnitario = 0;
                    var conceptoCreado = await _ConceptoService.CrearYObtener(nuevoConcepto);
                    precios[i].IdConcepto = conceptoCreado.Id;
                    var nuevoRegistro = await _PrecioUnitarioService.CrearYObtenerCopia(precios[i]);
                    var Proyecto = await _ProyectoService.ObtenXId(IdProyecto);
                    ProgramacionEstimadaGanttDTO nuevaProgramacion = new ProgramacionEstimadaGanttDTO();
                    nuevaProgramacion.IdConcepto = nuevoRegistro.IdConcepto;
                    nuevaProgramacion.Start = Proyecto.FechaInicio;
                    nuevaProgramacion.End = Proyecto.FechaInicio;
                    nuevaProgramacion.IdProyecto = Proyecto.Id;
                    nuevaProgramacion.IdPrecioUnitario = nuevoRegistro.Id;
                    nuevaProgramacion.Duracion = 1;
                    nuevaProgramacion.Progress = 0;
                    if (nuevoRegistro.IdPrecioUnitarioBase != 0)
                    {
                        var programacionesEstimadas = await _ProgramacionEstimadaGanttService.ObtenerXIdProyecto(Proyecto.Id, db);
                        var programacionEstimadaPadre = programacionesEstimadas.Where(z => z.IdPrecioUnitario == nuevoRegistro.IdPrecioUnitarioBase).FirstOrDefault();
                        nuevaProgramacion.Parent = programacionEstimadaPadre.Id;
                    }
                    else
                    {
                        nuevaProgramacion.Parent = "";
                    }
                    await _ProgramacionEstimadaGanttService.CrearYObtener(nuevaProgramacion);
                    if (precios[i].Hijos.Count > 0)
                    {
                        await CrearRegistrosSeleccionados(precios[i].Hijos, nuevoRegistro.Id, IdProyecto, db);
                    }
                    if (precios[i].TipoPrecioUnitario == 1)
                    {
                        var items = db.Database.SqlQueryRaw<string>(""""select Id, IdPrecioUnitario, IdInsumo, EsCompuesto, Cantidad, CantidadExcedente, IdPrecioUnitarioDetallePerteneciente from PrecioUnitarioDetalle where IdPrecioUnitario = '"""" + Id + """"' for json path"""").ToList();
                        if (items.Count > 0)
                        {
                            string json = string.Join("", items);
                            var datos = JsonSerializer.Deserialize<List<PrecioUnitarioDetalle>>(json);
                            var detalles = _Mapper.Map<List<PrecioUnitarioDetalleDTO>>(datos);
                            var precioUnitarioAuxiliar = await _PrecioUnitarioService.ObtenXId(Id);
                            var insumos = await _InsumoService.ObtenXIdProyecto(precioUnitarioAuxiliar.IdProyecto);
                            for (int j = 0; j < detalles.Count; j++)
                            {
                                var insumo = insumos.Where(z => z.id == detalles[j].IdInsumo).FirstOrDefault();
                                detalles[j].IdPrecioUnitario = nuevoRegistro.Id;
                                detalles[j].Codigo = insumo.Codigo;
                                detalles[j].Descripcion = insumo.Descripcion;
                                detalles[j].Unidad = insumo.Unidad;
                                detalles[j].IdTipoInsumo = insumo.idTipoInsumo;
                                detalles[j].IdFamiliaInsumo = insumo.idFamiliaInsumo;
                                detalles[j].CostoUnitario = insumo.CostoBase;
                                detalles[j].CostoBase = insumo.CostoBase;
                            }
                            await CrearDetalles(detalles, IdProyecto);
                            var insumosParaRecalculo = await _InsumoService.ObtenXIdProyecto(IdProyecto);
                            var detallesParaRecalculo = await _PrecioUnitarioDetalleService.ObtenerTodosXIdPrecioUnitario(nuevoRegistro.Id);
                            for (int j = 0; j < detallesParaRecalculo.Count; j++)
                            {
                                var insumo = insumosParaRecalculo.Where(z => z.id == detallesParaRecalculo[j].IdInsumo).FirstOrDefault();
                                detallesParaRecalculo[j].IdPrecioUnitario = nuevoRegistro.Id;
                                detallesParaRecalculo[j].Codigo = insumo.Codigo;
                                detallesParaRecalculo[j].Descripcion = insumo.Descripcion;
                                detallesParaRecalculo[j].Unidad = insumo.Unidad;
                                detallesParaRecalculo[j].IdTipoInsumo = insumo.idTipoInsumo;
                                detallesParaRecalculo[j].IdFamiliaInsumo = insumo.idFamiliaInsumo;
                                detallesParaRecalculo[j].CostoUnitario = insumo.CostoBase;
                                detallesParaRecalculo[j].CostoBase = insumo.CostoBase;
                            }
                            var datosObtenidos = await RecalcularDetalles(nuevoRegistro.Id, detallesParaRecalculo, insumosParaRecalculo);
                            var nuevoRegistroBuscado = await _PrecioUnitarioService.ObtenXId(nuevoRegistro.Id);
                            var concepto = await _ConceptoService.ObtenXId(nuevoRegistroBuscado.IdConcepto);
                            concepto.CostoUnitario = datosObtenidos.Total;
                            await _ConceptoService.Editar(concepto);
                            await RecalcularPrecioUnitario(nuevoRegistroBuscado);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < precios.Count; i++)
                {
                    precios[i].Nivel = 1;
                    precios[i].IdPrecioUnitarioBase = 0;
                    precios[i].IdProyecto = IdProyecto;
                    var Id = precios[i].Id;
                    precios[i].Id = 0;
                    var nuevoRegistro = await _PrecioUnitarioService.CrearYObtenerCopia(precios[i]);
                    var Proyecto = await _ProyectoService.ObtenXId(IdProyecto);
                    ProgramacionEstimadaDTO nuevaProgramacion = new ProgramacionEstimadaDTO();
                    nuevaProgramacion.IdConcepto = nuevoRegistro.IdConcepto;
                    nuevaProgramacion.Inicio = Proyecto.FechaInicio;
                    nuevaProgramacion.Termino = Proyecto.FechaInicio;
                    nuevaProgramacion.IdProyecto = Proyecto.Id;
                    nuevaProgramacion.IdPrecioUnitario = nuevoRegistro.Id;
                    nuevaProgramacion.DiasTranscurridos = 1;
                    nuevaProgramacion.Nivel = nuevoRegistro.Nivel;
                    nuevaProgramacion.Progreso = 0;
                    nuevaProgramacion.IdPredecesora = 0;
                    if (nuevoRegistro.IdPrecioUnitarioBase != 0)
                    {
                        var programacionesEstimadas = await _ProgramacionEstimadaService.ObtenerTodosXProyecto(Proyecto.Id);
                        var programacionEstimadaPadre = programacionesEstimadas.Where(z => z.IdPrecioUnitario == nuevoRegistro.IdPrecioUnitarioBase).FirstOrDefault();
                        nuevaProgramacion.IdPadre = programacionEstimadaPadre.Id;
                    }
                    else
                    {
                        nuevaProgramacion.IdPadre = 0;
                    }
                    await _ProgramacionEstimadaService.CrearYObtener(nuevaProgramacion);
                    nuevaProgramacion.Predecesor = "";
                    if (precios[i].Hijos.Count > 0)
                    {
                        await CrearRegistrosSeleccionados(precios[i].Hijos, nuevoRegistro.Id, IdProyecto, db);
                    }
                    if (precios[i].TipoPrecioUnitario == 1)
                    {
                        var detalles = await _PrecioUnitarioDetalleService.ObtenerTodosXIdPrecioUnitario(Id);
                    }

                }
            }
        }

        //public async Task CrearRegistrosSeleccionados(List<PrecioUnitarioCopiaDTO> precios, int IdPrecioUnitarioBase, int IdProyecto)
        //{
        //    var registrosFiltrados = await EstructurarPreciosParaCopiar(precios);
        //    if(IdPrecioUnitarioBase > 0)
        //    {
        //        var precioUnitarioBase = await _PrecioUnitarioService.ObtenXId(IdPrecioUnitarioBase);
        //        for(int i = 0; i < registrosFiltrados.Count; i++)
        //        {
        //            registrosFiltrados[i].Nivel = precioUnitarioBase.Nivel + 1;
        //            registrosFiltrados[i].IdPrecioUnitarioBase = precioUnitarioBase.Id;
        //            registrosFiltrados[i].IdProyecto = IdProyecto;
        //            var Id = registrosFiltrados[i].Id;
        //            registrosFiltrados[i].Id = 0;
        //            var conceptos = await _ConceptoService.ObtenerTodos(IdProyecto);
        //            var nuevoConcepto = new ConceptoDTO();
        //            nuevoConcepto.IdProyecto = IdProyecto;
        //            nuevoConcepto.Codigo = registrosFiltrados[i].Codigo;
        //            nuevoConcepto.Descripcion = registrosFiltrados[i].Descripcion;
        //            nuevoConcepto.Unidad = registrosFiltrados[i].Unidad;
        //            nuevoConcepto.CostoUnitario = 0;
        //            var conceptoCreado = await _ConceptoService.CrearYObtener(nuevoConcepto);
        //            registrosFiltrados[i].IdConcepto = conceptoCreado.Id;
        //            var nuevoRegistro = await _PrecioUnitarioService.CrearYObtenerCopia(registrosFiltrados[i]);
        //            if (registrosFiltrados[i].Hijos.Count > 0)
        //            {
        //                await CrearRegistrosSeleccionados(registrosFiltrados[i].Hijos, nuevoRegistro.Id, IdProyecto);
        //            }
        //            if (registrosFiltrados[i].TipoPrecioUnitario == 1)
        //            {
        //                var detalles = await _PrecioUnitarioDetalleService.ObtenerTodosXIdPrecioUnitario(Id);
        //                var precioUnitarioAuxiliar = await _PrecioUnitarioService.ObtenXId(Id);
        //                var insumos = await _InsumoService.Lista(precioUnitarioAuxiliar.IdProyecto);
        //                for(int j = 0; j < detalles.Count; j++)
        //                {
        //                    var insumo = insumos.Where(z => z.id == detalles[j].IdInsumo).FirstOrDefault();
        //                    detalles[j].IdPrecioUnitario = nuevoRegistro.Id;
        //                    detalles[j].Codigo = insumo.Codigo;
        //                    detalles[j].Descripcion = insumo.Descripcion;
        //                    detalles[j].Unidad = insumo.Unidad;
        //                    detalles[j].IdTipoInsumo = insumo.idTipoInsumo;
        //                    detalles[j].IdFamiliaInsumo = insumo.idFamiliaInsumo;
        //                    detalles[j].CostoUnitario = insumo.CostoUnitario;
        //                }
        //                await CrearDetalles(detalles, IdProyecto);
        //                var total = await RecalcularDetalles(nuevoRegistro.Id);
        //                var nuevoRegistroBuscado = await _PrecioUnitarioService.ObtenXId(nuevoRegistro.Id);
        //                var concepto = await _ConceptoService.ObtenXId(nuevoRegistroBuscado.IdConcepto);
        //                concepto.CostoUnitario = total;
        //                await _ConceptoService.Editar(concepto);
        //                await RecalcularPrecioUnitario(nuevoRegistroBuscado);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        for (int i = 0; i < registrosFiltrados.Count; i++)
        //        {
        //            registrosFiltrados[i].Nivel = 1;
        //            registrosFiltrados[i].IdPrecioUnitarioBase = 0;
        //            registrosFiltrados[i].IdProyecto = IdProyecto;
        //            var Id = registrosFiltrados[i].Id;
        //            registrosFiltrados[i].Id = 0;
        //            var nuevoRegistro = await _PrecioUnitarioService.CrearYObtenerCopia(registrosFiltrados[i]);
        //            if (registrosFiltrados[i].Hijos.Count > 0)
        //            {
        //                await CrearRegistrosSeleccionados(registrosFiltrados[i].Hijos, nuevoRegistro.Id, IdProyecto);
        //            }
        //            if (registrosFiltrados[i].TipoPrecioUnitario == 1)
        //            {
        //                var detalles = await _PrecioUnitarioDetalleService.ObtenerTodosXIdPrecioUnitario(Id);
        //            }

        //        }
        //    }
        //}

        public async Task CrearDetalles(List<PrecioUnitarioDetalleDTO> detalles, int IdProyecto)
        {
            var detallesFiltrados = detalles.Where(z => z.IdPrecioUnitarioDetallePerteneciente == 0).ToList();
            var insumos = await _InsumoService.ObtenXIdProyecto(IdProyecto);
            for (int i = 0; i < detallesFiltrados.Count; i++)
            {
                if (detallesFiltrados[i].EsCompuesto == false)
                {
                    var detalleBase = detallesFiltrados[i];
                    var existeInsumo = insumos.Where(z => z.Descripcion.ToLower() == detalleBase.Descripcion.ToLower()).ToList();
                    if (existeInsumo.Count > 0)
                    {
                        var insumo = existeInsumo.FirstOrDefault();
                        if (insumo.idTipoInsumo != 10001)
                        {
                            detallesFiltrados[i].IdInsumo = insumo.id;
                            detallesFiltrados[i].Id = 0;
                            var nuevoDetalle = await _PrecioUnitarioDetalleService.CrearYObtener(detallesFiltrados[i]);
                        }
                        else
                        {
                            var nuevoInsumo = new InsumoCreacionDTO();
                            nuevoInsumo.idTipoInsumo = detallesFiltrados[i].IdTipoInsumo;
                            nuevoInsumo.Codigo = detallesFiltrados[i].Codigo;
                            nuevoInsumo.Descripcion = detallesFiltrados[i].Descripcion;
                            nuevoInsumo.Unidad = detallesFiltrados[i].Unidad;
                            nuevoInsumo.idFamiliaInsumo = detallesFiltrados[i].IdFamiliaInsumo;
                            nuevoInsumo.CostoUnitario = detallesFiltrados[i].CostoBase;
                            nuevoInsumo.CostoBase = detallesFiltrados[i].CostoBase;
                            nuevoInsumo.IdProyecto = IdProyecto;
                            var nuevoInsumoCreado = await _InsumoService.CrearYObtener(nuevoInsumo);
                            detallesFiltrados[i].Id = 0;
                            detallesFiltrados[i].IdInsumo = nuevoInsumoCreado.id;
                            var nuevoDetalle = await _PrecioUnitarioDetalleService.CrearYObtener(detallesFiltrados[i]);
                        }
                    }
                    else
                    {
                        var nuevoInsumo = new InsumoCreacionDTO();
                        nuevoInsumo.idTipoInsumo = detallesFiltrados[i].IdTipoInsumo;
                        nuevoInsumo.Codigo = detallesFiltrados[i].Codigo;
                        nuevoInsumo.Descripcion = detallesFiltrados[i].Descripcion;
                        nuevoInsumo.Unidad = detallesFiltrados[i].Unidad;
                        nuevoInsumo.idFamiliaInsumo = detallesFiltrados[i].IdFamiliaInsumo;
                        nuevoInsumo.CostoUnitario = detallesFiltrados[i].CostoBase;
                        nuevoInsumo.CostoBase = detallesFiltrados[i].CostoBase;
                        nuevoInsumo.IdProyecto = IdProyecto;
                        var nuevoInsumoCreado = await _InsumoService.CrearYObtener(nuevoInsumo);
                        detallesFiltrados[i].Id = 0;
                        detallesFiltrados[i].IdInsumo = nuevoInsumoCreado.id;
                        var nuevoDetalle = await _PrecioUnitarioDetalleService.CrearYObtener(detallesFiltrados[i]);
                    }
                }
                else
                {
                    var idPadreBase = detallesFiltrados[i].Id;
                    var existeInsumo = insumos.Where(z => z.Codigo.ToLower() == detallesFiltrados[i].Codigo.ToLower()).ToList();
                    if (existeInsumo.Count > 0)
                    {
                        var insumo = existeInsumo.FirstOrDefault();
                        detallesFiltrados[i].IdInsumo = insumo.id;

                    }
                    else
                    {
                        var nuevoInsumo = new InsumoCreacionDTO();
                        nuevoInsumo.idTipoInsumo = detallesFiltrados[i].IdTipoInsumo;
                        nuevoInsumo.Codigo = detallesFiltrados[i].Codigo;
                        nuevoInsumo.Descripcion = detallesFiltrados[i].Descripcion;
                        nuevoInsumo.Unidad = detallesFiltrados[i].Unidad;
                        nuevoInsumo.idFamiliaInsumo = detallesFiltrados[i].IdFamiliaInsumo;
                        nuevoInsumo.CostoUnitario = detallesFiltrados[i].CostoBase;
                        nuevoInsumo.CostoBase = detallesFiltrados[i].CostoBase;
                        nuevoInsumo.IdProyecto = IdProyecto;
                        var nuevoInsumoCreado = await _InsumoService.CrearYObtener(nuevoInsumo);
                        detallesFiltrados[i].IdInsumo = nuevoInsumoCreado.id;
                    }
                    detallesFiltrados[i].Id = 0;
                    var nuevoDetalle = await _PrecioUnitarioDetalleService.CrearYObtener(detallesFiltrados[i]);
                    await CrearDetallesHijos(idPadreBase, detalles, nuevoDetalle, IdProyecto);
                    //var idPadreBase = detallesFiltrados[i].Id;
                    //var nuevoInsumo = new InsumoCreacionDTO();
                    //nuevoInsumo.idTipoInsumo = detallesFiltrados[i].IdTipoInsumo;
                    //nuevoInsumo.Codigo = detallesFiltrados[i].Codigo;
                    //nuevoInsumo.Descripcion = detallesFiltrados[i].Descripcion;
                    //nuevoInsumo.Unidad = detallesFiltrados[i].Unidad;
                    //nuevoInsumo.idFamiliaInsumo = detallesFiltrados[i].IdFamiliaInsumo;
                    //nuevoInsumo.CostoUnitario = detallesFiltrados[i].CostoUnitario;
                    //nuevoInsumo.IdProyecto = IdProyecto;
                    //var nuevoInsumoCreado = await _InsumoService.CrearYObtener(nuevoInsumo);
                    //detallesFiltrados[i].Id = 0;
                    //detallesFiltrados[i].IdInsumo = nuevoInsumoCreado.id;
                    //var nuevoDetalle = await _PrecioUnitarioDetalleService.CrearYObtener(detallesFiltrados[i]);
                    //await CrearDetallesHijos(idPadreBase, detalles, nuevoDetalle, IdProyecto);
                }
            }
        }

        public async Task CrearDetallesHijos(int idDetallePadreBase, List<PrecioUnitarioDetalleDTO> detalles, PrecioUnitarioDetalleDTO nuevoDetallePadre, int IdProyecto)
        {
            var detallesFiltrados = detalles.Where(z => z.IdPrecioUnitarioDetallePerteneciente == idDetallePadreBase).ToList();
            var insumos = await _InsumoService.ObtenXIdProyecto(IdProyecto);
            for (int i = 0; i < detallesFiltrados.Count; i++)
            {
                if (detallesFiltrados[i].EsCompuesto == false)
                {
                    var detalleBase = detallesFiltrados[i];
                    var existeInsumo = insumos.Where(z => z.Codigo.ToLower() == detalleBase.Codigo.ToLower()).ToList();
                    if (existeInsumo.Count > 0)
                    {
                        var insumo = existeInsumo.FirstOrDefault();
                        if (insumo.idTipoInsumo != 10001)
                        {
                            detallesFiltrados[i].IdInsumo = insumo.id;
                            detallesFiltrados[i].Id = 0;
                            detallesFiltrados[i].IdPrecioUnitarioDetallePerteneciente = nuevoDetallePadre.Id;
                            detallesFiltrados[i].IdPrecioUnitario = nuevoDetallePadre.IdPrecioUnitario;
                            var nuevoDetalle = await _PrecioUnitarioDetalleService.CrearYObtener(detallesFiltrados[i]);
                        }
                        else
                        {
                            var nuevoInsumo = new InsumoCreacionDTO();
                            nuevoInsumo.idTipoInsumo = detallesFiltrados[i].IdTipoInsumo;
                            nuevoInsumo.Codigo = detallesFiltrados[i].Codigo;
                            nuevoInsumo.Descripcion = detallesFiltrados[i].Descripcion;
                            nuevoInsumo.Unidad = detallesFiltrados[i].Unidad;
                            nuevoInsumo.idFamiliaInsumo = detallesFiltrados[i].IdFamiliaInsumo;
                            nuevoInsumo.CostoUnitario = detallesFiltrados[i].CostoBase;
                            nuevoInsumo.CostoBase = detallesFiltrados[i].CostoBase;
                            nuevoInsumo.IdProyecto = IdProyecto;
                            var nuevoInsumoCreado = await _InsumoService.CrearYObtener(nuevoInsumo);
                            detallesFiltrados[i].Id = 0;
                            detallesFiltrados[i].IdInsumo = nuevoInsumoCreado.id;
                            detallesFiltrados[i].IdPrecioUnitario = nuevoDetallePadre.IdPrecioUnitario;
                            detallesFiltrados[i].IdPrecioUnitarioDetallePerteneciente = nuevoDetallePadre.Id;
                            var nuevoDetalle = await _PrecioUnitarioDetalleService.CrearYObtener(detallesFiltrados[i]);
                        }

                    }
                    else
                    {
                        var nuevoInsumo = new InsumoCreacionDTO();
                        nuevoInsumo.idTipoInsumo = detallesFiltrados[i].IdTipoInsumo;
                        nuevoInsumo.Codigo = detallesFiltrados[i].Codigo;
                        nuevoInsumo.Descripcion = detallesFiltrados[i].Descripcion;
                        nuevoInsumo.Unidad = detallesFiltrados[i].Unidad;
                        nuevoInsumo.idFamiliaInsumo = detallesFiltrados[i].IdFamiliaInsumo;
                        nuevoInsumo.CostoUnitario = detallesFiltrados[i].CostoBase;
                        nuevoInsumo.CostoBase = detallesFiltrados[i].CostoBase;
                        nuevoInsumo.IdProyecto = IdProyecto;
                        var nuevoInsumoCreado = await _InsumoService.CrearYObtener(nuevoInsumo);
                        detallesFiltrados[i].Id = 0;
                        detallesFiltrados[i].IdInsumo = nuevoInsumoCreado.id;
                        detallesFiltrados[i].IdPrecioUnitario = nuevoDetallePadre.IdPrecioUnitario;
                        detallesFiltrados[i].IdPrecioUnitarioDetallePerteneciente = nuevoDetallePadre.Id;
                        var nuevoDetalle = await _PrecioUnitarioDetalleService.CrearYObtener(detallesFiltrados[i]);
                    }
                }
                else
                {
                    var idPadreBase = detallesFiltrados[i].Id;
                    var existeInsumo = insumos.Where(z => z.Codigo.ToLower() == detallesFiltrados[i].Codigo.ToLower()).ToList();
                    if (existeInsumo.Count > 0)
                    {
                        var insumo = existeInsumo.FirstOrDefault();
                        detallesFiltrados[i].IdInsumo = insumo.id;

                    }
                    else
                    {
                        var nuevoInsumo = new InsumoCreacionDTO();
                        nuevoInsumo.idTipoInsumo = detallesFiltrados[i].IdTipoInsumo;
                        nuevoInsumo.Codigo = detallesFiltrados[i].Codigo;
                        nuevoInsumo.Descripcion = detallesFiltrados[i].Descripcion;
                        nuevoInsumo.Unidad = detallesFiltrados[i].Unidad;
                        nuevoInsumo.idFamiliaInsumo = detallesFiltrados[i].IdFamiliaInsumo;
                        nuevoInsumo.CostoUnitario = detallesFiltrados[i].CostoBase;
                        nuevoInsumo.CostoBase = detallesFiltrados[i].CostoBase;
                        nuevoInsumo.IdProyecto = IdProyecto;
                        var nuevoInsumoCreado = await _InsumoService.CrearYObtener(nuevoInsumo);
                        detallesFiltrados[i].IdInsumo = nuevoInsumoCreado.id;
                    }
                    detallesFiltrados[i].Id = 0;
                    detallesFiltrados[i].IdPrecioUnitario = nuevoDetallePadre.IdPrecioUnitario;
                    detallesFiltrados[i].IdPrecioUnitarioDetallePerteneciente = nuevoDetallePadre.Id;
                    var nuevoDetalle = await _PrecioUnitarioDetalleService.CrearYObtener(detallesFiltrados[i]);
                    await CrearDetallesHijos(idPadreBase, detalles, nuevoDetalle, IdProyecto);
                }
            }
        }

        public async Task<List<PrecioUnitarioDTO>> CrearRegistrosDetallesCopia(List<PrecioUnitarioDetalleCopiaDTO> precios, int IdPrecioUnitarioBase, int IdProyecto, DbContext db)
        {
            var registrosEstructuradosParaCopiar = await EstructurarPreciosDetalleParaCopiar(precios);
            var registros = await ObtenerDetallesPorPU(registrosEstructuradosParaCopiar[0].IdPrecioUnitario, db);
            await CrearDetallesCopiadosSeleccionadosJson(registrosEstructuradosParaCopiar, IdPrecioUnitarioBase, IdProyecto, registros);
            var PU = _PrecioUnitarioService.ObtenXId(IdPrecioUnitarioBase).Result;

            var Detalles = ObtenerDetallesPorPU(PU.Id, db).Result;
            var insumos = await _InsumoService.ObtenXIdProyecto(PU.IdProyecto);
            var resultados = await RecalcularDetalles(PU.Id, Detalles, insumos);
            var DetallesFiltrados = resultados.Detalles.Where(z => z.IdPrecioUnitarioDetallePerteneciente == 0).ToList();
            decimal total = 0;
            for (int i = 0; i < DetallesFiltrados.Count; i++)
            {
                total = total + DetallesFiltrados[i].Importe;
            }
            var concepto = await _ConceptoService.ObtenXId(PU.IdConcepto);
            concepto.CostoUnitario = total;
            await _ConceptoService.Editar(concepto);
            await RecalcularPrecioUnitario(PU);
            var pus = await ObtenerPrecioUnitario(IdProyecto);
            return pus;
        }

        public async Task<List<PrecioUnitarioDTO>> CrearRegistrosDetallesCopiaConcepto(List<PrecioUnitarioDetalleCopiaDTO> precios, int IdPrecioUnitarioBase, int IdProyecto, DbContext db)
        {
            var registrosEstructuradosParaCopiar = await EstructurarPreciosDetalleParaCopiar(precios);
            var IdPuBase = registrosEstructuradosParaCopiar[0].IdPrecioUnitario;
            var PU = _PrecioUnitarioService.ObtenXId(IdPrecioUnitarioBase).Result;
            var listaNuevosPU = new List<PrecioUnitarioDTO>();
            for (int i = 0; i < registrosEstructuradosParaCopiar.Count; i++)
            {
                var nuevoPrecioUnitario = new PrecioUnitarioDTO();
                nuevoPrecioUnitario.IdProyecto = IdProyecto;
                nuevoPrecioUnitario.Cantidad = registrosEstructuradosParaCopiar[i].Cantidad;
                nuevoPrecioUnitario.TipoPrecioUnitario = 1;
                nuevoPrecioUnitario.CostoUnitario = registrosEstructuradosParaCopiar[i].CostoUnitario;
                nuevoPrecioUnitario.Nivel = PU.Nivel + 1;
                nuevoPrecioUnitario.NoSerie = PU.NoSerie;
                nuevoPrecioUnitario.IdPrecioUnitarioBase = IdPrecioUnitarioBase;
                nuevoPrecioUnitario.EsDetalle = true;
                nuevoPrecioUnitario.IdConcepto = 0;
                nuevoPrecioUnitario.Codigo = registrosEstructuradosParaCopiar[i].Codigo;
                nuevoPrecioUnitario.Descripcion = registrosEstructuradosParaCopiar[i].Descripcion;
                nuevoPrecioUnitario.Unidad = registrosEstructuradosParaCopiar[i].Unidad;
                nuevoPrecioUnitario.PrecioUnitario = registrosEstructuradosParaCopiar[i].Importe;
                nuevoPrecioUnitario.Importe = registrosEstructuradosParaCopiar[i].Importe;
                nuevoPrecioUnitario.ImporteSeries = registrosEstructuradosParaCopiar[i].Importe;
                await CrearYObtener(nuevoPrecioUnitario, db);
                var PUs = await ObtenerPrecioUnitarioSinEstructurar(IdProyecto);
                var registroCreado = PUs.Where(z => z.Descripcion == nuevoPrecioUnitario.Descripcion).LastOrDefault();
                listaNuevosPU.Add(registroCreado);
            }

            var Detalles0 = await ObtenerDetallesPorPU(IdPuBase, db);

            for (int i = 0; i < registrosEstructuradosParaCopiar.Count; i++)
            {
                var detallesACopiar = Detalles0.Where(z => z.IdPrecioUnitarioDetallePerteneciente == registrosEstructuradosParaCopiar[i].Id);
                var detallesACopiarCopia = new List<PrecioUnitarioDetalleCopiaDTO>();
                foreach (PrecioUnitarioDetalleDTO detalle in detallesACopiar)
                {
                    detalle.IdPrecioUnitarioDetallePerteneciente = 0;
                    var nuevoDetalleCopia = new PrecioUnitarioDetalleCopiaDTO();
                    nuevoDetalleCopia.Id = detalle.Id;
                    nuevoDetalleCopia.IdPrecioUnitario = detalle.IdPrecioUnitario;
                    nuevoDetalleCopia.IdInsumo = detalle.IdInsumo;
                    nuevoDetalleCopia.EsCompuesto = detalle.EsCompuesto;
                    nuevoDetalleCopia.CostoUnitario = detalle.CostoBase;
                    nuevoDetalleCopia.CostoBase = detalle.CostoBase;
                    nuevoDetalleCopia.Cantidad = detalle.Cantidad;
                    nuevoDetalleCopia.CantidadExcedente = detalle.CantidadExcedente;
                    nuevoDetalleCopia.Codigo = detalle.Codigo;
                    nuevoDetalleCopia.Descripcion = detalle.Descripcion;
                    nuevoDetalleCopia.Unidad = detalle.Unidad;
                    nuevoDetalleCopia.IdTipoInsumo = detalle.IdTipoInsumo;
                    nuevoDetalleCopia.IdFamiliaInsumo = detalle.IdFamiliaInsumo;
                    nuevoDetalleCopia.Importe = detalle.Importe;
                    nuevoDetalleCopia.Seleccionado = true;
                    detallesACopiarCopia.Add(nuevoDetalleCopia);
                }
                await CrearDetallesCopiadosSeleccionadosJson(detallesACopiarCopia, listaNuevosPU[i].Id, IdProyecto, Detalles0);
                var Detalles = ObtenerDetallesPorPU(listaNuevosPU[i].Id, db).Result;
                var insumos = await _InsumoService.ObtenXIdProyecto(listaNuevosPU[i].IdProyecto);
                var resultados = await RecalcularDetalles(listaNuevosPU[i].Id, Detalles, insumos);
                var DetallesFiltrados = resultados.Detalles.Where(z => z.IdPrecioUnitarioDetallePerteneciente == 0).ToList();
                decimal total = resultados.Total;
                var concepto = await _ConceptoService.ObtenXId(listaNuevosPU[i].IdConcepto);
                concepto.CostoUnitario = total;
                await _ConceptoService.Editar(concepto);
                await RecalcularPrecioUnitario(listaNuevosPU[i]);
            }
            var pus = await ObtenerPrecioUnitario(IdProyecto);
            return pus;
        }

        public async Task<List<PrecioUnitarioDetalleCopiaDTO>> EstructurarPreciosDetalleParaCopiar(List<PrecioUnitarioDetalleCopiaDTO> detalles)
        {
            var detallesCopiar = new List<PrecioUnitarioDetalleCopiaDTO>();
            for (int i = 0; i < detalles.Count; i++)
            {
                if (detalles[i].Seleccionado == true)
                {
                    detallesCopiar.Add(detalles[i]);
                }
            }
            return detallesCopiar;
        }

        public async Task CrearDetallesCopiadosSeleccionados(List<PrecioUnitarioDetalleCopiaDTO> detalles, int IdPrecioUnitario, int IdProyecto, DbContext db)
        {
            var detallesFiltrados = await EstructurarPreciosDetalleParaCopiar(detalles);
            var precioUnitarioDetalleCopia = await _PrecioUnitarioService.ObtenXId(detallesFiltrados[0].IdPrecioUnitario);
            var items = db.Database.SqlQueryRaw<string>(""""select Id, IdPrecioUnitario, IdInsumo, EsCompuesto, Cantidad, CantidadExcedente, IdPrecioUnitarioDetallePerteneciente from PrecioUnitarioDetalle where IdPrecioUnitario = '"""" + detallesFiltrados[0].IdPrecioUnitario + """"' for json path"""").ToList();
            if (items.Count > 0)
            {
                string json = string.Join("", items);
                var datos = JsonSerializer.Deserialize<List<PrecioUnitarioDetalle>>(json);
                var registros = _Mapper.Map<List<PrecioUnitarioDetalleDTO>>(datos);
                var insumosBase = await _InsumoService.ObtenXIdProyecto(precioUnitarioDetalleCopia.IdProyecto);
                var insumos = await _InsumoService.ObtenXIdProyecto(IdProyecto);
                for (int i = 0; i < registros.Count; i++)
                {
                    var insumo = insumosBase.Where(z => z.id == registros[i].IdInsumo).FirstOrDefault();
                    registros[i].Descripcion = insumo.Descripcion;
                    registros[i].Codigo = insumo.Codigo;
                    registros[i].Unidad = insumo.Unidad;
                    registros[i].CostoUnitario = insumo.CostoBase;
                    registros[i].CostoBase = insumo.CostoBase;
                    registros[i].IdFamiliaInsumo = insumo.idFamiliaInsumo;
                    registros[i].IdTipoInsumo = insumo.idTipoInsumo;
                }
                var registrosFiltrados = new List<PrecioUnitarioDetalleDTO>();
                for (int i = 0; i < detallesFiltrados.Count; i++)
                {
                    registrosFiltrados.Add(registros.Where(z => z.Id == detallesFiltrados[i].Id).FirstOrDefault()!);
                    registrosFiltrados[i].Id = 0;
                    registrosFiltrados[i].IdPrecioUnitario = IdPrecioUnitario;
                    if (registrosFiltrados[i].EsCompuesto == false)
                    {
                        var existeInsumo = insumos.Where(z => z.Codigo.ToLower() == registrosFiltrados[i].Codigo.ToLower()).ToList();
                        if (existeInsumo.Count > 0)
                        {
                            var insumo = existeInsumo.FirstOrDefault();
                            registrosFiltrados[i].IdInsumo = insumo.id;
                            registrosFiltrados[i].Id = 0;
                            var nuevoDetalle = await _PrecioUnitarioDetalleService.CrearYObtener(registrosFiltrados[i]);
                        }
                        else
                        {
                            var nuevoInsumo = new InsumoCreacionDTO();
                            nuevoInsumo.idTipoInsumo = registrosFiltrados[i].IdTipoInsumo;
                            nuevoInsumo.Codigo = registrosFiltrados[i].Codigo;
                            nuevoInsumo.Descripcion = registrosFiltrados[i].Descripcion;
                            nuevoInsumo.Unidad = registrosFiltrados[i].Unidad;
                            nuevoInsumo.idFamiliaInsumo = registrosFiltrados[i].IdFamiliaInsumo;
                            nuevoInsumo.CostoUnitario = registrosFiltrados[i].CostoBase;
                            nuevoInsumo.CostoBase = registrosFiltrados[i].CostoBase;
                            nuevoInsumo.IdProyecto = IdProyecto;
                            var nuevoInsumoCreado = await _InsumoService.CrearYObtener(nuevoInsumo);
                            registrosFiltrados[i].Id = 0;
                            registrosFiltrados[i].IdInsumo = nuevoInsumoCreado.id;
                            registrosFiltrados[i].IdPrecioUnitarioDetallePerteneciente = 0;
                            var nuevoDetalle = await _PrecioUnitarioDetalleService.CrearYObtener(registrosFiltrados[i]);
                        }
                    }
                    else
                    {
                        var idPadreBase = detallesFiltrados[i].Id;
                        var nuevoInsumo = new InsumoCreacionDTO();
                        nuevoInsumo.idTipoInsumo = registrosFiltrados[i].IdTipoInsumo;
                        nuevoInsumo.Codigo = registrosFiltrados[i].Codigo;
                        nuevoInsumo.Descripcion = registrosFiltrados[i].Descripcion;
                        nuevoInsumo.Unidad = registrosFiltrados[i].Unidad;
                        nuevoInsumo.idFamiliaInsumo = registrosFiltrados[i].IdFamiliaInsumo;
                        nuevoInsumo.CostoUnitario = registrosFiltrados[i].CostoBase;
                        nuevoInsumo.CostoBase = registrosFiltrados[i].CostoBase;
                        nuevoInsumo.IdProyecto = IdProyecto;
                        var nuevoInsumoCreado = await _InsumoService.CrearYObtener(nuevoInsumo);
                        detallesFiltrados[i].Id = 0;
                        detallesFiltrados[i].IdInsumo = nuevoInsumoCreado.id;
                        detallesFiltrados[i].IdPrecioUnitario = IdPrecioUnitario;
                        var nuevoDetalle = await _PrecioUnitarioDetalleService.CrearYObtener(detallesFiltrados[i]);
                        await CrearDetallesHijos(idPadreBase, registros, nuevoDetalle, IdProyecto);
                    }
                }
            }
        }

        public async Task CrearDetallesCopiadosSeleccionadosJson(List<PrecioUnitarioDetalleCopiaDTO> detalles, int IdPrecioUnitario, int IdProyecto, List<PrecioUnitarioDetalleDTO> registros)
        {
            var detallesFiltrados = await EstructurarPreciosDetalleParaCopiar(detalles);
            var precioUnitarioDetalleCopia = await _PrecioUnitarioService.ObtenXId(detallesFiltrados[0].IdPrecioUnitario);
            if (registros.Count > 0)
            {
                var insumos = await _InsumoService.ObtenXIdProyecto(IdProyecto);
                var registrosFiltrados = new List<PrecioUnitarioDetalleDTO>();
                for (int i = 0; i < detallesFiltrados.Count; i++)
                {
                    registrosFiltrados.Add(registros.Where(z => z.Id == detallesFiltrados[i].Id).FirstOrDefault()!);
                    registrosFiltrados[i].Id = 0;
                    registrosFiltrados[i].IdPrecioUnitarioDetallePerteneciente = 0;
                    registrosFiltrados[i].IdPrecioUnitario = IdPrecioUnitario;
                    if (registrosFiltrados[i].EsCompuesto == false)
                    {
                        var existeInsumo = insumos.Where(z => z.Codigo.ToLower() == registrosFiltrados[i].Codigo.ToLower()).ToList();
                        if (existeInsumo.Count > 0)
                        {
                            var insumo = existeInsumo.FirstOrDefault();
                            registrosFiltrados[i].IdInsumo = insumo.id;
                            registrosFiltrados[i].Id = 0;
                            var nuevoDetalle = await _PrecioUnitarioDetalleService.CrearYObtener(registrosFiltrados[i]);
                        }
                        else
                        {
                            var nuevoInsumo = new InsumoCreacionDTO();
                            nuevoInsumo.idTipoInsumo = registrosFiltrados[i].IdTipoInsumo;
                            nuevoInsumo.Codigo = registrosFiltrados[i].Codigo;
                            nuevoInsumo.Descripcion = registrosFiltrados[i].Descripcion;
                            nuevoInsumo.Unidad = registrosFiltrados[i].Unidad;
                            nuevoInsumo.idFamiliaInsumo = registrosFiltrados[i].IdFamiliaInsumo;
                            nuevoInsumo.CostoUnitario = registrosFiltrados[i].CostoBase;
                            nuevoInsumo.CostoBase = registrosFiltrados[i].CostoBase;
                            nuevoInsumo.IdProyecto = IdProyecto;
                            var nuevoInsumoCreado = await _InsumoService.CrearYObtener(nuevoInsumo);
                            registrosFiltrados[i].Id = 0;
                            registrosFiltrados[i].IdInsumo = nuevoInsumoCreado.id;
                            registrosFiltrados[i].IdPrecioUnitarioDetallePerteneciente = 0;
                            var nuevoDetalle = await _PrecioUnitarioDetalleService.CrearYObtener(registrosFiltrados[i]);
                        }
                    }
                    else
                    {

                        var idPadreBase = detallesFiltrados[i].Id;
                        var existeInsumo = insumos.Where(z => z.Codigo.ToLower() == registrosFiltrados[i].Codigo.ToLower()).ToList();
                        if (existeInsumo.Count > 0)
                        {
                            var insumo = existeInsumo.FirstOrDefault();
                            detallesFiltrados[i].IdInsumo = insumo.id;

                        }
                        else
                        {
                            var nuevoInsumo = new InsumoCreacionDTO();
                            nuevoInsumo.idTipoInsumo = registrosFiltrados[i].IdTipoInsumo;
                            nuevoInsumo.Codigo = registrosFiltrados[i].Codigo;
                            nuevoInsumo.Descripcion = registrosFiltrados[i].Descripcion;
                            nuevoInsumo.Unidad = registrosFiltrados[i].Unidad;
                            nuevoInsumo.idFamiliaInsumo = registrosFiltrados[i].IdFamiliaInsumo;
                            nuevoInsumo.CostoUnitario = registrosFiltrados[i].CostoBase;
                            nuevoInsumo.CostoBase = registrosFiltrados[i].CostoBase;
                            nuevoInsumo.IdProyecto = IdProyecto;
                            var nuevoInsumoCreado = await _InsumoService.CrearYObtener(nuevoInsumo);
                            detallesFiltrados[i].IdInsumo = nuevoInsumoCreado.id;
                        }
                        detallesFiltrados[i].Id = 0;
                        detallesFiltrados[i].IdPrecioUnitario = IdPrecioUnitario;
                        var nuevoDetalle = await _PrecioUnitarioDetalleService.CrearYObtener(detallesFiltrados[i]);
                        await CrearDetallesHijos(idPadreBase, registros, nuevoDetalle, IdProyecto);
                    }
                }
            }
        }

        public async Task CrearRegistrosDeInsumosDeExcel(int numero)
        {
            string path = @"D:\ExcelBimsa\InsumosParaSQL.xlsx";
            SLDocument sl = new SLDocument(path);

            int iRow = iRow = 2;
            var insumos = await _InsumoService.ObtenXIdProyecto(1);
            while (!string.IsNullOrEmpty(sl.GetCellValueAsString(iRow, 1)))
            {
                string ClaveInsumo = sl.GetCellValueAsString(iRow, 1);
                var insumo = insumos.Where(z => z.Codigo == ClaveInsumo).FirstOrDefault();
                var detalleNuevo = new PrecioUnitarioDetalleDTO();
                detalleNuevo.IdInsumo = insumo.id;
                detalleNuevo.IdPrecioUnitario = 2;
                detalleNuevo.EsCompuesto = false;
                detalleNuevo.Cantidad = 0;
                detalleNuevo.CantidadExcedente = 0;
                detalleNuevo.IdPrecioUnitarioDetallePerteneciente = 0;
                var detalleCreado = await _PrecioUnitarioDetalleService.CrearYObtener(detalleNuevo);
                iRow++;
            }
        }

        public async Task ActualizarInsumosBimsa(int numero)
        {
            string path = @"D:\ExcelBimsa\InsumosParaActualizar.xlsx";
            SLDocument sl = new SLDocument(path);
            var insumos = await _InsumoService.ObtenXIdProyecto(10000);
            int iRow = iRow = 2;
            while (!string.IsNullOrEmpty(sl.GetCellValueAsString(iRow, 2)))
            {
                string ClaveInsumo = sl.GetCellValueAsString(iRow, 1);
                var insumosRegistros = insumos.Where(z => z.Codigo == ClaveInsumo).ToList();
                if (insumosRegistros.Count > 0)
                {
                    var insumo = insumosRegistros.FirstOrDefault();
                    decimal valor = sl.GetCellValueAsDecimal(iRow, 2);
                    insumo.CostoUnitario = valor;
                    await _InsumoService.Editar(insumo);
                }
                iRow++;
            }
        }

        public async Task RecalcularArmadoPrecioUnitario(int IdPrecioUnitario, DbContext db)
        {
            var insumos = await _InsumoService.ObtenXIdProyecto(10000);
            var detalles = await ObtenerDetallesPorPU(10001, db);
            var detallesCompuestos = detalles.Where(z => z.EsCompuesto == true).ToList();
            foreach (PrecioUnitarioDetalleDTO registro in detallesCompuestos)
            {
                var detallesHijos = detalles.Where(z => z.IdPrecioUnitarioDetallePerteneciente == registro.Id).ToList();
                decimal total = 0;
                foreach (PrecioUnitarioDetalleDTO registroHijo in detallesHijos)
                {
                    if (registroHijo.EsCompuesto == true)
                    {
                        var precioRegistroHijo = await RecalcularArmadoHijos(insumos, detalles, registroHijo);
                        total = total + precioRegistroHijo;
                    }
                    else
                    {
                        total = total + registroHijo.Importe;
                    }
                }
                var inusmo = insumos.Where(z => z.id == registro.IdInsumo).FirstOrDefault();
                inusmo.CostoUnitario = total;
                await _InsumoService.Editar(inusmo);
            }
        }

        public async Task<decimal> RecalcularArmadoHijos(List<InsumoDTO> insumos, List<PrecioUnitarioDetalleDTO> detalles, PrecioUnitarioDetalleDTO padre)
        {
            var detallesHijos = detalles.Where(z => z.IdPrecioUnitarioDetallePerteneciente == padre.Id).ToList();
            decimal total = 0;
            foreach (PrecioUnitarioDetalleDTO registro in detallesHijos)
            {
                if (registro.EsCompuesto == true)
                {
                    var precioRegistroHijo = await RecalcularArmadoHijos(insumos, detalles, registro);
                    total = total + precioRegistroHijo;
                }
                else
                {
                    total = total + registro.Importe;
                }
            }
            var inusmo = insumos.Where(z => z.id == padre.IdInsumo).FirstOrDefault();
            inusmo.CostoUnitario = total;
            await _InsumoService.Editar(inusmo);
            return total;
        }

        public async Task CrearPresupuestoConExel(List<IFormFile> files, int IdProyecto)
        {
            foreach (var document in files)
            {
                var PreciosImportados = new List<PrecioUnitarioDTO>();
                var nombreDocument = document.FileName;
                using (var stream = new MemoryStream())
                {
                    await document.CopyToAsync(stream); // Copiar el archivo al MemoryStream
                    stream.Position = 0; // Reiniciar la posición del stream

                    // Registrar codificación para evitar problemas con caracteres especiales
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        do
                        {
                            while (reader.Read())
                            {
                                if (reader.GetValue(0) == null)
                                {
                                    continue;
                                }
                                if (reader.GetValue(0).ToString().ToUpper() == "CLAVE")
                                {
                                    continue;
                                }
                                var nuevoPU = new PrecioUnitarioDTO
                                {
                                    Codigo = reader.GetValue(0) != null ? reader.GetValue(0)?.ToString() : "",
                                    Descripcion = reader.GetValue(1) != null ? reader.GetValue(1)?.ToString() : "",
                                    Unidad = reader.GetValue(1) != null ? reader.GetValue(2)?.ToString() : "",
                                    Cantidad = Convert.ToDecimal(reader.GetValue(3) ?? 0),
                                    CodigoPadre = reader.GetValue(4) != null ? reader.GetValue(4)?.ToString() : "",
                                    IdProyecto = IdProyecto
                                };
                                PreciosImportados.Add(nuevoPU);
                            }
                        } while (reader.NextResult());
                    }

                }

                var padres = PreciosImportados.Where(z => z.CodigoPadre == "").ToList();
                foreach (var item in padres)
                {
                    item.Hijos = await EstructurarPresupuestoExcelHijos(PreciosImportados, item);
                    if (item.Hijos.Count() > 0)
                    {
                        item.TipoPrecioUnitario = 0;
                        item.Cantidad = 1;
                    }
                    else
                    {
                        item.TipoPrecioUnitario = 1;
                    }
                }
                var NuevaPartidas = new List<PrecioUnitarioDTO>();
                NuevaPartidas.Add(new PrecioUnitarioDTO()
                {
                    Codigo = document.FileName,
                    Descripcion = "Presupuesto importado de " + document.FileName,
                    TipoPrecioUnitario = 0,
                    Cantidad = 1,
                    IdProyecto = IdProyecto,
                    Hijos = padres,
                });

                var paraetros = new PreciosParaEditarPosicionDTO();
                paraetros.Seleccionado = new PrecioUnitarioDTO();
                paraetros.Seleccionado.IdProyecto = IdProyecto;

                var registrosSinEstructurar = await ObtenerPrecioUnitarioSinEstructurar(IdProyecto);

                await CopiarPU(NuevaPartidas, 0, paraetros, registrosSinEstructurar, _dbContex);
            }

            return;
        }

        public async Task<List<PrecioUnitarioDTO>> EstructurarPresupuestoExcel(List<PrecioUnitarioDTO> lista)
        {
            var listaEstructurada = new List<PrecioUnitarioDTO>();
            var padres = lista.Where(z => z.CodigoPadre == "").ToList();
            foreach (var item in padres)
            {
                item.Hijos = await EstructurarPresupuestoExcelHijos(lista, item);
                if (item.Hijos.Count() > 0)
                {
                    item.TipoPrecioUnitario = 0;
                    item.Cantidad = 1;
                }
                else
                {
                    item.TipoPrecioUnitario = 1;
                }
            }

            return padres;
        }

        public async Task<List<PrecioUnitarioDTO>> EstructurarPresupuestoExcelHijos(List<PrecioUnitarioDTO> lista, PrecioUnitarioDTO padre)
        {
            var listaEstructurada = new List<PrecioUnitarioDTO>();
            var hijos = lista.Where(z => z.CodigoPadre == padre.Codigo).ToList();
            foreach (var item in hijos)
            {
                item.TipoPrecioUnitario = 0;
                item.Hijos = await EstructurarPresupuestoExcelHijos(lista, item);
                if (item.Hijos.Count() > 0)
                {
                    item.TipoPrecioUnitario = 0;
                    item.Cantidad = 1;
                }
                else
                {
                    item.TipoPrecioUnitario = 1;
                }
            }

            return hijos;
        }

        public async Task CreardeExcel(int numero)
        {
            string path = @"D:\ExcelBimsa\Libro1.xlsx";
            SLDocument sl = new SLDocument(path);

            int iRow = iRow = 2;
            var insumos = await _InsumoService.ObtenXIdProyecto(1);

            while (!string.IsNullOrEmpty(sl.GetCellValueAsString(iRow, 1)))
            {
                string ClavePadre = sl.GetCellValueAsString(iRow, 1);
                string ClaveHijos = sl.GetCellValueAsString(iRow, 2);
                decimal Cantidad = sl.GetCellValueAsDecimal(iRow, 3);
                string Compuesto = sl.GetCellValueAsString(iRow, 4);
                decimal Costo = sl.GetCellValueAsDecimal(iRow, 5);
                if (ClaveHijos != "")
                {
                    var insumo = insumos.Where(z => z.Codigo == ClavePadre).ToList();//insumo padre
                    var insumoHijo = insumos.Where(z => z.Codigo == ClaveHijos).ToList();//insumo hijo
                    var detalles = await _PrecioUnitarioDetalleService.ObtenerTodosXIdPrecioUnitario(2);
                    var detallesFiltrados = detalles.Where(z => z.IdPrecioUnitarioDetallePerteneciente == 0);
                    var detalle = detalles.Where(z => z.IdInsumo == insumo[0].id).ToList();
                    if (detalle.Count > 0)
                    {
                        var nuevoDetalle = new PrecioUnitarioDetalleDTO();
                        nuevoDetalle.IdPrecioUnitarioDetallePerteneciente = detalle[0].Id;
                        if (ClaveHijos == "100200-1000")
                        {
                            var nuevoInsumo = new InsumoCreacionDTO();
                            nuevoInsumo.Codigo = insumoHijo[0].Codigo;
                            nuevoInsumo.Descripcion = insumoHijo[0].Descripcion;
                            nuevoInsumo.Codigo = insumoHijo[0].Unidad;
                            nuevoInsumo.idTipoInsumo = insumoHijo[0].idTipoInsumo;
                            nuevoInsumo.CostoUnitario = Costo;
                            nuevoInsumo.IdProyecto = 1;
                            var nuevoInsumoCreado = await _InsumoService.CrearYObtener(nuevoInsumo);
                            nuevoDetalle.IdInsumo = nuevoInsumoCreado.id;
                        }
                        else
                        {
                            nuevoDetalle.IdInsumo = insumoHijo[0].id;
                        }
                        if (ClaveHijos == "100200-1045")
                        {
                            var nuevoInsumo = new InsumoCreacionDTO();
                            nuevoInsumo.Codigo = insumoHijo[0].Codigo;
                            nuevoInsumo.Descripcion = insumoHijo[0].Descripcion;
                            nuevoInsumo.Codigo = insumoHijo[0].Unidad;
                            nuevoInsumo.idTipoInsumo = insumoHijo[0].idTipoInsumo;
                            nuevoInsumo.CostoUnitario = Costo;
                            nuevoInsumo.IdProyecto = 1;
                            var nuevoInsumoCreado = await _InsumoService.CrearYObtener(nuevoInsumo);
                            nuevoDetalle.IdInsumo = nuevoInsumoCreado.id;
                        }
                        nuevoDetalle.IdPrecioUnitario = 2;
                        if (Compuesto == "Compuesto")
                        {
                            nuevoDetalle.EsCompuesto = true;
                        }
                        else
                        {
                            nuevoDetalle.EsCompuesto = false;
                        }
                        nuevoDetalle.Cantidad = Cantidad;
                        nuevoDetalle.CantidadExcedente = 0;
                        var detalleCreado = await _PrecioUnitarioDetalleService.CrearYObtener(nuevoDetalle);
                    }
                    else
                    {
                        var nuevoDetalle = new PrecioUnitarioDetalleDTO();
                        nuevoDetalle.IdPrecioUnitarioDetallePerteneciente = 0;
                        nuevoDetalle.IdInsumo = insumo[0].id;
                        nuevoDetalle.IdPrecioUnitario = 2;
                        nuevoDetalle.EsCompuesto = true;
                        nuevoDetalle.Cantidad = 0;
                        nuevoDetalle.CantidadExcedente = 0;
                        var detalleCreado = await _PrecioUnitarioDetalleService.CrearYObtener(nuevoDetalle);
                        ////////////////////////////////////////////////////////////////////////////////////////////


                        var nuevoDetalleHijo = new PrecioUnitarioDetalleDTO();
                        nuevoDetalleHijo.IdPrecioUnitarioDetallePerteneciente = detalleCreado.Id;
                        if (ClaveHijos == "100200-1000")
                        {
                            var nuevoInsumo = new InsumoCreacionDTO();
                            nuevoInsumo.Codigo = insumoHijo[0].Codigo;
                            nuevoInsumo.Descripcion = insumoHijo[0].Descripcion;
                            nuevoInsumo.Codigo = insumoHijo[0].Unidad;
                            nuevoInsumo.idTipoInsumo = insumoHijo[0].idTipoInsumo;
                            nuevoInsumo.CostoUnitario = Costo;
                            nuevoInsumo.IdProyecto = 1;
                            var nuevoInsumoCreado = await _InsumoService.CrearYObtener(nuevoInsumo);
                            insumoHijo[0] = nuevoInsumoCreado;
                        }
                        if (ClaveHijos == "100200-1045")
                        {
                            var nuevoInsumo = new InsumoCreacionDTO();
                            nuevoInsumo.Codigo = insumoHijo[0].Codigo;
                            nuevoInsumo.Descripcion = insumoHijo[0].Descripcion;
                            nuevoInsumo.Codigo = insumoHijo[0].Unidad;
                            nuevoInsumo.idTipoInsumo = insumoHijo[0].idTipoInsumo;
                            nuevoInsumo.CostoUnitario = Costo;
                            nuevoInsumo.IdProyecto = 1;
                            var nuevoInsumoCreado = await _InsumoService.CrearYObtener(nuevoInsumo);
                            insumoHijo[0] = nuevoInsumoCreado;
                        }
                        nuevoDetalleHijo.IdInsumo = insumoHijo[0].id;
                        nuevoDetalleHijo.IdPrecioUnitario = 2;
                        if (Compuesto == "Compuesto")
                        {
                            nuevoDetalleHijo.EsCompuesto = true;
                        }
                        else
                        {
                            nuevoDetalleHijo.EsCompuesto = false;
                        }
                        nuevoDetalleHijo.Cantidad = Cantidad;
                        nuevoDetalleHijo.CantidadExcedente = 0;
                        var detalleHijoCreado = await _PrecioUnitarioDetalleService.CrearYObtener(nuevoDetalleHijo);
                    }
                }
                iRow++;
            }
        }

        public async Task<List<InsumoParaExplosionDTO>> ObtenerExplosionDeInsumoXConcepto(int IdPrecioUnitario)
        {
            var PrecioUnitario = await _PrecioUnitarioService.ObtenXId(IdPrecioUnitario);
            var ExplosionInsumos = new List<InsumoParaExplosionDTO>();
            var ExplosionInsumosSinRepetidos = new List<InsumoParaExplosionDTO>();
            var registros = await ObtenerDetallesPorPU(IdPrecioUnitario, _dbContex);
            var registrosInsumos = registros.Where(z => z.EsCompuesto == false).ToList();
            for (int i = 0; i < registrosInsumos.Count; i++)
            {
                if (registrosInsumos[i].IdPrecioUnitarioDetallePerteneciente > 0)
                {
                    var registroInsumo = await CalcularDetalleHijo(registrosInsumos[i], registros);
                    ExplosionInsumos.Add(registroInsumo);
                }
                else
                {
                    var insumo = new InsumoParaExplosionDTO();
                    insumo.id = registrosInsumos[i].IdInsumo;
                    insumo.Codigo = registrosInsumos[i].Codigo;
                    insumo.Descripcion = registrosInsumos[i].Descripcion;
                    insumo.Unidad = registrosInsumos[i].Unidad;
                    insumo.CostoUnitario = registrosInsumos[i].CostoUnitario;
                    insumo.CostoBase = registrosInsumos[i].CostoBase;
                    insumo.idTipoInsumo = registrosInsumos[i].IdTipoInsumo;
                    insumo.idFamiliaInsumo = registrosInsumos[i].IdFamiliaInsumo;
                    insumo.Cantidad = registrosInsumos[i].Cantidad;
                    insumo.Importe = registrosInsumos[i].Importe;
                    insumo.IdProyecto = PrecioUnitario.IdProyecto;
                    insumo.CostoUnitarioConFormato = String.Format("{0:#,##0.00}", insumo.CostoUnitario);
                    insumo.CostoBaseConFormato = String.Format("{0:#,##0.00}", insumo.CostoBase);
                    ExplosionInsumos.Add(insumo);
                }
            }
            for (int i = 0; i < ExplosionInsumos.Count; i++)
            {
                ExplosionInsumos[i].Cantidad = ExplosionInsumos[i].Cantidad * PrecioUnitario.Cantidad;
                ExplosionInsumos[i].CantidadConFormato = String.Format("{0:#,##0.0000}", ExplosionInsumos[i].Cantidad);
                ExplosionInsumos[i].CostoBaseConFormato = String.Format("{0:#,##0.00}", ExplosionInsumos[i].CostoBase);
                ExplosionInsumos[i].CostoUnitarioConFormato = String.Format("{0:#,##0.00}", ExplosionInsumos[i].CostoUnitario);
                ExplosionInsumos[i].Importe = ExplosionInsumos[i].Cantidad * ExplosionInsumos[i].CostoUnitario;
                ExplosionInsumos[i].ImporteConFormato = String.Format("{0:#,##0.00}", ExplosionInsumos[i].Importe);
            }
            return ExplosionInsumos;
        }

        public async Task<InsumoParaExplosionDTO> CalcularDetalleHijo(PrecioUnitarioDetalleDTO registro, List<PrecioUnitarioDetalleDTO> registros)
        {
            //var registroPadre = registros.Where(z => z.Id == registro.IdPrecioUnitarioDetallePerteneciente).FirstOrDefault();
            var registrosPadres = registros.Where(z => z.Id == registro.IdPrecioUnitarioDetallePerteneciente).ToList();
            if (registrosPadres.Count > 0)
            {
                var registroPadre = registrosPadres.FirstOrDefault();
                registro.Cantidad = registro.Cantidad * registroPadre.Cantidad;
                registro.Importe = registro.Cantidad * registro.CostoUnitario;
                registro.IdPrecioUnitarioDetallePerteneciente = registroPadre.IdPrecioUnitarioDetallePerteneciente;
                if (registro.IdPrecioUnitarioDetallePerteneciente > 0)
                {
                    var registroInsumo = await CalcularDetalleHijo(registro, registros);
                    return registroInsumo;
                }
                else
                {
                    var insumo = new InsumoParaExplosionDTO();
                    insumo.id = registro.IdInsumo;
                    insumo.Codigo = registro.Codigo;
                    insumo.Descripcion = registro.Descripcion;
                    insumo.Unidad = registro.Unidad;
                    insumo.CostoUnitario = registro.CostoUnitario;
                    insumo.CostoBase = registro.CostoBase;
                    insumo.idTipoInsumo = registro.IdTipoInsumo;
                    insumo.idFamiliaInsumo = registro.IdFamiliaInsumo;
                    insumo.Cantidad = registro.Cantidad;
                    insumo.Importe = registro.Importe;
                    return insumo;
                }
            }
            else
            {
                return new InsumoParaExplosionDTO();
            }
        }

        public async Task<List<InsumoParaExplosionDTO>> obtenerExplosion(int IdProyecto)
        {
            var ExplosionDeInsumos = new List<InsumoParaExplosionDTO>();
            var ExplosionDeInsumosSinRepetir = new List<InsumoParaExplosionDTO>();
            var Registros = await ObtenerPrecioUnitario(IdProyecto);
            for (int i = 0; i < Registros.Count; i++)
            {
                if (Registros[i].TipoPrecioUnitario == 0)
                {
                    var InsumosHijos = obtenerExplosionHijos(Registros[i], Registros[i].Hijos).Result;
                    for (int j = 0; j < InsumosHijos.Count; j++)
                    {
                        ExplosionDeInsumos.Add(InsumosHijos[j]);
                    }
                }
                else
                {
                    var ExplosionConcepto = await ObtenerExplosionDeInsumoXConcepto(Registros[i].Id);
                    for (int j = 0; j < ExplosionConcepto.Count; j++)
                    {
                        ExplosionDeInsumos.Add(ExplosionConcepto[j]);
                    }
                }
            }
            var agrupados = ExplosionDeInsumos.GroupBy(z => new { z.Codigo, z.Descripcion, z.CostoUnitario }).Select(x => new InsumoParaExplosionDTO
            {
                id = x.First().id,
                idFamiliaInsumo = x.First().idFamiliaInsumo,
                idTipoInsumo = x.First().idTipoInsumo,
                Unidad = x.First().Unidad,
                IdProyecto = x.First().IdProyecto,
                EsFsrGlobal = x.First().EsFsrGlobal,
                CostoUnitario = x.First().CostoUnitario,
                CostoUnitarioConFormato = x.First().CostoUnitarioConFormato,
                CostoBase = x.First().CostoBase,
                CostoBaseConFormato = x.First().CostoBaseConFormato,
                Codigo = x.Key.Codigo,
                Descripcion = x.Key.Descripcion,
                Cantidad = x.Sum(z => z.Cantidad),
                CantidadConFormato = String.Format("{0:#,##0.00}", x.Sum(z => z.Cantidad)),
                Importe = x.Sum(z => z.Cantidad) * x.First().CostoUnitario,
                ImporteConFormato = String.Format("{0:#,##0.00}", x.Sum(z => z.Cantidad) * x.First().CostoUnitario)
            }).ToList();
            //foreach (var item in agrupados) {
            //    var insumo = new InsumoParaExplosionDTO();
            //    insumo.id = item.id;
            //    insumo.idFamiliaInsumo = item.idFamiliaInsumo;
            //    insumo.idTipoInsumo = item.idTipoInsumo;
            //    insumo.Unidad = item.Unidad;
            //    insumo.Descripcion = item.Descripcion;
            //    insumo.Codigo = item.Codigo;
            //    insumo.IdProyecto = item.IdProyecto;
            //    insumo.EsFsrGlobal = item.EsFsrGlobal;
            //    insumo.CostoUnitario = item.CostoUnitario;
            //    insumo.CostoUnitarioConFormato = item.CostoUnitarioConFormato;
            //    insumo.CostoBase = item.CostoBase;
            //    insumo.CostoBaseConFormato = item.CostoBaseConFormato;
            //    decimal cantidadTotal = item.Cantidad;
            //    insumo.Cantidad = cantidadTotal;
            //    insumo.CantidadConFormato = String.Format("{0:#,##0.00}", cantidadTotal);
            //    decimal importeTotal = item.Importe;
            //    insumo.ImporteConFormato = String.Format("{0:#,##0.00}", importeTotal);
            //    insumo.Importe = importeTotal;

            //    ExplosionDeInsumosSinRepetir.Add(insumo);
            //}
            //for (int i = 0; i < ExplosionDeInsumos.Count; i++)
            //{
            //    var aux = ExplosionDeInsumosSinRepetir.Where(z => z.Codigo == ExplosionDeInsumos[i].Codigo).ToList();
            //    if (aux.Count > 0)
            //    {
            //        ExplosionDeInsumosSinRepetir.Where(z => z.Codigo == ExplosionDeInsumos[i].Codigo).FirstOrDefault().Cantidad = (ExplosionDeInsumosSinRepetir.Where(z => z.Codigo == ExplosionDeInsumos[i].Codigo).FirstOrDefault().Cantidad + ExplosionDeInsumos[i].Cantidad);
            //        ExplosionDeInsumosSinRepetir.Where(z => z.Codigo == ExplosionDeInsumos[i].Codigo).FirstOrDefault().Importe = (ExplosionDeInsumosSinRepetir.Where(z => z.Codigo == ExplosionDeInsumos[i].Codigo).FirstOrDefault().Cantidad * ExplosionDeInsumosSinRepetir.Where(z => z.Codigo == ExplosionDeInsumos[i].Codigo).FirstOrDefault().CostoUnitario);
            //    }
            //    else
            //    {
            //        ExplosionDeInsumosSinRepetir.Add(ExplosionDeInsumos[i]);
            //    }
            //}
            return agrupados.Where(z => z.id != 0).OrderBy(z => z.idTipoInsumo).ToList();
        }

        public async Task<List<InsumoParaExplosionDTO>> obtenerExplosionHijos(PrecioUnitarioDTO registro, List<PrecioUnitarioDTO> RegistrosHijos)
        {
            var ExplosionDeInsumos = new List<InsumoParaExplosionDTO>();
            for (int i = 0; i < RegistrosHijos.Count; i++)
            {
                if (RegistrosHijos[i].TipoPrecioUnitario == 0)
                {
                    var InsumosHijos = obtenerExplosionHijos(RegistrosHijos[i], RegistrosHijos[i].Hijos).Result;
                    for (int j = 0; j < InsumosHijos.Count; j++)
                    {
                        ExplosionDeInsumos.Add(InsumosHijos[j]);
                    }
                }
                else
                {
                    var ExplosionConcepto = await ObtenerExplosionDeInsumoXConcepto(RegistrosHijos[i].Id);
                    for (int j = 0; j < ExplosionConcepto.Count; j++)
                    {
                        ExplosionDeInsumos.Add(ExplosionConcepto[j]);
                    }
                }
            }
            return ExplosionDeInsumos;
        }

        /////////////////////////////////////////////// CorreccionBimsa
        public async Task<List<PrecioUnitarioDetalleDTO>> ObtenerDetallesSinFiltrar(int IdPrecioUnitario)
        {
            var PU = await _PrecioUnitarioService.ObtenXId(IdPrecioUnitario);
            var lista = await _PrecioUnitarioDetalleService.ObtenerTodosXIdPrecioUnitario(IdPrecioUnitario);
            var insumos = await _InsumoService.ObtenXIdProyecto(PU.IdProyecto);
            var listaFiltrada = lista.ToList();
            for (int i = 0; i < listaFiltrada.Count; i++)
            {
                var insumo = insumos.Where(z => z.id == listaFiltrada[i].IdInsumo).FirstOrDefault();
                listaFiltrada[i].Codigo = insumo.Codigo;
                listaFiltrada[i].Descripcion = insumo.Descripcion;
                listaFiltrada[i].Unidad = insumo.Unidad;
                listaFiltrada[i].CostoUnitario = insumo.CostoUnitario;
                listaFiltrada[i].IdTipoInsumo = insumo.idTipoInsumo;
                listaFiltrada[i].IdFamiliaInsumo = insumo.idFamiliaInsumo;
                listaFiltrada[i].Importe = listaFiltrada[i].Cantidad * listaFiltrada[i].CostoUnitario;
                listaFiltrada[i].ImporteConFormato = String.Format("{0:#,##0.00}", listaFiltrada[i].Importe);
                listaFiltrada[i].CantidadConFormato = String.Format("{0:#,##0.0000}", listaFiltrada[i].Cantidad);
                listaFiltrada[i].CostoUnitarioConFormato = String.Format("{0:#,##0.00}", listaFiltrada[i].CostoUnitario);
            }
            return listaFiltrada;
        }

        public async Task CorreccionBimsa()
        {
            var registros = await ObtenerDetallesSinFiltrar(2);
            for (int i = 0; i < registros.Count; i++)
            {
                if (registros[i].EsCompuesto == true)
                {
                    var hijos = registros.Where(z => z.IdPrecioUnitarioDetallePerteneciente == registros[i].Id);
                    if (hijos.Count() <= 0)
                    {
                        var registrosOriginalConElInsumo = registros.Where(z => z.IdInsumo == registros[i].IdInsumo);
                        var registroOriginal = registrosOriginalConElInsumo.FirstOrDefault();
                        var detallesACopiar = registros.Where(z => z.IdPrecioUnitarioDetallePerteneciente == registroOriginal.Id).ToList();
                        for (int j = 0; j < detallesACopiar.Count; j++)
                        {
                            var nuevoRegistroParaCrear = new PrecioUnitarioDetalleDTO();
                            nuevoRegistroParaCrear.IdPrecioUnitario = detallesACopiar[j].IdPrecioUnitario;
                            nuevoRegistroParaCrear.IdInsumo = detallesACopiar[j].IdInsumo;
                            nuevoRegistroParaCrear.EsCompuesto = detallesACopiar[j].EsCompuesto;
                            nuevoRegistroParaCrear.CostoUnitario = detallesACopiar[j].CostoUnitario;
                            nuevoRegistroParaCrear.CostoUnitarioConFormato = detallesACopiar[j].CostoUnitarioConFormato;
                            nuevoRegistroParaCrear.CostoUnitarioEditado = detallesACopiar[j].CostoUnitarioEditado;
                            nuevoRegistroParaCrear.Cantidad = detallesACopiar[j].Cantidad;
                            nuevoRegistroParaCrear.CantidadConFormato = detallesACopiar[j].CantidadConFormato;
                            nuevoRegistroParaCrear.CantidadEditado = detallesACopiar[j].CantidadEditado;
                            nuevoRegistroParaCrear.CantidadExcedente = detallesACopiar[j].CantidadExcedente;
                            nuevoRegistroParaCrear.IdPrecioUnitarioDetallePerteneciente = registros[i].Id;
                            nuevoRegistroParaCrear.Codigo = detallesACopiar[j].Codigo;
                            nuevoRegistroParaCrear.Descripcion = detallesACopiar[j].Descripcion;
                            nuevoRegistroParaCrear.Unidad = detallesACopiar[j].Unidad;
                            nuevoRegistroParaCrear.IdTipoInsumo = detallesACopiar[j].IdTipoInsumo;
                            nuevoRegistroParaCrear.IdFamiliaInsumo = detallesACopiar[j].IdFamiliaInsumo;
                            nuevoRegistroParaCrear.Importe = detallesACopiar[j].Importe;
                            nuevoRegistroParaCrear.ImporteConFormato = detallesACopiar[j].ImporteConFormato;
                            var registroCreado = await _PrecioUnitarioDetalleService.CrearYObtener(nuevoRegistroParaCrear);
                            nuevoRegistroParaCrear.Id = registroCreado.Id;
                            if (nuevoRegistroParaCrear.EsCompuesto == true)
                            {
                                var registrosHijosCreados = await CrearDetallesHijosCorreccion(registros, nuevoRegistroParaCrear);
                            }
                        }
                    }
                }
            }
        }

        public async Task<List<PrecioUnitarioDetalleDTO>> CrearDetallesHijosCorreccion(List<PrecioUnitarioDetalleDTO> registros, PrecioUnitarioDetalleDTO registroCreado)
        {
            if (registroCreado.EsCompuesto == true)
            {
                var registroOriginal = registros.Where(z => z.IdInsumo == registroCreado.IdInsumo).FirstOrDefault();
                var registrosHijos = registros.Where(z => z.IdPrecioUnitarioDetallePerteneciente == registroOriginal.Id).ToList();
                foreach (PrecioUnitarioDetalleDTO registroHijo in registrosHijos)
                {
                    var nuevoRegistroParaCrear = new PrecioUnitarioDetalleDTO();
                    nuevoRegistroParaCrear.IdPrecioUnitario = registroHijo.IdPrecioUnitario;
                    nuevoRegistroParaCrear.IdInsumo = registroHijo.IdInsumo;
                    nuevoRegistroParaCrear.EsCompuesto = registroHijo.EsCompuesto;
                    nuevoRegistroParaCrear.CostoUnitario = registroHijo.CostoUnitario;
                    nuevoRegistroParaCrear.CostoUnitarioConFormato = registroHijo.CostoUnitarioConFormato;
                    nuevoRegistroParaCrear.CostoUnitarioEditado = registroHijo.CostoUnitarioEditado;
                    nuevoRegistroParaCrear.Cantidad = registroHijo.Cantidad;
                    nuevoRegistroParaCrear.CantidadConFormato = registroHijo.CantidadConFormato;
                    nuevoRegistroParaCrear.CantidadEditado = registroHijo.CantidadEditado;
                    nuevoRegistroParaCrear.CantidadExcedente = registroHijo.CantidadExcedente;
                    nuevoRegistroParaCrear.IdPrecioUnitarioDetallePerteneciente = registroCreado.Id;
                    nuevoRegistroParaCrear.Codigo = registroHijo.Codigo;
                    nuevoRegistroParaCrear.Descripcion = registroHijo.Descripcion;
                    nuevoRegistroParaCrear.Unidad = registroHijo.Unidad;
                    nuevoRegistroParaCrear.IdTipoInsumo = registroHijo.IdTipoInsumo;
                    nuevoRegistroParaCrear.IdFamiliaInsumo = registroHijo.IdFamiliaInsumo;
                    nuevoRegistroParaCrear.Importe = registroHijo.Importe;
                    nuevoRegistroParaCrear.ImporteConFormato = registroHijo.ImporteConFormato;
                    var registroCreadoHijo = await _PrecioUnitarioDetalleService.CrearYObtener(nuevoRegistroParaCrear);
                    nuevoRegistroParaCrear.Id = registroCreadoHijo.Id;
                    if (registroHijo.EsCompuesto == true)
                    {
                        var registrosHijosCreados = await CrearDetallesHijosCorreccion(registros, nuevoRegistroParaCrear);
                    }
                }
            }
            return registros;
        }

        public async Task RecalcularPresupuesto(int IdProyecto, DbContext db)
        {
            var preciosUnitarios = await ObtenerPrecioUnitarioSinEstructurar(IdProyecto);
            var conceptos = preciosUnitarios.Where(z => z.TipoPrecioUnitario == 1).ToList();
            foreach (PrecioUnitarioDTO concepto in conceptos)
            {
                var insumos = await _InsumoService.ObtenXIdProyecto(IdProyecto);
                var detalles = await ObtenerDetallesPorPU(concepto.Id, db);
                var valores = await RecalcularDetalles(concepto.Id, detalles, insumos);
                var conceptoParaEditar = await _ConceptoService.ObtenXId(concepto.IdConcepto);
                conceptoParaEditar.CostoUnitario = valores.Total;
                var conceptoEditado = await _ConceptoService.Editar(conceptoParaEditar);
                await RecalcularPrecioUnitario(concepto);
            }
        }

        public async Task<bool> EditarIndirectoXConcepto(int IdConcepto, decimal PorcentajeIndirecto)
        {
            var PU = await _PrecioUnitarioService.ObtenXIdConcepto(IdConcepto);
            PU.PorcentajeIndirecto = PorcentajeIndirecto;
            return await EditarIndirectoPrecioUnitario(PU);
        }

        public async Task<bool> EditarIndirectoPrecioUnitario(PrecioUnitarioDTO registro)
        {
            var registros = await _PrecioUnitarioService.ObtenerTodos(registro.IdProyecto);
            var existenHijos = registros.Where(z => z.IdPrecioUnitarioBase == registro.Id).ToList();
            var concepto = await _ConceptoService.ObtenXId(registro.IdConcepto);

            concepto.PorcentajeIndirecto = registro.PorcentajeIndirecto;
            var editarPadre = await _ConceptoService.Editar(concepto);
            if (existenHijos.Count() > 0)
            {
                foreach (var hijos in existenHijos)
                {
                    hijos.PorcentajeIndirecto = registro.PorcentajeIndirecto;
                    await EditarIndirectosPrecioUnitarioHijo(hijos, registros);
                }
            }
            return true;
        }

        public async Task<PrecioUnitarioDTO> EditarIndirectosPrecioUnitarioHijo(PrecioUnitarioDTO padre, List<PrecioUnitarioDTO> registros)
        {
            var concepto = await _ConceptoService.ObtenXId(padre.IdConcepto);
            concepto.PorcentajeIndirecto = padre.PorcentajeIndirecto;
            var editarPadre = await _ConceptoService.Editar(concepto);
            var existenHijos = registros.Where(z => z.IdPrecioUnitarioBase == padre.Id);
            if (existenHijos.Count() > 0)
            {
                foreach (var hijos in existenHijos)
                {
                    hijos.PorcentajeIndirecto = padre.PorcentajeIndirecto;
                    await EditarIndirectosPrecioUnitarioHijo(hijos, registros);
                }
            }
            return padre;
        }

        public async Task modificarPosicion(PreciosParaEditarPosicionDTO registros)
        {
            var registrosSinEstructurar = await ObtenerPrecioUnitarioSinEstructurar(registros.Seleccionado.IdProyecto);
            var registroPadreOriginal = new PrecioUnitarioDTO();
            if (registros.Seleccionado.IdPrecioUnitarioBase != 0)
            {
                registroPadreOriginal = registrosSinEstructurar.Where(z => z.Id == registros.Seleccionado.IdPrecioUnitarioBase).FirstOrDefault();
            }

            var precios = new List<PrecioUnitarioDTO>();

            if (registros.EsSubnivel && registros.Destino.TipoPrecioUnitario == 0)
            {
                if (registros.EsCopiado)
                {
                    if (registros.Seleccionado.Id == registros.Destino.IdPrecioUnitarioBase)
                    {
                        return;
                    }
                    var hijos = registrosSinEstructurar.Where(z => z.IdPrecioUnitarioBase == registros.Seleccionado.Id).ToList();
                    foreach (var hijo in hijos)
                    {
                        var mismaFamilia = await ValidaMismaFamilia(registrosSinEstructurar, registros.Destino, hijo);
                        if (mismaFamilia)
                        {
                            return;
                        }
                    }
                    precios.Add(registros.Seleccionado);
                    await CopiarPU(precios, registros.Destino.Id, registros, registrosSinEstructurar, _dbContex);
                }
                else
                {
                    await Subnivel(registrosSinEstructurar, registros);
                }
            }
            else if (!registros.EsSubnivel)
            {
                if (registros.EsCopiado)
                {
                    if (registros.Seleccionado.Id == registros.Destino.IdPrecioUnitarioBase)
                    {
                        return;
                    }
                    var hijos = registrosSinEstructurar.Where(z => z.IdPrecioUnitarioBase == registros.Seleccionado.Id).ToList();
                    foreach (var hijo in hijos)
                    {
                        var mismaFamilia = await ValidaMismaFamilia(registrosSinEstructurar, registros.Destino, hijo);
                        if (mismaFamilia)
                        {
                            return;
                        }
                    }
                    precios.Add(registros.Seleccionado);
                    await CopiarPU(precios, registros.Destino.IdPrecioUnitarioBase, registros, registrosSinEstructurar, _dbContex);
                }
                else
                {
                    await MismoNivel(registrosSinEstructurar, registros);
                }
            }
            registrosSinEstructurar = await ObtenerPrecioUnitarioSinEstructurar(registros.Seleccionado.IdProyecto);
            var registrosHijos = registrosSinEstructurar.Where(z => z.IdPrecioUnitarioBase == registroPadreOriginal.Id).ToList();
            if (registrosHijos.Count() > 1)
            {
                for (int i = 0; i < registrosHijos.Count; i++)
                {
                    if (registrosHijos[i].Id != registros.Seleccionado.Id)
                    {
                        await RecalcularPrecioUnitario(registrosHijos[i]);
                        i = registrosHijos.Count();
                    }
                }
            }
            else
            {
                registroPadreOriginal.Importe = 0;
                await RecalcularPrecioUnitario(registroPadreOriginal);
            }
        }

        public async Task Subnivel(List<PrecioUnitarioDTO> registrosSinEstructurar, PreciosParaEditarPosicionDTO registros)
        {
            if (registros.Seleccionado.Id == registros.Destino.IdPrecioUnitarioBase)
            {
                return;
            }
            var hijos = registrosSinEstructurar.Where(z => z.IdPrecioUnitarioBase == registros.Seleccionado.Id).ToList();
            foreach (var hijo in hijos)
            {
                var mismaFamilia = await ValidaMismaFamilia(registrosSinEstructurar, registros.Destino, hijo);
                if (mismaFamilia)
                {
                    return;
                }
            }
            var hermanos = registrosSinEstructurar.Where(z => z.IdPrecioUnitarioBase == registros.Destino.Id).OrderBy(z => z.Posicion).ToList();
            if (hermanos.Count() <= 0)
            {
                registros.Seleccionado.Posicion = 0;
            }
            else
            {
                registros.Seleccionado.Posicion = hermanos.Count();
            }
            registros.Seleccionado.Nivel = registros.Destino.Nivel + 1;
            await AjustarNivelHijos(registros.Seleccionado, registrosSinEstructurar);
            registros.Seleccionado.IdPrecioUnitarioBase = registros.Destino.Id;
            await Editar(registros.Seleccionado);
        }

        public async Task<bool> ValidaMismaFamilia(List<PrecioUnitarioDTO> registrosSinEstructurar, PrecioUnitarioDTO destino, PrecioUnitarioDTO seleccionado)
        {
            var mismaFamilia = false;
            if (seleccionado.Id == destino.IdPrecioUnitarioBase)
            {
                return true;
            }
            var hijos = registrosSinEstructurar.Where(z => z.IdPrecioUnitarioBase == seleccionado.Id).ToList();
            foreach (var hijo in hijos)
            {
                mismaFamilia = await ValidaMismaFamilia(registrosSinEstructurar, destino, hijo);
                if (mismaFamilia)
                {
                    return true;
                }
            }
            return mismaFamilia;
        }

        public async Task MismoNivel(List<PrecioUnitarioDTO> registrosSinEstructurar, PreciosParaEditarPosicionDTO registros)
        {
            if (registros.Seleccionado.Id == registros.Destino.IdPrecioUnitarioBase)
            {
                return;
            }
            var hijos = registrosSinEstructurar.Where(z => z.IdPrecioUnitarioBase == registros.Seleccionado.Id).ToList();
            foreach (var hijo in hijos)
            {
                var mismaFamilia = await ValidaMismaFamilia(registrosSinEstructurar, registros.Destino, hijo);
                if (mismaFamilia)
                {
                    return;
                }
            }
            var registrosFiltrados = registrosSinEstructurar.Where(z => z.IdPrecioUnitarioBase == registros.Destino.IdPrecioUnitarioBase).OrderBy(z => z.Posicion).ToList();
            var index = registrosFiltrados.FindIndex(z => z.Id == registros.Destino.Id);
            var listaRegistrosOrdenados = new List<PrecioUnitarioDTO>();
            if (registros.Seleccionado.IdPrecioUnitarioBase == registros.Destino.IdPrecioUnitarioBase)
            {
                var indexEncontrado = false;
                var registroOriginalEncontrado = false;
                var destinoMenor = registros.Seleccionado.Posicion >= registros.Destino.Posicion ? true : false;
                if (destinoMenor)
                {
                    registros.Seleccionado.Posicion = index;
                    for (int i = 0; i < registrosFiltrados.Count; i++)
                    {
                        var desc = registrosFiltrados[i].Descripcion;
                        if (i == index)
                        {
                            registros.Seleccionado.Posicion = index;
                            listaRegistrosOrdenados.Add(registros.Seleccionado);
                            indexEncontrado = true;
                        }
                        if (indexEncontrado == true)
                        {
                            registrosFiltrados[i].Posicion = i + 1;
                        }
                        else
                        {
                            registrosFiltrados[i].Posicion = i;
                        }
                        if (registrosFiltrados[i].Id != registros.Seleccionado.Id)
                        {
                            listaRegistrosOrdenados.Add(registrosFiltrados[i]);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < registrosFiltrados.Count; i++)
                    {
                        registrosFiltrados[i].Posicion = i;
                        if (registrosFiltrados[i].Posicion > registros.Seleccionado.Posicion && registrosFiltrados[i].Posicion < registros.Destino.Posicion)
                        {
                            registrosFiltrados[i].Posicion = registrosFiltrados[i].Posicion - 1;
                        }
                        else if (registrosFiltrados[i].Id == registros.Seleccionado.Id)
                        {
                            registrosFiltrados[i].Posicion = index;
                        }
                        else if (registrosFiltrados[i].Id == registros.Destino.Id)
                        {
                            registrosFiltrados[i].Posicion = registrosFiltrados[i].Posicion - 1;
                        }
                        listaRegistrosOrdenados.Add(registrosFiltrados[i]);
                    }
                }

            }
            else
            {
                registros.Seleccionado.Posicion = index;

                var indexEncontrado = false;
                for (int i = 0; i < registrosFiltrados.Count; i++)
                {
                    if (i == index)
                    {
                        registros.Seleccionado.IdPrecioUnitarioBase = registros.Destino.IdPrecioUnitarioBase;
                        registros.Seleccionado.Nivel = registros.Destino.Nivel;
                        listaRegistrosOrdenados.Add(registros.Seleccionado);
                        indexEncontrado = true;
                    }
                    if (indexEncontrado == true)
                    {
                        registrosFiltrados[i].Posicion = i + 1;
                    }
                    else
                    {
                        registrosFiltrados[i].Posicion = i;
                    }
                    listaRegistrosOrdenados.Add(registrosFiltrados[i]);
                }
            }
            for (int i = 0; i < listaRegistrosOrdenados.Count; i++)
            {
                await Editar(listaRegistrosOrdenados[i]);
            }

            var hijosSeleccionado = registrosSinEstructurar.Where(z => z.IdPrecioUnitarioBase == registros.Seleccionado.Id).ToList();
            if (hijosSeleccionado.Count() > 0)
            {
                foreach (var hijo in hijosSeleccionado)
                {
                    hijo.Nivel = registros.Seleccionado.Nivel + 1;
                    await AjustarNivelHijos(hijo, registrosSinEstructurar);
                    await Editar(hijo);
                }
            }
        }

        public async Task AjustarNivelHijos(PrecioUnitarioDTO padre, List<PrecioUnitarioDTO> lista)
        {
            var hijos = lista.Where(z => z.IdPrecioUnitarioBase == padre.Id).ToList();
            if (hijos.Count() > 0)
            {
                foreach (var hijo in hijos)
                {
                    hijo.Nivel = padre.Nivel + 1;
                    await AjustarNivelHijos(hijo, lista);
                    await Editar(hijo);
                }
            }
        }

        public async Task CopiarPU(List<PrecioUnitarioDTO> precios, int IdPrecioUnitarioBase, PreciosParaEditarPosicionDTO registros, List<PrecioUnitarioDTO> registrosSinEstructurar, DbContext db)
        {
            var IdProyecto = registros.Seleccionado.IdProyecto;
            if (IdPrecioUnitarioBase > 0)
            {
                var precioUnitarioBase = await _PrecioUnitarioService.ObtenXId(IdPrecioUnitarioBase);
                for (int i = 0; i < precios.Count; i++)
                {

                    precios[i].Nivel = precioUnitarioBase.Nivel + 1;
                    precios[i].IdPrecioUnitarioBase = precioUnitarioBase.Id;
                    precios[i].IdProyecto = IdProyecto;
                    var Id = precios[i].Id;
                    precios[i].Id = 0;
                    var conceptos = await _ConceptoService.ObtenerTodos(IdProyecto);
                    var nuevoConcepto = new ConceptoDTO();
                    nuevoConcepto.IdProyecto = IdProyecto;
                    nuevoConcepto.Codigo = precios[i].Codigo;
                    nuevoConcepto.Descripcion = precios[i].Descripcion;
                    nuevoConcepto.Unidad = precios[i].Unidad;
                    nuevoConcepto.CostoUnitario = 0;
                    var conceptoCreado = await _ConceptoService.CrearYObtener(nuevoConcepto);
                    precios[i].IdConcepto = conceptoCreado.Id;
                    var nuevoRegistro = await _PrecioUnitarioService.CrearYObtener(precios[i]);
                    registrosSinEstructurar.Add(nuevoRegistro);
                    var Proyecto = await _ProyectoService.ObtenXId(IdProyecto);
                    ProgramacionEstimadaGanttDTO nuevaProgramacion = new ProgramacionEstimadaGanttDTO();
                    nuevaProgramacion.IdConcepto = nuevoRegistro.IdConcepto;
                    nuevaProgramacion.Start = Proyecto.FechaInicio;
                    nuevaProgramacion.End = Proyecto.FechaInicio;
                    nuevaProgramacion.IdProyecto = Proyecto.Id;
                    nuevaProgramacion.IdPrecioUnitario = nuevoRegistro.Id;
                    nuevaProgramacion.Duracion = 1;
                    nuevaProgramacion.Progress = 0;
                    if (nuevoRegistro.IdPrecioUnitarioBase != 0)
                    {
                        var programacionesEstimadas = await _ProgramacionEstimadaGanttService.ObtenerXIdProyecto(Proyecto.Id, db);
                        var programacionEstimadaPadre = programacionesEstimadas.Where(z => z.IdPrecioUnitario == nuevoRegistro.IdPrecioUnitarioBase).FirstOrDefault();
                        nuevaProgramacion.Parent = programacionEstimadaPadre.Id;
                    }
                    else
                    {
                        nuevaProgramacion.Parent = "";
                    }
                    await _ProgramacionEstimadaGanttService.CrearYObtener(nuevaProgramacion);
                    if (precios[i].Hijos.Count > 0)
                    {
                        await CopiarPU(precios[i].Hijos, nuevoRegistro.Id, registros, registrosSinEstructurar, db);
                    }
                    if (precios[i].TipoPrecioUnitario == 1)
                    {
                        var items = db.Database.SqlQueryRaw<string>(""""select Id, IdPrecioUnitario, IdInsumo, EsCompuesto, Cantidad, CantidadExcedente, IdPrecioUnitarioDetallePerteneciente from PrecioUnitarioDetalle where IdPrecioUnitario = '"""" + Id + """"' for json path"""").ToList();
                        if (items.Count > 0)
                        {
                            string json = string.Join("", items);
                            var datos = JsonSerializer.Deserialize<List<PrecioUnitarioDetalle>>(json);
                            var detalles = _Mapper.Map<List<PrecioUnitarioDetalleDTO>>(datos);
                            var precioUnitarioAuxiliar = await _PrecioUnitarioService.ObtenXId(Id);
                            var insumos = await _InsumoService.ObtenXIdProyecto(precioUnitarioAuxiliar.IdProyecto);
                            for (int j = 0; j < detalles.Count; j++)
                            {
                                var insumo = insumos.Where(z => z.id == detalles[j].IdInsumo).FirstOrDefault();
                                detalles[j].IdPrecioUnitario = nuevoRegistro.Id;
                                detalles[j].Codigo = insumo.Codigo;
                                detalles[j].Descripcion = insumo.Descripcion;
                                detalles[j].Unidad = insumo.Unidad;
                                detalles[j].IdTipoInsumo = insumo.idTipoInsumo;
                                detalles[j].IdFamiliaInsumo = insumo.idFamiliaInsumo;
                                detalles[j].CostoUnitario = insumo.CostoBase;
                                detalles[j].CostoBase = insumo.CostoBase;
                            }
                            await CrearDetalles(detalles, IdProyecto);
                            var insumosParaRecalculo = await _InsumoService.ObtenXIdProyecto(IdProyecto);
                            var detallesParaRecalculo = await _PrecioUnitarioDetalleService.ObtenerTodosXIdPrecioUnitario(nuevoRegistro.Id);
                            for (int j = 0; j < detallesParaRecalculo.Count; j++)
                            {
                                var insumo = insumosParaRecalculo.Where(z => z.id == detallesParaRecalculo[j].IdInsumo).FirstOrDefault();
                                detallesParaRecalculo[j].IdPrecioUnitario = nuevoRegistro.Id;
                                detallesParaRecalculo[j].Codigo = insumo.Codigo;
                                detallesParaRecalculo[j].Descripcion = insumo.Descripcion;
                                detallesParaRecalculo[j].Unidad = insumo.Unidad;
                                detallesParaRecalculo[j].IdTipoInsumo = insumo.idTipoInsumo;
                                detallesParaRecalculo[j].IdFamiliaInsumo = insumo.idFamiliaInsumo;
                                detallesParaRecalculo[j].CostoUnitario = insumo.CostoBase;
                                detallesParaRecalculo[j].CostoBase = insumo.CostoBase;
                            }
                            var datosObtenidos = await RecalcularDetalles(nuevoRegistro.Id, detallesParaRecalculo, insumosParaRecalculo);
                            var nuevoRegistroBuscado = await _PrecioUnitarioService.ObtenXId(nuevoRegistro.Id);
                            var concepto = await _ConceptoService.ObtenXId(nuevoRegistroBuscado.IdConcepto);
                            concepto.CostoUnitario = datosObtenidos.Total;
                            await _ConceptoService.Editar(concepto);
                            await RecalcularPrecioUnitario(nuevoRegistroBuscado);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < precios.Count; i++)
                {
                    precios[i].Nivel = 1;
                    precios[i].IdPrecioUnitarioBase = 0;
                    precios[i].IdProyecto = IdProyecto;
                    var Id = precios[i].Id;
                    precios[i].Id = 0;
                    var nuevoConcepto = new ConceptoDTO();
                    nuevoConcepto.IdProyecto = IdProyecto;
                    nuevoConcepto.Codigo = precios[i].Codigo;
                    nuevoConcepto.Descripcion = precios[i].Descripcion;
                    nuevoConcepto.Unidad = precios[i].Unidad;
                    nuevoConcepto.CostoUnitario = 0;
                    var conceptoCreado = await _ConceptoService.CrearYObtener(nuevoConcepto);
                    precios[i].IdConcepto = conceptoCreado.Id;
                    var nuevoRegistro = await _PrecioUnitarioService.CrearYObtener(precios[i]);
                    var Proyecto = await _ProyectoService.ObtenXId(IdProyecto);
                    ProgramacionEstimadaGanttDTO nuevaProgramacion = new ProgramacionEstimadaGanttDTO();
                    nuevaProgramacion.IdConcepto = nuevoRegistro.IdConcepto;
                    nuevaProgramacion.Start = Proyecto.FechaInicio;
                    nuevaProgramacion.End = Proyecto.FechaInicio;
                    nuevaProgramacion.IdProyecto = Proyecto.Id;
                    nuevaProgramacion.IdPrecioUnitario = nuevoRegistro.Id;
                    nuevaProgramacion.Duracion = 1;
                    nuevaProgramacion.Progress = 0;
                    if (nuevoRegistro.IdPrecioUnitarioBase != 0)
                    {
                        var programacionesEstimadas = await _ProgramacionEstimadaService.ObtenerTodosXProyecto(Proyecto.Id);
                        var programacionEstimadaPadre = programacionesEstimadas.Where(z => z.IdPrecioUnitario == nuevoRegistro.IdPrecioUnitarioBase).FirstOrDefault();
                        nuevaProgramacion.Parent = Convert.ToString(programacionEstimadaPadre.Id);
                    }
                    else
                    {
                        nuevaProgramacion.Parent = "0";
                    }
                    await _ProgramacionEstimadaGanttService.CrearYObtener(nuevaProgramacion);
                    if (precios[i].Hijos.Count > 0)
                    {
                        await CopiarPU(precios[i].Hijos, nuevoRegistro.Id, registros, registrosSinEstructurar, db);
                    }
                    if (precios[i].TipoPrecioUnitario == 1)
                    {
                        var detalles = await _PrecioUnitarioDetalleService.ObtenerTodosXIdPrecioUnitario(Id);
                    }

                }
            }
        }

        public async Task CopiarPUXCodigo(PrecioUnitarioDTO precio, DbContext db)
        {
            var Id = precio.Id;
            var precioUnitarioBase = await _PrecioUnitarioService.ObtenXId(precio.IdPrecioUnitarioBase);

            precio.Nivel = precioUnitarioBase.Nivel + 1;
            precio.Id = 0;
            var nuevoRegistro = await _PrecioUnitarioService.CrearYObtener(precio);
            var Proyecto = await _ProyectoService.ObtenXId(precio.IdProyecto);
            ProgramacionEstimadaGanttDTO nuevaProgramacion = new ProgramacionEstimadaGanttDTO();
            nuevaProgramacion.IdConcepto = nuevoRegistro.IdConcepto;
            nuevaProgramacion.Start = Proyecto.FechaInicio;
            nuevaProgramacion.End = Proyecto.FechaInicio;
            nuevaProgramacion.IdProyecto = Proyecto.Id;
            nuevaProgramacion.IdPrecioUnitario = nuevoRegistro.Id;
            nuevaProgramacion.Duracion = 1;
            nuevaProgramacion.Progress = 0;
            if (nuevoRegistro.IdPrecioUnitarioBase != 0)
            {
                var programacionesEstimadas = await _ProgramacionEstimadaGanttService.ObtenerXIdProyecto(Proyecto.Id, db);
                var programacionEstimadaPadre = programacionesEstimadas.Where(z => z.IdPrecioUnitario == nuevoRegistro.IdPrecioUnitarioBase).FirstOrDefault();
                nuevaProgramacion.Parent = programacionEstimadaPadre.Id;
            }
            else
            {
                nuevaProgramacion.Parent = "";
            }
            await _ProgramacionEstimadaGanttService.CrearYObtener(nuevaProgramacion);
            var items = db.Database.SqlQueryRaw<string>(""""select Id, IdPrecioUnitario, IdInsumo, EsCompuesto, Cantidad, CantidadExcedente, IdPrecioUnitarioDetallePerteneciente from PrecioUnitarioDetalle where IdPrecioUnitario = '"""" + Id + """"' for json path"""").ToList();
            if (items.Count > 0)
            {
                string json = string.Join("", items);
                var datos = JsonSerializer.Deserialize<List<PrecioUnitarioDetalle>>(json);
                var detalles = _Mapper.Map<List<PrecioUnitarioDetalleDTO>>(datos);
                var precioUnitarioAuxiliar = await _PrecioUnitarioService.ObtenXId(Id);
                var insumos = await _InsumoService.ObtenXIdProyecto(precioUnitarioAuxiliar.IdProyecto);
                for (int j = 0; j < detalles.Count; j++)
                {
                    var insumo = insumos.Where(z => z.id == detalles[j].IdInsumo).FirstOrDefault();
                    detalles[j].IdPrecioUnitario = nuevoRegistro.Id;
                    detalles[j].Codigo = insumo.Codigo;
                    detalles[j].Descripcion = insumo.Descripcion;
                    detalles[j].Unidad = insumo.Unidad;
                    detalles[j].IdTipoInsumo = insumo.idTipoInsumo;
                    detalles[j].IdFamiliaInsumo = insumo.idFamiliaInsumo;
                    detalles[j].CostoUnitario = insumo.CostoBase;
                    detalles[j].CostoBase = insumo.CostoBase;
                }
                await CrearDetalles(detalles, precio.IdProyecto);
                var insumosParaRecalculo = await _InsumoService.ObtenXIdProyecto(precio.IdProyecto);
                var detallesParaRecalculo = await _PrecioUnitarioDetalleService.ObtenerTodosXIdPrecioUnitario(nuevoRegistro.Id);
                for (int j = 0; j < detallesParaRecalculo.Count; j++)
                {
                    var insumo = insumosParaRecalculo.Where(z => z.id == detallesParaRecalculo[j].IdInsumo).FirstOrDefault();
                    detallesParaRecalculo[j].IdPrecioUnitario = nuevoRegistro.Id;
                    detallesParaRecalculo[j].Codigo = insumo.Codigo;
                    detallesParaRecalculo[j].Descripcion = insumo.Descripcion;
                    detallesParaRecalculo[j].Unidad = insumo.Unidad;
                    detallesParaRecalculo[j].IdTipoInsumo = insumo.idTipoInsumo;
                    detallesParaRecalculo[j].IdFamiliaInsumo = insumo.idFamiliaInsumo;
                    detallesParaRecalculo[j].CostoUnitario = insumo.CostoBase;
                    detallesParaRecalculo[j].CostoBase = insumo.CostoBase;
                }
                var datosObtenidos = await RecalcularDetalles(nuevoRegistro.Id, detallesParaRecalculo, insumosParaRecalculo);
                var nuevoRegistroBuscado = await _PrecioUnitarioService.ObtenXId(nuevoRegistro.Id);
                var concepto = await _ConceptoService.ObtenXId(nuevoRegistroBuscado.IdConcepto);
                concepto.CostoUnitario = datosObtenidos.Total;
                await _ConceptoService.Editar(concepto);
                await RecalcularPrecioUnitario(nuevoRegistroBuscado);
            }
        }

        public async Task<ActionResult<List<PrecioUnitarioDetalleDTO>>> CrearOperacion(OperacionesXPrecioUnitarioDetalleDTO registro)
        {
            DataTable table = new DataTable();
            object result = table.Compute(registro.Operacion, string.Empty);
            registro.Resultado = Convert.ToDecimal(result);
            var registroCreado = await _OperacionXPUService.CrearYObtener(registro);
            var obtenerOperaciones = await _OperacionXPUService.ObtenerXIdPrecioUnitarioDetalle(registro.IdPrecioUnitarioDetalle);
            decimal total = 0;
            foreach (var operacion in obtenerOperaciones)
            {
                total = total + operacion.Resultado;
            }

            var detalle = await _PrecioUnitarioDetalleService.ObtenerXId(registro.IdPrecioUnitarioDetalle);
            var detalles = await ObtenerDetalles(detalle.IdPrecioUnitario, _dbContex);
            detalle = detalles.Where(z => z.Id == detalle.Id).FirstOrDefault();
            detalle.Cantidad = total;
            var resultado = await EditarDetalle(detalle);
            return resultado;
        }

        public async Task<ActionResult<List<PrecioUnitarioDetalleDTO>>> EditarOperacion(OperacionesXPrecioUnitarioDetalleDTO registro)
        {
            DataTable table = new DataTable();
            object result = table.Compute(registro.Operacion, string.Empty);
            registro.Resultado = Convert.ToDecimal(result);
            var registroEditado = await _OperacionXPUService.Editar(registro);
            var obtenerOperaciones = await _OperacionXPUService.ObtenerXIdPrecioUnitarioDetalle(registro.IdPrecioUnitarioDetalle);
            decimal total = 0;
            foreach (var operacion in obtenerOperaciones)
            {
                total = total + operacion.Resultado;
            }

            var detalle = await _PrecioUnitarioDetalleService.ObtenerXId(registro.IdPrecioUnitarioDetalle);
            var detalles = await ObtenerDetalles(detalle.IdPrecioUnitario, _dbContex);
            detalle = detalles.Where(z => z.Id == detalle.Id).FirstOrDefault();
            detalle.Cantidad = total;
            var resultado = await EditarDetalle(detalle);
            return resultado;
        }

        public async Task<ActionResult<List<PrecioUnitarioDetalleDTO>>> EliminarOperacion(int Id)
        {
            var operacionEliminar = await _OperacionXPUService.ObtenerXId(Id);
            var registroEditado = await _OperacionXPUService.Eliminar(Id);
            var obtenerOperaciones = await _OperacionXPUService.ObtenerXIdPrecioUnitarioDetalle(operacionEliminar.IdPrecioUnitarioDetalle);
            decimal total = 0;
            foreach (var operacion in obtenerOperaciones)
            {
                total = total + operacion.Resultado;
            }

            var detalle = await _PrecioUnitarioDetalleService.ObtenerXId(operacionEliminar.IdPrecioUnitarioDetalle);
            var detalles = await ObtenerDetalles(detalle.IdPrecioUnitario, _dbContex);
            detalle = detalles.Where(z => z.Id == detalle.Id).FirstOrDefault();
            detalle.Cantidad = total;
            var resultado = await EditarDetalle(detalle);
            return resultado;
        }

        public async Task<ActionResult<List<OperacionesXPrecioUnitarioDetalleDTO>>> ObtenerOperaciones(int IdPrecioUnitarioDetalle)
        {
            return await _OperacionXPUService.ObtenerXIdPrecioUnitarioDetalle(IdPrecioUnitarioDetalle);
        }

        public async Task<ActionResult<List<InsumoParaExplosionDTO>>> EditarInsumoDesdeExplosion(InsumoParaExplosionDTO registro)
        {
            var insumoBase = await _InsumoService.ObtenXId(registro.id);
            if (registro.idTipoInsumo == 10001) {
                return await obtenerExplosion(insumoBase.IdProyecto);
            }
            var insumos = await _InsumoService.ObtenXIdProyecto(insumoBase.IdProyecto);
            var insumosFiltrados = insumos.Where(z => z.Codigo == registro.Codigo).ToList();
            var fsr = await _FsrxinsummoMdOService.ObtenerXIdInsumo(registro.id);
            if (fsr.Id > 0)
            {
                foreach (var insumo in insumosFiltrados)
                {
                    fsr.CostoDirecto = registro.CostoBase;
                    fsr.CostoFinal = registro.CostoBase * fsr.Fsr;
                    var editado = await _FsrxinsummoMdOService.Editar(fsr);
                    if (editado == true)
                    {
                        insumo.CostoBase = registro.CostoBase;
                        insumo.CostoUnitario = fsr.CostoFinal;
                        var insumoEditado = await _InsumoService.Editar(insumo);
                    }
                }
            }
            else
            {
                FactorSalarioRealDTO FSR = new FactorSalarioRealDTO();
                var ExisteFSR = await _FSRService.ObtenerTodosXProyecto(insumoBase.IdProyecto);
                foreach (var insumo in insumosFiltrados)
                {
                    if (ExisteFSR.Count > 0)
                    {
                        FSR = ExisteFSR.FirstOrDefault();
                        if (insumo.idTipoInsumo == 10000)
                        {
                            if (!FSR.EsCompuesto) {
                                insumo.CostoBase = registro.CostoBase;
                                insumo.CostoUnitario = registro.CostoBase * FSR.PorcentajeFsr;
                            }
                            else
                            {
                                insumo.CostoBase = registro.CostoBase;
                                await _factorSalarioRealProceso.actualizarCostoUnitarioInsumoMO(insumo, FSR);
                            }
                        }
                        else
                        {
                            insumo.CostoBase = registro.CostoBase;
                            insumo.CostoUnitario = registro.CostoBase;
                            var insumoEditado = await _InsumoService.Editar(insumo);
                        }
                    }
                    else
                    {
                        insumo.CostoBase = registro.CostoBase;
                        insumo.CostoUnitario = registro.CostoBase;
                        var insumoEditado = await _InsumoService.Editar(insumo);
                    }
                }
            }
            return await obtenerExplosion(insumoBase.IdProyecto);
        }

        public async Task GuardarArchivo(IFormFile archivo, string rutaDestino)
        {
            using (var stream = new FileStream(rutaDestino, FileMode.Create))
            {
                await archivo.CopyToAsync(stream);
            }
        }

        public async Task<List<List<ObjetoOpusDTO>>> extraerDatosDBF(IFormFile archivoDBF, IFormFile archivoFPT)
        {

            string ruta = @"C:\TempImportacionOpus";
            Directory.CreateDirectory(ruta);

            string rutaArchivo = Path.Combine(ruta, archivoDBF.FileName);
            string rutaArchivoFPT = Path.Combine(ruta, archivoFPT.FileName);

            await GuardarArchivo(archivoDBF, rutaArchivo);
            await GuardarArchivo(archivoFPT, rutaArchivoFPT);

            var opciones = new DbfDataReaderOptions
            {
                SkipDeletedRecords = true,
                Encoding = Encoding.GetEncoding(1252)
            };


            var registros = new List<List<ObjetoOpusDTO>>();
            using (var reader = new DbfDataReader.DbfDataReader(rutaArchivo, opciones))
            {
                while (reader.Read())
                {
                    if (reader.DbfRecord.IsDeleted == false)
                    {
                        var nuevoObjeto = new List<ObjetoOpusDTO>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            var objeto = new ObjetoOpusDTO();
                            string nombreCampo = reader.GetName(i);
                            object valor = reader.GetValue(i);
                            objeto.TipoCampo = nombreCampo;
                            objeto.Valor = Convert.ToString(valor);
                            nuevoObjeto.Add(objeto);
                        }
                        registros.Add(nuevoObjeto);
                    }
                }
            }
            File.Delete(rutaArchivo);
            File.Delete(rutaArchivoFPT);
            Directory.Delete(ruta);
            return registros;
        }

        public static string RecuperarTextoOriginal(string raw)
        {
            if (string.IsNullOrEmpty(raw))
                return raw;

            // 1) Obtiene los bytes como si estuvieran en UTF-8
            byte[] utf8Bytes = Encoding.UTF8.GetBytes(raw);

            // 2) Decodifica esos bytes usando Windows-1252
            string decoded = Latin1.GetString(utf8Bytes);

            return decoded;
        }

        public async Task ImportarInsumos(IFormFile archivoDBF, IFormFile archivoFPT, int IdProyecto) //Enviar P.DBF
        {
            var registros = await extraerDatosDBF(archivoDBF, archivoFPT);
            for (int i = 0; i < registros.Count(); i++)
            {
                var registro = registros[i].Where(z => z.TipoCampo == "PREFIJO").FirstOrDefault();
                if(registro.Valor == "32")
                {
                    //CrearConcepto
                    var Concepto = new ConceptoDTO();
                    Concepto.Codigo = registros[i].Where(z => z.TipoCampo == "NOMBRE").FirstOrDefault().Valor;
                    //string descipcion = RecuperarTextoOriginal(registros[i].Where(z => z.TipoCampo == "DESCRIPCIO").FirstOrDefault().Valor);

                    Concepto.Descripcion = registros[i].Where(z => z.TipoCampo == "DESCRIPCIO").FirstOrDefault().Valor;
                    Concepto.Unidad = registros[i].Where(z => z.TipoCampo == "UNIDAD").FirstOrDefault().Valor;
                    Concepto.IdEspecialidad = null;
                    Concepto.CostoUnitario = Convert.ToDecimal(registros[i].Where(z => z.TipoCampo == "PRECIO").FirstOrDefault().Valor);
                    Concepto.IdProyecto = IdProyecto;
                    Concepto.PorcentajeIndirecto = 0;
                    var ConceptoCreado = await _ConceptoService.CrearYObtener(Concepto);
                }
                    //CrearInsumo
                    var insumo = new InsumoCreacionDTO();
                    switch (registro.Valor)
                    {
                        case "1":
                            //Material
                            insumo.idTipoInsumo = 10004;
                            break;

                        case "2":
                            //Mano de obra
                            insumo.idTipoInsumo = 10000;
                            break;

                        case "4":
                            //%(mo)
                            insumo.idTipoInsumo = 10001;
                            break;

                        case "8":
                            //Herramienta
                            insumo.idTipoInsumo = 10002;
                            break;
                        case "16":
                            //Auxiliar
                            insumo.idTipoInsumo = 10005;
                            break;

                        default:
                            break;
                    }
                    if(registros[i].Where(z => z.TipoCampo == "BASICO").FirstOrDefault().Valor != "S")
                    {
                        insumo.idTipoInsumo = 10006;
                    }
                    insumo.Codigo = registros[i].Where(z => z.TipoCampo == "NOMBRE").FirstOrDefault().Valor;
                    //string descipcionInsumo = RecuperarTextoOriginal(registros[i].Where(z => z.TipoCampo == "DESCRIPCIO").FirstOrDefault().Valor);
                    insumo.Descripcion = registros[i].Where(z => z.TipoCampo == "DESCRIPCIO").FirstOrDefault().Valor;
                    insumo.Unidad = registros[i].Where(z => z.TipoCampo == "UNIDAD").FirstOrDefault().Valor;
                    insumo.idFamiliaInsumo = null;
                    insumo.CostoBase = Convert.ToDecimal(registros[i].Where(z => z.TipoCampo == "PBASEMN").FirstOrDefault().Valor);
                    insumo.CostoUnitario = insumo.CostoBase;
                    insumo.IdProyecto = IdProyecto;
                    var insumoCreado = await _InsumoService.CrearYObtener(insumo);
                    if(insumoCreado.idTipoInsumo == 10000 & registros[i].Where(z => z.TipoCampo == "BASICO").FirstOrDefault().Valor == "S")
                    {
                        var nuevoInsumoMDO = new FsrxinsummoMdODTO();
                        nuevoInsumoMDO.CostoDirecto = insumo.CostoBase;
                        nuevoInsumoMDO.CostoFinal = Convert.ToDecimal(registros[i].Where(z => z.TipoCampo == "PRECIO").FirstOrDefault().Valor);
                        nuevoInsumoMDO.Fsr = Convert.ToDecimal(registros[i].Where(z => z.TipoCampo == "FSR").FirstOrDefault().Valor);
                        nuevoInsumoMDO.IdInsumo = insumoCreado.id;
                        nuevoInsumoMDO.IdProyecto = IdProyecto;
                        var FSRCreado = await _FsrxinsummoMdOService.CrearYObtener(nuevoInsumoMDO);
                        insumoCreado.CostoUnitario = FSRCreado.CostoFinal;
                        insumoCreado.EsFsrGlobal = true;
                        await _InsumoService.Editar(insumoCreado);
                    }
            }
        }

        public async Task ImportarConceptos(IFormFile archivoDBF, IFormFile archivoFPT, int IdProyecto) //Enviar A.DBF
        {
            var registros = await extraerDatosDBF(archivoDBF, archivoFPT);
            foreach(var registro in registros)
            {
                var Concepto = new ConceptoDTO();
                Concepto.Codigo = registro.Where(z => z.TipoCampo == "NOMBRE").FirstOrDefault().Valor;
                Concepto.Descripcion = registro.Where(z => z.TipoCampo == "DESC").FirstOrDefault().Valor;
                Concepto.Unidad = "";
                Concepto.IdEspecialidad = null;
                Concepto.CostoUnitario = Convert.ToDecimal(registro.Where(z => z.TipoCampo == "PRECIO").FirstOrDefault().Valor);
                Concepto.IdProyecto = IdProyecto;
                Concepto.PorcentajeIndirecto = 0;
                var ConceptoCreado = await _ConceptoService.CrearYObtener(Concepto);
            }
        }

        public async Task ArmarCatalogoConceptos(IFormFile archivoDBF, IFormFile archivoFPT, int IdProyecto) //Enviar 1.DBF
        {
            var registros = await extraerDatosDBF(archivoDBF, archivoFPT);
            var registrosOrdenados = registros.OrderBy(sublista => sublista.FirstOrDefault(obj => obj.TipoCampo == "PRE_ID").Valor).ToList();
            var conceptos = await _ConceptoService.ObtenerTodos(IdProyecto);
            var Pus = new List<PUOpusRelacionDTO>();
            foreach (var registro in registrosOrdenados)
            {
                if(registro.Where(z => z.TipoCampo == "PRE_NIVEL").FirstOrDefault().Valor != "0")
                {
                    var PrecioUnitario = new PrecioUnitarioDTO();
                    PrecioUnitario.IdProyecto = IdProyecto;
                    PrecioUnitario.Cantidad = Convert.ToDecimal(registro.Where(z => z.TipoCampo == "PRE_VOL").FirstOrDefault().Valor);
                    PrecioUnitario.CantidadExcedente = 0;
                    if (registro.Where(z => z.TipoCampo == "PRE_TIP").FirstOrDefault().Valor == "0")
                    {
                        PrecioUnitario.TipoPrecioUnitario = 1;
                    }
                    var nivelDecimal = Convert.ToDecimal(registro.Where(z => z.TipoCampo == "PRE_NIVEL").FirstOrDefault().Valor);
                    PrecioUnitario.Nivel = Convert.ToInt32(nivelDecimal);
                    var concepto = conceptos.Where(z => z.Codigo == registro.Where(z => z.TipoCampo == "PRE_COM").FirstOrDefault().Valor).FirstOrDefault();
                    PrecioUnitario.IdConcepto = concepto.Id;
                    if(registro.Where(z => z.TipoCampo == "PRE_IDPAD").FirstOrDefault().Valor != "0")
                    {
                        //estructurar PrecioUnitario
                        var relacionEncontrada = Pus.Where(z => z.IdPrecioUnitarioOpus == registro.Where(z => z.TipoCampo == "PRE_IDPAD").FirstOrDefault().Valor).FirstOrDefault();
                        PrecioUnitario.IdPrecioUnitarioBase = relacionEncontrada.IdPrecioUnitarioTeckio;
                    }
                    var nuevoPU = await _PrecioUnitarioService.CrearYObtener(PrecioUnitario);
                    var relacion = new PUOpusRelacionDTO();
                    relacion.IdPrecioUnitarioTeckio = nuevoPU.Id;
                    relacion.IdPrecioUnitarioOpus = registro.Where(z => z.TipoCampo == "PRE_IDUNI").FirstOrDefault().Valor;
                    Pus.Add(relacion);
                }
            }
        }

        public async Task ArmarPreciosUnitarios(IFormFile archivoDBF, IFormFile archivoFPT, int IdProyecto) //enviar F.DBF
        {
            var registros = await extraerDatosDBF(archivoDBF, archivoFPT);
            var conceptos = await _ConceptoService.ObtenerTodos(IdProyecto);
            var Insumos = await _InsumoService.ObtenerInsumoXProyecto(IdProyecto);
            var PreciosUnitariosSinEstructurar = await ObtenerPrecioUnitarioSinEstructurar(IdProyecto);
            var registrosFiltrados = registros.Where(sublista => sublista.Any(z => z.TipoCampo == "PREF" && z.Valor == "32")).ToList();
            foreach(var registro in registrosFiltrados)
            {
                var concepto = conceptos.Where(z => z.Codigo == registro.Where(z => z.TipoCampo == "NOMBRE").FirstOrDefault().Valor).FirstOrDefault();
                var ExisteEnCatalogo = PreciosUnitariosSinEstructurar.Where(z => z.IdConcepto == concepto.Id).ToList();
                if(ExisteEnCatalogo.Count > 0)
                {
                    foreach(var regCatalogo in ExisteEnCatalogo)
                    {
                        var insumo = Insumos.Where(z => z.Codigo == registro.Where(z => z.TipoCampo == "COMPONENTE").FirstOrDefault().Valor).FirstOrDefault();
                        if (insumo != null)
                        {
                            var precioUnitarioDetalle = new PrecioUnitarioDetalleDTO();
                            precioUnitarioDetalle.IdPrecioUnitario = regCatalogo.Id;
                            precioUnitarioDetalle.IdPrecioUnitarioDetallePerteneciente = 0;
                            precioUnitarioDetalle.IdInsumo = insumo.id;
                            precioUnitarioDetalle.Cantidad = Convert.ToDecimal(registro.Where(z => z.TipoCampo == "CANTIDAD").FirstOrDefault().Valor);
                            var RegistrosExistentesCompuesto = registros.Where(sublista => sublista.Any(z => z.TipoCampo == "NOMBRE" && z.Valor == insumo.Codigo)).ToList();
                            if (RegistrosExistentesCompuesto.Count > 0)
                            {
                                precioUnitarioDetalle.EsCompuesto = true;
                                var precioUnitarioCreado = await _PrecioUnitarioDetalleService.CrearYObtener(precioUnitarioDetalle);
                                //Hacer metodo recursivo para armados (pasar parametros el precio creado y los registros guardados)
                                await ArmarPreciosUnitariosDetalles(registros, IdProyecto, precioUnitarioCreado, Insumos);
                            }
                            else
                            {
                                var precioUnitarioCreado = await _PrecioUnitarioDetalleService.CrearYObtener(precioUnitarioDetalle);
                            }
                        }
                    }
                }
            }
            await CrearProgramacionEstimadaGantt(IdProyecto);

        }

        public async Task ArmarPreciosUnitariosDetalles(List<List<ObjetoOpusDTO>> registros, int IdProyecto, PrecioUnitarioDetalleDTO Padre, List<InsumoDTO> insumos) //Callback
        {
            var insumo = insumos.Where(z => z.id == Padre.IdInsumo).FirstOrDefault();
            var hijos = registros.Where(sublista => sublista.Any(z => z.TipoCampo == "NOMBRE" && z.Valor == insumo.Codigo)).ToList();
            foreach(var hijo in hijos)
            {
                var insumoHijo = insumos.Where(z => z.Codigo == hijo.Where(z => z.TipoCampo == "COMPONENTE").FirstOrDefault().Valor).FirstOrDefault();
                if(insumoHijo != null)
                {
                    var precioUnitarioDetalle = new PrecioUnitarioDetalleDTO();
                    precioUnitarioDetalle.IdPrecioUnitario = Padre.IdPrecioUnitario;
                    precioUnitarioDetalle.IdPrecioUnitarioDetallePerteneciente = Padre.Id;
                    precioUnitarioDetalle.IdInsumo = insumoHijo.id;
                    precioUnitarioDetalle.Cantidad = Convert.ToDecimal(hijo.Where(z => z.TipoCampo == "CANTIDAD").FirstOrDefault().Valor);
                    var RegistrosExistentesCompuesto = registros.Where(sublista => sublista.Any(z => z.TipoCampo == "NOMBRE" && z.Valor == insumoHijo.Codigo)).ToList();
                    if (RegistrosExistentesCompuesto.Count > 0)
                    {
                        precioUnitarioDetalle.EsCompuesto = true;
                        var precioUnitarioCreado = await _PrecioUnitarioDetalleService.CrearYObtener(precioUnitarioDetalle);
                        //Hacer metodo recursivo para armados (pasar parametros el precio creado y los registros guardados)
                        await ArmarPreciosUnitariosDetalles(registros, IdProyecto, precioUnitarioCreado, insumos);
                    }
                    else
                    {
                        var precioUnitarioCreado = await _PrecioUnitarioDetalleService.CrearYObtener(precioUnitarioDetalle);
                    }
                }
            }

        }

        public async Task CrearProgramacionEstimadaGantt(int IdProyecto)
        {
            var catalogoConeptos = await ObtenerPrecioUnitarioSinEstructurar(IdProyecto);

            var programacionesGantt = new List<ProgramacionEstimadaGanttDTO>();
            foreach(var concepo in catalogoConeptos)
            {
                var nuevaProgramacion = new ProgramacionEstimadaGanttDTO();
                nuevaProgramacion.IdProyecto = IdProyecto;
                nuevaProgramacion.IdPrecioUnitario = concepo.Id;
                nuevaProgramacion.IdConcepto = concepo.IdConcepto;
                nuevaProgramacion.Start = DateTime.Now;
                nuevaProgramacion.End = nuevaProgramacion.Start.AddDays(1);
                nuevaProgramacion.Duracion = 1;
                if(concepo.IdPrecioUnitarioBase != 0)
                {
                    var padre = programacionesGantt.Where(z => z.IdPrecioUnitario == concepo.IdPrecioUnitarioBase).FirstOrDefault();
                    nuevaProgramacion.Parent = Convert.ToString(padre.Id);
                }
                var registroCreacion = await _ProgramacionEstimadaGanttService.CrearYObtener(nuevaProgramacion);
                programacionesGantt.Add(registroCreacion);
            }
        }
    }
}
