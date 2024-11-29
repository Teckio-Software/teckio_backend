using AutoMapper;
using ERP_TECKIO.Modelos;
using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;

namespace ERP_TECKIO
{
    public class AlmacenEntradaService<T> : IAlmacenEntradaService<T> where T : DbContext
    {
        private readonly IGenericRepository<AlmacenEntrada, T> _Repositorio;
        //private readonly PROCOMIContext _Context;
        private readonly IMapper _Mapper;
        public AlmacenEntradaService(IGenericRepository<AlmacenEntrada, T> repositorio
            , IMapper mapper)
        {
            _Repositorio = repositorio;
            //_Context = context;
            _Mapper = mapper;
        }
        //public async Task<RespuestaDTO> Crear(AlmacenEntradaCreacionDTO parametro)
        //{
        //    RespuestaDTO respuesta = new RespuestaDTO();
        //    try
        //    {
        //        if (string.IsNullOrEmpty(parametro.NoEntrada) || parametro.IdProyecto <= 0)
        //        {
        //            respuesta.Estatus = false;
        //            respuesta.Descripcion = "No hay un proyecto asociado";
        //            return respuesta;
        //        }
        //        if (parametro.IdProyecto <= 0 || parametro.IdAlmacen <= 0
        //            || parametro.IdContratista <= 0 || string.IsNullOrEmpty(parametro.PersonaRegistra))
        //        {
        //            respuesta.Estatus = false;
        //            respuesta.Descripcion = "Capture toda la información";
        //            return respuesta;
        //        }
        //        parametro.FechaRegistro = DateTime.Now;
        //        var objetoCreado = await _Repositorio.Crear(_Mapper.Map<AlmacenEntrada>(parametro));
        //        if (objetoCreado.Id <= 0)
        //        {
        //            respuesta.Estatus = false;
        //            respuesta.Descripcion = "No se pudó crear";
        //            return respuesta;
        //        }
        //        respuesta.Estatus = true;
        //        respuesta.Descripcion = "Entrada de almacén creada";
        //        return respuesta;
        //    }
        //    catch (Exception ex)
        //    {
        //        respuesta.Estatus = false;
        //        respuesta.Descripcion = "Algo salió mal en la creación de la entrada de almacén";
        //        return respuesta;
        //    }
        //}

        //public async Task<AlmacenEntradaDTO> CrearYObtener(AlmacenEntradaCreacionDTO parametro)
        //{
        //    try
        //    {
        //        if (parametro.IdProyecto <= 0 || parametro.IdAlmacen <= 0
        //            || parametro.IdContratista <= 0 || string.IsNullOrEmpty(parametro.PersonaRegistra)
        //            || string.IsNullOrEmpty(parametro.NoEntrada) || parametro.IdProyecto <= 0)
        //        {
        //            throw new TaskCanceledException("Capture toda la información");
        //        }
        //        parametro.FechaRegistro = DateTime.Now;
        //        var objetoCreado = await _Repositorio.Crear(_Mapper.Map<AlmacenEntrada>(parametro));
        //        if (objetoCreado.Id <= 0)
        //        {
        //            throw new TaskCanceledException("No se pudó crear");
        //        }
        //        return _Mapper.Map<AlmacenEntradaDTO>(objetoCreado);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new AlmacenEntradaDTO();
        //    }
        //}

        public async Task<RespuestaDTO> Editar(AlmacenEntradaDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == parametro.Id);
                if (objetoEncontrado == null || objetoEncontrado.Id <= 0)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "La entrada de almacén no existe";
                    return respuesta;
                }
                //objetoEncontrado.IdAlmacen = parametro.IdAlmacen;
                objetoEncontrado.IdContratista = parametro.IdContratista == 0 ? null : parametro.IdContratista;
                //objetoEncontrado.PersonaRegistra = parametro.PersonaRegistra;
                objetoEncontrado.Observaciones = parametro.Observaciones;
                respuesta.Estatus = await _Repositorio.Editar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo editar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Entrada de almacén editada";
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la edición de la entrada de almacén";
                return respuesta;
            }
        }
        //public async Task<RespuestaDTO> Cancelar(int Id)
        //{
        //    RespuestaDTO respuesta = new RespuestaDTO();
        //    try
        //    {
        //        var objetoEncontrado = await ObtenXId(Id);
        //        if (objetoEncontrado == null || objetoEncontrado.Id <= 0)
        //        {
        //            respuesta.Estatus = false;
        //            respuesta.Descripcion = "La entrada a almacén no existe";
        //            return respuesta;
        //        }
        //        objetoEncontrado.Estatus = 2;
        //        var modelo = _Mapper.Map<AlmacenEntrada>(objetoEncontrado);
        //        respuesta.Estatus = await _Repositorio.Editar(modelo);
        //        if (!respuesta.Estatus)
        //        {
        //            respuesta.Estatus = false;
        //            respuesta.Descripcion = "No se pudo cancelar";
        //            return respuesta;
        //        }
        //        respuesta.Estatus = true;
        //        respuesta.Descripcion = "Entrada a almacén cancelada";
        //        return respuesta;
        //    }
        //    catch (Exception ex)
        //    {
        //        respuesta.Estatus = false;
        //        respuesta.Descripcion = "Algo salió mal en la cancelación de la entrada de almacén";
        //        return respuesta;
        //    }
        //}
        public async Task<List<AlmacenEntradaDTO>> ObtenTodos()
        {
            try
            {
                var query = await _Repositorio.ObtenerTodos();
                return _Mapper.Map<List<AlmacenEntradaDTO>>(query);
            }
            catch (Exception ex)
            {
                return new List<AlmacenEntradaDTO>();
            }
        }

        public async Task<AlmacenEntradaDTO> ObtenXId(int Id)
        {
            try
            {
                var query = await _Repositorio.Obtener(z => z.Id == Id);
                return _Mapper.Map<AlmacenEntradaDTO>(query);
            }
            catch (Exception ex)
            {
                return new AlmacenEntradaDTO();
            }
        }

        public async Task<List<AlmacenEntradaDTO>> ObtenXIdAlmacen(int IdAlmacen)
        {
            try
            {
                var query = await _Repositorio.ObtenerTodos(z => z.IdAlmacen == IdAlmacen);
                return _Mapper.Map<List<AlmacenEntradaDTO>>(query);
            }
            catch (Exception ex)
            {
                return new List<AlmacenEntradaDTO>();
            }
        }

        public Task<RespuestaDTO> Crear(AlmacenEntradaCreacionDTO parametro)
        {
            throw new NotImplementedException();
        }

        public async Task<AlmacenEntradaDTO> CrearYObtener(AlmacenEntradaDTO parametro)
        {
            try
            {
                var creacion = await _Repositorio.Crear(_Mapper.Map<AlmacenEntrada>(parametro));

                if (creacion.Id == 0)
                    throw new TaskCanceledException("No se pudo crear");

                return _Mapper.Map<AlmacenEntradaDTO>(creacion);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
                return new AlmacenEntradaDTO();
            }
        }

        public Task<AlmacenEntradaDTO> CrearYObtener(AlmacenEntradaCreacionDTO parametro)
        {
            throw new NotImplementedException();
        }

        public Task<List<AlmacenEntradaDTO>> ObtenXIdRequisicion(int idRequsicion)
        {
            throw new NotImplementedException();
        }

        public Task<RespuestaDTO> Cancelar(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<AlmacenEntradaDTO>> ObtenXIdProyecto(int IdProyecto)
        {

            throw new NotImplementedException();

        }
    }
}
