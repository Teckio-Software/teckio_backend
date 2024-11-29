using AutoMapper;
using Microsoft.EntityFrameworkCore;using SistemaERP.BLL.Procesos;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class PorcentajeAcumuladoContratoService<T> : IPorcentajeAcumuladoContratoService<T> where T : DbContext
    {
        private readonly IGenericRepository<PorcentajeAcumuladoContrato, T> _Repositorio;
        private readonly IMapper _Mapper;

        public PorcentajeAcumuladoContratoService(IGenericRepository<PorcentajeAcumuladoContrato, T> repositorio
            , IMapper mapper)
        {
            _Repositorio = repositorio;
            _Mapper = mapper;
        }

        public async Task<PorcentajeAcumuladoContratoDTO> CrearYObtener(PorcentajeAcumuladoContratoDTO modelo)
        {
            try
            {
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<PorcentajeAcumuladoContrato>(modelo));
                if (objetoCreado.Id == 0)
                    throw new TaskCanceledException("No se pudó crear");
                return _Mapper.Map<PorcentajeAcumuladoContratoDTO>(objetoCreado);
            }
            catch
            {
                return new PorcentajeAcumuladoContratoDTO();
            }
        }

        public async Task<RespuestaDTO> Editar(PorcentajeAcumuladoContratoDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var modelo = _Mapper.Map<PorcentajeAcumuladoContrato>(parametro);
                var objetoEncontrado = await _Repositorio.Obtener(u => u.Id == modelo.Id);
                if (objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El registro no existe";
                    return respuesta;
                }
                objetoEncontrado.PorcentajeDestajoAcumulado = modelo.PorcentajeDestajoAcumulado;
                objetoEncontrado.IdPrecioUnitario = modelo.IdPrecioUnitario;
                respuesta.Estatus = await _Repositorio.Editar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo editar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "registro contrato editado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la edición del registro";
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
                    respuesta.Descripcion = "El registro no existe";
                    return respuesta;
                }
                respuesta.Estatus = await _Repositorio.Eliminar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Descripcion = "No se pudo eliminar";
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Registro eliminado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la eliminación del detalle";
                return respuesta;
            }
        }

        public async Task<List<PorcentajeAcumuladoContratoDTO>> ObtenerRegistros()
        {
            var query = await _Repositorio.ObtenerTodos();
            return _Mapper.Map<List<PorcentajeAcumuladoContratoDTO>>(query);
        }

        public async Task<PorcentajeAcumuladoContratoDTO> ObtenXId(int Id)
        {
            try
            {
                var query = await _Repositorio.Obtener(z => z.Id == Id);
                return _Mapper.Map<PorcentajeAcumuladoContratoDTO>(query);
            }
            catch
            {
                return new PorcentajeAcumuladoContratoDTO();
            }
        }
    }
}
