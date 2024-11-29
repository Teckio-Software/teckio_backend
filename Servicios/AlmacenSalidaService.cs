using AutoMapper;
using ERP_TECKIO.Modelos;
using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;

namespace ERP_TECKIO
{
    public class AlmacenSalidaService<T> : IAlmacenSalidaService<T> where T : DbContext
    {
        private readonly IGenericRepository<AlmacenSalida, T> _Repositorio;
        //private readonly PROCOMIContext _Context;
        private readonly IMapper _Mapper;

        public AlmacenSalidaService(IGenericRepository<AlmacenSalida, T> repositorio
            //, PROCOMIContext context
            , IMapper mapper)
        {
            _Repositorio = repositorio;
            //_Context = context;
            _Mapper = mapper;
        }
        //public async Task<RespuestaDTO> Crear(AlmacenSalidaCreacionDTO parametro)
        //{
        //    RespuestaDTO respuesta = new RespuestaDTO();
        //    try
        //    {
        //        if (parametro.IdAlmacen <= 0 || parametro.IdProyecto <= 0
        //            || string.IsNullOrEmpty(parametro.PersonaSurtio) || string.IsNullOrEmpty(parametro.PersonaRecibio))
        //        {
        //            respuesta.Estatus = false;
        //            respuesta.Descripcion = "No hay un almacén asociado";
        //            return respuesta;
        //        }
        //        parametro.Estatus = 1;
        //        parametro.FechaRegistro = DateTime.Now;
        //        var objetoCreado = await _Repositorio.Crear(_Mapper.Map<AlmacenSalida>(parametro));
        //        if (objetoCreado.Id <= 0)
        //        {
        //            respuesta.Estatus = false;
        //            respuesta.Descripcion = "No se pudó crear";
        //            return respuesta;
        //        }
        //        respuesta.Estatus = true;
        //        respuesta.Descripcion = "Salida de almacén creada";
        //        return respuesta;
        //    }
        //    catch (Exception ex)
        //    {
        //        respuesta.Estatus = false;
        //        respuesta.Descripcion = "Algo salió mal en la creación de la salida de almacén";
        //        return respuesta;
        //    }
        //}

        //public async Task<AlmacenSalidaDTO> CrearYObtener(AlmacenSalidaCreacionDTO parametro)
        //{
        //    try
        //    {
        //        if (parametro.IdAlmacen <= 0 || parametro.IdProyecto <= 0
        //            || string.IsNullOrEmpty(parametro.PersonaSurtio) || string.IsNullOrEmpty(parametro.PersonaRecibio))
        //        {
        //            return new AlmacenSalidaDTO();
        //        }
        //        parametro.Estatus = 1;
        //        parametro.FechaRegistro = DateTime.Now;
        //        var objetoCreado = await _Repositorio.Crear(_Mapper.Map<AlmacenSalida>(parametro));
        //        if (objetoCreado.Id <= 0)
        //        {
        //            throw new TaskCanceledException("No se pudo crear");
        //        }
        //        return _Mapper.Map<AlmacenSalidaDTO>(objetoCreado);
        //    }
        //    catch (Exception ex)
        //    {
        //        string msg = ex.Message.ToString();
        //        return new AlmacenSalidaDTO();
        //    }
        //}

