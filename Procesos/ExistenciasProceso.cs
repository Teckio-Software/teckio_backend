using Microsoft.EntityFrameworkCore;


using Microsoft.AspNetCore.Mvc;using ERP_TECKIO;

namespace ERP_TECKIO
{
    public class ExistenciasProceso<T> where T : DbContext
    {
        private readonly IInsumoService<T> _insumoService;
        private readonly IAlmacenExistenciaInsumoService<T> _almacenExistenciaInsumoService;
        private readonly IAlmacenService<T> _almacenService;
        public ExistenciasProceso(
            IInsumoService<T> insumoService,
            IAlmacenExistenciaInsumoService<T> almacenExistenciaInsumoService,
            IAlmacenService<T> almacenService
            ) { 
            _insumoService = insumoService;
            _almacenExistenciaInsumoService = almacenExistenciaInsumoService;
            _almacenService = almacenService;
        }

        public async Task<List<AlmacenExistenciaInsumoDTO>> obtenInsumosExistentes(int IdAlmacen) {
            List<AlmacenExistenciaInsumoDTO> lista = new List<AlmacenExistenciaInsumoDTO>();
            var insumosExistencia = await _almacenExistenciaInsumoService.ObtenXIdAlmacen(IdAlmacen);
            if (insumosExistencia.Count <= 0) {
                return lista;
            }
            var almacen = await _almacenService.ObtenXId(IdAlmacen);
            var agrupadosXidInsumo = insumosExistencia.GroupBy(z => z.IdInsumo);
            var insumos = await _insumoService.ObtenXIdProyecto(Convert.ToInt32(almacen.IdProyecto));
            foreach (var i in agrupadosXidInsumo)
            {
                decimal CantidaRetira = i.Sum(z => z.CantidadInsumosRetira);
                decimal CantidaAumenta = i.Sum(z => z.CantidadInsumosAumenta);
                decimal CantidadDisponibles = 0;
                if (CantidaRetira == 0 || CantidaRetira == null) {
                    CantidadDisponibles = CantidaAumenta;
                }
                else
                {
                    CantidadDisponibles = CantidaAumenta - CantidaRetira;
                }
                var insumo = insumos.Where(z => z.id == i.Key).ToList();
                if (insumo.Count() > 0) {
                    lista.Add(new AlmacenExistenciaInsumoDTO()
                    {
                        IdInsumo = i.Key,
                        Codigo = insumo[0].Codigo,
                        Descripcion = insumo[0].Descripcion,
                        Unidad = insumo[0].Unidad,
                        CantidadInsumos = CantidadDisponibles
                    });
                }
            }
            return lista;
        }

        public async Task<List<AlmacenExistenciaInsumoDTO>> obtenDetallesInsumosExistentes(int IdAlmacen, int IdInsumo) 
        {
            List<AlmacenExistenciaInsumoDTO> lista = new List<AlmacenExistenciaInsumoDTO>();
            var insumosExistencia = await _almacenExistenciaInsumoService.ObtenXIdAlmacen(IdAlmacen);
            var insumosExistenciaXIdInsumo = insumosExistencia.Where(z => z.IdInsumo == IdInsumo);
            var almacen = await _almacenService.ObtenXId(IdAlmacen);
            var insumos = await _insumoService.ObtenXIdProyecto(Convert.ToInt32(almacen.IdProyecto));
            foreach (var i in insumosExistenciaXIdInsumo)
            {
                var insumo = insumos.Where(z => z.id == i.IdInsumo).ToList();
                string TipoExistencia = "";
                decimal CantidadInsumos = 0;
                if (i.CantidadInsumosAumenta == 0 && i.CantidadInsumosRetira != 0) {
                    TipoExistencia = "Salida";
                    CantidadInsumos = i.CantidadInsumosRetira;
                }
                else
                {
                    TipoExistencia = "Entrada";
                    CantidadInsumos = i.CantidadInsumosAumenta;
                }
                lista.Add(new AlmacenExistenciaInsumoDTO()
                {
                    Id = i.Id,
                    TipoExistencia = TipoExistencia,
                    FechaRegistro = i.FechaRegistro,
                    EsNoConsumible = i.EsNoConsumible,
                    CantidadInsumos = CantidadInsumos
                });
            }
            return lista;
        }

        public async Task<RespuestaDTO> existenciaYAlmacenDeInsumo(int IdInsumo, int IdProyecto)
        {
            RespuestaDTO respuesta =  new RespuestaDTO();
            var insumosExistencia = await _almacenExistenciaInsumoService.ObtenXIdProyecto(IdProyecto);
            if (insumosExistencia.Count() <= 0) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No hay insumos en almacén";
                return respuesta;
            }
            var insumosExistenciaXIdInsumo = insumosExistencia.Where(z => z.IdInsumo == IdInsumo);
            if (insumosExistenciaXIdInsumo.Count() <= 0) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Este insumo no está en almacén";
                return respuesta;
            }
            var insumosExistentesAgrupado = insumosExistenciaXIdInsumo.GroupBy(z => z.IdAlmacen);
            decimal InsumosDisponibles = 0;
            decimal InsumosAumenta = 0;
            decimal InsumosRetira = 0;
            string almacenes = "";
            foreach (var IExistencia in insumosExistentesAgrupado) {
                InsumosAumenta += IExistencia.Sum(z => z.CantidadInsumosAumenta);
                InsumosRetira += IExistencia.Sum(z => z.CantidadInsumosRetira);
                var almacen = await _almacenService.ObtenXId(IExistencia.Key);
                almacenes += almacen.AlmacenNombre + ", ";
            }

            InsumosDisponibles = InsumosAumenta - InsumosRetira;

            if (InsumosDisponibles <= 0 || insumosExistentesAgrupado.Count() <= 0) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Este insumo no está disponible en almacén";
                return respuesta;
            }

            if (insumosExistentesAgrupado.Count() > 0) {
                respuesta.Estatus = true;
                respuesta.Descripcion = "El insumo está disponoble en "+almacenes+" existencia de "+InsumosDisponibles.ToString();
                return respuesta;
            }

            return respuesta;   
        }
    }
}
