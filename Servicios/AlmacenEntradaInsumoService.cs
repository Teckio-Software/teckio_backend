using AutoMapper;
using ERP_TECKIO.Modelos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO
{
    public class AlmacenEntradaInsumoService<T> : IInsumoXAlmacenEntradaService<T> where T : DbContext
    {
        private readonly IGenericRepository<AlmacenEntradaInsumo, T> _Repositorio;
        //private readonly PROCOMIContext _Context;
        private readonly IMapper _Mapper;

        public AlmacenEntradaInsumoService(
            IGenericRepository<AlmacenEntradaInsumo, T> repositorio
            //, PROCOMIContext context
            , IMapper mapper)
        {
            _Repositorio = repositorio;
            //_Context = context;
            _Mapper = mapper;
        }
        //public async Task<RespuestaDTO> Crear(AlmacenEntradaInsumoCreacionDTO parametro)
        //{
        //    RespuestaDTO respuesta = new RespuestaDTO();
        //    try
        //    {
        //        if (parametro.IdRequisicion <= 0 || parametro.IdProyecto <= 0)
        //        {
        //            respuesta.Estatus = false;
        //            respuesta.Descripcion = "No hay una requisición asociada";
        //            return respuesta;
        //        }
        //        var objetoCreado = await _Repositorio.Crear(_Mapper.Map<AlmacenEntradaInsumo>(parametro));
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
        //        respuesta.Descripcion = "Algo salió mal en la creación de la requisición";
        //        return respuesta;
        //    }
        //}

        //public async Task<AlmacenEntradaInsumoDTO> CrearYObtener(AlmacenEntradaInsumoCreacionDTO parametro)
        //{
        //    try
        //    {
        //        if (parametro.IdRequisicion <= 0 || parametro.IdProyecto <= 0)
        //        {
        //            throw new TaskCanceledException("Capture todos los campos");
        //        }
        //        var objetoCreado = await _Repositorio.Crear(_Mapper.Map<AlmacenEntradaInsumo>(parametro));
        //        if (objetoCreado.Id <= 0)
        //        {
        //            throw new TaskCanceledException("No se pudó crear");
        //        }
        //        return _Mapper.Map<AlmacenEntradaInsumoDTO>(objetoCreado);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new AlmacenEntradaInsumoDTO();
        //    }
        //}

        public async Task<RespuestaDTO> Editar(AlmacenEntradaInsumoDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await ObtenXId(parametro.Id);
                if (objetoEncontrado == null || objetoEncontrado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El insumo no existe";
                    return respuesta;
                }
                //Entrada del insumo cancelado
                if (objetoEncontrado.Estatus == 2)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El insumo esta cancelado";
                    return respuesta;
                }
                objetoEncontrado.IdProyecto = parametro.IdProyecto;
                objetoEncontrado.IdRequisicion = parametro.IdRequisicion;
                objetoEncontrado.IdOrdenCompra = parametro.IdOrdenCompra;
                objetoEncontrado.IdAlmacenEntrada = parametro.IdAlmacenEntrada;
                objetoEncontrado.IdInsumo = parametro.IdInsumo;
                objetoEncontrado.CantidadPorRecibir = parametro.CantidadPorRecibir;
                objetoEncontrado.CantidadRecibida = parametro.CantidadRecibida;
                var modelo = _Mapper.Map<AlmacenEntradaInsumo>(objetoEncontrado);
                respuesta.Estatus = await _Repositorio.Editar(modelo);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo editar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Insumo editado";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal al editar el insumo de la entrada del almacén";
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
                    respuesta.Descripcion = "El insumo no existe";
                    return respuesta;
                }
                //Entrada del insumo cancelado
                if (objetoEncontrado.Estatus == 2)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El insumo ya esta cancelado";
                    return respuesta;
                }
                objetoEncontrado.IdProyecto = 2;
                var modelo = _Mapper.Map<AlmacenEntradaInsumo>(objetoEncontrado);
                respuesta.Estatus = await _Repositorio.Editar(modelo);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo editar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Insumo cancelado";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal al editar el insumo de la entrada del almacén";
                return respuesta;
            }
        }
        public async Task<List<AlmacenEntradaInsumoDTO>> ObtenTodos()
        {
            try
            {
                var query = await _Repositorio.ObtenerTodos();
                return _Mapper.Map<List<AlmacenEntradaInsumoDTO>>(query);
            }
            catch (Exception ex)
            {
                return new List<AlmacenEntradaInsumoDTO>();
            }
        }

        public async Task<AlmacenEntradaInsumoDTO> ObtenXId(int Id)
        {
            try
            {
                var query = await _Repositorio.Obtener(z => z.Id == Id);
                return _Mapper.Map<AlmacenEntradaInsumoDTO>(query);
            }
            catch (Exception ex)
            {
                return new AlmacenEntradaInsumoDTO();
            }
        }

        public async Task<List<AlmacenEntradaInsumoDTO>> ObtenXIdAlmacenEntrada(int IdAlmacenEntrada)
        {
            try
            {
                var query = await _Repositorio.ObtenerTodos(z => z.IdAlmacenEntrada == IdAlmacenEntrada);
                return _Mapper.Map<List<AlmacenEntradaInsumoDTO>>(query);
            }
            catch (Exception ex)
            {
                return new List<AlmacenEntradaInsumoDTO>();
            }
        }

        public Task<RespuestaDTO> Crear(AlmacenEntradaInsumoCreacionDTO parametro)
        {
            throw new NotImplementedException();
        }

        public async Task<AlmacenEntradaInsumoDTO> CrearYObtener(AlmacenEntradaInsumoDTO parametro)
        {
            var respuesta = await _Repositorio.Crear(_Mapper.Map<AlmacenEntradaInsumo>(parametro));
            return _Mapper.Map<AlmacenEntradaInsumoDTO>(respuesta);
        }

        public async Task<bool> CrearLista(List<AlmacenEntradaInsumoDTO> parametro)
        {
            var objetosMapaeados = _Mapper.Map<List<AlmacenEntradaInsumo>>(parametro);
            return await _Repositorio.CrearMultiple(objetosMapaeados);
        }
    }
}
