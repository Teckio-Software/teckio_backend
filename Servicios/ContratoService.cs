using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class ContratoService<T> : IContratoService<T> where T : DbContext
    {
        private readonly IGenericRepository<Contrato, T> _Repositorio;
        private readonly IMapper _Mapper;

        public ContratoService(IGenericRepository<Contrato, T> repositorio
            , IMapper mapper)
        {
            _Repositorio = repositorio;
            _Mapper = mapper;
        }

        public async Task<ContratoDTO> CrearYObtener(ContratoDTO modelo)
        {
            try
            {
                modelo.FechaRegistro = DateTime.Now;
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<Contrato>(modelo));
                if (objetoCreado.Id == 0)
                    throw new TaskCanceledException("No se pudó crear");
                return _Mapper.Map<ContratoDTO>(objetoCreado);
            }
            catch
            {
                return new ContratoDTO();
            }
        }

        public async Task<RespuestaDTO> Editar(ContratoDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var modelo = _Mapper.Map<Contrato>(parametro);
                var objetoEncontrado = await _Repositorio.Obtener(u => u.Id == modelo.Id);
                if (objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El contrato no existe";
                    return respuesta;
                }
                objetoEncontrado.TipoContrato = modelo.TipoContrato;
                objetoEncontrado.NumeroDestajo = modelo.NumeroDestajo;
                objetoEncontrado.Estatus = modelo.Estatus;
                objetoEncontrado.IdProyecto = modelo.IdProyecto;
                objetoEncontrado.CostoDestajo = modelo.CostoDestajo;
                objetoEncontrado.IdContratista = modelo.IdContratista;
                respuesta.Estatus = await _Repositorio.Editar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo editar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Contrato contrato editado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la edición del contrato";
                return respuesta;
            }
        }

        public async Task<RespuestaDTO> Eliminar(int Id)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var objetoEncontrado = await _Repositorio.Obtener(u => u.Id == Id);

                if (objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El contrato no existe";
                    return respuesta;
                }
                respuesta.Estatus = await _Repositorio.Eliminar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Descripcion = "No se pudo eliminar";
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Contrato eliminado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la eliminación del detalle";
                return respuesta;
            }
        }

        public async Task<List<ContratoDTO>> ObtenerRegistrosXIdProyecto(int IdProyecto)
        {
            var query = await _Repositorio.ObtenerTodos(z => z.IdProyecto == IdProyecto);
            return _Mapper.Map<List<ContratoDTO>>(query);
        }

        public async Task<ContratoDTO> ObtenXId(int Id)
        {
            try
            {
                var query = await _Repositorio.Obtener(z => z.Id == Id);
                return _Mapper.Map<ContratoDTO>(query);
            }
            catch
            {
                return new ContratoDTO();
            }
        }
    }
}
