using AutoMapper;
using ERP_TECKIO.Modelos;
using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;

namespace ERP_TECKIO
{
    public class AlmacenExistenciaInsumoService<T> : IAlmacenExistenciaInsumoService<T> where T : DbContext
    {
        private readonly IGenericRepository<InsumoExistencia, T> _Repositorio;
        //private readonly PROCOMIContext _Context;
        private readonly IMapper _Mapper;

        public AlmacenExistenciaInsumoService(
            IGenericRepository<InsumoExistencia, T> repositorio
            //, PROCOMIContext context
            , IMapper mapper)
        {
            _Repositorio = repositorio;
            //_Context = context;
            _Mapper = mapper;
        }
        //public async Task<RespuestaDTO> CreaExistenciaInsumoEntrada(AlmacenExistenciaInsumoCreacionDTO parametro)
        //{
        //    RespuestaDTO respuesta = new RespuestaDTO();
        //    try
        //    {
        //        if (parametro.IdInsumo <= 0 || parametro.IdProyecto <= 0
        //            || parametro.IdAlmacen <= 0 || parametro.CantidadExistente <= 0)
        //        {
        //            respuesta.Estatus = false;
        //            respuesta.Descripcion = "Capture los campos necesarios";
        //            return respuesta;
        //        }
        //        var insumoEncontradoEnAlmacen = await ObtenInsumoXIdAlmacenEIdInsumo(parametro.IdInsumo, parametro.IdAlmacen);
        //        if (insumoEncontradoEnAlmacen.Id > 0)
        //        {
        //            respuesta = await ActualizaExistenciaInsumoEntrada(insumoEncontradoEnAlmacen.Id, parametro.CantidadExistente);
        //            return respuesta;
        //        }
        //        var insumoExistencia = _Mapper.Map<InsumoExistencia>(parametro);
        //        insumoExistencia.CantidadInsumosAumenta = 0;
        //        insumoExistencia.CantidadInsumosRetira = 0;
        //        var objetoCreado = await _Repositorio.Crear(insumoExistencia);
        //        if (objetoCreado.Id <= 0)
        //        {
        //            respuesta.Estatus = false;
        //            respuesta.Descripcion = "No se pudó crear";
        //            return respuesta;
        //        }
        //        respuesta.Estatus = true;
        //        respuesta.Descripcion = "Insumo registrado en almacén";
        //        return respuesta;
        //    }
        //    catch (Exception ex)
        //    {
        //        respuesta.Estatus = false;
        //        respuesta.Descripcion = "Algo salió mal al registrar";
        //        return respuesta;
        //    }
        //}

