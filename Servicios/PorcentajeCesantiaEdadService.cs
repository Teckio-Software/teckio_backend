using AutoMapper;
using ERP_TECKIO.DTO;
using ERP_TECKIO.Modelos;
using ERP_TECKIO.Modelos.Presupuesto;
using ERP_TECKIO.Servicios.Contratos;
using Microsoft.EntityFrameworkCore;

namespace ERP_TECKIO.Servicios
{
    public class PorcentajeCesantiaEdadService<T> : IPorcentajeCesantiaEdadService<T> where T : DbContext
    {
        private readonly IGenericRepository<PorcentajeCesantiaEdad, T> _Repositorio;
        private readonly IMapper _Mapper;
        public PorcentajeCesantiaEdadService(
            IGenericRepository<PorcentajeCesantiaEdad, T> Repositorio,
            IMapper Mapper
            ) {
            _Repositorio = Repositorio;
            _Mapper = Mapper;
        }

        public async Task<RespuestaDTO> Crear(PorcentajeCesantiaEdadDTO registro)
        {
            var respuesta = new RespuestaDTO();
            var objeto = await _Repositorio.Crear(_Mapper.Map<PorcentajeCesantiaEdad>(registro));
            if (objeto.Id <= 0) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se creo el registro";
                return respuesta;
            }
            respuesta.Estatus = true;
            respuesta.Descripcion = "Se creo el registro";
            return respuesta;
        }

        public Task<PorcentajeCesantiaEdadDTO> CrearYObtener(PorcentajeCesantiaEdadDTO registro)
        {
            throw new NotImplementedException();
        }

        public async Task<RespuestaDTO> Editar(PorcentajeCesantiaEdadDTO registro)
        {
            var respuesta = new RespuestaDTO();
            var objetoEncontrado = await _Repositorio.Obtener(z => z.Id == registro.Id);
            if (objetoEncontrado.Id <= 0) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se encontro el registro";
                return respuesta;
            }

            objetoEncontrado.RangoUMA = registro.RangoUMA;
            objetoEncontrado.Porcentaje = registro.Porcentaje;
            var editar = await _Repositorio.Editar(objetoEncontrado);
            if (!editar) {
                respuesta.Estatus = false;
                respuesta.Descripcion = "No se edito el registro";
                return respuesta;
            }

            respuesta.Estatus = true;
            respuesta.Descripcion = "Se edito el registro";
            return respuesta;
        }

        public Task<RespuestaDTO> Eliminar(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PorcentajeCesantiaEdadDTO>> ObtenerXIdProyecto(int IdProyecto)
        {
            var objetos = await _Repositorio.ObtenerTodos(z => z.IdProyecto == IdProyecto);
            return _Mapper.Map<List<PorcentajeCesantiaEdadDTO>>(objetos);
        }

        public Task<PorcentajeCesantiaEdadDTO> ObtenXId(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
