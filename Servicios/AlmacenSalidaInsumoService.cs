using AutoMapper;
using ERP_TECKIO.Modelos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO
{
    public class AlmacenSalidaInsumoService<T> : IInsumoXAlmacenSalidaService<T> where T : DbContext
    {
        private readonly IGenericRepository<AlmacenSalidaInsumo, T> _Repositorio;
        //private readonly PROCOMIContext _Context;
        private readonly IMapper _Mapper;

        public AlmacenSalidaInsumoService(
            IGenericRepository<AlmacenSalidaInsumo, T> repositorio
            //, PROCOMIContext context
            , IMapper mapper)
        {
            _Repositorio = repositorio;
            //_Context = context;
            _Mapper = mapper;
        }
        //public async Task<RespuestaDTO> Crear(AlmacenSalidaInsumoCreacionDTO parametro)
        //{
        //    RespuestaDTO respuesta = new RespuestaDTO();
        //    try
        //    {
        //        if (parametro.IdProyecto <= 0 || parametro.IdAlmacenSalida <= 0
        //            || parametro.IdInsumoExistencia <= 0 || parametro.IdInsumo <= 0)
        //        {
        //            respuesta.Estatus = false;
        //            respuesta.Descripcion = "No hay una requisición asociada";
        //            return respuesta;
        //        }
        //        parametro.Estatus = 1;
        //        var objetoCreado = await _Repositorio.Crear(_Mapper.Map<AlmacenSalidaInsumo>(parametro));
        //        if (objetoCreado.Id <= 0)
        //        {
        //            respuesta.Estatus = false;
        //            respuesta.Descripcion = "No se pudó crear";
        //            return respuesta;
        //        }
        //        respuesta.Estatus = true;
        //        respuesta.Descripcion = "Entrada del insumo creado";
        //        return respuesta;
        //    }
        //    catch (Exception ex)
        //    {
        //        respuesta.Estatus = false;
        //        respuesta.Descripcion = "Algo salió mal en la creación del insumo en la salida del almacén";
        //        return respuesta;
        //    }
        //}

        //public async Task<AlmacenSalidaInsumoDTO> CrearYObtener(AlmacenSalidaInsumoCreacionDTO parametro)
        //{
        //    try
        //    {
        //        if (parametro.IdProyecto <= 0 || parametro.IdAlmacenSalida <= 0
        //            || parametro.IdInsumoExistencia <= 0 || parametro.IdInsumo <= 0)
        //        {
        //            throw new TaskCanceledException("Capture todos los campos");
        //        }
        //        parametro.Estatus = 1;
        //        var objetoCreado = await _Repositorio.Crear(_Mapper.Map<AlmacenSalidaInsumo>(parametro));
        //        if (objetoCreado.Id <= 0)
        //        {
        //            throw new TaskCanceledException("No se pudó crear");
        //        }
        //        return _Mapper.Map<AlmacenSalidaInsumoDTO>(objetoCreado);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new AlmacenSalidaInsumoDTO();
        //    }
        //}

        //public async Task<RespuestaDTO> Editar(AlmacenSalidaInsumoDTO parametro)
        //{
        //    RespuestaDTO respuesta = new RespuestaDTO();
        //    try
        //    {
        //        var objetoEncontrado = await ObtenXId(parametro.Id);
        //        if (objetoEncontrado == null || objetoEncontrado.Id <= 0)
        //        {
        //            respuesta.Estatus = false;
        //            respuesta.Descripcion = "El insumo no existe";
        //            return respuesta;
        //        }
        //        //Salida del insumo cancelado
        //        if (objetoEncontrado.Estatus == 3)
        //        {
        //            respuesta.Estatus = false;
        //            respuesta.Descripcion = "El insumo esta cancelado";
        //            return respuesta;
        //        }
        //        objetoEncontrado.IdProyecto = parametro.IdProyecto;
        //        objetoEncontrado.IdAlmacenSalida = parametro.IdAlmacenSalida;
        //        objetoEncontrado.IdInsumoExistencia = parametro.IdInsumoExistencia;
        //        objetoEncontrado.IdInsumo = parametro.IdInsumo;
        //        objetoEncontrado.CantidadPorSalir = parametro.CantidadPorSalir;
        //        var modelo = _Mapper.Map<AlmacenSalidaInsumo>(objetoEncontrado);
        //        respuesta.Estatus = await _Repositorio.Editar(modelo);
        //        if (!respuesta.Estatus)
        //        {
        //            respuesta.Estatus = false;
        //            respuesta.Descripcion = "No se pudo editar";
        //            return respuesta;
        //        }
        //        respuesta.Estatus = true;
        //        respuesta.Descripcion = "Insumo editado";
        //        return respuesta;
        //    }
        //    catch (Exception ex)
        //    {
        //        respuesta.Estatus = false;
        //        respuesta.Descripcion = "Algo salió mal al editar el insumo de la salida del almacén";
        //        return respuesta;
        //    }
        //}
        //public async Task<RespuestaDTO> Autorizar(int Id)
        //{
        //    RespuestaDTO respuesta = new RespuestaDTO();
        //    try
        //    {
        //        var objetoEncontrado = await ObtenXId(Id);
        //        if (objetoEncontrado == null || objetoEncontrado.Id <= 0)
        //        {
        //            respuesta.Estatus = false;
        //            respuesta.Descripcion = "El insumo no existe";
        //            return respuesta;
        //        }
        //        //Salida del insumo cancelado
        //        if (objetoEncontrado.Estatus == 3)
        //        {
        //            respuesta.Estatus = false;
        //            respuesta.Descripcion = "El insumo esta cancelado";
        //            return respuesta;
        //        }
        //        objetoEncontrado.Estatus = 2;
        //        var modelo = _Mapper.Map<AlmacenSalidaInsumo>(objetoEncontrado);
        //        respuesta.Estatus = await _Repositorio.Editar(modelo);
        //        if (!respuesta.Estatus)
        //        {
        //            respuesta.Estatus = false;
        //            respuesta.Descripcion = "No se pudo autorizar";
        //            return respuesta;
        //        }
        //        respuesta.Estatus = true;
        //        respuesta.Descripcion = "Insumo autorizado";
        //        return respuesta;
        //    }
        //    catch (Exception ex)
        //    {
        //        respuesta.Estatus = false;
        //        respuesta.Descripcion = "Algo salió mal al autorizar el insumo de la salida del almacén";
        //        return respuesta;
        //    }
        //}

        //public async Task<RespuestaDTO> Cancelar(int Id)
        //{
        //    RespuestaDTO respuesta = new RespuestaDTO();
        //    try
        //    {
        //        var objetoEncontrado = await ObtenXId(Id);
        //        if (objetoEncontrado == null || objetoEncontrado.Id <= 0)
        //        {
        //            respuesta.Estatus = false;
        //            respuesta.Descripcion = "El insumo no existe";
        //            return respuesta;
        //        }
        //        //Salida del insumo cancelado
        //        if (objetoEncontrado.Estatus == 3)
        //        {
        //            respuesta.Estatus = false;
        //            respuesta.Descripcion = "El insumo ya esta cancelado";
        //            return respuesta;
        //        }
        //        objetoEncontrado.Estatus = 3;
        //        var modelo = _Mapper.Map<AlmacenSalidaInsumo>(objetoEncontrado);
        //        respuesta.Estatus = await _Repositorio.Editar(modelo);
        //        if (!respuesta.Estatus)
        //        {
        //            respuesta.Estatus = false;
        //            respuesta.Descripcion = "No se pudo cancelar";
        //            return respuesta;
        //        }
        //        respuesta.Estatus = true;
        //        respuesta.Descripcion = "Insumo editado";
        //        return respuesta;
        //    }
        //    catch (Exception ex)
        //    {
        //        respuesta.Estatus = false;
        //        respuesta.Descripcion = "Algo salió mal al editar el insumo de la salida del almacén";
        //        return respuesta;
        //    }
        //}
        public async Task<List<AlmacenSalidaInsumoDTO>> ObtenTodos()
        {
            try
            {
                var query = await _Repositorio.ObtenerTodos();
                return _Mapper.Map<List<AlmacenSalidaInsumoDTO>>(query);
            }
            catch (Exception ex)
            {
                return new List<AlmacenSalidaInsumoDTO>();
            }
        }

        public async Task<List<AlmacenSalidaInsumoDTO>> ObtenXIdAlmacenSalida(int IdAlmacenSalida)
        {
            try
            {
                var query = await _Repositorio.ObtenerTodos(z => z.IdAlmacenSalida == IdAlmacenSalida);
                return _Mapper.Map<List<AlmacenSalidaInsumoDTO>>(query);
            }
            catch (Exception ex)
            {
                return new List<AlmacenSalidaInsumoDTO>();
            }
        }
        public async Task<AlmacenSalidaInsumoDTO> ObtenXId(int Id)
        {
            try
            {
                var query = await _Repositorio.Obtener(z => z.Id == Id);
                return _Mapper.Map<AlmacenSalidaInsumoDTO>(query);
            }
            catch (Exception ex)
            {
                return new AlmacenSalidaInsumoDTO();
            }
        }

        public async Task<bool> CrearLista(List<AlmacenSalidaInsumoDTO> parametro)
        {
            var objetosMapaeados = _Mapper.Map<List<AlmacenSalidaInsumo>>(parametro);
            return await _Repositorio.CrearMultiple(objetosMapaeados);
        }

        public async Task<RespuestaDTO> Crear(AlmacenSalidaInsumoCreacionDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objeto = _Mapper.Map<AlmacenSalidaInsumo>(parametro);
                var resultado = await _Repositorio.Crear(objeto);
                if (resultado.Id > 0)
                {
                    respuesta.Estatus = true;
                    respuesta.Descripcion = "Salida del insumo creada exitosamente";
                }
                else
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo crear la salida del insumo";
                }
                return respuesta;

            }
            catch
            {
                respuesta.Descripcion = "No se pudo crear la salida del insumo";
                respuesta.Estatus = false;
                return respuesta;
            }
            
        }

        public async Task<AlmacenSalidaInsumoDTO> CrearYObtener(AlmacenSalidaInsumoDTO parametro)
        {
            var objeto = await _Repositorio.Crear(_Mapper.Map<AlmacenSalidaInsumo>(parametro));
            return _Mapper.Map<AlmacenSalidaInsumoDTO>(objeto);
        }

        public async Task<RespuestaDTO> Editar(AlmacenSalidaInsumoDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objeto = await _Repositorio.Obtener(z => z.Id == parametro.Id);
                if(objeto.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se encontró la salida del insumo";
                    return respuesta;
                }
                objeto.CantidadPorSalir = parametro.CantidadPorSalir;
                objeto.IdInsumo = parametro.IdInsumo;
                objeto.IdAlmacenSalida = parametro.IdAlmacenSalida;
                objeto.IdProyecto = parametro.IdProyecto;
                objeto.EstatusInsumo = parametro.EstatusInsumo;
                objeto.PersonaRecibio = parametro.PersonaRecibio;
                objeto.EsPrestamo = parametro.EsPrestamo;
                objeto.PrestamoFinalizado = parametro.PrestamoFinalizado;
                objeto.PersonaRecibio = parametro.PersonaRecibio;
                objeto.PrestamoFinalizado = parametro.PrestamoFinalizado;
                var resultado = await _Repositorio.Editar(objeto);
                if (resultado)
                {
                    respuesta.Estatus = true;
                    respuesta.Descripcion = "Salida del insumo editada exitosamente";
                }
                else
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo editar la salida del insumo";
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal al editar el insumo de la salida del almacén";
                return respuesta;
            }
        }

        public Task<RespuestaDTO> Cancelar(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<RespuestaDTO> Autorizar(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> EditarLista(List<AlmacenSalidaInsumoDTO> parametro)
        {
            var objetosMapaeados = _Mapper.Map<List<AlmacenSalidaInsumo>>(parametro);
            return await _Repositorio.EditarMultiple(objetosMapaeados);
        }
    }
}