        //public async Task<AlmacenExistenciaInsumoDTO> CreaExistenciaInsumoEntradaYRegresa(AlmacenExistenciaInsumoCreacionDTO parametro)
        //{
        //    RespuestaDTO respuesta = new RespuestaDTO();
        //    try
        //    {
        //        if (parametro.IdInsumo <= 0 || parametro.IdProyecto <= 0
        //            || parametro.IdAlmacen <= 0 || parametro.CantidadExistente <= 0)
        //        {
        //            throw new TaskCanceledException("Capture todos los campos");
        //        }
        //        var insumoEncontradoEnAlmacen = await ObtenInsumoXIdAlmacenEIdInsumo(parametro.IdInsumo, parametro.IdAlmacen);
        //        if (insumoEncontradoEnAlmacen.Id > 0)
        //        {
        //            respuesta = await ActualizaExistenciaInsumoEntrada(insumoEncontradoEnAlmacen.Id, parametro.CantidadExistente);
        //            if (respuesta.Estatus)
        //            {
        //                return insumoEncontradoEnAlmacen;
        //            }
        //            else
        //            {
        //                return new AlmacenExistenciaInsumoDTO();
        //            }
        //        }
        //        var insumoExistencia = _Mapper.Map<InsumoExistencia>(parametro);
        //        insumoExistencia.CantidadInsumosAumenta = 0;
        //        insumoExistencia.CantidadInsumosRetira = 0;
        //        var objetoCreado = await _Repositorio.Crear(insumoExistencia);
        //        if (objetoCreado.Id <= 0)
        //        {
        //            return new AlmacenExistenciaInsumoDTO();
        //        }
        //        return _Mapper.Map<AlmacenExistenciaInsumoDTO>(objetoCreado);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new AlmacenExistenciaInsumoDTO();
        //    }
        //}
        /// <summary>
        /// Actualiza
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Cantidad">Cantidad que se suma a la existente y al acumulado del almacén</param>
        /// <returns></returns>
        //public async Task<RespuestaDTO> ActualizaExistenciaInsumoEntrada(int Id, decimal Cantidad)
        //{
        //    RespuestaDTO respuesta = new RespuestaDTO();
        //    try
        //    {
        //        var almacenExistenciaInsumoDTO = await ObtenXId(Id);
        //        if (almacenExistenciaInsumoDTO.Id <= 0)
        //        {
        //            respuesta.Estatus = false;
        //            respuesta.Descripcion = "No existe el insumo";
        //            return respuesta;
        //        }
        //        almacenExistenciaInsumoDTO.CantidadExistente = almacenExistenciaInsumoDTO.CantidadExistente + Cantidad;
        //        almacenExistenciaInsumoDTO.CantidadInsumosAumenta = almacenExistenciaInsumoDTO.CantidadInsumosAumenta + Cantidad;
        //        respuesta.Estatus = await _Repositorio.Editar(_Mapper.Map<InsumoExistencia>(almacenExistenciaInsumoDTO));
        //        if (!respuesta.Estatus)
        //        {
        //            respuesta.Estatus = false;
        //            respuesta.Descripcion = "No se pudó editar";
        //            return respuesta;
        //        }
        //        respuesta.Estatus = true;
        //        respuesta.Descripcion = "Entrada del insumo editado";
        //        return respuesta;
        //    }
        //    catch (Exception ex)
        //    {
        //        respuesta.Estatus = false;
        //        respuesta.Descripcion = "Algo salió mal al registrar la entrada del insumo al almacén";
        //        return respuesta;
        //    }
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Cantidad">Cantidad que se resta a la existente y se suma al acumulado de salidas</param>
        /// <returns></returns>
        //public async Task<RespuestaDTO> ActualizaExistenciaInsumoSalida(int Id, decimal Cantidad)
        //{
        //    RespuestaDTO respuesta = new RespuestaDTO();
        //    try
        //    {
        //        var almacenExistenciaInsumoDTO = await ObtenXId(Id);
        //        if (almacenExistenciaInsumoDTO.Id <= 0 || almacenExistenciaInsumoDTO.CantidadExistente <= 0)
        //        {
        //            respuesta.Estatus = false;
        //            respuesta.Descripcion = "No hay existencias de este insumo";
        //            return respuesta;
        //        }
        //        almacenExistenciaInsumoDTO.CantidadExistente = almacenExistenciaInsumoDTO.CantidadExistente + Cantidad;
        //        almacenExistenciaInsumoDTO.CantidadInsumosRetira = almacenExistenciaInsumoDTO.CantidadInsumosRetira + Cantidad;
        //        respuesta.Estatus = await _Repositorio.Editar(_Mapper.Map<InsumoExistencia>(almacenExistenciaInsumoDTO));
        //        if (!respuesta.Estatus)
        //        {
        //            respuesta.Estatus = false;
        //            respuesta.Descripcion = "No se pudó editar";
        //            return respuesta;
        //        }
        //        respuesta.Estatus = true;
        //        respuesta.Descripcion = "Entrada del insumo editado";
        //        return respuesta;
        //    }
        //    catch (Exception ex)
        //    {
        //        respuesta.Estatus = false;
        //        respuesta.Descripcion = "Algo salió mal al registrar la salida del insumo al almacén";
        //        return respuesta;
        //    }
        //}

        public async Task<List<AlmacenExistenciaInsumoDTO>> ObtenTodos()
        {
            try
            {
                var query = await _Repositorio.ObtenerTodos();
                return _Mapper.Map<List<AlmacenExistenciaInsumoDTO>>(query);
            }
            catch (Exception ex)
            {
                return new List<AlmacenExistenciaInsumoDTO>();
            }
        }

