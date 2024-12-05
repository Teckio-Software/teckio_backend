using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ERP_TECKIO.Modelos;



namespace ERP_TECKIO.Servicios
{
    public class DetalleXContratoService<T> : IDetalleXContratoService<T> where T : DbContext
    {
        private readonly IGenericRepository<DetalleXContrato, T> _Repositorio;
        private readonly IMapper _Mapper;

        public DetalleXContratoService(IGenericRepository<DetalleXContrato, T> repositorio
            , IMapper mapper)
        {
            _Repositorio = repositorio;
            _Mapper = mapper;
        }

        public async Task<DetalleXContratoDTO> CrearYObtener(DetalleXContratoDTO modelo)
        {
            try
            {
                var objetoCreado = await _Repositorio.Crear(_Mapper.Map<DetalleXContrato>(modelo));
                if (objetoCreado.Id == 0)
                    throw new TaskCanceledException("No se pudó crear");
                return _Mapper.Map<DetalleXContratoDTO>(objetoCreado);
            }
            catch
            {
                return new DetalleXContratoDTO();
            }
        }

        public async Task<RespuestaDTO> Editar(DetalleXContratoDTO parametro)
        {
            RespuestaDTO respuesta = new RespuestaDTO();
            try
            {
                var modelo = _Mapper.Map<DetalleXContrato>(parametro);
                var objetoEncontrado = await _Repositorio.Obtener(u => u.Id == modelo.Id);
                if (objetoEncontrado == null)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "El insumo no existe";
                    return respuesta;
                }
                objetoEncontrado.IdPrecioUnitario = modelo.IdPrecioUnitario;
                objetoEncontrado.IdContrato = modelo.IdContrato;
                objetoEncontrado.PorcentajeDestajo = modelo.PorcentajeDestajo;
                objetoEncontrado.ImporteDestajo = modelo.ImporteDestajo;
                objetoEncontrado.FactorDestajo = modelo.FactorDestajo;
                respuesta.Estatus = await _Repositorio.Editar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Estatus = false;
                    respuesta.Descripcion = "No se pudo editar";
                    return respuesta;
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Detalle contrato editado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la edición del detalle contrato";
                return respuesta;
            }
        }

        public async Task<bool> EditarMultiple(List<DetalleXContratoDTO> lista)
        {
            var objetos = _Mapper.Map<List<DetalleXContrato>>(lista);
            return await _Repositorio.EditarMultiple(objetos);
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
                    respuesta.Descripcion = "El detalle no existe";
                    return respuesta;
                }
                respuesta.Estatus = await _Repositorio.Eliminar(objetoEncontrado);
                if (!respuesta.Estatus)
                {
                    respuesta.Descripcion = "No se pudo eliminar";
                }
                respuesta.Estatus = true;
                respuesta.Descripcion = "Detalle eliminado";
                return respuesta;
            }
            catch
            {
                respuesta.Estatus = false;
                respuesta.Descripcion = "Algo salió mal en la eliminación del detalle";
                return respuesta;
            }
        }

        public async Task<List<DetalleXContratoDTO>> ObtenerRegistrosXIdContrato(int IdContrato)
        {
            var query = await _Repositorio.ObtenerTodos(z => z.IdContrato == IdContrato);
            return _Mapper.Map<List<DetalleXContratoDTO>>(query);
        }

        public async Task<List<DetalleXContratoDTO>> ObtenerRegistrosXIdPrecioUnitario(int IdPrecioUnitario)
        {
            var query = await _Repositorio.ObtenerTodos(z => z.IdPrecioUnitario == IdPrecioUnitario);
            return _Mapper.Map<List<DetalleXContratoDTO>>(query);

        }

        public async Task<DetalleXContratoDTO> ObtenXId(int Id)
        {
            try
            {
                var query = await _Repositorio.Obtener(z => z.Id == Id);
                return _Mapper.Map<DetalleXContratoDTO>(query);
            }
            catch
            {
                return new DetalleXContratoDTO();
            }
        }
    }
}