        //public async Task<RespuestaDTO> Editar(AlmacenSalidaDTO parametro)
        //{
        //    RespuestaDTO respuesta = new RespuestaDTO();
        //    try
        //    {
        //        var objetoEncontrado = await ObtenXId(parametro.Id);
        //        if (objetoEncontrado == null || objetoEncontrado.Id <= 0)
        //        {
        //            respuesta.Estatus = false;
        //            respuesta.Descripcion = "La salida de almacén no existe";
        //            return respuesta;
        //        }
        //        if (objetoEncontrado.Estatus != 1)
        //        {
        //            respuesta.Estatus = false;
        //            respuesta.Descripcion = "Solo se puede editar si esta capturado";
        //            return respuesta;
        //        }
        //        objetoEncontrado.IdProyecto = parametro.IdProyecto;
        //        objetoEncontrado.IdAlmacen = parametro.IdAlmacen;
        //        objetoEncontrado.Observaciones = parametro.Observaciones;
        //        objetoEncontrado.PersonaRecibio = parametro.PersonaRecibio;
        //        objetoEncontrado.PersonaSurtio = parametro.PersonaSurtio;
        //        var modelo = _Mapper.Map<AlmacenSalida>(objetoEncontrado);
        //        respuesta.Estatus = await _Repositorio.Editar(modelo);
        //        if (!respuesta.Estatus)
        //        {
        //            respuesta.Estatus = false;
        //            respuesta.Descripcion = "No se pudo editar";
        //            return respuesta;
        //        }
        //        respuesta.Estatus = true;
        //        respuesta.Descripcion = "Salida de almacén editada";
        //        return respuesta;
        //    }
        //    catch (Exception ex)
        //    {
        //        respuesta.Estatus = false;
        //        respuesta.Descripcion = "Algo salió mal en la edición de la salida de almacén";
        //        return respuesta;
        //    }
        //}
        public async Task<RespuestaDTO> Autorizar(int Id)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await ObtenXId(Id);
                if (objetoEncontrado == null || objetoEncontrado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La salida de almacén no existe";
                    return respuesta;
                }
                if (objetoEncontrado.Estatus != 1)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Solo se puede editar si esta capturado";
                    return respuesta;
                }
                objetoEncontrado.Estatus = 2;
                var modelo = _Mapper.Map<AlmacenSalida>(objetoEncontrado);
                respuesta.Estatus = await _Repositorio.Editar(modelo);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo autorizar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Salida de almacén autorizada";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la edición de la salida de almacén";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> Cancelar(int Id)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await ObtenXId(Id);
                if (objetoEncontrado == null || objetoEncontrado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La salida de almacén no existe";
                    return respuesta;
                }
                if (objetoEncontrado.Estatus != 1)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "Solo se puede editar si esta capturado";
                    return respuesta;
                }
                objetoEncontrado.Estatus = 3;
                var modelo = _Mapper.Map<AlmacenSalida>(objetoEncontrado);
                respuesta.Estatus = await _Repositorio.Editar(modelo);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo autorizar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Salida de almacén autorizada";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la edición de la salida de almacén";
                return respuesta;
            }
        }
        public async Task<List<AlmacenSalidaDTO>> ObtenTodos()
        {
            try
            {
                var query = await _Repositorio.ObtenerTodos();
                return _Mapper.Map<List<AlmacenSalidaDTO>>(query);
            }
            catch (Exception ex)
            {
                return new List<AlmacenSalidaDTO>();
            }
        }
        public async Task<List<AlmacenSalidaDTO>> ObtenXIdAlmacen(int IdAlmacen)
        {
            try
            {
                var query = await _Repositorio.ObtenerTodos(z => z.IdAlmacen == IdAlmacen);
                return _Mapper.Map<List<AlmacenSalidaDTO>>(query);
            }
            catch (Exception ex)
            {
                return new List<AlmacenSalidaDTO>();
            }
        }

        public async Task<List<AlmacenSalidaDTO>> ObtenXIdProyecto(int IdProyecto)
        {
            try
            {
                var query = await _Repositorio.ObtenerTodos(z => z.IdAlmacenNavigation.IdProyecto == IdProyecto);
                return _Mapper.Map<List<AlmacenSalidaDTO>>(query);
            }
            catch (Exception ex)
            {
                return new List<AlmacenSalidaDTO>();
            }
        }

        public async Task<AlmacenSalidaDTO> ObtenXId(int Id)
        {
            try
            {
                var query = await _Repositorio.Obtener(z => z.Id == Id);
                return _Mapper.Map<AlmacenSalidaDTO>(query);
            }
            catch (Exception ex)
            {
                return new AlmacenSalidaDTO();
            }
        }

        public async Task<AlmacenSalidaDTO> CrearYObtener(AlmacenSalidaDTO parametro)
        {
            try
            {
                var creacion = await _Repositorio.Crear(_Mapper.Map<AlmacenSalida>(parametro));

                if (creacion.Id == 0)
                    throw new TaskCanceledException("No se pudo crear");

                return _Mapper.Map<AlmacenSalidaDTO>(creacion);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
                return new AlmacenSalidaDTO();
            }
        }

        public Task<RespuestaDTO> Crear(AlmacenSalidaCreacionDTO parametro)
        {
            throw new NotImplementedException();
        }

        public Task<AlmacenSalidaDTO> CrearYObtener(AlmacenSalidaCreacionDTO parametro)
        {
            throw new NotImplementedException();
        }

        public async Task<RespuestaDTO> Editar(AlmacenSalidaDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();

            var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == parametro.Id);
            if (objetoEncontrado.Id <= 0) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "La salida de almacén no existe";
                return respuesta;
            }

            objetoEncontrado.PersonaRecibio = parametro.PersonaRecibio;
            objetoEncontrado.Observaciones = parametro.Observaciones;
            var editarObjeto = await _Repositorio.Editar(objetoEncontrado);
            if (!editarObjeto)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "La salida de almacén no se actualizó";
                return respuesta;
            }
            respuesta.Estatus = true;
            respuesta.Descripcion = "La salida de almacén se actualizó";
            return respuesta;
        }
    }
}