        public async Task<AlmacenExistenciaInsumoDTO> ObtenXId(int Id)
        {
            try
            {
                var query = await _Repositorio.Obtener(z => z.Id == Id);
                return _Mapper.Map<AlmacenExistenciaInsumoDTO>(query);
            }
            catch (Exception ex)
            {
                return new AlmacenExistenciaInsumoDTO();
            }
        }
        /// <summary>
        /// Puede ser usado para consultas internas o externas (controllers)
        /// </summary>
        /// <param name="IdProyecto"></param>
        /// <returns></returns>
        public async Task<List<AlmacenExistenciaInsumoDTO>> ObtenXIdProyecto(int IdProyecto)
        {
            try
            {
                var query = await _Repositorio.ObtenerTodos(z => z.IdProyecto == IdProyecto);
                return _Mapper.Map<List<AlmacenExistenciaInsumoDTO>>(query);
            }
            catch (Exception ex)
            {
                return new List<AlmacenExistenciaInsumoDTO>();
            }
        }
        /// <summary>
        /// Para consultar externamente con el DTO
        /// </summary>
        /// <param name="IdAlmacen"></param>
        /// <returns></returns>
        public async Task<List<AlmacenExistenciaInsumoDTO>> ObtenXIdAlmacen(int IdAlmacen)
        {
            try
            {
                var query = await _Repositorio.ObtenerTodos(z => z.IdAlmacen == IdAlmacen);
                return _Mapper.Map<List<AlmacenExistenciaInsumoDTO>>(query);
            }
            catch (Exception ex)
            {
                return new List<AlmacenExistenciaInsumoDTO>();
            }
        }
        /// <summary>
        /// Para consultas internas
        /// </summary>
        /// <param name="IdInsumo">Identificador del insumo</param>
        /// <param name="IdAlmacen">Identificador del almacén</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<AlmacenExistenciaInsumoDTO> ObtenInsumoXIdAlmacenEIdInsumo(int IdInsumo, int IdAlmacen)
        {
            var insumosEnAlmacen = await ObtenXIdAlmacen(IdAlmacen);
            var insumos = insumosEnAlmacen.AsQueryable().Where(z => z.IdInsumo == IdInsumo).ToList();
            if (insumos.Count <= 0)
            {
                return new AlmacenExistenciaInsumoDTO();
            }
            return insumos.FirstOrDefault()!;
        }

        public async Task<AlmacenExistenciaInsumoDTO> ObtenInsumoXIdProyectoEIdInsumo(int IdInsumo, int IdProyecto)
        {
            var insumosEnProyecto = await ObtenXIdProyecto(IdProyecto);
            var insumos = insumosEnProyecto.AsQueryable().Where(z => z.IdInsumo == IdInsumo).ToList();
            if (insumos.Count <= 0)
            {
                return new AlmacenExistenciaInsumoDTO();
            }
            return insumos.FirstOrDefault()!;
        }

        public async Task<bool> CrearLista(List<AlmacenExistenciaInsumoCreacionDTO> parametro)
        {
            var objetosMapaeados = _Mapper.Map<List<InsumoExistencia>>(parametro);
            return await _Repositorio.CrearMultiple(objetosMapaeados);
        }

        public async Task<RespuestaDTO> CreaExistenciaInsumoEntrada(AlmacenExistenciaInsumoCreacionDTO parametro)
        {
            var respuesta = new RespuestaDTO();

            var objeto = await _Repositorio.Crear(_Mapper.Map<InsumoExistencia>(parametro));
            if (objeto.Id <= 0) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se creo la existecia";
                return respuesta;
            }
            respuesta.Estatus = true;
            respuesta.Descripcion = "Se creo la existecia";
            return respuesta;
        }

        public Task<AlmacenExistenciaInsumoDTO> CreaExistenciaInsumoEntradaYRegresa(AlmacenExistenciaInsumoCreacionDTO parametro)
        {
            throw new NotImplementedException();
        }

        public Task<RespuestaDTO> ActualizaExistenciaInsumoEntrada(int Id, decimal Cantidad)
        {
            throw new NotImplementedException();
        }

        public Task<RespuestaDTO> ActualizaExistenciaInsumoSalida(int Id, decimal Cantidad)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Crear(AlmacenExistenciaInsumoCreacionDTO parametro)
        {
            throw new NotImplementedException();
        }
    }
}
